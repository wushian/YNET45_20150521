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

namespace DD2015_45.Forms.bas
{
  public partial class fm_bdlr : FormBase
  {
    string st_object_func = "UNbdlr";
    //string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$";  //編輯頁面
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";  //編輯頁面
    string st_ContentPlaceHolderEdit = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$WebTabEdtRightTop$tmpl0$";
    bool bl_resetKey = true;
    bool bl_showRowA = true;
    //
    //string st_dd_apx = "UNbdlr";         //UNdcnews   與apx 相關
    //string st_dd_table = "bdlr";         //dcnews     與table 相關 
    //string st_ren_head = "";            //DC         與單號相關 
    //string st_ren_yymmtext = "";     //"         與單號相關 
    //string st_ren_cls = "bdlr";        //ren        與單號cls相關 
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
          dr_bdlr_BDMCU = sFN.DropDownListFromClasses(ref dr_bdlr_BDMCU, "UNbcvwh_RTP", "class_text", "class_value");
          dr_bdlr_BDCLA = sFN.DropDownListFromTable(ref dr_bdlr_BDCLA, "PDBDCLA", "BKNUM", "BKNAM", " ( BKNUM LIKE '0%' OR  BKNUM LIKE 'C%') ", "BKNUM");
          dr_bdlr_BDDEX = sFN.DropDownListFromTable(ref dr_bdlr_BDDEX, "PDBDDEX", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDPAY = sFN.DropDownListFromTable(ref dr_bdlr_BDPAY, "PDBDPAY", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDSMA = sFN.DropDownListFromTable(ref dr_bdlr_BDSMA, "PDBDSMA", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDIVY = sFN.DropDownListFromTable(ref dr_bdlr_BDIVY, "PDBDIVY", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDMLB = sFN.DropDownListFromTable(ref dr_bdlr_BDMLB, "PDBPMLB ", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDDTL = sFN.DropDownListFromTable(ref dr_bdlr_BDDTL, "PDYNS", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDCHN = sFN.DropDownListFromTable(ref dr_bdlr_BDCHN, "PDBDCHN", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDTXP = sFN.DropDownListFromClasses(ref dr_bdlr_BDTXP, "UNtax_TXP", "class_text", "class_value");
          //
          WebTab_form.SelectedIndex = 0;
          WebTabEdtRightTop.SelectedIndex = 0;
          Set_Control_A();
          //
          tx_bdlr_BDNUM_s1.Text = "";
          tx_bdlr_BDNAM_s1.Text = "";
          //
          if (Session["fmbdlr_CmdQueryS"] == null)
          {
            act_SERS_L();
          }
          else
          {
            get_CmdQueryS();
            Obj_bdlr.TypeName = "DD2015_45.DAC_bdlr";
            Obj_bdlr.SelectMethod = "SelectTable_bdlr";
            WebDataGrid_bdlr.DataSourceID = "Obj_bdlr";
            Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
            get_CmdQueryS_ba();
            Obj_bdlrba.TypeName = "DD2015_45.DAC_bdlr";
            Obj_bdlrba.SelectMethod = "SelectTable_bdlrba";
            WebDataGrid_bdlrba.DataSourceID = "Obj_bdlrba";
            Bind_WebDataGrid_A(WebDataGrid_bdlrba, bl_showRowA, bl_resetKey); //reset gkey,mkey
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
            //Bind_WebDataGrid_A(WebDataGrid_bdlrba,bl_showRowA,!bl_resetKey);
            ShowOneRow_A(hh_fun_mkey.Value);
            sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlrba, "bdlr_mkey", hh_fun_mkey.Value);
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
        CmdQueryS = (OleDbCommand)Session["fmbdlr_CmdQueryS"];
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
        CmdQueryS.CommandText += " and a.BDTYP='S'  ";
      }

    }

    private void act_SERS_L()
    {
      try
      {
        if (Session["fmbdlr_CmdQueryS"] == null)
        {
          reset_CmdQueryS_comm();
          //
          Session["fmbdlr_CmdQueryS"] = CmdQueryS;
          Obj_bdlr.TypeName = "DD2015_45.DAC_bdlr";
          Obj_bdlr.SelectMethod = "SelectTable_bdlr";
          WebDataGrid_bdlr.DataSourceID = "Obj_bdlr";
          Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_bdlrba.TypeName = "DD2015_45.DAC_bdlr";
          Obj_bdlrba.SelectMethod = "SelectTable_bdlrba";
          WebDataGrid_bdlrba.DataSourceID = "Obj_bdlrba";
          Bind_WebDataGrid_A(WebDataGrid_bdlrba, bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
        }
        else
        {
          get_CmdQueryS();
          Session["fmbdlr_CmdQueryS"] = CmdQueryS;
          Obj_bdlr.TypeName = "DD2015_45.DAC_bdlr";
          Obj_bdlr.SelectMethod = "SelectTable_bdlr";
          WebDataGrid_bdlr.DataSourceID = "Obj_bdlr";
          Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_bdlrba.TypeName = "DD2015_45.DAC_bdlr";
          Obj_bdlrba.SelectMethod = "SelectTable_bdlrba";
          WebDataGrid_bdlrba.DataSourceID = "Obj_bdlrba";
          Bind_WebDataGrid_A(WebDataGrid_bdlrba, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
        }
      }
      catch
      {
        reset_CmdQueryS_comm();
        //
        Session["fmbdlr_CmdQueryS"] = CmdQueryS;
        Obj_bdlr.TypeName = "DD2015_45.DAC_bdlr";
        Obj_bdlr.SelectMethod = "SelectTable_bdlr";
        WebDataGrid_bdlr.DataSourceID = "Obj_bdlr";
        Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
        //
        Obj_bdlrba.TypeName = "DD2015_45.DAC_bdlr";
        Obj_bdlrba.SelectMethod = "SelectTable_bdlrba";
        WebDataGrid_bdlrba.DataSourceID = "Obj_bdlrba";
        Bind_WebDataGrid_A(WebDataGrid_bdlrba, !bl_showRowA, bl_resetKey); //reset gkey,mkey
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
      WebDataGrid_bdlr.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_bdlrba.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      //sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNbdlr", "bdlr");
      sFN.SetFormControlsText(this, PublicVariable.LangType,ApVer, "UNbdlr", "bdlr");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_bdlr, PublicVariable.LangType, ApVer, "UNbdlr", "bdlr");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_bdlrba, PublicVariable.LangType, ApVer, "UNbdlr", "bdlr");
    }

    protected void act_SERS()
    {
      reset_CmdQueryS_comm();
      //
      Session["fmbdlr_CmdQueryS"] = CmdQueryS;
      Obj_bdlr.TypeName = "DD2015_45.DAC_bdlr";
      Obj_bdlr.SelectMethod = "SelectTable_bdlr";
      WebDataGrid_bdlr.DataSourceID = "Obj_bdlr";
      Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      //
      get_CmdQueryS_ba();
      Obj_bdlrba.TypeName = "DD2015_45.DAC_bdlr";
      Obj_bdlrba.SelectMethod = "SelectTable_bdlrba";
      WebDataGrid_bdlrba.DataSourceID = "Obj_bdlrba";
      Bind_WebDataGrid_A(WebDataGrid_bdlrba, bl_showRowA, bl_resetKey); //reset gkey,mkey
    }

    protected void Obj_bdlr_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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

    protected void WebDataGrid_bdlr_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      hh_fun_name.Value = "showa";
      hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("bdlr_mkey").Value);
      WebTab_form.SelectedIndex = 1;
      //
      ShowOneRow_A(hh_fun_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlrba, "bdlr_mkey", hh_fun_mkey.Value);
      SetSerMod_A();
    }

    protected void WebDataGrid_bdlrba_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      hh_fun_name.Value = "showa";
      hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("bdlr_mkey").Value);
      WebTab_form.SelectedIndex = 1;
      //
      ShowOneRow_A(hh_fun_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlrba, "bdlr_mkey", hh_fun_mkey.Value);
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
      ck_bdlr_BDISP.Checked = false;  //分倉資料
      ck_bdlr_BDISF.Checked = false;  //廠商資料
      ck_bdlr_BDISG.Checked = false;  //寄賣經銷
      tx_bdlr_BDNAM.Text = "";  //名　　稱
      tx_bdlr_BDSHT.Text = "";  //簡　　稱
      tx_bdlr_BDNME.Text = "";  //英文名稱
      tx_bdlr_BDC11.Text = "";  //聯絡地址
      tx_bdlr_BDC12.Text = "";  //聯絡地址
      tx_bdlr_BDC13.Text = "";  //聯絡地址
      tx_bdlr_BDA11.Text = "";  //送貨地址
      tx_bdlr_BDA12.Text = "";  //送貨地址
      tx_bdlr_BDA13.Text = "";  //送貨地址
      tx_bdlr_BDTEL.Text = "";  //連絡電話
      tx_bdlr_BDFAX.Text = "";  //傳真號碼
      tx_bdlr_BDMN2.Text = "";  //連絡人　
      tx_bdlr_BDMT2.Text = "";  //連絡人行動
      tx_bdlr_BDEML.Text = "";  //EMAIL 
      tx_bdlr_BDWWW.Text = "";  //公司網址
      tx_bdlr_BDNUP.Text = "";  //請款客戶
      tx_bdlr_BDNUPN.Text = "";  //請款客戶
      tx_bdlr_BDSAL.Text = "";  //負責業務
      tx_es101_BDSAL.Text = "";  //負責業務
      dr_bdlr_BDMCU.SelectedIndex = -1;  //貨運公司
      tx_bdlr_BDINA.Text = "";  //發票抬頭
      tx_bdlr_BDINV.Text = "";  //統一編號
      tx_bdlr_BDB11.Text = "";  //發票地址
      tx_bdlr_BDB12.Text = "";  //發票地址
      tx_bdlr_BDB13.Text = "";  //發票地址
      tx_bdlr_BDMN1.Text = "";  //負責人　
      tx_bdlr_BDMT1.Text = "";  //行動電話
      tx_bdlr_BDD11.Text = "";  //帳單地址
      tx_bdlr_BDD12.Text = "";  //帳單地址
      tx_bdlr_BDD13.Text = "";  //帳單地址
      tx_bdlr_BDMNA.Text = "";  //連絡會計
      tx_bdlr_BDTEA.Text = "";  //會計電話
      ck_bdlr_BDPR1.Checked = false;  //列印名條
      ck_bdlr_BDACC.Checked = false;  //轉換傳票
      tx_bdlr_BDBK1.Text = "";  //付款銀行
      tx_bdlr_BDBN1.Text = "";  //銀行名稱
      tx_bdlr_BDBO1.Text = "";  //銀行帳號
      tx_bdlr_BDBK2.Text = "";  //付款銀行
      tx_bdlr_BDBN2.Text = "";  //銀行名稱
      tx_bdlr_BDBO2.Text = "";  //銀行帳號
      tx_bdlr_BDBK3.Text = "";  //付款銀行
      tx_bdlr_BDBN3.Text = "";  //銀行名稱
      tx_bdlr_BDBO3.Text = "";  //銀行帳號
      tx_bdlr_BDEND.Text = "";  //停止日期
      tx_bdlr_BDDPT.Text = "";  //部門設定
      dr_bdlr_BDCLA.SelectedIndex = -1;  //客戶等級
      dr_bdlr_BDDEX.SelectedIndex = -1;  //價格等級
      tx_bdlr_BDCRD.Text = "0";  //信用額度
      dr_bdlr_BDMLB.SelectedIndex = -1;  //管理品牌
      dr_bdlr_BDPAY.SelectedIndex = -1;  //付款方式
      dr_bdlr_BDDTL.SelectedIndex = -1;  //資料安全
      tx_bdlr_BDBCN.Text = "";  //連接編號
      dr_bdlr_BDCHN.SelectedIndex = -1;  //通路類別
      dr_bdlr_BDSMA.SelectedIndex = -1;  //營業類別
      dr_bdlr_BDIVY.SelectedIndex = -1;  //發票方式
      dr_bdlr_BDTXP.SelectedIndex = -1;  //計稅方式
      tx_bdlr_BDNXM.Text = "0";  //下月帳日
      tx_bdlr_BDRMK.Text = "";  //備　　註
      tx_bdlr_BDNOT.Text = "";  //備註說明
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
      ck_bdlr_BDISP.Enabled = false;  //分倉資料
      ck_bdlr_BDISF.Enabled = false;  //廠商資料
      ck_bdlr_BDISG.Enabled = false;  //寄賣經銷
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, false);   //名　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDSHT, false);   //簡　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDNME, false);   //英文名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDC11, false);   //聯絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDC12, false);   //聯絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDC13, false);   //聯絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA11, false);   //送貨地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA12, false);   //送貨地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA13, false);   //送貨地址
      clsGV.TextBox_Set(ref tx_bdlr_BDTEL, false);   //連絡電話
      clsGV.TextBox_Set(ref tx_bdlr_BDFAX, false);   //傳真號碼
      clsGV.TextBox_Set(ref tx_bdlr_BDMN2, false);   //連絡人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT2, false);   //連絡人行動
      clsGV.TextBox_Set(ref tx_bdlr_BDEML, false);   //EMAIL 
      clsGV.TextBox_Set(ref tx_bdlr_BDWWW, false);   //公司網址
      clsGV.TextBox_Set(ref tx_bdlr_BDNUP, false);   //請款客戶
      clsGV.TextBox_Set(ref tx_bdlr_BDNUPN, false);   //請款客戶
      clsGV.TextBox_Set(ref tx_bdlr_BDSAL, false);   //負責業務
      clsGV.TextBox_Set(ref tx_es101_BDSAL, false);   //負責業務
      clsGV.Drpdown_Set(ref dr_bdlr_BDMCU, ref tx_bdlr_BDMCU, false);   //貨運公司
      clsGV.TextBox_Set(ref tx_bdlr_BDINA, false);   //發票抬頭
      clsGV.TextBox_Set(ref tx_bdlr_BDINV, false);   //統一編號
      clsGV.TextBox_Set(ref tx_bdlr_BDB11, false);   //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB12, false);   //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB13, false);   //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDMN1, false);   //負責人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT1, false);   //行動電話
      clsGV.TextBox_Set(ref tx_bdlr_BDD11, false);   //帳單地址
      clsGV.TextBox_Set(ref tx_bdlr_BDD12, false);   //帳單地址
      clsGV.TextBox_Set(ref tx_bdlr_BDD13, false);   //帳單地址
      clsGV.TextBox_Set(ref tx_bdlr_BDMNA, false);   //連絡會計
      clsGV.TextBox_Set(ref tx_bdlr_BDTEA, false);   //會計電話
      ck_bdlr_BDPR1.Enabled = false;  //列印名條
      ck_bdlr_BDACC.Enabled = false;  //轉換傳票
      clsGV.TextBox_Set(ref tx_bdlr_BDBK1, false);   //付款銀行
      clsGV.TextBox_Set(ref tx_bdlr_BDBN1, false);   //銀行名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDBO1, false);   //銀行帳號
      clsGV.TextBox_Set(ref tx_bdlr_BDBK2, false);   //付款銀行
      clsGV.TextBox_Set(ref tx_bdlr_BDBN2, false);   //銀行名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDBO2, false);   //銀行帳號
      clsGV.TextBox_Set(ref tx_bdlr_BDBK3, false);   //付款銀行
      clsGV.TextBox_Set(ref tx_bdlr_BDBN3, false);   //銀行名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDBO3, false);   //銀行帳號
      clsGV.TextBox_Set(ref tx_bdlr_BDEND, false);   //停止日期
      clsGV.TextBox_Set(ref tx_bdlr_BDDPT, false);   //部門設定
      clsGV.Drpdown_Set(ref dr_bdlr_BDCLA, ref tx_bdlr_BDCLA, false);   //客戶等級
      clsGV.Drpdown_Set(ref dr_bdlr_BDDEX, ref tx_bdlr_BDDEX, false);   //價格等級
      clsGV.TextBox_Set(ref tx_bdlr_BDCRD, false);   //信用額度
      clsGV.Drpdown_Set(ref dr_bdlr_BDMLB, ref tx_bdlr_BDMLB, false);   //管理品牌
      clsGV.Drpdown_Set(ref dr_bdlr_BDPAY, ref tx_bdlr_BDPAY, false);   //付款方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDDTL, ref tx_bdlr_BDDTL, false);   //資料安全
      clsGV.TextBox_Set(ref tx_bdlr_BDBCN, false);   //連接編號
      clsGV.Drpdown_Set(ref dr_bdlr_BDCHN, ref tx_bdlr_BDCHN, false);   //通路類別
      clsGV.Drpdown_Set(ref dr_bdlr_BDSMA, ref tx_bdlr_BDSMA, false);   //營業類別
      clsGV.Drpdown_Set(ref dr_bdlr_BDIVY, ref tx_bdlr_BDIVY, false);   //發票方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDTXP, ref tx_bdlr_BDTXP, false);   //計稅方式
      clsGV.TextBox_Set(ref tx_bdlr_BDNXM, false);   //下月帳日
      clsGV.TextBox_Set(ref tx_bdlr_BDRMK, false);   //備　　註
      clsGV.TextBox_Set(ref tx_bdlr_BDNOT, false);   //備註說明
      //
      clsGV.SetControlShowAlert(ref lb_bdlr_BDNUM, ref tx_bdlr_BDNUM, true);  // 編　　號
      clsGV.SetControlShowAlert(ref lb_bdlr_BDNAM, ref tx_bdlr_BDNAM, true);  // 名　　稱
      clsGV.SetControlShowAlert(ref lb_bdlr_BDTEL, ref tx_bdlr_BDTEL, true);  // 連絡電話
      //
      tx_bdlr_BDNUP.Attributes.Remove("onblur");
      tx_bdlr_BDSAL.Attributes.Remove("onblur");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      WebDataGrid_bdlr.Enabled = true;
      WebDataGrid_bdlrba.Enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetEditMod_A()
    {
      //
      clsGV.TextBox_Set(ref tx_bdlr_BDNUM, true);  //編　　號
      clsGV.TextBox_Set(ref tx_bdlr_BDCJ5, true);  //拼音代碼
      ck_bdlr_BDISP.Enabled = true;  //分倉資料
      ck_bdlr_BDISF.Enabled = true;  //廠商資料
      ck_bdlr_BDISG.Enabled = true;  //寄賣經銷
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, true);  //名　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDSHT, true);  //簡　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDNME, true);  //英文名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDC11, true);  //聯絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDC12, true);  //聯絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDC13, true);  //聯絡地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA11, true);  //送貨地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA12, true);  //送貨地址
      clsGV.TextBox_Set(ref tx_bdlr_BDA13, true);  //送貨地址
      clsGV.TextBox_Set(ref tx_bdlr_BDTEL, true);  //連絡電話
      clsGV.TextBox_Set(ref tx_bdlr_BDFAX, true);  //傳真號碼
      clsGV.TextBox_Set(ref tx_bdlr_BDMN2, true);  //連絡人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT2, true);  //連絡人行動
      clsGV.TextBox_Set(ref tx_bdlr_BDEML, true);  //EMAIL 
      clsGV.TextBox_Set(ref tx_bdlr_BDWWW, true);  //公司網址
      clsGV.TextBox_Set(ref tx_bdlr_BDNUP, true);  //請款客戶
      clsGV.TextBox_Set(ref tx_bdlr_BDNUPN, true);  //請款客戶
      clsGV.TextBox_Set(ref tx_bdlr_BDSAL, true);  //負責業務
      clsGV.TextBox_Set(ref tx_es101_BDSAL, true);  //負責業務
      clsGV.Drpdown_Set(ref dr_bdlr_BDMCU, ref tx_bdlr_BDMCU, true);   //貨運公司
      clsGV.TextBox_Set(ref tx_bdlr_BDINA, true);  //發票抬頭
      clsGV.TextBox_Set(ref tx_bdlr_BDINV, true);  //統一編號
      clsGV.TextBox_Set(ref tx_bdlr_BDB11, true);  //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB12, true);  //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDB13, true);  //發票地址
      clsGV.TextBox_Set(ref tx_bdlr_BDMN1, true);  //負責人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT1, true);  //行動電話
      clsGV.TextBox_Set(ref tx_bdlr_BDD11, true);  //帳單地址
      clsGV.TextBox_Set(ref tx_bdlr_BDD12, true);  //帳單地址
      clsGV.TextBox_Set(ref tx_bdlr_BDD13, true);  //帳單地址
      clsGV.TextBox_Set(ref tx_bdlr_BDMNA, true);  //連絡會計
      clsGV.TextBox_Set(ref tx_bdlr_BDTEA, true);  //會計電話
      ck_bdlr_BDPR1.Enabled = true;  //列印名條
      ck_bdlr_BDACC.Enabled = true;  //轉換傳票
      clsGV.TextBox_Set(ref tx_bdlr_BDBK1, true);  //付款銀行
      clsGV.TextBox_Set(ref tx_bdlr_BDBN1, true);  //銀行名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDBO1, true);  //銀行帳號
      clsGV.TextBox_Set(ref tx_bdlr_BDBK2, true);  //付款銀行
      clsGV.TextBox_Set(ref tx_bdlr_BDBN2, true);  //銀行名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDBO2, true);  //銀行帳號
      clsGV.TextBox_Set(ref tx_bdlr_BDBK3, true);  //付款銀行
      clsGV.TextBox_Set(ref tx_bdlr_BDBN3, true);  //銀行名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDBO3, true);  //銀行帳號
      clsGV.TextBox_Set(ref tx_bdlr_BDEND, true);  //停止日期
      clsGV.TextBox_Set(ref tx_bdlr_BDDPT, true);  //部門設定
      clsGV.Drpdown_Set(ref dr_bdlr_BDCLA, ref tx_bdlr_BDCLA, true);   //客戶等級
      clsGV.Drpdown_Set(ref dr_bdlr_BDDEX, ref tx_bdlr_BDDEX, true);   //價格等級
      clsGV.TextBox_Set(ref tx_bdlr_BDCRD, true);  //信用額度
      clsGV.Drpdown_Set(ref dr_bdlr_BDMLB, ref tx_bdlr_BDMLB, true);   //管理品牌
      clsGV.Drpdown_Set(ref dr_bdlr_BDPAY, ref tx_bdlr_BDPAY, true);   //付款方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDDTL, ref tx_bdlr_BDDTL, true);   //資料安全
      clsGV.TextBox_Set(ref tx_bdlr_BDBCN, true);  //連接編號
      clsGV.Drpdown_Set(ref dr_bdlr_BDCHN, ref tx_bdlr_BDCHN, true);   //通路類別
      clsGV.Drpdown_Set(ref dr_bdlr_BDSMA, ref tx_bdlr_BDSMA, true);   //營業類別
      clsGV.Drpdown_Set(ref dr_bdlr_BDIVY, ref tx_bdlr_BDIVY, true);   //發票方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDTXP, ref tx_bdlr_BDTXP, true);   //計稅方式
      clsGV.TextBox_Set(ref tx_bdlr_BDNXM, true);  //下月帳日
      clsGV.TextBox_Set(ref tx_bdlr_BDRMK, true);  //備　　註
      clsGV.TextBox_Set(ref tx_bdlr_BDNOT, true);  //備註說明
      // 
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDNUM, ref tx_bdlr_BDNUM, true);  // 編　　號
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDNAM, ref tx_bdlr_BDNAM, true);  // 名　　稱
      clsGV.SetTextBoxEditAlert(ref lb_bdlr_BDTEL, ref tx_bdlr_BDTEL, true);  // 連絡電話
      //
      tx_bdlr_BDNUP.Attributes.Add("onblur", "return  get_bdlr_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_bdlr_BDNUP','" + st_ContentPlaceHolderEdit + "tx_bdlr_BDNUPN'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bdlr.aspx" + "','" + "廠商資料" + "')");
      tx_bdlr_BDSAL.Attributes.Add("onblur", "return get_es101_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_bdlr_BDSAL','" + st_ContentPlaceHolderEdit + "tx_es010_BDSAL'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_es101.aspx" + "','" + "業務資料" + "')");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      WebDataGrid_bdlr.Enabled = false;
      WebDataGrid_bdlrba.Enabled = false;
    }

