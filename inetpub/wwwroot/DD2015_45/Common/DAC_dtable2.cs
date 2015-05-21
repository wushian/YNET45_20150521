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
  public class DAC_dtable2 : DAC
  {
    private clsFN daoFN = new clsFN();



    public DAC_dtable2()
      : base()
    {
    }

    public DAC_dtable2(OleDbConnection conn)
      : base(conn)
    {
    }


    /// <summary>
    /// SELECT TABLE dtable2 ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_dtable2(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNdtable2", "dtable2", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    public DataTable SelectTableForTextEdit_dtable2(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNdtable2", "dtable2", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }

    /// <summary>
    /// InsertTable dtable2
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public string InsertTable_dtable2(string dtable2_BDCCN, decimal dtable2_BDDCX, decimal dtable2_BDQTY, decimal dtable2_BDRAT, string dtable2_BDRMK, string dtable2_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      DataTable tb_dtable2_ins = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection Conni = NewReaderConnr();
      //
      Conni.Open();
      CmdQueryObj.CommandText = " and 1=0";
      //
      DbDataAdapter da_ADP_ins = GetDataAdapter(PublicVariable.ApVer, "UNdtable2", "dtable2", "", false, "", CmdQueryObj, "", "", "SEL INS");
      da_ADP_ins.SelectCommand.Connection = Conni;
      da_ADP_ins.InsertCommand.Connection = Conni;
      da_ADP_ins.Fill(tb_dtable2_ins);
      OleDbTransaction thistran = Conni.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP_ins.InsertCommand.Transaction = thistran;
      try
      {
        DataRow ins_row = tb_dtable2_ins.NewRow();
        ins_row["dtable2_gkey"] = dtable2_actkey;    // 
        ins_row["dtable2_mkey"] = dtable2_actkey;    //
        //
        ins_row["dtable2_BDCCN"] = dtable2_BDCCN;       // 合約編號
        ins_row["dtable2_BDDCX"] = dtable2_BDDCX;       // 折扣比率
        ins_row["dtable2_BDQTY"] = dtable2_BDQTY;      // 銷售數量
        ins_row["dtable2_BDRAT"] = dtable2_BDRAT;       // 版稅比率
        ins_row["dtable2_BDRMK"] = dtable2_BDRMK;       // 備　　註
        //
        ins_row["dtable2_trusr"] = UserGkey;  //
        tb_dtable2_ins.Rows.Add(ins_row);
        //
        da_ADP_ins.Update(tb_dtable2_ins);
        Insertbalog(Conni, thistran, "dtable2", dtable2_actkey, UserGkey);
        Insertbtlog(Conni, thistran, "dtable2", DAC.GetStringValue(ins_row["dtable2_BDCCN"]) + " " + DAC.GetStringValue(ins_row["dtable2_BDDCX"]), "I", UserGkey, DAC.GetStringValue(ins_row["dtable2_BDRAT"]).ToString() + " " + DAC.GetStringValue(ins_row["dtable2_BDRMK"]));
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
        tb_dtable2_ins.Dispose();
        CmdQueryObj.Dispose();
        da_ADP_ins.Dispose();
        Conni.Close();
        //
      }
      return st_dberrmsg;
    }

    /// <summary>
    /// DELETE Table dtable2
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public string DeleteTable_dtable2(string original_dtable2_gkey, string dtable2_gkey, string dtable2_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      int int_countR = 0;
      DataTable tb_Del = new DataTable();
      DataRow dr_Del;
      OleDbConnection connR = DAC.NewReaderConnr();
      OleDbCommand cmdR = new OleDbCommand();
      connR.Open();
      cmdR.Connection = connR;
      cmdR.CommandText = "select * from dtable2 WITH (NOLOCK)  where gkey=? ";
      cmdR.Parameters.Clear();
      AddParam(cmdR, "gkey", original_dtable2_gkey);
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
        cmdD.CommandText = "DELETE FROM dtable2 where gkey= ? ";
        AddParam(cmdD, "original_dtable2_gkey", original_dtable2_gkey);
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
            Insertbalog(connD, thistran, "dtable2", dtable2_actkey, UserGkey);
            Insertbtlog(connD, thistran, "dtable2", DAC.GetStringValue(dr_Del["BDCCN"]) + " " + DAC.GetStringValue(dr_Del["BDDCX"]), "D", UserGkey, DAC.GetStringValue(dr_Del["BDCCN"]).ToString() + " " + DAC.GetStringValue(dr_Del["BDDCX"]) + " " + DAC.GetStringValue(dr_Del["BDRAT"]) + " " + DAC.GetStringValue(dr_Del["BDRMK"]));
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
    /// UpdateTable dtable2
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Update)]
    public string UpdateTable_dtable2(string original_dtable2_gkey, string dtable2_gkey, string dtable2_mkey, string dtable2_BDCCN, decimal dtable2_BDDCX, decimal dtable2_BDQTY, decimal dtable2_BDRAT, string dtable2_BDRMK, string dtable2_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      string st_tempgkey = "";
      string st_SelDataKey = "";
      //
      DataTable tb_dtable2_upt = new DataTable();
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
      AddParam(CmdQueryObj, "mkey", dtable2_mkey);
      //
      DbDataAdapter da_ADP_upt = GetDataAdapter(PublicVariable.ApVer, "UNdtable2", "dtable2", "", false, "", CmdQueryObj, "", "", "SEL UPT");
      da_ADP_upt.SelectCommand.Connection = connU;
      da_ADP_upt.UpdateCommand.Connection = connU;
      connU.Open();
      da_ADP_upt.Fill(tb_dtable2_upt);
      st_SelDataKey = "dtable2_mkey='" + dtable2_mkey + "' ";
      DataRow[] mod_rows = tb_dtable2_upt.Select(st_SelDataKey);
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
          mod_row["dtable2_BDDCX"] = dtable2_BDDCX;       // 折扣比率
          mod_row["dtable2_BDQTY"] = dtable2_BDQTY;       // 銷售數量
          mod_row["dtable2_BDRAT"] = dtable2_BDRAT;       // 版稅比率
          mod_row["dtable2_BDRMK"] = dtable2_BDRMK;       // 備　　註
          //
          mod_row["dtable2_mkey"] = DAC.get_guidkey();        //
          mod_row["dtable2_trusr"] = UserGkey;  //
          mod_row.EndEdit();
          da_ADP_upt.Update(tb_dtable2_upt);
          Insertbalog(connU, thistran, "dtable2", dtable2_actkey, UserGkey);
          Insertbtlog(connU, thistran, "dtable2", DAC.GetStringValue(mod_row["dtable2_BDCCN"]) + " " + DAC.GetStringValue(mod_row["dtable2_BDDCX"]), "M", UserGkey, DAC.GetStringValue(mod_row["dtable2_BDCCN"]).ToString() + " " + DAC.GetStringValue(mod_row["dtable2_BDDCX"]) + " " + DAC.GetStringValue(mod_row["dtable2_BDRAT"]) + " " + DAC.GetStringValue(mod_row["dtable2_BDRMK"]));
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
      tb_dtable2_upt.Dispose();
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