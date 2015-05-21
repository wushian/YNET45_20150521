﻿using System;
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

namespace DD2015_45.Forms.bas
{
  public partial class fm_bdsh : FormBase
  {

    string st_object_func = "UNbdsh";
    //string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$";  //編輯頁面
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";  //編輯頁面
    string st_ContentPlaceHolderEdit = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$WebTabEdtRightTop$tmpl0$";
    bool bl_resetKey = true;
    bool bl_showRowA = true;
    //
    //string st_dd_apx = "UNbdsh";         //UNdcnews   與apx 相關
    //string st_dd_table = "bdlr";         //dcnews     與table 相關 
    //string st_ren_head = "";            //DC         與單號相關 
    //string st_ren_yymmtext = "";     //"         與單號相關 
    //string st_ren_cls = "bdsh";        //ren        與單號cls相關 
    //string st_ren_cos = "1";        //1          與單號cos相關 
    //int in_ren_len = 4;            //6          與單號流水號 
    //
    private OleDbCommand CmdQueryS_ba = new OleDbCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      //檢查權限&狀態
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          //
          dr_bdlr_BDCNX = sFN.DropDownListFromTable(ref dr_bdlr_BDCNX, "PDBDCNX", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDMLB = sFN.DropDownListFromTable(ref dr_bdlr_BDMLB, "PDBPMLB", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDLEV = sFN.DropDownListFromTable(ref dr_bdlr_BDLEV, "PDBDLEV", "BKNUM", "BKNUM", "", "BKNUM");
          dr_bdlr_BDLC1 = sFN.DropDownListFromTable(ref dr_bdlr_BDLC1, "PDBDLC1", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDLC2 = sFN.DropDownListFromTable(ref dr_bdlr_BDLC2, "PDBDLC2", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDLC3 = sFN.DropDownListFromTable(ref dr_bdlr_BDLC3, "PDBDLC3", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDLC4 = sFN.DropDownListFromTable(ref dr_bdlr_BDLC4, "PDBDLC4", "BKNUM", "BKNAM", "", "BKNUM");

          //
          WebTab_form.SelectedIndex = 0;
          Set_Control_A();
          //
          tx_bdlr_BDNUM_s1.Text = "";
          tx_bdlr_BDNAM_s1.Text = "";
          //
          if (Session["fmbdsh_CmdQueryS"] == null)
          {
            act_SERS_L();
          }
          else
          {
            get_CmdQueryS();
            Obj_bdsh.TypeName = "DD2015_45.DAC_bdsh";
            Obj_bdsh.SelectMethod = "SelectTable_bdsh";
            WebDataGrid_bdsh.DataSourceID = "Obj_bdsh";
            Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
            get_CmdQueryS_ba();
            Obj_bdshba.TypeName = "DD2015_45.DAC_bdsh";
            Obj_bdshba.SelectMethod = "SelectTable_bdshba";
            WebDataGrid_bdshba.DataSourceID = "Obj_bdshba";
            Bind_WebDataGrid_A(WebDataGrid_bdshba, bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
          }
          SetSerMod_A();
        }
      }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        string errorMessage = "";
        if (Request[Page.postEventSourceID] == "btnAction")
        {
          string[] arguments = Request[Page.postEventArgumentID].Split('&');
          if (arguments.Length > 0) hh_fun_name.Value = DAC.GetStringValue(arguments[0]);
          if (arguments.Length > 1) hh_fun_mkey.Value = DAC.GetStringValue(arguments[1]);
          //
          if (hh_fun_name.Value.ToLower() == "showa")
          {
            //Bind_WebDataGrid_A(WebDataGrid_bdshba,bl_showRowA,!bl_resetKey);
            ShowOneRow_A(hh_fun_mkey.Value);
            sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdshba, "bdlr_mkey", hh_fun_mkey.Value);
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

    private void get_CmdQueryS()
    {
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmbdsh_CmdQueryS"];
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
      //
      if (tx_bdlr_BDNUM_s1.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDNUM like ?  ";
        DAC.AddParam(CmdQueryS, "bdlr_BDNUM_s1", tx_bdlr_BDNUM_s1.Text + "%");
      }
      //
      if (tx_bdlr_BDNAM_s1.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDNAM like ?  ";
        DAC.AddParam(CmdQueryS, "bdlr_BDNAM_s1", tx_bdlr_BDNAM_s1.Text + "%");
      }
      //
      if (CmdQueryS.CommandText == "")
      {
        CmdQueryS.CommandText += " and 1=1  ";
      }
      CmdQueryS.CommandText += " and a.BDTYP='P'  ";
    }

    private void act_SERS_L()
    {
      try
      {
        if (Session["fmbdsh_CmdQueryS"] == null)
        {
          reset_CmdQueryS_comm();
          //
          Session["fmbdsh_CmdQueryS"] = CmdQueryS;
          Obj_bdsh.TypeName = "DD2015_45.DAC_bdsh";
          Obj_bdsh.SelectMethod = "SelectTable_bdsh";
          WebDataGrid_bdsh.DataSourceID = "Obj_bdsh";
          Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_bdshba.TypeName = "DD2015_45.DAC_bdsh";
          Obj_bdshba.SelectMethod = "SelectTable_bdshba";
          WebDataGrid_bdshba.DataSourceID = "Obj_bdshba";
          Bind_WebDataGrid_A(WebDataGrid_bdshba, bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
        }
        else
        {
          get_CmdQueryS();
          Session["fmbdsh_CmdQueryS"] = CmdQueryS;
          Obj_bdsh.TypeName = "DD2015_45.DAC_bdsh";
          Obj_bdsh.SelectMethod = "SelectTable_bdsh";
          WebDataGrid_bdsh.DataSourceID = "Obj_bdsh";
          Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_bdshba.TypeName = "DD2015_45.DAC_bdsh";
          Obj_bdshba.SelectMethod = "SelectTable_bdshba";
          WebDataGrid_bdshba.DataSourceID = "Obj_bdshba";
          Bind_WebDataGrid_A(WebDataGrid_bdshba, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
        }
      }
      catch
      {
        reset_CmdQueryS_comm();
        //
        Session["fmbdsh_CmdQueryS"] = CmdQueryS;
        Obj_bdsh.TypeName = "DD2015_45.DAC_bdsh";
        Obj_bdsh.SelectMethod = "SelectTable_bdsh";
        WebDataGrid_bdsh.DataSourceID = "Obj_bdsh";
        Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, bl_resetKey); //reset gkey,mkey
        //
        Obj_bdshba.TypeName = "DD2015_45.DAC_bdsh";
        Obj_bdshba.SelectMethod = "SelectTable_bdshba";
        WebDataGrid_bdshba.DataSourceID = "Obj_bdshba";
        Bind_WebDataGrid_A(WebDataGrid_bdshba, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      }
      //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="WebDataGrid"></param>
    private void Bind_WebDataGrid_A(Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, bool bl_showOneRowText, bool bl_resetkey)
    {
      WebDataGrid.Rows.Clear();
      WebDataGrid.DataBind();
      //
      if (bl_resetkey)
      {
        if (WebDataGrid.Rows.Count > 0)
        {
          hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "bdlr_gkey");
          hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "bdlr_mkey");
          if (bl_showOneRowText)
          {
            ShowOneRow_A(hh_mkey.Value);
          }
          //
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
      WebDataGrid_bdsh.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_bdshba.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      //sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNbdsh", "bdlr");
      sFN.SetFormControlsText(this, PublicVariable.LangType, ApVer, "UNbdsh", "bdlr");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_bdsh, PublicVariable.LangType, ApVer, "UNbdsh", "bdlr");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_bdshba, PublicVariable.LangType, ApVer, "UNbdsh", "bdlr");
    }

    protected void act_SERS()
    {
      reset_CmdQueryS_comm();
      //
      Session["fmbdsh_CmdQueryS"] = CmdQueryS;
      Obj_bdsh.TypeName = "DD2015_45.DAC_bdsh";
      Obj_bdsh.SelectMethod = "SelectTable_bdsh";
      WebDataGrid_bdsh.DataSourceID = "Obj_bdsh";
      Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      //
      get_CmdQueryS_ba();
      Obj_bdshba.TypeName = "DD2015_45.DAC_bdsh";
      Obj_bdshba.SelectMethod = "SelectTable_bdshba";
      WebDataGrid_bdshba.DataSourceID = "Obj_bdshba";
      Bind_WebDataGrid_A(WebDataGrid_bdshba, bl_showRowA, bl_resetKey); //reset gkey,mkey
    }

    protected void Obj_bdsh_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
        e.InputParameters["st_orderKey"] = " A.BDNUM ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_bdsh_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      hh_fun_name.Value = "showa";
      hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("bdlr_mkey").Value);
      WebTab_form.SelectedIndex = 1;
      //
      ShowOneRow_A(hh_fun_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdshba, "bdlr_mkey", hh_fun_mkey.Value);
      SetSerMod_A();
    }

    protected void WebDataGrid_bdshba_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      hh_fun_name.Value = "showa";
      hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("bdlr_mkey").Value);
      WebTab_form.SelectedIndex = 1;
      //
      ShowOneRow_A(hh_fun_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdshba, "bdlr_mkey", hh_fun_mkey.Value);
      SetSerMod_A();
    }

    protected void bt_08_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      act_SERS();
    }

    protected void bt_QUT_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Response.Redirect("~/Master/Default/Mainform.aspx");
    }
    ///
    /// 
    /// 
    /// 
    /// 
    private void ClearText_A()
    {
      tx_bdlr_BDNUM.Text = "";  //編　　號
      tx_bdlr_BDCJ5.Text = "";  //拼音代碼
      dr_bdlr_BDCNX.SelectedIndex = -1;  //分倉類別
      ck_bdlr_BDISP.Checked = false;  //分倉資料
      ck_bdlr_BDISD.Checked = false;  //百貨專櫃
      ck_bdlr_BDISH.Checked = false;  //直營門市
      ck_bdlr_BDISM.Checked = false;  //辦事處
      tx_bdlr_BDNAM.Text = "";  //名　　稱
      tx_bdlr_BDSHT.Text = "";  //簡　　稱
      dr_bdlr_BDMLB.SelectedIndex = -1;  //管理品牌
      tx_bdlr_BDNME.Text = "";  //英文名稱
      dr_bdlr_BDLEV.SelectedIndex = -1;  //等　　級
      dr_bdlr_BDLC1.SelectedIndex = -1;  //區　　域
      dr_bdlr_BDLC2.SelectedIndex = -1;  //辦 事 處
      dr_bdlr_BDLC3.SelectedIndex = -1;  //業務區域
      dr_bdlr_BDLC4.SelectedIndex = -1;  //商圈區域
      tx_bdlr_BDMN2.Text = "";  //連絡人　
      tx_bdlr_BDA11.Text = "";  //連絡地址
      tx_bdlr_BDA12.Text = "";  //連絡地址
      tx_bdlr_BDA13.Text = "";  //連絡地址
      tx_bdlr_BDMT2.Text = "";  //連絡人行動
      tx_bdlr_BDTEL.Text = "";  //連絡電話
      tx_bdlr_BDFAX.Text = "";  //傳真號碼
      tx_bdlr_BDDPT.Text = "";  //專櫃編號
      tx_bdlr_BDEND.Text = "";  //停止日期
      ck_bdlr_BDACC.Checked = false;  //轉換傳票
      tx_bdlr_BDRMK.Text = "";  //備　　註
      tx_bdlr_BDINV.Text = "";  //統一編號
      tx_bdlr_BDINA.Text = "";  //發票抬頭
      tx_bdlr_BDB11.Text = "";  //發票地址
      tx_bdlr_BDB12.Text = "";  //發票地址
      tx_bdlr_BDB13.Text = "";  //發票地址
      tx_bdlr_BDTXN.Text = "";  //稅籍編號
      tx_bdlr_BDSAL.Text = "";  //負責店長
      tx_es101_BDSAL.Text = "";  //負責店長
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetSerMod_A()
    {
      //
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDNUM, ref tx_bdlr_BDNUM, false);  //編　　號
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDNAM, ref tx_bdlr_BDNAM, false);  //名　　稱
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDTEL, ref tx_bdlr_BDTEL, false);  //連絡電話
      //
      clsGV.TextBox_Set(ref tx_bdlr_BDNUM, false);   //編　　號
      clsGV.TextBox_Set(ref tx_bdlr_BDCJ5, false);   //拼音代碼
      clsGV.Drpdown_Set(ref dr_bdlr_BDCNX, ref tx_bdlr_BDCNX, false);   //分倉類別
      ck_bdlr_BDISP.Enabled = false;  //分倉資料
      ck_bdlr_BDISD.Enabled = false;  //百貨專櫃
      ck_bdlr_BDISH.Enabled = false;  //直營門市
      ck_bdlr_BDISM.Enabled = false;  //辦事處
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, false);   //名　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDSHT, false);   //簡　　稱
      clsGV.Drpdown_Set(ref dr_bdlr_BDMLB, ref tx_bdlr_BDMLB, false);   //管理品牌
      clsGV.TextBox_Set(ref tx_bdlr_BDNME, false);   //英文名稱
      clsGV.Drpdown_Set(ref dr_bdlr_BDLEV, ref tx_bdlr_BDLEV, false);   //等　　級
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC1, ref tx_bdlr_BDLC1, false);   //區　　域
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC2, ref tx_bdlr_BDLC2, false);   //辦 事 處
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC3, ref tx_bdlr_BDLC3, false);   //業務區域
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC4, ref tx_bdlr_BDLC4, false);   //商圈區域
      clsGV.TextBox_Set(ref tx_bdlr_BDMN2, false);   //連絡人　
      clsGV.TextBox_Set(ref tx_bdlr_BDA11, false);   //連絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA12, false);   //連絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA13, false);   //連絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDMT2, false);   //連絡人行動
      clsGV.TextBox_Set(ref tx_bdlr_BDTEL, false);   //連絡電話
      clsGV.TextBox_Set(ref tx_bdlr_BDFAX, false);   //傳真號碼
      clsGV.TextBox_Set(ref tx_bdlr_BDDPT, false);   //專櫃編號
      clsGV.TextBox_Set(ref tx_bdlr_BDEND, false);   //停止日期
      ck_bdlr_BDACC.Enabled = false;  //轉換傳票
      clsGV.TextBox_Set(ref tx_bdlr_BDRMK, false);   //備　　註
      clsGV.TextBox_Set(ref tx_bdlr_BDINV, false);   //統一編號
      clsGV.TextBox_Set(ref tx_bdlr_BDINA, false);   //發票抬頭
      clsGV.TextBox_Set(ref tx_bdlr_BDB11, false);   //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB12, false);   //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB13, false);   //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDTXN, false);   //稅籍編號
      clsGV.TextBox_Set(ref tx_bdlr_BDSAL, false);   //負責店長
      clsGV.TextBox_Set(ref tx_es101_BDSAL, false);   //負責店長
      //
      clsGV.SetControlShowAlert(ref lb_bdlr_BDNUM, ref tx_bdlr_BDNUM, true);  // 編　　號
      clsGV.SetControlShowAlert(ref lb_bdlr_BDNAM, ref tx_bdlr_BDNAM, true);  // 名　　稱
      clsGV.SetControlShowAlert(ref lb_bdlr_BDTEL, ref tx_bdlr_BDTEL, true);  // 連絡電話
      //
      tx_bdlr_BDSAL.Attributes.Remove("onblur");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      WebDataGrid_bdsh.Enabled = true;
      WebDataGrid_bdshba.Enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetEditMod_A()
    {
      //
      clsGV.TextBox_Set(ref tx_bdlr_BDNUM, true);  //編　　號
      clsGV.TextBox_Set(ref tx_bdlr_BDCJ5, true);  //拼音代碼
      clsGV.Drpdown_Set(ref dr_bdlr_BDCNX, ref tx_bdlr_BDCNX, true);   //分倉類別
      ck_bdlr_BDISP.Enabled = true;  //分倉資料
      ck_bdlr_BDISD.Enabled = true;  //百貨專櫃
      ck_bdlr_BDISH.Enabled = true;  //直營門市
      ck_bdlr_BDISM.Enabled = true;  //辦事處
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, true);  //名　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDSHT, true);  //簡　　稱
      clsGV.Drpdown_Set(ref dr_bdlr_BDMLB, ref tx_bdlr_BDMLB, true);   //管理品牌
      clsGV.TextBox_Set(ref tx_bdlr_BDNME, true);  //英文名稱
      clsGV.Drpdown_Set(ref dr_bdlr_BDLEV, ref tx_bdlr_BDLEV, true);   //等　　級
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC1, ref tx_bdlr_BDLC1, true);   //區　　域
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC2, ref tx_bdlr_BDLC2, true);   //辦 事 處
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC3, ref tx_bdlr_BDLC3, true);   //業務區域
      clsGV.Drpdown_Set(ref dr_bdlr_BDLC4, ref tx_bdlr_BDLC4, true);   //商圈區域
      clsGV.TextBox_Set(ref tx_bdlr_BDMN2, true);  //連絡人　
      clsGV.TextBox_Set(ref tx_bdlr_BDA11, true);  //連絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA12, true);  //連絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA13, true);  //連絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDMT2, true);  //連絡人行動
      clsGV.TextBox_Set(ref tx_bdlr_BDTEL, true);  //連絡電話
      clsGV.TextBox_Set(ref tx_bdlr_BDFAX, true);  //傳真號碼
      clsGV.TextBox_Set(ref tx_bdlr_BDDPT, true);  //專櫃編號
      clsGV.TextBox_Set(ref tx_bdlr_BDEND, true);  //停止日期
      ck_bdlr_BDACC.Enabled = true;  //轉換傳票
      clsGV.TextBox_Set(ref tx_bdlr_BDRMK, true);  //備　　註
      clsGV.TextBox_Set(ref tx_bdlr_BDINV, true);  //統一編號
      clsGV.TextBox_Set(ref tx_bdlr_BDINA, true);  //發票抬頭
      clsGV.TextBox_Set(ref tx_bdlr_BDB11, true);  //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB12, true);  //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB13, true);  //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDTXN, true);  //稅籍編號
      clsGV.TextBox_Set(ref tx_bdlr_BDSAL, true);  //負責店長
      clsGV.TextBox_Set(ref tx_es101_BDSAL, true);  //負責店長
      // 
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDNUM, ref tx_bdlr_BDNUM, true);  // 編　　號
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDNAM, ref tx_bdlr_BDNAM, true);  // 名　　稱
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDTEL, ref tx_bdlr_BDTEL, true);  // 連絡電話
      //
      tx_bdlr_BDSAL.Attributes.Add("onblur", "return get_es101_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_bdlr_BDSAL','" + st_ContentPlaceHolderEdit + "tx_es010_BDSAL'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_es101.aspx" + "','" + "業務資料" + "')");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //bt_05.OnClientClick = " return false;";
      //
      WebDataGrid_bdsh.Enabled = false;
      WebDataGrid_bdshba.Enabled = false;
    }

    private void ShowOneRow_A(string st_mkey)
    {
      DAC_bdsh bdshDao = new DAC_bdsh(conn);
      DataTable tb_bdsh = new DataTable();
      OleDbCommand cmd_where = new OleDbCommand();
      //
      cmd_where.CommandText = " and a.mkey=? ";
      DAC.AddParam(cmd_where, "mkey", st_mkey);
      tb_bdsh = bdshDao.SelectTableForTextEdit_bdsh(cmd_where);
      if (tb_bdsh.Rows.Count == 1)
      {
        BindText_A(tb_bdsh.Rows[0]);
        //bt_05.OnClientClick = "return btnDEL_c()";
        //bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        ClearText_A();
        //bt_05.OnClientClick = "return btnDEL0_c()";
        //bt_04.OnClientClick = "return btnMOD0_c()";
      }
      cmd_where.Dispose();
      tb_bdsh.Dispose();
      bdshDao.Dispose();
      //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CurRow"></param>
    private void BindText_A(DataRow CurRow)
    {
      //
      tx_bdlr_BDNUM.Text = DAC.GetStringValue(CurRow["bdlr_BDNUM"]);  //編　　號
      tx_bdlr_BDCJ5.Text = DAC.GetStringValue(CurRow["bdlr_BDCJ5"]);  //拼音代碼
      dr_bdlr_BDCNX = sFN.SetDropDownList(ref dr_bdlr_BDCNX, DAC.GetStringValue(CurRow["bdlr_BDCNX"]));  //分倉類別
      ck_bdlr_BDISP.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISP"]));  //分倉資料
      ck_bdlr_BDISD.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISD"]));  //百貨專櫃
      ck_bdlr_BDISH.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISH"]));  //直營門市
      ck_bdlr_BDISM.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISM"]));  //辦事處
      tx_bdlr_BDNAM.Text = DAC.GetStringValue(CurRow["bdlr_BDNAM"]);  //名　　稱
      tx_bdlr_BDSHT.Text = DAC.GetStringValue(CurRow["bdlr_BDSHT"]);  //簡　　稱
      dr_bdlr_BDMLB = sFN.SetDropDownList(ref dr_bdlr_BDMLB, DAC.GetStringValue(CurRow["bdlr_BDMLB"]));  //管理品牌
      tx_bdlr_BDNME.Text = DAC.GetStringValue(CurRow["bdlr_BDNME"]);  //英文名稱
      dr_bdlr_BDLEV = sFN.SetDropDownList(ref dr_bdlr_BDLEV, DAC.GetStringValue(CurRow["bdlr_BDLEV"]));  //等　　級
      dr_bdlr_BDLC1 = sFN.SetDropDownList(ref dr_bdlr_BDLC1, DAC.GetStringValue(CurRow["bdlr_BDLC1"]));  //區　　域
      dr_bdlr_BDLC2 = sFN.SetDropDownList(ref dr_bdlr_BDLC2, DAC.GetStringValue(CurRow["bdlr_BDLC2"]));  //辦 事 處
      dr_bdlr_BDLC3 = sFN.SetDropDownList(ref dr_bdlr_BDLC3, DAC.GetStringValue(CurRow["bdlr_BDLC3"]));  //業務區域
      dr_bdlr_BDLC4 = sFN.SetDropDownList(ref dr_bdlr_BDLC4, DAC.GetStringValue(CurRow["bdlr_BDLC4"]));  //商圈區域
      tx_bdlr_BDMN2.Text = DAC.GetStringValue(CurRow["bdlr_BDMN2"]);  //連絡人　
      tx_bdlr_BDA11.Text = DAC.GetStringValue(CurRow["bdlr_BDA11"]);  //連絡地址
      tx_bdlr_BDA12.Text = DAC.GetStringValue(CurRow["bdlr_BDA12"]);  //連絡地址
      tx_bdlr_BDA13.Text = DAC.GetStringValue(CurRow["bdlr_BDA13"]);  //連絡地址
      tx_bdlr_BDMT2.Text = DAC.GetStringValue(CurRow["bdlr_BDMT2"]);  //連絡人行動
      tx_bdlr_BDTEL.Text = DAC.GetStringValue(CurRow["bdlr_BDTEL"]);  //連絡電話
      tx_bdlr_BDFAX.Text = DAC.GetStringValue(CurRow["bdlr_BDFAX"]);  //傳真號碼
      tx_bdlr_BDDPT.Text = DAC.GetStringValue(CurRow["bdlr_BDDPT"]);  //專櫃編號
      if (CurRow["bdlr_BDEND"] == DBNull.Value) { tx_bdlr_BDEND.Text = ""; } else { tx_bdlr_BDEND.Text = DAC.GetDateTimeValue(CurRow["bdlr_BDEND"]).ToString(sys_DateFormat); }  //停止日期
      ck_bdlr_BDACC.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDACC"]));  //轉換傳票
      tx_bdlr_BDRMK.Text = DAC.GetStringValue(CurRow["bdlr_BDRMK"]);  //備　　註
      tx_bdlr_BDINV.Text = DAC.GetStringValue(CurRow["bdlr_BDINV"]);  //統一編號
      tx_bdlr_BDINA.Text = DAC.GetStringValue(CurRow["bdlr_BDINA"]);  //發票抬頭
      tx_bdlr_BDB11.Text = DAC.GetStringValue(CurRow["bdlr_BDB11"]);  //發票地址
      tx_bdlr_BDB12.Text = DAC.GetStringValue(CurRow["bdlr_BDB12"]);  //發票地址
      tx_bdlr_BDB13.Text = DAC.GetStringValue(CurRow["bdlr_BDB13"]);  //發票地址
      tx_bdlr_BDTXN.Text = DAC.GetStringValue(CurRow["bdlr_BDTXN"]);  //稅籍編號
      tx_bdlr_BDSAL.Text = DAC.GetStringValue(CurRow["bdlr_BDSAL"]);  //負責店長
      tx_es101_BDSAL.Text = DAC.GetStringValue(CurRow["es101_BDSAL"]);  //負責店長
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["bdlr_mkey"]);
      hh_GridGkey.Value = DAC.GetStringValue(CurRow["bdlr_gkey"]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_02_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      WebTab_form.SelectedIndex = 1;
      WebTabEdtRightTop.SelectedIndex = 0;
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
        //li_Msg.Text = "<script> document.all('" + st_ContentPlaceHolder + "tx_baur_BCNUM').focus(); </script>";
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        tx_bdlr_BDNUM.Enabled = false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bt_CAN_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actCAN_A();
    }

    private void actCAN_A()
    {
      ClearText_A();
      hh_GridCtrl.Value = "ser";
      Set_Control_A();
      Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, !bl_resetKey);
      Bind_WebDataGrid_A(WebDataGrid_bdshba, !bl_showRowA, !bl_resetKey);
      //
      ShowOneRow_A(hh_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdshba, "bdlr_mkey", hh_mkey.Value);
      //
      SetSerMod_A();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        string st_nextkey = sFN.WebDataGrid_NextKey(WebDataGrid_bdshba, "bdlr_mkey", hh_mkey.Value);
        //
        DAC bdshDao = new DAC_bdsh(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "' and bdlr_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_bdsh = new DataTable();
        OleDbConnection connD = new OleDbConnection();
        connD = DAC.NewReaderConnr();
        connD.Open();
        DbDataAdapter da_ADP = bdshDao.GetDataAdapter(ApVer, "UNbdsh", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "", "SEL DEL ");
        da_ADP.SelectCommand.Connection = connD;
        da_ADP.DeleteCommand.Connection = connD;
        da_ADP.Fill(tb_bdsh);
        DataRow[] DelRow = tb_bdsh.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          OleDbTransaction thistran = connD.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            bdshDao.Insertbalog(connD, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
            bdshDao.Insertbtlog(connD, thistran, "bdlr", DAC.GetStringValue(DelRow[0]["bdlr_BDNUM"]), "D", UserName, DAC.GetStringValue(DelRow[0]["bdlr_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_bdsh);
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
        bdshDao.Dispose();
        tb_bdsh.Dispose();
        da_ADP.Dispose();
        //
        if (bl_delok)
        {
          hh_mkey.Value = st_nextkey;
          act_SERS_L();
          SetSerMod_A();
          ShowOneRow_A(hh_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdshba, "bdlr_mkey", hh_mkey.Value);
        }
      }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        DAC_bdsh bdshDao = new DAC_bdsh(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins_a")
          {
            //自動編號
            //st_ren_yymmtext = sFN.strzeroi(tx_ri3a_RIDAT.Date.Year, 4) + sFN.strzeroi(tx_ri3a_RIDAT.Date.Month, 2);
            //st_ren_cls = st_ren_yymmtext;
            //tx_ri3a_RIREN.Text = ri3aDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            //conn.Close();
            //檢查重複
            if (bdshDao.IsExists("bdlr", "BDNUM", tx_bdlr_BDNUM.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已存在.");
              //bdshDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_bdlr_BDNUM.Text);
              //st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已重新取號.");
              //tx_bdlr_BDNUM.Text = bdshDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_bdlr_BDNUM.Text ="";
            }
            else
            {
              conn.Open();
              DataTable tb_bdsh_ins = new DataTable();
              DbDataAdapter da_ADP_ins = bdshDao.GetDataAdapter(ApVer, "UNbdsh", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_bdsh_ins);
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_bdsh_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["bdlr_gkey"] = st_tempgkey;    // 
                ins_row["bdlr_mkey"] = st_tempgkey;    //
                //
                ins_row["bdlr_BDNUM"] = tx_bdlr_BDNUM.Text.Trim();       // 編　　號
                ins_row["bdlr_BDCJ5"] = tx_bdlr_BDCJ5.Text.Trim();       // 拼音代碼
                ins_row["bdlr_BDCNX"] = dr_bdlr_BDCNX.SelectedValue;       // 分倉類別
                ins_row["bdlr_BDISP"] = ck_bdlr_BDISP.Checked ? "1" : "0";       // 分倉資料
                ins_row["bdlr_BDISD"] = ck_bdlr_BDISD.Checked ? "1" : "0";       // 百貨專櫃
                ins_row["bdlr_BDISH"] = ck_bdlr_BDISH.Checked ? "1" : "0";       // 直營門市
                ins_row["bdlr_BDISM"] = ck_bdlr_BDISM.Checked ? "1" : "0";       // 辦事處
                ins_row["bdlr_BDNAM"] = tx_bdlr_BDNAM.Text.Trim();       // 名　　稱
                ins_row["bdlr_BDSHT"] = tx_bdlr_BDSHT.Text.Trim();       // 簡　　稱
                ins_row["bdlr_BDMLB"] = dr_bdlr_BDMLB.SelectedValue;       // 管理品牌
                ins_row["bdlr_BDNME"] = tx_bdlr_BDNME.Text.Trim();       // 英文名稱
                ins_row["bdlr_BDLEV"] = dr_bdlr_BDLEV.SelectedValue;       // 等　　級
                ins_row["bdlr_BDLC1"] = dr_bdlr_BDLC1.SelectedValue;       // 區　　域
                ins_row["bdlr_BDLC2"] = dr_bdlr_BDLC2.SelectedValue;       // 辦 事 處
                ins_row["bdlr_BDLC3"] = dr_bdlr_BDLC3.SelectedValue;       // 業務區域
                ins_row["bdlr_BDLC4"] = dr_bdlr_BDLC4.SelectedValue;       // 商圈區域
                ins_row["bdlr_BDMN2"] = tx_bdlr_BDMN2.Text.Trim();       // 連絡人　
                ins_row["bdlr_BDA11"] = tx_bdlr_BDA11.Text.Trim();       // 連絡地址
                ins_row["bdlr_BDA12"] = tx_bdlr_BDA12.Text.Trim();       // 連絡地址
                ins_row["bdlr_BDA13"] = tx_bdlr_BDA13.Text.Trim();       // 連絡地址
                ins_row["bdlr_BDMT2"] = tx_bdlr_BDMT2.Text.Trim();       // 連絡人行動
                ins_row["bdlr_BDTEL"] = tx_bdlr_BDTEL.Text.Trim();       // 連絡電話
                ins_row["bdlr_BDFAX"] = tx_bdlr_BDFAX.Text.Trim();       // 傳真號碼
                ins_row["bdlr_BDDPT"] = tx_bdlr_BDDPT.Text.Trim();       // 專櫃編號
                if (tx_bdlr_BDEND.Text.Trim() == "") { ins_row["bdlr_BDEND"] = DBNull.Value; } else { ins_row["bdlr_BDEND"] = sFN.DateStringToDateTime(tx_bdlr_BDEND.Text); }       //停止日期
                ins_row["bdlr_BDACC"] = ck_bdlr_BDACC.Checked ? "1" : "0";       // 轉換傳票
                ins_row["bdlr_BDRMK"] = tx_bdlr_BDRMK.Text.Trim();       // 備　　註
                ins_row["bdlr_BDINV"] = tx_bdlr_BDINV.Text.Trim();       // 統一編號
                ins_row["bdlr_BDINA"] = tx_bdlr_BDINA.Text.Trim();       // 發票抬頭
                ins_row["bdlr_BDB11"] = tx_bdlr_BDB11.Text.Trim();       // 發票地址
                ins_row["bdlr_BDB12"] = tx_bdlr_BDB12.Text.Trim();       // 發票地址
                ins_row["bdlr_BDB13"] = tx_bdlr_BDB13.Text.Trim();       // 發票地址
                ins_row["bdlr_BDTXN"] = tx_bdlr_BDTXN.Text.Trim();       // 稅籍編號
                ins_row["bdlr_BDSAL"] = tx_bdlr_BDSAL.Text.Trim();       // 負責店長
                //
                ins_row["bdlr_BDTYP"] = "P";       //  
                ins_row["bdlr_BDCRD"] = 0;         // 信用额度
                ins_row["bdlr_BDLAB"] = "_";       // 管理品牌
                //
                ins_row["bdlr_trusr"] = UserGkey;  //
                tb_bdsh_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_bdsh_ins);
                //bdshDao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_bdlr_BDNUM.Text.Trim());
                bdshDao.Insertbalog(conn, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
                bdshDao.Insertbtlog(conn, thistran, "bdlr", DAC.GetStringValue(ins_row["bdlr_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["bdlr_gkey"]));
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
                bdshDao.Dispose();
                tb_bdsh_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_bdshba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsh, "bdlr_mkey", hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdshba, "bdlr_mkey", hh_mkey.Value);
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
            if (bdshDao.IsExists("bdlr", "BDNUM", tx_bdlr_BDNUM.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已存在.");
            }
            else
            {
              DataTable tb_bdsh_mod = new DataTable();
              DbDataAdapter da_ADP_mod = bdshDao.GetDataAdapter(ApVer, "UNbdsh", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_bdsh_mod);
              st_SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "' and bdlr_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_bdsh_mod.Select(st_SelDataKey);
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
                  //
                  mod_row["bdlr_BDCJ5"] = tx_bdlr_BDCJ5.Text.Trim();       // 拼音代碼
                  mod_row["bdlr_BDCNX"] = dr_bdlr_BDCNX.SelectedValue;       // 分倉類別
                  mod_row["bdlr_BDISP"] = ck_bdlr_BDISP.Checked ? "1" : "0";       // 分倉資料
                  mod_row["bdlr_BDISD"] = ck_bdlr_BDISD.Checked ? "1" : "0";       // 百貨專櫃
                  mod_row["bdlr_BDISH"] = ck_bdlr_BDISH.Checked ? "1" : "0";       // 直營門市
                  mod_row["bdlr_BDISM"] = ck_bdlr_BDISM.Checked ? "1" : "0";       // 辦事處
                  mod_row["bdlr_BDNAM"] = tx_bdlr_BDNAM.Text.Trim();       // 名　　稱
                  mod_row["bdlr_BDSHT"] = tx_bdlr_BDSHT.Text.Trim();       // 簡　　稱
                  mod_row["bdlr_BDMLB"] = dr_bdlr_BDMLB.SelectedValue;       // 管理品牌
                  mod_row["bdlr_BDNME"] = tx_bdlr_BDNME.Text.Trim();       // 英文名稱
                  mod_row["bdlr_BDLEV"] = dr_bdlr_BDLEV.SelectedValue;       // 等　　級
                  mod_row["bdlr_BDLC1"] = dr_bdlr_BDLC1.SelectedValue;       // 區　　域
                  mod_row["bdlr_BDLC2"] = dr_bdlr_BDLC2.SelectedValue;       // 辦 事 處
                  mod_row["bdlr_BDLC3"] = dr_bdlr_BDLC3.SelectedValue;       // 業務區域
                  mod_row["bdlr_BDLC4"] = dr_bdlr_BDLC4.SelectedValue;       // 商圈區域
                  mod_row["bdlr_BDMN2"] = tx_bdlr_BDMN2.Text.Trim();       // 連絡人　
                  mod_row["bdlr_BDA11"] = tx_bdlr_BDA11.Text.Trim();       // 連絡地址
                  mod_row["bdlr_BDA12"] = tx_bdlr_BDA12.Text.Trim();       // 連絡地址
                  mod_row["bdlr_BDA13"] = tx_bdlr_BDA13.Text.Trim();       // 連絡地址
                  mod_row["bdlr_BDMT2"] = tx_bdlr_BDMT2.Text.Trim();       // 連絡人行動
                  mod_row["bdlr_BDTEL"] = tx_bdlr_BDTEL.Text.Trim();       // 連絡電話
                  mod_row["bdlr_BDFAX"] = tx_bdlr_BDFAX.Text.Trim();       // 傳真號碼
                  mod_row["bdlr_BDDPT"] = tx_bdlr_BDDPT.Text.Trim();       // 專櫃編號
                  if (tx_bdlr_BDEND.Text.Trim() == "") { mod_row["bdlr_BDEND"] = DBNull.Value; } else { mod_row["bdlr_BDEND"] = sFN.DateStringToDateTime(tx_bdlr_BDEND.Text); }       //停止日期
                  mod_row["bdlr_BDACC"] = ck_bdlr_BDACC.Checked ? "1" : "0";       // 轉換傳票
                  mod_row["bdlr_BDRMK"] = tx_bdlr_BDRMK.Text.Trim();       // 備　　註
                  mod_row["bdlr_BDINV"] = tx_bdlr_BDINV.Text.Trim();       // 統一編號
                  mod_row["bdlr_BDINA"] = tx_bdlr_BDINA.Text.Trim();       // 發票抬頭
                  mod_row["bdlr_BDB11"] = tx_bdlr_BDB11.Text.Trim();       // 發票地址
                  mod_row["bdlr_BDB12"] = tx_bdlr_BDB12.Text.Trim();       // 發票地址
                  mod_row["bdlr_BDB13"] = tx_bdlr_BDB13.Text.Trim();       // 發票地址
                  mod_row["bdlr_BDTXN"] = tx_bdlr_BDTXN.Text.Trim();       // 稅籍編號
                  mod_row["bdlr_BDSAL"] = tx_bdlr_BDSAL.Text.Trim();       // 負責店長
                  //
                  mod_row["bdlr_mkey"] = st_tempgkey;        //
                  mod_row["bdlr_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_bdsh_mod);
                  bdshDao.Insertbalog(conn, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
                  bdshDao.Insertbtlog(conn, thistran, "bdlr", DAC.GetStringValue(mod_row["bdlr_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["bdlr_gkey"]));
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
                  bdshDao.Dispose();
                  tb_bdsh_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_bdsh, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_bdshba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdshba, "bdlr_mkey", hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsh, "bdlr_mkey", hh_mkey.Value);
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
        bdshDao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }

    }

    private bool ServerEditCheck_A(ref string sMsg)
    {
      bool ret = true;
      ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();
      //
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bdlr_BDNUM.Text, lb_bdlr_BDNUM.Text, ref sMsg, LangType, sFN);  //編　　號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bdlr_BDNAM.Text, lb_bdlr_BDNAM.Text, ref sMsg, LangType, sFN);  //名　　稱
      if (tx_bdlr_BDINV.Text != "")
      {
        ret = DataCheck.cIsTaiwanINVChk(ret, tx_bdlr_BDINV.Text, lb_bdlr_BDINV.Text, ref sMsg, LangType, sFN);  //統一編號
      }
      //
      if (ret)
      {
        if (tx_bdlr_BDSHT.Text == "")
        {
          tx_bdlr_BDSHT.Text = (tx_bdlr_BDNAM.Text + "   ").Substring(0, 3).Trim();
        }
        tx_bdlr_BDCJ5.Text = sFN.GetCJ5P(tx_bdlr_BDNAM.Text);
      }
      //
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bdlr_BDSHT.Text, lb_bdlr_BDSHT.Text, ref sMsg, LangType, sFN);  //簡　　稱
      //
      DataCheck.Dispose();
      return ret;
    }

    protected void bt_QTS_Click(object sender, EventArgs e)
    {

    }

    ///
    ///
    ///
    ///
    ///
    protected void get_CmdQueryS_ba()
    {
      try
      {
        CmdQueryS_ba = (OleDbCommand)Session["fmbdsh_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ba.CommandText = " and 1=0 ";
      }
    }

    protected void Obj_bdshba_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
        e.InputParameters["st_orderKey"] = " A.BDNUM ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

  }
}