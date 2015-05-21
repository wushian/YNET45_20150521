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
  public class DAC_login : DAC
  {
    public DAC_login()
      : base()
    {
    }

    public DAC_login(OleDbConnection conn)
      : base(conn)
    {
    }

    public string get_es101gkey(string st_userid, string st_pwd, ref string st_no,ref string st_cname, ref string st_ename)
    {
      OleDbCommand cmd = NewCommand();
      DataTable tb_ss101 = new DataTable();
      DataRow dw_Row;
      string st_es101gkey = "";
      st_cname = "";
      st_ename="";
      st_no="";
      cmd.CommandText = "select a.es101gkey as es101gkey,b.no as no,b.cname as cname,b.ename as ename from ss101 a left outer join es101 b on a.es101gkey=b.gkey  where a.usercode=? and a.password=?  ";
      AddParam(cmd, "usercode", st_userid);
      AddParam(cmd, "password", st_pwd);
      tb_ss101 = Select(cmd);
      if (tb_ss101.Rows.Count == 1)
      {
        dw_Row = tb_ss101.Rows[0];
        st_es101gkey = DAC.GetStringValue(dw_Row["es101gkey"]);
        st_no = DAC.GetStringValue(dw_Row["no"]);
        st_cname = DAC.GetStringValue(dw_Row["cname"]);
        st_ename = DAC.GetStringValue(dw_Row["ename"]);
      }
      cmd.Dispose(); 
      tb_ss101.Dispose();
      return st_es101gkey;
    }

    /// <summary>
    /// login_gkey
    /// st_login_id=ss101_usercoode,st_es101gkey=es101_gkey,cname=return es101_cname,ename=return es101_ename,st_dberrmsg=return errmessage
    /// </summary>
    public string get_login_gkey(string st_login_id, string st_es101gkey,string cname,string ename, ref string st_dberrmsg)
    {
      OleDbDataAdapter ad_DataDataAdapter;
      OleDbCommand CmdQuery= new OleDbCommand();
      DataTable tb_sys_user = new DataTable();
      DataRow ins_row;
      string st_login_gkey = "";
      string st_temp_gkey = "";
      bool bl_insok = false;
      //
      string st_ip = HttpContext.Current.Request.UserHostAddress; 
      if (string.IsNullOrEmpty(st_ip))
      {
       st_ip = "*";
      }
      CmdQuery.CommandText=" and 1=0 ";
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = GetDataAdapter("YN01", "UNsys_user", "sys_user", st_addselect, false, st_addjoin, CmdQuery, st_addunion, "");
      ad_DataDataAdapter.Fill(tb_sys_user);
      //
      conn.Open();
      OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      ad_DataDataAdapter.InsertCommand.Transaction = thistran;
      try
      {
        ins_row = tb_sys_user.NewRow();
        st_temp_gkey = DAC.get_guidkey();
        //
        ins_row["sys_user_login_id"] = st_login_id; //  NVARCHAR(50)  NOT NULL,/*帳號 */
        ins_row["sys_user_cname"] = cname; //  NVARCHAR(40)  NULL,/*員工姓名 */
        ins_row["sys_user_ename"] = ename; //  NVARCHAR(40)  NULL,/*英文姓名 */
        ins_row["sys_user_login_time"] = DateTime.Now; //  DATETIME NOT NULL,/*login_time */
        ins_row["sys_user_check_time"] = DateTime.Now; //  DATETIME NOT NULL,/*check_time */
        ins_row["sys_user_logout_time"] = DBNull.Value; //  DATETIME NULL,/*logout_time */
        ins_row["sys_user_client_ip"] = st_ip; // NVARCHAR(40)  NULL,/*client_ip */
        ins_row["sys_user_login_status"] = "1";    //  NVARCHAR(10)  NOT NULL,/*login_status */
        ins_row["sys_user_es101gkey"] = st_es101gkey; //  VARCHAR(40)  NOT NULL,/*系統使用 */
        ins_row["sys_user_login_gkey"] = st_temp_gkey; //  VARCHAR(40)  NOT NULL,/*login_gkey */
        //
        ins_row["sys_user_gkey"] = DAC.get_guidkey();       // 
        ins_row["sys_user_mkey"] = DAC.get_guidkey(); //
        ins_row["sys_user_trusr"] = st_es101gkey;  //
        tb_sys_user.Rows.Add(ins_row);
        //
        //
        ad_DataDataAdapter.Update(tb_sys_user);
        thistran.Commit();
        bl_insok = true;
      }
      catch (Exception e)
      {
        thistran.Rollback();
        bl_insok = false;
        st_dberrmsg = e.Message;
      }
      finally
      {
        thistran.Dispose();
        tb_sys_user.Dispose();
        ad_DataDataAdapter.Dispose();
        CmdQuery.Dispose();
        conn.Close();
      }
      //
      if (bl_insok)
      {
        st_login_gkey=st_temp_gkey;
      }
      return  st_login_gkey;
    }

    /// <summary>
    /// logout_gkey
    /// </summary>
    public bool logout_gkey(string login_gkey)
    { bool bl_uptok=false;
      OleDbCommand CmdUpdate = new OleDbCommand();
      CmdUpdate.CommandText="update sys_user set login_status='2',logout_time=? where login_gkey=? "; //1=normal 0=timelout 2=logout
      AddParam(CmdUpdate, "logout_time", DateTime.Now);
      AddParam(CmdUpdate, "login_gkey", login_gkey);
      bl_uptok= (ExecuteNonQuery(CmdUpdate)==1?true:false);
      return bl_uptok;
    }

  }
}