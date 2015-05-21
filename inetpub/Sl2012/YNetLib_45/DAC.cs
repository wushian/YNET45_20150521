using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;

namespace YNetLib_45
{
  public class DAC : IDisposable
  {
    protected OleDbConnection conn = null;
    protected OleDbTransaction tran = null;

    public const string connTypeOLEDB = "OLEDB";
    public const string connTypeMSSQL = "MSSQL";
    public static string ConnectionType = connTypeOLEDB;
    public static string ConnectionString = "";

    public DAC()
    {
    }

    public DAC(OleDbConnection conn)
    {
      this.conn = conn;
    }

    /// <summary>
    /// 判斷資料是否存在tablel中
    /// </summary>
    public bool IsExists(string st_table, string st_selectfiled, string st_selectvalue, string st_wherekey)
    {
      return (SelectCount(st_table, st_selectfiled, st_selectvalue, st_wherekey) > 0);
    }

    public Int32 SelectCount(string st_table, string st_selectfiled, string st_selectvalue, string st_wherekey)
    {
      OleDbCommand cmd = NewCommand();
      cmd.CommandText = "select count(1) from " + st_table + " with (nolock) where " + st_selectfiled + "=? ";
      if (st_wherekey.Trim() != "")
      {
        cmd.CommandText += " and " + st_wherekey + " ";
      }
      AddParam(cmd, "st_selectfiled", st_selectvalue);
      int i;
      ExecuteScalar(cmd, out i);
      return i;
    }

