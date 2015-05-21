using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using YNetLib_45;

namespace DD2015_45.Forms.rix
{
  public partial class fm_ri3a : FormBase
  {
    public string st_object_func = "UNri3a";
    public string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    public string st_ContentPlaceHolderEdit = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$";
    bool bl_resetKey = true;
    bool bl_showRowA = true;
    //
    string st_dd_apx = "UNri3a";         //UNdcnews   與apx 相關
    //string st_dd_table = "ri3a";         //dcnews     與table 相關 
    string st_ren_head = "SA";            //DC         與單號相關 
    string st_ren_yymmtext = "YYYYMM";     //"         與單號相關 
    string st_ren_cls = "ri3a";        //ren        與單號cls相關 
    string st_ren_cos = "1";        //1          與單號cos相關 
    int in_ren_len = 4;            //6          與單號流水號 
    //
    private OleDbCommand CmdQueryS_ba = new OleDbCommand();
    private OleDbCommand CmdQueryS_b = new OleDbCommand();
    private OleDbCommand Cmd_ri3b_RBUNI = new OleDbCommand();
    //
    protected void Page_Load(object sender, EventArgs e)
    {
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          Set_DataSource();
          //
          dr_ri3a_RIRTP = sFN.DropDownListFromClasses(ref dr_ri3a_RIRTP, "UNbcvwh_RTP", "class_text", "class_value");
          dr_ri3a_RITME = sFN.DropDownListFromClasses(ref dr_ri3a_RITME, "UNbcvwh_TME", "class_text", "class_value");
          dr_ri3a_RITXP = sFN.DropDownListFromClasses(ref dr_ri3a_RITXP, "UNtax_TXP", "class_text", "class_value");
          dr_ri3a_RIIVT = sFN.DropDownListFromClasses(ref dr_ri3a_RIIVT, "UNtax_IVT", "class_text", "class_value");
          dr_ri3a_RICNS = sFN.DropDownListFromTable(ref dr_ri3a_RICNS, "bdlr", "BDNUM", "BDNAM", " BDTYP='P' ", "BDNUM");
          //
          dr_ri3a_RIEDT = sFN.DropDownList_YYYYMM(ref dr_ri3a_RIEDT, DateTime.Today, -5, 2, "YYYYMM", 5);
          DateTime dt_EDT = sFN.DateToLastDate(DateTime.Today);
          string st_EDT = sFN.strzeroi(dt_EDT.Year, 4) + "-" + sFN.strzeroi(dt_EDT.Month, 2) + "-" + sFN.strzeroi(dt_EDT.Day, 2);
          dr_ri3a_RIEDT = sFN.SetDropDownList(ref dr_ri3a_RIEDT, st_EDT);
          //
          WebTab_form.SelectedIndex = 0;
          Set_Control_A();
          //
          tx_ri3a_RIDAT_s1.Value = sFN.DateToFirstDate(DateTime.Today.AddMonths(-6));
          tx_ri3a_RIDAT_s2.Value = DateTime.Today;
          tx_ri3a_RIREN_s.Text = "";
          //
          tx_ri3a_RIDAT.Value = DateTime.Today;
          //
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_ri3b, false);
          //this.Form.Attributes.Add("onkeydown", "do_Keydown_EnterToTab();");    //SiteMM.Master中輸入Enter轉Tab
          SetSerMod_B();
          //
          if (Session["fmri3a_CmdQueryS"] == null)
          {
            act_SERS_L();
          }
          else
          {
            get_CmdQueryS();
            Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
            get_CmdQueryS_ba();
            Bind_WebDataGrid_A(WebDataGrid_ri3ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
          }
          tx_ri3a_RIMEN.Attributes.Add("onblur", "return get_es101_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_ri3a_RIMEN','" + st_ContentPlaceHolderEdit + "tx_es101_RIMEN'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_es101.aspx" + "','" + "員工資料" + "')");
          tx_ri3a_RINUM.Attributes.Add("onblur", "return get_bdlr_cname('tx', '" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_ri3a_RINUM','" + st_ContentPlaceHolderEdit + "tx_bdlr_RINUM'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bdlr.aspx" + "','" + "客戶資料" + "')");
          tx_ri3a_RISAL.Attributes.Add("onblur", "return get_es101_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_ri3a_RISAL','" + st_ContentPlaceHolderEdit + "tx_es101_RISAL'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_es101.aspx" + "','" + "業務資料" + "')");
          tx_ri3a_RICUS.Attributes.Add("onblur", "return get_bcvw_cname_ri('ri3a','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_ri3a_RICUS','" + st_ContentPlaceHolderEdit + "tx_bcvw_RICUS'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bcvw.aspx" + "','" + "會員資料" + "')");
          //
          SetSerMod_A();
        }
      }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
      string errorMessage = "";
      if (clsFN.chkLoginState())
      {
        if (Request[Page.postEventSourceID] == "ContentPlaceHolder1_btnAction")
        {
          string[] arguments = Request[Page.postEventArgumentID].Split('&');
          if (arguments.Length > 0) hh_fun_name.Value = DAC.GetStringValue(arguments[0]);
          if (arguments.Length > 1) hh_fun_mkey.Value = DAC.GetStringValue(arguments[1]);
          //
          if (hh_fun_name.Value.ToLower() == "showa")
          {
            WebTab_form.SelectedIndex = 1;
            ShowOneRow_A(hh_fun_mkey.Value);
            sFN.WebDataGrid_SelectRow(ref WebDataGrid_ri3ba, "ri3a_mkey", hh_fun_mkey.Value);
            SetSerMod_A();
          }
          if (errorMessage != "")
          {
            ClientShowMessage(errorMessage);
          }
        }
        else if (Request[Page.postEventSourceID] == "btnPost")
        {
          string[] arguments = Request[Page.postEventArgumentID].Split('&');
          if (arguments.Length > 0) hh_fun_name.Value = DAC.GetStringValue(arguments[0]);
          if (arguments.Length > 1) hh_fun_mkey.Value = DAC.GetStringValue(arguments[1]);
        }
      }
      else
      {
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    private void Set_DataSource()
    {
      Obj_ri3a.TypeName = "DD2015_45.DAC_ri3a";
      Obj_ri3a.SelectMethod = "SelectTable_ri3a";
      WebDataGrid_ri3a.DataSourceID = "Obj_ri3a";
      //
      Obj_ri3ba.TypeName = "DD2015_45.DAC_ri3a";
      Obj_ri3ba.SelectMethod = "SelectTable_ri3ba";
      WebDataGrid_ri3ba.DataSourceID = "Obj_ri3ba";
      //
      Obj_ri3b.TypeName = "DD2015_45.DAC_ri3b";
      Obj_ri3b.SelectMethod = "SelectTable_ri3b";
      Obj_ri3b.UpdateMethod = "UpdateTable_ri3b";
      Obj_ri3b.InsertMethod = "InsertTable_ri3b";
      Obj_ri3b.DeleteMethod = "DeleteTable_ri3b";
      WebDataGrid_ri3b.DataSourceID = "Obj_ri3b";
    }

    private void get_CmdQueryS()
    {
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmri3a_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = " and 1=0 ";
      }
    }