    private void ShowOneRow_A(string st_mkey)
    {
      DAC_bdlr bdlrDao = new DAC_bdlr(conn);
      DataTable tb_bdlr = new DataTable();
      OleDbCommand cmd_where = new OleDbCommand();
      //
      cmd_where.CommandText = " and a.mkey=? ";
      DAC.AddParam(cmd_where, "mkey", st_mkey);
      tb_bdlr = bdlrDao.SelectTableForTextEdit_bdlr(cmd_where);
      if (tb_bdlr.Rows.Count == 1)
      {
        BindText_A(tb_bdlr.Rows[0]);
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
      tb_bdlr.Dispose();
      bdlrDao.Dispose();
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
      ck_bdlr_BDISP.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISP"]));  //分倉資料
      ck_bdlr_BDISF.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISF"]));  //廠商資料
      ck_bdlr_BDISG.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISG"]));  //寄賣經銷
      tx_bdlr_BDNAM.Text = DAC.GetStringValue(CurRow["bdlr_BDNAM"]);  //名　　稱
      tx_bdlr_BDSHT.Text = DAC.GetStringValue(CurRow["bdlr_BDSHT"]);  //簡　　稱
      tx_bdlr_BDNME.Text = DAC.GetStringValue(CurRow["bdlr_BDNME"]);  //英文名稱
      tx_bdlr_BDC11.Text = DAC.GetStringValue(CurRow["bdlr_BDC11"]);  //聯絡地址
      tx_bdlr_BDC12.Text = DAC.GetStringValue(CurRow["bdlr_BDC12"]);  //聯絡地址
      tx_bdlr_BDC13.Text = DAC.GetStringValue(CurRow["bdlr_BDC13"]);  //聯絡地址
      tx_bdlr_BDA11.Text = DAC.GetStringValue(CurRow["bdlr_BDA11"]);  //送貨地址
      tx_bdlr_BDA12.Text = DAC.GetStringValue(CurRow["bdlr_BDA12"]);  //送貨地址
      tx_bdlr_BDA13.Text = DAC.GetStringValue(CurRow["bdlr_BDA13"]);  //送貨地址
      tx_bdlr_BDTEL.Text = DAC.GetStringValue(CurRow["bdlr_BDTEL"]);  //連絡電話
      tx_bdlr_BDFAX.Text = DAC.GetStringValue(CurRow["bdlr_BDFAX"]);  //傳真號碼
      tx_bdlr_BDMN2.Text = DAC.GetStringValue(CurRow["bdlr_BDMN2"]);  //連絡人　
      tx_bdlr_BDMT2.Text = DAC.GetStringValue(CurRow["bdlr_BDMT2"]);  //連絡人行動
      tx_bdlr_BDEML.Text = DAC.GetStringValue(CurRow["bdlr_BDEML"]);  //EMAIL 
      tx_bdlr_BDWWW.Text = DAC.GetStringValue(CurRow["bdlr_BDWWW"]);  //公司網址
      tx_bdlr_BDNUP.Text = DAC.GetStringValue(CurRow["bdlr_BDNUP"]);  //請款客戶
      tx_bdlr_BDNUPN.Text = DAC.GetStringValue(CurRow["bdlr_BDNUPN"]);  //請款客戶
      tx_bdlr_BDSAL.Text = DAC.GetStringValue(CurRow["bdlr_BDSAL"]);  //負責業務
      tx_es101_BDSAL.Text = DAC.GetStringValue(CurRow["es101_BDSAL"]);  //負責業務
      dr_bdlr_BDMCU = sFN.SetDropDownList(ref dr_bdlr_BDMCU, DAC.GetStringValue(CurRow["bdlr_BDMCU"]));  //貨運公司
      tx_bdlr_BDINA.Text = DAC.GetStringValue(CurRow["bdlr_BDINA"]);  //發票抬頭
      tx_bdlr_BDINV.Text = DAC.GetStringValue(CurRow["bdlr_BDINV"]);  //統一編號
      tx_bdlr_BDB11.Text = DAC.GetStringValue(CurRow["bdlr_BDB11"]);  //發票地址
      tx_bdlr_BDB12.Text = DAC.GetStringValue(CurRow["bdlr_BDB12"]);  //發票地址
      tx_bdlr_BDB13.Text = DAC.GetStringValue(CurRow["bdlr_BDB13"]);  //發票地址
      tx_bdlr_BDMN1.Text = DAC.GetStringValue(CurRow["bdlr_BDMN1"]);  //負責人　
      tx_bdlr_BDMT1.Text = DAC.GetStringValue(CurRow["bdlr_BDMT1"]);  //行動電話
      tx_bdlr_BDD11.Text = DAC.GetStringValue(CurRow["bdlr_BDD11"]);  //帳單地址
      tx_bdlr_BDD12.Text = DAC.GetStringValue(CurRow["bdlr_BDD12"]);  //帳單地址
      tx_bdlr_BDD13.Text = DAC.GetStringValue(CurRow["bdlr_BDD13"]);  //帳單地址
      tx_bdlr_BDMNA.Text = DAC.GetStringValue(CurRow["bdlr_BDMNA"]);  //連絡會計
      tx_bdlr_BDTEA.Text = DAC.GetStringValue(CurRow["bdlr_BDTEA"]);  //會計電話
      ck_bdlr_BDPR1.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDPR1"]));  //列印名條
      ck_bdlr_BDACC.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDACC"]));  //轉換傳票
      tx_bdlr_BDBK1.Text = DAC.GetStringValue(CurRow["bdlr_BDBK1"]);  //付款銀行
      tx_bdlr_BDBN1.Text = DAC.GetStringValue(CurRow["bdlr_BDBN1"]);  //銀行名稱
      tx_bdlr_BDBO1.Text = DAC.GetStringValue(CurRow["bdlr_BDBO1"]);  //銀行帳號
      tx_bdlr_BDBK2.Text = DAC.GetStringValue(CurRow["bdlr_BDBK2"]);  //付款銀行
      tx_bdlr_BDBN2.Text = DAC.GetStringValue(CurRow["bdlr_BDBN2"]);  //銀行名稱
      tx_bdlr_BDBO2.Text = DAC.GetStringValue(CurRow["bdlr_BDBO2"]);  //銀行帳號
      tx_bdlr_BDBK3.Text = DAC.GetStringValue(CurRow["bdlr_BDBK3"]);  //付款銀行
      tx_bdlr_BDBN3.Text = DAC.GetStringValue(CurRow["bdlr_BDBN3"]);  //銀行名稱
      tx_bdlr_BDBO3.Text = DAC.GetStringValue(CurRow["bdlr_BDBO3"]);  //銀行帳號
      if (CurRow["bdlr_BDEND"] == DBNull.Value) { tx_bdlr_BDEND.Text = ""; } else { tx_bdlr_BDEND.Text = DAC.GetDateTimeValue(CurRow["bdlr_BDEND"]).ToString(sys_DateFormat); }  //停止日期
      tx_bdlr_BDDPT.Text = DAC.GetStringValue(CurRow["bdlr_BDDPT"]);  //部門設定
      dr_bdlr_BDCLA = sFN.SetDropDownList(ref dr_bdlr_BDCLA, DAC.GetStringValue(CurRow["bdlr_BDCLA"]));  //客戶等級
      dr_bdlr_BDDEX = sFN.SetDropDownList(ref dr_bdlr_BDDEX, DAC.GetStringValue(CurRow["bdlr_BDDEX"]));  //價格等級
      tx_bdlr_BDCRD.Text = DAC.GetStringValue(CurRow["bdlr_BDCRD"]);  //信用額度
      dr_bdlr_BDMLB = sFN.SetDropDownList(ref dr_bdlr_BDMLB, DAC.GetStringValue(CurRow["bdlr_BDMLB"]));  //管理品牌
      dr_bdlr_BDPAY = sFN.SetDropDownList(ref dr_bdlr_BDPAY, DAC.GetStringValue(CurRow["bdlr_BDPAY"]));  //付款方式
      dr_bdlr_BDDTL = sFN.SetDropDownList(ref dr_bdlr_BDDTL, DAC.GetStringValue(CurRow["bdlr_BDDTL"]));  //資料安全
      tx_bdlr_BDBCN.Text = DAC.GetStringValue(CurRow["bdlr_BDBCN"]);  //連接編號
      dr_bdlr_BDCHN = sFN.SetDropDownList(ref dr_bdlr_BDCHN, DAC.GetStringValue(CurRow["bdlr_BDCHN"]));  //通路類別
      dr_bdlr_BDSMA = sFN.SetDropDownList(ref dr_bdlr_BDSMA, DAC.GetStringValue(CurRow["bdlr_BDSMA"]));  //營業類別
      dr_bdlr_BDIVY = sFN.SetDropDownList(ref dr_bdlr_BDIVY, DAC.GetStringValue(CurRow["bdlr_BDIVY"]));  //發票方式
      dr_bdlr_BDTXP = sFN.SetDropDownList(ref dr_bdlr_BDTXP, DAC.GetStringValue(CurRow["bdlr_BDTXP"]));  //計稅方式
      tx_bdlr_BDNXM.Text = DAC.GetStringValue(CurRow["bdlr_BDNXM"]);  //下月帳日
      tx_bdlr_BDRMK.Text = DAC.GetStringValue(CurRow["bdlr_BDRMK"]);  //備　　註
      tx_bdlr_BDNOT.Text = DAC.GetStringValue(CurRow["bdlr_BDNOT"]);  //備註說明
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
        WebTab_form.SelectedIndex = 1;
        WebTabEdtRightTop.SelectedIndex = 0;
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
        WebTab_form.SelectedIndex = 1;
        WebTabEdtRightTop.SelectedIndex = 0;
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
      Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, !bl_resetKey);
      Bind_WebDataGrid_A(WebDataGrid_bdlrba, !bl_showRowA, !bl_resetKey);
      //
      ShowOneRow_A(hh_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlrba, "bdlr_mkey", hh_mkey.Value);
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
        string st_nextkey = sFN.WebDataGrid_NextKey(WebDataGrid_bdlrba, "bdlr_mkey", hh_mkey.Value);
        //
        DAC bdlrDao = new DAC_bdlr(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "' and bdlr_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_bdlr = new DataTable();
        OleDbConnection connD = new OleDbConnection();
        connD = DAC.NewReaderConnr();
        connD.Open();
        DbDataAdapter da_ADP = bdlrDao.GetDataAdapter(ApVer, "UNbdlr", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "", "SEL DEL ");
        da_ADP.SelectCommand.Connection = connD;
        da_ADP.DeleteCommand.Connection = connD;
        da_ADP.Fill(tb_bdlr);
        DataRow[] DelRow = tb_bdlr.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          OleDbTransaction thistran = connD.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            bdlrDao.Insertbalog(connD, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
            bdlrDao.Insertbtlog(connD, thistran, "bdlr", DAC.GetStringValue(DelRow[0]["bdlr_BDNUM"]), "D", UserName, DAC.GetStringValue(DelRow[0]["bdlr_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_bdlr);
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
        bdlrDao.Dispose();
        tb_bdlr.Dispose();
        da_ADP.Dispose();
        //
        if (bl_delok)
        {
          hh_mkey.Value = st_nextkey;
          act_SERS_L();
          SetSerMod_A();
          ShowOneRow_A(hh_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlrba, "bdlr_mkey", hh_mkey.Value);
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
        DAC_bdlr bdlrDao = new DAC_bdlr(conn);
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
            if (bdlrDao.IsExists("bdlr", "BDNUM", tx_bdlr_BDNUM.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已存在.");
              //bdlrDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_bdlr_BDNUM.Text);
              //st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已重新取號.");
              //tx_bdlr_BDNUM.Text = bdlrDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_bdlr_BDNUM.Text ="";
            }
            else
            {
              conn.Open();
              DataTable tb_bdlr_ins = new DataTable();
              DbDataAdapter da_ADP_ins = bdlrDao.GetDataAdapter(ApVer, "UNbdlr", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_bdlr_ins);
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_bdlr_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["bdlr_gkey"] = st_tempgkey;    // 
                ins_row["bdlr_mkey"] = st_tempgkey;    //
                //
                ins_row["bdlr_BDNUM"] = tx_bdlr_BDNUM.Text.Trim();       // 編　　號
                ins_row["bdlr_BDCJ5"] = tx_bdlr_BDCJ5.Text.Trim();       // 拼音代碼
                ins_row["bdlr_BDISP"] = ck_bdlr_BDISP.Checked ? "1" : "0";       // 分倉資料
                ins_row["bdlr_BDISF"] = ck_bdlr_BDISF.Checked ? "1" : "0";       // 廠商資料
                ins_row["bdlr_BDISG"] = ck_bdlr_BDISG.Checked ? "1" : "0";       // 寄賣經銷
                ins_row["bdlr_BDNAM"] = tx_bdlr_BDNAM.Text.Trim();       // 名　　稱
                ins_row["bdlr_BDSHT"] = tx_bdlr_BDSHT.Text.Trim();       // 簡　　稱
                ins_row["bdlr_BDNME"] = tx_bdlr_BDNME.Text.Trim();       // 英文名稱
                ins_row["bdlr_BDC11"] = tx_bdlr_BDC11.Text.Trim();       // 聯絡地址
                ins_row["bdlr_BDC12"] = tx_bdlr_BDC12.Text.Trim();       // 聯絡地址
                ins_row["bdlr_BDC13"] = tx_bdlr_BDC13.Text.Trim();       // 聯絡地址
                ins_row["bdlr_BDA11"] = tx_bdlr_BDA11.Text.Trim();       // 送貨地址
                ins_row["bdlr_BDA12"] = tx_bdlr_BDA12.Text.Trim();       // 送貨地址
                ins_row["bdlr_BDA13"] = tx_bdlr_BDA13.Text.Trim();       // 送貨地址
                ins_row["bdlr_BDTEL"] = tx_bdlr_BDTEL.Text.Trim();       // 連絡電話
                ins_row["bdlr_BDFAX"] = tx_bdlr_BDFAX.Text.Trim();       // 傳真號碼
                ins_row["bdlr_BDMN2"] = tx_bdlr_BDMN2.Text.Trim();       // 連絡人　
                ins_row["bdlr_BDMT2"] = tx_bdlr_BDMT2.Text.Trim();       // 連絡人行動
                ins_row["bdlr_BDEML"] = tx_bdlr_BDEML.Text.Trim();       // EMAIL 
                ins_row["bdlr_BDWWW"] = tx_bdlr_BDWWW.Text.Trim();       // 公司網址
                ins_row["bdlr_BDNUP"] = tx_bdlr_BDNUP.Text.Trim();       // 請款客戶
                ins_row["bdlr_BDSAL"] = tx_bdlr_BDSAL.Text.Trim();       // 負責業務
                ins_row["bdlr_BDMCU"] = dr_bdlr_BDMCU.SelectedValue;       // 貨運公司
                ins_row["bdlr_BDINA"] = tx_bdlr_BDINA.Text.Trim();       // 發票抬頭
                ins_row["bdlr_BDINV"] = tx_bdlr_BDINV.Text.Trim();       // 統一編號
                ins_row["bdlr_BDB11"] = tx_bdlr_BDB11.Text.Trim();       // 發票地址
                ins_row["bdlr_BDB12"] = tx_bdlr_BDB12.Text.Trim();       // 發票地址
                ins_row["bdlr_BDB13"] = tx_bdlr_BDB13.Text.Trim();       // 發票地址
                ins_row["bdlr_BDMN1"] = tx_bdlr_BDMN1.Text.Trim();       // 負責人　
                ins_row["bdlr_BDMT1"] = tx_bdlr_BDMT1.Text.Trim();       // 行動電話
                ins_row["bdlr_BDD11"] = tx_bdlr_BDD11.Text.Trim();       // 帳單地址
                ins_row["bdlr_BDD12"] = tx_bdlr_BDD12.Text.Trim();       // 帳單地址
                ins_row["bdlr_BDD13"] = tx_bdlr_BDD13.Text.Trim();       // 帳單地址
                ins_row["bdlr_BDMNA"] = tx_bdlr_BDMNA.Text.Trim();       // 連絡會計
                ins_row["bdlr_BDTEA"] = tx_bdlr_BDTEA.Text.Trim();       // 會計電話
                ins_row["bdlr_BDPR1"] = ck_bdlr_BDPR1.Checked ? "1" : "0";       // 列印名條
                ins_row["bdlr_BDACC"] = ck_bdlr_BDACC.Checked ? "1" : "0";       // 轉換傳票
                ins_row["bdlr_BDBK1"] = tx_bdlr_BDBK1.Text.Trim();       // 付款銀行
                ins_row["bdlr_BDBN1"] = tx_bdlr_BDBN1.Text.Trim();       // 銀行名稱
                ins_row["bdlr_BDBO1"] = tx_bdlr_BDBO1.Text.Trim();       // 銀行帳號
                ins_row["bdlr_BDBK2"] = tx_bdlr_BDBK2.Text.Trim();       // 付款銀行
                ins_row["bdlr_BDBN2"] = tx_bdlr_BDBN2.Text.Trim();       // 銀行名稱
                ins_row["bdlr_BDBO2"] = tx_bdlr_BDBO2.Text.Trim();       // 銀行帳號
                ins_row["bdlr_BDBK3"] = tx_bdlr_BDBK3.Text.Trim();       // 付款銀行
                ins_row["bdlr_BDBN3"] = tx_bdlr_BDBN3.Text.Trim();       // 銀行名稱
                ins_row["bdlr_BDBO3"] = tx_bdlr_BDBO3.Text.Trim();       // 銀行帳號
                if (tx_bdlr_BDEND.Text.Trim() == "") { ins_row["bdlr_BDEND"] = DBNull.Value; } else { ins_row["bdlr_BDEND"] = sFN.DateStringToDateTime(tx_bdlr_BDEND.Text); }       //停止日期
                ins_row["bdlr_BDDPT"] = tx_bdlr_BDDPT.Text.Trim();       // 部門設定
                ins_row["bdlr_BDCLA"] = dr_bdlr_BDCLA.SelectedValue;       // 客戶等級
                ins_row["bdlr_BDDEX"] = dr_bdlr_BDDEX.SelectedValue;       // 價格等級
                ins_row["bdlr_BDCRD"] = tx_bdlr_BDCRD.Text.Trim();       // 信用額度
                ins_row["bdlr_BDMLB"] = dr_bdlr_BDMLB.SelectedValue;       // 管理品牌
                ins_row["bdlr_BDPAY"] = dr_bdlr_BDPAY.SelectedValue;       // 付款方式
                ins_row["bdlr_BDDTL"] = dr_bdlr_BDDTL.SelectedValue;       // 資料安全
                ins_row["bdlr_BDBCN"] = tx_bdlr_BDBCN.Text.Trim();       // 連接編號
                ins_row["bdlr_BDCHN"] = dr_bdlr_BDCHN.SelectedValue;       // 通路類別
                ins_row["bdlr_BDSMA"] = dr_bdlr_BDSMA.SelectedValue;       // 營業類別
                ins_row["bdlr_BDIVY"] = dr_bdlr_BDIVY.SelectedValue;       // 發票方式
                ins_row["bdlr_BDTXP"] = dr_bdlr_BDTXP.SelectedValue;       // 計稅方式
                ins_row["bdlr_BDNXM"] = tx_bdlr_BDNXM.Text.Trim();       // 下月帳日
                ins_row["bdlr_BDRMK"] = tx_bdlr_BDRMK.Text.Trim();       // 備　　註
                ins_row["bdlr_BDNOT"] = tx_bdlr_BDNOT.Text.Trim();       // 備註說明
                //
                ins_row["bdlr_BDTYP"] = "S";       //  
                ins_row["bdlr_BDMLB"] = "_";       // 管理品牌
                ins_row["bdlr_BDCRD"] = 0;         // 信用额度
                //
                ins_row["bdlr_trusr"] = UserGkey;  //
                tb_bdlr_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_bdlr_ins);
                //bdlrDao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_bdlr_BDNUM.Text.Trim());
                bdlrDao.Insertbalog(conn, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
                bdlrDao.Insertbtlog(conn, thistran, "bdlr", DAC.GetStringValue(ins_row["bdlr_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["bdlr_gkey"]));
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
                bdlrDao.Dispose();
                tb_bdlr_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_bdlrba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlr, "bdlr_mkey", hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlrba, "bdlr_mkey", hh_mkey.Value);
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
            if (bdlrDao.IsExists("bdlr", "BDNUM", tx_bdlr_BDNUM.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已存在.");
            }
            else
            {
              DataTable tb_bdlr_mod = new DataTable();
              DbDataAdapter da_ADP_mod = bdlrDao.GetDataAdapter(ApVer, "UNbdlr", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_bdlr_mod);
              st_SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "' and bdlr_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_bdlr_mod.Select(st_SelDataKey);
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
                  mod_row["bdlr_BDISP"] = ck_bdlr_BDISP.Checked ? "1" : "0";       // 分倉資料
                  mod_row["bdlr_BDISF"] = ck_bdlr_BDISF.Checked ? "1" : "0";       // 廠商資料
                  mod_row["bdlr_BDISG"] = ck_bdlr_BDISG.Checked ? "1" : "0";       // 寄賣經銷
                  mod_row["bdlr_BDNAM"] = tx_bdlr_BDNAM.Text.Trim();       // 名　　稱
                  mod_row["bdlr_BDSHT"] = tx_bdlr_BDSHT.Text.Trim();       // 簡　　稱
                  mod_row["bdlr_BDNME"] = tx_bdlr_BDNME.Text.Trim();       // 英文名稱
                  mod_row["bdlr_BDC11"] = tx_bdlr_BDC11.Text.Trim();       // 聯絡地址
                  mod_row["bdlr_BDC12"] = tx_bdlr_BDC12.Text.Trim();       // 聯絡地址
                  mod_row["bdlr_BDC13"] = tx_bdlr_BDC13.Text.Trim();       // 聯絡地址
                  mod_row["bdlr_BDA11"] = tx_bdlr_BDA11.Text.Trim();       // 送貨地址
                  mod_row["bdlr_BDA12"] = tx_bdlr_BDA12.Text.Trim();       // 送貨地址
                  mod_row["bdlr_BDA13"] = tx_bdlr_BDA13.Text.Trim();       // 送貨地址
                  mod_row["bdlr_BDTEL"] = tx_bdlr_BDTEL.Text.Trim();       // 連絡電話
                  mod_row["bdlr_BDFAX"] = tx_bdlr_BDFAX.Text.Trim();       // 傳真號碼
                  mod_row["bdlr_BDMN2"] = tx_bdlr_BDMN2.Text.Trim();       // 連絡人　
                  mod_row["bdlr_BDMT2"] = tx_bdlr_BDMT2.Text.Trim();       // 連絡人行動
                  mod_row["bdlr_BDEML"] = tx_bdlr_BDEML.Text.Trim();       // EMAIL 
                  mod_row["bdlr_BDWWW"] = tx_bdlr_BDWWW.Text.Trim();       // 公司網址
                  mod_row["bdlr_BDNUP"] = tx_bdlr_BDNUP.Text.Trim();       // 請款客戶
                  mod_row["bdlr_BDSAL"] = tx_bdlr_BDSAL.Text.Trim();       // 負責業務
                  mod_row["bdlr_BDMCU"] = dr_bdlr_BDMCU.SelectedValue;       // 貨運公司
                  mod_row["bdlr_BDINA"] = tx_bdlr_BDINA.Text.Trim();       // 發票抬頭
                  mod_row["bdlr_BDINV"] = tx_bdlr_BDINV.Text.Trim();       // 統一編號
                  mod_row["bdlr_BDB11"] = tx_bdlr_BDB11.Text.Trim();       // 發票地址
                  mod_row["bdlr_BDB12"] = tx_bdlr_BDB12.Text.Trim();       // 發票地址
                  mod_row["bdlr_BDB13"] = tx_bdlr_BDB13.Text.Trim();       // 發票地址
                  mod_row["bdlr_BDMN1"] = tx_bdlr_BDMN1.Text.Trim();       // 負責人　
                  mod_row["bdlr_BDMT1"] = tx_bdlr_BDMT1.Text.Trim();       // 行動電話
                  mod_row["bdlr_BDD11"] = tx_bdlr_BDD11.Text.Trim();       // 帳單地址
                  mod_row["bdlr_BDD12"] = tx_bdlr_BDD12.Text.Trim();       // 帳單地址
                  mod_row["bdlr_BDD13"] = tx_bdlr_BDD13.Text.Trim();       // 帳單地址
                  mod_row["bdlr_BDMNA"] = tx_bdlr_BDMNA.Text.Trim();       // 連絡會計
                  mod_row["bdlr_BDTEA"] = tx_bdlr_BDTEA.Text.Trim();       // 會計電話
                  mod_row["bdlr_BDPR1"] = ck_bdlr_BDPR1.Checked ? "1" : "0";       // 列印名條
                  mod_row["bdlr_BDACC"] = ck_bdlr_BDACC.Checked ? "1" : "0";       // 轉換傳票
                  mod_row["bdlr_BDBK1"] = tx_bdlr_BDBK1.Text.Trim();       // 付款銀行
                  mod_row["bdlr_BDBN1"] = tx_bdlr_BDBN1.Text.Trim();       // 銀行名稱
                  mod_row["bdlr_BDBO1"] = tx_bdlr_BDBO1.Text.Trim();       // 銀行帳號
                  mod_row["bdlr_BDBK2"] = tx_bdlr_BDBK2.Text.Trim();       // 付款銀行
                  mod_row["bdlr_BDBN2"] = tx_bdlr_BDBN2.Text.Trim();       // 銀行名稱
                  mod_row["bdlr_BDBO2"] = tx_bdlr_BDBO2.Text.Trim();       // 銀行帳號
                  mod_row["bdlr_BDBK3"] = tx_bdlr_BDBK3.Text.Trim();       // 付款銀行
                  mod_row["bdlr_BDBN3"] = tx_bdlr_BDBN3.Text.Trim();       // 銀行名稱
                  mod_row["bdlr_BDBO3"] = tx_bdlr_BDBO3.Text.Trim();       // 銀行帳號
                  if (tx_bdlr_BDEND.Text.Trim() == "") { mod_row["bdlr_BDEND"] = DBNull.Value; } else { mod_row["bdlr_BDEND"] = sFN.DateStringToDateTime(tx_bdlr_BDEND.Text); }       //停止日期
                  mod_row["bdlr_BDDPT"] = tx_bdlr_BDDPT.Text.Trim();       // 部門設定
                  mod_row["bdlr_BDCLA"] = dr_bdlr_BDCLA.SelectedValue;       // 客戶等級
                  mod_row["bdlr_BDDEX"] = dr_bdlr_BDDEX.SelectedValue;       // 價格等級
                  mod_row["bdlr_BDCRD"] = tx_bdlr_BDCRD.Text.Trim();       // 信用額度
                  mod_row["bdlr_BDMLB"] = dr_bdlr_BDMLB.SelectedValue;       // 管理品牌
                  mod_row["bdlr_BDPAY"] = dr_bdlr_BDPAY.SelectedValue;       // 付款方式
                  mod_row["bdlr_BDDTL"] = dr_bdlr_BDDTL.SelectedValue;       // 資料安全
                  mod_row["bdlr_BDBCN"] = tx_bdlr_BDBCN.Text.Trim();       // 連接編號
                  mod_row["bdlr_BDCHN"] = dr_bdlr_BDCHN.SelectedValue;       // 通路類別
                  mod_row["bdlr_BDSMA"] = dr_bdlr_BDSMA.SelectedValue;       // 營業類別
                  mod_row["bdlr_BDIVY"] = dr_bdlr_BDIVY.SelectedValue;       // 發票方式
                  mod_row["bdlr_BDTXP"] = dr_bdlr_BDTXP.SelectedValue;       // 計稅方式
                  mod_row["bdlr_BDNXM"] = tx_bdlr_BDNXM.Text.Trim();       // 下月帳日
                  mod_row["bdlr_BDRMK"] = tx_bdlr_BDRMK.Text.Trim();       // 備　　註
                  mod_row["bdlr_BDNOT"] = tx_bdlr_BDNOT.Text.Trim();       // 備註說明

                  //
                  mod_row["bdlr_mkey"] = st_tempgkey;        //
                  mod_row["bdlr_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_bdlr_mod);
                  bdlrDao.Insertbalog(conn, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
                  bdlrDao.Insertbtlog(conn, thistran, "bdlr", DAC.GetStringValue(mod_row["bdlr_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["bdlr_gkey"]));
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
                  bdlrDao.Dispose();
                  tb_bdlr_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_bdlr, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_bdlrba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlrba, "bdlr_mkey", hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdlr, "bdlr_mkey", hh_mkey.Value);
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
        bdlrDao.Dispose();
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
        CmdQueryS_ba = (OleDbCommand)Session["fmbdlr_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ba.CommandText = " and 1=0 ";
      }
    }

    protected void Obj_bdlrba_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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