    public OleDbCommand GetSelectCommand(string st_ver, string st_apx, string st_tbl, string st_addSelect, bool bl_lock, string st_addJoin, OleDbCommand cmd_QueryKey, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmd_Select = NewCommand();
      DataTable tb_dbset;
      DataRow dr_Row;
      string st_sel = "select", st_jia = "", st_joinalias = "", st_uco = "";
      //
      cmd_Select.CommandText = "select * from dbset where dbver='" + st_ver + "' and dbapx='" + st_apx + "' and dbtbl='" + st_tbl + "' order by dbrow,dbcol,dbfld ";
      tb_dbset = Select(cmd_Select);
      if (tb_dbset.Rows.Count > 0)
      {
        for (int vr = 0; vr <= tb_dbset.Rows.Count - 1; vr++)
        {
          dr_Row = tb_dbset.Rows[vr];
          st_jia = DAC.GetStringValue(dr_Row["dbjia"]).ToLower();
          st_uco = DAC.GetStringValue(dr_Row["dbuco"]).ToLower();
          if (st_jia == "a")
          {
            if (st_uco == "address")
            {
              st_sel += ((st_sel == "select") ? " " : ",") + st_jia + "." + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1" + " as  " + st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1" + " ";
              st_sel += ((st_sel == "select") ? " " : ",") + st_jia + "." + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2" + " as  " + st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2" + " ";
              st_sel += ((st_sel == "select") ? " " : ",") + st_jia + "." + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3" + " as  " + st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3" + " ";
            }
            else
            {
              st_sel += ((st_sel == "select") ? " " : ",") + st_jia + "." + DAC.GetStringValue(dr_Row["dbfld"]) + " as  " + st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]) + " ";
            }
          }
          else
          {
            st_sel += ((st_sel == "select") ? " " : ",") + st_jia + "." + DAC.GetStringValue(dr_Row["dbjif"]) + " as " + DAC.GetStringValue(dr_Row["dbfld"]) + " ";
          }
        }
        st_sel += ",a.gkey as " + st_tbl + "_gkey" + ",a.mkey as " + st_tbl + "_mkey ";
        st_sel += ",a.trcls as " + st_tbl + "_trcls,a.trcrd as " + st_tbl + "_trcrd,a.trmod as " + st_tbl + "_trmod,a.trusr as " + st_tbl + "_trusr";
      }
      if (st_addSelect != "")
      {
        st_sel += "," + st_addSelect;
      }
      st_sel += " from " + st_tbl + " a ";
      if (!bl_lock)
      {
        st_sel += " with (nolock) ";
      }
      //
      st_joinalias = "";
      if (tb_dbset.Rows.Count > 0)
      {
        for (int vr = 0; vr <= tb_dbset.Rows.Count - 1; vr++)
        {
          dr_Row = tb_dbset.Rows[vr];
          st_jia = DAC.GetStringValue(dr_Row["dbjia"]).ToLower();
          if (st_jia != "a")
          {
            if (st_joinalias.IndexOf(st_jia) < 0)
            {
              st_sel += " left outer join " + DAC.GetStringValue(dr_Row["dbjin"]) + " " + st_jia + " on " + DAC.GetStringValue(dr_Row["dbjik"]) + " ";
            }
            st_joinalias += "," + st_jia + ",";
          }
        }
      }
      //
      if (st_addJoin != "")
      {
        st_sel += "," + st_addJoin;
      }
      st_sel += " where 1=1 ";
      //select command 
      cmd_Select.CommandText = st_sel;
      if (cmd_QueryKey.CommandText != "")
      {
        cmd_Select.CommandText = cmd_Select.CommandText + cmd_QueryKey.CommandText;
        for (int in_Pi = 0; in_Pi < cmd_QueryKey.Parameters.Count; in_Pi++)
        {
          DoAddParam(cmd_Select, cmd_QueryKey.Parameters[in_Pi].ParameterName, cmd_QueryKey.Parameters[in_Pi].Value);
        }
      }
      if (st_addUnion != "")
      {
        cmd_Select.CommandText += " union " + st_addUnion + " ";
      }
      if (st_orderKey != "")
      {
        cmd_Select.CommandText += " order by " + st_orderKey + " ";
      }
      tb_dbset.Dispose();
      return cmd_Select;
    }

    public DataRow SelectTableEmptyRow(string st_ver, string st_apx, string st_tbl, string st_addSelect, bool bl_lock, string st_addJoin)
    {
      OleDbCommand cmd_QueryKey = new OleDbCommand();
      cmd_QueryKey.CommandText = " and 1=0 ";
      // 
      OleDbCommand cmd_Select = GetSelectCommand(st_ver, st_apx, st_tbl, st_addSelect, bl_lock, st_addJoin, cmd_QueryKey, "", "");
      DataTable tb_dbset;
      cmd_Select.Connection = conn;
      tb_dbset = Select(cmd_Select);
      //
      cmd_QueryKey.Dispose();
      return tb_dbset.NewRow();
    }

    public DataRow[] SelectTableRows(string st_ver, string st_apx, string st_tbl, string st_addSelect, bool bl_lock, string st_addJoin, OleDbCommand cmd_QueryKey, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmd_Select = GetSelectCommand(st_ver, st_apx, st_tbl, st_addSelect, bl_lock, st_addJoin, cmd_QueryKey, st_addUnion, st_orderKey);
      DataTable tb_dbset;
      cmd_Select.Connection = conn;
      tb_dbset = Select(cmd_Select);
      return tb_dbset.Select("1=1");
    }


    public DataTable SelectTableWhereKeyByOrder(string st_ver, string st_apx, string st_tbl, string st_addSelect, bool bl_lock, string st_addJoin, OleDbCommand cmd_QueryKey, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmd_Select = GetSelectCommand(st_ver, st_apx, st_tbl, st_addSelect, bl_lock, st_addJoin, cmd_QueryKey, st_addUnion, st_orderKey);
      DataTable tb_dbset;
      cmd_Select.Connection = conn;
      tb_dbset = Select(cmd_Select);
      return tb_dbset;
    }

    public DataTable SelectTableWhereKeyByOrder(string st_ver, string st_apx, string st_tbl, string st_addSelect, string st_addJoin, OleDbCommand cmd_QueryKey, string st_orderKey)
    {
      return (SelectTableWhereKeyByOrder(st_ver, st_apx, st_tbl, st_addSelect, false, st_addJoin, cmd_QueryKey, "", st_orderKey));
    }

    public DataTable SelectTableWhereKeyByOrder(string st_ver, string st_apx, string st_tbl, OleDbCommand cmd_QueryKey, string st_orderKey)
    {
      return (SelectTableWhereKeyByOrder(st_ver, st_apx, st_tbl, "", false, "", cmd_QueryKey, "", st_orderKey));
    }

    public OleDbDataAdapter GetDataAdapter(string st_ver, string st_apx, string st_tbl, string st_addSelect, bool bl_lock, string st_addJoin, OleDbCommand cmd_QueryKey, string st_addUnion, string st_orderKey)
    {
      return GetDataAdapter(st_ver, st_apx, st_tbl, st_addSelect, bl_lock, st_addJoin, cmd_QueryKey, st_addUnion, st_orderKey, "SEL INS UPT DEL");
    }

    public OleDbDataAdapter GetDataAdapter(string st_ver, string st_apx, string st_tbl, string st_addSelect, bool bl_lock, string st_addJoin, OleDbCommand cmd_QueryKey, string st_addUnion, string st_orderKey, string st_comfunc)
    {
      OleDbCommand cmd_DD = NewCommand();
      DataTable tb_dbset;
      DataRow dr_Row;
      DataRow[] dr_Rows;
      //
      OleDbDataAdapter da_DataAdapter = NewDataAdapter();
      OleDbCommand cmd_Select = NewCommand();
      //
      string st_uco = "", st_typ = "";
      //select command 
      cmd_Select = GetSelectCommand(st_ver, st_apx, st_tbl, st_addSelect, bl_lock, st_addJoin, cmd_QueryKey, st_addUnion, st_orderKey);
      cmd_Select.Connection = conn;
      da_DataAdapter.SelectCommand = cmd_Select;
      //
      cmd_DD.CommandText = "select * from dbset where dbver='" + st_ver + "' and dbapx='" + st_apx + "' and dbtbl='" + st_tbl + "' order by dbrow,dbcol,dbfld ";
      tb_dbset = Select(cmd_DD);
      //
      //delete command
      if (st_comfunc.ToUpper().IndexOf("DEL") >= 0)
      {
        OleDbCommand cmd_Delete = new OleDbCommand();
        cmd_Delete.CommandText = @"delete from  " + st_tbl + " where gkey=? and mkey=? ";
        cmd_Delete.Parameters.Add("@Or_gkey", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_gkey").SourceVersion = DataRowVersion.Original;
        cmd_Delete.Parameters.Add("@Or_mkey", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_mkey").SourceVersion = DataRowVersion.Original;
        cmd_Delete.Connection = conn;
        da_DataAdapter.DeleteCommand = cmd_Delete;
      }
      //
      //update command
      if (st_comfunc.ToUpper().IndexOf("UPT") >= 0)
      {
        dr_Rows = tb_dbset.Select(" dbmod='1' ", "dbrow,dbcol,dbfld");
        OleDbCommand cmd_Update = new OleDbCommand();
        cmd_Update.CommandText = "";
        if (dr_Rows.Length > 0)
        {
          for (int vr = 0; vr < dr_Rows.Length; vr++)
          {
            dr_Row = dr_Rows[vr];
            st_uco = DAC.GetStringValue(dr_Row["dbuco"]).ToLower();
            st_typ = DAC.GetStringValue(dr_Row["dbtyp"]).ToLower();
            if (cmd_Update.CommandText == "")
            {
              if (st_uco == "address")
              {
                cmd_Update.CommandText += @"update " + st_tbl + " SET ";
                cmd_Update.CommandText += " " + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1" + "= ? ";
                cmd_Update.CommandText += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2" + "= ? ";
                cmd_Update.CommandText += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3" + "= ? ";
              }
              else
              {
                cmd_Update.CommandText += @"update " + st_tbl + " SET " + DAC.GetStringValue(dr_Row["dbfld"]) + "=? ";
              }
            }
            else
            {
              if (st_uco == "address")
              {
                cmd_Update.CommandText += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1" + "= ? ";
                cmd_Update.CommandText += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2" + "= ? ";
                cmd_Update.CommandText += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3" + "= ? ";
              }
              else
              {
                cmd_Update.CommandText += "," + DAC.GetStringValue(dr_Row["dbfld"]) + "=? ";
              }
            }
            //
            if ((DAC.GetStringValue(dr_Row["dbtyp"]).ToUpper() == "NVARCHAR") && (DAC.GetInt16Value(dr_Row["dblen"]) >= 2000) && (st_uco != "address"))
            {
              cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]), DAC.GetDataType(DAC.GetStringValue(dr_Row["dbtyp"])), 10240000, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"])).SourceVersion = DataRowVersion.Current;
            }
            else
            {
              if (st_uco == "address")
              {
                if (st_typ == "naddress")
                {
                  cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1", DAC.GetDataType("NVARCHAR"), 10, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1").SourceVersion = DataRowVersion.Current;
                  cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2", DAC.GetDataType("NVARCHAR"), 50, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2").SourceVersion = DataRowVersion.Current;
                  cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3", DAC.GetDataType("NVARCHAR"), 100, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3").SourceVersion = DataRowVersion.Current;
                }
                else
                {
                  cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1", DAC.GetDataType("VARCHAR"), 10, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1").SourceVersion = DataRowVersion.Current;
                  cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2", DAC.GetDataType("VARCHAR"), 50, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2").SourceVersion = DataRowVersion.Current;
                  cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3", DAC.GetDataType("VARCHAR"), 100, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3").SourceVersion = DataRowVersion.Current;
                }
              }
              else
              {
                cmd_Update.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]), DAC.GetDataType(DAC.GetStringValue(dr_Row["dbtyp"])), DAC.GetInt16Value(dr_Row["dblen"]), st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"])).SourceVersion = DataRowVersion.Current;
              }
            }
            //
          }
          cmd_Update.CommandText += ",mkey=?,trcls='modify',trmod=getdate(),trusr=? ";
          cmd_Update.Parameters.Add("@mkey", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_mkey").SourceVersion = DataRowVersion.Current;
          cmd_Update.Parameters.Add("@trusr", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_trusr").SourceVersion = DataRowVersion.Current;
          //
          cmd_Update.CommandText += " where gkey=? and mkey=? ";
          cmd_Update.Parameters.Add("@Or_gkey", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_gkey").SourceVersion = DataRowVersion.Original;
          cmd_Update.Parameters.Add("@Or_mkey", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_mkey").SourceVersion = DataRowVersion.Original;
        }
        cmd_Update.Connection = conn;
        da_DataAdapter.UpdateCommand = cmd_Update;
      }
      //insert command
      if (st_comfunc.ToUpper().IndexOf("INS") >= 0)
      {
        dr_Rows = tb_dbset.Select(" dbins='1' ", "dbrow,dbcol,dbfld");
        OleDbCommand cmd_Insert = new OleDbCommand();
        string st_insqli = "";
        string st_insqlv = "";
        cmd_Insert.CommandText = "";
        if (dr_Rows.Length > 0)
        {
          for (int vr = 0; vr < dr_Rows.Length; vr++)
          {
            dr_Row = dr_Rows[vr];
            st_uco = DAC.GetStringValue(dr_Row["dbuco"]).ToLower();
            st_typ = DAC.GetStringValue(dr_Row["dbtyp"]).ToLower();
            if (st_insqli == "")
            {
              if (st_uco == "address")
              {
                st_insqli += "insert into " + st_tbl + " (";
                st_insqli += " " + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1" + " ";
                st_insqli += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2" + " ";
                st_insqli += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3" + " ";
                st_insqlv += "( ?,?,?";
              }
              else
              {
                st_insqli += "insert into " + st_tbl + " (" + DAC.GetStringValue(dr_Row["dbfld"]) + " ";
                st_insqlv += "( ?";
              }
            }
            else
            {
              if (st_uco == "address")
              {
                st_insqli += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1" + " ";
                st_insqli += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2" + " ";
                st_insqli += "," + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3" + " ";
                st_insqlv += ",? ,? ,? ";
              }
              else
              {
                st_insqli += "," + DAC.GetStringValue(dr_Row["dbfld"]) + " ";
                st_insqlv += ",? ";
              }
            }
            //
            if ((DAC.GetStringValue(dr_Row["dbtyp"]).ToUpper() == "NVARCHAR") && (DAC.GetInt16Value(dr_Row["dblen"]) >= 2000) && (st_uco != "address"))
            {
              cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]), DAC.GetDataType(DAC.GetStringValue(dr_Row["dbtyp"])), 10240000, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"])).SourceVersion = DataRowVersion.Current;
            }
            else
            {
              if (st_uco == "address")
              {
                if (st_typ == "naddress")
                {
                  cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1", DAC.GetDataType("NVARCHAR"), 10, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1").SourceVersion = DataRowVersion.Current;
                  cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2", DAC.GetDataType("NVARCHAR"), 50, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2").SourceVersion = DataRowVersion.Current;
                  cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3", DAC.GetDataType("NVARCHAR"), 100, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3").SourceVersion = DataRowVersion.Current;
                }
                else
                {
                  cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1", DAC.GetDataType("VARCHAR"), 10, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "1").SourceVersion = DataRowVersion.Current;
                  cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2", DAC.GetDataType("VARCHAR"), 50, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "2").SourceVersion = DataRowVersion.Current;
                  cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3", DAC.GetDataType("VARCHAR"), 100, st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"]).Substring(0, DAC.GetStringValue(dr_Row["dbfld"]).Length - 1) + "3").SourceVersion = DataRowVersion.Current;
                }
              }
              else
              {
                cmd_Insert.Parameters.Add("@" + DAC.GetStringValue(dr_Row["dbfld"]), DAC.GetDataType(DAC.GetStringValue(dr_Row["dbtyp"])), DAC.GetInt16Value(dr_Row["dblen"]), st_tbl + "_" + DAC.GetStringValue(dr_Row["dbfld"])).SourceVersion = DataRowVersion.Current;
              }
            }

          }
          st_insqli += ",gkey ,mkey ,trcls   ,trcrd     ,trmod     ,trusr) ";
          st_insqlv += ",?    ,?    ,'insert',getdate() ,getdate() ,?    ) ";
          cmd_Insert.CommandText = st_insqli + " values " + st_insqlv;
          //
          cmd_Insert.Parameters.Add("@gkey", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_gkey").SourceVersion = DataRowVersion.Current;
          cmd_Insert.Parameters.Add("@mkey", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_mkey").SourceVersion = DataRowVersion.Current;
          cmd_Insert.Parameters.Add("@trusr", DAC.GetDataType("VARCHAR"), 40, st_tbl + "_trusr").SourceVersion = DataRowVersion.Current;
        }
        cmd_Insert.Connection = conn;
        da_DataAdapter.InsertCommand = cmd_Insert;
      }
      //
      cmd_DD.Dispose();
      tb_dbset.Dispose();
      //
      return da_DataAdapter;
    }

    public DataTable SelectDbset(string st_ver, string st_apx, string st_tbl)
    {
      DataTable tb_ss102dd = new DataTable();
      OleDbCommand cmdd = new OleDbCommand();
      cmdd.Connection = conn;
      cmdd.CommandText = "";
      cmdd.CommandText += "select b.DBJIA as sys_dbset_DBJIA ,a.DBFLD as  sys_dbset_DBFLD ,a.DBCNA as  sys_dbset_DBCNA ,a.DBTNA as  sys_dbset_DBTNA ,a.DBENA as  sys_dbset_DBENA ,a.DBVNA as  sys_dbset_DBVNA ,a.DBDEF as  sys_dbset_DBDEF ,a.DBSOR as  sys_dbset_DBSOR ,a.DBTBL as  sys_dbset_DBTBL ";
      cmdd.CommandText += "from sys_dbset a  with (nolock) ";
      cmdd.CommandText += "left outer join dbset b on a.DBVER=b.DBVER AND A.DBAPX=b.DBAPX and a.DBTBL=b.DBTBL and a.DBFLD=b.DBFLD ";
      cmdd.CommandText += "where a.DBVER='" + st_ver + "' AND A.DBAPX='" + st_apx + "' and a.DBTBL='" + st_tbl + "' ";
      cmdd.CommandText += "union ";
      cmdd.CommandText += "select a.DBJIA as sys_dbset_DBJIA ,a.DBFLD as  sys_dbset_DBFLD ,a.DBCNA as  sys_dbset_DBCNA ,a.DBTNA as  sys_dbset_DBTNA ,a.DBENA as  sys_dbset_DBENA ,a.DBCNA as  sys_dbset_DBVNA ,a.DBDEF as  sys_dbset_DBDEF ,a.DBSOR as  sys_dbset_DBSOR ,a.DBTBL as  sys_dbset_DBTBL ";
      cmdd.CommandText += "from dbset a  with (nolock) ";
      cmdd.CommandText += "where 1=1  AND a.DBVER='" + st_ver + "' AND A.DBAPX='" + st_apx + "' and a.DBTBL='" + st_tbl + "' ";
      cmdd.CommandText += @" and a.DBVER+a.DBAPX+a.DBTBL+a.DBFLD NOT in (select a.DBVER+a.DBAPX+a.DBTBL+a.DBFLD  from sys_dbset) order by  sys_dbset_DBFLD ";
      tb_ss102dd = Select(cmdd);
      cmdd.Dispose();
      return tb_ss102dd;
    }


    public string GetDbsetName(DataTable tb_dbset, string st_lang, string st_defstr, string st_field)
    {
      DataRow[] da_Rows;

      da_Rows = tb_dbset.Select("sys_dbset_DBFLD='" + st_field + "'");
      if (da_Rows.Length == 1)
      {
        if (st_lang == "ZH-TW")
        {
          st_defstr = DAC.GetStringValue(da_Rows[0]["sys_dbset_DBTNA"]);
        }
        else if (st_lang == "ZH-CN")
        {
          st_defstr = DAC.GetStringValue(da_Rows[0]["sys_dbset_DBCNA"]);
        }
        else if (st_lang == "ZH-VN")
        {
          st_defstr = DAC.GetStringValue(da_Rows[0]["sys_dbset_DBVNA"]);
        }
        else if (st_lang == "ZH-US")
        {
          st_defstr = DAC.GetStringValue(da_Rows[0]["sys_dbset_DBENA"]);
        }
        else
        {
          st_defstr = DAC.GetStringValue(da_Rows[0]["sys_dbset_DBTNA"]);
        }

      }
      return st_defstr;
    }

    public string GET_SS102_status(string st_code)
    {
      string st_ret = "";
      OleDbCommand cmd = NewCommand();
      OleDbDataReader rd_reader = null;
      cmd.CommandText = "select code as ss102_code,status as ss102_status from ss102 where code=? ";
      AddParam(cmd, "code", st_code);
      try
      {
        rd_reader = SelectReader(cmd, rd_reader);
        if (rd_reader.Read())
        {
          st_ret = GetStringValue(rd_reader["ss102_status"]);
        }
      }
      finally
      {
        rd_reader.Close();
        cmd.Dispose();
        conn.Close();
        rd_reader.Dispose();
      }
      return st_ret;
    }

    public static OleDbType GetDataType(string st_DataType)
    {
      OleDbType RetType;
      st_DataType = st_DataType.ToUpper();
      if (st_DataType == "NVARCHAR") { RetType = OleDbType.VarWChar; }
      else if (st_DataType == "VARCHAR") { RetType = OleDbType.VarChar; }
      else if (st_DataType == "BIGINT") { RetType = OleDbType.BigInt; }
      else if (st_DataType == "INT") { RetType = OleDbType.Integer; }
      else if (st_DataType == "INT_IDENT") { RetType = OleDbType.Integer; }
      else if (st_DataType == "DATETIME") { RetType = OleDbType.DBTimeStamp; }
      else if (st_DataType == "BOOLEAN") { RetType = OleDbType.Boolean; }
      else if (st_DataType == "CHAR") { RetType = OleDbType.Char; }
      else if (st_DataType == "CURRENCY") { RetType = OleDbType.Currency; }
      else if (st_DataType == "MONEY") { RetType = OleDbType.Currency; }
      else if (st_DataType == "REAL") { RetType = OleDbType.Single; }
      else if (st_DataType == "FLOAT") { RetType = OleDbType.Double; }
      else { RetType = OleDbType.VarWChar; }
      return (RetType);
    }

    public static string get_guidkey()
    {
      string gkey = Guid.NewGuid().ToString("N");
      return gkey;
    }

    public static void checkAccessFunc(string st_uxxx, string st_fxxx)
    {

    }


    public static OleDbConnection NewReaderConnr()
    {
      OleDbConnection connr;
      connr = NewConnection();
      connr.ConnectionString = ConnectionString;
      return connr;
    }

    public OleDbDataReader OleDbReader(OleDbConnection connr, OleDbCommand cmd)
    {
      OleDbDataReader rd_reader;
      cmd.Connection = connr;
      rd_reader = cmd.ExecuteReader();
      return rd_reader;
    }

    /// <summary>
    /// 確定是可有原conn,如果沒有再新conn物件,不open
    /// </summary>
    private void EnsureConnection()
    {
      if (conn == null)
      {
        conn = NewConnection();
        conn.ConnectionString = ConnectionString;
      }
    }

    public OleDbConnection Connection
    {
      set
      {
        conn = value;
      }
      get
      {
        return conn;
      }
    }

    public OleDbTransaction Transaction
    {
      set
      {
        tran = value;
      }
      get
      {
        return tran;
      }
    }

    #region Factory Method

    public static OleDbConnection NewConnection()
    {
      switch (ConnectionType)
      {
        case connTypeOLEDB:
          return new OleDbConnection();
        default:
          return null;
      }
    }

    public static OleDbCommand NewCommand()
    {
      switch (ConnectionType)
      {
        case connTypeOLEDB:
          return new OleDbCommand();

        default:
          return null;
      }
    }

    public static OleDbDataAdapter NewDataAdapter()
    {
      switch (ConnectionType)
      {
        case connTypeOLEDB:
          return new OleDbDataAdapter();
        default:
          return null;
      }
    }

    public static OleDbCommandBuilder NewCommandBuilder()
    {
      switch (ConnectionType)
      {
        case connTypeOLEDB:
          return new OleDbCommandBuilder();
        default:
          return null;
      }
    }

    public static OleDbConnectionStringBuilder NewConnectionStringBuilder()
    {
      switch (ConnectionType)
      {
        case connTypeOLEDB:
          return new OleDbConnectionStringBuilder();
        default:
          return null;
      }
    }

    public static OleDbParameter NewParameter(string parameterName, object value)
    {
      switch (ConnectionType)
      {
        case connTypeOLEDB:
          return new OleDbParameter(parameterName, value);
        default:
          return null;
      }
    }

    #endregion

    /// <summary>
    /// EnsureConnection 處理確定open連接
    /// </summary>
    public bool OpenConnection()
    {
      EnsureConnection();

      if (conn.State != ConnectionState.Open)
      {
        conn.Open();
        return true;
      }
      return false;
    }

    /// <summary>
    /// 指定的connr,處理確定open連接
    /// </summary>
    public bool OpenConnection(OleDbConnection connr)
    {
      if (connr.State != ConnectionState.Open)
      {
        connr.Open();
        return true;
      }
      return false;
    }

    /// <summary>
    /// EnsureConnection 處理確定close連接
    /// </summary>
    public void CloseConnection(bool wantClose)
    {
      EnsureConnection();

      if (wantClose && conn.State != ConnectionState.Closed)
        conn.Close();
    }

    /// <summary>
    /// 執行 SELECT SQL，回傳 DataTable
    /// </summary>
    public DataTable Select(OleDbCommand cmd)
    {
      EnsureConnection();

      DataTable result = new DataTable();
      OleDbDataAdapter da = NewDataAdapter();
      da.SelectCommand = cmd;
      cmd.Connection = conn;
      cmd.Transaction = tran;
      bool wantClose = false;
      try
      {
        wantClose = OpenConnection();
        da.Fill(result);
      }
      finally
      {
        CloseConnection(wantClose);
        cmd.Dispose();
        da.Dispose(); // 
      }
      return result;
    }

    /// <summary>
    /// 執行 SELECT SQL，回傳 DataTable
    /// </summary>
    public DataTable Select(OleDbConnection connr, OleDbCommand cmd)
    {
      OpenConnection(connr);
      DataTable result = new DataTable();
      OleDbDataAdapter da = NewDataAdapter();
      da.SelectCommand = cmd;
      cmd.Connection = connr;
      try
      {
        da.Fill(result);
      }
      finally
      {
        cmd.Dispose();
        da.Dispose(); // 
        connr.Close();
      }
      return result;
    }

    /// <summary>
    /// 執行 SELECT SQL，填入指定的 IDataList 執行個體 DataReader
    /// </summary>
    public void Select(OleDbCommand cmd, ref IDataList result)
    {
      EnsureConnection();
      OleDbDataReader reader = null;

      bool wantClose = false;
      try
      {
        wantClose = OpenConnection();
        cmd.Connection = conn;
        cmd.Transaction = tran;
        reader = cmd.ExecuteReader();
        result.Fill(reader);
      }
      finally
      {
        if (reader != null)
        {
          reader.Close();
          reader.Dispose();
        }
        cmd.Dispose();
        CloseConnection(wantClose);
      }
    }

    /// <summary>
    /// 執行 SELECT SQL，回傳個體 OleDbDataReader
    /// </summary>
    public OleDbDataReader SelectReader(OleDbCommand cmd, OleDbDataReader rd_reader)
    {
      EnsureConnection();
      bool wantClose = false;
      try
      {
        wantClose = OpenConnection();
        cmd.Connection = conn;
        cmd.Transaction = tran;
        rd_reader = cmd.ExecuteReader();
      }
      finally
      {
      }
      return rd_reader;
    }


    public static void DoAddParam(OleDbCommand cmd, string paramName, object paramValue)
    {
      switch (ConnectionType)
      {
        case connTypeOLEDB:
          (cmd as OleDbCommand).Parameters.AddWithValue(paramName, paramValue);
          break;
      }
    }

    /// <summary>
    /// 把參數以及參數值加入 SqlCommand.Parameters
    /// </summary>
    public static void AddParam(OleDbCommand cmd, string paramName, string value)
    {
      if (cmd.Parameters.IndexOf(paramName) >= 0)
      {
        if (value != null)
          cmd.Parameters[paramName].Value = value;
        else
          cmd.Parameters[paramName].Value = DBNull.Value;
      }
      else
      {
        if (value != null)
          DoAddParam(cmd, paramName, value);
        else
          DoAddParam(cmd, paramName, DBNull.Value);
      }
    }

    public static void AddParam(OleDbCommand cmd, string paramName, OleDbType DataType, int DataLen, DataRowVersion data_version)
    {
      if (cmd.Parameters.IndexOf(paramName) < 0)
      {
        cmd.Parameters.Add("@" + paramName, DataType, DataLen, paramName);
      }
      cmd.Parameters["@" + paramName].SourceVersion = data_version;
    }

    /// <summary>
    /// 把參數以及參數值加入 SqlCommand.Parameters
    /// </summary>
    public static void AddParam(OleDbCommand cmd, string paramName, DateTime value)
    {
      if (cmd.Parameters.IndexOf(paramName) >= 0)
      {
        if (value.Ticks > 0)
          cmd.Parameters[paramName].Value = value;
        else
          cmd.Parameters[paramName].Value = DBNull.Value;
      }
      else
      {
        if (value.Ticks > 0)
          DoAddParam(cmd, paramName, value);
        else
          DoAddParam(cmd, paramName, DBNull.Value);
      }
    }

    /// <summary>
    /// 把參數以及參數值加入 SqlCommand.Parameters
    /// </summary>
    public static void AddParam(OleDbCommand cmd, string paramName, bool value)
    {
      if (cmd.Parameters.IndexOf(paramName) >= 0)
      {
        cmd.Parameters[paramName].Value = value;
      }
      else
      {
        DoAddParam(cmd, paramName, value);
      }
    }

    /// <summary>
    /// 把參數以及參數值加入 SqlCommand.Parameters
    /// </summary>
    public static void AddParam(OleDbCommand cmd, string paramName, Int32 value)
    {
      if (cmd.Parameters.IndexOf(paramName) >= 0)
      {
        cmd.Parameters[paramName].Value = value;
      }
      else
      {
        DoAddParam(cmd, paramName, value);
      }
    }

    /// <summary>
    /// 把參數以及參數值加入 SqlCommand.Parameters
    /// </summary>
    public static void AddParam(OleDbCommand cmd, string paramName, Int16 value)
    {
      if (cmd.Parameters.IndexOf(paramName) >= 0)
      {
        cmd.Parameters[paramName].Value = value;
      }
      else
      {
        DoAddParam(cmd, paramName, value);
      }
    }

    /// <summary>
    /// 把參數以及參數值加入 SqlCommand.Parameters
    /// </summary>
    public static void AddParam(OleDbCommand cmd, string paramName, double value)
    {
      if (cmd.Parameters.IndexOf(paramName) >= 0)
      {
        cmd.Parameters[paramName].Value = value;
      }
      else
      {
        DoAddParam(cmd, paramName, value);
      }
    }

    /// <summary>
    /// 把參數以及參數值加入 SqlCommand.Parameters
    /// </summary>
    public static void AddParam(OleDbCommand cmd, string paramName, object value)
    {
      if (cmd.Parameters.IndexOf(paramName) >= 0)
      {
        cmd.Parameters[paramName].Value = value;
      }
      else
      {
        DoAddParam(cmd, paramName, value);
      }
    }

    /// <summary>
    /// 執行 SELECT SQL，傳回第一筆資料的第一個欄位值
    /// </summary>
    protected bool ExecuteScalar(OleDbCommand cmd, out object result)
    {
      EnsureConnection();

      bool wantClose = false;
      try
      {
        wantClose = OpenConnection();
        cmd.Connection = conn;
        cmd.Transaction = tran;
        result = cmd.ExecuteScalar();
      }
      finally
      {
        CloseConnection(wantClose);
        cmd.Dispose();
      }

      return result != null && result != DBNull.Value;
    }

    /// <summary>
    /// 執行 SELECT SQL，傳回第一筆資料的第一個欄位值
    /// </summary>
    protected bool ExecuteScalar(OleDbCommand cmd, out int result)
    {
      object o;
      if (ExecuteScalar(cmd, out o))
      {
        result = Convert.ToInt32(o);
        return true;
      }
      {
        result = 0;
        return false;
      }
    }

    /// <summary>
    /// 執行 SELECT SQL，傳回第一筆資料的第一個欄位值
    /// </summary>
    protected bool ExecuteScalar(OleDbCommand cmd, out string result)
    {
      object o;
      if (ExecuteScalar(cmd, out o))
      {
        result = o.ToString();
        return true;
      }
      {
        result = "";
        return false;
      }
    }

    /// <summary>
    /// 執行 SELECT SQL，傳回第一筆資料的第一個欄位值
    /// </summary>
    protected bool ExecuteScalar(OleDbCommand cmd, out DateTime result)
    {
      object o;
      if (ExecuteScalar(cmd, out o))
      {
        result = Convert.ToDateTime(o);
        return true;
      }
      {
        result = new DateTime();
        return false;
      }
    }

    /// <summary>
    /// 執行 SELECT SQL，傳回第一筆資料的第一個欄位值
    /// </summary>
    protected bool ExecuteScalar(OleDbCommand cmd, out double result)
    {
      object o;
      if (ExecuteScalar(cmd, out o))
      {
        result = Convert.ToDouble(o);
        return true;
      }
      {
        result = 0d;
        return false;
      }
    }

    /// <summary>
    /// 啟動一個交易
    /// </summary>
    public OleDbTransaction StartTransaction()
    {
      EnsureConnection();

      if (conn.State == ConnectionState.Closed)
      {
        conn.Open();
      }
      return conn.BeginTransaction(IsolationLevel.ReadCommitted);
    }

    /// <summary>
    /// 認可一個交易
    /// </summary>
    public void CommitTransaction(OleDbTransaction tran)
    {
      tran.Commit();
      tran = null;
    }

    /// <summary>
    /// 駁回一個交易
    /// </summary>
    public void RollbackTransaction(OleDbTransaction tran)
    {
      tran.Rollback();
      tran = null;
    }

    /// <summary>
    /// 執行非 SELECT 的 SQL 敘述
    /// </summary>
    public int ExecuteNonQuery(OleDbCommand cmd)
    {
      EnsureConnection();

      bool wantClose = false;
      int rowsAffected = 0;
      try
      {
        wantClose = OpenConnection();
        cmd.Connection = conn;
        cmd.Transaction = tran;
        rowsAffected = cmd.ExecuteNonQuery();
      }
      finally
      {
        CloseConnection(wantClose);
        cmd.Dispose();
      }
      return rowsAffected;
    }

    /// <summary>
    /// 執行非 SELECT 的 SQL 敘述，用於迴圈中大量執行之用
    /// </summary>
    public int ExecuteNonQueryBulk(OleDbCommand cmd)
    {
      int in_rcount = 0;
      cmd.Connection = conn;
      cmd.Transaction = tran;
      in_rcount = cmd.ExecuteNonQuery();
      return in_rcount;
    }

    public void Insertbalog(OleDbCommand cmd, string st_ActKey, string st_UserId)
    {
      cmd.CommandText = @" insert into balog (lgkey,lgdate,lguser) values ( ?, ?, ?) ";
      cmd.Parameters.Clear();
      AddParam(cmd, "lgkey", st_ActKey);
      AddParam(cmd, "lgdate", DateTime.Now);
      AddParam(cmd, "lguser", st_UserId);
      ExecuteNonQueryBulk(cmd);
    }

    public void Insertbalog(OleDbConnection ConnF, OleDbTransaction Tranf, string st_table, string st_ActKey, string st_UserId)
    {
      OleDbCommand cmd = NewCommand();
      cmd.Connection = ConnF;
      cmd.Transaction = Tranf;
      cmd.Parameters.Clear();
      cmd.CommandText = @" insert into balog (lgtbl,lgkey,lgdate,lguser) values (?, ?, ?, ?) ";
      AddParam(cmd, "lgtbl", st_table);
      AddParam(cmd, "lgkey", st_ActKey);
      AddParam(cmd, "lgdate", DateTime.Now);
      AddParam(cmd, "lguser", st_UserId);
      cmd.ExecuteNonQuery();
      cmd.Dispose();
    }

    public void Insertbtlog(OleDbCommand cmd, string st_tablename, string st_datakey, string st_typ, string st_UserId, string st_datadesc)
    {
      cmd.Parameters.Clear();
      cmd.CommandText = @" insert into btlog (btdat,bttab,btkey,bttyp,btusr,btrmk) values ( ?, ?, ?, ?, ?, ?) ";
      AddParam(cmd, "btdat", DateTime.Now);
      AddParam(cmd, "bttab", st_tablename);
      AddParam(cmd, "btkey", st_datakey);
      AddParam(cmd, "bttyp", st_typ);
      AddParam(cmd, "btusr", st_UserId);
      AddParam(cmd, "btrmk", st_datadesc);
      ExecuteNonQueryBulk(cmd);
    }

    public void Insertbtlog(OleDbConnection ConnF, OleDbTransaction Tranf, string st_tablename, string st_datakey, string st_typ, string st_UserId, string st_datadesc)
    {
      OleDbCommand cmd = NewCommand();
      cmd.Connection = ConnF;
      cmd.Transaction = Tranf;
      cmd.Parameters.Clear();
      cmd.CommandText = @" insert into btlog (btdat,bttab,btkey,bttyp,btusr,btrmk) values ( ?, ?, ?, ?, ?, ?) ";
      AddParam(cmd, "btdat", DateTime.Now);
      AddParam(cmd, "bttab", st_tablename);
      AddParam(cmd, "btkey", st_datakey);
      AddParam(cmd, "bttyp", st_typ);
      AddParam(cmd, "btusr", st_UserId);
      AddParam(cmd, "btrmk", st_datadesc);
      cmd.ExecuteNonQuery();
      cmd.Dispose();
    }

    /// <summary>
    /// 取得單號 S201406 0001
    /// st_Unit = dcnews
    /// st_cls  = 201406
    /// st_cos  = 1
    /// st_head = S201406
    /// in_len  = 4 流水號長度
    /// 取得單號 
    /// </summary>
    public string GetRenW(OleDbConnection Conn, string st_unit, string st_cls, string st_cos, string st_head, string st_yymmtext, int in_len, bool bl_lock)
    {
      string st_retren = "", st_val = "";
      int in_headlen = 0, in_val = 0;
      OleDbCommand cmd = NewCommand();
      DataTable tb_ren = new DataTable();
      //
      in_headlen = st_head.Length + st_yymmtext.Length + in_len;
      //
      cmd.Connection = Conn;
      cmd.Parameters.Clear();
      cmd.CommandText = "";
      cmd.CommandText += "select brval from sys_brenw ";
      cmd.CommandText += (bl_lock == true) ? " " : " with (nolock) ";
      cmd.CommandText += " where bruni=? and brcls=? and brcos=? and len(brval)=" + in_headlen.ToString();
      AddParam(cmd, "bruni", st_unit);
      AddParam(cmd, "brcls", st_cls);
      AddParam(cmd, "brcos", st_cos);
      tb_ren = Select(cmd);
      if (tb_ren.Rows.Count == 0)
      {

        st_retren = st_head + st_yymmtext + GetStringZeroi(0, in_len);
        OleDbCommand cmdi = NewCommand();
        Conn.Open();
        cmdi.Connection = Conn;
        cmdi.Parameters.Clear();
        cmdi.CommandText = "insert into sys_brenw (bruni,brcls,brcos,brval,brrmk,trcls,trcrd,trmod,trusr) values (?,?,?,?,?,?,?,?,?) ";
        AddParam(cmdi, "bruni", st_unit);
        AddParam(cmdi, "brcls", st_cls);
        AddParam(cmdi, "brcos", st_cos);
        AddParam(cmdi, "brval", st_retren);
        AddParam(cmdi, "brrmk", "");
        AddParam(cmdi, "trcls", "insert");
        AddParam(cmdi, "trcrd", DateTime.Today);
        AddParam(cmdi, "trmod", DateTime.Today);
        AddParam(cmdi, "trusr", "Admin");
        cmdi.ExecuteNonQuery();
        st_retren = st_head + st_yymmtext + GetStringZeroi(1, in_len);
      }
      else
      {
        in_headlen = st_head.Length + st_yymmtext.Length;
        st_val = GetStringValue(tb_ren.Rows[0]["brval"]).Substring(in_headlen, in_len);
        in_val = Convert.ToInt32(st_val) + 1;
        st_retren = st_head + st_yymmtext + GetStringZeroi(in_val, in_len);
      }
      tb_ren.Dispose();
      cmd.Dispose();
      return st_retren;
    }

    /// <summary>
    /// 更新單號 S201406 0002
    /// st_unit = dcnews
    /// st_cls  = 201406
    /// st_cos  = 1
    /// st_val = S2014060002
    /// 更新單號 
    /// </summary>
    public bool UpDateRenW(OleDbConnection Conn, OleDbTransaction thisTran, string st_unit, string st_cls, string st_cos, string st_val)
    {
      bool bl_updateok = false;
      OleDbCommand cmd = NewCommand();
      cmd.Parameters.Clear();
      cmd.Connection = Conn;
      cmd.CommandText = "update sys_brenw set brval=? where bruni=? and brcls=? and brcos=? ";
      AddParam(cmd, "brval", st_val);
      AddParam(cmd, "bruni", st_unit);
      AddParam(cmd, "brcls", st_cls);
      AddParam(cmd, "brcos", st_cos);
      cmd.Transaction = thisTran;
      cmd.ExecuteNonQuery();
      bl_updateok = true;
      cmd.Dispose();
      return bl_updateok;
    }

    /// <summary>
    /// 更新單號 S201406 0002
    /// st_unit = dcnews
    /// st_cls  = 201406
    /// st_cos  = 1
    /// st_val = S2014060002
    /// 更新單號 
    /// </summary>
    public bool UpDateRenW(string st_unit, string st_cls, string st_cos, string st_val)
    {
      bool bl_updateok = false;
      OleDbCommand cmd = NewCommand();
      cmd.Parameters.Clear();
      cmd.Connection = conn;
      cmd.CommandText = "update sys_brenw set brval=? where bruni=? and brcls=? and brcos=? ";
      AddParam(cmd, "brval", st_val);
      AddParam(cmd, "bruni", st_unit);
      AddParam(cmd, "brcls", st_cls);
      AddParam(cmd, "brcos", st_cos);
      ExecuteNonQuery(cmd);
      //cmd.ExecuteNonQuery();
      bl_updateok = true;
      return bl_updateok;
    }


    public static string GetStringZeroi(int in_value, int in_len)
    {
      string st_Str1 = "", st_Str2 = "";
      int in_Len1 = 0, in_Len2 = 0, in_Len3 = 0;
      st_Str1 = in_value.ToString();
      in_Len1 = st_Str1.Length;
      in_Len2 = in_len - in_Len1;
      in_Len3 = 0;
      while (in_Len3 < in_Len2)
      {
        st_Str2 = st_Str2 + "0";
        in_Len3 = in_Len3 + 1;
      }
      return (st_Str2 + st_Str1);
    }

    public static string GetStringValue(object o)
    {
      return (o != DBNull.Value && o != null) ? o.ToString() : "";
    }

    public static string GetStringValueBool(bool bl)
    {
      return (bl == true) ? "1" : "0";
    }

    public static int GetInt32Value(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToInt32(o) : 0;
    }

    public static Int64 GetInt64Value(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToInt64(o) : Convert.ToInt64(0);
    }

    public static Int16 GetInt16Value(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToInt16(o) : Convert.ToInt16(0);
    }

    public static double GetDoubleValue(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToDouble(o) : 0D;
    }

    public static DateTime GetDateTimeValue(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToDateTime(o) : new DateTime();
    }

    public static Boolean GetBooleanValue(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToBoolean(o) : false;
    }

    public static Boolean GetBooleanValueString(string s)
    {
      return (s == "1") ? true : false;
    }

    public static Byte GetByteValue(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToByte(o) : Convert.ToByte(0);
    }

    public static Decimal GetDecimalValue(object o)
    {
      return (o != DBNull.Value && o != null && o.ToString() != "") ? Convert.ToDecimal(o) : Convert.ToDecimal(0);
    }

    public static Decimal GetDecimalValueString(string st)
    {
      return (st != "") ? Convert.ToDecimal(st) : Convert.ToDecimal(0);
    }

    public static String GetGridViewRowId(int in_index)
    {
      if ((in_index >= 0) && (in_index < 10))
      {
        return ("0" + in_index.ToString());
      }
      else
      {
        return in_index.ToString();
      }
    }

    public static String GetWebDataGridRowId(string st_WebDataGrid_Name, int bindItem, int in_index)
    {
      //ctl00$ContentPlaceHolder1$WebDataGrid_grsa$it0_1$ck_grsa_RIOKE
      //ctl00$ContentPlaceHolder1$WebDataGrid_grsa$it0_0$tx_bpud_gkey02
      return st_WebDataGrid_Name + "$" + "it" + bindItem.ToString() + "_" + in_index.ToString() + "$";
    }

    public decimal Round(decimal vVAlue, byte vLN)
    {
      double vRET = 0;
      vRET = Round(Convert.ToDouble(vVAlue), vLN);
      return Convert.ToDecimal(vRET);
    }

    public double Round(double vVAlue, byte vLN)
    {
      double vRET = 0;
      string vS1 = "";
      int vPOS = 0;
      //
      if (vVAlue < 0)
      {
        vS1 = (vVAlue * Math.Pow(10, vLN) - 0.5).ToString();
        vPOS = vS1.IndexOf(".");
        if (vPOS > -1)
        {
          vS1 = vS1.Substring(0, vPOS);
        }
        else if (vS1.IndexOf(".") == 0)
        {
          vS1 = "0";
        }
        vRET = Math.Floor(Convert.ToDouble(vS1)) / Math.Pow(10, vLN);
      }
      else if (vVAlue > 0)
      {
        vS1 = (vVAlue * Math.Pow(10, vLN) + 0.5).ToString();
        vPOS = vS1.IndexOf(".");
        if (vPOS > -1)
        {
          vS1 = vS1.Substring(0, vPOS);
        }
        else if (vS1.IndexOf(".") == 0)
        {
          vS1 = "0";
        }
        vRET = Math.Floor(Convert.ToDouble(vS1)) / Math.Pow(10, vLN);
      }
      else
      {
        vRET = vVAlue;
      }
      return vRET;
    }



    #region IDisposable 成員

    public void Dispose()
    {
      if (conn != null)
      {
        if (conn.State != ConnectionState.Closed)
          conn.Close();
      }
      // GC.SuppressFinalize(this);
    }

    #endregion
  }

  public interface IDataList
  {
    void Fill(OleDbDataReader reader);
  }
}
