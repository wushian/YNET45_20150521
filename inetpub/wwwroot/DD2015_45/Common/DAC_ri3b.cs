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
  public class DAC_ri3b : DAC
  {
    private clsFN daoFN = new clsFN();


    public DAC_ri3b()
      : base()
    {
    }

    public DAC_ri3b(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE ri3b ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_ri3b(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNri3b", "ri3b", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    public DataTable SelectTableForTextEdit_ri3b(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNri3b", "ri3b", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }

    /// <summary>
    /// InsertTable ri3b
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public string InsertTable_ri3b(Int32 ri3b_RBITM, string ri3b_RBREN, string ri3b_RBPTN, string ri3b_RBUNI, decimal ri3b_RBQTY, decimal ri3b_RBUPC, decimal ri3b_RBAMT, string ri3b_RBRMK, string ri3b_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      DataTable tb_ri3b_ins = new DataTable();
      DataTable tb_bpud = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbCommand CmdQueryBpud = new OleDbCommand();
      OleDbConnection Conni = NewReaderConnr();
      OleDbConnection Connr = NewReaderConnr();
      //
      Conni.Open();
      CmdQueryObj.CommandText = " and 1=0";
      CmdQueryBpud.CommandText = "select * from bpud WITH (NOLOCK)  where BPNUM= ? ";
      //
      DbDataAdapter da_ADP_ins = GetDataAdapter(PublicVariable.ApVer, "UNri3b", "ri3b", "", false, "", CmdQueryObj, "", "", "SEL INS");
      da_ADP_ins.SelectCommand.Connection = Conni;
      da_ADP_ins.InsertCommand.Connection = Conni;
      da_ADP_ins.Fill(tb_ri3b_ins);
      OleDbTransaction thistran = Conni.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP_ins.InsertCommand.Transaction = thistran;
      try
      {
        DataRow ins_row = tb_ri3b_ins.NewRow();
        ins_row["ri3b_gkey"] = ri3b_actkey;    // 
        ins_row["ri3b_mkey"] = ri3b_actkey;    //
        ins_row["ri3b_RBREN"] = ri3b_RBREN;       // 銷貨單號
        ins_row["ri3b_RBPTN"] = ri3b_RBPTN;       // 商品編號
        if (ri3b_RBITM == 0)
        {
          ins_row["ri3b_RBITM"] = daoFN.lookupint32("select max(RBITM) AS RBITM from ri3b WITH (NOLOCK) where rbren='" + ri3b_RBREN + "'", "RBITM") + 1;  // 銷貨項次
        }
        else
        {
          ins_row["ri3b_RBITM"] = ri3b_RBITM;  // 銷貨項次
        }
        ins_row["ri3b_RBCOS"] = "01";       // 型別
        ins_row["ri3b_RBPKN"] = "";         // 箱號
        ins_row["ri3b_RBBAT"] = "";       // 批號
        //
        Connr.Open();
        CmdQueryBpud.Connection = Connr;
        CmdQueryBpud.Parameters.Clear();
        AddParam(CmdQueryBpud, "BPNUM", ri3b_RBPTN);
        tb_bpud.Load(CmdQueryBpud.ExecuteReader());
        Connr.Close();
        //
        if (tb_bpud.Rows.Count == 1)
        {
          //ins_row["ri3b_RBNUU"] = tb_bpud.Rows[0]["BPNUU"];       // 廠商編號
          ins_row["ri3b_RBNCR"] = tb_bpud.Rows[0]["BPNCR"];       // 系列代號
          ins_row["ri3b_RBNUB"] = tb_bpud.Rows[0]["BPNUB"];       // 條碼編號
          ins_row["ri3b_RBLAB"] = tb_bpud.Rows[0]["BPLAB"];       // 品牌名稱
          ins_row["ri3b_RBNAM"] = tb_bpud.Rows[0]["BPTNA"];       // 品　　名
          ins_row["ri3b_RBCLA"] = tb_bpud.Rows[0]["BPCLA"];       // 規　　格
          ins_row["ri3b_RBCLR"] = tb_bpud.Rows[0]["BPCLR"];       // 顏色代號
          ins_row["ri3b_RBCLN"] = tb_bpud.Rows[0]["BPCLN"];       // 顏色名稱
          //ins_row["ri3b_RBSIX"] = tb_bpud.Rows[0]["BPSIX"];       // 尺寸代號
          ins_row["ri3b_RBSIZ"] = tb_bpud.Rows[0]["BPSIZ"];       // 尺寸名稱
          ins_row["ri3b_RBUNI"] = ri3b_RBUNI;       // 單位
          ins_row["ri3b_RBDPC"] = tb_bpud.Rows[0]["BPDE1"];       // 定　　價
        }
        ins_row["ri3b_RBUPC"] = ri3b_RBUPC;       // 單　　價
        ins_row["ri3b_RBUPX"] = ri3b_RBUPC;       // 未稅單價
        ins_row["ri3b_RBQTU"] = 1;       // 包裝入數
        ins_row["ri3b_RBQTY"] = ri3b_RBQTY;       // 數　　量
        ins_row["ri3b_RBQTZ"] = 0;       // 贈　　量
        ins_row["ri3b_RBQTD"] = 0;       // 取消數量
        ins_row["ri3b_RBAMT"] = ri3b_RBAMT;       // 金　　額
        ins_row["ri3b_RBAMX"] = ri3b_RBAMT;       // 未稅金額
        ins_row["ri3b_RBRMK"] = ri3b_RBRMK;       // 備註說明
        if (DAC.GetDecimalValue(ins_row["ri3b_RBDPC"]) == 0)
        {
          ins_row["ri3b_RBDCX"] = 100;       // 折　　數
        }
        else
        {
          ins_row["ri3b_RBDCX"] = daoFN.Round(DAC.GetDecimalValue(ins_row["ri3b_RBUPC"]) * 100 / DAC.GetDecimalValue(ins_row["ri3b_RBDPC"]), 2);
        }
        ins_row["ri3b_RBMSG"] = "";       // 訊息說明
        //
        ins_row["ri3b_trusr"] = UserGkey;  //
        tb_ri3b_ins.Rows.Add(ins_row);
        //
        da_ADP_ins.Update(tb_ri3b_ins);
        Insertbalog(Conni, thistran, "ri3b", ri3b_actkey, UserGkey);
        Insertbtlog(Conni, thistran, "ri3b", DAC.GetStringValue(ins_row["ri3b_RBREN"]) + " " + DAC.GetStringValue(ins_row["ri3b_RBPTN"]), "I", UserGkey, DAC.GetInt32Value(ins_row["ri3b_RBITM"]).ToString() + " " + DAC.GetStringValue(ins_row["ri3b_RBREN"]) + " " + DAC.GetStringValue(ins_row["ri3b_RBPTN"]) + " " + DAC.GetDecimalValue(ins_row["ri3b_RBQTY"]).ToString() + " " + DAC.GetDecimalValue(ins_row["ri3b_RBUPC"]).ToString() + " " + DAC.GetDecimalValue(ins_row["ri3b_RBAMT"]).ToString() + " " + DAC.GetStringValue(ins_row["ri3b_RBRMK"]));
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
        tb_ri3b_ins.Dispose();
        tb_bpud.Dispose();
        CmdQueryObj.Dispose();
        CmdQueryBpud.Dispose();
        da_ADP_ins.Dispose();
        Conni.Close();
        //
        UpdateTol_ri3a(ri3b_RBREN);
        UpdateITM_ri3b(ri3b_RBREN);
        //
      }
      return st_dberrmsg;
    }

    /// <summary>
    /// DELETE Table ri3b 
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public string DeleteTable_ri3b(string original_ri3b_gkey, string ri3b_gkey, string ri3b_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      string st_ren = "";
      int int_countR = 0;
      DataTable tb_Del = new DataTable();
      DataRow dr_Del;
      OleDbConnection connR = DAC.NewReaderConnr();
      OleDbCommand cmdR = new OleDbCommand();
      connR.Open();
      cmdR.Connection = connR;
      cmdR.CommandText = "select * from ri3b WITH (NOLOCK)  where gkey=? ";
      cmdR.Parameters.Clear();
      AddParam(cmdR, "gkey", original_ri3b_gkey);
      tb_Del.Load(cmdR.ExecuteReader());
      int_countR = tb_Del.Rows.Count;
      connR.Close();
      cmdR.Dispose();
      //
      int int_count = 0;
      if (int_countR == 1)
      {
        st_ren = DAC.GetStringValue(tb_Del.Rows[0]["RBREN"]);
        OleDbConnection connD = DAC.NewReaderConnr();
        OleDbCommand cmdD = new OleDbCommand();
        connD.Open();
        cmdD.CommandText = "DELETE FROM ri3b where gkey= ? ";
        AddParam(cmdD, "original_ri3b_gkey", original_ri3b_gkey);
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
            Insertbalog(connD, thistran, "ri3b", ri3b_actkey, UserGkey);
            Insertbtlog(connD, thistran, "ri3b", DAC.GetStringValue(dr_Del["RBREN"]) + " " + DAC.GetStringValue(dr_Del["RBPTN"]), "D", UserGkey, DAC.GetInt32Value(dr_Del["RBITM"]).ToString() + " " + DAC.GetStringValue(dr_Del["RBREN"]) + " " + DAC.GetStringValue(dr_Del["RBPTN"]) + " " + DAC.GetDecimalValue(dr_Del["RBQTY"]).ToString() + " " + DAC.GetDecimalValue(dr_Del["RBUPC"]).ToString() + " " + DAC.GetDecimalValue(dr_Del["RBAMT"]).ToString() + " " + DAC.GetStringValue(dr_Del["RBRMK"]));
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
          UpdateTol_ri3a(st_ren);
          UpdateITM_ri3b(st_ren);
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
    /// UpdateTable ri3b
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Update)]
    public string UpdateTable_ri3b(string original_ri3b_gkey, string ri3b_gkey, string ri3b_mkey, Int32 ri3b_RBITM, string ri3b_RBREN, string ri3b_RBPTN, string ri3b_RBUNI, decimal ri3b_RBQTY, decimal ri3b_RBUPC, decimal ri3b_RBAMT, string ri3b_RBRMK, string ri3b_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      string st_tempgkey = "";
      string st_SelDataKey = "";
      string st_ren = "";
      //
      DataTable tb_ri3b_upt = new DataTable();
      DataTable tb_bpud = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection connU = NewReaderConnr();
      OleDbConnection connR = NewReaderConnr();
      //
      OleDbCommand CmdBpud = new OleDbCommand();
      CmdBpud.CommandText = "select * from bpud WITH (NOLOCK)  where BPNUM= ? ";
      CmdBpud.Connection = connR;
      CmdBpud.Parameters.Clear();
      AddParam(CmdBpud, "BPNUM", ri3b_RBPTN);
      connR.Open();
      tb_bpud.Load(CmdBpud.ExecuteReader());
      connR.Close();
      //
      CmdQueryObj.Parameters.Clear();
      CmdQueryObj.CommandText = " and a.mkey = ? ";
      AddParam(CmdQueryObj, "mkey", ri3b_mkey);
      //
      DbDataAdapter da_ADP_upt = GetDataAdapter(PublicVariable.ApVer, "UNri3b", "ri3b", "", false, "", CmdQueryObj, "", "", "SEL UPT");
      da_ADP_upt.SelectCommand.Connection = connU;
      da_ADP_upt.UpdateCommand.Connection = connU;
      connU.Open();
      da_ADP_upt.Fill(tb_ri3b_upt);
      st_SelDataKey = "ri3b_mkey='" + ri3b_mkey + "' ";
      DataRow[] mod_rows = tb_ri3b_upt.Select(st_SelDataKey);
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
          st_ren = DAC.GetStringValue(mod_row["ri3b_RBREN"]);
          mod_row.BeginEdit();
          //
          st_tempgkey = DAC.get_guidkey();
          //
          mod_row["ri3b_RBPTN"] = ri3b_RBPTN;       // 商品編號
          if (ri3b_RBITM == 0)
          {
            mod_row["ri3b_RBITM"] = daoFN.lookupint32("select max(RBITM) AS RBITM from ri3b WITH (NOLOCK) where rbren='" + ri3b_RBREN + "'", "RBITM") + 1;  // 銷貨項次
          }
          else
          {
            mod_row["ri3b_RBITM"] = ri3b_RBITM;  // 銷貨項次
          }
          //mod_row["ri3b_RBNUU"] = tb_bpud.Rows[0]["BPNUU"];       // 廠商編號
          mod_row["ri3b_RBNCR"] = tb_bpud.Rows[0]["BPNCR"];       // 系列代號
          mod_row["ri3b_RBNUB"] = tb_bpud.Rows[0]["BPNUB"];       // 條碼編號
          mod_row["ri3b_RBLAB"] = tb_bpud.Rows[0]["BPLAB"];       // 品牌名稱
          mod_row["ri3b_RBNAM"] = tb_bpud.Rows[0]["BPTNA"];       // 品　　名
          mod_row["ri3b_RBCLA"] = tb_bpud.Rows[0]["BPCLA"];       // 規　　格
          mod_row["ri3b_RBCLR"] = tb_bpud.Rows[0]["BPCLR"];       // 顏色代號
          mod_row["ri3b_RBCLN"] = tb_bpud.Rows[0]["BPCLN"];       // 顏色名稱
          //mod_row["ri3b_RBSIX"] = tb_bpud.Rows[0]["BPSIX"];       // 尺寸代號
          mod_row["ri3b_RBSIZ"] = tb_bpud.Rows[0]["BPSIZ"];       // 尺寸名稱
          mod_row["ri3b_RBUNI"] = ri3b_RBUNI;       // 單位
          mod_row["ri3b_RBDPC"] = tb_bpud.Rows[0]["BPDE1"];       // 定　　價
          mod_row["ri3b_RBUPC"] = ri3b_RBUPC;       // 單　　價
          mod_row["ri3b_RBUPX"] = ri3b_RBUPC;       // 未稅單價
          mod_row["ri3b_RBQTU"] = 1;       // 包裝入數
          mod_row["ri3b_RBQTY"] = ri3b_RBQTY;       // 數　　量
          mod_row["ri3b_RBQTZ"] = 0;       // 贈　　量
          mod_row["ri3b_RBQTD"] = 0;       // 取消數量
          mod_row["ri3b_RBAMT"] = ri3b_RBAMT;       // 金　　額
          mod_row["ri3b_RBAMX"] = ri3b_RBAMT;       // 未稅金額
          mod_row["ri3b_RBRMK"] = ri3b_RBRMK;       // 備註說明
          if (DAC.GetDecimalValue(mod_row["ri3b_RBDPC"]) == 0)
          {
            mod_row["ri3b_RBDCX"] = 100;       // 折　　數
          }
          else
          {
            mod_row["ri3b_RBDCX"] = daoFN.Round(DAC.GetDecimalValue(mod_row["ri3b_RBUPC"]) * 100 / DAC.GetDecimalValue(mod_row["ri3b_RBDPC"]), 2);
          }
          mod_row["ri3b_RBMSG"] = "";       // 訊息說明
          //
          mod_row["ri3b_mkey"] = DAC.get_guidkey();        //
          mod_row["ri3b_trusr"] = UserGkey;  //
          mod_row.EndEdit();
          da_ADP_upt.Update(tb_ri3b_upt);
          Insertbalog(connU, thistran, "ri3b", ri3b_actkey, UserGkey);
          Insertbtlog(connU, thistran, "ri3b", DAC.GetStringValue(mod_row["ri3b_RBREN"]) + " " + DAC.GetStringValue(mod_row["ri3b_RBPTN"]), "M", UserGkey, DAC.GetInt32Value(mod_row["ri3b_RBITM"]).ToString() + " " + DAC.GetStringValue(mod_row["ri3b_RBREN"]) + " " + DAC.GetStringValue(mod_row["ri3b_RBPTN"]) + " " + DAC.GetDecimalValue(mod_row["ri3b_RBQTY"]).ToString() + " " + DAC.GetDecimalValue(mod_row["ri3b_RBUPC"]).ToString() + " " + DAC.GetDecimalValue(mod_row["ri3b_RBAMT"]).ToString() + " " + DAC.GetStringValue(mod_row["ri3b_RBRMK"]));
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
          da_ADP_upt.Dispose();
          //
          UpdateTol_ri3a(st_ren);
          UpdateITM_ri3b(st_ren);
        }
      } //mod_rows.Length=1
      //
      connU.Close();
      //
      tb_ri3b_upt.Dispose();
      tb_bpud.Dispose();
      //
      CmdQueryObj.Dispose();
      CmdBpud.Dispose();
      // 
      return st_dberrmsg;
    }

    public string UpdateTol_ri3a(string st_ren)
    {
      string st_ret = "";
      string ri3a_RITXP = "";
      decimal ri3a_RIRMN = 0, ri3a_RIDMN = 0, ri3a_RITXN = 0, ri3a_RITOL = 0;
      decimal de_taxrat = DAC.GetDecimalValue(0.05);
      //
      OleDbDataReader Redaer;
      OleDbCommand CmdReader = new OleDbCommand();
      OleDbConnection connR = NewReaderConnr();
      CmdReader.CommandText = "SELECT RITXP,RIRMN,RIDMN,RITXN,RITOL FROM RI3A WITH (NOLOCK) where RIREN= ? ";
      CmdReader.Connection = connR;
      connR.Open();
      CmdReader.Parameters.Clear();
      AddParam(CmdReader, "RIREN", st_ren);
      Redaer = CmdReader.ExecuteReader();
      if (Redaer.Read())
      {
        //RITXP 00稅內含,01稅外加,02免稅
        ri3a_RITXP = DAC.GetStringValue(Redaer["RITXP"]);
        //運費
        ri3a_RIRMN = DAC.GetDecimalValue(Redaer["RIRMN"]);
        //商品金額
        ri3a_RIDMN = DAC.GetDecimalValue(Redaer["RIDMN"]);
      }
      Redaer.Close();
      //
      CmdReader.CommandText = "SELECT SUM(RBAMT) AS RBAMT FROM RI3B WITH (NOLOCK) where RBREN= ? ";
      CmdReader.Connection = connR;
      CmdReader.Parameters.Clear();
      AddParam(CmdReader, "RBREN", st_ren);
      Redaer = CmdReader.ExecuteReader();
      if (Redaer.Read())
      {
        ri3a_RIDMN = DAC.GetDecimalValue(Redaer["RBAMT"]);
      }
      Redaer.Close();

      //稅額
      if (ri3a_RITXP == "00")
      {
        //稅內含
        ri3a_RITXN = Round((ri3a_RIRMN + ri3a_RIDMN) * de_taxrat / (1 + de_taxrat), 0);
        ri3a_RITOL = ri3a_RIRMN + ri3a_RIDMN;
      }
      else
      {
        ri3a_RITXN = Round((ri3a_RIRMN + ri3a_RIDMN) * de_taxrat, 0);
        ri3a_RITOL = ri3a_RIRMN + ri3a_RIDMN + ri3a_RITXN;
      }
      //
      OleDbCommand CmdUpdateTol = new OleDbCommand();
      OleDbConnection connU = NewReaderConnr();
      CmdUpdateTol.CommandText = "update RI3A SET RIRMN=?,RIDMN=?,RITXN=?,RITOL=? where RIREN= ? ";
      CmdUpdateTol.Connection = connU;
      connU.Open();
      CmdUpdateTol.Parameters.Clear();
      AddParam(CmdUpdateTol, "RIRMN", ri3a_RIRMN);
      AddParam(CmdUpdateTol, "RIDMN", ri3a_RIDMN);
      AddParam(CmdUpdateTol, "RITXN", ri3a_RITXN);
      AddParam(CmdUpdateTol, "RITOL", ri3a_RITOL);
      AddParam(CmdUpdateTol, "RIREN", st_ren);
      try
      {
        CmdUpdateTol.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        st_ret = ex.Message;
      }
      finally
      {
        connU.Close();
        CmdUpdateTol.Dispose();
        //
        connR.Close();
        CmdReader.Dispose();
      }
      return st_ren;
    }

    public string UpdateITM_ri3b(string ri3b_RBREN)
    {
      string st_dberrmsg = "";
      //
      DataTable tb_ri3b_upt = new DataTable();
      OleDbCommand CmdQuery = new OleDbCommand();
      DataRow mod_row;
      OleDbConnection connU = NewReaderConnr();
      connU.Open();
      //
      CmdQuery.Parameters.Clear();
      CmdQuery.CommandText = " and a.RBREN = ? ";
      AddParam(CmdQuery, "RBREN", ri3b_RBREN);
      DbDataAdapter da_ADP_upt = GetDataAdapter(PublicVariable.ApVer, "UNri3b", "ri3b", "", false, "", CmdQuery, "", "a.RBREN,a.RBITM", "SEL UPT");
      da_ADP_upt.SelectCommand.Connection = connU;
      da_ADP_upt.UpdateCommand.Connection = connU;
      da_ADP_upt.Fill(tb_ri3b_upt);
      OleDbTransaction thistran = connU.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP_upt.UpdateCommand.Transaction = thistran;
      try
      {
        for (Int32 in_Item = 0; in_Item < tb_ri3b_upt.Rows.Count; in_Item++)
        {
          mod_row = tb_ri3b_upt.Rows[in_Item];
          mod_row.BeginEdit();
          mod_row["ri3b_RBITM"] = in_Item + 1;
          mod_row.EndEdit();
        }
        da_ADP_upt.Update(tb_ri3b_upt);
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
        connU.Close();
        tb_ri3b_upt.Dispose();
        da_ADP_upt.Dispose();
        CmdQuery.Dispose();
      }
      return st_dberrmsg;
    }

  }
}