    private void reset_CmdQueryS_comm()
    {
      CmdQueryS.CommandText = "";
      CmdQueryS.Parameters.Clear();
      if ((tx_ri3a_RIDAT_s1.Text != "") && (tx_ri3a_RIDAT_s1.Text != ""))
      {
        CmdQueryS.CommandText += " and a.RIDAT >=?  and a.RIDAT <=? ";
        DAC.AddParam(CmdQueryS, "ri3a_RIDAT_s1", tx_ri3a_RIDAT_s1.Date);
        DAC.AddParam(CmdQueryS, "ri3a_RIDAT_s2", tx_ri3a_RIDAT_s2.Date);
      }
      else if (tx_ri3a_RIDAT_s1.Text != "")
      {
        CmdQueryS.CommandText += " and a.RIDAT >=?  ";
        DAC.AddParam(CmdQueryS, "ri3a_RIDAT_s1", tx_ri3a_RIDAT_s1.Date);
      }
      else if (tx_ri3a_RIDAT_s2.Text != "")
      {
        CmdQueryS.CommandText += " and a.RIDAT <=?  ";
        DAC.AddParam(CmdQueryS, "ri3a_RIDAT_s2", tx_ri3a_RIDAT_s2.Date);
      }
      //
      if (tx_ri3a_RIREN_s.Text != "")
      {
        CmdQueryS.CommandText += " and a.RIREN like ?  ";
        DAC.AddParam(CmdQueryS, "ri3a_RIREN_s1", tx_ri3a_RIREN_s.Text + "%");
      }
      //
      if (CmdQueryS.CommandText == "")
      {
        CmdQueryS.CommandText += " and 1=1  ";
      }

    }

    private void act_SERS_L()
    {
      try
      {
        if (Session["fmri3a_CmdQueryS"] == null)
        {
          reset_CmdQueryS_comm();
          //
          Session["fmri3a_CmdQueryS"] = CmdQueryS;
          Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Bind_WebDataGrid_A(WebDataGrid_ri3ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
        }
        else
        {
          get_CmdQueryS();
          Session["fmri3a_CmdQueryS"] = CmdQueryS;
          Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Bind_WebDataGrid_A(WebDataGrid_ri3ba, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
        }
      }
      catch
      {
        reset_CmdQueryS_comm();
        //
        Session["fmri3a_CmdQueryS"] = CmdQueryS;
        Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, bl_resetKey); //reset gkey,mkey
        //
        Bind_WebDataGrid_A(WebDataGrid_ri3ba, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      }
      //
    }

    private void Bind_WebDataGrid_A(Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, bool bl_showRow, bool bl_resetkey)
    {
      WebDataGrid.Rows.Clear();
      WebDataGrid.DataBind();
      //
      if (bl_resetkey)
      {
        if (WebDataGrid.Rows.Count > 0)
        {
          hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "ri3a_gkey");
          hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "ri3a_mkey");
          if (bl_showRow)
          {
            ShowOneRow_A(hh_mkey.Value);
          }
          //
        }
        else
        {
          hh_GridGkey.Value = "";
          hh_mkey.Value = "";
          ClearText_A();
        }
      }
    }

