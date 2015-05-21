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

  public class DAC_rcntr : DAC
  {
    private clsFN daoFN = new clsFN();

    public DAC_rcntr()
      : base()
    {
    }

    public DAC_rcntr(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE rcntr ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_rcntr(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrcntr", "rcntr", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    public DataTable SelectTableForTextEdit_rcntr(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrcntr", "rcntr", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }

    /// <summary>
    /// InsertTable rcntr
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public string InsertTable_rcntr(string rcntr_BDCCN, string rcntr_BDAUR, string rcntr_BDAMT, string rcntr_BDEDT, string rcntr_BDINV, string rcntr_BDINT, string rcntr_BDRMK, string rcntr_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      DataTable tb_rcntr_ins = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection Conni = NewReaderConnr();
      //
      Conni.Open();
      CmdQueryObj.CommandText = " and 1=0";
      //
      DbDataAdapter da_ADP_ins = GetDataAdapter(PublicVariable.ApVer, "UNrcntr", "rcntr", "", false, "", CmdQueryObj, "", "", "SEL INS");
      da_ADP_ins.SelectCommand.Connection = Conni;
      da_ADP_ins.InsertCommand.Connection = Conni;
      da_ADP_ins.Fill(tb_rcntr_ins);
      OleDbTransaction thistran = Conni.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP_ins.InsertCommand.Transaction = thistran;
      try
      {
        DataRow ins_row = tb_rcntr_ins.NewRow();
        ins_row["rcntr_gkey"] = rcntr_actkey;    // 
        ins_row["rcntr_mkey"] = rcntr_actkey;    //
        //
        ins_row["rcntr_BDCCN"] = rcntr_BDCCN;       // 合約編號
        ins_row["rcntr_BDAUR"] = rcntr_BDAUR;       // 作者
        //
        ins_row["rcntr_BDAMT"] = DAC.GetDecimalValue(rcntr_BDAMT);       // 預付金額
        if ((rcntr_BDEDT == "") || (rcntr_BDEDT == null))
        {
          ins_row["rcntr_BDEDT"] = DBNull.Value;       // 預付日期
        }
        else
        {
          ins_row["rcntr_BDEDT"] = rcntr_BDEDT;       // 預付日期
        }
        //
        ins_row["rcntr_BDINV"] = rcntr_BDINV;       // 統一編號
        ins_row["rcntr_BDINT"] = rcntr_BDINT;       // 發票抬頭
        ins_row["rcntr_BDRMK"] = rcntr_BDRMK;       // 備　　註
        //
        ins_row["rcntr_trusr"] = UserGkey;  //
        tb_rcntr_ins.Rows.Add(ins_row);
        //
        da_ADP_ins.Update(tb_rcntr_ins);
        Insertbalog(Conni, thistran, "rcntr", rcntr_actkey, UserGkey);
        Insertbtlog(Conni, thistran, "rcntr", DAC.GetStringValue(ins_row["rcntr_BDCCN"]) + " " + DAC.GetStringValue(ins_row["rcntr_BDAUR"]), "I", UserGkey, DAC.GetStringValue(ins_row["rcntr_BDAUR"]).ToString() + " " + DAC.GetStringValue(ins_row["rcntr_BDINV"]) + " " + DAC.GetStringValue(ins_row["rcntr_BDINT"]));
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
        tb_rcntr_ins.Dispose();
        CmdQueryObj.Dispose();
        da_ADP_ins.Dispose();
        Conni.Close();
        //
      }
      return st_dberrmsg;
    }

    /// <summary>
    /// DELETE Table rcntr
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public string DeleteTable_rcntr(string original_rcntr_gkey, string rcntr_gkey, string rcntr_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      int int_countR = 0;
      DataTable tb_Del = new DataTable();
      DataRow dr_Del;
      OleDbConnection connR = DAC.NewReaderConnr();
      OleDbCommand cmdR = new OleDbCommand();
      connR.Open();
      cmdR.Connection = connR;
      cmdR.CommandText = "select * from rcntr WITH (NOLOCK)  where gkey=? ";
      cmdR.Parameters.Clear();
      AddParam(cmdR, "gkey", original_rcntr_gkey);
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
        cmdD.CommandText = "DELETE FROM rcntr where gkey= ? ";
        AddParam(cmdD, "original_rcntr_gkey", original_rcntr_gkey);
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
            Insertbalog(connD, thistran, "rcntr", rcntr_actkey, UserGkey);
            Insertbtlog(connD, thistran, "rcntr", DAC.GetStringValue(dr_Del["BDCCN"]) + " " + DAC.GetStringValue(dr_Del["BDAUR"]), "D", UserGkey, DAC.GetStringValue(dr_Del["BDAUR"]).ToString() + " " + DAC.GetStringValue(dr_Del["BDINV"]) + " " + DAC.GetStringValue(dr_Del["BDINT"]));
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
    /// UpdateTable rcntr
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Update)]
    public string UpdateTable_rcntr(string original_rcntr_gkey, string rcntr_gkey, string rcntr_mkey, string rcntr_BDCCN, string rcntr_BDAUR, string rcntr_BDAMT, string rcntr_BDEDT, string rcntr_BDINV, string rcntr_BDINT, string rcntr_BDRMK, string rcntr_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      string st_tempgkey = "";
      string st_SelDataKey = "";
      //
      DataTable tb_rcntr_upt = new DataTable();
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
      AddParam(CmdQueryObj, "mkey", rcntr_mkey);
      //
      DbDataAdapter da_ADP_upt = GetDataAdapter(PublicVariable.ApVer, "UNrcntr", "rcntr", "", false, "", CmdQueryObj, "", "", "SEL UPT");
      da_ADP_upt.SelectCommand.Connection = connU;
      da_ADP_upt.UpdateCommand.Connection = connU;
      connU.Open();
      da_ADP_upt.Fill(tb_rcntr_upt);
      st_SelDataKey = "rcntr_mkey='" + rcntr_mkey + "' ";
      DataRow[] mod_rows = tb_rcntr_upt.Select(st_SelDataKey);
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
          mod_row["rcntr_BDAUR"] = rcntr_BDAUR;       // 作者
          //
          mod_row["rcntr_BDAMT"] = DAC.GetDecimalValue(rcntr_BDAMT);       // 預付金額
          if ((rcntr_BDEDT == "") || (rcntr_BDEDT == null))
          {
            mod_row["rcntr_BDEDT"] = DBNull.Value;       // 預付日期
          }
          else
          {
            mod_row["rcntr_BDEDT"] = rcntr_BDEDT;       // 預付日期
          }
          //
          mod_row["rcntr_BDINV"] = rcntr_BDINV;       // 統一編號
          mod_row["rcntr_BDINT"] = rcntr_BDINT;       // 發票抬頭
          mod_row["rcntr_BDRMK"] = rcntr_BDRMK;       // 備　　註
          //
          mod_row["rcntr_mkey"] = DAC.get_guidkey();        //
          mod_row["rcntr_trusr"] = UserGkey;  //
          mod_row.EndEdit();
          da_ADP_upt.Update(tb_rcntr_upt);
          Insertbalog(connU, thistran, "rcntr", rcntr_actkey, UserGkey);
          Insertbtlog(connU, thistran, "rcntr", DAC.GetStringValue(mod_row["rcntr_BDCCN"]) + " " + DAC.GetStringValue(mod_row["rcntr_BDAUR"]), "M", UserGkey, DAC.GetStringValue(mod_row["rcntr_BDAUR"]).ToString() + " " + DAC.GetStringValue(mod_row["rcntr_BDINV"]) + " " + DAC.GetStringValue(mod_row["rcntr_BDINT"]));
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
      tb_rcntr_upt.Dispose();
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