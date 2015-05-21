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
  public partial class fm_rmsa : FormBase
  {

    public string st_object_func = "UNrmsa";
    public string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    public string st_ContentPlaceHolderEdit = "ctl00$ContentPlaceHolder1$WebTab_form$tmpl1$";
    bool bl_resetKey = true;
    bool bl_showRowA = true;
    //
    string st_dd_apx = "UNrmsa";         //UNdcnews   與apx 相關
    //string st_dd_table = "rmsa";         //dcnews     與table 相關 
    string st_ren_head = "CA";            //DC         與單號相關 
    string st_ren_yymmtext = "YYYYMM";     //"         與單號相關 
    string st_ren_cls = "rmsa";        //ren        與單號cls相關 
    string st_ren_cos = "1";        //1          與單號cos相關 
    int in_ren_len = 4;            //6          與單號流水號 
    //
    private OleDbCommand CmdQueryS_ba = new OleDbCommand();
    private OleDbCommand CmdQueryS_b = new OleDbCommand();
    private OleDbCommand CmdQueryS_c = new OleDbCommand();
    //

    #region Page_Load__SetControl

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
          dr_rmsa_RDKD1 = sFN.DropDownListFromTable(ref dr_rmsa_RDKD1, "pdrmakd1", "BKNUM", "BKNAM", "", "BKNUM");
          dr_rmsa_RDKD2 = sFN.DropDownListFromTable(ref dr_rmsa_RDKD2, "pdrmakd2", "BKNUM", "BKNAM", "", "BKNUM");
          dr_rmsa_RDKD3 = sFN.DropDownListFromTable(ref dr_rmsa_RDKD3, "pdrmakd3", "BKNUM", "BKNAM", "", "BKNUM");
          //
          WebTab_form.SelectedIndex = 0;
          Set_Control_A();
          //
          tx_rmsa_RDDAT_s1.Value = DateTime.Today.AddDays(-7);
          tx_rmsa_RDDAT_s2.Value = DateTime.Today;
          //
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsb, false);
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsc, false);
          this.Form.Attributes.Add("onkeydown", "do_Keydown_EnterToTab();");    //SiteMM.Master中輸入Enter轉Tab
          SetSerMod_B();
          //
          if (Session["fmrmsa_CmdQueryS"] == null)
          {
            act_SERS_L();
          }
          else
          {
            get_CmdQueryS();
            Obj_rmsa.TypeName = "DD2015_45.DAC_rmsa";
            Obj_rmsa.SelectMethod = "SelectTable_rmsa";
            WebDataGrid_rmsa.DataSourceID = "Obj_rmsa";
            Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, bl_resetKey); //reset gkey,mkey
            //
            get_CmdQueryS_ba();
            Obj_rmsa_ba.TypeName = "DD2015_45.DAC_rmsa";
            Obj_rmsa_ba.SelectMethod = "SelectTable_rmsa_ba";
            WebDataGrid_rmsa_ba.DataSourceID = "Obj_rmsa_ba";
            Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
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
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_rmsa_ba, "rmsa_mkey", hh_fun_mkey.Value);
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

    private void Set_Control_A()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      WebDataGrid_rmsa.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      WebDataGrid_rmsa_ba.Behaviors.Paging.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      //sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolderEdit, ApVer, "UNrmsa", "rmsa");
      sFN.SetFormControlsText(this.Form, PublicVariable.LangType, ApVer, "UNrmsa", "rmsa");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rmsa, PublicVariable.LangType, ApVer, "UNrmsa", "rmsa");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rmsa_ba, PublicVariable.LangType, ApVer, "UNrmsa", "rmsa");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rmsb, PublicVariable.LangType, ApVer, "UNrmsb", "rmsb");
      sFN.SetWebDataGridHeadText(ref WebDataGrid_rmsc, PublicVariable.LangType, ApVer, "UNrmsc", "rmsc");
    }

    #endregion

    #region CmdQueryS
    private void get_CmdQueryS()
    {
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmrmsa_CmdQueryS"];
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
      if ((tx_rmsa_RDDAT_s1.Text != "") && (tx_rmsa_RDDAT_s2.Text != ""))
      {
        CmdQueryS.CommandText += " and a.RDDAT >=?  and a.RDDAT <=? ";
        DAC.AddParam(CmdQueryS, "rmsa_RDDAT_s1", tx_rmsa_RDDAT_s1.Date);
        DAC.AddParam(CmdQueryS, "rmsa_RDDAT_s2", tx_rmsa_RDDAT_s2.Date);
      }
      else if (tx_rmsa_RDDAT_s1.Text != "")
      {
        CmdQueryS.CommandText += " and a.RDDAT >=?  ";
        DAC.AddParam(CmdQueryS, "rmsa_RDDAT_s1", tx_rmsa_RDDAT_s1.Date);
      }
      else if (tx_rmsa_RDDAT_s2.Text != "")
      {
        CmdQueryS.CommandText += " and a.RDDAT <=?  ";
        DAC.AddParam(CmdQueryS, "rmsa_RDDAT_s2", tx_rmsa_RDDAT_s2.Date);
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
        if (Session["fmrmsa_CmdQueryS"] == null)
        {
          reset_CmdQueryS_comm();
          //
          Session["fmrmsa_CmdQueryS"] = CmdQueryS;
          Obj_rmsa.TypeName = "DD2015_45.DAC_rmsa";
          Obj_rmsa.SelectMethod = "SelectTable_rmsa";
          WebDataGrid_rmsa.DataSourceID = "Obj_rmsa";
          Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_rmsa_ba.TypeName = "DD2015_45.DAC_rmsa";
          Obj_rmsa_ba.SelectMethod = "SelectTable_rmsa_ba";
          WebDataGrid_rmsa_ba.DataSourceID = "Obj_rmsa_ba";
          Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
          //
        }
        else
        {
          get_CmdQueryS();
          Session["fmrmsa_CmdQueryS"] = CmdQueryS;
          Obj_rmsa.TypeName = "DD2015_45.DAC_rmsa";
          Obj_rmsa.SelectMethod = "SelectTable_rmsa";
          WebDataGrid_rmsa.DataSourceID = "Obj_rmsa";
          Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
          get_CmdQueryS_ba();
          Obj_rmsa_ba.TypeName = "DD2015_45.DAC_rmsa";
          Obj_rmsa_ba.SelectMethod = "SelectTable_rmsa_ba";
          WebDataGrid_rmsa_ba.DataSourceID = "Obj_rmsa_ba";
          Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, !bl_showRowA, !bl_resetKey); //do'nt reset gkey,mkey
          //
        }
      }
      catch
      {
        reset_CmdQueryS_comm();
        //
        Session["fmrmsa_CmdQueryS"] = CmdQueryS;
        Obj_rmsa.TypeName = "DD2015_45.DAC_rmsa";
        Obj_rmsa.SelectMethod = "SelectTable_rmsa";
        WebDataGrid_rmsa.DataSourceID = "Obj_rmsa";
        Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, bl_resetKey); //reset gkey,mkey
        //
        Obj_rmsa_ba.TypeName = "DD2015_45.DAC_rmsa";
        Obj_rmsa_ba.SelectMethod = "SelectTable_rmsa_ba";
        WebDataGrid_rmsa_ba.DataSourceID = "Obj_rmsa_ba";
        Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      }
      //
    }
    #endregion

    #region Bind_WebDataGrid_A
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
          hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rmsa_gkey");
          hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rmsa_mkey");
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

    #endregion

    #region bt_SER_A

    protected void bt_08_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      act_SERS();
    }

    protected void act_SERS()
    {
      reset_CmdQueryS_comm();
      //
      Session["fmrmsa_CmdQueryS"] = CmdQueryS;
      Obj_rmsa.TypeName = "DD2015_45.DAC_rmsa";
      Obj_rmsa.SelectMethod = "SelectTable_rmsa";
      WebDataGrid_rmsa.DataSourceID = "Obj_rmsa";
      Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, bl_resetKey); //reset gkey,mkey
      //
      get_CmdQueryS_ba();
      Obj_rmsa_ba.TypeName = "DD2015_45.DAC_rmsa";
      Obj_rmsa_ba.SelectMethod = "SelectTable_rmsa_ba";
      WebDataGrid_rmsa_ba.DataSourceID = "Obj_rmsa_ba";
      Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, bl_showRowA, bl_resetKey); //reset gkey,mkey
    }

    #endregion

    #region Obj_rmsa
    protected void Obj_rmsa_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
        e.InputParameters["st_orderKey"] = " A.RDDAT DESC,A.RDREN ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }
    #endregion

    #region Set_Text

    private void ClearText_A()
    {
      tx_rmsa_RDREN.Text = "";  //訊息編號
      tx_rmsa_RDDAT.Text = "";  //訊息日期
      ck_rmsa_RDALT.Checked = false;  //ALTER
      tx_rmsa_RDENO.Text = "";  //員工編號
      tx_es101_RDENO.Text = "";  //員工名稱
      tx_rmsa_RDNUM.Text = "";  //經銷編號
      tx_bdlr_RDNUM.Text = "";  //經銷名稱
      tx_rmsa_RDCUS.Text = "";  //會員編號
      tx_bcvw_RDCUS.Text = "";  //會員名稱
      tx_rmsa_RDTIL.Text = "";  //訊息TITLE
      dr_rmsa_RDKD1.SelectedIndex = -1;  //訊息類１
      dr_rmsa_RDKD2.SelectedIndex = -1;  //訊息類２
      dr_rmsa_RDKD3.SelectedIndex = -1;  //訊息類３
      tx_rmsa_RDPT1.Text = "";  //商品編號
      tx_bpud_RDPT1.Text = "";  //商品名稱
      ck_rmsa_RDOKE.Checked = false;  //完成
      tx_rmsa_RDOKM.Text = "";  //確認人員
      tx_es101_RDOKM.Text = "";  //確認人員名
      tx_rmsa_RDOKD.Text = "";  //確認日期
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetSerMod_A()
    {
      clsGV.SetTextBoxEditAlert(ref lb_rmsa_RDREN, ref tx_rmsa_RDREN, false);  //訊息編號
      clsGV.SetTextBoxEditAlert(ref lb_rmsa_RDDAT, ref tx_rmsa_RDDAT, false);  //訊息日期
      //
      clsGV.TextBox_Set(ref tx_rmsa_RDREN, false);   //訊息編號
      clsGV.TextBox_Set(ref tx_rmsa_RDDAT, false);   //訊息日期
      ck_rmsa_RDALT.Enabled = false;  //ALTER
      clsGV.TextBox_Set(ref tx_rmsa_RDENO, false);   //員工編號
      clsGV.TextBox_Set(ref tx_es101_RDENO, false);   //員工名稱
      clsGV.TextBox_Set(ref tx_rmsa_RDNUM, false);   //經銷編號
      clsGV.TextBox_Set(ref tx_bdlr_RDNUM, false);   //經銷名稱
      clsGV.TextBox_Set(ref tx_rmsa_RDCUS, false);   //會員編號
      clsGV.TextBox_Set(ref tx_bcvw_RDCUS, false);   //會員名稱
      clsGV.TextBox_Set(ref tx_rmsa_RDTIL, false);   //訊息TITLE
      clsGV.Drpdown_Set(ref dr_rmsa_RDKD1, ref tx_rmsa_RDKD1, false);   //訊息類１
      clsGV.Drpdown_Set(ref dr_rmsa_RDKD2, ref tx_rmsa_RDKD2, false);   //訊息類２
      clsGV.Drpdown_Set(ref dr_rmsa_RDKD3, ref tx_rmsa_RDKD3, false);   //訊息類３
      clsGV.TextBox_Set(ref tx_rmsa_RDPT1, false);   //商品編號
      clsGV.TextBox_Set(ref tx_bpud_RDPT1, false);   //商品名稱
      ck_rmsa_RDOKE.Enabled = false;  //完成
      clsGV.TextBox_Set(ref tx_rmsa_RDOKM, false);   //確認人員
      clsGV.TextBox_Set(ref tx_es101_RDOKM, false);   //確認人員名
      clsGV.TextBox_Set(ref tx_rmsa_RDOKD, false);   //確認日期
      //
      clsGV.SetControlShowAlert(ref lb_rmsa_RDREN, ref tx_rmsa_RDREN, true);  //訊息編號
      clsGV.SetControlShowAlert(ref lb_rmsa_RDDAT, ref tx_rmsa_RDDAT, true);  //訊息日期
      //
      tx_rmsa_RDENO.Attributes.Remove("onblur");   //員工編號
      tx_rmsa_RDNUM.Attributes.Remove("onblur");   //經銷編號
      tx_rmsa_RDCUS.Attributes.Remove("onblur");   //會員編號
      tx_rmsa_RDPT1.Attributes.Remove("onblur");   //商品編號
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      SetSerMod_B();
      //
      WebDataGrid_rmsa.Enabled = true;
      WebDataGrid_rmsa_ba.Enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetEditMod_A()
    {
      //
      clsGV.TextBox_Set(ref tx_rmsa_RDREN, true);  //訊息編號
      clsGV.TextBox_Set(ref tx_rmsa_RDDAT, true);  //訊息日期
      ck_rmsa_RDALT.Enabled = true;  //ALTER
      clsGV.TextBox_Set(ref tx_rmsa_RDENO, true);  //員工編號
      clsGV.TextBox_Set(ref tx_es101_RDENO, true);  //員工名稱
      clsGV.TextBox_Set(ref tx_rmsa_RDNUM, true);  //經銷編號
      clsGV.TextBox_Set(ref tx_bdlr_RDNUM, true);  //經銷名稱
      clsGV.TextBox_Set(ref tx_rmsa_RDCUS, true);  //會員編號
      clsGV.TextBox_Set(ref tx_bcvw_RDCUS, true);  //會員名稱
      clsGV.TextBox_Set(ref tx_rmsa_RDTIL, true);  //訊息TITLE
      clsGV.Drpdown_Set(ref dr_rmsa_RDKD1, ref tx_rmsa_RDKD1, true);   //訊息類１
      clsGV.Drpdown_Set(ref dr_rmsa_RDKD2, ref tx_rmsa_RDKD2, true);   //訊息類２
      clsGV.Drpdown_Set(ref dr_rmsa_RDKD3, ref tx_rmsa_RDKD3, true);   //訊息類３
      clsGV.TextBox_Set(ref tx_rmsa_RDPT1, true);  //商品編號
      clsGV.TextBox_Set(ref tx_bpud_RDPT1, true);  //商品名稱
      ck_rmsa_RDOKE.Enabled = true;  //完成
      clsGV.TextBox_Set(ref tx_rmsa_RDOKM, true);  //確認人員
      clsGV.TextBox_Set(ref tx_es101_RDOKM, true);  //確認人員名
      clsGV.TextBox_Set(ref tx_rmsa_RDOKD, true);  //確認日期
      //
      clsGV.SetTextBoxEditAlert(ref lb_rmsa_RDREN, ref tx_rmsa_RDREN, true);  //訊息編號
      clsGV.SetTextBoxEditAlert(ref lb_rmsa_RDDAT, ref tx_rmsa_RDDAT, true);  //訊息日期
      //
      tx_rmsa_RDNUM.Attributes.Add("onblur", "return get_bdlr_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_rmsa_RDNUM','" + st_ContentPlaceHolderEdit + "tx_bdlr_RDNUM'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bdlr.aspx" + "','" + "經銷商資料" + "')");
      tx_rmsa_RDPT1.Attributes.Add("onblur", "return get_bpud_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_rmsa_RDPT1','" + st_ContentPlaceHolderEdit + "tx_bpud_RDPT1'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bpud.aspx" + "','" + "商品資料" + "')");
      tx_rmsa_RDENO.Attributes.Add("onblur", "return get_es101_cname('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_rmsa_RDENO','" + st_ContentPlaceHolderEdit + "tx_es101_RDENO'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_es101.aspx" + "','" + "員工資料" + "')");
      tx_rmsa_RDCUS.Attributes.Add("onblur", "return get_bcvw_cname_ri('tx','" + st_ContentPlaceHolderEdit + "','" + st_ContentPlaceHolderEdit + "tx_rmsa_RDCUS','" + st_ContentPlaceHolderEdit + "tx_bcvw_RDCUS'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bcvw.aspx" + "','" + "會員資料" + "')");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      SetSerMod_B();
      //
      WebDataGrid_rmsa.Enabled = false;
      WebDataGrid_rmsa_ba.Enabled = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CurRow"></param>
    private void BindText_A(DataRow CurRow)
    {
      //
      tx_rmsa_RDREN.Text = DAC.GetStringValue(CurRow["rmsa_RDREN"]);  //訊息編號
      if (CurRow["rmsa_RDDAT"] == DBNull.Value) { tx_rmsa_RDDAT.Text = ""; } else { tx_rmsa_RDDAT.Text = DAC.GetDateTimeValue(CurRow["rmsa_RDDAT"]).ToString(sys_DateFormat); }  //訊息日期
      ck_rmsa_RDALT.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["rmsa_RDALT"]));  //ALTER
      tx_rmsa_RDENO.Text = DAC.GetStringValue(CurRow["rmsa_RDENO"]);  //員工編號
      tx_es101_RDENO.Text = DAC.GetStringValue(CurRow["es101_RDENO"]);  //員工名稱
      tx_rmsa_RDNUM.Text = DAC.GetStringValue(CurRow["rmsa_RDNUM"]);  //經銷編號
      tx_bdlr_RDNUM.Text = DAC.GetStringValue(CurRow["bdlr_RDNUM"]);  //經銷名稱
      tx_rmsa_RDCUS.Text = DAC.GetStringValue(CurRow["rmsa_RDCUS"]);  //會員編號
      tx_bcvw_RDCUS.Text = DAC.GetStringValue(CurRow["bcvw_RDCUS"]);  //會員名稱
      tx_rmsa_RDTIL.Text = DAC.GetStringValue(CurRow["rmsa_RDTIL"]);  //訊息TITLE
      dr_rmsa_RDKD1 = sFN.SetDropDownList(ref dr_rmsa_RDKD1, DAC.GetStringValue(CurRow["rmsa_RDKD1"]));  //訊息類１
      dr_rmsa_RDKD2 = sFN.SetDropDownList(ref dr_rmsa_RDKD2, DAC.GetStringValue(CurRow["rmsa_RDKD2"]));  //訊息類２
      dr_rmsa_RDKD3 = sFN.SetDropDownList(ref dr_rmsa_RDKD3, DAC.GetStringValue(CurRow["rmsa_RDKD3"]));  //訊息類３
      tx_rmsa_RDPT1.Text = DAC.GetStringValue(CurRow["rmsa_RDPT1"]);  //商品編號
      tx_bpud_RDPT1.Text = DAC.GetStringValue(CurRow["bpud_RDPT1"]);  //商品名稱
      ck_rmsa_RDOKE.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["rmsa_RDOKE"]));  //完成
      tx_rmsa_RDOKM.Text = DAC.GetStringValue(CurRow["rmsa_RDOKM"]);  //確認人員
      tx_es101_RDOKM.Text = DAC.GetStringValue(CurRow["es101_RDOKM"]);  //確認人員名
      if (CurRow["rmsa_RDOKD"] == DBNull.Value) { tx_rmsa_RDOKD.Text = ""; } else { tx_rmsa_RDOKD.Text = DAC.GetDateTimeValue(CurRow["rmsa_RDOKD"]).ToString(sys_DateFormat); }  //確認日期
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["rmsa_mkey"]);
      hh_GridGkey.Value = DAC.GetStringValue(CurRow["rmsa_gkey"]);
    }


    #endregion



    #region bt_NEW_A
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
        WebDataGrid_rmsb.Rows.Clear();
        WebDataGrid_rmsb.DataBind();

        //WebDataGrid_rmsc.Rows.Clear();
        //WebDataGrid_rmsc.DataBind();
        //
        tx_rmsa_RDREN.Enabled = false;
        tx_rmsa_RDDAT.Date = DateTime.Today;
        //
      }
    }
    #endregion

    #region bt_MOD_A
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
        tx_rmsa_RDREN.Enabled = false;
      }
    }
    #endregion

    #region bt_CAN_A
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
      Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, !bl_resetKey);
      Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, !bl_showRowA, !bl_resetKey);
      //
      ShowOneRow_A(hh_mkey.Value);
      sFN.WebDataGrid_SelectRow(ref WebDataGrid_rmsa_ba, "rmsa_mkey", hh_mkey.Value);
      //
      SetSerMod_A();
    }
    #endregion

    #region bt_DEL_A
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
        string st_nextkey = sFN.WebDataGrid_NextKey(WebDataGrid_rmsa_ba, "rmsa_mkey", hh_mkey.Value);
        //
        DAC bcvwDao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "rmsa_gkey='" + hh_GridGkey.Value + "' and rmsa_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_bcvw = new DataTable();
        OleDbConnection connD = new OleDbConnection();
        connD = DAC.NewReaderConnr();
        connD.Open();
        DbDataAdapter da_ADP = bcvwDao.GetDataAdapter(ApVer, "UNrmsa", "rmsa", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "", "SEL DEL ");
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
            bcvwDao.Insertbalog(connD, thistran, "rmsa", hh_ActKey.Value, hh_GridGkey.Value);
            bcvwDao.Insertbtlog(connD, thistran, "rmsa", DAC.GetStringValue(DelRow[0]["rmsa_RDREN"]), "D", UserName, DAC.GetStringValue(DelRow[0]["rmsa_gkey"]));
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
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_rmsa_ba, "rmsa_mkey", hh_mkey.Value);
        }
      }

    }
    #endregion

    #region bt_SAV_A
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
        DAC_rmsa rmsaDao = new DAC_rmsa(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "rmsa_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins_a")
          {
            //自動編號
            st_ren_yymmtext = sFN.strzeroi(tx_rmsa_RDDAT.Date.Year, 4) + sFN.strzeroi(tx_rmsa_RDDAT.Date.Month, 2);
            st_ren_cls = st_ren_yymmtext;
            tx_rmsa_RDREN.Text = rmsaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            conn.Close();
            //檢查重複
            if (rmsaDao.IsExists("rmsa", "RDREN", tx_rmsa_RDREN.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_rmsa_RDREN.Text + ",已存在.");
              rmsaDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_rmsa_RDREN.Text);
              st_dberrmsg = StringTable.GetString(tx_rmsa_RDREN.Text + ",已重新取號.");
              tx_rmsa_RDREN.Text = rmsaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_rmsa_RIREN.Text ="";
            }
            else
            {
              conn.Open();
              DataTable tb_rmsa_ins = new DataTable();
              DbDataAdapter da_ADP_ins = rmsaDao.GetDataAdapter(ApVer, "UNrmsa", "rmsa", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_rmsa_ins);
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_rmsa_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["rmsa_gkey"] = st_tempgkey;    // 
                ins_row["rmsa_mkey"] = st_tempgkey;    //
                //
                ins_row["rmsa_RDREN"] = tx_rmsa_RDREN.Text.Trim();       // 訊息編號
                if (tx_rmsa_RDDAT.Text.Trim() == "") { ins_row["rmsa_RDDAT"] = DBNull.Value; } else { ins_row["rmsa_RDDAT"] = sFN.DateStringToDateTime(tx_rmsa_RDDAT.Text); }       //訊息日期
                ins_row["rmsa_RDALT"] = ck_rmsa_RDALT.Checked ? "1" : "0";       // ALTER
                ins_row["rmsa_RDENO"] = tx_rmsa_RDENO.Text.Trim();       // 員工編號
                ins_row["rmsa_RDNUM"] = tx_rmsa_RDNUM.Text.Trim();       // 經銷編號
                ins_row["rmsa_RDCUS"] = tx_rmsa_RDCUS.Text.Trim();       // 會員編號
                ins_row["rmsa_RDTIL"] = tx_rmsa_RDTIL.Text.Trim();       // 訊息TITLE
                ins_row["rmsa_RDKD1"] = dr_rmsa_RDKD1.SelectedValue;       // 訊息類１
                ins_row["rmsa_RDKD2"] = dr_rmsa_RDKD2.SelectedValue;       // 訊息類２
                ins_row["rmsa_RDKD3"] = dr_rmsa_RDKD3.SelectedValue;       // 訊息類３
                ins_row["rmsa_RDPT1"] = tx_rmsa_RDPT1.Text.Trim();       // 商品編號
                ins_row["rmsa_RDOKE"] = ck_rmsa_RDOKE.Checked ? "1" : "0";       // 完成
                ins_row["rmsa_RDOKM"] = tx_rmsa_RDOKM.Text.Trim();       // 確認人員
                if (tx_rmsa_RDOKD.Text.Trim() == "") { ins_row["rmsa_RDOKD"] = DBNull.Value; } else { ins_row["rmsa_RDOKD"] = sFN.DateStringToDateTime(tx_rmsa_RDOKD.Text); }       //確認日期

                //
                ins_row["rmsa_trusr"] = UserGkey;  //
                tb_rmsa_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_rmsa_ins);
                rmsaDao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_rmsa_RDREN.Text.Trim());
                rmsaDao.Insertbalog(conn, thistran, "rmsa", hh_ActKey.Value, hh_GridGkey.Value);
                rmsaDao.Insertbtlog(conn, thistran, "rmsa", DAC.GetStringValue(ins_row["rmsa_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["rmsa_gkey"]));
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
                rmsaDao.Dispose();
                tb_rmsa_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, !bl_showRowA, !bl_resetKey);
              //
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_rmsa_ba, "rmsa_mkey", hh_mkey.Value);
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
            if (rmsaDao.IsExists("rmsa", "RDREN", tx_rmsa_RDREN.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_rmsa_RDREN.Text + ",已存在.");
            }
            else
            {
              DataTable tb_rmsa_mod = new DataTable();
              DbDataAdapter da_ADP_mod = rmsaDao.GetDataAdapter(ApVer, "UNrmsa", "rmsa", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_rmsa_mod);
              st_SelDataKey = "rmsa_gkey='" + hh_GridGkey.Value + "' and rmsa_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_rmsa_mod.Select(st_SelDataKey);
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
                  mod_row["rmsa_RDALT"] = ck_rmsa_RDALT.Checked ? "1" : "0";       // ALTER
                  mod_row["rmsa_RDENO"] = tx_rmsa_RDENO.Text.Trim();       // 員工編號
                  mod_row["rmsa_RDNUM"] = tx_rmsa_RDNUM.Text.Trim();       // 經銷編號
                  mod_row["rmsa_RDCUS"] = tx_rmsa_RDCUS.Text.Trim();       // 會員編號
                  mod_row["rmsa_RDTIL"] = tx_rmsa_RDTIL.Text.Trim();       // 訊息TITLE
                  mod_row["rmsa_RDKD1"] = dr_rmsa_RDKD1.SelectedValue;       // 訊息類１
                  mod_row["rmsa_RDKD2"] = dr_rmsa_RDKD2.SelectedValue;       // 訊息類２
                  mod_row["rmsa_RDKD3"] = dr_rmsa_RDKD3.SelectedValue;       // 訊息類３
                  mod_row["rmsa_RDPT1"] = tx_rmsa_RDPT1.Text.Trim();       // 商品編號
                  mod_row["rmsa_RDOKE"] = ck_rmsa_RDOKE.Checked ? "1" : "0";       // 完成
                  mod_row["rmsa_RDOKM"] = tx_rmsa_RDOKM.Text.Trim();       // 確認人員
                  if (tx_rmsa_RDOKD.Text.Trim() == "") { mod_row["rmsa_RDOKD"] = DBNull.Value; } else { mod_row["rmsa_RDOKD"] = sFN.DateStringToDateTime(tx_rmsa_RDOKD.Text); }       //確認日期
                  mod_row["rmsa_mkey"] = st_tempgkey;        //
                  mod_row["rmsa_trusr"] = UserGkey;  //

                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_rmsa_mod);
                  rmsaDao.Insertbalog(conn, thistran, "rmsa", hh_ActKey.Value, hh_GridGkey.Value);
                  rmsaDao.Insertbtlog(conn, thistran, "rmsa", DAC.GetStringValue(mod_row["rmsa_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["rmsa_gkey"]));
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
                  rmsaDao.Dispose();
                  tb_rmsa_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_mkey.Value = st_tempgkey;
              hh_fun_mkey.Value = st_tempgkey;
              Bind_WebDataGrid_A(WebDataGrid_rmsa, !bl_showRowA, !bl_resetKey);
              Bind_WebDataGrid_A(WebDataGrid_rmsa_ba, !bl_showRowA, !bl_resetKey);
              //
              ShowOneRow_A(hh_mkey.Value);
              sFN.WebDataGrid_SelectRow(ref WebDataGrid_rmsa_ba, "rmsa_mkey", hh_mkey.Value);
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
        rmsaDao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }

    }

    #endregion

    #region bt_QUT_A
    protected void bt_QUT_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Response.Redirect("~/Master/Default/Mainform.aspx");
    }
    #endregion

    #region EditCheck_A
    private bool ServerEditCheck_A(ref string sMsg)
    {
      bool ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_rmsa_RDDAT.Text, lb_rmsa_RDDAT.Text, ref sMsg, PublicVariable.LangType, sFN); //訊息日期
      DataCheck.Dispose();
      return ret;
    }
    #endregion

    #region get_CmdQueryS_ba
    protected void get_CmdQueryS_ba()
    {
      try
      {
        CmdQueryS_ba = (OleDbCommand)Session["fmrmsa_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ba.CommandText = " and 1=0 ";
      }
    }
    #endregion


    #region Obj_rmsa_ba

    protected void Obj_rmsa_ba_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
        e.InputParameters["st_orderKey"] = " A.RDDAT DESC,A.RDREN ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }
    #endregion



    #region get_CmdQueryS_b

    /// <summary>
    /// get_CmdQueryS_b
    /// </summary>
    /// <param name="st_ren"></param>
    private void get_CmdQueryS_b(string st_ren)
    {
      CmdQueryS_b.CommandText = " and A.RDREN=? ";
      CmdQueryS_b.Parameters.Clear();
      DAC.AddParam(CmdQueryS_b, "RDREN", st_ren);
    }

    /// <summary>
    /// Bind_WebDataGrid_B
    /// </summary>
    /// <param name="WebDataGrid"></param>
    private void Bind_WebDataGrid_B(Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, bool bl_resetkey)
    {
      WebDataGrid.Rows.Clear();
      WebDataGrid.DataBind();
      //
      if (bl_resetkey)
      {
        if (WebDataGrid.Rows.Count > 0)
        {
          //hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rmsb_gkey");
          //hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rmsb_mkey");
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

    #region WebDataGrid_rmsb

    //******************
    //WebDataGrid_rmsb 
    //******************

    /// <summary>
    /// WebDataGrid_rmsb_RowAdding
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsb_RowAdding(object sender, Infragistics.Web.UI.GridControls.RowAddingEventArgs e)
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

    /// <summary>
    /// WebDataGrid_rmsb_RowAdded
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsb_RowAdded(object sender, Infragistics.Web.UI.GridControls.RowAddedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsb.CustomAJAXResponse.Message = DAC.GetStringValue(e.Exception.Message);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.Exception.Message);
      }

    }

    /// <summary>
    /// WebDataGrid_rmsb_RowUpdating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsb_RowUpdating(object sender, Infragistics.Web.UI.GridControls.RowUpdatingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        string st_RDREN = DAC.GetStringValue(e.Values["rmsb_RDREN"]);
        Int32 st_RDITM = DAC.GetInt32Value(e.Values["rmsb_RDITM"]);
        string st_gkey = DAC.GetStringValue(e.Values["rmsb_gkey"]);
        OleDbCommand Qcheck = new OleDbCommand();
        Qcheck.Parameters.Clear();
        Qcheck.CommandText = " and RDREN=? and RDITM=? and gkey!=? ";
        DAC.AddParam(Qcheck, "rmsb_RDREN", st_RDREN);
        DAC.AddParam(Qcheck, "rmsb_RDITM", st_RDITM);
        DAC.AddParam(Qcheck, "rmsb_gkey", st_gkey);
        if (sFN.lookups("select gkey from  rmsb ", Qcheck, "gkey") != "")
        {
          e.Cancel = true;
          WebDataGrid_rmsb.CustomAJAXResponse.Message = "資料已存在!";
        }
        Qcheck.Dispose();
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }


    /// <summary>
    /// WebDataGrid_rmsb_RowUpdated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsb_RowUpdated(object sender, Infragistics.Web.UI.GridControls.RowUpdatedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsb.CustomAJAXResponse.Message = DAC.GetStringValue(e.Exception.Message);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.Exception.Message);
      }
    }

    /// <summary>
    /// WebDataGrid_rmsb_RowsDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsb_RowsDeleting(object sender, Infragistics.Web.UI.GridControls.RowDeletingEventArgs e)
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

    /// <summary>
    /// WebDataGrid_rmsb_RowDeleted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsb_RowDeleted(object sender, Infragistics.Web.UI.GridControls.RowDeletedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsb.CustomAJAXResponse.Message = DAC.GetStringValue(e.Exception.Message);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.Exception.Message);
      }
    }

    #endregion

    #region Obj_rmsb

    //*********
    //Obj_rmsb 
    //*********

    /// <summary>
    /// Obj_rmsb_Selecting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsb_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //CmdQueryS 必須再此定義,post back才可以找到
        //mkey 轉單號
        string st_ren = "";
        st_ren = sFN.GetRenFromGkey("rmsa", "RDREN", "mkey", hh_fun_mkey.Value);
        get_CmdQueryS_b(st_ren);
        e.InputParameters["WhereQuery"] = CmdQueryS_b;
        e.InputParameters["st_addSelect"] = "";
        e.InputParameters["bl_lock"] = false;
        e.InputParameters["st_addJoin"] = "";
        e.InputParameters["st_addUnion"] = "";
        e.InputParameters["st_orderKey"] = " A.RDITM DESC ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    /// <summary>
    /// Obj_rmsb_Selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsb_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsb.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    /// <summary>
    /// Obj_rmsb_Inserting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsb_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["rmsb_RDREN"] = tx_rmsa_RDREN.Text;       // 訊息編號
        //
        e.InputParameters["rmsb_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    /// <summary>
    /// Obj_rmsb_Inserted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsb_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsb.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    /// <summary>
    /// Obj_rmsb_Updating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsb_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["rmsb_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_rmsb_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsb.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    /// <summary>
    /// Obj_rmsb_Deleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsb_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["rmsb_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsb_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsb.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }
    #endregion



    #region get_CmdQueryS_c
    //****************
    //rmsc control
    //****************

    /// <summary>
    /// Bind_WebDataGrid_C
    /// </summary>
    /// <param name="WebDataGrid"></param>
    private void Bind_WebDataGrid_C(Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, bool bl_resetkey)
    {
      WebDataGrid.Rows.Clear();
      WebDataGrid.DataBind();
      //
      if (bl_resetkey)
      {
        if (WebDataGrid.Rows.Count > 0)
        {
          //hh_GridGkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rmsc_rmsb_gkey");
          //hh_mkey.Value = clsGV.get_ColFromKey(WebDataGrid.Rows, 0, "rmsb_mkey");
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

    /// <summary>
    /// get_CmdQueryS_c
    /// </summary>
    /// <param name="st_ren"></param>
    private void get_CmdQueryS_c(string st_rmsb_gkey)
    {
      CmdQueryS_c.CommandText = " and A.rmsb_gkey=? ";
      CmdQueryS_c.Parameters.Clear();
      DAC.AddParam(CmdQueryS_c, "rmsb_gkey", st_rmsb_gkey);
    }

    #endregion

    #region WebDataGrid_rmsc

    //******************
    //WebDataGrid_rmsc 
    //******************

    /// <summary>
    /// WebDataGrid_rmsc_RowAdding
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsc_RowAdding(object sender, Infragistics.Web.UI.GridControls.RowAddingEventArgs e)
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

    /// <summary>
    /// WebDataGrid_rmsc_RowAdded
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsc_RowAdded(object sender, Infragistics.Web.UI.GridControls.RowAddedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsc.CustomAJAXResponse.Message = DAC.GetStringValue(e.Exception.Message);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.Exception.Message);
      }
    }

    /// <summary>
    /// WebDataGrid_rmsc_RowUpdating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsc_RowUpdating(object sender, Infragistics.Web.UI.GridControls.RowUpdatingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        string st_RDREN = DAC.GetStringValue(e.Values["rmsc_RDREN"]);
        Int32 st_RDITM = DAC.GetInt32Value(e.Values["rmsc_RDITM"]);
        string st_rmsb_gkey = DAC.GetStringValue(e.Values["rmsc_rmsb_gkey"]);
        string st_gkey = DAC.GetStringValue(e.Values["rmsc_gkey"]);
        OleDbCommand Qcheck = new OleDbCommand();
        Qcheck.Parameters.Clear();
        Qcheck.CommandText = " and RDREN=? and RDITM=? and rmsb_gkey=? and gkey!=? ";
        DAC.AddParam(Qcheck, "rmsc_RDREN", st_RDREN);
        DAC.AddParam(Qcheck, "rmsc_RDITM", st_RDITM);
        DAC.AddParam(Qcheck, "rmsb_gkey", st_rmsb_gkey);
        DAC.AddParam(Qcheck, "rmsc_gkey", st_gkey);
        if (sFN.lookups("select gkey from  rmsc ", Qcheck, "gkey") != "")
        {
          e.Cancel = true;
          WebDataGrid_rmsc.CustomAJAXResponse.Message = "資料已存在!";
        }
        Qcheck.Dispose();
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }


    /// <summary>
    /// WebDataGrid_rmsc_RowUpdated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsc_RowUpdated(object sender, Infragistics.Web.UI.GridControls.RowUpdatedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsc.CustomAJAXResponse.Message = DAC.GetStringValue(e.Exception.Message);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.Exception.Message);
      }
    }

    /// <summary>
    /// WebDataGrid_rmsc_RowsDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsc_RowsDeleting(object sender, Infragistics.Web.UI.GridControls.RowDeletingEventArgs e)
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

    /// <summary>
    /// WebDataGrid_rmsc_RowDeleted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void WebDataGrid_rmsc_RowDeleted(object sender, Infragistics.Web.UI.GridControls.RowDeletedEventArgs e)
    {
      if (e.Exception != null)
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsc.CustomAJAXResponse.Message = DAC.GetStringValue(e.Exception.Message);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.Exception.Message);
      }
    }

    #endregion

    #region Obj_rmsc

    //*********
    //Obj_rmsc 
    //*********

    /// <summary>
    /// Obj_rmsc_Selecting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsc_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        //get_CmdQueryS_c 必須再此定義,post back才可以找到
        //form table_b gkey 
        get_CmdQueryS_c(DAC.GetStringValue(Session["fm_rmsa_rmsb_gkey"]));
        e.InputParameters["WhereQuery"] = CmdQueryS_c;
        e.InputParameters["st_addSelect"] = "";
        e.InputParameters["bl_lock"] = false;
        e.InputParameters["st_addJoin"] = "";
        e.InputParameters["st_addUnion"] = "";
        e.InputParameters["st_orderKey"] = " A.RDITM ";
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    /// <summary>
    /// Obj_rmsc_Selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsc_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsc.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    /// <summary>
    /// Obj_rmsc_Inserting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsc_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["rmsc_RDREN"] = tx_rmsa_RDREN.Text;       // 訊息編號
        e.InputParameters["rmsc_rmsb_gkey"] = DAC.GetStringValue(Session["fm_rmsa_rmsb_gkey"]);       // b的gkey
        //
        e.InputParameters["rmsc_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    /// <summary>
    /// Obj_rmsc_Inserted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsc_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsc.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    /// <summary>
    /// Obj_rmsc_Updating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsc_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["rmsc_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void Obj_rmsc_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsc.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    /// <summary>
    /// Obj_rmsc_Deleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsc_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        e.InputParameters["rmsc_actkey"] = DAC.get_guidkey();
        e.InputParameters["UserGkey"] = UserGkey;
      }
      else
      {
        e.Cancel = true;
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Obj_rmsc_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (DAC.GetStringValue(e.ReturnValue) != "")
      {
        e.ExceptionHandled = true;
        WebDataGrid_rmsc.CustomAJAXResponse.Message = DAC.GetStringValue(e.ReturnValue);
        //
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = DAC.GetStringValue(e.ReturnValue);
      }
    }

    #endregion



    #region bt_MOD_B_Control
    protected void bt_MOD_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      SetEditMod_B();
    }

    protected void bt_SAVE_B_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      if (WebTabGrid.SelectedIndex == 0)
      {
        Bind_WebDataGrid_B(WebDataGrid_rmsb, bl_resetKey);
        SetSerMod_B();
      }
      else if (WebTabGrid.SelectedIndex == 1)
      {
        Bind_WebDataGrid_C(WebDataGrid_rmsc, bl_resetKey);
        SetSerMod_B();
      }
    }

    private void SetSerMod_B()
    {
      if (WebTabGrid.SelectedIndex == 0)
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsb, false);
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsc, false);
      }
      else if (WebTabGrid.SelectedIndex == 1)
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsb, false);
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsc, false);
      }
      bt_MOD_B = sFN.SetWebImageButtonDetail(bt_MOD_B);
      bt_MOD_B.Visible = true;
      //
      bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
      bt_New_B.Visible = false;
      //
      bt_SAVE_B = sFN.SetWebImageButtonDetail(bt_SAVE_B);
      bt_SAVE_B.Visible = false;
      bt_New_B.ClientSideEvents.Click = "";
      //
      WebDataGrid_rmsa.Enabled = true;
      WebDataGrid_rmsa_ba.Enabled = true;
      PanBtns.Enabled = true;
      //Control C
      WebDataGrid_rmsb.Behaviors.Selection.AutoPostBackFlags.CellSelectionChanged = true;

    }
    private void SetEditMod_B()
    {
      if (WebTabGrid.SelectedIndex == 0)
      {
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsb, true);
        sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsc, false);
        bt_New_B.ClientSideEvents.Click = "webDataGrid_RowAddFocus('" + WebDataGrid_rmsb.ClientID + "','rmsb_RDTXT');";
        bt_MOD_B = sFN.SetWebImageButtonDetail(bt_MOD_B);
        bt_MOD_B.Visible = false;
        //
        bt_New_B = sFN.SetWebImageButtonDetail(bt_New_B);
        bt_New_B.Visible = true;
        //
        bt_SAVE_B = sFN.SetWebImageButtonDetail(bt_SAVE_B);
        bt_SAVE_B.Visible = true;
        //
        WebDataGrid_rmsa.Enabled = false;
        WebDataGrid_rmsa_ba.Enabled = false;
        PanBtns.Enabled = false;
        //Control C
        WebDataGrid_rmsb.Behaviors.Selection.AutoPostBackFlags.CellSelectionChanged = false;
      }
      else if (WebTabGrid.SelectedIndex == 1)
      {
        if (DAC.GetStringValue(Session["fm_rmsa_rmsb_gkey"]) != "") 
        {
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsb, false);
          sFN.WebDataGrid_SetEdit(ref WebDataGrid_rmsc, true);
          bt_New_B.ClientSideEvents.Click = "webDataGrid_RowAddFocus('" + WebDataGrid_rmsc.ClientID + "','rmsc_RDENO');";
        }
        else
        {
          lb_ErrorMessage.Visible=true;
          lb_ErrorMessage.Text = "須選擇," + WebTabGrid.Tabs[0].Text;
        }
      }
      //
    }

    #endregion


    #region Selection

    protected void WebTabGrid_SelectedIndexChanged(object sender, Infragistics.Web.UI.LayoutControls.TabSelectedIndexChangedEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        if ((e.NewIndex == 1) && (DAC.GetStringValue(Session["fm_rmsa_rmsb_gkey"]) != ""))
        {
          get_CmdQueryS_c(DAC.GetStringValue(Session["fm_rmsa_rmsb_gkey"]));
          Obj_rmsc.TypeName = "DD2015_45.DAC_rmsc";
          Obj_rmsc.SelectMethod = "SelectTable_rmsc";
          Obj_rmsc.UpdateMethod = "UpdateTable_rmsc";
          Obj_rmsc.InsertMethod = "InsertTable_rmsc";
          Obj_rmsc.DeleteMethod = "DeleteTable_rmsc";
          WebDataGrid_rmsc.DataSourceID = "Obj_rmsc";
          Bind_WebDataGrid_C(WebDataGrid_rmsc, bl_resetKey);
        }
        else
        {
          WebDataGrid_rmsc.Rows.Clear();
          get_CmdQueryS_c("#");
          Bind_WebDataGrid_C(WebDataGrid_rmsc, bl_resetKey);
          //
          Obj_rmsc.TypeName = "DD2015_45.DAC_rmsc";
          Obj_rmsc.SelectMethod = "SelectTable_rmsc";
          Obj_rmsc.UpdateMethod = "UpdateTable_rmsc";
          Obj_rmsc.InsertMethod = "InsertTable_rmsc";
          Obj_rmsc.DeleteMethod = "DeleteTable_rmsc";
          WebDataGrid_rmsc.DataSourceID = "Obj_rmsc";
          Bind_WebDataGrid_C(WebDataGrid_rmsc, bl_resetKey);
        }
      }
      else
      {
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    protected void WebDataGrid_rmsa_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      try
      {
        SetSerMod_B();
        hh_fun_name.Value = "showa";
        hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("rmsa_mkey").Value);
        WebTab_form.SelectedIndex = 1;
        //
        ShowOneRow_A(hh_fun_mkey.Value);
        sFN.WebDataGrid_SelectRow(ref WebDataGrid_rmsa_ba, "rmsa_mkey", hh_fun_mkey.Value);
        SetSerMod_A();
      }
      catch
      {
      }
    }

    protected void WebDataGrid_rmsa_ba_RowSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedRowEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        try
        {
          SetSerMod_B();
          hh_fun_name.Value = "showa";
          hh_fun_mkey.Value = DAC.GetStringValue(e.CurrentSelectedRows[0].Items.FindItemByKey("rmsa_mkey").Value);
          WebTab_form.SelectedIndex = 1;
          //
          ShowOneRow_A(hh_fun_mkey.Value);
          sFN.WebDataGrid_SelectRow(ref WebDataGrid_rmsa_ba, "rmsa_mkey", hh_fun_mkey.Value);
          SetSerMod_A();
        }
        catch
        {
        }
      }
      else
      {
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }
    }

    private void ShowOneRow_A(string st_mkey)
    {
      DAC_rmsa rmsaDao = new DAC_rmsa(conn);
      DataTable tb_rmsa = new DataTable();
      OleDbCommand cmd_where = new OleDbCommand();
      string st_ren = "";
      //
      cmd_where.CommandText = " and a.mkey=? ";
      DAC.AddParam(cmd_where, "mkey", st_mkey);
      tb_rmsa = rmsaDao.SelectTableForTextEdit_rmsa(cmd_where);
      if (tb_rmsa.Rows.Count == 1)
      {
        BindText_A(tb_rmsa.Rows[0]);
        //rmsb 
        st_ren = sFN.GetRenFromGkey("rmsa", "RDREN", "mkey", st_mkey);
        get_CmdQueryS_b(st_ren);
        Obj_rmsb.TypeName = "DD2015_45.DAC_rmsb";
        Obj_rmsb.SelectMethod = "SelectTable_rmsb";
        Obj_rmsb.UpdateMethod = "UpdateTable_rmsb";
        Obj_rmsb.InsertMethod = "InsertTable_rmsb";
        Obj_rmsb.DeleteMethod = "DeleteTable_rmsb";
        WebDataGrid_rmsb.DataSourceID = "Obj_rmsb";
        Bind_WebDataGrid_B(WebDataGrid_rmsb, bl_resetKey);
        ////rmsc
        //st_ren = sFN.GetRenFromGkey("rmsa", "RDREN", "mkey", st_mkey);
        //get_CmdQueryS_b1(st_ren);
        //Obj_rmsc.TypeName = "DD2015_45.DAC_rmsc";
        //Obj_rmsc.SelectMethod = "SelectTable_rmsc";
        //Obj_rmsc.UpdateMethod = "UpdateTable_rmsc";Session["fm_rmsa_rmsb_gkey"]
        //Obj_rmsc.InsertMethod = "InsertTable_rmsc";
        //Obj_rmsc.DeleteMethod = "DeleteTable_rmsc";
        //WebDataGrid_rmsc.DataSourceID = "Obj_rmsc";
        //Bind_WebDataGrid_B(WebDataGrid_rmsc, bl_resetKey);
      }
      else
      {
        ClearText_A();
      }
      cmd_where.Dispose();
      tb_rmsa.Dispose();
      rmsaDao.Dispose();
      //
    }

    protected void WebDataGrid_rmsb_CellSelectionChanged(object sender, Infragistics.Web.UI.GridControls.SelectedCellEventArgs e)
    {
      if (clsFN.chkLoginState())
      {
        Session["fm_rmsa_rmsb_gkey"] = DAC.GetStringValue(e.CurrentSelectedCells[0].Row.Items.FindItemByKey("rmsb_gkey").Value);
        Session["fm_rmsa_rmsb_RDREN"] = DAC.GetStringValue(e.CurrentSelectedCells[0].Row.Items.FindItemByKey("rmsb_RDREN").Value);
        Session["fm_rmsa_rmsb_RDITM"] = DAC.GetStringValue(e.CurrentSelectedCells[0].Row.Items.FindItemByKey("rmsb_RDITM").Value);
      }
      else
      {
        HttpContext.Current.Response.Redirect("~/login.aspx");
      }

    }

    #endregion
  }
}