    private void Set_Control_A()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      WebDataGrid_ri3a.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_ri3ba.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormControlsText(this.Form, PublicVariable.LangType, ApVer, "UNri3a", "ri3a");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_ri3a, PublicVariable.LangType, ApVer, "UNri3a", "ri3a");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_ri3ba, PublicVariable.LangType, ApVer, "UNri3a", "ri3a");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_ri3b, PublicVariable.LangType, ApVer, "UNri3b", "ri3b");
    }

    protected void act_SERS()
    {
      reset_CmdQueryS_comm();
      //
      Session["fmri3a_CmdQueryS"] = CmdQueryS;
      Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      //
      get_CmdQueryS_ba();
      Bind_WebDataGrid_A(WebDataGrid_ri3ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
    }

    protected void Obj_ri3a_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        get_CmdQueryS();
        e.InputParameters["WhereQuery"] = CmdQueryS;
        e.InputParameters["st_addSelect"] = "";
        e.InputParameters["bl_lock"] = false;
        e.InputParameters["st_addJoin"] = "";
        e.InputParameters["st_addUnion"] = "";
        e.InputParameters["st_orderKey"] = " A.RIDAT DESC,A.RIREN DESC ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }


    protected void bt_08_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      act_SERS();
    }

    private void ClearText_A()
    {
      //
      tx_ri3a_RIREN.Text = "";  //銷貨單號
      tx_ri3a_RINUM.Text = "";  //經銷編號
      tx_bdlr_RINUM.Text = "";  //經銷商名
      tx_ri3a_RIMEN.Text = "";  //開單人員
      tx_es101_RIMEN.Text = "";  //開單人名
      dr_ri3a_RICNS.SelectedIndex = -1;  //分倉編號
      //
      tx_ri3a_RISAL.Text = "";  //業務人員
      tx_es101_RISAL.Text = "";  //業務人名
      tx_ri3a_RICUS.Text = "";  //會員編號
      tx_bcvw_RICUS.Text = "";  //會員名稱
      tx_ri3a_RINAM.Text = "";  //收貨人名
      tx_ri3a_RIGSM.Text = "";  //手機號碼
      tx_ri3a_RITEL.Text = "";  //連絡電話
      tx_ri3a_RIIVV.Text = "";  //統一編號
      tx_ri3a_RIZIP.Text = "";  //郵遞區號
      tx_ri3a_RIADR.Text = "";  //收貨地址
      dr_ri3a_RIRTP.SelectedIndex = -1;  //運送方式
      dr_ri3a_RITME.SelectedIndex = -1;  //收貨時段
      tx_ri3a_RIRMN.Value = 0;  //運費金額
      tx_ri3a_RIDMN.Value = 0;  //貨品金額
      dr_ri3a_RITXP.SelectedIndex = -1;  //計稅方式
      tx_ri3a_RIIVN.Text = "";  //發票號碼
      dr_ri3a_RIIVT.SelectedIndex = -1;  //發票方式
      tx_ri3a_RITXN.Value = "0";  //稅　　額
      tx_ri3a_RITOL.Text = "";  //合計金額
      tx_ri3a_RIRMK.Text = "";  //備註說明
      //
      ck_ri3a_RICK1.Checked = false;  //銷貨類1
    }


    private void SetSerMod_A()
    {
      clsGV.SetTextBoxEditAlert(ref lb_ri3a_RIDAT, ref tx_ri3a_RIDAT, false);  //銷貨日期
      clsGV.SetTextBoxEditAlert(ref lb_ri3a_RINUM, ref tx_ri3a_RINUM, false);  //經銷編號
      clsGV.SetTextBoxEditAlert(ref lb_ri3a_RITOL, ref tx_ri3a_RITOL, false);  //合計金額

      //
      clsGV.TextBox_Set(ref tx_ri3a_RIREN, false);   //銷貨單號
      clsGV.TextBox_Set(ref tx_ri3a_RIDAT, false);   //銷貨日期
      clsGV.TextBox_Set(ref tx_ri3a_RINUM, false);   //經銷編號
      clsGV.TextBox_Set(ref tx_bdlr_RINUM, false);   //經銷商名
      clsGV.TextBox_Set(ref tx_ri3a_RIMEN, false);   //開單人員
      clsGV.TextBox_Set(ref tx_es101_RIMEN, false);   //開單人名
      clsGV.Drpdown_Set(ref dr_ri3a_RICNS, ref tx_ri3a_RICNS, false);   //分倉編號
      clsGV.Drpdown_Set(ref dr_ri3a_RIEDT, ref tx_ri3a_RIEDT, false);   //帳款日期
      clsGV.TextBox_Set(ref tx_ri3a_RISAL, false);   //業務人員

      clsGV.TextBox_Set(ref tx_es101_RISAL, false);   //業務人名
      clsGV.TextBox_Set(ref tx_ri3a_RICUS, false);   //會員編號
      clsGV.TextBox_Set(ref tx_bcvw_RICUS, false);   //會員名稱
      clsGV.TextBox_Set(ref tx_ri3a_RINAM, false);   //收貨人名
      clsGV.TextBox_Set(ref tx_ri3a_RIGSM, false);   //手機號碼
      clsGV.TextBox_Set(ref tx_ri3a_RITEL, false);   //連絡電話
      clsGV.TextBox_Set(ref tx_ri3a_RIIVV, false);   //統一編號
      clsGV.TextBox_Set(ref tx_ri3a_RIZIP, false);   //郵遞區號
      clsGV.TextBox_Set(ref tx_ri3a_RIADR, false);   //收貨地址
      clsGV.Drpdown_Set(ref dr_ri3a_RIRTP, ref tx_ri3a_RIRTP, false);   //運送方式
      clsGV.Drpdown_Set(ref dr_ri3a_RITME, ref tx_ri3a_RITME, false);   //收貨時段
      clsGV.TextBox_Set(ref tx_ri3a_RIRMN, false);   //運費金額
      clsGV.TextBox_Set(ref tx_ri3a_RIDMN, false);   //貨品金額
      clsGV.Drpdown_Set(ref dr_ri3a_RITXP, ref tx_ri3a_RITXP, false);   //計稅方式
      clsGV.TextBox_Set(ref tx_ri3a_RIIVN, false);   //發票號碼
      clsGV.Drpdown_Set(ref dr_ri3a_RIIVT, ref tx_ri3a_RIIVT, false);   //發票方式
      clsGV.TextBox_Set(ref tx_ri3a_RITXN, false);   //稅　　額
      clsGV.TextBox_Set(ref tx_ri3a_RITOL, false);   //合計金額
      clsGV.TextBox_Set(ref tx_ri3a_RIRMK, false);   //備註說明
      ck_ri3a_RICK1.Enabled = false;  //銷貨類1
      //
      clsGV.SetControlShowAlert(ref lb_ri3a_RIDAT, ref tx_ri3a_RIDAT, true);  // 銷貨日期
      clsGV.SetControlShowAlert(ref lb_ri3a_RINUM, ref tx_ri3a_RINUM, true);  // 經銷編號
      clsGV.SetControlShowAlert(ref lb_ri3a_RITOL, ref tx_ri3a_RITOL, true);  // 合計金額
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      PanEdtRightDown.Enabled = true;
      SetSerMod_B();
      //
      WebDataGrid_ri3a.Enabled = true;
      WebDataGrid_ri3ba.Enabled = true;
    }

    private void SetEditMod_A()
    {
      //
      clsGV.TextBox_Set(ref tx_ri3a_RIREN, true);  //銷貨單號
      clsGV.TextBox_Set(ref tx_ri3a_RIDAT, true);  //銷貨日期
      clsGV.TextBox_Set(ref tx_ri3a_RINUM, true);  //經銷編號
      clsGV.TextBox_Set(ref tx_bdlr_RINUM, false);  //經銷商名
      clsGV.TextBox_Set(ref tx_ri3a_RIMEN, true);  //開單人員
      clsGV.TextBox_Set(ref tx_es101_RIMEN, false);  //開單人名
      clsGV.Drpdown_Set(ref dr_ri3a_RICNS, ref tx_ri3a_RICNS, true);   //分倉編號
      clsGV.Drpdown_Set(ref dr_ri3a_RIEDT, ref tx_ri3a_RIEDT, true);   //帳款日期
      clsGV.TextBox_Set(ref tx_ri3a_RISAL, true);  //業務人員
      clsGV.TextBox_Set(ref tx_es101_RISAL, false);  //業務人名
      clsGV.TextBox_Set(ref tx_ri3a_RICUS, true);  //會員編號
      clsGV.TextBox_Set(ref tx_bcvw_RICUS, false);  //會員名稱
      clsGV.TextBox_Set(ref tx_ri3a_RINAM, true);  //收貨人名
      clsGV.TextBox_Set(ref tx_ri3a_RIGSM, true);  //手機號碼
      clsGV.TextBox_Set(ref tx_ri3a_RITEL, true);  //連絡電話
      clsGV.TextBox_Set(ref tx_ri3a_RIIVV, true);  //統一編號
      clsGV.TextBox_Set(ref tx_ri3a_RIZIP, true);  //郵遞區號
      clsGV.TextBox_Set(ref tx_ri3a_RIADR, true);  //收貨地址
      clsGV.Drpdown_Set(ref dr_ri3a_RIRTP, ref tx_ri3a_RIRTP, true);   //運送方式
      clsGV.Drpdown_Set(ref dr_ri3a_RITME, ref tx_ri3a_RITME, true);   //收貨時段
      clsGV.TextBox_Set(ref tx_ri3a_RIRMN, true);  //運費金額
      clsGV.TextBox_Set(ref tx_ri3a_RIDMN, false);  //貨品金額
      clsGV.Drpdown_Set(ref dr_ri3a_RITXP, ref tx_ri3a_RITXP, true);   //計稅方式
      clsGV.TextBox_Set(ref tx_ri3a_RIIVN, true);  //發票號碼
      clsGV.Drpdown_Set(ref dr_ri3a_RIIVT, ref tx_ri3a_RIIVT, true);   //發票方式
      clsGV.TextBox_Set(ref tx_ri3a_RITXN, false);  //稅　　額
      clsGV.TextBox_Set(ref tx_ri3a_RITOL, false);  //合計金額
      clsGV.TextBox_Set(ref tx_ri3a_RIRMK, true);  //備註說明
      ck_ri3a_RICK1.Enabled = true;  //銷貨類1
      //
      clsGV.SetTextBoxEditAlert(ref lb_ri3a_RIDAT, ref tx_ri3a_RIDAT, true);  // 銷貨日期
      clsGV.SetTextBoxEditAlert(ref lb_ri3a_RINUM, ref tx_ri3a_RINUM, true);  // 經銷編號
      clsGV.SetTextBoxEditAlert(ref lb_ri3a_RITOL, ref tx_ri3a_RITOL, true);  // 合計金額
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //bt_05.OnClientClick = " return false;";
      //
      SetSerMod_B();
      PanEdtRightDown.Enabled = false;
      WebDataGrid_ri3a.Enabled = false;
      WebDataGrid_ri3ba.Enabled = false;
    }

    private void ShowOneRow_A(string st_mkey)
    {
      DAC_ri3a ri3aDao = new DAC_ri3a(conn);
      DataTable tb_ri3a = new DataTable();
      OleDbCommand cmd_where = new OleDbCommand();
      string st_ren = "";
      //
      cmd_where.CommandText = " and a.mkey=? ";
      DAC.AddParam(cmd_where, "mkey", st_mkey);
      tb_ri3a = ri3aDao.SelectTableForTextEdit_ri3a(cmd_where);
      if (tb_ri3a.Rows.Count == 1)
      {
        BindText_A(tb_ri3a.Rows[0]);
        //bt_05.OnClientClick = "return btnDEL_c()";
        //bt_04.OnClientClick = "return btnMOD_c()";
        //RI3B
        st_ren = sFN.GetRenFromGkey("ri3a", "RIREN", "mkey", st_mkey);
        get_CmdQueryS_b(st_ren);
        Bind_WebDataGrid_B(WebDataGrid_ri3b, bl_resetKey);
      }
      else
      {
        ClearText_A();
        //bt_05.OnClientClick = "return btnDEL0_c()";
        //bt_04.OnClientClick = "return btnMOD0_c()";
      }
      cmd_where.Dispose();
      tb_ri3a.Dispose();
      ri3aDao.Dispose();
      //
    }

    private void BindText_A(DataRow CurRow)
    {
      //
      tx_ri3a_RIREN.Text = DAC.GetStringValue(CurRow["ri3a_RIREN"]);  //銷貨單號
      if (CurRow["ri3a_RIDAT"] == DBNull.Value) { tx_ri3a_RIDAT.Text = ""; } else { tx_ri3a_RIDAT.Text = DAC.GetDateTimeValue(CurRow["ri3a_RIDAT"]).ToString(sys_DateFormat); }  //銷貨日期
      tx_ri3a_RINUM.Text = DAC.GetStringValue(CurRow["ri3a_RINUM"]);  //經銷編號
      tx_bdlr_RINUM.Text = DAC.GetStringValue(CurRow["bdlr_RINUM"]);  //經銷商名
      tx_ri3a_RIMEN.Text = DAC.GetStringValue(CurRow["ri3a_RIMEN"]);  //開單人員
      tx_es101_RIMEN.Text = DAC.GetStringValue(CurRow["es101_RIMEN"]);  //開單人名
      dr_ri3a_RICNS = sFN.SetDropDownList(ref dr_ri3a_RICNS, DAC.GetStringValue(CurRow["ri3a_RICNS"]));  //分倉編號
      //
      DateTime dt_EDT = DAC.GetDateTimeValue(CurRow["ri3a_RIEDT"]);
      string st_EDT = sFN.strzeroi(dt_EDT.Date.Year, 4) + "-" + sFN.strzeroi(dt_EDT.Date.Month, 2) + "-" + sFN.strzeroi(dt_EDT.Date.Day, 2);
      dr_ri3a_RIEDT = sFN.SetDropDownList(ref dr_ri3a_RIEDT, st_EDT);
      //
      tx_ri3a_RISAL.Text = DAC.GetStringValue(CurRow["ri3a_RISAL"]);  //業務人員
      tx_es101_RISAL.Text = DAC.GetStringValue(CurRow["es101_RISAL"]);  //業務人名
      tx_ri3a_RICUS.Text = DAC.GetStringValue(CurRow["ri3a_RICUS"]);  //會員編號
      tx_bcvw_RICUS.Text = DAC.GetStringValue(CurRow["bcvw_RICUS"]);  //會員名稱
      tx_ri3a_RINAM.Text = DAC.GetStringValue(CurRow["ri3a_RINAM"]);  //收貨人名
      tx_ri3a_RIGSM.Text = DAC.GetStringValue(CurRow["ri3a_RIGSM"]);  //手機號碼
      tx_ri3a_RITEL.Text = DAC.GetStringValue(CurRow["ri3a_RITEL"]);  //連絡電話
      tx_ri3a_RIIVV.Text = DAC.GetStringValue(CurRow["ri3a_RIIVV"]);  //統一編號
      tx_ri3a_RIZIP.Text = DAC.GetStringValue(CurRow["ri3a_RIZIP"]);  //郵遞區號
      tx_ri3a_RIADR.Text = DAC.GetStringValue(CurRow["ri3a_RIADR"]);  //收貨地址
      dr_ri3a_RIRTP = sFN.SetDropDownList(ref dr_ri3a_RIRTP, DAC.GetStringValue(CurRow["ri3a_RIRTP"]));  //運送方式
      dr_ri3a_RITME = sFN.SetDropDownList(ref dr_ri3a_RITME, DAC.GetStringValue(CurRow["ri3a_RITME"]));  //收貨時段
      tx_ri3a_RIRMN.Text = DAC.GetStringValue(CurRow["ri3a_RIRMN"]);  //運費金額
      tx_ri3a_RIDMN.Text = DAC.GetStringValue(CurRow["ri3a_RIDMN"]);  //貨品金額
      dr_ri3a_RITXP = sFN.SetDropDownList(ref dr_ri3a_RITXP, DAC.GetStringValue(CurRow["ri3a_RITXP"]));  //計稅方式
      tx_ri3a_RIIVN.Text = DAC.GetStringValue(CurRow["ri3a_RIIVN"]);  //發票號碼
      dr_ri3a_RIIVT = sFN.SetDropDownList(ref dr_ri3a_RIIVT, DAC.GetStringValue(CurRow["ri3a_RIIVT"]));  //發票方式
      tx_ri3a_RITXN.Text = DAC.GetStringValue(CurRow["ri3a_RITXN"]);  //稅　　額
      tx_ri3a_RITOL.Text = DAC.GetStringValue(CurRow["ri3a_RITOL"]);  //合計金額
      tx_ri3a_RIRMK.Text = DAC.GetStringValue(CurRow["ri3a_RIRMK"]);  //備註說明
      ck_ri3a_RICK1.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["ri3a_RICK1"]));  //銷貨類1
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["ri3a_mkey"]);
      hh_GridGkey.Value = DAC.GetStringValue(CurRow["ri3a_gkey"]);
    }

    #region bt_NEW

    protected void bt_02_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      WebTab_form.SelectedIndex = 1;
      actNEW_A();
    }

    private void actNEW_A()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 2, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "ins_a";
        Set_Control_A();
        ClearText_A();
        //定義guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        SetEditMod_A();
        //
        hh_fun_mkey_old.Value = hh_fun_mkey.Value;
        hh_fun_mkey.Value = "#";
        WebDataGrid_ri3b.Rows.Clear();
        WebDataGrid_ri3b.DataBind();
        // 
        tx_ri3a_RIMEN.Text = UserId;
        tx_es101_RIMEN.Text = UserName;
        //
        try
        {
          if (Session["UNri3a_input_tx_ri3a_RIDAT"] != null)
          {
            tx_ri3a_RIDAT.Date = DAC.GetDateTimeValue(Session["UNri3a_input_tx_ri3a_RIDAT"]);
          }
          else
          {
            tx_ri3a_RIDAT.Date = DateTime.Today;
          }
        }
        catch
        {
          tx_ri3a_RIDAT.Date = DateTime.Today;
        }
        //
        if (tx_ri3a_RIDAT.Date != DateTime.Today)
        {
          lb_ri3a_RIDAT.ForeColor = Color.Red;
        }
        else
        {
          lb_ri3a_RIDAT.ForeColor = default(Color);
        }
        //
        DateTime dt_EDT = sFN.DateToLastDate(tx_ri3a_RIDAT.Date);
        string st_EDT = sFN.strzeroi(dt_EDT.Date.Year, 4) + "-" + sFN.strzeroi(dt_EDT.Date.Month, 2) + "-" + sFN.strzeroi(dt_EDT.Date.Day, 2);
        dr_ri3a_RIEDT = sFN.SetDropDownList(ref dr_ri3a_RIEDT, st_EDT);
        //
        tx_ri3a_RIRMN.Value = 0;
        tx_ri3a_RIDMN.Value = 0;
        tx_ri3a_RITOL.Value = 0;
        tx_ri3a_RITXN.Value = 0;
        tx_ri3a_RIREN.Enabled = false;
        ck_ri3a_RICK1.Checked = true;
        //li_Msg.Text = "<script> document.all('" + st_ContentPlaceHolder + "tx_baur_BCNUM').focus(); </script>";
      }
    }

    #endregion

    #region bt_MOD

    protected void bt_04_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actMOD_A();
    }

    private void actMOD_A()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "mod_a";
        Set_Control_A();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        ShowOneRow_A(hh_mkey.Value);
        SetEditMod_A();
        tx_ri3a_RIREN.Enabled = false;
        dr_ri3a_RICNS.Enabled = false;
      }
    }

    #endregion

    #region bt_CAN

    protected void bt_CAN_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actCAN_A();
    }

    private void actCAN_A()
    {
      ClearText_A();
      hh_GridCtrl.Value = "ser";
      Set_Control_A();
      if ((hh_fun_mkey.Value == "#") && (hh_fun_mkey_old.Value != ""))
      {
        hh_fun_mkey.Value = hh_fun_mkey_old.Value;
        hh_fun_mkey_old.Value = "";
      }
      Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, !bl_resetKey);
      Bind_WebDataGrid_A(WebDataGrid_ri3ba, !bl_showRowA, !bl_resetKey);
      //
      ShowOneRow_A(hh_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_ri3ba, "ri3a_mkey", hh_mkey.Value);
      //
      SetSerMod_A();
    }

    #endregion

    #region bt_DEL

    protected void bt_05_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actDEL_A();
    }

    private void actDEL_A()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 5, UserLoginGkey, ref li_AccMsg))
      {
        bool bl_delok = false;
        Set_Control_A();
        hh_ActKey.Value = DAC.get_guidkey();
        string st_nextkey = sFN.WebDataGrid_NextKey(WebDataGrid_ri3ba, "ri3a_mkey", hh_mkey.Value);
        //
        DAC ri3aDao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "ri3a_gkey='" + hh_GridGkey.Value + "' and ri3a_mkey='" + hh_mkey.Value + "' ";
        OleDbConnection connD = new OleDbConnection();
        connD = DAC.NewReaderConnr();
        connD.Open();
        if (sFN.lookupint32("SELECT COUNT(*) AS CNT FROM RI3B WHERE RBREN='" + tx_ri3a_RIREN.Text + "'", "CNT") == 0)
        {
          DataTable tb_bcvw = new DataTable();
          DbDataAdapter da_ADP = ri3aDao.GetDataAdapter(ApVer, "UNri3a", "ri3a", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "", "SEL DEL ");
          da_ADP.SelectCommand.Connection = connD;
          da_ADP.DeleteCommand.Connection = connD;
          da_ADP.Fill(tb_bcvw);
          DataRow[] DelRow = tb_bcvw.Select(st_SelDataKey);
          if (DelRow.Length == 1)
          {
            OleDbTransaction thistran = connD.BeginTransaction(IsolationLevel.ReadCommitted);
            da_ADP.DeleteCommand.Transaction = thistran;
            try
            {
              ri3aDao.Insertbalog(connD, thistran, "ri3a", hh_ActKey.Value, hh_GridGkey.Value);
              ri3aDao.Insertbtlog(connD, thistran, "ri3a", DAC.GetStringValue(DelRow[0]["ri3a_RIREN"]), "D", UserName, DAC.GetStringValue(DelRow[0]["ri3a_gkey"]));
              DelRow[0].Delete();
              da_ADP.Update(tb_bcvw);
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
              connD.Close();
            }
          }
          else
          {
            bl_delok = false;
            lb_ErrorMessage.Visible = true;
            lb_ErrorMessage.Text = StringTable.GetString("資料已變更,請重新選取!");
          }
          tb_bcvw.Dispose();
          da_ADP.Dispose();
        }
        else
        {
          bl_delok = false;
          lb_ErrorMessage.Visible = true;
          lb_ErrorMessage.Text = StringTable.GetString("請先刪除單據內的商品資料!");
          li_Msg.Text = "<script> alert('請先刪除單據內的商品資料!'); </script>";
        }
        ri3aDao.Dispose();
        //
        if (bl_delok)
        {
          hh_mkey.Value = st_nextkey;
          act_SERS_L();
          SetSerMod_A();
          ShowOneRow_A(hh_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_ri3ba, "rcnta_mkey", hh_mkey.Value);
        }
      }
    }

    #endregion

    #region bt_SAV
    protected void bt_SAV_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      if ((hh_GridCtrl.Value == "ins_a") || (hh_GridCtrl.Value == "mod_a"))
      {
        actSAV_A();
      }
    }

    private void actSAV_A()
    {
      string st_ckerrmsg = "";
      string st_dberrmsg = "";
      string st_tempgkey = "";
      bool bl_insok = false, bl_updateok = false;
      //
      Set_Control_A();
      if (ServerEditCheck_A(ref st_ckerrmsg))
      {
        DAC_ri3a ri3aDao = new DAC_ri3a(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "ri3a_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins_a")
          {
            //自動編號
            st_ren_yymmtext = sFN.strzeroi(tx_ri3a_RIDAT.Date.Year, 4) + sFN.strzeroi(tx_ri3a_RIDAT.Date.Month, 2);
            st_ren_cls = st_ren_yymmtext;
            tx_ri3a_RIREN.Text = ri3aDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            conn.Close();
            //檢查重複
            if (ri3aDao.IsExists("ri3a", "RIREN", tx_ri3a_RIREN.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_ri3a_RIREN.Text + ",已存在.");
              ri3aDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_ri3a_RIREN.Text);
              st_dberrmsg = StringTable.GetString(tx_ri3a_RIREN.Text + ",已重新取號.");
              tx_ri3a_RIREN.Text = ri3aDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_ri3a_RIREN.Text ="";
            }
            else
            {
              conn.Open();
              DataTable tb_ri3a_ins = new DataTable();
              DbDataAdapter da_ADP_ins = ri3aDao.GetDataAdapter(ApVer, "UNri3a", "ri3a", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_ri3a_ins);
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_ri3a_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["ri3a_gkey"] = st_tempgkey;    // 
                ins_row["ri3a_mkey"] = st_tempgkey;    //
                //
                ins_row["ri3a_RIREN"] = tx_ri3a_RIREN.Text.Trim();       // 銷貨單號
                if (tx_ri3a_RIDAT.Text.Trim() == "") { ins_row["ri3a_RIDAT"] = DBNull.Value; } else { ins_row["ri3a_RIDAT"] = tx_ri3a_RIDAT.Date; }       //銷貨日期
                ins_row["ri3a_RINUM"] = tx_ri3a_RINUM.Text.Trim();       // 經銷編號
                ins_row["ri3a_RIMEN"] = tx_ri3a_RIMEN.Text.Trim();       // 開單人員
                ins_row["ri3a_RICNS"] = dr_ri3a_RICNS.SelectedValue;       // 分倉編號
                ins_row["ri3a_RIEDT"] = dr_ri3a_RIEDT.SelectedValue;       // 帳款日期
                ins_row["ri3a_RISAL"] = tx_ri3a_RISAL.Text.Trim();       // 業務人員
                ins_row["ri3a_RICUS"] = tx_ri3a_RICUS.Text.Trim();       // 會員編號
                ins_row["ri3a_RINAM"] = tx_ri3a_RINAM.Text.Trim();       // 收貨人名
                ins_row["ri3a_RIGSM"] = tx_ri3a_RIGSM.Text.Trim();       // 手機號碼
                ins_row["ri3a_RITEL"] = tx_ri3a_RITEL.Text.Trim();       // 連絡電話
                ins_row["ri3a_RIIVV"] = tx_ri3a_RIIVV.Text.Trim();       // 統一編號
                ins_row["ri3a_RIZIP"] = tx_ri3a_RIZIP.Text.Trim();       // 郵遞區號
                ins_row["ri3a_RIADR"] = tx_ri3a_RIADR.Text.Trim();       // 收貨地址
                ins_row["ri3a_RIRTP"] = dr_ri3a_RIRTP.SelectedValue;       // 運送方式
                ins_row["ri3a_RITME"] = dr_ri3a_RITME.SelectedValue;       // 收貨時段
                ins_row["ri3a_RIRMN"] = tx_ri3a_RIRMN.Text.Trim();       // 運費金額
                ins_row["ri3a_RIDMN"] = tx_ri3a_RIDMN.Text.Trim();       // 貨品金額
                ins_row["ri3a_RITXP"] = dr_ri3a_RITXP.SelectedValue;       // 計稅方式
                ins_row["ri3a_RIIVN"] = tx_ri3a_RIIVN.Text.Trim();       // 發票號碼
                ins_row["ri3a_RIIVT"] = dr_ri3a_RIIVT.SelectedValue;       // 發票方式
                ins_row["ri3a_RITXN"] = tx_ri3a_RITXN.Text.Trim();       // 稅　　額
                ins_row["ri3a_RITOL"] = tx_ri3a_RITOL.Text.Trim();       // 合計金額
                ins_row["ri3a_RITPD"] = 0;       // 預付金額
                ins_row["ri3a_RITMN"] = 0;       // 未付餘額
                ins_row["ri3a_RIRMK"] = tx_ri3a_RIRMK.Text.Trim();       // 備註說明
                ins_row["ri3a_RICK1"] = ck_ri3a_RICK1.Checked ? "1" : "0";       // 銷貨類1
                ins_row["ri3a_trusr"] = UserGkey;  //
                tb_ri3a_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_ri3a_ins);
                ri3aDao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_ri3a_RIREN.Text.Trim());
                ri3aDao.Insertbalog(conn, thistran, "ri3a", hh_ActKey.Value, hh_GridGkey.Value);
                ri3aDao.Insertbtlog(conn, thistran, "ri3a", DAC.GetStringValue(ins_row["ri3a_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["ri3a_gkey"]));
                thistran.Commit();
                Session["UNri3a_input_tx_ri3a_RIDAT"] = tx_ri3a_RIDAT.Date;
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
                ri3aDao.Dispose();
                tb_ri3a_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              //
              DAC_ri3b ri3bDao = new DAC_ri3b();
              ri3bDao.UpdateTol_ri3a(tx_ri3a_RIREN.Text.Trim());
              ri3bDao.Dispose();
              //
              Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_ri3ba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_ri3ba, "ri3a_mkey", hh_mkey.Value);
              //
              hh_GridCtrl.Value = "ser";
              SetSerMod_A();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_insok
          }  //ins
          else if (hh_GridCtrl.Value.ToLower() == "mod_a")
          {
            if (ri3aDao.IsExists("ri3a", "RIREN", tx_ri3a_RIREN.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_ri3a_RIREN.Text + ",已存在.");
            }
            else
            {
              DataTable tb_ri3a_mod = new DataTable();
              DbDataAdapter da_ADP_mod = ri3aDao.GetDataAdapter(ApVer, "UNri3a", "ri3a", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_ri3a_mod);
              st_SelDataKey = "ri3a_gkey='" + hh_GridGkey.Value + "' and ri3a_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_ri3a_mod.Select(st_SelDataKey);
              DataRow mod_row;
              if (mod_rows.Length != 1)
              {
                bl_updateok = false;
                st_dberrmsg = StringTable.GetString("資料已變更,請重新選取!");
              }
              else
              {
                conn.Open();
                OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                da_ADP_mod.UpdateCommand.Transaction = thistran;
                try
                {
                  st_tempgkey = DAC.get_guidkey();
                  mod_row = mod_rows[0];
                  mod_row.BeginEdit();
                  if (tx_ri3a_RIDAT.Text.Trim() == "") { mod_row["ri3a_RIDAT"] = DBNull.Value; } else { mod_row["ri3a_RIDAT"] = tx_ri3a_RIDAT.Date; }       //銷貨日期
                  mod_row["ri3a_RINUM"] = tx_ri3a_RINUM.Text.Trim();       // 經銷編號
                  mod_row["ri3a_RIMEN"] = tx_ri3a_RIMEN.Text.Trim();       // 開單人員
                  mod_row["ri3a_RICNS"] = dr_ri3a_RICNS.SelectedValue;       // 分倉編號
                  mod_row["ri3a_RIEDT"] = dr_ri3a_RIEDT.SelectedValue;       // 帳款日期
                  mod_row["ri3a_RISAL"] = tx_ri3a_RISAL.Text.Trim();       // 業務人員
                  mod_row["ri3a_RICUS"] = tx_ri3a_RICUS.Text.Trim();       // 會員編號
                  mod_row["ri3a_RINAM"] = tx_ri3a_RINAM.Text.Trim();       // 收貨人名
                  mod_row["ri3a_RIGSM"] = tx_ri3a_RIGSM.Text.Trim();       // 手機號碼
                  mod_row["ri3a_RITEL"] = tx_ri3a_RITEL.Text.Trim();       // 連絡電話
                  mod_row["ri3a_RIIVV"] = tx_ri3a_RIIVV.Text.Trim();       // 統一編號
                  mod_row["ri3a_RIZIP"] = tx_ri3a_RIZIP.Text.Trim();       // 郵遞區號
                  mod_row["ri3a_RIADR"] = tx_ri3a_RIADR.Text.Trim();       // 收貨地址
                  mod_row["ri3a_RIRTP"] = dr_ri3a_RIRTP.SelectedValue;       // 運送方式
                  mod_row["ri3a_RITME"] = dr_ri3a_RITME.SelectedValue;       // 收貨時段
                  mod_row["ri3a_RIRMN"] = tx_ri3a_RIRMN.Text.Trim();       // 運費金額
                  mod_row["ri3a_RIDMN"] = tx_ri3a_RIDMN.Text.Trim();       // 貨品金額
                  mod_row["ri3a_RITXP"] = dr_ri3a_RITXP.SelectedValue;       // 計稅方式
                  mod_row["ri3a_RIIVN"] = tx_ri3a_RIIVN.Text.Trim();       // 發票號碼
                  mod_row["ri3a_RIIVT"] = dr_ri3a_RIIVT.SelectedValue;       // 發票方式
                  mod_row["ri3a_RITXN"] = tx_ri3a_RITXN.Text.Trim();       // 稅　　額
                  mod_row["ri3a_RITOL"] = tx_ri3a_RITOL.Text.Trim();       // 合計金額
                  mod_row["ri3a_RIRMK"] = tx_ri3a_RIRMK.Text.Trim();       // 備註說明
                  mod_row["ri3a_RICK1"] = ck_ri3a_RICK1.Checked ? "1" : "0";       // 銷貨類1

                  mod_row["ri3a_mkey"] = st_tempgkey;        //
                  mod_row["ri3a_trusr"] = UserGkey;  //

                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_ri3a_mod);
                  ri3aDao.Insertbalog(conn, thistran, "ri3a", hh_ActKey.Value, hh_GridGkey.Value);
                  ri3aDao.Insertbtlog(conn, thistran, "ri3a", DAC.GetStringValue(mod_row["ri3a_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["ri3a_gkey"]));
                  thistran.Commit();
                  bl_updateok = true;
                }
                catch (Exception e)
                {
                  thistran.Rollback();
                  bl_updateok = false;
                  st_dberrmsg = e.Message;
                }
                finally
                {
                  thistran.Dispose();
                  ri3aDao.Dispose();
                  tb_ri3a_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              //update ri3a
              DAC_ri3b ri3bDao = new DAC_ri3b();
              ri3bDao.UpdateTol_ri3a(tx_ri3a_RIREN.Text.Trim());
              ri3bDao.Dispose();
              //
              Bind_WebDataGrid_A(WebDataGrid_ri3a, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_ri3ba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_ri3ba, "ri3a_mkey", hh_mkey.Value);
              //
              hh_GridCtrl.Value = "ser";
              SetSerMod_A();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_updateok
          }   //mod
        }  //ins & mod
        ri3aDao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }

    }
    #endregion

    #region  bt_QUT
    protected void bt_QUT_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Response.Redirect("~/Master/Default/Mainform.aspx");
    }
    #endregion

    #region ServerEditCheck_A
    private bool ServerEditCheck_A(ref string sMsg)
    {
      bool ret;
      ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();
      //ret = DataCheck.cIsStrEmptyChk(ret, tx_ri3a_RIREN.Text, lb_ri3a_RIREN.Text, ref sMsg, PublicVariable.LangType, sFN);  //銷貨單號
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_ri3a_RIDAT.Text, lb_ri3a_RIDAT.Text, ref sMsg, PublicVariable.LangType, sFN); //銷貨日期
      ret = DataCheck.cIsStrEmptyChk(ret, tx_ri3a_RINUM.Text, lb_ri3a_RINUM.Text, ref sMsg, PublicVariable.LangType, sFN);  //經銷編號
      DataCheck.Dispose();
      return ret;
    }
    #endregion

    #region select_ri3ba

    protected void get_CmdQueryS_ba()
    {
      try
      {
        CmdQueryS_ba = (OleDbCommand)Session["fmri3a_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ba.CommandText = " and 1=0 ";
      }
    }

    protected void Obj_ri3ba_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        get_CmdQueryS_ba();
        e.InputParameters["WhereQuery"] = CmdQueryS_ba;
        e.InputParameters["st_addSelect"] = "";
        e.InputParameters["bl_lock"] = false;
        e.InputParameters["st_addJoin"] = "";
        e.InputParameters["st_addUnion"] = "";
        e.InputParameters["st_orderKey"] = " A.RIDAT DESC,A.RIREN DESC ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }

    }

    protected void WebDataGrid_ri3ba_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      SetSerMod_B();
      hh_fun_name.Value = "showa";
      try
      {
        hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("ri3a_mkey").Value);
      }
      catch
      {
      }
      //
      if (hh_fun_mkey.Value != "")
      {
        WebTab_form.SelectedIndex = 1;
        //
        ShowOneRow_A(hh_fun_mkey.Value);
        sFN.WebDataGrid_SelectRow(ref WebDataGrid_ri3ba, "ri3a_mkey", hh_fun_mkey.Value);
        SetSerMod_A();
      }
    }

    #endregion

    #region select_ri3b

    private void get_CmdQueryS_b(string st_ren)
    {
      CmdQueryS_b.CommandText = " and A.RBREN=? ";
      CmdQueryS_b.Parameters.Clear();
      DAC.AddParam(CmdQueryS_b, "RBREN", st_ren);
    }

    private void Bind_WebDataGrid_B(Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, bool bl_resetkey)
    {
      WebDataGrid.Rows.Clear();
      WebDataGrid.DataBind();
      //
      if (bl_resetkey)
      {
        if (WebDataGrid.Rows.Count > 0)
        {
          //hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "ri3a_gkey");
          //hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "ri3a_mkey");
          //SelectOneRow_A(hh_mkey.Value);
        }
        else
        {
          //hh_GridGkey.Value = "";
          //hh_mkey.Value = "";
          //ClearText_A();
        }
      }
    }

    #endregion

    #region WebDataGrid_ri3b
    protected void WebDataGrid_ri3b_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      //string st_key = e.CurrentSelectedRows[0].DataKey[0].ToString();
    }

    protected void WebDataGrid_ri3b_RowAdding(object sender, Infragistics.Web.UI.GridControls.RowAddingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }


    protected void WebDataGrid_ri3b_RowAdded(object sender, Infragistics.Web.UI.GridControls.RowAddedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_ri3b.CustomAJAXResponse.Message = e.Exception.InnerException.Message;
      }
    }

    protected void WebDataGrid_ri3b_RowsDeleting(object sender, Infragistics.Web.UI.GridControls.RowDeletingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }


    protected void WebDataGrid_ri3b_RowUpdating(object sender, Infragistics.Web.UI.GridControls.RowUpdatingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_ri3b_RowUpdated(object sender, Infragistics.Web.UI.GridControls.RowUpdatedEventArgs e)
    {

    }

    #endregion

    #region ri3b_object

    protected void Obj_ri3b_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        //mkey 轉單號
        string st_ren = "";
        st_ren = sFN.GetRenFromGkey("ri3a", "RIREN", "mkey", hh_fun_mkey.Value);
        get_CmdQueryS_b(st_ren);
        e.InputParameters["WhereQuery"] = CmdQueryS_b;
        e.InputParameters["st_addSelect"] = "";
        e.InputParameters["bl_lock"] = false;
        e.InputParameters["st_addJoin"] = "";
        e.InputParameters["st_addUnion"] = "";
        e.InputParameters["st_orderKey"] = " A.RBITM ";
        //
        tx_ri3a_RIRMN.ValueDecimal = sFN.lookupDecimal("select RIRMN FROM RI3A WITH (NOLOCK) WHERE RIREN='" + st_ren + "'", "RIRMN");
        tx_ri3a_RIDMN.ValueDecimal = sFN.lookupDecimal("select RIDMN FROM RI3A WITH (NOLOCK) WHERE RIREN='" + st_ren + "'", "RIDMN");
        tx_ri3a_RITXN.ValueDecimal = sFN.lookupDecimal("select RITXN FROM RI3A WITH (NOLOCK) WHERE RIREN='" + st_ren + "'", "RITXN");
        tx_ri3a_RITOL.ValueDecimal = sFN.lookupDecimal("select RITOL FROM RI3A WITH (NOLOCK) WHERE RIREN='" + st_ren + "'", "RITOL");
        //
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_ri3b_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if (DAC.GetStringValue(e.InputParameters["ri3b_RBPTN"]) != "")
        {
          if (btnUpdateCancel.Value != "1")
          {
            e.InputParameters["ri3b_RBREN"] = tx_ri3a_RIREN.Text;
            e.InputParameters["ri3b_actkey"] = DAC.get_guidkey();
            e.InputParameters["UserGkey"] = UserGkey;
            if (DAC.GetStringValue(e.InputParameters["ri3b_RBITM"]) == "")
            {
              e.InputParameters["ri3b_RBITM"] = 0;
            }
          }
          else
          {
            e.Cancel = true;
          }
        }
        else
        {
          e.Cancel = true;
        }
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_ri3b_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_ri3b.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessageB.Visible = true;
        lb_ErrorMessageB.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_ri3b_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if (btnUpdateCancel.Value != "1")
        {
          e.InputParameters["ri3b_actkey"] = DAC.get_guidkey();
          e.InputParameters["UserGkey"] = UserGkey;
        }
        else
        {
          e.Cancel = true;
        }
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_ri3b_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_ri3b.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessageB.Visible = true;
        lb_ErrorMessageB.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_ri3b_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if (DAC.GetStringValue(e.InputParameters["ri3b_RBPTN"]) != "")
        {
          if (btnUpdateCancel.Value != "1")
          {
            e.InputParameters["ri3b_actkey"] = DAC.get_guidkey();
            e.InputParameters["UserGkey"] = UserGkey;
            if (DAC.GetStringValue(e.InputParameters["ri3b_RBITM"]) == "")
            {
              e.InputParameters["ri3b_RBITM"] = 0;
            }
          }
          else
          {
            e.Cancel = true;
          }
        }
        else
        {
          e.Cancel = true;
        }
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_ri3b_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_ri3b.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessageB.Visible = true;
        lb_ErrorMessageB.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    #endregion

    #region Obj_ri3b_RBUNI

    protected void Obj_ri3b_RBUNI_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        Cmd_ri3b_RBUNI.Parameters.Clear();
        Cmd_ri3b_RBUNI.CommandText = " and 1=1 ";
        e.InputParameters["WhereQuery"] = Cmd_ri3b_RBUNI;
        e.InputParameters["st_orderKey"] = " A.BKNUM ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    #endregion

    #region bt_MOD_B

    protected void bt_MOD_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      if (bt_SAVE_B.Visible)
      {
        Bind_WebDataGrid_B(WebDataGrid_ri3b, bl_resetKey);
        SetSerMod_B();
      }
      else
      {
        SetEditMod_B();
      }
    }

    private void SetSerMod_B()
    {
      sFN.WebDataGrid_SetEdit(ref WebDataGrid_ri3b, false);
      WebDataGrid_ri3b.Columns["ri3b_RBUNI"].Hidden = true;
      WebDataGrid_ri3b.Columns["ri3b_RBUNIN"].Hidden = false;
      //
      bt_MOD_B.Text = "W更正";
      bt_MOD_B.AccessKey = "W";
      bt_MOD_B = sFN.SetWebImageButtonDetail(bt_MOD_B);
      if (sFN.chkAccessFuncButton(UserGkey, st_object_func, 2) || sFN.chkAccessFuncButton(UserGkey, st_object_func, 4) || sFN.chkAccessFuncButton(UserGkey, st_object_func, 5)) // 新增||更正||刪除
      {
        bt_MOD_B.Visible = true;
      }
      else
      {
        bt_MOD_B.Visible = false;
      }
      //
      bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
      bt_New_B.Visible = false;
      //
      bt_SAVE_B = sFN.SetWebImageButtonDetail(bt_SAVE_B);
      bt_SAVE_B.Visible = false;
      bt_New_B.ClientSideEvents.Click = "";
      //
      bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
      bt_New_B.Visible = false;
      bt_New_B.ClientSideEvents.Click = "";
      //
      bt_Cancel_B = sFN.SetWebImageButtonDetail(bt_Cancel_B);
      bt_Cancel_B.Visible = false;
      //
      WebDataGrid_ri3a.Enabled = true;
      WebDataGrid_ri3ba.Enabled = true;
      PanBtns.Enabled = true;
    }
    private void SetEditMod_B()
    {
      if (sFN.chkAccessFuncButton(UserGkey, st_object_func, 2) || sFN.chkAccessFuncButton(UserGkey, st_object_func, 4) || sFN.chkAccessFuncButton(UserGkey, st_object_func, 5)) // 新增||更正||刪除
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_ri3b, true);
        WebDataGrid_ri3b.Columns["ri3b_RBUNI"].Hidden = false;
        WebDataGrid_ri3b.Columns["ri3b_RBUNIN"].Hidden = true;

        //
        bt_MOD_B.Text = "E完成";
        bt_MOD_B.AccessKey = "E";
        bt_MOD_B = sFN.SetWebImageButtonDetail(bt_MOD_B);
        bt_MOD_B.Visible = true;
        //
        bt_SAVE_B = sFN.SetWebImageButtonDetail(bt_SAVE_B);
        bt_SAVE_B.Visible = true;
        //
        if (sFN.chkAccessFuncButton(UserGkey, st_object_func, 2)) // 新增 
        {
          bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
          bt_New_B.Visible = true;
          bt_New_B.ClientSideEvents.Click = "webDataGrid_AddRowB()";
        }
        //
        if (sFN.chkAccessFuncButton(UserGkey, st_object_func, 5)) // 刪除 
        {
          WebDataGrid_ri3b.Columns.FromKey("EDIT").Hidden = false;
        }      
        else
        {
          WebDataGrid_ri3b.Columns.FromKey("EDIT").Hidden = true;
        }
        //
        WebDataGrid_ri3a.Enabled = false;
        WebDataGrid_ri3ba.Enabled = false;
        PanBtns.Enabled = false;
        //
        bt_Cancel_B = sFN.SetWebImageButtonDetail(bt_Cancel_B);
        bt_Cancel_B.Visible = true;
      }
    }

    protected void bt_Cancel_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Bind_WebDataGrid_B(WebDataGrid_ri3b, bl_resetKey);
    }

    protected void bt_SAVE_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Bind_WebDataGrid_B(WebDataGrid_ri3b, bl_resetKey);
    }

    #endregion

  }
}