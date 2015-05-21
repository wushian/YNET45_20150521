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

namespace DD2015_45.Forms.rix
{
  public partial class fm_rcnta : FormBase
  {
    public string st_object_func = "UNrcnta";
    public string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    public string st_ContentPlaceHolderEdit = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$";
    bool bl_resetKey = true;
    bool bl_showRowA = true;
    //
    string st_dd_apx = "UNrcnta";         //UNdcnews   與apx 相關
    //string st_dd_table = "rcnta";         //dcnews     與table 相關 
    string st_ren_head = "CA";            //DC         與單號相關 
    string st_ren_yymmtext = "YYYYMM";     //"         與單號相關 
    string st_ren_cls = "rcnta";        //ren        與單號cls相關 
    string st_ren_cos = "1";        //1          與單號cos相關 
    int in_ren_len = 4;            //6          與單號流水號 
    //
    private OleDbCommand CmdQueryS_ba = new OleDbCommand();
    private OleDbCommand CmdQueryS_b = new OleDbCommand();
    private OleDbCommand CmdQueryS_b1 = new OleDbCommand();
    //
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
          dr_rcnta_BDTXP = sFN.DropDownListFromClasses(ref dr_rcnta_BDTXP, "UNtax_TXP", "class_text", "class_value");
          Set_DataSource();
          //
          WebTab_form.SelectedIndex = 0;
          Set_Control_A();
          //
          tx_rcnta_BDDTS_s1.Value = DateTime.Today.AddYears(-10);
          tx_rcnta_BDDTS_s2.Value = DateTime.Today;
          tx_rcnta_BDPTN_s.Text = "";
          tx_bpud_BPTNA_s.Text = "";
          //
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_rcntd, false);
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_rcntr, false);
          this.Form.Attributes.Add("onkeydown", "do_Keydown_EnterToTab();");    //SiteMM.Master中輸入Enter轉Tab
          //this.Form.Attributes.Add("onkeydown", "return do_Keydown_CancelEnter();"); //SiteMM.Master中輸入Enter取消
          SetSerMod_B();
          //
          if (Session["fmrcnta_CmdQueryS"] == null)
          {
            act_SERS_L();
          }
          else
          {
            get_CmdQueryS();
            //Obj_rcnta.TypeName = "DD2015_45.DAC_rcnta";
            //Obj_rcnta.SelectMethod = "SelectTable_rcnta";
            //WebDataGrid_rcnta.DataSourceID = "Obj_rcnta";
            Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
            get_CmdQueryS_ba();
            //Obj_rcnta_ba.TypeName = "DD2015_45.DAC_rcnta";
            //Obj_rcnta_ba.SelectMethod = "SelectTable_rcnta_ba";
            //WebDataGrid_rcnta_ba.DataSourceID = "Obj_rcnta_ba";
            Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
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
      string errorMessage = "";
      if (Request[Page.postEventSourceID] == "ContentPlaceHolder1_btnAction")
      {
        string[] arguments = Request[Page.postEventArgumentID].Split('&');
        if (arguments.Length > 0) hh_fun_name.Value = DAC.GetStringValue(arguments[0]);
        if (arguments.Length > 1) hh_fun_mkey.Value = DAC.GetStringValue(arguments[1]);
        //
        if (hh_fun_name.Value.ToLower() == "showa")
        {
          WebTab_form.SelectedIndex = 1;
          WebTabGrid.SelectedIndex = 0;
          ShowOneRow_A(hh_fun_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_rcnta_ba, "rcnta_mkey", hh_fun_mkey.Value);
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

    //
    private void Set_DataSource()
    {
      Obj_rcnta.TypeName = "DD2015_45.DAC_rcnta";
      Obj_rcnta.SelectMethod = "SelectTable_rcnta";
      WebDataGrid_rcnta.DataSourceID = "Obj_rcnta";

      Obj_rcnta_ba.TypeName = "DD2015_45.DAC_rcnta";
      Obj_rcnta_ba.SelectMethod = "SelectTable_rcnta_ba";
      WebDataGrid_rcnta_ba.DataSourceID = "Obj_rcnta_ba";

      Obj_rcntd.TypeName = "DD2015_45.DAC_rcntd";
      Obj_rcntd.SelectMethod = "SelectTable_rcntd";
      Obj_rcntd.UpdateMethod = "UpdateTable_rcntd";
      Obj_rcntd.InsertMethod = "InsertTable_rcntd";
      Obj_rcntd.DeleteMethod = "DeleteTable_rcntd";
      WebDataGrid_rcntd.DataSourceID = "Obj_rcntd";

      Obj_rcntr.TypeName = "DD2015_45.DAC_rcntr";
      Obj_rcntr.SelectMethod = "SelectTable_rcntr";
      Obj_rcntr.UpdateMethod = "UpdateTable_rcntr";
      Obj_rcntr.InsertMethod = "InsertTable_rcntr";
      Obj_rcntr.DeleteMethod = "DeleteTable_rcntr";
      WebDataGrid_rcntr.DataSourceID = "Obj_rcntr";
    }


    private void get_CmdQueryS()
    {
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmrcnta_CmdQueryS"];
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
      if ((tx_rcnta_BDDTS_s1.Text != "") && (tx_rcnta_BDDTS_s2.Text != ""))
      {
        CmdQueryS.CommandText += " and a.BDDTS >=?  and a.BDDTS <=? ";
        DAC.AddParam(CmdQueryS, "rcnta_BDDTS_s1", tx_rcnta_BDDTS_s1.Date);
        DAC.AddParam(CmdQueryS, "rcnta_BDDTS_s2", tx_rcnta_BDDTS_s2.Date);
      }
      else if (tx_rcnta_BDDTS_s1.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDDTS >=?  ";
        DAC.AddParam(CmdQueryS, "rcnta_BDDTS_s1", tx_rcnta_BDDTS_s1.Date);
      }
      else if (tx_rcnta_BDDTS_s2.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDDTS <=?  ";
        DAC.AddParam(CmdQueryS, "rcnta_BDDTS_s2", tx_rcnta_BDDTS_s2.Date);
      }
      //
      if (tx_rcnta_BDPTN_s.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDPTN like ?  ";
        DAC.AddParam(CmdQueryS, "rcnta_BDPTN_s", tx_rcnta_BDPTN_s.Text + "%");
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
        if (Session["fmrcnta_CmdQueryS"] == null)
        {
          reset_CmdQueryS_comm();
          //
          Session["fmrcnta_CmdQueryS"] = CmdQueryS;
          //Obj_rcnta.TypeName = "DD2015_45.DAC_rcnta";
          //Obj_rcnta.SelectMethod = "SelectTable_rcnta";
          //WebDataGrid_rcnta.DataSourceID = "Obj_rcnta";
          Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
          get_CmdQueryS_ba();
          //Obj_rcnta_ba.TypeName = "DD2015_45.DAC_rcnta";
          //Obj_rcnta_ba.SelectMethod = "SelectTable_rcnta_ba";
          //WebDataGrid_rcnta_ba.DataSourceID = "Obj_rcnta_ba";
          Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
        }
        else
        {
          get_CmdQueryS();
          Session["fmrcnta_CmdQueryS"] = CmdQueryS;
          //Obj_rcnta.TypeName = "DD2015_45.DAC_rcnta";
          //Obj_rcnta.SelectMethod = "SelectTable_rcnta";
          //WebDataGrid_rcnta.DataSourceID = "Obj_rcnta";
          Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
          get_CmdQueryS_ba();
          //Obj_rcnta_ba.TypeName = "DD2015_45.DAC_rcnta";
          //Obj_rcnta_ba.SelectMethod = "SelectTable_rcnta_ba";
          //WebDataGrid_rcnta_ba.DataSourceID = "Obj_rcnta_ba";
          Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
        }
      }
      catch
      {
        reset_CmdQueryS_comm();
        //
        Session["fmrcnta_CmdQueryS"] = CmdQueryS;
        //Obj_rcnta.TypeName = "DD2015_45.DAC_rcnta";
        //Obj_rcnta.SelectMethod = "SelectTable_rcnta";
        //WebDataGrid_rcnta.DataSourceID = "Obj_rcnta";
        Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, bl_resetKey); //reset gkey,mkey
        //
        //Obj_rcnta_ba.TypeName = "DD2015_45.DAC_rcnta";
        //Obj_rcnta_ba.SelectMethod = "SelectTable_rcnta_ba";
        //WebDataGrid_rcnta_ba.DataSourceID = "Obj_rcnta_ba";
        Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      }
      //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="WebDataGrid"></param>
    private void Bind_WebDataGrid_A(Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, bool bl_showRow, bool bl_resetkey)
    {
      WebDataGrid.Rows.Clear();
      WebDataGrid.DataBind();
      //
      if (bl_resetkey)
      {
        if (WebDataGrid.Rows.Count > 0)
        {
          hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rcnta_gkey");
          hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rcnta_mkey");
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
      WebDataGrid_rcnta.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_rcnta_ba.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_rcntr.Behaviors.Paging.PageSize = 5;
      WebDataGrid_rcntd.Behaviors.Paging.PageSize = 5;
      //
      this.Page.Title = FunctionName;
      //sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNrcnta", "rcnta");
      //sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolderEdit, ApVer, "UNrcnta", "rcnta");
      sFN.SetFormControlsText(this.Form, PublicVariable.LangType, ApVer, "UNrcnta", "rcnta");
      //
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rcnta, PublicVariable.LangType, ApVer, "UNrcnta", "rcnta");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rcnta_ba, PublicVariable.LangType, ApVer, "UNrcnta", "rcnta");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rcntd, PublicVariable.LangType, ApVer, "UNrcntd", "rcntd");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rcntr, PublicVariable.LangType, ApVer, "UNrcntr", "rcntr");
    }

    protected void act_SERS()
    {
      reset_CmdQueryS_comm();
      //
      Session["fmrcnta_CmdQueryS"] = CmdQueryS;
      //Obj_rcnta.TypeName = "DD2015_45.DAC_rcnta";
      //Obj_rcnta.SelectMethod = "SelectTable_rcnta";
      //WebDataGrid_rcnta.DataSourceID = "Obj_rcnta";
      Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      //
      get_CmdQueryS_ba();
      //Obj_rcnta_ba.TypeName = "DD2015_45.DAC_rcnta";
      //Obj_rcnta_ba.SelectMethod = "SelectTable_rcnta_ba";
      //WebDataGrid_rcnta_ba.DataSourceID = "Obj_rcnta_ba";
      Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
    }

    protected void Obj_rcnta_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
        e.InputParameters["st_orderKey"] = " A.BDDTS DESC ";
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


    ///
    /// 
    /// 
    /// 
    /// 
    private void ClearText_A()
    {
      tx_rcnta_BDCCN.Text = "";  //合約編號
      tx_rcnta_BDDTS.Text = "";  //開始日期
      tx_rcnta_BDDTE.Text = "";  //結束日期
      tx_rcnta_BDNUM.Text = "";  //出版社號
      tx_bdlr_BDNAM.Text = "";  //出版社名
      tx_rcnta_BDPTN.Text = "";  //商品編號
      tx_bpud_BPTNA.Text = "";  //商品名稱
      tx_rcnta_BDQTY.Value = 0;  //預付本數
      tx_rcnta_BDDE1.Value = 0;  //定價金額
      tx_rcnta_BDRAT.Value = 0;  //預付比率
      dr_rcnta_BDTXP.SelectedIndex = -1;  //計稅方式
      //tx_rcnta_BDAMT.Value = 0;  //預付金額
      //tx_rcnta_BDEDT.Text = "";  //預付月份
      tx_rcnta_BDRMK.Text = "";  //備　　註
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetSerMod_A()
    {
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDCCN, ref tx_rcnta_BDCCN, false);  //合約編號
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDDTS, ref tx_rcnta_BDDTS, false);  //開始日期
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDNUM, ref tx_rcnta_BDNUM, false);  //出版社號
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDPTN, ref tx_rcnta_BDPTN, false);  //商品編號
      //clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDEDT, ref tx_rcnta_BDEDT, false);  //預付月份
      //
      clsGV.TextBox_Set(ref tx_rcnta_BDCCN, false);   //合約編號
      clsGV.TextBox_Set(ref tx_rcnta_BDDTS, false);   //開始日期
      clsGV.TextBox_Set(ref tx_rcnta_BDDTE, false);   //結束日期
      clsGV.TextBox_Set(ref tx_rcnta_BDNUM, false);   //出版社號
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, false);   //出版社名
      clsGV.TextBox_Set(ref tx_rcnta_BDPTN, false);   //商品編號
      clsGV.TextBox_Set(ref tx_bpud_BPTNA, false);   //商品名稱
      clsGV.TextBox_Set(ref tx_rcnta_BDQTY, false);   //預付本數
      clsGV.TextBox_Set(ref tx_rcnta_BDDE1, false);   //定價金額
      clsGV.TextBox_Set(ref tx_rcnta_BDRAT, false);   //預付比率
      clsGV.Drpdown_Set(ref dr_rcnta_BDTXP, ref tx_rcnta_BDTXP, false);   //計稅方式
      //clsGV.TextBox_Set(ref tx_rcnta_BDAMT, false);   //預付金額
      //clsGV.TextBox_Set(ref tx_rcnta_BDEDT, false);   //預付月份
      clsGV.TextBox_Set(ref tx_rcnta_BDRMK, false);   //備　　註
      //
      clsGV.SetControlShowAlert(ref lb_rcnta_BDCCN, ref tx_rcnta_BDCCN, true);  // 合約編號
      clsGV.SetControlShowAlert(ref lb_rcnta_BDDTS, ref tx_rcnta_BDDTS, true);  // 開始日期
      clsGV.SetControlShowAlert(ref lb_rcnta_BDNUM, ref tx_rcnta_BDNUM, true);  // 出版社號
      clsGV.SetControlShowAlert(ref lb_rcnta_BDPTN, ref tx_rcnta_BDPTN, true);  // 商品編號
      //clsGV.SetControlShowAlert(ref lb_rcnta_BDEDT, ref tx_rcnta_BDEDT, true);  // 預付月份
      //
      tx_rcnta_BDNUM.Attributes.Remove("onblur");
      tx_rcnta_BDPTN.Attributes.Remove("onblur");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      SetSerMod_B();
      PanEdtRightDown.Enabled = true;
      //
      WebDataGrid_rcnta.Enabled = true;
      WebDataGrid_rcnta_ba.Enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetEditMod_A()
    {
      //
      clsGV.TextBox_Set(ref tx_rcnta_BDCCN, true);  //合約編號
      clsGV.TextBox_Set(ref tx_rcnta_BDDTS, true);  //開始日期
      clsGV.TextBox_Set(ref tx_rcnta_BDDTE, true);  //結束日期
      clsGV.TextBox_Set(ref tx_rcnta_BDNUM, true);  //出版社號
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, false);  //出版社名
      clsGV.TextBox_Set(ref tx_rcnta_BDPTN, true);  //商品編號
      clsGV.TextBox_Set(ref tx_bpud_BPTNA, false);  //商品名稱
      clsGV.TextBox_Set(ref tx_rcnta_BDQTY, true);  //預付本數
      clsGV.TextBox_Set(ref tx_rcnta_BDDE1, true);  //定價金額
      clsGV.TextBox_Set(ref tx_rcnta_BDRAT, true);  //預付比率
      clsGV.Drpdown_Set(ref dr_rcnta_BDTXP, ref tx_rcnta_BDTXP, true);   //計稅方式
      //clsGV.TextBox_Set(ref tx_rcnta_BDAMT, true);  //預付金額
      //clsGV.TextBox_Set(ref tx_rcnta_BDEDT, true);  //預付月份
      clsGV.TextBox_Set(ref tx_rcnta_BDRMK, true);  //備　　註
      //
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDCCN, ref tx_rcnta_BDCCN, true);  // 合約編號
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDDTS, ref tx_rcnta_BDDTS, true);  // 開始日期
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDNUM, ref tx_rcnta_BDNUM, true);  // 出版社號
      clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDPTN, ref tx_rcnta_BDPTN, true);  // 商品編號
      //clsGV.SetTextBoxEditAlert(ref lb_rcnta_BDEDT, ref tx_rcnta_BDEDT, true);  // 預付月份
      //
      tx_rcnta_BDNUM.Attributes.Add("onblur", "return  get_bdlr_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_rcnta_BDNUM','" + st_ContentPlaceHolderEdit + "tx_bdlr_BDNAM'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bdlr.aspx" + "','" + "廠商資料" + "')");
      tx_rcnta_BDPTN.Attributes.Add("onblur", "return  get_bpud_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_rcnta_BDPTN','" + st_ContentPlaceHolderEdit + "tx_bpud_BPTNA'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bpud.aspx" + "','" + "商品資料" + "')");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      SetSerMod_B();
      PanEdtRightDown.Enabled = false;      //
      WebDataGrid_rcnta.Enabled = false;
      WebDataGrid_rcnta_ba.Enabled = false;
    }

    private void ShowOneRow_A(string st_mkey)
    {
      DAC_rcnta rcntaDao = new DAC_rcnta(conn);
      DataTable tb_rcnta = new DataTable();
      OleDbCommand cmd_where = new OleDbCommand();
      string st_ren = "";
      //
      cmd_where.CommandText = " and a.mkey=? ";
      DAC.AddParam(cmd_where, "mkey", st_mkey);
      tb_rcnta = rcntaDao.SelectTableForTextEdit_rcnta(cmd_where);
      if (tb_rcnta.Rows.Count == 1)
      {
        BindText_A(tb_rcnta.Rows[0]);
        //bt_05.OnClientClick = "return btnDEL_c()";
        //bt_04.OnClientClick = "return btnMOD_c()";
        //rcntd 
        st_ren = sFN.GetRenFromGkey("rcnta", "BDCCN", "mkey", st_mkey);
        get_CmdQueryS_b(st_ren);
        //Obj_rcntd.TypeName = "DD2015_45.DAC_rcntd";
        //Obj_rcntd.SelectMethod = "SelectTable_rcntd";
        //Obj_rcntd.UpdateMethod = "UpdateTable_rcntd";
        //Obj_rcntd.InsertMethod = "InsertTable_rcntd";
        //Obj_rcntd.DeleteMethod = "DeleteTable_rcntd";
        //WebDataGrid_rcntd.DataSourceID = "Obj_rcntd";
        Bind_WebDataGrid_B(WebDataGrid_rcntd, bl_resetKey);
        //rcntr
        st_ren = sFN.GetRenFromGkey("rcnta", "BDCCN", "mkey", st_mkey);
        get_CmdQueryS_b1(st_ren);
        //Obj_rcntr.TypeName = "DD2015_45.DAC_rcntr";
        //Obj_rcntr.SelectMethod = "SelectTable_rcntr";
        //Obj_rcntr.UpdateMethod = "UpdateTable_rcntr";
        //Obj_rcntr.InsertMethod = "InsertTable_rcntr";
        //Obj_rcntr.DeleteMethod = "DeleteTable_rcntr";
        //WebDataGrid_rcntr.DataSourceID = "Obj_rcntr";
        Bind_WebDataGrid_B(WebDataGrid_rcntr, bl_resetKey);
      }
      else
      {
        ClearText_A();
        //bt_05.OnClientClick = "return btnDEL0_c()";
        //bt_04.OnClientClick = "return btnMOD0_c()";
      }
      cmd_where.Dispose();
      tb_rcnta.Dispose();
      rcntaDao.Dispose();
      //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CurRow"></param>
    private void BindText_A(DataRow CurRow)
    {
      //
      tx_rcnta_BDCCN.Text = DAC.GetStringValue(CurRow["rcnta_BDCCN"]);  //合約編號
      if (CurRow["rcnta_BDDTS"] == DBNull.Value) { tx_rcnta_BDDTS.Text = ""; } else { tx_rcnta_BDDTS.Text = DAC.GetDateTimeValue(CurRow["rcnta_BDDTS"]).ToString(sys_DateFormat); }  //開始日期
      if (CurRow["rcnta_BDDTE"] == DBNull.Value) { tx_rcnta_BDDTE.Text = ""; } else { tx_rcnta_BDDTE.Text = DAC.GetDateTimeValue(CurRow["rcnta_BDDTE"]).ToString(sys_DateFormat); }  //結束日期
      tx_rcnta_BDNUM.Text = DAC.GetStringValue(CurRow["rcnta_BDNUM"]);  //出版社號
      tx_bdlr_BDNAM.Text = DAC.GetStringValue(CurRow["bdlr_BDNAM"]);  //出版社名
      tx_rcnta_BDPTN.Text = DAC.GetStringValue(CurRow["rcnta_BDPTN"]);  //商品編號
      tx_bpud_BPTNA.Text = DAC.GetStringValue(CurRow["bpud_BPTNA"]);  //商品名稱
      tx_rcnta_BDQTY.Text = DAC.GetStringValue(CurRow["rcnta_BDQTY"]);  //預付本數
      tx_rcnta_BDDE1.Text = DAC.GetStringValue(CurRow["rcnta_BDDE1"]);  //定價金額
      tx_rcnta_BDRAT.Text = DAC.GetStringValue(CurRow["rcnta_BDRAT"]);  //預付比率
      dr_rcnta_BDTXP = sFN.SetDropDownList(ref dr_rcnta_BDTXP, DAC.GetStringValue(CurRow["rcnta_BDTXP"]));  //計稅方式
      //tx_rcnta_BDAMT.Text = DAC.GetStringValue(CurRow["rcnta_BDAMT"]);  //預付金額
      //if (CurRow["rcnta_BDEDT"] == DBNull.Value) { tx_rcnta_BDEDT.Text = ""; } else { tx_rcnta_BDEDT.Text = DAC.GetDateTimeValue(CurRow["rcnta_BDEDT"]).ToString(sys_DateFormat); }  //預付月份
      tx_rcnta_BDRMK.Text = DAC.GetStringValue(CurRow["rcnta_BDRMK"]);  //備　　註
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["rcnta_mkey"]);
      hh_GridGkey.Value = DAC.GetStringValue(CurRow["rcnta_gkey"]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        WebDataGrid_rcntd.Rows.Clear();
        WebDataGrid_rcntd.DataBind();
        WebDataGrid_rcntr.Rows.Clear();
        WebDataGrid_rcntr.DataBind();
        //
        tx_rcnta_BDDTS.Date = DateTime.Today;
        tx_rcnta_BDCCN.Enabled = false;
        //tx_rcnta_BDEDT.Date = DateTime.Today;
        //
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
        if (hh_mkey.Value != "")
        {
          WebTab_form.SelectedIndex = 1;
          hh_GridCtrl.Value = "mod_a";
          Set_Control_A();
          //取Act guidkey
          hh_ActKey.Value = DAC.get_guidkey();
          ShowOneRow_A(hh_mkey.Value);
          SetEditMod_A();
          tx_rcnta_BDCCN.Enabled = false;
        }
        else
        {
          lb_ErrorMessage.Text = "未選擇修改資料.";
          lb_ErrorMessage.Visible = true;
        }
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
      if ((hh_fun_mkey.Value == "#") && (hh_fun_mkey_old.Value != ""))
      {
        hh_fun_mkey.Value = hh_fun_mkey_old.Value;
        hh_fun_mkey_old.Value = "";
      }
      Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, !bl_resetKey);
      Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, !bl_showRowA, !bl_resetKey);
      //
      ShowOneRow_A(hh_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_rcnta_ba, "rcnta_mkey", hh_mkey.Value);
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
        string st_nextkey = sFN.WebDataGrid_NextKey(WebDataGrid_rcnta_ba, "rcnta_mkey", hh_mkey.Value);
        //
        DAC bcvwDao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "rcnta_gkey='" + hh_GridGkey.Value + "' and rcnta_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_bcvw = new DataTable();
        OleDbConnection connD = new OleDbConnection();
        connD = DAC.NewReaderConnr();
        connD.Open();
        DbDataAdapter da_ADP = bcvwDao.GetDataAdapter(ApVer, "UNrcnta", "rcnta", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "", "SEL DEL ");
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
            bcvwDao.Insertbalog(connD, thistran, "rcnta", hh_ActKey.Value, hh_GridGkey.Value);
            bcvwDao.Insertbtlog(connD, thistran, "rcnta", DAC.GetStringValue(DelRow[0]["rcnta_BDCCN"]), "D", UserName, DAC.GetStringValue(DelRow[0]["rcnta_gkey"]));
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
        bcvwDao.Dispose();
        tb_bcvw.Dispose();
        da_ADP.Dispose();
        //
        if (bl_delok)
        {
          hh_mkey.Value = st_nextkey;
          act_SERS_L();
          SetSerMod_A();
          ShowOneRow_A(hh_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_rcnta_ba, "rcnta_mkey", hh_mkey.Value);
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
        DAC_rcnta rcntaDao = new DAC_rcnta(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "rcnta_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins_a")
          {
            //自動編號
            st_ren_yymmtext = sFN.strzeroi(tx_rcnta_BDDTS.Date.Year, 4) + sFN.strzeroi(tx_rcnta_BDDTS.Date.Month, 2);
            st_ren_cls = st_ren_yymmtext;
            tx_rcnta_BDCCN.Text = rcntaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            conn.Close();
            //檢查重複
            if (rcntaDao.IsExists("rcnta", "BDCCN", tx_rcnta_BDCCN.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_rcnta_BDCCN.Text + ",已存在.");
              rcntaDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_rcnta_BDCCN.Text);
              st_dberrmsg = StringTable.GetString(tx_rcnta_BDCCN.Text + ",已重新取號.");
              tx_rcnta_BDCCN.Text = rcntaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_rcnta_RIREN.Text ="";
            }
            else
            {
              conn.Open();
              DataTable tb_rcnta_ins = new DataTable();
              DbDataAdapter da_ADP_ins = rcntaDao.GetDataAdapter(ApVer, "UNrcnta", "rcnta", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_rcnta_ins);
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_rcnta_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["rcnta_gkey"] = st_tempgkey;    // 
                ins_row["rcnta_mkey"] = st_tempgkey;    //
                //
                ins_row["rcnta_BDCCN"] = tx_rcnta_BDCCN.Text.Trim();       //  
                if (tx_rcnta_BDDTS.Text.Trim() == "") { ins_row["rcnta_BDDTS"] = DBNull.Value; } else { ins_row["rcnta_BDDTS"] = sFN.DateStringToDateTime(tx_rcnta_BDDTS.Text); }       //開始日期
                if (tx_rcnta_BDDTE.Text.Trim() == "") { ins_row["rcnta_BDDTE"] = DBNull.Value; } else { ins_row["rcnta_BDDTE"] = sFN.DateStringToDateTime(tx_rcnta_BDDTE.Text); }       //結束日期
                ins_row["rcnta_BDNUM"] = tx_rcnta_BDNUM.Text.Trim();       // 出版社號
                ins_row["rcnta_BDPTN"] = tx_rcnta_BDPTN.Text.Trim();       // 商品編號
                ins_row["rcnta_BDQTY"] = tx_rcnta_BDQTY.Text.Trim();       // 預付本數
                ins_row["rcnta_BDDE1"] = tx_rcnta_BDDE1.Text.Trim();       // 定價金額
                ins_row["rcnta_BDRAT"] = tx_rcnta_BDRAT.Text.Trim();       // 預付比率
                ins_row["rcnta_BDTXP"] = dr_rcnta_BDTXP.SelectedValue;       // 計稅方式
                //
                //tx_rcnta_BDAMT.ValueDecimal =sFN.Round(  tx_rcnta_BDDE1.ValueDecimal * tx_rcnta_BDQTY.ValueDecimal* tx_rcnta_BDRAT.ValueDecimal /100,0);
                ins_row["rcnta_BDAMT"] = 0;       // 預付金額
                //if (tx_rcnta_BDEDT.Text.Trim() == "") { ins_row["rcnta_BDEDT"] = DBNull.Value; } else { ins_row["rcnta_BDEDT"] = sFN.DateStringToDateTime(tx_rcnta_BDEDT.Text); }       //預付月份
                ins_row["rcnta_BDEDT"] = DBNull.Value; // 預付金額
                ins_row["rcnta_BDRMK"] = tx_rcnta_BDRMK.Text.Trim();       // 備　　註
                ins_row["rcnta_trusr"] = UserGkey;  //
                tb_rcnta_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_rcnta_ins);
                rcntaDao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_rcnta_BDCCN.Text.Trim());
                rcntaDao.Insertbalog(conn, thistran, "rcnta", hh_ActKey.Value, hh_GridGkey.Value);
                rcntaDao.Insertbtlog(conn, thistran, "rcnta", DAC.GetStringValue(ins_row["rcnta_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["rcnta_gkey"]));
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
                rcntaDao.Dispose();
                tb_rcnta_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, !bl_showRowA, !bl_resetKey);
              //
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_rcnta_ba, "rcnta_mkey", hh_mkey.Value);
              ShowOneRow_A(hh_mkey.Value);
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
            if (rcntaDao.IsExists("rcnta", "BDCCN", tx_rcnta_BDCCN.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_rcnta_BDCCN.Text + ",已存在.");
            }
            else
            {
              DataTable tb_rcnta_mod = new DataTable();
              DbDataAdapter da_ADP_mod = rcntaDao.GetDataAdapter(ApVer, "UNrcnta", "rcnta", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_rcnta_mod);
              st_SelDataKey = "rcnta_gkey='" + hh_GridGkey.Value + "' and rcnta_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_rcnta_mod.Select(st_SelDataKey);
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
                  if (tx_rcnta_BDDTS.Text.Trim() == "") { mod_row["rcnta_BDDTS"] = DBNull.Value; } else { mod_row["rcnta_BDDTS"] = sFN.DateStringToDateTime(tx_rcnta_BDDTS.Text); }       //開始日期
                  if (tx_rcnta_BDDTE.Text.Trim() == "") { mod_row["rcnta_BDDTE"] = DBNull.Value; } else { mod_row["rcnta_BDDTE"] = sFN.DateStringToDateTime(tx_rcnta_BDDTE.Text); }       //結束日期
                  mod_row["rcnta_BDNUM"] = tx_rcnta_BDNUM.Text.Trim();       // 出版社號
                  mod_row["rcnta_BDPTN"] = tx_rcnta_BDPTN.Text.Trim();       // 商品編號
                  mod_row["rcnta_BDQTY"] = tx_rcnta_BDQTY.Text.Trim();       // 預付本數
                  mod_row["rcnta_BDDE1"] = tx_rcnta_BDDE1.Text.Trim();       // 定價金額
                  mod_row["rcnta_BDRAT"] = tx_rcnta_BDRAT.Text.Trim();       // 預付比率
                  mod_row["rcnta_BDTXP"] = dr_rcnta_BDTXP.SelectedValue;       // 計稅方式
                  //tx_rcnta_BDAMT.ValueDecimal = sFN.Round(tx_rcnta_BDDE1.ValueDecimal * tx_rcnta_BDQTY.ValueDecimal * tx_rcnta_BDRAT.ValueDecimal / 100, 0);
                  mod_row["rcnta_BDAMT"] = 0;       // 預付金額
                  //if (tx_rcnta_BDEDT.Text.Trim() == "") { mod_row["rcnta_BDEDT"] = DBNull.Value; } else { mod_row["rcnta_BDEDT"] = sFN.DateStringToDateTime(tx_rcnta_BDEDT.Text); }       //預付月份
                  mod_row["rcnta_BDEDT"] = DBNull.Value; //預付月份
                  mod_row["rcnta_BDRMK"] = tx_rcnta_BDRMK.Text.Trim();       // 備　　註
                  mod_row["rcnta_mkey"] = st_tempgkey;        //
                  mod_row["rcnta_trusr"] = UserGkey;  //

                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_rcnta_mod);
                  rcntaDao.Insertbalog(conn, thistran, "rcnta", hh_ActKey.Value, hh_GridGkey.Value);
                  rcntaDao.Insertbtlog(conn, thistran, "rcnta", DAC.GetStringValue(mod_row["rcnta_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["rcnta_gkey"]));
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
                  rcntaDao.Dispose();
                  tb_rcnta_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_rcnta, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_rcnta_ba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_rcnta_ba, "rcnta_mkey", hh_mkey.Value);
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
        rcntaDao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }

    }

    protected void bt_QUT_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Response.Redirect("~/Master/Default/Mainform.aspx");
    }

    private bool ServerEditCheck_A(ref string sMsg)
    {
      bool ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_rcnta_BDDTS.Text, lb_rcnta_BDDTS.Text, ref sMsg, LangType, sFN); //開始日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_rcnta_BDDTE.Text, lb_rcnta_BDDTE.Text, ref sMsg, LangType, sFN); //結束日期
      ret = DataCheck.cIsStrEmptyChk(ret, tx_rcnta_BDNUM.Text, lb_rcnta_BDNUM.Text, ref sMsg, LangType, sFN);  //出版社號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_rcnta_BDPTN.Text, lb_rcnta_BDPTN.Text, ref sMsg, LangType, sFN);  //商品編號
      //ret = DataCheck.cIsStrEmptyChk(ret, tx_rcnta_BDEDT.Text, lb_rcnta_BDEDT.Text, ref sMsg, LangType, sFN); //預付月份

      ret = DataCheck.cIsStrDecimalChk(ret, tx_rcnta_BDQTY.Text, lb_rcnta_BDQTY.Text, ref sMsg, LangType, sFN); //預付本數
      ret = DataCheck.cIsStrDecimalChk(ret, tx_rcnta_BDDE1.Text, lb_rcnta_BDDE1.Text, ref sMsg, LangType, sFN); //定價金額
      ret = DataCheck.cIsStrDecimalChk(ret, tx_rcnta_BDRAT.Text, lb_rcnta_BDRAT.Text, ref sMsg, LangType, sFN); //預付比率
      //ret = DataCheck.cIsStrDecimalChk(ret, tx_rcnta_BDAMT.Text, lb_rcnta_BDAMT.Text, ref sMsg, LangType, sFN); //預付金額
      //ret = DataCheck.cIsStrDatetimeChk(ret, tx_rcnta_BDEDT.Text, lb_rcnta_BDEDT.Text, ref sMsg, LangType, sFN); //預付月份
      DataCheck.Dispose();
      return ret;
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
        CmdQueryS_ba = (OleDbCommand)Session["fmrcnta_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ba.CommandText = " and 1=0 ";
      }
    }

    protected void Obj_rcnta_ba_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
        e.InputParameters["st_orderKey"] = " A.BDDTS DESC ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_rcnta_ba_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      try
      {
        SetSerMod_B();
        hh_fun_name.Value = "showa";
        hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("rcnta_mkey").Value);
        WebTab_form.SelectedIndex = 1;
        //
        ShowOneRow_A(hh_fun_mkey.Value);
        sFN.WebDataGrid_SelectRow(ref WebDataGrid_rcnta_ba, "rcnta_mkey", hh_fun_mkey.Value);
        SetSerMod_A();
      }
      catch
      {
      }
    }


    ///rcntd control
    /// <summary>
    /// 
    /// </summary>
    /// <param name="st_ren"></param>
    private void get_CmdQueryS_b(string st_ren)
    {
      CmdQueryS_b.CommandText = " and A.BDCCN=? ";
      CmdQueryS_b.Parameters.Clear();
      DAC.AddParam(CmdQueryS_b, "BDCCN", st_ren);
    }

    ///rcntr control
    /// <summary>
    /// 
    /// </summary>
    /// <param name="st_ren"></param>
    private void get_CmdQueryS_b1(string st_ren)
    {
      CmdQueryS_b1.CommandText = " and A.BDCCN=? ";
      CmdQueryS_b1.Parameters.Clear();
      DAC.AddParam(CmdQueryS_b1, "BDCCN", st_ren);
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
          //hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rcntd_gkey");
          //hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rcntd_mkey");
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


    #region WebDataGrid_rcntd

    protected void WebDataGrid_rcntd_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      //string st_key = e.CurrentSelectedRows[0].DataKey[0].ToString();
    }

    protected void WebDataGrid_rcntd_RowAdding(object sender, Infragistics.Web.UI.GridControls.RowAddingEventArgs e)
    {
      //string st_mkey="";
      //st_mkey=DAC.GetStringValue(e.Values["rcntd_mkey"]);
      if (clsFN.chkLoginState())
      {
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_rcntd_RowAdded(object sender, Infragistics.Web.UI.GridControls.RowAddedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntd.CustomAJAXResponse.Message = e.Exception.InnerException.Message;
      }
      else
      {
        //有新資料,設到最後的Page
        try
        {
          WebDataGrid_rcntd.Behaviors.Paging.PageIndex = WebDataGrid_rcntd.Behaviors.Paging.PageCount - 1;
        }
        catch
        {
        }
      }
    }

    protected void WebDataGrid_rcntd_RowUpdating(object sender, Infragistics.Web.UI.GridControls.RowUpdatingEventArgs e)
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

    protected void WebDataGrid_rcntd_RowUpdated(object sender, Infragistics.Web.UI.GridControls.RowUpdatedEventArgs e)
    {

    }

    #endregion

    #region Obj_rcntd

    protected void Obj_rcntd_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        //mkey 轉單號
        string st_ren = "";
        st_ren = sFN.GetRenFromGkey("rcnta", "BDCCN", "mkey", hh_fun_mkey.Value);
        get_CmdQueryS_b(st_ren);
        e.InputParameters["WhereQuery"] = CmdQueryS_b;
        e.InputParameters["st_addSelect"] = "";
        e.InputParameters["bl_lock"] = false;
        e.InputParameters["st_addJoin"] = "";
        e.InputParameters["st_addUnion"] = "";
        e.InputParameters["st_orderKey"] = " A.BDQTY DESC ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_rcntd_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if (btnUpdateCancel.Value != "1")
        {
          e.InputParameters["rcntd_BDCCN"] = tx_rcnta_BDCCN.Text;
          e.InputParameters["rcntd_actkey"] = DAC.get_guidkey();
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

    protected void Obj_rcntd_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntd.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }



    protected void Obj_rcntd_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if (btnUpdateCancel.Value != "1")
        {
          e.InputParameters["rcntd_actkey"] = DAC.get_guidkey();
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

    protected void Obj_rcntd_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntd.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_rcntd_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if (btnUpdateCancel.Value != "1")
        {
          e.InputParameters["rcntd_actkey"] = DAC.get_guidkey();
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

    protected void Obj_rcntd_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntd.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }
    #endregion



    #region WebDataGrid_rcntr

    protected void WebDataGrid_rcntr_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      //string st_key = e.CurrentSelectedRows[0].DataKey[0].ToString();
    }

    protected void WebDataGrid_rcntr_RowAdding(object sender, Infragistics.Web.UI.GridControls.RowAddingEventArgs e)
    {
      //string st_mkey="";
      //st_mkey=DAC.GetStringValue(e.Values["rcntr_mkey"]);
      if (clsFN.chkLoginState())
      {
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_rcntr_RowAdded(object sender, Infragistics.Web.UI.GridControls.RowAddedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntr.CustomAJAXResponse.Message = e.Exception.InnerException.Message;
      }
      else
      {
        //有新資料,設到最後的Page
        try
        {
          WebDataGrid_rcntr.Behaviors.Paging.PageIndex = WebDataGrid_rcntr.Behaviors.Paging.PageCount - 1;
        }
        catch
        {
        }
      }

    }

    protected void WebDataGrid_rcntr_RowUpdating(object sender, Infragistics.Web.UI.GridControls.RowUpdatingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        string st_BDCCN = DAC.GetStringValue(e.Values["rcntr_BDCCN"]);
        string st_BDAUR = DAC.GetStringValue(e.Values["rcntr_BDAUR"]);
        string st_gkey = DAC.GetStringValue(e.Values["rcntr_gkey"]);
        OleDbCommand Qcheck = new OleDbCommand();
        Qcheck.Parameters.Clear();
        Qcheck.CommandText = " and BDCCN=? and BDAUR=? and gkey!=? ";
        DAC.AddParam(Qcheck, "rcntr_BDCCN", st_BDCCN);
        DAC.AddParam(Qcheck, "rcntr_BDAUR", st_BDAUR);
        DAC.AddParam(Qcheck, "rcntr_gkey", st_gkey);
        if (sFN.lookups("select gkey from  rcntr ", Qcheck, "gkey") != "")
        {
          e.Cancel = true;
          WebDataGrid_rcntr.CustomAJAXResponse.Message = "資料已存在!";
          //Response.Redirect("fm_rcnta.aspx");  
        }
        Qcheck.Dispose();
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }


    }

    protected void WebDataGrid_rcntr_RowUpdated(object sender, Infragistics.Web.UI.GridControls.RowUpdatedEventArgs e)
    {

    }
    #endregion

    #region Obj_rcntr

    protected void Obj_rcntr_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        //mkey 轉單號
        string st_ren = "";
        st_ren = sFN.GetRenFromGkey("rcnta", "BDCCN", "mkey", hh_fun_mkey.Value);
        get_CmdQueryS_b1(st_ren);
        e.InputParameters["WhereQuery"] = CmdQueryS_b1;
        e.InputParameters["st_addSelect"] = "";
        e.InputParameters["bl_lock"] = false;
        e.InputParameters["st_addJoin"] = "";
        e.InputParameters["st_addUnion"] = "";
        e.InputParameters["st_orderKey"] = " A.BDAUR ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }


    protected void Obj_rcntr_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if ((btnUpdateCancel.Value != "1") && (DAC.GetStringValue(e.InputParameters["rcntr_BDAUR"]) != "") && (e.InputParameters["rcntr_BDAUR"] != null))
        {
          e.InputParameters["rcntr_BDCCN"] = tx_rcnta_BDCCN.Text;
          e.InputParameters["rcntr_actkey"] = DAC.get_guidkey();
          e.InputParameters["UserGkey"] = UserGkey;
        }
        else
        {
          //do'nt insert
          e.Cancel = true;
        }
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_rcntr_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntr.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_rcntr_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if (btnUpdateCancel.Value != "1")
        {
          e.InputParameters["rcntr_actkey"] = DAC.get_guidkey();
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

    protected void Obj_rcntr_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntr.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_rcntr_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if ((btnUpdateCancel.Value != "1") && (DAC.GetStringValue(e.InputParameters["rcntr_BDAUR"]) != "") && (e.InputParameters["rcntr_BDAUR"] != null))
        {
          e.InputParameters["rcntr_actkey"] = DAC.get_guidkey();
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

    protected void Obj_rcntr_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rcntr.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    #endregion

    protected void bt_MOD_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      if (bt_SAVE_B.Visible)
      {
        SetSerMod_B();
      }
      else
      {
        SetEditMod_B();
      } 
    }

    private void SetSerMod_B()
    {
      sFN.WebDataGrid_SetEdit(ref WebDataGrid_rcntr, false);
      sFN.WebDataGrid_SetEdit(ref WebDataGrid_rcntd, false);
      //
      bt_MOD_B.Text = "W更正";
      bt_MOD_B.AccessKey = "W"; 
      bt_MOD_B = sFN.SetWebImageButtonDetail(bt_MOD_B);
      bt_MOD_B.Visible = true;
      //
      bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
      bt_New_B.Visible = false;
      bt_New_B.ClientSideEvents.Click = "";
      //
      bt_SAVE_B = sFN.SetWebImageButtonDetail(bt_SAVE_B);
      bt_SAVE_B.Visible = false;
      //
      bt_Cancel_B = sFN.SetWebImageButtonDetail(bt_Cancel_B);
      bt_Cancel_B.Visible = false;
      //
      WebDataGrid_rcnta.Enabled = true;
      WebDataGrid_rcnta_ba.Enabled = true;
      PanBtns.Enabled = true;
    }
    private void SetEditMod_B()
    {
      sFN.WebDataGrid_SetEdit(ref WebDataGrid_rcntr, true);
      sFN.WebDataGrid_SetEdit(ref WebDataGrid_rcntd, true);
      bt_New_B.ClientSideEvents.Click = "webDataGrid_AddRowB()";
      //
      bt_MOD_B.Text="E完成";
      bt_MOD_B.AccessKey="E"; 
      bt_MOD_B = sFN.SetWebImageButtonDetail(bt_MOD_B);
      bt_MOD_B.Visible = true;
      //
      bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
      bt_New_B.Visible = true;
      //
      bt_SAVE_B = sFN.SetWebImageButtonDetail(bt_SAVE_B);
      bt_SAVE_B.Visible = true;
      //
      bt_Cancel_B = sFN.SetWebImageButtonDetail(bt_Cancel_B);
      bt_Cancel_B.Visible = true;
      //
      WebDataGrid_rcnta.Enabled = false;
      WebDataGrid_rcnta_ba.Enabled = false;
      PanBtns.Enabled = false;
    }

    protected void bt_SAVE_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Bind_WebDataGrid_B(WebDataGrid_rcntr, bl_resetKey);
      Bind_WebDataGrid_B(WebDataGrid_rcntd, bl_resetKey);
    }
 
  }
}