using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.ComponentModel;
using YNetLib_45;

namespace DD2015_45 
{
  [DataObject]
  public class DAC_rcntd : DAC
  {
    private clsFN daoFN = new clsFN();



    public DAC_rcntd()
      : base()
    {
    }

    public DAC_rcntd(OleDbConnection conn)
      : base(conn)
    {
    }


    /// <summary>
    /// SELECT TABLE rcntd ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_rcntd(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrcntd", "rcntd", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    public DataTable SelectTableForTextEdit_rcntd(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrcntd", "rcntd", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }

    /// <summary>
    /// InsertTable rcntd
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public string InsertTable_rcntd(string rcntd_BDCCN, decimal rcntd_BDDCX, decimal rcntd_BDQTY, decimal rcntd_BDRAT, string rcntd_BDRMK, string rcntd_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      DataTable tb_rcntd_ins = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection Conni = NewReaderConnr();
      //
      Conni.Open();
      CmdQueryObj.CommandText = " and 1=0";
      //
      DbDataAdapter da_ADP_ins = GetDataAdapter(PublicVariable.ApVer, "UNrcntd", "rcntd", "", false, "", CmdQueryObj, "", "", "SEL INS");
      da_ADP_ins.SelectCommand.Connection = Conni;
      da_ADP_ins.InsertCommand.Connection = Conni;
      da_ADP_ins.Fill(tb_rcntd_ins);
      OleDbTransaction thistran = Conni.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP_ins.InsertCommand.Transaction = thistran;
      try
      {
        DataRow ins_row = tb_rcntd_ins.NewRow();
        ins_row["rcntd_gkey"] = rcntd_actkey;    // 
        ins_row["rcntd_mkey"] = rcntd_actkey;    //
        //
        ins_row["rcntd_BDCCN"] = rcntd_BDCCN;       // 合約編號
        ins_row["rcntd_BDDCX"] = rcntd_BDDCX;       // 折扣比率
        ins_row["rcntd_BDQTY"] = rcntd_BDQTY;      // 銷售數量
        ins_row["rcntd_BDRAT"] = rcntd_BDRAT;       // 版稅比率
        ins_row["rcntd_BDRMK"] = rcntd_BDRMK;       // 備　　註
        //
        ins_row["rcntd_trusr"] = UserGkey;  //
        tb_rcntd_ins.Rows.Add(ins_row);
        //
        da_ADP_ins.Update(tb_rcntd_ins);
        Insertbalog(Conni, thistran, "rcntd", rcntd_actkey, UserGkey);
        Insertbtlog(Conni, thistran, "rcntd", DAC.GetStringValue(ins_row["rcntd_BDCCN"]) + " " + DAC.GetStringValue(ins_row["rcntd_BDDCX"]), "I", UserGkey, DAC.GetStringValue(ins_row["rcntd_BDRAT"]).ToString() + " " + DAC.GetStringValue(ins_row["rcntd_BDRMK"]));
        thistran.Commit();
      }
      catch (Exception e)
      {
        thistran.Rollback();
        st_dberrmsg = e.Message;
      }
      finally
      {
        thistran.Dispose();
        tb_rcntd_ins.Dispose();
        CmdQueryObj.Dispose();
        da_ADP_ins.Dispose();
        Conni.Close();
        //
      }
      return st_dberrmsg;
    }

    /// <summary>
    /// DELETE Table rcntd
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public string DeleteTable_rcntd(string original_rcntd_gkey, string rcntd_gkey, string rcntd_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      int int_countR = 0;
      DataTable tb_Del = new DataTable();
      DataRow dr_Del;
      OleDbConnection connR = DAC.NewReaderConnr();
      OleDbCommand cmdR = new OleDbCommand();
      connR.Open();
      cmdR.Connection = connR;
      cmdR.CommandText = "select * from rcntd WITH (NOLOCK)  where gkey=? ";
      cmdR.Parameters.Clear();
      AddParam(cmdR, "gkey", original_rcntd_gkey);
      tb_Del.Load(cmdR.ExecuteReader());
      int_countR = tb_Del.Rows.Count;
      connR.Close();
      cmdR.Dispose();
      //
      int int_count = 0;
      if (int_countR == 1)
      {
        OleDbConnection connD = DAC.NewReaderConnr();
        OleDbCommand cmdD = new OleDbCommand();
        connD.Open();
        cmdD.CommandText = "DELETE FROM rcntd where gkey= ? ";
        AddParam(cmdD, "original_rcntd_gkey", original_rcntd_gkey);
        cmdD.Connection = connD;
        //
        OleDbTransaction thistran = connD.BeginTransaction(IsolationLevel.ReadCommitted);
        cmdD.Transaction = thistran;
        try
        {
          int_count = cmdD.ExecuteNonQuery();
          if (int_count > 0)
          {
            dr_Del = tb_Del.Rows[0];
            Insertbalog(connD, thistran, "rcntd", rcntd_actkey, UserGkey);
            Insertbtlog(connD, thistran, "rcntd", DAC.GetStringValue(dr_Del["BDCCN"]) + " " + DAC.GetStringValue(dr_Del["BDDCX"]), "D", UserGkey, DAC.GetStringValue(dr_Del["BDCCN"]).ToString() + " " + DAC.GetStringValue(dr_Del["BDDCX"]) + " " + DAC.GetStringValue(dr_Del["BDRAT"]) + " " + DAC.GetStringValue(dr_Del["BDRMK"]));
          }
          thistran.Commit();
        }
        catch (Exception e)
        {
          thistran.Rollback();
          st_dberrmsg = e.Message;
        }
        finally
        {
          thistran.Dispose();
          connD.Close();
          cmdD.Dispose();
        }
      }
      else
      {
        st_dberrmsg = "Data missing";
      }
      tb_Del.Dispose();
      return st_dberrmsg;
    }


