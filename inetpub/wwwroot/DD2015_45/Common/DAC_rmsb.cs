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
  public class DAC_rmsb : DAC
  {
    private clsFN daoFN = new clsFN();

    public DAC_rmsb()
      : base()
    {
    }

    public DAC_rmsb(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE rmsb ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_rmsb(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrmsb", "rmsb", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    public DataTable SelectTableForTextEdit_rmsb(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrmsb", "rmsb", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }

    /// <summary>
    /// InsertTable rmsb
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public string InsertTable_rmsb(string rmsb_RDREN, Int32 rmsb_RDITM, string rmsb_RDDAT, string rmsb_RDENO, string rmsb_RDNUM, string rmsb_RDCUS, string rmsb_RDTXT, string rmsb_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      DataTable tb_rmsb_ins = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection Conni = NewReaderConnr();
      //
      Conni.Open();
      CmdQueryObj.CommandText = " and 1=0";
      //
      DbDataAdapter da_ADP_ins = GetDataAdapter(PublicVariable.ApVer, "UNrmsb", "rmsb", "", false, "", CmdQueryObj, "", "", "SEL INS");
      da_ADP_ins.SelectCommand.Connection = Conni;
      da_ADP_ins.InsertCommand.Connection = Conni;
      da_ADP_ins.Fill(tb_rmsb_ins);
      OleDbTransaction thistran = Conni.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP_ins.InsertCommand.Transaction = thistran;
      try
      {
        DataRow ins_row = tb_rmsb_ins.NewRow();
        ins_row["rmsb_gkey"] = rmsb_actkey;    // 
        ins_row["rmsb_mkey"] = rmsb_actkey;    //
        //
        ins_row["rmsb_RDREN"] = rmsb_RDREN;       // 訊息編號
        if (rmsb_RDITM == 0)
        {
          ins_row["rmsb_RDITM"] = daoFN.lookupint32("select max(RDITM) AS RDITM from rmsb WITH (NOLOCK) where RDREN='" + rmsb_RDREN + "'", "RDITM") + 1;  // 項次
        }
        else
        {
          ins_row["rmsb_RDITM"] = rmsb_RDITM;  // 項次
        }
        if (rmsb_RDDAT == "") { ins_row["rmsb_RDDAT"] = DBNull.Value; } else { ins_row["rmsb_RDDAT"] = daoFN.DateStringToDateTime(rmsb_RDDAT); }       //訊息日期
        ins_row["rmsb_RDENO"] = rmsb_RDENO;       // 員工編號
        ins_row["rmsb_RDNUM"] = rmsb_RDNUM;       // 經銷編號
        ins_row["rmsb_RDCUS"] = rmsb_RDCUS;       // 會員編號
        ins_row["rmsb_RDTXT"] = rmsb_RDTXT;       // 留言內容
        //
        ins_row["rmsb_trusr"] = UserGkey;  //
        tb_rmsb_ins.Rows.Add(ins_row);
        //
        da_ADP_ins.Update(tb_rmsb_ins);
        Insertbalog(Conni, thistran, "rmsb", rmsb_actkey, UserGkey);
        Insertbtlog(Conni, thistran, "rmsb", DAC.GetStringValue(ins_row["rmsb_RDREN"]) + " " + DAC.GetStringValue(ins_row["rmsb_RDITM"]), "I", UserGkey, DAC.GetStringValue(ins_row["rmsb_RDENO"]).ToString() + " " + DAC.GetStringValue(ins_row["rmsb_RDCUS"]) + " " + DAC.GetStringValue(ins_row["rmsb_RDNUM"]));
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
        tb_rmsb_ins.Dispose();
        CmdQueryObj.Dispose();
        da_ADP_ins.Dispose();
        Conni.Close();
        //
      }
      return st_dberrmsg;
    }

    /// <summary>
    /// DELETE Table rmsb
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public string DeleteTable_rmsb(string original_rmsb_gkey, string rmsb_gkey, string rmsb_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      int int_countR = 0;
      DataTable tb_Del = new DataTable();
      DataRow dr_Del;
      OleDbConnection connR = DAC.NewReaderConnr();
      OleDbCommand cmdR = new OleDbCommand();
      connR.Open();
      cmdR.Connection = connR;
      cmdR.CommandText = "select * from rmsb WITH (NOLOCK)  where gkey=? ";
      cmdR.Parameters.Clear();
      AddParam(cmdR, "gkey", original_rmsb_gkey);
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
        cmdD.CommandText = "DELETE FROM rmsb where gkey= ? ";
        AddParam(cmdD, "original_rmsb_gkey", original_rmsb_gkey);
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
            Insertbalog(connD, thistran, "rmsb", rmsb_actkey, UserGkey);
            Insertbtlog(connD, thistran, "rmsb", DAC.GetStringValue(dr_Del["RDREN"]) + " " + DAC.GetStringValue(dr_Del["RDITM"]), "D", UserGkey, DAC.GetStringValue(dr_Del["RDENO"]).ToString() + " " + DAC.GetStringValue(dr_Del["RDCUS"]) + " " + DAC.GetStringValue(dr_Del["RDNUM"]));
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
    /// UpdateTable rmsb
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Update)]
    public string UpdateTable_rmsb(string original_rmsb_gkey, string rmsb_gkey, string rmsb_mkey, string rmsb_RDREN, Int32 rmsb_RDITM, string rmsb_RDDAT, string rmsb_RDENO, string rmsb_RDNUM, string rmsb_RDCUS, string rmsb_RDTXT, string rmsb_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      string st_tempgkey = "";
      string st_SelDataKey = "";
      //
      DataTable tb_rmsb_upt = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection connU = NewReaderConnr();
      //
      CmdQueryObj.Parameters.Clear();
      CmdQueryObj.CommandText = " and a.mkey = ? ";
      AddParam(CmdQueryObj, "mkey", rmsb_mkey);
      //
      DbDataAdapter da_ADP_upt = GetDataAdapter(PublicVariable.ApVer, "UNrmsb", "rmsb", "", false, "", CmdQueryObj, "", "", "SEL UPT");
      da_ADP_upt.SelectCommand.Connection = connU;
      da_ADP_upt.UpdateCommand.Connection = connU;
      connU.Open();
      da_ADP_upt.Fill(tb_rmsb_upt);
      st_SelDataKey = "rmsb_mkey='" + rmsb_mkey + "' ";
      DataRow[] mod_rows = tb_rmsb_upt.Select(st_SelDataKey);
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
          if (rmsb_RDDAT == "") { mod_row["rmsb_RDDAT"] = DBNull.Value; } else { mod_row["rmsb_RDDAT"] = daoFN.DateStringToDateTime(rmsb_RDDAT); }       //訊息日期
          mod_row["rmsb_RDENO"] = rmsb_RDENO;       // 員工編號
          mod_row["rmsb_RDNUM"] = rmsb_RDNUM;       // 經銷編號
          mod_row["rmsb_RDCUS"] = rmsb_RDCUS;       // 會員編號
          mod_row["rmsb_RDTXT"] = rmsb_RDTXT;       // 留言內容
          //
          mod_row["rmsb_mkey"] = DAC.get_guidkey();        //
          mod_row["rmsb_trusr"] = UserGkey;  //
          mod_row.EndEdit();
          da_ADP_upt.Update(tb_rmsb_upt);
          Insertbalog(connU, thistran, "rmsb", rmsb_actkey, UserGkey);
          Insertbtlog(connU, thistran, "rmsb", DAC.GetStringValue(mod_row["rmsb_RDREN"]) + " " + DAC.GetStringValue(mod_row["rmsb_RDITM"]), "M", UserGkey, DAC.GetStringValue(mod_row["rmsb_RDENO"]).ToString() + " " + DAC.GetStringValue(mod_row["rmsb_RDCUS"]) + " " + DAC.GetStringValue(mod_row["rmsb_RDNUM"]));
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
      tb_rmsb_upt.Dispose();
      da_ADP_upt.Dispose();
      //
      CmdQueryObj.Dispose();
      // 
      return st_dberrmsg;
    }

  }
}