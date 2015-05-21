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
  public class DAC_rmsc : DAC
  {
    private clsFN daoFN = new clsFN();

    public DAC_rmsc()
      : base()
    {
    }

    public DAC_rmsc(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE rmsc ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_rmsc(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrmsc", "rmsc", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    public DataTable SelectTableForTextEdit_rmsc(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrmsc", "rmsc", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }

    /// <summary>
    /// InsertTable rmsc
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public string InsertTable_rmsc(string rmsc_RDREN, Int32 rmsc_RDITM, string rmsc_rmsb_gkey, string rmsc_RDENO, string rmsc_RDNUM, string rmsc_RDCUS, string rmsc_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      DataTable tb_rmsc_ins = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection Conni = NewReaderConnr();
      //
      Conni.Open();
      CmdQueryObj.CommandText = " and 1=0";
      //
      DbDataAdapter da_ADP_ins = GetDataAdapter(PublicVariable.ApVer, "UNrmsc", "rmsc", "", false, "", CmdQueryObj, "", "", "SEL INS");
      da_ADP_ins.SelectCommand.Connection = Conni;
      da_ADP_ins.InsertCommand.Connection = Conni;
      da_ADP_ins.Fill(tb_rmsc_ins);
      OleDbTransaction thistran = Conni.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP_ins.InsertCommand.Transaction = thistran;
      try
      {
        DataRow ins_row = tb_rmsc_ins.NewRow();
        ins_row["rmsc_gkey"] = rmsc_actkey;    // 
        ins_row["rmsc_mkey"] = rmsc_actkey;    //
        //
        ins_row["rmsc_RDREN"] = rmsc_RDREN;       // 訊息編號
        if (rmsc_RDITM == 0)
        {
          ins_row["rmsc_RDITM"] = daoFN.lookupint32("select max(RDITM) AS RDITM from rmsc WITH (NOLOCK) where RDREN='" + rmsc_RDREN + "'", "RDITM") + 1;  // 項次
        }
        else
        {
          ins_row["rmsc_RDITM"] = rmsc_RDITM;  // 項次
        }
        ins_row["rmsc_rmsb_gkey"] = rmsc_rmsb_gkey;
        ins_row["rmsc_RDDAT"] = DateTime.Today.Date;
        ins_row["rmsc_RDENO"] = rmsc_RDENO;       // 員工編號
        ins_row["rmsc_RDNUM"] = rmsc_RDNUM;       // 經銷編號
        ins_row["rmsc_RDCUS"] = rmsc_RDCUS;       // 會員編號
        //
        ins_row["rmsc_trusr"] = UserGkey;  //
        tb_rmsc_ins.Rows.Add(ins_row);
        //
        da_ADP_ins.Update(tb_rmsc_ins);
        Insertbalog(Conni, thistran, "rmsc", rmsc_actkey, UserGkey);
        Insertbtlog(Conni, thistran, "rmsc", DAC.GetStringValue(ins_row["rmsc_RDREN"]) + " " + DAC.GetStringValue(ins_row["rmsc_RDITM"]), "I", UserGkey, DAC.GetStringValue(ins_row["rmsc_RDENO"]).ToString() + " " + DAC.GetStringValue(ins_row["rmsc_RDCUS"]) + " " + DAC.GetStringValue(ins_row["rmsc_RDNUM"]));
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
        tb_rmsc_ins.Dispose();
        CmdQueryObj.Dispose();
        da_ADP_ins.Dispose();
        Conni.Close();
        //
      }
      return st_dberrmsg;
    }

    /// <summary>
    /// DELETE Table rmsc
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public string DeleteTable_rmsc(string original_rmsc_gkey, string rmsc_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      int int_countR = 0;
      DataTable tb_Del = new DataTable();
      DataRow dr_Del;
      OleDbConnection connR = DAC.NewReaderConnr();
      OleDbCommand cmdR = new OleDbCommand();
      connR.Open();
      cmdR.Connection = connR;
      cmdR.CommandText = "select * from rmsc WITH (NOLOCK)  where gkey=? ";
      cmdR.Parameters.Clear();
      AddParam(cmdR, "gkey", original_rmsc_gkey);
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
        cmdD.CommandText = "DELETE FROM rmsc where gkey= ? ";
        AddParam(cmdD, "original_rmsc_gkey", original_rmsc_gkey);
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
            Insertbalog(connD, thistran, "rmsc", rmsc_actkey, UserGkey);
            Insertbtlog(connD, thistran, "rmsc", DAC.GetStringValue(dr_Del["RDREN"]) + " " + DAC.GetStringValue(dr_Del["RDITM"]), "D", UserGkey, DAC.GetStringValue(dr_Del["RDENO"]).ToString() + " " + DAC.GetStringValue(dr_Del["RDCUS"]) + " " + DAC.GetStringValue(dr_Del["RDNUM"]));
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
    /// UpdateTable rmsc
    /// 必須自定義 daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Update)]
    public string UpdateTable_rmsc(string original_rmsc_gkey, string rmsc_gkey, string rmsc_mkey, string rmsc_RDREN, Int32 rmsc_RDITM, string rmsc_RDDAT, string rmsc_RDENO, string rmsc_RDNUM, string rmsc_RDCUS, string rmsc_actkey, string UserGkey)
    {
      string st_dberrmsg = "";
      string st_tempgkey = "";
      string st_SelDataKey = "";
      //
      DataTable tb_rmsc_upt = new DataTable();
      OleDbCommand CmdQueryObj = new OleDbCommand();
      OleDbConnection connU = NewReaderConnr();
      //
      CmdQueryObj.Parameters.Clear();
      CmdQueryObj.CommandText = " and a.mkey = ? ";
      AddParam(CmdQueryObj, "mkey", rmsc_mkey);
      //
      DbDataAdapter da_ADP_upt = GetDataAdapter(PublicVariable.ApVer, "UNrmsc", "rmsc", "", false, "", CmdQueryObj, "", "", "SEL UPT");
      da_ADP_upt.SelectCommand.Connection = connU;
      da_ADP_upt.UpdateCommand.Connection = connU;
      connU.Open();
      da_ADP_upt.Fill(tb_rmsc_upt);
      st_SelDataKey = "rmsc_mkey='" + rmsc_mkey + "' ";
      DataRow[] mod_rows = tb_rmsc_upt.Select(st_SelDataKey);
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
          mod_row["rmsc_RDENO"] = rmsc_RDENO;       // 員工編號
          mod_row["rmsc_RDNUM"] = rmsc_RDNUM;       // 經銷編號
          mod_row["rmsc_RDCUS"] = rmsc_RDCUS;       // 會員編號
          //
          mod_row["rmsc_mkey"] = DAC.get_guidkey();        //
          mod_row["rmsc_trusr"] = UserGkey;  //
          mod_row.EndEdit();
          da_ADP_upt.Update(tb_rmsc_upt);
          Insertbalog(connU, thistran, "rmsc", rmsc_actkey, UserGkey);
          Insertbtlog(connU, thistran, "rmsc", DAC.GetStringValue(mod_row["rmsc_RDREN"]) + " " + DAC.GetStringValue(mod_row["rmsc_RDITM"]), "M", UserGkey, DAC.GetStringValue(mod_row["rmsc_RDENO"]).ToString() + " " + DAC.GetStringValue(mod_row["rmsc_RDCUS"]) + " " + DAC.GetStringValue(mod_row["rmsc_RDNUM"]));
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
      tb_rmsc_upt.Dispose();
      da_ADP_upt.Dispose();
      //
      CmdQueryObj.Dispose();
      // 
      return st_dberrmsg;
    }

  }
}