    /// <summary>
    /// UpdateTable rcntd
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Update)]
    public string UpdateTable_rcntd(string original_rcntd_gkey, string rcntd_gkey, string rcntd_mkey, string rcntd_BDCCN, decimal rcntd_BDDCX, decimal rcntd_BDQTY, decimal rcntd_BDRAT, string rcntd_BDRMK, string rcntd_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      string st_tempgkey = "";
      string st_SelDataKey = "";
      //
      DataTable tb_rcntd_upt = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection connU = NewReaderConnr();
      //
      //DataTable tb_bpud = new DataTable();
      //OleDbConnection connR = NewReaderConnr();
      //
      //OleDbCommand CmdBpud = new OleDbCommand();
      //CmdBpud.CommandText = "select * from bpud WITH (NOLOCK)  where BPNUM= ? ";
      //CmdBpud.Connection = connR;
      //CmdBpud.Parameters.Clear();
      //AddParam(CmdBpud, "BPNUM", ri3b_RBPTN);
      //connR.Open();
      //tb_bpud.Load(CmdBpud.ExecuteReader());
      //connR.Close();
      //
      CmdQueryObj.Parameters.Clear();
      CmdQueryObj.CommandText = " and a.mkey = ? ";
      AddParam(CmdQueryObj, "mkey", rcntd_mkey);
      //
      DbDataAdapter da_ADP_upt = GetDataAdapter(PublicVariable.ApVer, "UNrcntd", "rcntd", "", false, "", CmdQueryObj, "", "", "SEL UPT");
      da_ADP_upt.SelectCommand.Connection = connU;
      da_ADP_upt.UpdateCommand.Connection = connU;
      connU.Open();
      da_ADP_upt.Fill(tb_rcntd_upt);
      st_SelDataKey = "rcntd_mkey='" + rcntd_mkey + "' ";
      DataRow[] mod_rows = tb_rcntd_upt.Select(st_SelDataKey);
      DataRow mod_row;
      if (mod_rows.Length != 1)
      {
        st_dberrmsg = StringTable.GetString("資料已變更,請重新選取!");
      }
      else
      {
        OleDbTransaction thistran = connU.BeginTransaction(IsolationLevel.ReadCommitted);
        da_ADP_upt.UpdateCommand.Transaction = thistran;
        try
        {
          mod_row = mod_rows[0];
          mod_row.BeginEdit();
          //
          st_tempgkey = DAC.get_guidkey();
          //
          mod_row["rcntd_BDDCX"] = rcntd_BDDCX;       // 折扣比率
          mod_row["rcntd_BDQTY"] = rcntd_BDQTY;       // 銷售數量
          mod_row["rcntd_BDRAT"] = rcntd_BDRAT;       // 版稅比率
          mod_row["rcntd_BDRMK"] = rcntd_BDRMK;       // 備　　註
          //
          mod_row["rcntd_mkey"] = DAC.get_guidkey();        //
          mod_row["rcntd_trusr"] = UserGkey;  //
          mod_row.EndEdit();
          da_ADP_upt.Update(tb_rcntd_upt);
          Insertbalog(connU, thistran, "rcntd", rcntd_actkey, UserGkey);
          Insertbtlog(connU, thistran, "rcntd", DAC.GetStringValue(mod_row["rcntd_BDCCN"]) + " " + DAC.GetStringValue(mod_row["rcntd_BDDCX"]), "M", UserGkey, DAC.GetStringValue(mod_row["rcntd_BDCCN"]).ToString() + " " + DAC.GetStringValue(mod_row["rcntd_BDDCX"]) + " " + DAC.GetStringValue(mod_row["rcntd_BDRAT"]) + " " + DAC.GetStringValue(mod_row["rcntd_BDRMK"]));
          thistran.Commit();
        }
        catch (Exception e)
        {
          thistran.Rollback();
          st_dberrmsg = e.Message;
        }
        finally
        {
          thistran.Dispose();
        }
      } //mod_rows.Length=1
      //
      connU.Close();
      //
      tb_rcntd_upt.Dispose();
      da_ADP_upt.Dispose();
      //tb_bpud.Dispose();
      //
      CmdQueryObj.Dispose();
      //CmdBpud.Dispose();
      // 
      return st_dberrmsg;
    }
  }
}