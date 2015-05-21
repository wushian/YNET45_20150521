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
  public partial class fm_temp_mdd : FormBase
  {
    public string st_object_func = "UNmtable";
    public string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    public string st_ContentPlaceHolderEdit = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$";
    bool bl_resetKey = true;
    bool bl_showRowA = true;
    //
    string st_dd_apx = "UNmtable";         //UNdcnews   與apx 相關
    //string st_dd_table = "mtable";         //dcnews     與table 相關 
    string st_ren_head = "CA";            //DC         與單號相關 
    string st_ren_yymmtext = "YYYYMM";     //"         與單號相關 
    string st_ren_cls = "mtable";        //ren        與單號cls相關 
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
          dr_mtable_BDTXP = sFN.DropDownListFromClasses(ref dr_mtable_BDTXP, "UNtax_TXP", "class_text", "class_value");
          //
          WebTab_form.SelectedIndex = 0;
          Set_Control_A();
          //
          tx_mtable_BDDTS_s1.Value = DateTime.Today.AddYears(-10);
          tx_mtable_BDDTS_s2.Value = DateTime.Today;
          tx_mtable_BDPTN_s.Text = "";
          tx_bpud_BPTNA_s.Text = "";
          //
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_dtable2, false);
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_dtable1, false);
          this.Form.Attributes.Add("onkeydown", "do_Keydown_EnterToTab();");    //SiteMM.Master中輸入Enter轉Tab
          //this.Form.Attributes.Add("onkeydown", "return do_Keydown_CancelEnter();"); //SiteMM.Master中輸入Enter取消
          SetSerMod_B();
          //
          if (Session["fmmtable_CmdQueryS"] == null)
          {
            act_SERS_L();
          }
          else
          {
            get_CmdQueryS();
            Obj_mtable.TypeName = "DD2015_45.DAC_mtable";
            Obj_mtable.SelectMethod = "SelectTable_mtable";
            WebDataGrid_mtable.DataSourceID = "Obj_mtable";
            Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
            get_CmdQueryS_ba();
            Obj_mtable_ba.TypeName = "DD2015_45.DAC_mtable";
            Obj_mtable_ba.SelectMethod = "SelectTable_mtable_ba";
            WebDataGrid_mtable_ba.DataSourceID = "Obj_mtable_ba";
            Bind_WebDataGrid_A(WebDataGrid_mtable_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
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
      if (Request[Page.postEventSourceID] == "btnAction")
      {
        string[] arguments = Request[Page.postEventArgumentID].Split('&');
        if (arguments.Length > 0) hh_fun_name.Value = DAC.GetStringValue(arguments[0]);
        if (arguments.Length > 1) hh_fun_mkey.Value = DAC.GetStringValue(arguments[1]);
        //
        if (hh_fun_name.Value.ToLower() == "showa")
        {
          ShowOneRow_A(hh_fun_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_mtable_ba, "mtable_mkey", hh_fun_mkey.Value);
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

    private void get_CmdQueryS()
    {
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmmtable_CmdQueryS"];
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
      if ((tx_mtable_BDDTS_s1.Text != "") && (tx_mtable_BDDTS_s2.Text != ""))
      {
        CmdQueryS.CommandText += " and a.BDDTS >=?  and a.BDDTS <=? ";
        DAC.AddParam(CmdQueryS, "mtable_BDDTS_s1", tx_mtable_BDDTS_s1.Date);
        DAC.AddParam(CmdQueryS, "mtable_BDDTS_s2", tx_mtable_BDDTS_s2.Date);
      }
      else if (tx_mtable_BDDTS_s1.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDDTS >=?  ";
        DAC.AddParam(CmdQueryS, "mtable_BDDTS_s1", tx_mtable_BDDTS_s1.Date);
      }
      else if (tx_mtable_BDDTS_s2.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDDTS <=?  ";
        DAC.AddParam(CmdQueryS, "mtable_BDDTS_s2", tx_mtable_BDDTS_s2.Date);
      }
      //
      if (tx_mtable_BDPTN_s.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDPTN like ?  ";
        DAC.AddParam(CmdQueryS, "mtable_BDPTN_s", tx_mtable_BDPTN_s.Text + "%");
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
        if (Session["fmmtable_CmdQueryS"] == null)
        {
          reset_CmdQueryS_comm();
          //
          Session["fmmtable_CmdQueryS"] = CmdQueryS;
          Obj_mtable.TypeName = "DD2015_45.DAC_mtable";
          Obj_mtable.SelectMethod = "SelectTable_mtable";
          WebDataGrid_mtable.DataSourceID = "Obj_mtable";
          Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_mtable_ba.TypeName = "DD2015_45.DAC_mtable";
          Obj_mtable_ba.SelectMethod = "SelectTable_mtable_ba";
          WebDataGrid_mtable_ba.DataSourceID = "Obj_mtable_ba";
          Bind_WebDataGrid_A(WebDataGrid_mtable_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
        }
        else
        {
          get_CmdQueryS();
          Session["fmmtable_CmdQueryS"] = CmdQueryS;
          Obj_mtable.TypeName = "DD2015_45.DAC_mtable";
          Obj_mtable.SelectMethod = "SelectTable_mtable";
          WebDataGrid_mtable.DataSourceID = "Obj_mtable";
          Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_mtable_ba.TypeName = "DD2015_45.DAC_mtable";
          Obj_mtable_ba.SelectMethod = "SelectTable_mtable_ba";
          WebDataGrid_mtable_ba.DataSourceID = "Obj_mtable_ba";
          Bind_WebDataGrid_A(WebDataGrid_mtable_ba, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
        }
      }
      catch
      {
        reset_CmdQueryS_comm();
        //
        Session["fmmtable_CmdQueryS"] = CmdQueryS;
        Obj_mtable.TypeName = "DD2015_45.DAC_mtable";
        Obj_mtable.SelectMethod = "SelectTable_mtable";
        WebDataGrid_mtable.DataSourceID = "Obj_mtable";
        Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, bl_resetKey); //reset gkey,mkey
        //
        Obj_mtable_ba.TypeName = "DD2015_45.DAC_mtable";
        Obj_mtable_ba.SelectMethod = "SelectTable_mtable_ba";
        WebDataGrid_mtable_ba.DataSourceID = "Obj_mtable_ba";
        Bind_WebDataGrid_A(WebDataGrid_mtable_ba, !bl_showRowA, bl_resetKey); //reset gkey,mkey
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
          hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "mtable_gkey");
          hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "mtable_mkey");
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
      WebDataGrid_mtable.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_mtable_ba.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_dtable1.Behaviors.Paging.PageSize = 5;
      WebDataGrid_dtable2.Behaviors.Paging.PageSize = 5;
      //
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNmtable", "mtable");
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolderEdit, ApVer, "UNmtable", "mtable");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_mtable, PublicVariable.LangType, ApVer, "UNmtable", "mtable");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_mtable_ba, PublicVariable.LangType, ApVer, "UNmtable", "mtable");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_dtable2, PublicVariable.LangType, ApVer, "UNdtable2", "dtable2");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_dtable1, PublicVariable.LangType, ApVer, "UNdtable1", "dtable1");
    }

    protected void act_SERS()
    {
      reset_CmdQueryS_comm();
      //
      Session["fmmtable_CmdQueryS"] = CmdQueryS;
      Obj_mtable.TypeName = "DD2015_45.DAC_mtable";
      Obj_mtable.SelectMethod = "SelectTable_mtable";
      WebDataGrid_mtable.DataSourceID = "Obj_mtable";
      Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      //
      get_CmdQueryS_ba();
      Obj_mtable_ba.TypeName = "DD2015_45.DAC_mtable";
      Obj_mtable_ba.SelectMethod = "SelectTable_mtable_ba";
      WebDataGrid_mtable_ba.DataSourceID = "Obj_mtable_ba";
      Bind_WebDataGrid_A(WebDataGrid_mtable_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
    }

    protected void Obj_mtable_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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

    protected void WebDataGrid_mtable_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      try
      {
        SetSerMod_B();
        hh_fun_name.Value = "showa";
        hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("mtable_mkey").Value);
        WebTab_form.SelectedIndex = 1;
        //
        ShowOneRow_A(hh_fun_mkey.Value);
        sFN.WebDataGrid_SelectRow(ref WebDataGrid_mtable_ba, "mtable_mkey", hh_fun_mkey.Value);
        SetSerMod_A();
      }
      catch
      {
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
      tx_mtable_BDCCN.Text = "";  //合約編號
      tx_mtable_BDDTS.Text = "";  //開始日期
      tx_mtable_BDDTE.Text = "";  //結束日期
      tx_mtable_BDNUM.Text = "";  //出版社號
      tx_bdlr_BDNAM.Text = "";  //出版社名
      tx_mtable_BDPTN.Text = "";  //商品編號
      tx_bpud_BPTNA.Text = "";  //商品名稱
      tx_mtable_BDQTY.Value = 0;  //預付本數
      tx_mtable_BDDE1.Value = 0;  //定價金額
      tx_mtable_BDRAT.Value = 0;  //預付比率
      dr_mtable_BDTXP.SelectedIndex = -1;  //計稅方式
      //tx_mtable_BDAMT.Value = 0;  //預付金額
      //tx_mtable_BDEDT.Text = "";  //預付月份
      tx_mtable_BDRMK.Text = "";  //備　　註
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetSerMod_A()
    {
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDCCN, ref tx_mtable_BDCCN, false);  //合約編號
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDDTS, ref tx_mtable_BDDTS, false);  //開始日期
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDNUM, ref tx_mtable_BDNUM, false);  //出版社號
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDPTN, ref tx_mtable_BDPTN, false);  //商品編號
      //clsGV.SetTextBoxEditAlert(ref lb_mtable_BDEDT, ref tx_mtable_BDEDT, false);  //預付月份
      //
      clsGV.TextBox_Set(ref tx_mtable_BDCCN, false);   //合約編號
      clsGV.TextBox_Set(ref tx_mtable_BDDTS, false);   //開始日期
      clsGV.TextBox_Set(ref tx_mtable_BDDTE, false);   //結束日期
      clsGV.TextBox_Set(ref tx_mtable_BDNUM, false);   //出版社號
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, false);   //出版社名
      clsGV.TextBox_Set(ref tx_mtable_BDPTN, false);   //商品編號
      clsGV.TextBox_Set(ref tx_bpud_BPTNA, false);   //商品名稱
      clsGV.TextBox_Set(ref tx_mtable_BDQTY, false);   //預付本數
      clsGV.TextBox_Set(ref tx_mtable_BDDE1, false);   //定價金額
      clsGV.TextBox_Set(ref tx_mtable_BDRAT, false);   //預付比率
      clsGV.Drpdown_Set(ref dr_mtable_BDTXP, ref tx_mtable_BDTXP, false);   //計稅方式
      //clsGV.TextBox_Set(ref tx_mtable_BDAMT, false);   //預付金額
      //clsGV.TextBox_Set(ref tx_mtable_BDEDT, false);   //預付月份
      clsGV.TextBox_Set(ref tx_mtable_BDRMK, false);   //備　　註
      //
      clsGV.SetControlShowAlert(ref lb_mtable_BDCCN, ref tx_mtable_BDCCN, true);  // 合約編號
      clsGV.SetControlShowAlert(ref lb_mtable_BDDTS, ref tx_mtable_BDDTS, true);  // 開始日期
      clsGV.SetControlShowAlert(ref lb_mtable_BDNUM, ref tx_mtable_BDNUM, true);  // 出版社號
      clsGV.SetControlShowAlert(ref lb_mtable_BDPTN, ref tx_mtable_BDPTN, true);  // 商品編號
      //clsGV.SetControlShowAlert(ref lb_mtable_BDEDT, ref tx_mtable_BDEDT, true);  // 預付月份
      //
      tx_mtable_BDNUM.Attributes.Remove("onblur");
      tx_mtable_BDPTN.Attributes.Remove("onblur");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      SetSerMod_B();
      //
      WebDataGrid_mtable.Enabled = true;
      WebDataGrid_mtable_ba.Enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetEditMod_A()
    {
      //
      clsGV.TextBox_Set(ref tx_mtable_BDCCN, true);  //合約編號
      clsGV.TextBox_Set(ref tx_mtable_BDDTS, true);  //開始日期
      clsGV.TextBox_Set(ref tx_mtable_BDDTE, true);  //結束日期
      clsGV.TextBox_Set(ref tx_mtable_BDNUM, true);  //出版社號
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, false);  //出版社名
      clsGV.TextBox_Set(ref tx_mtable_BDPTN, true);  //商品編號
      clsGV.TextBox_Set(ref tx_bpud_BPTNA, false);  //商品名稱
      clsGV.TextBox_Set(ref tx_mtable_BDQTY, true);  //預付本數
      clsGV.TextBox_Set(ref tx_mtable_BDDE1, true);  //定價金額
      clsGV.TextBox_Set(ref tx_mtable_BDRAT, true);  //預付比率
      clsGV.Drpdown_Set(ref dr_mtable_BDTXP, ref tx_mtable_BDTXP, true);   //計稅方式
      //clsGV.TextBox_Set(ref tx_mtable_BDAMT, true);  //預付金額
      //clsGV.TextBox_Set(ref tx_mtable_BDEDT, true);  //預付月份
      clsGV.TextBox_Set(ref tx_mtable_BDRMK, true);  //備　　註
      //
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDCCN, ref tx_mtable_BDCCN, true);  // 合約編號
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDDTS, ref tx_mtable_BDDTS, true);  // 開始日期
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDNUM, ref tx_mtable_BDNUM, true);  // 出版社號
      clsGV.SetTextBoxEditAlert(ref lb_mtable_BDPTN, ref tx_mtable_BDPTN, true);  // 商品編號
      //clsGV.SetTextBoxEditAlert(ref lb_mtable_BDEDT, ref tx_mtable_BDEDT, true);  // 預付月份
      //
      tx_mtable_BDNUM.Attributes.Add("onblur", "return  get_bdlr_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_mtable_BDNUM','" + st_ContentPlaceHolderEdit + "tx_bdlr_BDNAM'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bdlr.aspx" + "','" + "廠商資料" + "')");
      tx_mtable_BDPTN.Attributes.Add("onblur", "return  get_bpud_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_mtable_BDPTN','" + st_ContentPlaceHolderEdit + "tx_bpud_BPTNA'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bpud.aspx" + "','" + "商品資料" + "')");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      SetSerMod_B();
      //
      WebDataGrid_mtable.Enabled = false;
      WebDataGrid_mtable_ba.Enabled = false;
    }

    private void ShowOneRow_A(string st_mkey)
    {
      DAC_mtable mtableDao = new DAC_mtable(conn);
      DataTable tb_mtable = new DataTable();
      OleDbCommand cmd_where = new OleDbCommand();
      string st_ren = "";
      //
      cmd_where.CommandText = " and a.mkey=? ";
      DAC.AddParam(cmd_where, "mkey", st_mkey);
      tb_mtable = mtableDao.SelectTableForTextEdit_mtable(cmd_where);
      if (tb_mtable.Rows.Count == 1)
      {
        BindText_A(tb_mtable.Rows[0]);
        //bt_05.OnClientClick = "return btnDEL_c()";
        //bt_04.OnClientClick = "return btnMOD_c()";
        //dtable2 
        st_ren = sFN.GetRenFromGkey("mtable", "BDCCN", "mkey", st_mkey);
        get_CmdQueryS_b(st_ren);
        Obj_dtable2.TypeName = "DD2015_45.DAC_dtable2";
        Obj_dtable2.SelectMethod = "SelectTable_dtable2";
        Obj_dtable2.UpdateMethod = "UpdateTable_dtable2";
        Obj_dtable2.InsertMethod = "InsertTable_dtable2";
        Obj_dtable2.DeleteMethod = "DeleteTable_dtable2";
        WebDataGrid_dtable2.DataSourceID = "Obj_dtable2";
        Bind_WebDataGrid_B(WebDataGrid_dtable2, bl_resetKey);
        //dtable1
        st_ren = sFN.GetRenFromGkey("mtable", "BDCCN", "mkey", st_mkey);
        get_CmdQueryS_b1(st_ren);
        Obj_dtable1.TypeName = "DD2015_45.DAC_dtable1";
        Obj_dtable1.SelectMethod = "SelectTable_dtable1";
        Obj_dtable1.UpdateMethod = "UpdateTable_dtable1";
        Obj_dtable1.InsertMethod = "InsertTable_dtable1";
        Obj_dtable1.DeleteMethod = "DeleteTable_dtable1";
        WebDataGrid_dtable1.DataSourceID = "Obj_dtable1";
        Bind_WebDataGrid_B(WebDataGrid_dtable1, bl_resetKey);
      }
      else
      {
        ClearText_A();
        //bt_05.OnClientClick = "return btnDEL0_c()";
        //bt_04.OnClientClick = "return btnMOD0_c()";
      }
      cmd_where.Dispose();
      tb_mtable.Dispose();
      mtableDao.Dispose();
      //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CurRow"></param>
    private void BindText_A(DataRow CurRow)
    {
      //
      tx_mtable_BDCCN.Text = DAC.GetStringValue(CurRow["mtable_BDCCN"]);  //合約編號
      if (CurRow["mtable_BDDTS"] == DBNull.Value) { tx_mtable_BDDTS.Text = ""; } else { tx_mtable_BDDTS.Text = DAC.GetDateTimeValue(CurRow["mtable_BDDTS"]).ToString(sys_DateFormat); }  //開始日期
      if (CurRow["mtable_BDDTE"] == DBNull.Value) { tx_mtable_BDDTE.Text = ""; } else { tx_mtable_BDDTE.Text = DAC.GetDateTimeValue(CurRow["mtable_BDDTE"]).ToString(sys_DateFormat); }  //結束日期
      tx_mtable_BDNUM.Text = DAC.GetStringValue(CurRow["mtable_BDNUM"]);  //出版社號
      tx_bdlr_BDNAM.Text = DAC.GetStringValue(CurRow["bdlr_BDNAM"]);  //出版社名
      tx_mtable_BDPTN.Text = DAC.GetStringValue(CurRow["mtable_BDPTN"]);  //商品編號
      tx_bpud_BPTNA.Text = DAC.GetStringValue(CurRow["bpud_BPTNA"]);  //商品名稱
      tx_mtable_BDQTY.Text = DAC.GetStringValue(CurRow["mtable_BDQTY"]);  //預付本數
      tx_mtable_BDDE1.Text = DAC.GetStringValue(CurRow["mtable_BDDE1"]);  //定價金額
      tx_mtable_BDRAT.Text = DAC.GetStringValue(CurRow["mtable_BDRAT"]);  //預付比率
      dr_mtable_BDTXP = sFN.SetDropDownList(ref dr_mtable_BDTXP, DAC.GetStringValue(CurRow["mtable_BDTXP"]));  //計稅方式
      //tx_mtable_BDAMT.Text = DAC.GetStringValue(CurRow["mtable_BDAMT"]);  //預付金額
      //if (CurRow["mtable_BDEDT"] == DBNull.Value) { tx_mtable_BDEDT.Text = ""; } else { tx_mtable_BDEDT.Text = DAC.GetDateTimeValue(CurRow["mtable_BDEDT"]).ToString(sys_DateFormat); }  //預付月份
      tx_mtable_BDRMK.Text = DAC.GetStringValue(CurRow["mtable_BDRMK"]);  //備　　註
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["mtable_mkey"]);
      hh_GridGkey.Value = DAC.GetStringValue(CurRow["mtable_gkey"]);
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
        WebDataGrid_dtable2.Rows.Clear();
        WebDataGrid_dtable2.DataBind();
        WebDataGrid_dtable1.Rows.Clear();
        WebDataGrid_dtable1.DataBind();
        //
        tx_mtable_BDDTS.Date = DateTime.Today;
        tx_mtable_BDCCN.Enabled = false;
        //tx_mtable_BDEDT.Date = DateTime.Today;
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
        hh_GridCtrl.Value = "mod_a";
        Set_Control_A();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        ShowOneRow_A(hh_mkey.Value);
        SetEditMod_A();
        tx_mtable_BDCCN.Enabled = false;
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
      Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, !bl_resetKey);
      Bind_WebDataGrid_A(WebDataGrid_mtable_ba, !bl_showRowA, !bl_resetKey);
      //
      ShowOneRow_A(hh_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_mtable_ba, "mtable_mkey", hh_mkey.Value);
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
        string st_nextkey = sFN.WebDataGrid_NextKey(WebDataGrid_mtable_ba, "mtable_mkey", hh_mkey.Value);
        //
        DAC bcvwDao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "mtable_gkey='" + hh_GridGkey.Value + "' and mtable_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_bcvw = new DataTable();
        OleDbConnection connD = new OleDbConnection();
        connD = DAC.NewReaderConnr();
        connD.Open();
        DbDataAdapter da_ADP = bcvwDao.GetDataAdapter(ApVer, "UNmtable", "mtable", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "", "SEL DEL ");
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
            bcvwDao.Insertbalog(connD, thistran, "mtable", hh_ActKey.Value, hh_GridGkey.Value);
            bcvwDao.Insertbtlog(connD, thistran, "mtable", DAC.GetStringValue(DelRow[0]["mtable_BDCCN"]), "D", UserName, DAC.GetStringValue(DelRow[0]["mtable_gkey"]));
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
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_mtable_ba, "mtable_mkey", hh_mkey.Value);
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
        DAC_mtable mtableDao = new DAC_mtable(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "mtable_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins_a")
          {
            //自動編號
            st_ren_yymmtext = sFN.strzeroi(tx_mtable_BDDTS.Date.Year, 4) + sFN.strzeroi(tx_mtable_BDDTS.Date.Month, 2);
            st_ren_cls = st_ren_yymmtext;
            tx_mtable_BDCCN.Text = mtableDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            conn.Close();
            //檢查重複
            if (mtableDao.IsExists("mtable", "BDCCN", tx_mtable_BDCCN.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_mtable_BDCCN.Text + ",已存在.");
              mtableDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_mtable_BDCCN.Text);
              st_dberrmsg = StringTable.GetString(tx_mtable_BDCCN.Text + ",已重新取號.");
              tx_mtable_BDCCN.Text = mtableDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_mtable_RIREN.Text ="";
            }
            else
            {
              conn.Open();
              DataTable tb_mtable_ins = new DataTable();
              DbDataAdapter da_ADP_ins = mtableDao.GetDataAdapter(ApVer, "UNmtable", "mtable", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_mtable_ins);
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_mtable_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["mtable_gkey"] = st_tempgkey;    // 
                ins_row["mtable_mkey"] = st_tempgkey;    //
                //
                ins_row["mtable_BDCCN"] = tx_mtable_BDCCN.Text.Trim();       //  
                if (tx_mtable_BDDTS.Text.Trim() == "") { ins_row["mtable_BDDTS"] = DBNull.Value; } else { ins_row["mtable_BDDTS"] = sFN.DateStringToDateTime(tx_mtable_BDDTS.Text); }       //開始日期
                if (tx_mtable_BDDTE.Text.Trim() == "") { ins_row["mtable_BDDTE"] = DBNull.Value; } else { ins_row["mtable_BDDTE"] = sFN.DateStringToDateTime(tx_mtable_BDDTE.Text); }       //結束日期
                ins_row["mtable_BDNUM"] = tx_mtable_BDNUM.Text.Trim();       // 出版社號
                ins_row["mtable_BDPTN"] = tx_mtable_BDPTN.Text.Trim();       // 商品編號
                ins_row["mtable_BDQTY"] = tx_mtable_BDQTY.Text.Trim();       // 預付本數
                ins_row["mtable_BDDE1"] = tx_mtable_BDDE1.Text.Trim();       // 定價金額
                ins_row["mtable_BDRAT"] = tx_mtable_BDRAT.Text.Trim();       // 預付比率
                ins_row["mtable_BDTXP"] = dr_mtable_BDTXP.SelectedValue;       // 計稅方式
                //
                //tx_mtable_BDAMT.ValueDecimal =sFN.Round(  tx_mtable_BDDE1.ValueDecimal * tx_mtable_BDQTY.ValueDecimal* tx_mtable_BDRAT.ValueDecimal /100,0);
                ins_row["mtable_BDAMT"] = 0;       // 預付金額
                //if (tx_mtable_BDEDT.Text.Trim() == "") { ins_row["mtable_BDEDT"] = DBNull.Value; } else { ins_row["mtable_BDEDT"] = sFN.DateStringToDateTime(tx_mtable_BDEDT.Text); }       //預付月份
                ins_row["mtable_BDEDT"] = DBNull.Value; // 預付金額
                ins_row["mtable_BDRMK"] = tx_mtable_BDRMK.Text.Trim();       // 備　　註
                ins_row["mtable_trusr"] = UserGkey;  //
                tb_mtable_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_mtable_ins);
                mtableDao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_mtable_BDCCN.Text.Trim());
                mtableDao.Insertbalog(conn, thistran, "mtable", hh_ActKey.Value, hh_GridGkey.Value);
                mtableDao.Insertbtlog(conn, thistran, "mtable", DAC.GetStringValue(ins_row["mtable_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["mtable_gkey"]));
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
                mtableDao.Dispose();
                tb_mtable_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_mtable_ba, !bl_showRowA, !bl_resetKey);
              //
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_mtable_ba, "mtable_mkey", hh_mkey.Value);
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
            if (mtableDao.IsExists("mtable", "BDCCN", tx_mtable_BDCCN.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_mtable_BDCCN.Text + ",已存在.");
            }
            else
            {
              DataTable tb_mtable_mod = new DataTable();
              DbDataAdapter da_ADP_mod = mtableDao.GetDataAdapter(ApVer, "UNmtable", "mtable", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_mtable_mod);
              st_SelDataKey = "mtable_gkey='" + hh_GridGkey.Value + "' and mtable_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_mtable_mod.Select(st_SelDataKey);
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
                  if (tx_mtable_BDDTS.Text.Trim() == "") { mod_row["mtable_BDDTS"] = DBNull.Value; } else { mod_row["mtable_BDDTS"] = sFN.DateStringToDateTime(tx_mtable_BDDTS.Text); }       //開始日期
                  if (tx_mtable_BDDTE.Text.Trim() == "") { mod_row["mtable_BDDTE"] = DBNull.Value; } else { mod_row["mtable_BDDTE"] = sFN.DateStringToDateTime(tx_mtable_BDDTE.Text); }       //結束日期
                  mod_row["mtable_BDNUM"] = tx_mtable_BDNUM.Text.Trim();       // 出版社號
                  mod_row["mtable_BDPTN"] = tx_mtable_BDPTN.Text.Trim();       // 商品編號
                  mod_row["mtable_BDQTY"] = tx_mtable_BDQTY.Text.Trim();       // 預付本數
                  mod_row["mtable_BDDE1"] = tx_mtable_BDDE1.Text.Trim();       // 定價金額
                  mod_row["mtable_BDRAT"] = tx_mtable_BDRAT.Text.Trim();       // 預付比率
                  mod_row["mtable_BDTXP"] = dr_mtable_BDTXP.SelectedValue;       // 計稅方式
                  //tx_mtable_BDAMT.ValueDecimal = sFN.Round(tx_mtable_BDDE1.ValueDecimal * tx_mtable_BDQTY.ValueDecimal * tx_mtable_BDRAT.ValueDecimal / 100, 0);
                  mod_row["mtable_BDAMT"] = 0;       // 預付金額
                  //if (tx_mtable_BDEDT.Text.Trim() == "") { mod_row["mtable_BDEDT"] = DBNull.Value; } else { mod_row["mtable_BDEDT"] = sFN.DateStringToDateTime(tx_mtable_BDEDT.Text); }       //預付月份
                  mod_row["mtable_BDEDT"] = DBNull.Value; //預付月份
                  mod_row["mtable_BDRMK"] = tx_mtable_BDRMK.Text.Trim();       // 備　　註
                  mod_row["mtable_mkey"] = st_tempgkey;        //
                  mod_row["mtable_trusr"] = UserGkey;  //

                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_mtable_mod);
                  mtableDao.Insertbalog(conn, thistran, "mtable", hh_ActKey.Value, hh_GridGkey.Value);
                  mtableDao.Insertbtlog(conn, thistran, "mtable", DAC.GetStringValue(mod_row["mtable_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["mtable_gkey"]));
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
                  mtableDao.Dispose();
                  tb_mtable_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_mtable, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_mtable_ba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_mtable_ba, "mtable_mkey", hh_mkey.Value);
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
        mtableDao.Dispose();
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
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_mtable_BDDTS.Text, lb_mtable_BDDTS.Text, ref sMsg, LangType, sFN); //開始日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_mtable_BDDTE.Text, lb_mtable_BDDTE.Text, ref sMsg, LangType, sFN); //結束日期
      ret = DataCheck.cIsStrEmptyChk(ret, tx_mtable_BDNUM.Text, lb_mtable_BDNUM.Text, ref sMsg, LangType, sFN);  //出版社號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_mtable_BDPTN.Text, lb_mtable_BDPTN.Text, ref sMsg, LangType, sFN);  //商品編號
      //ret = DataCheck.cIsStrEmptyChk(ret, tx_mtable_BDEDT.Text, lb_mtable_BDEDT.Text, ref sMsg, LangType, sFN); //預付月份

      ret = DataCheck.cIsStrDecimalChk(ret, tx_mtable_BDQTY.Text, lb_mtable_BDQTY.Text, ref sMsg, LangType, sFN); //預付本數
      ret = DataCheck.cIsStrDecimalChk(ret, tx_mtable_BDDE1.Text, lb_mtable_BDDE1.Text, ref sMsg, LangType, sFN); //定價金額
      ret = DataCheck.cIsStrDecimalChk(ret, tx_mtable_BDRAT.Text, lb_mtable_BDRAT.Text, ref sMsg, LangType, sFN); //預付比率
      //ret = DataCheck.cIsStrDecimalChk(ret, tx_mtable_BDAMT.Text, lb_mtable_BDAMT.Text, ref sMsg, LangType, sFN); //預付金額
      //ret = DataCheck.cIsStrDatetimeChk(ret, tx_mtable_BDEDT.Text, lb_mtable_BDEDT.Text, ref sMsg, LangType, sFN); //預付月份
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
        CmdQueryS_ba = (OleDbCommand)Session["fmmtable_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ba.CommandText = " and 1=0 ";
      }
    }

    protected void Obj_mtable_ba_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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

    protected void WebDataGrid_mtable_ba_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      try
      {
        SetSerMod_B();
        hh_fun_name.Value = "showa";
        hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("mtable_mkey").Value);
        WebTab_form.SelectedIndex = 1;
        //
        ShowOneRow_A(hh_fun_mkey.Value);
        sFN.WebDataGrid_SelectRow(ref WebDataGrid_mtable_ba, "mtable_mkey", hh_fun_mkey.Value);
        SetSerMod_A();
      }
      catch
      {
      }
    }


    ///dtable2 control
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

    ///dtable1 control
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
          //hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "dtable2_gkey");
          //hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "dtable2_mkey");
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


    #region WebDataGrid_dtable2

    protected void WebDataGrid_dtable2_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      //string st_key = e.CurrentSelectedRows[0].DataKey[0].ToString();
    }

    protected void WebDataGrid_dtable2_RowAdding(object sender, Infragistics.Web.UI.GridControls.RowAddingEventArgs e)
    {
      //string st_mkey="";
      //st_mkey=DAC.GetStringValue(e.Values["dtable2_mkey"]);
      if (clsFN.chkLoginState())
      {
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_dtable2_RowAdded(object sender, Infragistics.Web.UI.GridControls.RowAddedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable2.CustomAJAXResponse.Message = e.Exception.InnerException.Message;
      }
      else
      {
        //有新資料,設到最後的Page
        try
        {
          WebDataGrid_dtable2.Behaviors.Paging.PageIndex = WebDataGrid_dtable2.Behaviors.Paging.PageCount - 1;
        }
        catch
        {
        }
      }
    }

    protected void WebDataGrid_dtable2_RowUpdating(object sender, Infragistics.Web.UI.GridControls.RowUpdatingEventArgs e)
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

    protected void WebDataGrid_dtable2_RowUpdated(object sender, Infragistics.Web.UI.GridControls.RowUpdatedEventArgs e)
    {

    }

    #endregion

    #region Obj_dtable2

    protected void Obj_dtable2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        //mkey 轉單號
        string st_ren = "";
        st_ren = sFN.GetRenFromGkey("mtable", "BDCCN", "mkey", hh_fun_mkey.Value);
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

    protected void Obj_dtable2_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["dtable2_BDCCN"] = tx_mtable_BDCCN.Text;
        e.InputParameters["dtable2_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_dtable2_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable2.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }



    protected void Obj_dtable2_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["dtable2_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_dtable2_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable2.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_dtable2_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["dtable2_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_dtable2_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable2.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }
    #endregion



    #region WebDataGrid_dtable1

    protected void WebDataGrid_dtable1_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      //string st_key = e.CurrentSelectedRows[0].DataKey[0].ToString();
    }

    protected void WebDataGrid_dtable1_RowAdding(object sender, Infragistics.Web.UI.GridControls.RowAddingEventArgs e)
    {
      //string st_mkey="";
      //st_mkey=DAC.GetStringValue(e.Values["dtable1_mkey"]);
      if (clsFN.chkLoginState())
      {
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_dtable1_RowAdded(object sender, Infragistics.Web.UI.GridControls.RowAddedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable1.CustomAJAXResponse.Message = e.Exception.InnerException.Message;
      }
      else
      {
        //有新資料,設到最後的Page
        try
        {
          WebDataGrid_dtable1.Behaviors.Paging.PageIndex = WebDataGrid_dtable1.Behaviors.Paging.PageCount - 1;
        }
        catch
        {
        }
      }

    }

    protected void WebDataGrid_dtable1_RowUpdating(object sender, Infragistics.Web.UI.GridControls.RowUpdatingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        string st_BDCCN = DAC.GetStringValue(e.Values["dtable1_BDCCN"]);
        string st_BDAUR = DAC.GetStringValue(e.Values["dtable1_BDAUR"]);
        string st_gkey = DAC.GetStringValue(e.Values["dtable1_gkey"]);
        OleDbCommand Qcheck = new OleDbCommand();
        Qcheck.Parameters.Clear();
        Qcheck.CommandText = " and BDCCN=? and BDAUR=? and gkey!=? ";
        DAC.AddParam(Qcheck, "dtable1_BDCCN", st_BDCCN);
        DAC.AddParam(Qcheck, "dtable1_BDAUR", st_BDAUR);
        DAC.AddParam(Qcheck, "dtable1_gkey", st_gkey);
        if (sFN.lookups("select gkey from  dtable1 ", Qcheck, "gkey") != "")
        {
          e.Cancel = true;
          WebDataGrid_dtable1.CustomAJAXResponse.Message = "資料已存在!";
          //Response.Redirect("fm_mtable.aspx");  
        }
        Qcheck.Dispose();
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }


    }

    protected void WebDataGrid_dtable1_RowUpdated(object sender, Infragistics.Web.UI.GridControls.RowUpdatedEventArgs e)
    {

    }
    #endregion

    #region Obj_dtable1

    protected void Obj_dtable1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        //mkey 轉單號
        string st_ren = "";
        st_ren = sFN.GetRenFromGkey("mtable", "BDCCN", "mkey", hh_fun_mkey.Value);
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


    protected void Obj_dtable1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if ((DAC.GetStringValue(e.InputParameters["dtable1_BDAUR"]) != "") && (e.InputParameters["dtable1_BDAUR"] != null))
        {
          e.InputParameters["dtable1_BDCCN"] = tx_mtable_BDCCN.Text;
          e.InputParameters["dtable1_actkey"] = DAC.get_guidkey();
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

    protected void Obj_dtable1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable1.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_dtable1_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["dtable1_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_dtable1_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable1.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    protected void Obj_dtable1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if ((DAC.GetStringValue(e.InputParameters["dtable1_BDAUR"]) != "") && (e.InputParameters["dtable1_BDAUR"] != null))
        {
          e.InputParameters["dtable1_actkey"] = DAC.get_guidkey();
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

    protected void Obj_dtable1_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_dtable1.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    #endregion

    protected void bt_MOD_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      SetEditMod_B();
    }

    protected void bt_SAVE_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      if (WebTabGrid.SelectedIndex == 0)
      {
        Bind_WebDataGrid_B(WebDataGrid_dtable1, bl_resetKey);
        SetSerMod_B();
      }
      else if (WebTabGrid.SelectedIndex == 1)
      {
        Bind_WebDataGrid_B(WebDataGrid_dtable2, bl_resetKey);
        SetSerMod_B();
      }
    }

    private void SetSerMod_B()
    {
      if (WebTabGrid.SelectedIndex == 0)
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_dtable1, false);
      }
      else if (WebTabGrid.SelectedIndex == 1)
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_dtable2, false);
      }
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
      WebDataGrid_mtable.Enabled = true;
      WebDataGrid_mtable_ba.Enabled = true;
      PanBtns.Enabled = true;
    }
    private void SetEditMod_B()
    {
      if (WebTabGrid.SelectedIndex == 0)
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_dtable1, true);
        bt_New_B.ClientSideEvents.Click = "webDataGrid_dtable1_AddRow('" + WebDataGrid_dtable1.ClientID + "','dtable1_BDAUR');";
      }
      else if (WebTabGrid.SelectedIndex == 1)
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_dtable2, true);
        bt_New_B.ClientSideEvents.Click = "webDataGrid_dtable2_AddRow('" + WebDataGrid_dtable2.ClientID + "','dtable2_BDQTY');";
      }
      //
      bt_MOD_B = sFN.SetWebImageButtonDetail(bt_MOD_B);
      bt_MOD_B.Visible = false;
      //
      bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
      bt_New_B.Visible = true;
      //
      bt_SAVE_B = sFN.SetWebImageButtonDetail(bt_SAVE_B);
      bt_SAVE_B.Visible = true;

      //
      WebDataGrid_mtable.Enabled = false;
      WebDataGrid_mtable_ba.Enabled = false;
      PanBtns.Enabled = false;
    }

  }
}