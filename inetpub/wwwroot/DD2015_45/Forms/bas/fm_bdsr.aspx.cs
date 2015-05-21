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
  public partial class fm_bdsr : FormBase
  {

    string st_object_func = "UNbdsr";
    //string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$";  //編輯頁面
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";  //編輯頁面
    string st_ContentPlaceHolderEdit = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$WebTabEdtRightTop$tmpl0$";
    bool bl_resetKey = true;
    bool bl_showRowA = true;
    //
    //string st_dd_apx = "UNbdsr";         //UNdcnews   與apx 相關
    //string st_dd_table = "bdlr";         //dcnews     與table 相關 
    //string st_ren_head = "";            //DC         與單號相關 
    //string st_ren_yymmtext = "";     //"         與單號相關 
    //string st_ren_cls = "bdsr";        //ren        與單號cls相關 
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
          dr_bdlr_BDCLA = sFN.DropDownListFromTable(ref dr_bdlr_BDCLA, "PDBDCLA", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDDEX = sFN.DropDownListFromTable(ref dr_bdlr_BDDEX, "PDBDDEX", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDPAY = sFN.DropDownListFromTable(ref dr_bdlr_BDPAY, "PDBDPAY", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDSPY = sFN.DropDownListFromTable(ref dr_bdlr_BDSPY, "PDBDSPY", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDDRS = sFN.DropDownListFromTable(ref dr_bdlr_BDDRS, "PDBDDRS", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDSMA = sFN.DropDownListFromTable(ref dr_bdlr_BDSMA, "PDBDSMA", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDIVY = sFN.DropDownListFromTable(ref dr_bdlr_BDIVY, "PDBDIVY", "BKNUM", "BKNAM", "", "BKNUM");
          dr_bdlr_BDCTY = sFN.DropDownListFromTable(ref dr_bdlr_BDCTY, "PDBDCTY", "BKNUM", "BKNAM", "", "BKNUM");
          //
          WebTab_form.SelectedIndex = 0;
          Set_Control_A();
          //
          tx_bdlr_BDNUM_s1.Text = "";
          tx_bdlr_BDNAM_s1.Text = "";
          //
          if (Session["fmbdsr_CmdQueryS"] == null)
          {
            act_SERS_L();
          }
          else
          {
            get_CmdQueryS();
            Obj_bdsr.TypeName = "DD2015_45.DAC_bdsr";
            Obj_bdsr.SelectMethod = "SelectTable_bdsr";
            WebDataGrid_bdsr.DataSourceID = "Obj_bdsr";
            Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
            get_CmdQueryS_ba();
            Obj_bdsrba.TypeName = "DD2015_45.DAC_bdsr";
            Obj_bdsrba.SelectMethod = "SelectTable_bdsrba";
            WebDataGrid_bdsrba.DataSourceID = "Obj_bdsrba";
            Bind_WebDataGrid_A(WebDataGrid_bdsrba, bl_showRowA, bl_resetKey); //reset gkey,mkey
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
            //Bind_WebDataGrid_A(WebDataGrid_bdsrba,bl_showRowA,!bl_resetKey);
            ShowOneRow_A(hh_fun_mkey.Value);
            sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsrba, "bdlr_mkey", hh_fun_mkey.Value);
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
        CmdQueryS = (OleDbCommand)Session["fmbdsr_CmdQueryS"];
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
        CmdQueryS.CommandText += " and a.BDTYP='F'  ";
      }

    }

    private void act_SERS_L()
    {
      try
      {
        if (Session["fmbdsr_CmdQueryS"] == null)
        {
          reset_CmdQueryS_comm();
          //
          Session["fmbdsr_CmdQueryS"] = CmdQueryS;
          Obj_bdsr.TypeName = "DD2015_45.DAC_bdsr";
          Obj_bdsr.SelectMethod = "SelectTable_bdsr";
          WebDataGrid_bdsr.DataSourceID = "Obj_bdsr";
          Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_bdsrba.TypeName = "DD2015_45.DAC_bdsr";
          Obj_bdsrba.SelectMethod = "SelectTable_bdsrba";
          WebDataGrid_bdsrba.DataSourceID = "Obj_bdsrba";
          Bind_WebDataGrid_A(WebDataGrid_bdsrba, bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
        }
        else
        {
          get_CmdQueryS();
          Session["fmbdsr_CmdQueryS"] = CmdQueryS;
          Obj_bdsr.TypeName = "DD2015_45.DAC_bdsr";
          Obj_bdsr.SelectMethod = "SelectTable_bdsr";
          WebDataGrid_bdsr.DataSourceID = "Obj_bdsr";
          Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_bdsrba.TypeName = "DD2015_45.DAC_bdsr";
          Obj_bdsrba.SelectMethod = "SelectTable_bdsrba";
          WebDataGrid_bdsrba.DataSourceID = "Obj_bdsrba";
          Bind_WebDataGrid_A(WebDataGrid_bdsrba, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
        }
      }
      catch
      {
        reset_CmdQueryS_comm();
        //
        Session["fmbdsr_CmdQueryS"] = CmdQueryS;
        Obj_bdsr.TypeName = "DD2015_45.DAC_bdsr";
        Obj_bdsr.SelectMethod = "SelectTable_bdsr";
        WebDataGrid_bdsr.DataSourceID = "Obj_bdsr";
        Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
        //
        Obj_bdsrba.TypeName = "DD2015_45.DAC_bdsr";
        Obj_bdsrba.SelectMethod = "SelectTable_bdsrba";
        WebDataGrid_bdsrba.DataSourceID = "Obj_bdsrba";
        Bind_WebDataGrid_A(WebDataGrid_bdsrba, !bl_showRowA, bl_resetKey); //reset gkey,mkey
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
      WebDataGrid_bdsr.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_bdsrba.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      //sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNbdsr", "bdlr");
      sFN.SetFormControlsText(this, PublicVariable.LangType, ApVer, "UNbdsr", "bdlr");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_bdsr, PublicVariable.LangType, ApVer, "UNbdsr", "bdlr");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_bdsrba, PublicVariable.LangType, ApVer, "UNbdsr", "bdlr");
    }

    protected void act_SERS()
    {
      reset_CmdQueryS_comm();
      //
      Session["fmbdsr_CmdQueryS"] = CmdQueryS;
      Obj_bdsr.TypeName = "DD2015_45.DAC_bdsr";
      Obj_bdsr.SelectMethod = "SelectTable_bdsr";
      WebDataGrid_bdsr.DataSourceID = "Obj_bdsr";
      Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      //
      get_CmdQueryS_ba();
      Obj_bdsrba.TypeName = "DD2015_45.DAC_bdsr";
      Obj_bdsrba.SelectMethod = "SelectTable_bdsrba";
      WebDataGrid_bdsrba.DataSourceID = "Obj_bdsrba";
      Bind_WebDataGrid_A(WebDataGrid_bdsrba, bl_showRowA, bl_resetKey); //reset gkey,mkey
    }

    protected void Obj_bdsr_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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

    protected void WebDataGrid_bdsr_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      hh_fun_name.Value = "showa";
      hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("bdlr_mkey").Value);
      WebTab_form.SelectedIndex = 1;
      //
      ShowOneRow_A(hh_fun_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsrba, "bdlr_mkey", hh_fun_mkey.Value);
      SetSerMod_A();
    }

    protected void WebDataGrid_bdsrba_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      hh_fun_name.Value = "showa";
      hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("bdlr_mkey").Value);
      WebTab_form.SelectedIndex = 1;
      //
      ShowOneRow_A(hh_fun_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsrba, "bdlr_mkey", hh_fun_mkey.Value);
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
      //
      tx_bdlr_BDNUM.Text = "";  //編　　號
      tx_bdlr_BDCJ5.Text = "";  //拼音代碼
      ck_bdlr_BDISS.Checked = false;  //客戶資料
      tx_bdlr_BDNAM.Text = "";  //名　　稱
      tx_bdlr_BDSHT.Text = "";  //簡　　稱
      tx_bdlr_BDNME.Text = "";  //英文名稱
      tx_bdlr_BDTEL.Text = "";  //連絡電話
      tx_bdlr_BDFAX.Text = "";  //傳真號碼
      tx_bdlr_BDMN2.Text = "";  //連絡人　
      tx_bdlr_BDMT2.Text = "";  //連絡人行動
      tx_bdlr_BDEML.Text = "";  //EMAIL 
      tx_bdlr_BDWWW.Text = "";  //公司網址
      tx_bdlr_BDNUP.Text = "";  //付款廠商
      tx_bdlr_BDNUPN.Text = "";  //付款廠商
      tx_bdlr_BDSAL.Text = "";  //負責業務
      tx_es010_BDSAL.Text = "";  //負責業務
      dr_bdlr_BDMCU.SelectedIndex = -1;  //貨運公司
      tx_bdlr_BDINA.Text = "";  //發票抬頭
      tx_bdlr_BDINV.Text = "";  //統一編號
      tx_bdlr_BDMN1.Text = "";  //負責人　
      tx_bdlr_BDMT1.Text = "";  //行動電話
      tx_bdlr_BDDPT.Text = "";  //部門設定
      ck_bdlr_BDACC.Checked = false;  //轉換傳票
      dr_bdlr_BDCLA.SelectedIndex = -1;  //廠商等級
      dr_bdlr_BDDEX.SelectedIndex = -1;  //價格等級
      dr_bdlr_BDPAY.SelectedIndex = -1;  //付款方式
      dr_bdlr_BDSPY.SelectedIndex = -1;  //文易方式
      dr_bdlr_BDDRS.SelectedIndex = -1;  //使用幣別
      dr_bdlr_BDSMA.SelectedIndex = -1;  //營業類別
      dr_bdlr_BDIVY.SelectedIndex = -1;  //發票方式
      dr_bdlr_BDCTY.SelectedIndex = -1;  //國別設定
      tx_bdlr_BDBCN.Text = "";  //連接編號
      tx_bdlr_BDRMK.Text = "";  //備　　註
      tx_bdlr_BDED1.Text = "";  //國外地址
      tx_bdlr_BDED2.Text = "";  //國外地址
      tx_bdlr_BDED3.Text = "";  //國外地址
      tx_bdlr_BDED4.Text = "";  //國外地址
      tx_bdlr_BDEM1.Text = "";  //國外聯絡
      tx_bdlr_BDET1.Text = "";  //國外電話
      tx_bdlr_BDEF1.Text = "";  //國外傳真
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
      ck_bdlr_BDISS.Enabled = false;  //客戶資料
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, false);   //名　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDSHT, false);   //簡　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDNME, false);   //英文名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDTEL, false);   //連絡電話
      clsGV.TextBox_Set(ref tx_bdlr_BDFAX, false);   //傳真號碼
      clsGV.TextBox_Set(ref tx_bdlr_BDMN2, false);   //連絡人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT2, false);   //連絡人行動
      clsGV.TextBox_Set(ref tx_bdlr_BDEML, false);   //EMAIL 
      clsGV.TextBox_Set(ref tx_bdlr_BDWWW, false);   //公司網址
      clsGV.TextBox_Set(ref tx_bdlr_BDNUP, false);   //付款廠商
      clsGV.TextBox_Set(ref tx_bdlr_BDNUPN, false);   //付款廠商
      clsGV.TextBox_Set(ref tx_bdlr_BDSAL, false);   //負責業務
      clsGV.TextBox_Set(ref tx_es010_BDSAL, false);   //負責業務
      clsGV.Drpdown_Set(ref dr_bdlr_BDMCU, ref tx_bdlr_BDMCU, false);   //貨運公司
      clsGV.TextBox_Set(ref tx_bdlr_BDINA, false);   //發票抬頭
      clsGV.TextBox_Set(ref tx_bdlr_BDINV, false);   //統一編號
      clsGV.TextBox_Set(ref tx_bdlr_BDMN1, false);   //負責人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT1, false);   //行動電話
      clsGV.TextBox_Set(ref tx_bdlr_BDDPT, false);   //部門設定
      ck_bdlr_BDACC.Enabled = false;  //轉換傳票
      clsGV.Drpdown_Set(ref dr_bdlr_BDCLA, ref tx_bdlr_BDCLA, false);   //廠商等級
      clsGV.Drpdown_Set(ref dr_bdlr_BDDEX, ref tx_bdlr_BDDEX, false);   //價格等級
      clsGV.Drpdown_Set(ref dr_bdlr_BDPAY, ref tx_bdlr_BDPAY, false);   //付款方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDSPY, ref tx_bdlr_BDSPY, false);   //文易方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDDRS, ref tx_bdlr_BDDRS, false);   //使用幣別
      clsGV.Drpdown_Set(ref dr_bdlr_BDSMA, ref tx_bdlr_BDSMA, false);   //營業類別
      clsGV.Drpdown_Set(ref dr_bdlr_BDIVY, ref tx_bdlr_BDIVY, false);   //發票方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDCTY, ref tx_bdlr_BDCTY, false);   //國別設定
      clsGV.TextBox_Set(ref tx_bdlr_BDBCN, false);   //連接編號
      clsGV.TextBox_Set(ref tx_bdlr_BDRMK, false);   //備　　註
      clsGV.TextBox_Set(ref tx_bdlr_BDED1, false);   //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDED2, false);   //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDED3, false);   //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDED4, false);   //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDEM1, false);   //國外聯絡
      clsGV.TextBox_Set(ref tx_bdlr_BDET1, false);   //國外電話
      clsGV.TextBox_Set(ref tx_bdlr_BDEF1, false);   //國外傳真
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
      WebDataGrid_bdsr.Enabled = true;
      WebDataGrid_bdsrba.Enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetEditMod_A()
    {
      //
      clsGV.TextBox_Set(ref tx_bdlr_BDNUM, true);  //編　　號
      clsGV.TextBox_Set(ref tx_bdlr_BDCJ5, false);  //拼音代碼
      ck_bdlr_BDISS.Enabled = true;  //客戶資料
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, true);  //名　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDSHT, true);  //簡　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDNME, true);  //英文名稱
      clsGV.TextBox_Set(ref tx_bdlr_BDTEL, true);  //連絡電話
      clsGV.TextBox_Set(ref tx_bdlr_BDFAX, true);  //傳真號碼
      clsGV.TextBox_Set(ref tx_bdlr_BDMN2, true);  //連絡人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT2, true);  //連絡人行動
      clsGV.TextBox_Set(ref tx_bdlr_BDEML, true);  //EMAIL 
      clsGV.TextBox_Set(ref tx_bdlr_BDWWW, true);  //公司網址
      clsGV.TextBox_Set(ref tx_bdlr_BDNUP, true);  //付款廠商
      clsGV.TextBox_Set(ref tx_bdlr_BDNUPN, true);  //付款廠商
      clsGV.TextBox_Set(ref tx_bdlr_BDSAL, true);  //負責業務
      clsGV.TextBox_Set(ref tx_es010_BDSAL, true);  //負責業務
      clsGV.Drpdown_Set(ref dr_bdlr_BDMCU, ref tx_bdlr_BDMCU, true);   //貨運公司
      clsGV.TextBox_Set(ref tx_bdlr_BDINA, true);  //發票抬頭
      clsGV.TextBox_Set(ref tx_bdlr_BDINV, true);  //統一編號
      clsGV.TextBox_Set(ref tx_bdlr_BDMN1, true);  //負責人　
      clsGV.TextBox_Set(ref tx_bdlr_BDMT1, true);  //行動電話
      clsGV.TextBox_Set(ref tx_bdlr_BDDPT, true);  //部門設定
      ck_bdlr_BDACC.Enabled = true;  //轉換傳票
      clsGV.Drpdown_Set(ref dr_bdlr_BDCLA, ref tx_bdlr_BDCLA, true);   //廠商等級
      clsGV.Drpdown_Set(ref dr_bdlr_BDDEX, ref tx_bdlr_BDDEX, true);   //價格等級
      clsGV.Drpdown_Set(ref dr_bdlr_BDPAY, ref tx_bdlr_BDPAY, true);   //付款方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDSPY, ref tx_bdlr_BDSPY, true);   //文易方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDDRS, ref tx_bdlr_BDDRS, true);   //使用幣別
      clsGV.Drpdown_Set(ref dr_bdlr_BDSMA, ref tx_bdlr_BDSMA, true);   //營業類別
      clsGV.Drpdown_Set(ref dr_bdlr_BDIVY, ref tx_bdlr_BDIVY, true);   //發票方式
      clsGV.Drpdown_Set(ref dr_bdlr_BDCTY, ref tx_bdlr_BDCTY, true);   //國別設定
      clsGV.TextBox_Set(ref tx_bdlr_BDBCN, true);  //連接編號
      clsGV.TextBox_Set(ref tx_bdlr_BDRMK, true);  //備　　註
      clsGV.TextBox_Set(ref tx_bdlr_BDED1, true);  //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDED2, true);  //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDED3, true);  //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDED4, true);  //國外地址
      clsGV.TextBox_Set(ref tx_bdlr_BDEM1, true);  //國外聯絡
      clsGV.TextBox_Set(ref tx_bdlr_BDET1, true);  //國外電話
      clsGV.TextBox_Set(ref tx_bdlr_BDEF1, true);  //國外傳真
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
      //bt_05.OnClientClick = " return false;";
      //
      WebDataGrid_bdsr.Enabled = false;
      WebDataGrid_bdsrba.Enabled = false;
    }

    private void ShowOneRow_A(string st_mkey)
    {
      DAC_bdsr bdsrDao = new DAC_bdsr(conn);
      DataTable tb_bdsr = new DataTable();
      OleDbCommand cmd_where = new OleDbCommand();
      //
      cmd_where.CommandText = " and a.mkey=? ";
      DAC.AddParam(cmd_where, "mkey", st_mkey);
      tb_bdsr = bdsrDao.SelectTableForTextEdit_bdsr(cmd_where);
      if (tb_bdsr.Rows.Count == 1)
      {
        BindText_A(tb_bdsr.Rows[0]);
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
      tb_bdsr.Dispose();
      bdsrDao.Dispose();
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
      tx_bdlr_BDCJ5.Text = DAC.GetStringValue(CurRow["bdlr_BDCJ5"]);  //名稱首碼
      ck_bdlr_BDISS.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDISS"]));  //客戶資料
      tx_bdlr_BDNAM.Text = DAC.GetStringValue(CurRow["bdlr_BDNAM"]);  //名　　稱
      tx_bdlr_BDSHT.Text = DAC.GetStringValue(CurRow["bdlr_BDSHT"]);  //簡　　稱
      tx_bdlr_BDNME.Text = DAC.GetStringValue(CurRow["bdlr_BDNME"]);  //英文名稱
      tx_bdlr_BDTEL.Text = DAC.GetStringValue(CurRow["bdlr_BDTEL"]);  //連絡電話
      tx_bdlr_BDFAX.Text = DAC.GetStringValue(CurRow["bdlr_BDFAX"]);  //傳真號碼
      tx_bdlr_BDMN2.Text = DAC.GetStringValue(CurRow["bdlr_BDMN2"]);  //連絡人員　
      tx_bdlr_BDMT2.Text = DAC.GetStringValue(CurRow["bdlr_BDMT2"]);  //連絡行動
      tx_bdlr_BDEML.Text = DAC.GetStringValue(CurRow["bdlr_BDEML"]);  //電子郵件
      tx_bdlr_BDWWW.Text = DAC.GetStringValue(CurRow["bdlr_BDWWW"]);  //公司網址
      tx_bdlr_BDNUP.Text = DAC.GetStringValue(CurRow["bdlr_BDNUP"]);  //付款廠商
      tx_bdlr_BDNUPN.Text = DAC.GetStringValue(CurRow["bdlr_BDNUPN"]);  //付款廠商
      tx_bdlr_BDSAL.Text = DAC.GetStringValue(CurRow["bdlr_BDSAL"]);  //負責業務
      tx_es010_BDSAL.Text = DAC.GetStringValue(CurRow["es010_BDSAL"]);  //負責業務
      dr_bdlr_BDMCU = sFN.SetDropDownList(ref dr_bdlr_BDMCU, DAC.GetStringValue(CurRow["bdlr_BDMCU"]));  //貨運公司
      tx_bdlr_BDINA.Text = DAC.GetStringValue(CurRow["bdlr_BDINA"]);  //發票抬頭
      tx_bdlr_BDINV.Text = DAC.GetStringValue(CurRow["bdlr_BDINV"]);  //統一編號
      tx_bdlr_BDMN1.Text = DAC.GetStringValue(CurRow["bdlr_BDMN1"]);  //負責人名
      tx_bdlr_BDMT1.Text = DAC.GetStringValue(CurRow["bdlr_BDMT1"]);  //行動電話
      tx_bdlr_BDDPT.Text = DAC.GetStringValue(CurRow["bdlr_BDDPT"]);  //部門設定
      ck_bdlr_BDACC.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["bdlr_BDACC"]));  //轉換傳票
      dr_bdlr_BDCLA = sFN.SetDropDownList(ref dr_bdlr_BDCLA, DAC.GetStringValue(CurRow["bdlr_BDCLA"]));  //廠商等級
      dr_bdlr_BDDEX = sFN.SetDropDownList(ref dr_bdlr_BDDEX, DAC.GetStringValue(CurRow["bdlr_BDDEX"]));  //價格等級
      dr_bdlr_BDPAY = sFN.SetDropDownList(ref dr_bdlr_BDPAY, DAC.GetStringValue(CurRow["bdlr_BDPAY"]));  //付款方式
      dr_bdlr_BDSPY = sFN.SetDropDownList(ref dr_bdlr_BDSPY, DAC.GetStringValue(CurRow["bdlr_BDSPY"]));  //文易方式
      dr_bdlr_BDDRS = sFN.SetDropDownList(ref dr_bdlr_BDDRS, DAC.GetStringValue(CurRow["bdlr_BDDRS"]));  //使用幣別
      dr_bdlr_BDSMA = sFN.SetDropDownList(ref dr_bdlr_BDSMA, DAC.GetStringValue(CurRow["bdlr_BDSMA"]));  //營業類別
      dr_bdlr_BDIVY = sFN.SetDropDownList(ref dr_bdlr_BDIVY, DAC.GetStringValue(CurRow["bdlr_BDIVY"]));  //結帳發票
      dr_bdlr_BDCTY = sFN.SetDropDownList(ref dr_bdlr_BDCTY, DAC.GetStringValue(CurRow["bdlr_BDCTY"]));  //國別設定
      tx_bdlr_BDBCN.Text = DAC.GetStringValue(CurRow["bdlr_BDBCN"]);  //連接編號
      tx_bdlr_BDRMK.Text = DAC.GetStringValue(CurRow["bdlr_BDRMK"]);  //備　　註
      tx_bdlr_BDED1.Text = DAC.GetStringValue(CurRow["bdlr_BDED1"]);  //國外地址
      tx_bdlr_BDED2.Text = DAC.GetStringValue(CurRow["bdlr_BDED2"]);  //國外地址
      tx_bdlr_BDED3.Text = DAC.GetStringValue(CurRow["bdlr_BDED3"]);  //國外地址
      tx_bdlr_BDED4.Text = DAC.GetStringValue(CurRow["bdlr_BDED4"]);  //國外地址
      tx_bdlr_BDEM1.Text = DAC.GetStringValue(CurRow["bdlr_BDEM1"]);  //國外聯絡
      tx_bdlr_BDET1.Text = DAC.GetStringValue(CurRow["bdlr_BDET1"]);  //國外電話
      tx_bdlr_BDEF1.Text = DAC.GetStringValue(CurRow["bdlr_BDEF1"]);  //國外傳真
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
      Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, !bl_resetKey);
      Bind_WebDataGrid_A(WebDataGrid_bdsrba, !bl_showRowA, !bl_resetKey);
      //
      ShowOneRow_A(hh_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsrba, "bdlr_mkey", hh_mkey.Value);
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
        string st_nextkey = sFN.WebDataGrid_NextKey(WebDataGrid_bdsrba, "bdlr_mkey", hh_mkey.Value);
        //
        DAC bdsrDao = new DAC_bdsr(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "' and bdlr_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_bdsr = new DataTable();
        OleDbConnection connD = new OleDbConnection();
        connD = DAC.NewReaderConnr();
        connD.Open();
        DbDataAdapter da_ADP = bdsrDao.GetDataAdapter(ApVer, "UNbdsr", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "", "SEL DEL ");
        da_ADP.SelectCommand.Connection = connD;
        da_ADP.DeleteCommand.Connection = connD;
        da_ADP.Fill(tb_bdsr);
        DataRow[] DelRow = tb_bdsr.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          OleDbTransaction thistran = connD.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            bdsrDao.Insertbalog(connD, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
            bdsrDao.Insertbtlog(connD, thistran, "bdlr", DAC.GetStringValue(DelRow[0]["bdlr_BDNUM"]), "D", UserName, DAC.GetStringValue(DelRow[0]["bdlr_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_bdsr);
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
        bdsrDao.Dispose();
        tb_bdsr.Dispose();
        da_ADP.Dispose();
        //
        if (bl_delok)
        {
          hh_mkey.Value = st_nextkey;
          act_SERS_L();
          SetSerMod_A();
          ShowOneRow_A(hh_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsrba, "bdlr_mkey", hh_mkey.Value);
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
        DAC_bdsr bdsrDao = new DAC_bdsr(conn);
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
            if (bdsrDao.IsExists("bdlr", "BDNUM", tx_bdlr_BDNUM.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已存在.");
              //bdsrDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_bdlr_BDNUM.Text);
              //st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已重新取號.");
              //tx_bdlr_BDNUM.Text = bdsrDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_bdlr_BDNUM.Text ="";
            }
            else
            {
              conn.Open();
              DataTable tb_bdsr_ins = new DataTable();
              DbDataAdapter da_ADP_ins = bdsrDao.GetDataAdapter(ApVer, "UNbdsr", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_bdsr_ins);
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_bdsr_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["bdlr_gkey"] = st_tempgkey;    // 
                ins_row["bdlr_mkey"] = st_tempgkey;    //
                //
                ins_row["bdlr_BDNUM"] = tx_bdlr_BDNUM.Text.Trim();       // 編　　號
                ins_row["bdlr_BDCJ5"] = tx_bdlr_BDCJ5.Text.Trim();       // 拼音代碼
                ins_row["bdlr_BDISS"] = ck_bdlr_BDISS.Checked ? "1" : "0";       // 客戶資料
                ins_row["bdlr_BDISF"] = "0";       // 廠商資料
                ins_row["bdlr_BDISH"] = "0";       // 直營門市
                ins_row["bdlr_BDISP"] = "0";       // 分倉資料
                ins_row["bdlr_BDISD"] = "0";       // 百貨專櫃
                ins_row["bdlr_BDNAM"] = tx_bdlr_BDNAM.Text.Trim();       // 名　　稱
                ins_row["bdlr_BDSHT"] = tx_bdlr_BDSHT.Text.Trim();       // 簡　　稱
                ins_row["bdlr_BDNME"] = tx_bdlr_BDNME.Text.Trim();       // 英文名稱
                ins_row["bdlr_BDTEL"] = tx_bdlr_BDTEL.Text.Trim();       // 連絡電話
                ins_row["bdlr_BDFAX"] = tx_bdlr_BDFAX.Text.Trim();       // 傳真號碼
                ins_row["bdlr_BDMN2"] = tx_bdlr_BDMN2.Text.Trim();       // 連絡人　
                ins_row["bdlr_BDMT2"] = tx_bdlr_BDMT2.Text.Trim();       // 連絡人行動
                ins_row["bdlr_BDEML"] = tx_bdlr_BDEML.Text.Trim();       // EMAIL 
                ins_row["bdlr_BDWWW"] = tx_bdlr_BDWWW.Text.Trim();       // 公司網址
                ins_row["bdlr_BDNUP"] = tx_bdlr_BDNUP.Text.Trim();       // 付款廠商
                ins_row["bdlr_BDSAL"] = tx_bdlr_BDSAL.Text.Trim();       // 負責業務
                ins_row["bdlr_BDMCU"] = dr_bdlr_BDMCU.SelectedValue;       // 貨運公司
                ins_row["bdlr_BDINA"] = tx_bdlr_BDINA.Text.Trim();       // 發票抬頭
                ins_row["bdlr_BDINV"] = tx_bdlr_BDINV.Text.Trim();       // 統一編號
                ins_row["bdlr_BDMN1"] = tx_bdlr_BDMN1.Text.Trim();       // 負責人　
                ins_row["bdlr_BDMT1"] = tx_bdlr_BDMT1.Text.Trim();       // 行動電話
                ins_row["bdlr_BDDPT"] = tx_bdlr_BDDPT.Text.Trim();       // 部門設定
                ins_row["bdlr_BDACC"] = ck_bdlr_BDACC.Checked ? "1" : "0";       // 轉換傳票
                ins_row["bdlr_BDCLA"] = dr_bdlr_BDCLA.SelectedValue;       // 廠商等級
                ins_row["bdlr_BDDEX"] = dr_bdlr_BDDEX.SelectedValue;       // 價格等級
                ins_row["bdlr_BDPAY"] = dr_bdlr_BDPAY.SelectedValue;       // 付款方式
                ins_row["bdlr_BDSPY"] = dr_bdlr_BDSPY.SelectedValue;       // 文易方式
                ins_row["bdlr_BDDRS"] = dr_bdlr_BDDRS.SelectedValue;       // 使用幣別
                ins_row["bdlr_BDSMA"] = dr_bdlr_BDSMA.SelectedValue;       // 營業類別
                ins_row["bdlr_BDIVY"] = dr_bdlr_BDIVY.SelectedValue;       // 發票方式
                ins_row["bdlr_BDCTY"] = dr_bdlr_BDCTY.SelectedValue;       // 國別設定
                ins_row["bdlr_BDBCN"] = tx_bdlr_BDBCN.Text.Trim();       // 連接編號
                ins_row["bdlr_BDRMK"] = tx_bdlr_BDRMK.Text.Trim();       // 備　　註
                ins_row["bdlr_BDED1"] = tx_bdlr_BDED1.Text.Trim();       // 國外地址
                ins_row["bdlr_BDED2"] = tx_bdlr_BDED2.Text.Trim();       // 國外地址
                ins_row["bdlr_BDED3"] = tx_bdlr_BDED3.Text.Trim();       // 國外地址
                ins_row["bdlr_BDED4"] = tx_bdlr_BDED4.Text.Trim();       // 國外地址
                ins_row["bdlr_BDEM1"] = tx_bdlr_BDEM1.Text.Trim();       // 國外聯絡
                ins_row["bdlr_BDET1"] = tx_bdlr_BDET1.Text.Trim();       // 國外電話
                ins_row["bdlr_BDEF1"] = tx_bdlr_BDEF1.Text.Trim();       // 國外傳真
                ins_row["bdlr_BDNOT"] = tx_bdlr_BDNOT.Text.Trim();       // 備註說明
                //
                ins_row["bdlr_BDTYP"] = "F";       //  
                ins_row["bdlr_BDMLB"] = "_";       // 管理品牌
                ins_row["bdlr_BDCRD"] = 0;         // 信用额度
                //
                ins_row["bdlr_trusr"] = UserGkey;  //
                tb_bdsr_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_bdsr_ins);
                //bdsrDao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_bdlr_BDNUM.Text.Trim());
                bdsrDao.Insertbalog(conn, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
                bdsrDao.Insertbtlog(conn, thistran, "bdlr", DAC.GetStringValue(ins_row["bdlr_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["bdlr_gkey"]));
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
                bdsrDao.Dispose();
                tb_bdsr_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_bdsrba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsr, "bdlr_mkey", hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsrba, "bdlr_mkey", hh_mkey.Value);
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
            if (bdsrDao.IsExists("bdlr", "BDNUM", tx_bdlr_BDNUM.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_bdlr_BDNUM.Text + ",已存在.");
            }
            else
            {
              DataTable tb_bdsr_mod = new DataTable();
              DbDataAdapter da_ADP_mod = bdsrDao.GetDataAdapter(ApVer, "UNbdsr", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_bdsr_mod);
              st_SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "' and bdlr_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_bdsr_mod.Select(st_SelDataKey);
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
                  mod_row["bdlr_BDISS"] = ck_bdlr_BDISS.Checked ? "1" : "0";       // 客戶資料
                  mod_row["bdlr_BDNAM"] = tx_bdlr_BDNAM.Text.Trim();       // 名　　稱
                  mod_row["bdlr_BDSHT"] = tx_bdlr_BDSHT.Text.Trim();       // 簡　　稱
                  mod_row["bdlr_BDNME"] = tx_bdlr_BDNME.Text.Trim();       // 英文名稱
                  mod_row["bdlr_BDTEL"] = tx_bdlr_BDTEL.Text.Trim();       // 連絡電話
                  mod_row["bdlr_BDFAX"] = tx_bdlr_BDFAX.Text.Trim();       // 傳真號碼
                  mod_row["bdlr_BDMN2"] = tx_bdlr_BDMN2.Text.Trim();       // 連絡人　
                  mod_row["bdlr_BDMT2"] = tx_bdlr_BDMT2.Text.Trim();       // 連絡人行動
                  mod_row["bdlr_BDEML"] = tx_bdlr_BDEML.Text.Trim();       // EMAIL 
                  mod_row["bdlr_BDWWW"] = tx_bdlr_BDWWW.Text.Trim();       // 公司網址
                  mod_row["bdlr_BDNUP"] = tx_bdlr_BDNUP.Text.Trim();       // 付款廠商
                  mod_row["bdlr_BDSAL"] = tx_bdlr_BDSAL.Text.Trim();       // 負責業務
                  mod_row["bdlr_BDMCU"] = dr_bdlr_BDMCU.SelectedValue;       // 貨運公司
                  mod_row["bdlr_BDINA"] = tx_bdlr_BDINA.Text.Trim();       // 發票抬頭
                  mod_row["bdlr_BDINV"] = tx_bdlr_BDINV.Text.Trim();       // 統一編號
                  mod_row["bdlr_BDMN1"] = tx_bdlr_BDMN1.Text.Trim();       // 負責人　
                  mod_row["bdlr_BDMT1"] = tx_bdlr_BDMT1.Text.Trim();       // 行動電話
                  mod_row["bdlr_BDDPT"] = tx_bdlr_BDDPT.Text.Trim();       // 部門設定
                  mod_row["bdlr_BDACC"] = ck_bdlr_BDACC.Checked ? "1" : "0";       // 轉換傳票
                  mod_row["bdlr_BDCLA"] = dr_bdlr_BDCLA.SelectedValue;       // 廠商等級
                  mod_row["bdlr_BDDEX"] = dr_bdlr_BDDEX.SelectedValue;       // 價格等級
                  mod_row["bdlr_BDPAY"] = dr_bdlr_BDPAY.SelectedValue;       // 付款方式
                  mod_row["bdlr_BDSPY"] = dr_bdlr_BDSPY.SelectedValue;       // 文易方式
                  mod_row["bdlr_BDDRS"] = dr_bdlr_BDDRS.SelectedValue;       // 使用幣別
                  mod_row["bdlr_BDSMA"] = dr_bdlr_BDSMA.SelectedValue;       // 營業類別
                  mod_row["bdlr_BDIVY"] = dr_bdlr_BDIVY.SelectedValue;       // 發票方式
                  mod_row["bdlr_BDCTY"] = dr_bdlr_BDCTY.SelectedValue;       // 國別設定
                  mod_row["bdlr_BDBCN"] = tx_bdlr_BDBCN.Text.Trim();       // 連接編號
                  mod_row["bdlr_BDRMK"] = tx_bdlr_BDRMK.Text.Trim();       // 備　　註
                  mod_row["bdlr_BDED1"] = tx_bdlr_BDED1.Text.Trim();       // 國外地址
                  mod_row["bdlr_BDED2"] = tx_bdlr_BDED2.Text.Trim();       // 國外地址
                  mod_row["bdlr_BDED3"] = tx_bdlr_BDED3.Text.Trim();       // 國外地址
                  mod_row["bdlr_BDED4"] = tx_bdlr_BDED4.Text.Trim();       // 國外地址
                  mod_row["bdlr_BDEM1"] = tx_bdlr_BDEM1.Text.Trim();       // 國外聯絡
                  mod_row["bdlr_BDET1"] = tx_bdlr_BDET1.Text.Trim();       // 國外電話
                  mod_row["bdlr_BDEF1"] = tx_bdlr_BDEF1.Text.Trim();       // 國外傳真
                  mod_row["bdlr_BDNOT"] = tx_bdlr_BDNOT.Text.Trim();       // 備註說明
                  //
                  mod_row["bdlr_mkey"] = st_tempgkey;        //
                  mod_row["bdlr_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_bdsr_mod);
                  bdsrDao.Insertbalog(conn, thistran, "bdlr", hh_ActKey.Value, hh_GridGkey.Value);
                  bdsrDao.Insertbtlog(conn, thistran, "bdlr", DAC.GetStringValue(mod_row["bdlr_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["bdlr_gkey"]));
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
                  bdsrDao.Dispose();
                  tb_bdsr_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_bdsr, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_bdsrba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsrba, "bdlr_mkey", hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_bdsr, "bdlr_mkey", hh_mkey.Value);
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
        bdsrDao.Dispose();
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
        CmdQueryS_ba = (OleDbCommand)Session["fmbdsr_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ba.CommandText = " and 1=0 ";
      }
    }

    protected void Obj_bdsrba_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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