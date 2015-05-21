using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using YNetLib_45;

namespace DD2015_45.Forms.sys
{
  public partial class fm_User : FormBase
  {
    string st_object_func = "sys_user";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    //int in_PageSize = 10;
    //OleDbCommand CmdQueryS = DAC.NewCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
      //檢查Db Session狀態
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmsys_user_CmdQueryS"] = CmdQueryS;
          Set_Control();
          SetSerMod();
          BindNew(true);
          Session["fmsys_user_gr_GridView_sys_user_PageIndex"] = gr_GridView_sys_user.PageIndex;
          Session["fmsys_user_gr_GridView_sys_user_SelectedIndex"] = gr_GridView_sys_user.SelectedIndex;

        }
        else
        {
          if ((hh_GridCtrl.Value.ToString().ToLower() == "ins") || (hh_GridCtrl.Value.ToString().ToLower() == "mod"))
          {
            BindNew(false);
          }
          else
          {
            //BindNew(true);
          }
        }
      }
    }

    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      gr_GridView_sys_user.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNsys_user", "sys_user");
    }

    private void ClearText()
    {
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      bt_DEL.Visible = true;
      bt_QUT.Visible = true;
      //
      gr_GridView_sys_user.Enabled = true;
    }

    private void SetEditMod()
    {
      bt_DEL.Visible = false;
      bt_QUT.Visible = false;
      //
      bt_DEL.OnClientClick = " return false;";
      //
      gr_GridView_sys_user.Enabled = false;
      //gr_GridView_sys_user.Columns[0].Visible = true;
    }

    private void BindText(DataRow CurRow)
    {
      clsFN sFN = new clsFN();
      hh_mkey.Value = DAC.GetStringValue(CurRow["sys_user_mkey"]);
      sFN.Dispose();
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmsys_user_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = "";
      }
      //
      DataTable tb_sys_user = new DataTable();
      DAC_login sys_userDao = new DAC_login(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = sys_userDao.GetDataAdapter("YN01", "UNsys_user", "sys_user", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " login_time desc ");
      ad_DataDataAdapter.Fill(tb_sys_user);
      //
      if (tb_sys_user.Rows.Count > 0)
      {
        bt_DEL.OnClientClick = "return btnDEL_c()";
      }
      else
      {
        bt_DEL.OnClientClick = "return btnDEL0_c()";
      }
      gr_GridView_sys_user.DataSource = tb_sys_user;
      gr_GridView_sys_user = clsGV.BindGridView(gr_GridView_sys_user, tb_sys_user, hh_GridCtrl, ref hh_GridGkey, "fmsys_user_gr_GridView_sys_user");
      gr_GridView_sys_user.DataBind();
      SelDataKey = "sys_user_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_sys_user.Select(SelDataKey);
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          BindText(CurRow);
        }
        else
        {
          hh_GridCtrl.Value = "init";
          ClearText();
        }
      }
      tb_sys_user.Dispose();
      sys_userDao.Dispose();
    }

    protected void gr_GridView_sys_user_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_sys_user.PageIndex = e.NewPageIndex;
      Session["fmsys_user_gr_GridView_sys_user_PageIndex"] = gr_GridView_sys_user.PageIndex;
      hh_GridGkey.Value = gr_GridView_sys_user.DataKeys[gr_GridView_sys_user.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_sys_user_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_sys_user.Enabled)
      {
        SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        Session["fmsys_user_gr_GridView_sys_user_PageIndex"] = gr_GridView_sys_user.PageIndex;
        Session["fmsys_user_gr_GridView_sys_user_SelectedIndex"] = gr_GridView_sys_user.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_sys_user_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmsys_user_gr_GridView_sys_user_PageIndex"] = gr_GridView_sys_user.PageIndex;
      Session["fmsys_user_gr_GridView_sys_user_SelectedIndex"] = gr_GridView_sys_user.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_sys_user.DataKeys[gr_GridView_sys_user.SelectedIndex].Value.ToString();
      SetSerMod();
    }

    protected void bt_QUT_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Master/" + Page.Theme + "/MainForm.aspx");
    }

    protected void bt_DEL_Click(object sender, EventArgs e)
    {
      actDEL();
    }

    protected void actDEL()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 5, UserLoginGkey, ref li_AccMsg))
      {
        bool bl_delok = false;
        Set_Control();
        hh_ActKey.Value = DAC.get_guidkey();
        //
        DAC_login sys_userDao = new DAC_login(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "sys_user_gkey='" + hh_GridGkey.Value + "'";
        DataTable tb_sys_user = new DataTable();
        DbDataAdapter da_ADP = sys_userDao.GetDataAdapter("YN01", "UNsys_user", "sys_user", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_sys_user);
        DataRow[] DelRow = tb_sys_user.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            DelRow[0].Delete();
            da_ADP.Update(tb_sys_user);
            thistran.Commit();
            bl_delok = true;
          }
          catch (Exception e)
          {
            thistran.Rollback();
            bl_delok = false;
            lb_ErrorMessage.Visible = true;
            lb_ErrorMessage.Text = e.Message;
          }
          finally
          {
            thistran.Dispose();
            sys_userDao.Dispose();
            tb_sys_user.Dispose();
            da_ADP.Dispose();
            conn.Close();
          }
        }
        tb_sys_user.Clear();
        if (bl_delok)
        {
          gr_GridView_sys_user = clsGV.SetGridCursor("del", gr_GridView_sys_user, -2);
        }
        //
        SetSerMod();
        BindNew(true);
      }
    }

    protected void gr_GridView_sys_user_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      DateTime dt_datavalue;
      clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //帳號
        if (e.Row.FindControl("tx_sys_user_login_id02") != null)
        {
          TextBox tx_sys_user_login_id02 = e.Row.FindControl("tx_sys_user_login_id02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_user_login_id"]).Trim();
          tx_sys_user_login_id02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_login_id02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_login_id02, false); }
        }
        //員工姓名
        if (e.Row.FindControl("tx_sys_user_cname02") != null)
        {
          TextBox tx_sys_user_cname02 = e.Row.FindControl("tx_sys_user_cname02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_user_cname"]).Trim();
          tx_sys_user_cname02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_cname02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_cname02, false); }
        }
        //英文姓名
        if (e.Row.FindControl("tx_sys_user_ename02") != null)
        {
          TextBox tx_sys_user_ename02 = e.Row.FindControl("tx_sys_user_ename02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_user_ename"]).Trim();
          tx_sys_user_ename02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_ename02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_ename02, false); }
        }
        //login_time
        if (e.Row.FindControl("tx_sys_user_login_time02") != null)
        {
          TextBox tx_sys_user_login_time02 = e.Row.FindControl("tx_sys_user_login_time02") as TextBox;
          dt_datavalue = DAC.GetDateTimeValue(rowView["sys_user_login_time"]);
          st_datavalue = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt_datavalue);
          tx_sys_user_login_time02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_login_time02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_login_time02, false); }
        }
        //check_time
        if (e.Row.FindControl("tx_sys_user_check_time02") != null)
        {
          TextBox tx_sys_user_check_time02 = e.Row.FindControl("tx_sys_user_check_time02") as TextBox;
          dt_datavalue = DAC.GetDateTimeValue(rowView["sys_user_check_time"]);
          st_datavalue = String.Format("{0:MM-dd HH:mm:ss}", dt_datavalue);
          tx_sys_user_check_time02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_check_time02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_check_time02, false); }
        }
        //logout_time
        if (e.Row.FindControl("tx_sys_user_logout_time02") != null)
        {
          TextBox tx_sys_user_logout_time02 = e.Row.FindControl("tx_sys_user_logout_time02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_user_logout_time"]).Trim();
          if (st_datavalue != "")
          {
            dt_datavalue = DAC.GetDateTimeValue(rowView["sys_user_logout_time"]);
            st_datavalue = String.Format("{0:MM-dd HH:mm:ss}", dt_datavalue);
          }
          tx_sys_user_logout_time02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_logout_time02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_logout_time02, false); }
        }
        //client_ip
        if (e.Row.FindControl("tx_sys_user_client_ip02") != null)
        {
          TextBox tx_sys_user_client_ip02 = e.Row.FindControl("tx_sys_user_client_ip02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_user_client_ip"]).Trim();
          tx_sys_user_client_ip02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_client_ip02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_client_ip02, false); }
        }
        //login_status
        if (e.Row.FindControl("tx_sys_user_login_status02") != null)
        {
          TextBox tx_sys_user_login_status02 = e.Row.FindControl("tx_sys_user_login_status02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_user_login_status"]).Trim();
          if (st_datavalue == "1")
          {
            tx_sys_user_login_status02.Text = "active";
          }
          else if (st_datavalue == "2")
          {
            tx_sys_user_login_status02.Text = "logout";
          }
          else if (st_datavalue == "0")
          {
            tx_sys_user_login_status02.Text = "timeout";
          }
          else
          {
            tx_sys_user_login_status02.Text = st_datavalue;
          }
          clsGV.TextBox_Set(ref tx_sys_user_login_status02, false);
        }
        //login_gkey
        if (e.Row.FindControl("tx_sys_user_login_gkey02") != null)
        {
          TextBox tx_sys_user_login_gkey02 = e.Row.FindControl("tx_sys_user_login_gkey02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_user_login_gkey"]).Trim();
          tx_sys_user_login_gkey02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_user_login_gkey02, true); } else { clsGV.TextBox_Set(ref tx_sys_user_login_gkey02, false); }
        }
      }
      sFN.Dispose();
    }
  }
}