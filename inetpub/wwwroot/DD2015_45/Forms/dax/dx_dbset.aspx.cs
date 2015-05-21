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
using System.IO;
using YNetLib_45;


namespace DD2015_45.Forms.dax
{
  public partial class dx_dbset : FormBase
  {
    string st_object_func = "UNDBSET";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    //char c34 = (char)34;
    string s34 = "" + (char)34;
    string s1310 = "" + (char)13 + (char)10;
    //
    //string st_dd_apx = "UNDBSET";         //UNdcnews   與apx 相關
    //string st_dd_table = "DBSET";       //dcnews     與table 相關 
    //string st_ren_head = "";       //DC         與單號相關 
    //string st_ren_yymmtext = "";   //"         與單號相關 
    //string st_ren_cls = "";        //ren        與單號cls相關 
    //string st_ren_cos = "";        //1          與單號cos相關 
    //int in_ren_len = 0;            //6          與單號流水號 
    //
    protected void Page_Load(object sender, EventArgs e)
    {
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      //檢查權限&狀態
      if (DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]) == "")
      {
        Response.Redirect("~/Forms/dax/dx_daset.aspx");
      }
      else if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          dr_DBSET_DBTYP = sFN.DropDownListFromClasses(ref dr_DBSET_DBTYP, "DATATYPE", "class_text", "class_value");
          dr_DBSET_DBUCO = sFN.DropDownListFromClasses(ref dr_DBSET_DBUCO, "CONTROL_NAME", "class_text", "class_value");

          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmDBSET_CmdQueryS"] = CmdQueryS;
          //Set_Control();

          if (DAC.GetStringValue(Session["fmDBSET_gr_GridView_DBSET_GridGkey"]) != "")
          {
            gr_GridView_DBSET.PageIndex = DAC.GetInt16Value(Session["fmDBSET_gr_GridView_DBSET_PageIndex"]);
            gr_GridView_DBSET.SelectedIndex = DAC.GetInt16Value(Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"]);
            hh_GridGkey.Value = DAC.GetStringValue(Session["fmDBSET_gr_GridView_DBSET_GridGkey"]);
          }
          //

          //
          Set_Control();
          BindNew(true);
          SetSerMod();
          //
          Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
          Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
          SetSerMod();
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
      FunctionName = sFN.SetFormTitle(st_object_func, LangType);   //取Page Title
      gr_GridView_DBSET.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormControlsText(this, LangType, ApVer, "UNDBSET", "DBSET");
    }

    private void ClearText()
    {
      //
      tx_DBSET_DBVER.Text = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]);  //版本編號
      tx_DBSET_DBNUM.Text = DAC.GetStringValue(Session["fmDASET_tx_DASET_DANUM"]);  //客戶編號
      tx_DBSET_DBAPX.Text = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]);  //程式名稱
      tx_DBSET_DBITM.Text = "";  //項　　次
      tx_DBSET_DBFLD.Text = "";  //欄位名稱
      tx_DBSET_DBTNA.Text = "";  //繁體名稱
      dr_DBSET_DBTYP.SelectedIndex = -1;  //資料型態
      dr_DBSET_DBTYP = sFN.SetDropDownList(ref dr_DBSET_DBTYP, "NVARCHAR");  //資料型態
      tx_DBSET_DBLEN.Text = "20";  //資料長度
      tx_DBSET_DBENA.Text = "";  //英文名稱
      tx_DBSET_DBCNA.Text = "";  //簡體名稱
      tx_DBSET_DBJIA.Text = "A";  //JoinAlias
      tx_DBSET_DBJIN.Text = "";  //JoinTable
      tx_DBSET_DBJIF.Text = "";  //ret field
      tx_DBSET_DBJIK.Text = "";  //Join Key
      tx_DBSET_DBROW.Text = "0";  //ROW 位置
      tx_DBSET_DBCOL.Text = "0";  //COL 位置
      dr_DBSET_DBUCO = sFN.SetDropDownList(ref dr_DBSET_DBUCO, "TEXTBOX");  //使用元件
      tx_DBSET_DBWID.Text = "80";  //元件寬度
      tx_DBSET_DBUED.Text = "0";  //EDIT寬度
      tx_DBSET_DBUTB.Text = "";  //參考Table
      tx_DBSET_DBUHO.Text = "";  //參考Class
      tx_DBSET_DBGRD.Text = "0";  //GridList
      tx_DBSET_DBDEF.Text = "<>";  //Default

      ck_DBSET_DBPRY.Checked = false;  //Pri  Key
      ck_DBSET_DBINS.Checked = true;  //是否新增
      ck_DBSET_DBMOD.Checked = true;  //是否更正
      ck_DBSET_DBEMP.Checked = true;  //是否空白
      ck_DBSET_DBSER.Checked = false;  //查詢鍵值
      ck_DBSET_DBSOR.Checked = false;  //排序鍵值
      tx_DBSET_DBRMK.Text = "";  //備註資料
      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      //
      //
      clsGV.TextBox_Set(ref tx_DBSET_DBVER, false);   //版本編號
      clsGV.TextBox_Set(ref tx_DBSET_DBNUM, false);   //客戶編號
      clsGV.TextBox_Set(ref tx_DBSET_DBAPX, false);   //程式名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBITM, false);   //項　　次
      clsGV.TextBox_Set(ref tx_DBSET_DBFLD, false);   //欄位名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBTNA, false);   //繁體名稱
      clsGV.Drpdown_Set(ref dr_DBSET_DBTYP, ref tx_DBSET_DBTYP, false);   //資料型態
      clsGV.TextBox_Set(ref tx_DBSET_DBLEN, false);   //資料長度
      clsGV.TextBox_Set(ref tx_DBSET_DBENA, false);   //英文名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBCNA, false);   //簡體名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBJIA, false);   //JoinAlias
      clsGV.TextBox_Set(ref tx_DBSET_DBJIN, false);   //JoinTable
      clsGV.TextBox_Set(ref tx_DBSET_DBJIF, false);   //ret field
      clsGV.TextBox_Set(ref tx_DBSET_DBJIK, false);   //Join Key
      clsGV.TextBox_Set(ref tx_DBSET_DBROW, false);   //ROW 位置
      clsGV.TextBox_Set(ref tx_DBSET_DBCOL, false);   //COL 位置
      clsGV.Drpdown_Set(ref dr_DBSET_DBUCO, ref tx_DBSET_DBUCO, false);   //使用元件
      clsGV.TextBox_Set(ref tx_DBSET_DBWID, false);   //元件寬度
      clsGV.TextBox_Set(ref tx_DBSET_DBUED, false);   //EDIT寬度
      clsGV.TextBox_Set(ref tx_DBSET_DBUTB, false);   //參考Table
      clsGV.TextBox_Set(ref tx_DBSET_DBUHO, false);   //參考Class
      clsGV.TextBox_Set(ref tx_DBSET_DBGRD, false);   //GridList
      clsGV.TextBox_Set(ref tx_DBSET_DBDEF, false);   //Default
      ck_DBSET_DBPRY.Enabled = false;  //Pri  Key
      ck_DBSET_DBINS.Enabled = false;  //是否新增
      ck_DBSET_DBMOD.Enabled = false;  //是否更正
      ck_DBSET_DBEMP.Enabled = false;  //是否空白
      ck_DBSET_DBSER.Enabled = false;  //查詢鍵值
      ck_DBSET_DBSOR.Enabled = false;  //排序鍵值
      clsGV.TextBox_Set(ref tx_DBSET_DBRMK, false);   //備註資料
      //
      //
      sFN.SetButtons(this, LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_DBSET.Enabled = true;
      //gr_GridView_DBSET.Columns[0].Visible=true;
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_DBSET_DBVER, false);  //版本編號
      clsGV.TextBox_Set(ref tx_DBSET_DBNUM, false);  //客戶編號
      clsGV.TextBox_Set(ref tx_DBSET_DBAPX, false);  //程式名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBITM, false);  //項　　次
      clsGV.TextBox_Set(ref tx_DBSET_DBFLD, true);  //欄位名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBTNA, true);  //繁體名稱
      clsGV.Drpdown_Set(ref dr_DBSET_DBTYP, ref tx_DBSET_DBTYP, true);   //資料型態
      clsGV.TextBox_Set(ref tx_DBSET_DBLEN, true);  //資料長度
      clsGV.TextBox_Set(ref tx_DBSET_DBENA, true);  //英文名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBCNA, true);  //簡體名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBJIA, true);  //JoinAlias
      clsGV.TextBox_Set(ref tx_DBSET_DBJIN, true);  //JoinTable
      clsGV.TextBox_Set(ref tx_DBSET_DBJIF, true);  //ret field
      clsGV.TextBox_Set(ref tx_DBSET_DBJIK, true);  //Join Key
      clsGV.TextBox_Set(ref tx_DBSET_DBROW, true);  //ROW 位置
      clsGV.TextBox_Set(ref tx_DBSET_DBCOL, true);  //COL 位置
      clsGV.Drpdown_Set(ref dr_DBSET_DBUCO, ref tx_DBSET_DBUCO, true);   //使用元件
      clsGV.TextBox_Set(ref tx_DBSET_DBWID, true);  //元件寬度
      clsGV.TextBox_Set(ref tx_DBSET_DBUED, true);  //EDIT寬度
      clsGV.TextBox_Set(ref tx_DBSET_DBUTB, true);  //參考Table
      clsGV.TextBox_Set(ref tx_DBSET_DBUHO, true);  //參考Class
      clsGV.TextBox_Set(ref tx_DBSET_DBGRD, true);  //GridList
      clsGV.TextBox_Set(ref tx_DBSET_DBDEF, true);  //Default
      ck_DBSET_DBPRY.Enabled = true;  //Pri  Key
      ck_DBSET_DBINS.Enabled = true;  //是否新增
      ck_DBSET_DBMOD.Enabled = true;  //是否更正
      ck_DBSET_DBEMP.Enabled = true;  //是否空白
      ck_DBSET_DBSER.Enabled = true;  //查詢鍵值
      ck_DBSET_DBSOR.Enabled = true;  //排序鍵值
      clsGV.TextBox_Set(ref tx_DBSET_DBRMK, true);  //備註資料
      // 
      sFN.SetButtons(this, LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_DBSET.Enabled = false;
      //gr_GridView_DBSET.Columns[0].Visible = false;
    }

    private void SetEditModAll()
    {
      // 
      clsGV.TextBox_Set(ref tx_DBSET_DBVER, false);  //版本編號
      clsGV.TextBox_Set(ref tx_DBSET_DBNUM, false);  //客戶編號
      clsGV.TextBox_Set(ref tx_DBSET_DBAPX, false);  //程式名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBITM, false);  //項　　次
      clsGV.TextBox_Set(ref tx_DBSET_DBFLD, false);  //欄位名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBTNA, false);  //繁體名稱
      clsGV.Drpdown_Set(ref dr_DBSET_DBTYP, ref tx_DBSET_DBTYP, true);   //資料型態
      clsGV.TextBox_Set(ref tx_DBSET_DBLEN, false);  //資料長度
      clsGV.TextBox_Set(ref tx_DBSET_DBENA, false);  //英文名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBCNA, false);  //簡體名稱
      clsGV.TextBox_Set(ref tx_DBSET_DBJIA, false);  //JoinAlias
      clsGV.TextBox_Set(ref tx_DBSET_DBJIN, false);  //JoinTable
      clsGV.TextBox_Set(ref tx_DBSET_DBJIF, false);  //ret field
      clsGV.TextBox_Set(ref tx_DBSET_DBJIK, false);  //Join Key
      clsGV.TextBox_Set(ref tx_DBSET_DBROW, false);  //ROW 位置
      clsGV.TextBox_Set(ref tx_DBSET_DBCOL, false);  //COL 位置
      clsGV.Drpdown_Set(ref dr_DBSET_DBUCO, ref tx_DBSET_DBUCO, true);   //使用元件
      clsGV.TextBox_Set(ref tx_DBSET_DBWID, false);  //元件寬度
      clsGV.TextBox_Set(ref tx_DBSET_DBUED, false);  //EDIT寬度
      clsGV.TextBox_Set(ref tx_DBSET_DBUTB, false);  //參考Table
      clsGV.TextBox_Set(ref tx_DBSET_DBUHO, false);  //參考Class
      clsGV.TextBox_Set(ref tx_DBSET_DBGRD, false);  //GridList
      clsGV.TextBox_Set(ref tx_DBSET_DBDEF, false);  //Default
      ck_DBSET_DBPRY.Enabled = false;  //Pri  Key
      ck_DBSET_DBINS.Enabled = false;  //是否新增
      ck_DBSET_DBMOD.Enabled = false;  //是否更正
      ck_DBSET_DBEMP.Enabled = false;  //是否空白
      ck_DBSET_DBSER.Enabled = false;  //查詢鍵值
      ck_DBSET_DBSOR.Enabled = false;  //排序鍵值
      clsGV.TextBox_Set(ref tx_DBSET_DBRMK, false);  //備註資料
      // 
      sFN.SetButtons(this, LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_DBSET.Enabled = true;
      //gr_GridView_DBSET.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      tx_DBSET_DBVER.Text = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]);  //版本編號
      tx_DBSET_DBNUM.Text = DAC.GetStringValue(Session["fmDASET_tx_DASET_DANUM"]);  //客戶編號
      tx_DBSET_DBAPX.Text = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]);  //程式名稱
      tx_DBSET_DBITM.Text = DAC.GetStringValue(CurRow["DBSET_DBITM"]);  //項　　次
      tx_DBSET_DBFLD.Text = DAC.GetStringValue(CurRow["DBSET_DBFLD"]);  //欄位名稱
      tx_DBSET_DBTNA.Text = DAC.GetStringValue(CurRow["DBSET_DBTNA"]);  //繁體名稱
      dr_DBSET_DBTYP = sFN.SetDropDownList(ref dr_DBSET_DBTYP, DAC.GetStringValue(CurRow["DBSET_DBTYP"]));  //資料型態
      tx_DBSET_DBLEN.Text = DAC.GetStringValue(CurRow["DBSET_DBLEN"]);  //資料長度
      tx_DBSET_DBENA.Text = DAC.GetStringValue(CurRow["DBSET_DBENA"]);  //英文名稱
      tx_DBSET_DBCNA.Text = DAC.GetStringValue(CurRow["DBSET_DBCNA"]);  //簡體名稱
      tx_DBSET_DBJIA.Text = DAC.GetStringValue(CurRow["DBSET_DBJIA"]);  //JoinAlias
      tx_DBSET_DBJIN.Text = DAC.GetStringValue(CurRow["DBSET_DBJIN"]);  //JoinTable
      tx_DBSET_DBJIF.Text = DAC.GetStringValue(CurRow["DBSET_DBJIF"]);  //ret field
      tx_DBSET_DBJIK.Text = DAC.GetStringValue(CurRow["DBSET_DBJIK"]);  //Join Key
      tx_DBSET_DBROW.Text = DAC.GetStringValue(CurRow["DBSET_DBROW"]);  //ROW 位置
      tx_DBSET_DBCOL.Text = DAC.GetStringValue(CurRow["DBSET_DBCOL"]);  //COL 位置
      dr_DBSET_DBUCO = sFN.SetDropDownList(ref dr_DBSET_DBUCO, DAC.GetStringValue(CurRow["DBSET_DBUCO"]));  //使用元件
      tx_DBSET_DBWID.Text = DAC.GetStringValue(CurRow["DBSET_DBWID"]);  //元件寬度
      tx_DBSET_DBUED.Text = DAC.GetStringValue(CurRow["DBSET_DBUED"]);  //EDIT寬度
      tx_DBSET_DBUTB.Text = DAC.GetStringValue(CurRow["DBSET_DBUTB"]);  //參考Table
      tx_DBSET_DBUHO.Text = DAC.GetStringValue(CurRow["DBSET_DBUHO"]);  //參考Class
      tx_DBSET_DBGRD.Text = DAC.GetStringValue(CurRow["DBSET_DBGRD"]);  //GridList
      tx_DBSET_DBDEF.Text = DAC.GetStringValue(CurRow["DBSET_DBDEF"]);  //Default
      ck_DBSET_DBPRY.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["DBSET_DBPRY"]));  //Pri  Key
      ck_DBSET_DBINS.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["DBSET_DBINS"]));  //是否新增
      ck_DBSET_DBMOD.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["DBSET_DBMOD"]));  //是否更正
      ck_DBSET_DBEMP.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["DBSET_DBEMP"]));  //是否空白
      ck_DBSET_DBSER.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["DBSET_DBSER"]));  //查詢鍵值
      ck_DBSET_DBSOR.Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow["DBSET_DBSOR"]));  //排序鍵值
      tx_DBSET_DBRMK.Text = DAC.GetStringValue(CurRow["DBSET_DBRMK"]);  //備註資料
      //
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["DBSET_mkey"]);
      //
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmDBSET_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = "";
      }
      CmdQueryS.CommandText = " and a.DBVER=? and a.DBAPX=? and a.DBTBL=? and a.DBREN=? ";
      DAC.AddParam(CmdQueryS, "DBVER", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]));
      DAC.AddParam(CmdQueryS, "DBAPX", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]));
      DAC.AddParam(CmdQueryS, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
      DAC.AddParam(CmdQueryS, "DBREN", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]));

      //
      DataTable tb_DBSET = new DataTable();
      DAC DBSETDao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = DBSETDao.GetDataAdapter(ApVer, "UNDBSET", "DBSET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " a.DBROW,a.DBCOL ");
      ad_DataDataAdapter.Fill(tb_DBSET);
      //
      if (tb_DBSET.Rows.Count > 0)
      {
        bt_05.OnClientClick = "return btnDEL_c()";
        bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        bt_05.OnClientClick = "return btnDEL0_c()";
        bt_04.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_DBSET.DataSource = tb_DBSET;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_DBSET = clsGV.BindGridView(gr_GridView_DBSET, tb_DBSET, hh_GridCtrl, ref hh_GridGkey, "fmDBSET_gr_GridView_DBSET");
      gr_GridView_DBSET.DataBind();
      SelDataKey = "DBSET_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_DBSET.Select(SelDataKey);
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          Session["fmDBSET_gr_GridView_DBSET_GridGkey"] = hh_GridGkey.Value;
          BindText(CurRow);
        }
        else
        {
          hh_GridCtrl.Value = "init";
          ClearText();
        }
      }
      tb_DBSET.Dispose();
      DBSETDao.Dispose();
    }

    protected void gr_GridView_DBSET_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_DBSET.PageIndex = e.NewPageIndex;
      Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
      hh_GridGkey.Value = gr_GridView_DBSET.DataKeys[gr_GridView_DBSET.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_DBSET_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_DBSET.Enabled)
      {
        //SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        SetSerMod();
        Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
        Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_DBSET_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
      Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_DBSET.DataKeys[gr_GridView_DBSET.SelectedIndex].Value.ToString();
      SetSerMod();
    }

    protected void bt_02_Click(object sender, EventArgs e)
    {
      actNEW();
    }

    protected void actNEW()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 2, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "ins";
        Set_Control();
        ClearText();
        //定義guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(false);
        SetEditMod();
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_DBSET_DBFLD').focus(); </script>";
      }
    }

    protected void bt_04_Click(object sender, EventArgs e)
    {
      actMOD();
    }

    protected void actMOD()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "mod";
        Set_Control();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(true);
        SetEditMod();
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_DBSET_DBFLD').focus(); </script>";
      }
    }

    protected void bt_11_Click(object sender, EventArgs e)
    {
      actMODALL();
    }

    protected void actMODALL()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "modall";
        Set_Control();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(true);
        SetEditModAll();
      }
    }

    protected void bt_QUT_Click(object sender, EventArgs e)
    {
      Response.Redirect("dx_daset.aspx");
    }

    protected void bt_CAN_Click(object sender, EventArgs e)
    {
      actCAN();
    }

    protected void actCAN()
    {
      hh_GridCtrl.Value = "ser";
      Set_Control();
      ClearText();
      BindNew(true);
      SetSerMod();
    }
    protected void bt_05_Click(object sender, EventArgs e)
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
        DAC DBSETDao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "DBSET_gkey='" + hh_GridGkey.Value + "' and DBSET_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_DBSET = new DataTable();
        DbDataAdapter da_ADP = DBSETDao.GetDataAdapter(ApVer, "UNDBSET", "DBSET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_DBSET);
        DataRow[] DelRow = tb_DBSET.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            DBSETDao.Insertbalog(conn, thistran, "DBSET", hh_ActKey.Value, hh_GridGkey.Value);
            DBSETDao.Insertbtlog(conn, thistran, "DBSET", DAC.GetStringValue(DelRow[0]["DBSET_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0]["DBSET_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_DBSET);
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
            DBSETDao.Dispose();
            tb_DBSET.Dispose();
            da_ADP.Dispose();
            conn.Close();
          }
        }
        else
        {
          bl_delok = false;
          lb_ErrorMessage.Visible = true;
          lb_ErrorMessage.Text = StringTable.GetString("資料已變更,請重新選取!");
        }
        tb_DBSET.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_DBSET = clsGV.SetGridCursor("del", gr_GridView_DBSET, -2);
          SetSerMod();
          BindNew(true);
        }
        //bl_delok
      }
    }

    protected void bt_SAV_Click(object sender, EventArgs e)
    {
      actSAV();
    }

    protected void actSAV()
    {
      string st_ckerrmsg = "";
      string st_dberrmsg = "";
      string st_tempgkey = "";
      bool bl_insok = false, bl_updateok = false;
      //
      Set_Control();
      if (ServerEditCheck(ref st_ckerrmsg))
      {
        DAC DBSETDao = new DAC(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {
          if (UpdateDataAll(hh_ActKey.Value, ref st_dberrmsg))
          {
            SetSerMod();
            hh_GridCtrl.Value = "rekey";
            BindNew(true);
            hh_GridCtrl.Value = "ser";
          }
          else
          {
            lb_ErrorMessage.Visible = true;
            lb_ErrorMessage.Text = st_dberrmsg;
          }
        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "DBSET_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //自動編號
            //DateTime dt_idat=Convert.ToDateTime(tx_riza_RIDAT.Text);
            //st_ren_yymmtext =sFN.strzeroi(dt_idat.Year,4)+sFN.strzeroi(dt_idat.Month,2);
            //st_ren_cls=st_ren_yymmtext;
            //tx_riza_RIREN.Text = rizaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            //conn.Close();

            //檢查重複
            if (DBSETDao.IsExists("DBSET", "DBFLD", tx_DBSET_DBFLD.Text, " DBVER='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]) + "' AND DBAPX='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]) + "' AND DBTBL='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]) + "' AND DBFLD='" + tx_DBSET_DBFLD.Text + "'"))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_DBSET_DBFLD.Text + ",已存在.");

              // DBSETDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_riza_RIREN.Text);
              // st_dberrmsg = StringTable.GetString(tx_riza_RIREN.Text + ",已重新取號.");
              // //tx_riza_RIREN.Text = DBSETDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_riza_RIREN.Text ="";
              // conn.Close();

            }
            else
            {
              DataTable tb_DBSET_ins = new DataTable();
              DbDataAdapter da_ADP_ins = DBSETDao.GetDataAdapter(ApVer, "UNDBSET", "DBSET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_DBSET_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_DBSET_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["DBSET_gkey"] = st_tempgkey;       // 
                ins_row["DBSET_mkey"] = DAC.get_guidkey(); //
                ins_row["DBSET_DBVER"] = tx_DBSET_DBVER.Text.Trim();       // 版本編號
                ins_row["DBSET_DBNUM"] = tx_DBSET_DBNUM.Text.Trim();       // 客戶編號
                ins_row["DBSET_DBAPX"] = tx_DBSET_DBAPX.Text.Trim();       // 程式名稱
                ins_row["DBSET_DBITM"] = 0;       // 項　　次
                ins_row["DBSET_DBFLD"] = tx_DBSET_DBFLD.Text.Trim();       // 欄位名稱
                ins_row["DBSET_DBTNA"] = tx_DBSET_DBTNA.Text.Trim();       // 繁體名稱
                ins_row["DBSET_DBTYP"] = dr_DBSET_DBTYP.SelectedValue;       // 資料型態
                ins_row["DBSET_DBLEN"] = tx_DBSET_DBLEN.Text.Trim();       // 資料長度
                ins_row["DBSET_DBENA"] = tx_DBSET_DBENA.Text.Trim();       // 英文名稱
                ins_row["DBSET_DBCNA"] = tx_DBSET_DBCNA.Text.Trim();       // 簡體名稱
                ins_row["DBSET_DBJIA"] = tx_DBSET_DBJIA.Text.Trim();       // JoinAlias
                ins_row["DBSET_DBJIN"] = tx_DBSET_DBJIN.Text.Trim();       // JoinTable
                ins_row["DBSET_DBJIF"] = tx_DBSET_DBJIF.Text.Trim();       // ret field
                ins_row["DBSET_DBJIK"] = tx_DBSET_DBJIK.Text.Trim();       // Join Key
                ins_row["DBSET_DBROW"] = tx_DBSET_DBROW.Text.Trim();       // ROW 位置
                ins_row["DBSET_DBCOL"] = tx_DBSET_DBCOL.Text.Trim();       // COL 位置
                ins_row["DBSET_DBUCO"] = dr_DBSET_DBUCO.SelectedValue;       // 使用元件
                ins_row["DBSET_DBWID"] = tx_DBSET_DBWID.Text.Trim();       // 元件寬度
                ins_row["DBSET_DBUED"] = tx_DBSET_DBUED.Text.Trim();       // EDIT寬度
                ins_row["DBSET_DBUTB"] = tx_DBSET_DBUTB.Text.Trim();       // 參考Table
                ins_row["DBSET_DBUHO"] = tx_DBSET_DBUHO.Text.Trim();       // 參考Class
                ins_row["DBSET_DBGRD"] = tx_DBSET_DBGRD.Text.Trim();       // GridList
                ins_row["DBSET_DBDEF"] = tx_DBSET_DBDEF.Text.Trim();       // Default
                ins_row["DBSET_DBPRY"] = ck_DBSET_DBPRY.Checked ? "1" : "0";        // Pri  Key
                ins_row["DBSET_DBINS"] = ck_DBSET_DBINS.Checked ? "1" : "0";       // 是否新增
                ins_row["DBSET_DBMOD"] = ck_DBSET_DBMOD.Checked ? "1" : "0";       // 是否更正
                ins_row["DBSET_DBEMP"] = ck_DBSET_DBEMP.Checked ? "1" : "0";       // 是否空白
                ins_row["DBSET_DBSER"] = ck_DBSET_DBSER.Checked ? "1" : "0";       // 查詢鍵值
                ins_row["DBSET_DBSOR"] = ck_DBSET_DBSOR.Checked ? "1" : "0";       // 排序鍵值
                ins_row["DBSET_DBREN"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]);       // B檔序號
                ins_row["DBSET_DBTBL"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);       // TABLE名
                ins_row["DBSET_DBTYD"] = "";       // 資料型態
                ins_row["DBSET_DBUFX"] = "";       // DBUFX

                ins_row["DBSET_trusr"] = UserGkey;  //
                tb_DBSET_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_DBSET_ins);
                DBSETDao.Insertbalog(conn, thistran, "DBSET", hh_ActKey.Value, hh_GridGkey.Value);
                DBSETDao.Insertbtlog(conn, thistran, "DBSET", DAC.GetStringValue(ins_row["DBSET_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["DBSET_gkey"]));
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
                DBSETDao.Dispose();
                tb_DBSET_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_GridCtrl.Value = "rekey";
              BindNew(true);
              hh_GridCtrl.Value = "ser";
              SetSerMod();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_insok
          }  //ins
          else if (hh_GridCtrl.Value.ToLower() == "mod")
          {
            if (DBSETDao.IsExists("DBSET", "DBFLD", tx_DBSET_DBFLD.Text, "gkey<>'" + hh_GridGkey.Value + "' AND DBVER='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]) + "' AND DBAPX='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]) + "' AND DBTBL='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]) + "'  "))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_DBSET_DBFLD.Text + ",已存在.");
            }
            else
            {
              DataTable tb_DBSET_mod = new DataTable();
              DbDataAdapter da_ADP_mod = DBSETDao.GetDataAdapter(ApVer, "UNDBSET", "DBSET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_DBSET_mod);
              st_SelDataKey = "DBSET_gkey='" + hh_GridGkey.Value + "' and DBSET_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_DBSET_mod.Select(st_SelDataKey);
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
                  mod_row = mod_rows[0];
                  mod_row.BeginEdit();

                  mod_row["DBSET_DBVER"] = tx_DBSET_DBVER.Text.Trim();       // 版本編號
                  mod_row["DBSET_DBNUM"] = tx_DBSET_DBNUM.Text.Trim();       // 客戶編號
                  mod_row["DBSET_DBAPX"] = tx_DBSET_DBAPX.Text.Trim();       // 程式名稱
                  mod_row["DBSET_DBFLD"] = tx_DBSET_DBFLD.Text.Trim();       // 欄位名稱
                  mod_row["DBSET_DBTNA"] = tx_DBSET_DBTNA.Text.Trim();       // 繁體名稱
                  mod_row["DBSET_DBTYP"] = dr_DBSET_DBTYP.SelectedValue;     // 資料型態
                  mod_row["DBSET_DBLEN"] = tx_DBSET_DBLEN.Text.Trim();       // 資料長度
                  mod_row["DBSET_DBENA"] = tx_DBSET_DBENA.Text.Trim();       // 英文名稱
                  mod_row["DBSET_DBCNA"] = tx_DBSET_DBCNA.Text.Trim();       // 簡體名稱
                  mod_row["DBSET_DBJIA"] = tx_DBSET_DBJIA.Text.Trim();       // JoinAlias
                  mod_row["DBSET_DBJIN"] = tx_DBSET_DBJIN.Text.Trim();       // JoinTable
                  mod_row["DBSET_DBJIF"] = tx_DBSET_DBJIF.Text.Trim();       // ret field
                  mod_row["DBSET_DBJIK"] = tx_DBSET_DBJIK.Text.Trim();       // Join Key
                  mod_row["DBSET_DBROW"] = tx_DBSET_DBROW.Text.Trim();       // ROW 位置
                  mod_row["DBSET_DBCOL"] = tx_DBSET_DBCOL.Text.Trim();       // COL 位置
                  mod_row["DBSET_DBUCO"] = dr_DBSET_DBUCO.SelectedValue;       // 使用元件
                  mod_row["DBSET_DBWID"] = tx_DBSET_DBWID.Text.Trim();       // 元件寬度
                  mod_row["DBSET_DBUED"] = tx_DBSET_DBUED.Text.Trim();       // EDIT寬度
                  mod_row["DBSET_DBUTB"] = tx_DBSET_DBUTB.Text.Trim();       // 參考Table
                  mod_row["DBSET_DBUHO"] = tx_DBSET_DBUHO.Text.Trim();       // 參考Class
                  mod_row["DBSET_DBGRD"] = tx_DBSET_DBGRD.Text.Trim();       // GridList
                  mod_row["DBSET_DBDEF"] = tx_DBSET_DBDEF.Text.Trim();       // Default
                  mod_row["DBSET_DBPRY"] = ck_DBSET_DBPRY.Checked ? "1" : "0";       // Pri  Key
                  mod_row["DBSET_DBINS"] = ck_DBSET_DBINS.Checked ? "1" : "0";       // 是否新增
                  mod_row["DBSET_DBMOD"] = ck_DBSET_DBMOD.Checked ? "1" : "0";       // 是否更正
                  mod_row["DBSET_DBEMP"] = ck_DBSET_DBEMP.Checked ? "1" : "0";       // 是否空白
                  mod_row["DBSET_DBSER"] = ck_DBSET_DBSER.Checked ? "1" : "0";       // 查詢鍵值
                  mod_row["DBSET_DBSOR"] = ck_DBSET_DBSOR.Checked ? "1" : "0";       // 排序鍵值                  mod_row["DBSET_DBRMK"] = tx_DBSET_DBRMK.Text.Trim();       // 備註資料
                  mod_row["DBSET_DBREN"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]);        // B檔序號
                  mod_row["DBSET_DBTBL"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);         // TABLE名
                  mod_row["DBSET_DBTYD"] = "";       // 資料型態
                  mod_row["DBSET_DBUFX"] = "";       // DBUFX

                  mod_row["DBSET_mkey"] = DAC.get_guidkey();        //
                  mod_row["DBSET_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_DBSET_mod);
                  DBSETDao.Insertbalog(conn, thistran, "DBSET", hh_ActKey.Value, hh_GridGkey.Value);
                  DBSETDao.Insertbtlog(conn, thistran, "DBSET", DAC.GetStringValue(mod_row["DBSET_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["DBSET_gkey"]));
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
                  DBSETDao.Dispose();
                  tb_DBSET_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              //hh_GridCtrl.Value = "rekey";
              BindNew(true);
              hh_GridCtrl.Value = "ser";
              SetSerMod();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_updateok
          }   //mod
        }  //ins & mod
        DBSETDao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }
    }

    private bool ServerEditCheck(ref string sMsg)
    {
      bool ret;
      ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();

      ret = DataCheck.cIsStrEmptyChk(ret, tx_DBSET_DBVER.Text, lb_DBSET_DBVER.Text, ref sMsg, LangType, sFN);  //版本編號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DBSET_DBNUM.Text, lb_DBSET_DBNUM.Text, ref sMsg, LangType, sFN);  //客戶編號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DBSET_DBAPX.Text, lb_DBSET_DBAPX.Text, ref sMsg, LangType, sFN);  //程式名稱
      //
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DBSET_DBFLD.Text, lb_DBSET_DBFLD.Text, ref sMsg, LangType, sFN);  //欄位名稱
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DBSET_DBTNA.Text, lb_DBSET_DBTNA.Text, ref sMsg, LangType, sFN);  //繁體名稱
      ret = DataCheck.cIsStrIntChk(ret, tx_DBSET_DBLEN.Text, lb_DBSET_DBLEN.Text, ref sMsg, LangType, sFN); //資料長度
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DBSET_DBJIA.Text, lb_DBSET_DBJIA.Text, ref sMsg, LangType, sFN);  //JoinAlias
      ret = DataCheck.cIsStrIntChk(ret, tx_DBSET_DBROW.Text, lb_DBSET_DBROW.Text, ref sMsg, LangType, sFN); //ROW 位置
      ret = DataCheck.cIsStrIntChk(ret, tx_DBSET_DBCOL.Text, lb_DBSET_DBCOL.Text, ref sMsg, LangType, sFN); //COL 位置
      ret = DataCheck.cIsStrIntChk(ret, tx_DBSET_DBWID.Text, lb_DBSET_DBWID.Text, ref sMsg, LangType, sFN); //元件寬度
      ret = DataCheck.cIsStrIntChk(ret, tx_DBSET_DBUED.Text, lb_DBSET_DBUED.Text, ref sMsg, LangType, sFN); //EDIT寬度
      ret = DataCheck.cIsStrIntChk(ret, tx_DBSET_DBGRD.Text, lb_DBSET_DBGRD.Text, ref sMsg, LangType, sFN); //GridList
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DBSET_DBDEF.Text, lb_DBSET_DBDEF.Text, ref sMsg, LangType, sFN);  //Default

      DataCheck.Dispose();
      return ret;
    }


    protected void gr_GridView_DBSET_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //欄位名稱
        if (e.Row.FindControl("tx_DBSET_DBFLD02") != null)
        {
          TextBox tx_DBSET_DBFLD02 = e.Row.FindControl("tx_DBSET_DBFLD02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBFLD"]).Trim();
          tx_DBSET_DBFLD02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBFLD02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBFLD02, false); }
        }
        //繁體名稱
        if (e.Row.FindControl("tx_DBSET_DBTNA02") != null)
        {
          TextBox tx_DBSET_DBTNA02 = e.Row.FindControl("tx_DBSET_DBTNA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBTNA"]).Trim();
          tx_DBSET_DBTNA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBTNA02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBTNA02, false); }
        }
        //資料型態
        if (e.Row.FindControl("dr_DBSET_DBTYP02") != null)
        {
          DropDownList dr_DBSET_DBTYP02 = e.Row.FindControl("dr_DBSET_DBTYP02") as DropDownList;
          TextBox tx_DBSET_DBTYP02 = e.Row.FindControl("tx_DBSET_DBTYP02") as TextBox;
          //dr_DBSET_DBTYP02 = sFN.DropDownListFromTable(ref dr_DBSET_DBTYP02, "", "", "", "", "");
          dr_DBSET_DBTYP02 = sFN.DropDownListFromClasses(ref dr_DBSET_DBTYP02, "DATATYPE", "class_text", "class_value");
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBTYP"]).Trim();
          dr_DBSET_DBTYP02 = sFN.SetDropDownList(ref dr_DBSET_DBTYP02, st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.Drpdown_Set(ref dr_DBSET_DBTYP02, ref tx_DBSET_DBTYP02, true); } else { clsGV.Drpdown_Set(ref dr_DBSET_DBTYP02, ref tx_DBSET_DBTYP02, false); }
        }
        //資料長度
        if (e.Row.FindControl("tx_DBSET_DBLEN02") != null)
        {
          TextBox tx_DBSET_DBLEN02 = e.Row.FindControl("tx_DBSET_DBLEN02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBLEN"]).Trim();
          tx_DBSET_DBLEN02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBLEN02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBLEN02, false); }
        }
        //英文名稱
        if (e.Row.FindControl("tx_DBSET_DBENA02") != null)
        {
          TextBox tx_DBSET_DBENA02 = e.Row.FindControl("tx_DBSET_DBENA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBENA"]).Trim();
          tx_DBSET_DBENA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBENA02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBENA02, false); }
        }
        //簡體名稱
        if (e.Row.FindControl("tx_DBSET_DBCNA02") != null)
        {
          TextBox tx_DBSET_DBCNA02 = e.Row.FindControl("tx_DBSET_DBCNA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBCNA"]).Trim();
          tx_DBSET_DBCNA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBCNA02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBCNA02, false); }
        }
        //JoinAlias
        if (e.Row.FindControl("tx_DBSET_DBJIA02") != null)
        {
          TextBox tx_DBSET_DBJIA02 = e.Row.FindControl("tx_DBSET_DBJIA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBJIA"]).Trim();
          tx_DBSET_DBJIA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBJIA02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBJIA02, false); }
        }
        //JoinTable
        if (e.Row.FindControl("tx_DBSET_DBJIN02") != null)
        {
          TextBox tx_DBSET_DBJIN02 = e.Row.FindControl("tx_DBSET_DBJIN02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBJIN"]).Trim();
          tx_DBSET_DBJIN02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBJIN02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBJIN02, false); }
        }
        //ret field
        if (e.Row.FindControl("tx_DBSET_DBJIF02") != null)
        {
          TextBox tx_DBSET_DBJIF02 = e.Row.FindControl("tx_DBSET_DBJIF02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBJIF"]).Trim();
          tx_DBSET_DBJIF02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBJIF02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBJIF02, false); }
        }
        //Join Key
        if (e.Row.FindControl("tx_DBSET_DBJIK02") != null)
        {
          TextBox tx_DBSET_DBJIK02 = e.Row.FindControl("tx_DBSET_DBJIK02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBJIK"]).Trim();
          tx_DBSET_DBJIK02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBJIK02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBJIK02, false); }
        }
        //ROW 位置
        if (e.Row.FindControl("tx_DBSET_DBROW02") != null)
        {
          TextBox tx_DBSET_DBROW02 = e.Row.FindControl("tx_DBSET_DBROW02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBROW"]).Trim();
          tx_DBSET_DBROW02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBROW02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBROW02, false); }
        }
        //COL 位置
        if (e.Row.FindControl("tx_DBSET_DBCOL02") != null)
        {
          TextBox tx_DBSET_DBCOL02 = e.Row.FindControl("tx_DBSET_DBCOL02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBCOL"]).Trim();
          tx_DBSET_DBCOL02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_DBSET_DBCOL02, true); } else { clsGV.TextBox_Set(ref tx_DBSET_DBCOL02, false); }
        }
        //使用元件
        if (e.Row.FindControl("dr_DBSET_DBUCO02") != null)
        {
          DropDownList dr_DBSET_DBUCO02 = e.Row.FindControl("dr_DBSET_DBUCO02") as DropDownList;
          TextBox tx_DBSET_DBUCO02 = e.Row.FindControl("tx_DBSET_DBUCO02") as TextBox;
          //dr_DBSET_DBUCO02 = sFN.DropDownListFromTable(ref dr_DBSET_DBUCO02, "", "", "", "", "");
          dr_DBSET_DBUCO02 = sFN.DropDownListFromClasses(ref dr_DBSET_DBUCO02, "CONTROL_NAME", "class_text", "class_value");
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBUCO"]).Trim();
          dr_DBSET_DBUCO02 = sFN.SetDropDownList(ref dr_DBSET_DBUCO02, st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.Drpdown_Set(ref dr_DBSET_DBUCO02, ref tx_DBSET_DBUCO02, true); } else { clsGV.Drpdown_Set(ref dr_DBSET_DBUCO02, ref tx_DBSET_DBUCO02, false); }
        }
        //Pri  Key
        if (e.Row.FindControl("ck_DBSET_DBPRY02") != null)
        {
          CheckBox ck_DBSET_DBPRY02 = e.Row.FindControl("ck_DBSET_DBPRY02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBPRY"]).Trim();
          if (st_datavalue == "1") ck_DBSET_DBPRY02.Checked = true; else ck_DBSET_DBPRY02.Checked = false;
          if (hh_GridCtrl.Value == "modall") { ck_DBSET_DBPRY02.Enabled = true; } else { ck_DBSET_DBPRY02.Enabled = false; }
        }
        //是否新增
        if (e.Row.FindControl("ck_DBSET_DBINS02") != null)
        {
          CheckBox ck_DBSET_DBINS02 = e.Row.FindControl("ck_DBSET_DBINS02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBINS"]).Trim();
          if (st_datavalue == "1") ck_DBSET_DBINS02.Checked = true; else ck_DBSET_DBINS02.Checked = false;
          if (hh_GridCtrl.Value == "modall") { ck_DBSET_DBINS02.Enabled = true; } else { ck_DBSET_DBINS02.Enabled = false; }
        }
        //是否更正
        if (e.Row.FindControl("ck_DBSET_DBMOD02") != null)
        {
          CheckBox ck_DBSET_DBMOD02 = e.Row.FindControl("ck_DBSET_DBMOD02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBMOD"]).Trim();
          if (st_datavalue == "1") ck_DBSET_DBMOD02.Checked = true; else ck_DBSET_DBMOD02.Checked = false;
          if (hh_GridCtrl.Value == "modall") { ck_DBSET_DBMOD02.Enabled = true; } else { ck_DBSET_DBMOD02.Enabled = false; }
        }
        //是否空白
        if (e.Row.FindControl("ck_DBSET_DBEMP02") != null)
        {
          CheckBox ck_DBSET_DBEMP02 = e.Row.FindControl("ck_DBSET_DBEMP02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBEMP"]).Trim();
          if (st_datavalue == "1") ck_DBSET_DBEMP02.Checked = true; else ck_DBSET_DBEMP02.Checked = false;
          if (hh_GridCtrl.Value == "modall") { ck_DBSET_DBEMP02.Enabled = true; } else { ck_DBSET_DBEMP02.Enabled = false; }
        }
        //查詢鍵值
        if (e.Row.FindControl("ck_DBSET_DBSER02") != null)
        {
          CheckBox ck_DBSET_DBSER02 = e.Row.FindControl("ck_DBSET_DBSER02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBSER"]).Trim();
          if (st_datavalue == "1") ck_DBSET_DBSER02.Checked = true; else ck_DBSET_DBSER02.Checked = false;
          if (hh_GridCtrl.Value == "modall") { ck_DBSET_DBSER02.Enabled = true; } else { ck_DBSET_DBSER02.Enabled = false; }
        }
        //排序鍵值
        if (e.Row.FindControl("ck_DBSET_DBSOR02") != null)
        {
          CheckBox ck_DBSET_DBSOR02 = e.Row.FindControl("ck_DBSET_DBSOR02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["DBSET_DBSOR"]).Trim();
          if (st_datavalue == "1") ck_DBSET_DBSOR02.Checked = true; else ck_DBSET_DBSOR02.Checked = false;
          if (hh_GridCtrl.Value == "modall") { ck_DBSET_DBSOR02.Enabled = true; } else { ck_DBSET_DBSOR02.Enabled = false; }
        }
      }
    }

    protected bool UpdateDataAll(string st_ActKey, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_DBSET_gkey = "", st_DBSET_mkey = "", st_DBSET_DBFLD = "", st_DBSET_DBTNA = "", st_DBSET_DBTYP = "", st_DBSET_DBLEN = "", st_DBSET_DBENA = "", st_DBSET_DBCNA = "", st_DBSET_DBJIA = "", st_DBSET_DBJIN = "", st_DBSET_DBJIF = "", st_DBSET_DBJIK = "", st_DBSET_DBROW = "", st_DBSET_DBCOL = "", st_DBSET_DBUCO = "", st_DBSET_DBPRY = "", st_DBSET_DBINS = "", st_DBSET_DBMOD = "", st_DBSET_DBEMP = "", st_DBSET_DBSER = "", st_DBSET_DBSOR = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      //
      st_ctrl = st_ContentPlaceHolder + "gr_GridView_DBSET$ctl";
      CmdQueryS.CommandText = " AND A.DBVER='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]) + "' AND A.DBAPX='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]) + "' AND A.DBTBL='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]) + "' ";

      DataTable tb_DBSET = new DataTable();
      DAC DBSETDao = new DAC(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      DbDataAdapter da_ADP = DBSETDao.GetDataAdapter(ApVer, "UNDBSET", "DBSET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
      da_ADP.Fill(tb_DBSET);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_DBSET.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_gkey02";
            st_DBSET_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //mkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_mkey02";
            st_DBSET_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //欄位名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBFLD02";
            st_DBSET_DBFLD = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //繁體名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBTNA02";
            st_DBSET_DBTNA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //資料型態
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$dr_DBSET_DBTYP02";
            st_DBSET_DBTYP = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;
            //資料長度
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBLEN02";
            st_DBSET_DBLEN = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //英文名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBENA02";
            st_DBSET_DBENA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //簡體名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBCNA02";
            st_DBSET_DBCNA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //JoinAlias
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBJIA02";
            st_DBSET_DBJIA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //JoinTable
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBJIN02";
            st_DBSET_DBJIN = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //ret field
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBJIF02";
            st_DBSET_DBJIF = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //Join Key
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBJIK02";
            st_DBSET_DBJIK = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //ROW 位置
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBROW02";
            st_DBSET_DBROW = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //COL 位置
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_DBSET_DBCOL02";
            st_DBSET_DBCOL = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //使用元件
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$dr_DBSET_DBUCO02";
            st_DBSET_DBUCO = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;
            //Pri  Key
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_DBSET_DBPRY02";
            st_DBSET_DBPRY = ((CheckBox)FindControl(st_ctrlname)).Checked ? "1" : "0";
            //是否新增
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_DBSET_DBINS02";
            st_DBSET_DBINS = ((CheckBox)FindControl(st_ctrlname)).Checked ? "1" : "0";
            //是否更正
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_DBSET_DBMOD02";
            st_DBSET_DBMOD = ((CheckBox)FindControl(st_ctrlname)).Checked ? "1" : "0";
            //是否空白
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_DBSET_DBEMP02";
            st_DBSET_DBEMP = ((CheckBox)FindControl(st_ctrlname)).Checked ? "1" : "0";
            //查詢鍵值
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_DBSET_DBSER02";
            st_DBSET_DBSER = ((CheckBox)FindControl(st_ctrlname)).Checked ? "1" : "0";
            //排序鍵值
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_DBSET_DBSOR02";
            st_DBSET_DBSOR = DAC.GetStringValueBool(((CheckBox)FindControl(st_ctrlname)).Checked);
            //
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_DBSET.Select("DBSET_gkey='" + st_DBSET_gkey + "'");
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (
                   (DAC.GetStringValue(mod_row["DBSET_DBFLD"]) != st_DBSET_DBFLD)
                || (DAC.GetStringValue(mod_row["DBSET_DBTNA"]) != st_DBSET_DBTNA)
                || (DAC.GetStringValue(mod_row["DBSET_DBTYP"]) != st_DBSET_DBTYP)
                || (DAC.GetStringValue(mod_row["DBSET_DBLEN"]) != st_DBSET_DBLEN)
                || (DAC.GetStringValue(mod_row["DBSET_DBENA"]) != st_DBSET_DBENA)
                || (DAC.GetStringValue(mod_row["DBSET_DBCNA"]) != st_DBSET_DBCNA)
                || (DAC.GetStringValue(mod_row["DBSET_DBJIA"]) != st_DBSET_DBJIA)
                || (DAC.GetStringValue(mod_row["DBSET_DBJIN"]) != st_DBSET_DBJIN)
                || (DAC.GetStringValue(mod_row["DBSET_DBJIF"]) != st_DBSET_DBJIF)
                || (DAC.GetStringValue(mod_row["DBSET_DBJIK"]) != st_DBSET_DBJIK)
                || (DAC.GetStringValue(mod_row["DBSET_DBROW"]) != st_DBSET_DBROW)
                || (DAC.GetStringValue(mod_row["DBSET_DBCOL"]) != st_DBSET_DBCOL)
                || (DAC.GetStringValue(mod_row["DBSET_DBUCO"]) != st_DBSET_DBUCO)
                || (DAC.GetStringValue(mod_row["DBSET_DBPRY"]) != st_DBSET_DBPRY)
                || (DAC.GetStringValue(mod_row["DBSET_DBINS"]) != st_DBSET_DBINS)
                || (DAC.GetStringValue(mod_row["DBSET_DBMOD"]) != st_DBSET_DBMOD)
                || (DAC.GetStringValue(mod_row["DBSET_DBEMP"]) != st_DBSET_DBEMP)
                || (DAC.GetStringValue(mod_row["DBSET_DBSER"]) != st_DBSET_DBSER)
                || (DAC.GetStringValue(mod_row["DBSET_DBSOR"]) != st_DBSET_DBSOR)
              )
              {
                DBSETDao.Insertbalog(conn, thistran, "DBSET", st_ActKey, st_DBSET_gkey);
                DBSETDao.Insertbtlog(conn, thistran, "DBSET", DAC.GetStringValue(mod_row["DBSET_gkey"]), "M", UserGkey, DAC.GetStringValue(mod_row["DBSET_gkey"]) + " " + DAC.GetStringValue(mod_row["DBSET_gkey"]) + " " + DAC.GetStringValue(mod_row["DBSET_gkey"]));
                mod_row.BeginEdit();
                mod_row["DBSET_DBFLD"] = st_DBSET_DBFLD;      //欄位名稱
                mod_row["DBSET_DBTNA"] = st_DBSET_DBTNA;      //繁體名稱
                mod_row["DBSET_DBTYP"] = st_DBSET_DBTYP;      //資料型態
                mod_row["DBSET_DBLEN"] = st_DBSET_DBLEN;      //資料長度
                mod_row["DBSET_DBENA"] = st_DBSET_DBENA;      //英文名稱
                mod_row["DBSET_DBCNA"] = st_DBSET_DBCNA;      //簡體名稱
                mod_row["DBSET_DBJIA"] = st_DBSET_DBJIA;      //JoinAlias
                mod_row["DBSET_DBJIN"] = st_DBSET_DBJIN;      //JoinTable
                mod_row["DBSET_DBJIF"] = st_DBSET_DBJIF;      //ret field
                mod_row["DBSET_DBJIK"] = st_DBSET_DBJIK;      //Join Key
                mod_row["DBSET_DBROW"] = st_DBSET_DBROW;      //ROW 位置
                mod_row["DBSET_DBCOL"] = st_DBSET_DBCOL;      //COL 位置
                mod_row["DBSET_DBUCO"] = st_DBSET_DBUCO;      //使用元件
                mod_row["DBSET_DBPRY"] = st_DBSET_DBPRY;      //Pri  Key
                mod_row["DBSET_DBINS"] = st_DBSET_DBINS;      //是否新增
                mod_row["DBSET_DBMOD"] = st_DBSET_DBMOD;      //是否更正
                mod_row["DBSET_DBEMP"] = st_DBSET_DBEMP;      //是否空白
                mod_row["DBSET_DBSER"] = st_DBSET_DBSER;      //查詢鍵值
                mod_row["DBSET_DBSOR"] = st_DBSET_DBSOR;      //排序鍵值
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();  //
              }
            }  //sel_rows.Length == 1
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_DBSET);
        thistran.Commit();
        bl_updateok = true;
      }  //try
      catch (Exception e)
      {
        thistran.Rollback();
        bl_updateok = false;
        st_errmsg += e.Message;
      }
      finally
      {
        thistran.Dispose();
        DBSETDao.Dispose();
        tb_DBSET.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }

    protected void bt_06_Click(object sender, EventArgs e)
    {
      actCOPY();
    }

    protected void actCOPY()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 6, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "ins";
        Set_Control();
        SetEditMod();
        //定義guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(false);
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_DBSET_DBFLD').focus(); </script>";
      }
    }

    protected void bt_11_Click1(object sender, EventArgs e)
    {
      actMODALL();
    }

    protected void bt_12_Click(object sender, EventArgs e)
    {
      Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
      Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
      Session["fmDBSET_gr_GridView_DBSET_GridGkey"] = gr_GridView_DBSET.DataKeys[gr_GridView_DBSET.SelectedIndex].Value.ToString();
      //
      Session["fm_httpx_exit"] = "~\\forms\\dax\\dx_dbset.aspx";
      Session["fm_httpx_HyFile1_Text"] = "";
      Session["fm_httpx_HyFile1_url"] = "";
      Session["fm_httpx_HyFile2_Text"] = "";
      Session["fm_httpx_HyFile2_url"] = "";
      //
      act_SQL();
      //
      Response.Redirect("~\\forms\\Download\\fm_httpx.aspx");
    }

    protected void act_SQL()
    {
      StreamWriter Fhdsw;
      DAC dbset_DAO = new DAC(conn);
      OleDbCommand cmd = new OleDbCommand();
      DataTable tb_DBSET = new DataTable();
      DataRow dr_ROW;
      string v_Str = "";
      string v_Type = "";
      string v_Empty = "";
      string v_doc_file = "";
      string v_doc_http = "";
      string v_doc_guid = sFN.Get_guid5();
      string v_fdef = "";
      //
      v_doc_file = sys_DocFilePath + tx_DBSET_DBAPX.Text + "_SQL_" + v_doc_guid + ".txt";
      v_doc_http = tx_DBSET_DBAPX.Text + "_SQL_" + v_doc_guid + ".txt";
      Fhdsw = File.CreateText(v_doc_file);
      cmd.CommandText = "SELECT * FROM DBSET WHERE DBVER=? AND DBREN=? AND DBAPX=? AND DBNUM=? AND DBTBL=? ORDER BY DBROW,DBCOL ";
      DAC.AddParam(cmd, "DBVER", tx_DBSET_DBVER.Text);
      DAC.AddParam(cmd, "DBREN", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]));
      DAC.AddParam(cmd, "DBAPX", tx_DBSET_DBAPX.Text);
      DAC.AddParam(cmd, "DBNUM", tx_DBSET_DBNUM.Text);
      DAC.AddParam(cmd, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
      tb_DBSET = dbset_DAO.Select(cmd);
      //
      v_Str += "CREATE TABLE dbo." + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]) + "(" + s1310;
      v_Str += "  gkey ";
      v_Str += "VARCHAR(40) NOT NULL, ";
      v_Str += "/* gkey */" + s1310;
      for (int in_rec = 0; in_rec < tb_DBSET.Rows.Count; in_rec++)
      {
        dr_ROW = tb_DBSET.Rows[in_rec];
        v_fdef = "";
        if (DAC.GetStringValue(dr_ROW["DBJIA"]) == "A")
        {
          v_Type = DAC.GetStringValue(dr_ROW["DBTYP"]).ToUpper();
          v_Empty = DAC.GetStringValue(dr_ROW["DBEMP"]).ToUpper();
          //
          v_fdef = DAC.GetStringValue(dr_ROW["DBDEF"]);
          v_fdef = v_fdef.Replace("ALTER", "");
          v_fdef = v_fdef.Replace("<>", "");
          if (v_fdef == "")
          {
            if ((v_Type.IndexOf("INT") >= 0) || (v_Type == "CURRENCY") || (v_Type == "REAL") || (v_Type == "FLOAT"))
            {
              v_fdef = "0";
            }
            else if (v_Type.IndexOf("CHAR") >= 0)
            {
              v_fdef = "''";
            }
          }
          //n,1,2=null 0=not null
          if ((v_Empty == "N") || (v_Empty == "1") || (v_Empty == "2")) { v_Empty = "N"; }
          //
          if ((v_Type != "ADDRESS") && (v_Type != "NADDRESS"))
          {
            v_Str += "  " + DAC.GetStringValue(dr_ROW["DBFLD"]) + "  ";
          }
          else
          {
            v_Str += "  " + DAC.GetStringValue(dr_ROW["DBFLD"]).Substring(0, 4) + "1" + "  ";
          }
          if (v_Type == "CURRENCY")
          {
            v_Str += "Money";
          }
          else if (v_Type == "INT_IDENT")
          {
            v_Str += "INT IDENTITY (1, 1)";
          }
          else
          {
            v_Str += DAC.GetStringValue(dr_ROW["DBTYP"]);
          }
          //
          if ((v_Type == "NVARCHAR") || (v_Type == "VARCHAR") || (v_Type == "CHAR") || (v_Type == "NCHAR"))
          {
            v_Str += "(" + DAC.GetStringValue(dr_ROW["DBLEN"]) + ") ";
          }
          //
          if ((v_Type != "ADDRESS") && (v_Type != "NADDRESS"))
          {
            if ((v_Empty != "N") && (v_Type == "DATETIME") )
            {
              v_Str += " NOT NULL ," + "/*" + DAC.GetStringValue(dr_ROW["DBTNA"]) + " */" + s1310;
            }
            else if (v_Type == "INT_IDENT") 
            {
              v_Str += " NOT NULL DEFAULT 0," + "/*" + DAC.GetStringValue(dr_ROW["DBTNA"]) + " */" + s1310;
            }
            else if ((v_Empty != "N") )
            {
              if (v_fdef != "")
              {
                v_Str = v_Str + " NOT NULL DEFAULT " + v_fdef + " ," + "/*" + DAC.GetStringValue(dr_ROW["DBTNA"]) + " */" + s1310;
              }
              else
              {
                v_Str = v_Str + " NOT NULL ," + "/*" + DAC.GetStringValue(dr_ROW["DBTNA"]) + " */" + s1310;
              }
            }
            else
            {
              if (v_fdef != "") 
              {
                v_Str = v_Str + " NULL DEFAULT " + v_fdef + " ," + "/*" + DAC.GetStringValue(dr_ROW["DBTNA"]) + " */" + s1310;
              }
              else
              {
                v_Str = v_Str + " NULL ," + "/*" + DAC.GetStringValue(dr_ROW["DBTNA"]) + " */" + s1310;
              }
            }
          }
          //
        }
      }
      v_Str += "  mkey   VARCHAR(40)     NOT NULL, /*異動key */ " + s1310;
      v_Str += "  trcls  VARCHAR(10) NOT NULL, /*異動旗標 */ " + s1310;
      v_Str += "  trcrd  DATETIME    NOT NULL, /*建檔日期 */ " + s1310;
      v_Str += "  trmod  DATETIME    NOT NULL, /*更正日期 */ " + s1310;
      v_Str += "  trusr  NVARCHAR(40) NOT NULL  /*更正人員 */ " + s1310;
      v_Str += "  ) ON [PRIMARY] " + s1310;
      //
      v_Str += s1310;
      v_Str += "ALTER TABLE [dbo].[" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]) + "] WITH NOCHECK ADD " + s1310;
      v_Str += "  CONSTRAINT [PK_" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]) + "] PRIMARY KEY  NONCLUSTERED " + s1310;
      v_Str += "  (" + s1310;
      v_Str += "    [gkey]" + s1310;
      v_Str += "  )  ON [PRIMARY] " + s1310;
      //
      Fhdsw.Write(v_Str);
      Fhdsw.Close();
      Fhdsw.Dispose();
      dbset_DAO.Dispose();
      cmd.Dispose();
      tb_DBSET.Dispose();
      Session["fm_httpx_HyFile1_Text"] = "Microsoft Sql Schema. Table:" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      Session["fm_httpx_HyFile1_url"] = v_doc_http;
    }

    protected void bt_13_Click(object sender, EventArgs e)
    {
      Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
      Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
      Session["fmDBSET_gr_GridView_DBSET_GridGkey"] = gr_GridView_DBSET.DataKeys[gr_GridView_DBSET.SelectedIndex].Value.ToString();
      //
      Session["fm_httpx_exit"] = "~\\forms\\dax\\dx_dbset.aspx";
      Session["fm_httpx_HyFile1_Text"] = "";
      Session["fm_httpx_HyFile1_url"] = "";
      Session["fm_httpx_HyFile2_Text"] = "";
      Session["fm_httpx_HyFile2_url"] = "";
      Session["fm_httpx_HyFile3_Text"] = "";
      Session["fm_httpx_HyFile3_url"] = "";
      Session["fm_httpx_HyFile4_Text"] = "";
      Session["fm_httpx_HyFile4_url"] = "";
      //
      act_Grid();
      act_HTML();
      //act_WebDataGrid();
      //
      Response.Redirect("~\\forms\\Download\\fm_httpx.aspx");
    }

    protected void act_WebDataGrid()
    {
      string v_Str_YNET_grid;
      string v_Str_YNET_update0, v_Str_YNET_update;
      string v_Str_YNET_dim;
      string v_Str_YNET_code;
      string v_Str_YNET_updateF1, v_Str_YNET_updateF2, v_Str_YNET_updateF2S, v_Str_YNET_updateF2B, v_Str_YNET_updateL;
      //string vOROW;
      string j_Tbl;
      string v_Tbl = DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      string v_Apx = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]);
      //
      StreamWriter Fhdsw;
      DAC dbset_DAO = new DAC(conn);
      OleDbCommand cmd = new OleDbCommand();
      DataTable tb_DBSET = new DataTable();
      DataRow dr_ROW;
      //
      string v_doc_file = "";
      string v_doc_http = "";
      string v_doc_guid = sFN.Get_guid5();
      //
      v_doc_file = sys_DocFilePath + tx_DBSET_DBAPX.Text + "_WebDataGrid_" + v_doc_guid + ".txt";
      v_doc_http = tx_DBSET_DBAPX.Text + "_WebDataGrid_" + v_doc_guid + ".txt";
      //
      Fhdsw = File.CreateText(v_doc_file);
      cmd.CommandText = "SELECT * FROM DBSET WHERE DBVER=? AND DBREN=? AND DBAPX=? AND DBNUM=? AND DBTBL=? ORDER BY DBROW,DBCOL ";
      DAC.AddParam(cmd, "DBVER", tx_DBSET_DBVER.Text);
      DAC.AddParam(cmd, "DBREN", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]));
      DAC.AddParam(cmd, "DBAPX", tx_DBSET_DBAPX.Text);
      DAC.AddParam(cmd, "DBNUM", tx_DBSET_DBNUM.Text);
      DAC.AddParam(cmd, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
      tb_DBSET = dbset_DAO.Select(cmd);
      //
      //vOROW = "#";
      v_Str_YNET_update0 = "protected bool UpdateDataAll(string st_ActKey, ref string st_errmsg)" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "{" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  bool bl_updateok = false;" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  bool bl_Mod = false;" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  //" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  string st_ctrl = " + s34 + s34 + ", st_ctrlname = " + s34 + s34 + ";" + s1310;
      //
      v_Str_YNET_update = "";
      v_Str_YNET_update = v_Str_YNET_update + "  DataRow mod_row;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DataRow[] sel_rows;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  //" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  st_ctrl = st_ContentPlaceHolder +" + s34 + "gr_GridView_" + v_Tbl + "$ctl" + s34 + "; " + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  CmdQueryS.CommandText = " + s34 + " and a.gkey='" + s34 + "+hh_qkey.Value+" + s34 + "'" + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DataTable tb_" + v_Tbl + " = new DataTable();" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DAC_" + v_Tbl + " " + v_Tbl + "Dao = new DAC_" + v_Tbl + "(conn);" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  string st_addselect=" + s34 + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  string st_addjoin=" + s34 + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  string st_addunion=" + s34 + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DbDataAdapter da_ADP =" + v_Tbl + "Dao.GetDataAdapter(ApVer, " + s34 + v_Apx + s34 + ", " + s34 + v_Tbl + s34 + ", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " + s34 + "" + s34 + ");" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.Fill(tb_" + v_Tbl + ");" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  //" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  OleDbTransaction thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  conn.Open();" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.UpdateCommand.Transaction = thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.DeleteCommand.Transaction = thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.InsertCommand.Transaction = thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  try" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  {" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "    for (int in_g = 0; in_g <= gr_GridView_" + v_Tbl + ".Rows.Count + 4; in_g++)" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "    {" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "      //gkey" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "      st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+" + s34 + "$tx_" + v_Tbl + "_gkey02" + s34 + ";" + s1310;
      //
      v_Str_YNET_updateF1 = "";
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      if (FindControl(st_ctrlname) != null)" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      {" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //gkey" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+" + s34 + "$tx_" + v_Tbl + "_gkey02" + s34 + ";" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + v_Tbl + "_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //mkey" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+" + s34 + "$tx_" + v_Tbl + "_mkey02" + s34 + ";" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + v_Tbl + "_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();" + s1310;
      //
      v_Str_YNET_updateF2 = "";
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "      if (bl_Mod) " + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "      {" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "        sel_rows = tb_" + v_Tbl + ".Select(" + s34 + v_Tbl + "_gkey='" + s34 + "+st_" + v_Tbl + "_gkey" + "+" + s34 + "'" + s34 + ");" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "        if (sel_rows.Length == 1) " + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "        {" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          mod_row = sel_rows[0];" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          if (" + s1310;
      v_Str_YNET_updateF2S = "";
      v_Str_YNET_updateF2B = "";
      v_Str_YNET_dim = "  string st_" + v_Tbl + "_gkey=" + s34 + s34 + "," + " st_" + v_Tbl + "_mkey=" + s34 + s34 + ",";
      //
      v_Str_YNET_code = "protected void gr_GridView_" + v_Tbl + "_RowDataBound(object sender, GridViewRowEventArgs e)" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "{" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "  string st_datavalue = " + s34 + s34 + "; " + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "  if (e.Row.RowIndex >= 0) " + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "  {" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "    DataRowView rowView = (DataRowView)e.Row.DataItem;" + s1310;
      //
      v_Str_YNET_grid = "<asp:GridView ID=" + s34 + "gr_GridView_" + v_Tbl + s34 + " runat=" + s34 + "server" + s34 + " AutoGenerateColumns=" + s34 + "False" + s34 + " DataKeyNames=" + s34 + v_Tbl + "_gkey" + s34 + " EnableModelValidation=" + s34 + "True" + s34 + " AllowPaging=" + s34 + "false" + s34 + " OnRowDataBound=" + s34 + "gr_GridView_" + v_Tbl + "_RowDataBound" + s34 + ">" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "  <Columns>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%# DD2012.PublicVariable.st_choose %></b>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:ImageButton CommandName=" + s34 + "Select" + s34 + " ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem," + s34 + v_Tbl + "_gkey" + s34 + ").ToString() ? " + s34 + "~\\images\\GridCheck.gif" + s34 + ":" + s34 + "~\\images\\GridUnCheck.gif" + s34 + " %>'" + " runat=" + s34 + "server" + s34 + " ID=" + s34 + "Imagebutton1" + s34 + " />" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <input id=" + s34 + "tx_" + v_Tbl + "_gkey02" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "tx_" + v_Tbl + "_gkey02" + s34 + " value='<%# DataBinder.Eval(Container.DataItem," + s34 + v_Tbl + "_gkey" + s34 + ").ToString() %>' runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <input id=" + s34 + "tx_" + v_Tbl + "_mkey02" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "tx_" + v_Tbl + "_mkey02" + s34 + " value='<%# DataBinder.Eval(Container.DataItem," + s34 + v_Tbl + "_mkey" + s34 + ").ToString() %>' runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
      for (int in_rec = 0; in_rec < tb_DBSET.Rows.Count; in_rec++)
      {
        dr_ROW = tb_DBSET.Rows[in_rec];
        if (DAC.GetStringValue(dr_ROW["DBGRD"]) != "0")
        {
          if (DAC.GetStringValue(dr_ROW["DBJIA"]) == "A")
          {
            j_Tbl = v_Tbl + "_";
          }
          else
          {
            j_Tbl = DAC.GetStringValue(dr_ROW["DBJIN"]);
            j_Tbl = "";
          }
          //
          if (v_Str_YNET_updateF2S == "")
          {
            v_Str_YNET_updateF2S = v_Str_YNET_updateF2S + "               (DAC.GetStringValue(mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]) != st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ") " + s1310;
            v_Str_YNET_dim = v_Str_YNET_dim + "st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "=" + s34 + s34;
          }
          else
          {
            v_Str_YNET_updateF2S = v_Str_YNET_updateF2S + "            || (DAC.GetStringValue(mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]) != st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ") " + s1310;
            v_Str_YNET_dim = v_Str_YNET_dim + ",st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "=" + s34 + s34;
          }
          //
          if (v_Str_YNET_updateF2B == "")
          {
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "           " + v_Tbl + "Dao.Insertbalog(conn, thistran, " + s34 + v_Tbl + s34 + ", st_ActKey, st_" + v_Tbl + "_gkey);" + s1310;
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "           " + v_Tbl + "Dao.Insertbtlog(conn, thistran, " + s34 + v_Tbl + s34 + ", DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]), " + s34 + "M" + s34 + ",UserGkey, DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]) + " + s34 + " " + s34 + " + DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]) +" + s34 + " " + s34 + " + DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]));" + s1310;
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row.BeginEdit();" + s1310;
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "] = st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ";      //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          }
          else
          {
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "] = st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ";      //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          }
          //
          if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "DROPDOWNLIST")
          {
            v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:TextBox visible=" + s34 + "false" + s34 + "  runat=" + s34 + "server" + s34 + " ID=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + "/>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:dropdownlist Width=" + s34 + "70px" + s34 + "  runat=" + s34 + "server" + s34 + " ID=" + s34 + "dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + "/>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
            //
            v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      DropDownList dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as DropDownList;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      TextBox tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as TextBox;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = sFN.DropDownListFromTable(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02," + s34 + DAC.GetStringValue(dr_ROW["DBJIN"]) + s34 + ", " + s34 + DAC.GetStringValue(dr_ROW["DBJIK"]) + s34 + ", " + s34 + DAC.GetStringValue(dr_ROW["DBJIF"]) + s34 + ", " + s34 + s34 + ", " + s34 + DAC.GetStringValue(dr_ROW["DBJIF"]) + s34 + ");" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      st_datavalue = DAC.GetStringValue(rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]).Trim();" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = sFN.SetDropDownList(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, st_datavalue);" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {clsGV.Drpdown_Set(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, true);} else {clsGV.Drpdown_Set(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, false);}" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
            //
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + " + s34 + "$dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " ;" + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + " = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "CHECKBOX")
          {
            v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:checkbox width=" + s34 + "70px" + s34 + " runat=" + s34 + "server" + s34 + " id=" + s34 + "ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + "/>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
            //
            v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      CheckBox ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as CheckBox;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      st_datavalue = DAC.GetStringValue(rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]).Trim();" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      if (st_datavalue == " + s34 + "1" + s34 + ") ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Checked = true; else ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Checked = false;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Enabled = true;} else {ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Enabled = false;}" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
            //
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) +" + s34 + "$ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + " = ((CheckBox)FindControl(st_ctrlname)).Checked ? " + s34 + "1" + s34 + " : " + s34 + "0" + s34 + " ;" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "TEXTBOX")
          {
            if (DAC.GetStringValue(dr_ROW["DBFLD"]).ToUpper().IndexOf("GKEY") >= 0 || DAC.GetStringValue(dr_ROW["DBFLD"]).ToUpper().IndexOf("MKEY") >= 0)
            {
              v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <input id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " value='<%# DataBinder.Eval(Container.DataItem," + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + ").ToString() %>' runat=" + s34 + "server" + s34 + "/> " + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
              //
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+ " + s34 + "$tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "= ((TextBox)FindControl(st_ctrlname)).Text.Trim();" + s1310;
            }
            else if (DAC.GetStringValue(dr_ROW["DBUHO"]).ToUpper() == "DATETIME")
            {
              v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:TextBox id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " runat=" + s34 + "server" + s34 + " MaxLength=" + s34 + DAC.GetStringValue(dr_ROW["DBLEN"]) + s34 + " Width=" + s34 + "80px" + s34 + " TEXT='<%# sFN.DateToDateString(Convert.ToDateTime(DataBinder.Eval(Container.DataItem," + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + ")),sys_DateFormat) %>'></asp:TextBox>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
              //
              v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      TextBox tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as TextBox;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      if (rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]==DBNull.Value)" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      {" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "        st_datavalue=" + s34 + s34 + ";" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      }" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      else" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      { " + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "        st_datavalue=sFN.DateToDateString(  DAC.GetDateTimeValue( rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]),sys_DateFormat);" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      }" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Text = st_datavalue;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, true);} else {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, false);}" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
              //
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + " + s34 + "$tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "= ((TextBox)FindControl(st_ctrlname)).Text.Trim();" + s1310;
            }
            else
            {
              v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:TextBox id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " runat=" + s34 + "server" + s34 + " MaxLength=" + s34 + DAC.GetStringValue(dr_ROW["DBLEN"]) + s34 + " Width=" + s34 + "60px" + s34 + " TEXT='<%# DataBinder.Eval(Container.DataItem," + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + ").ToString() %>'></asp:TextBox>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
              //
              v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      TextBox tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as TextBox;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      st_datavalue = DAC.GetStringValue(rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]).Trim();" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Text = st_datavalue;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, true);} else {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, false);}" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
              //
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + " + s34 + "$tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "= ((TextBox)FindControl(st_ctrlname)).Text.Trim();" + s1310;
            }
          }
        }
      }
      v_Str_YNET_grid = v_Str_YNET_grid + "  </Columns>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "</asp:GridView>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "<asp:Literal ID=" + s34 + "li_Msg" + s34 + " runat=" + s34 + "server" + s34 + "></asp:Literal>" + s1310;
      //
      v_Str_YNET_code = v_Str_YNET_code + "  }" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "}" + s1310;
      //
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        bl_Mod = true;" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      }" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      else" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      {" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        bl_Mod = false;" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      }" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      //" + s1310;
      //
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + v_Str_YNET_updateF2S;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          )" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          {" + s1310;
      //
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row.EndEdit();" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            st_ActKey=DAC.get_guidkey();  //" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "          }" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "        }  //sel_rows.Length == 1" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "      }  //bl_Mod" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    }  //for" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    da_ADP.Update(tb_" + v_Tbl + ");" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    thistran.Commit();" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    bl_updateok=true;" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "  }  //try" + s1310;
      //
      v_Str_YNET_updateL = "" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  catch (Exception e)" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  {" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    thistran.Rollback();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    bl_updateok = false;" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    st_errmsg += e.Message;" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  }" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  finally" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  {" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    thistran.Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    " + v_Tbl + "Dao.Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    tb_" + v_Tbl + ".Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    da_ADP.Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  }" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  return bl_updateok;" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "}" + s1310;
      //
      Fhdsw.Write(v_Str_YNET_grid + s1310 + s1310 + v_Str_YNET_code + s1310 + s1310 + v_Str_YNET_update0 + v_Str_YNET_dim + ";" + s1310 + v_Str_YNET_update + v_Str_YNET_updateF1 + v_Str_YNET_updateF2 + v_Str_YNET_updateF2B + v_Str_YNET_updateL);
      Fhdsw.Close();
      Fhdsw.Dispose();
      dbset_DAO.Dispose();
      cmd.Dispose();
      tb_DBSET.Dispose();
      //
      Session["fm_httpx_HyFile3_Text"] = "WebDataGrid Source & HTML:" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      Session["fm_httpx_HyFile3_url"] = v_doc_http;
    }


    protected void act_Grid()
    {
      string v_Str_YNET_grid;
      string v_Str_YNET_update0, v_Str_YNET_update;
      string v_Str_YNET_dim;
      string v_Str_YNET_code;
      string v_Str_YNET_updateF1, v_Str_YNET_updateF2, v_Str_YNET_updateF2S, v_Str_YNET_updateF2B, v_Str_YNET_updateL;
      //string vOROW;
      string j_Tbl;
      string v_Tbl = DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      string v_Apx = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]);
      //
      StreamWriter Fhdsw;
      DAC dbset_DAO = new DAC(conn);
      OleDbCommand cmd = new OleDbCommand();
      DataTable tb_DBSET = new DataTable();
      DataRow dr_ROW;
      //
      string v_doc_file = "";
      string v_doc_http = "";
      string v_doc_guid = sFN.Get_guid5();
      //
      v_doc_file = sys_DocFilePath + tx_DBSET_DBAPX.Text + "_GRID_" + v_doc_guid + ".txt";
      v_doc_http = tx_DBSET_DBAPX.Text + "_GRID_" + v_doc_guid + ".txt";
      //
      Fhdsw = File.CreateText(v_doc_file);
      cmd.CommandText = "SELECT * FROM DBSET WHERE DBVER=? AND DBREN=? AND DBAPX=? AND DBNUM=? AND DBTBL=? ORDER BY DBROW,DBCOL ";
      DAC.AddParam(cmd, "DBVER", tx_DBSET_DBVER.Text);
      DAC.AddParam(cmd, "DBREN", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]));
      DAC.AddParam(cmd, "DBAPX", tx_DBSET_DBAPX.Text);
      DAC.AddParam(cmd, "DBNUM", tx_DBSET_DBNUM.Text);
      DAC.AddParam(cmd, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
      tb_DBSET = dbset_DAO.Select(cmd);
      //
      //vOROW = "#";
      v_Str_YNET_update0 = "protected bool UpdateDataAll(string st_ActKey, ref string st_errmsg)" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "{" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  bool bl_updateok = false;" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  bool bl_Mod = false;" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  //" + s1310;
      v_Str_YNET_update0 = v_Str_YNET_update0 + "  string st_ctrl = " + s34 + s34 + ", st_ctrlname = " + s34 + s34 + ";" + s1310;
      //
      v_Str_YNET_update = "";
      v_Str_YNET_update = v_Str_YNET_update + "  DataRow mod_row;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DataRow[] sel_rows;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  //" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  st_ctrl = st_ContentPlaceHolder +" + s34 + "gr_GridView_" + v_Tbl + "$ctl" + s34 + "; " + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  CmdQueryS.CommandText = " + s34 + " and a.gkey='" + s34 + "+hh_qkey.Value+" + s34 + "'" + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DataTable tb_" + v_Tbl + " = new DataTable();" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DAC_" + v_Tbl + " " + v_Tbl + "Dao = new DAC_" + v_Tbl + "(conn);" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  string st_addselect=" + s34 + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  string st_addjoin=" + s34 + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  string st_addunion=" + s34 + s34 + ";" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  DbDataAdapter da_ADP =" + v_Tbl + "Dao.GetDataAdapter(ApVer, " + s34 + v_Apx + s34 + ", " + s34 + v_Tbl + s34 + ", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " + s34 + "" + s34 + ");" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.Fill(tb_" + v_Tbl + ");" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  //" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  OleDbTransaction thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  conn.Open();" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.UpdateCommand.Transaction = thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.DeleteCommand.Transaction = thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  da_ADP.InsertCommand.Transaction = thistran;" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  try" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "  {" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "    for (int in_g = 0; in_g <= gr_GridView_" + v_Tbl + ".Rows.Count + 4; in_g++)" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "    {" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "      //gkey" + s1310;
      v_Str_YNET_update = v_Str_YNET_update + "      st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+" + s34 + "$tx_" + v_Tbl + "_gkey02" + s34 + ";" + s1310;
      //
      v_Str_YNET_updateF1 = "";
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      if (FindControl(st_ctrlname) != null)" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      {" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //gkey" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+" + s34 + "$tx_" + v_Tbl + "_gkey02" + s34 + ";" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + v_Tbl + "_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //mkey" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+" + s34 + "$tx_" + v_Tbl + "_mkey02" + s34 + ";" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + v_Tbl + "_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();" + s1310;
      //
      v_Str_YNET_updateF2 = "";
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "      if (bl_Mod) " + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "      {" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "        sel_rows = tb_" + v_Tbl + ".Select(" + s34 + v_Tbl + "_gkey='" + s34 + "+st_" + v_Tbl + "_gkey" + "+" + s34 + "'" + s34 + ");" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "        if (sel_rows.Length == 1) " + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "        {" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          mod_row = sel_rows[0];" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          if (" + s1310;
      v_Str_YNET_updateF2S = "";
      v_Str_YNET_updateF2B = "";
      v_Str_YNET_dim = "  string st_" + v_Tbl + "_gkey=" + s34 + s34 + "," + " st_" + v_Tbl + "_mkey=" + s34 + s34 + ",";
      //
      v_Str_YNET_code = "protected void gr_GridView_" + v_Tbl + "_RowDataBound(object sender, GridViewRowEventArgs e)" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "{" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "  string st_datavalue = " + s34 + s34 + "; " + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "  if (e.Row.RowIndex >= 0) " + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "  {" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "    DataRowView rowView = (DataRowView)e.Row.DataItem;" + s1310;
      //
      v_Str_YNET_grid = "<asp:GridView ID=" + s34 + "gr_GridView_" + v_Tbl + s34 + " runat=" + s34 + "server" + s34 + " AutoGenerateColumns=" + s34 + "False" + s34 + " DataKeyNames=" + s34 + v_Tbl + "_gkey" + s34 + " EnableModelValidation=" + s34 + "True" + s34 + " AllowPaging=" + s34 + "false" + s34 + " OnRowDataBound=" + s34 + "gr_GridView_" + v_Tbl + "_RowDataBound" + s34 + ">" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "  <Columns>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%# DD2012.PublicVariable.st_choose %></b>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:ImageButton CommandName=" + s34 + "Select" + s34 + " ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem," + s34 + v_Tbl + "_gkey" + s34 + ").ToString() ? " + s34 + "~\\images\\GridCheck.gif" + s34 + ":" + s34 + "~\\images\\GridUnCheck.gif" + s34 + " %>'" + " runat=" + s34 + "server" + s34 + " ID=" + s34 + "Imagebutton1" + s34 + " />" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <input id=" + s34 + "tx_" + v_Tbl + "_gkey02" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "tx_" + v_Tbl + "_gkey02" + s34 + " value='<%# DataBinder.Eval(Container.DataItem," + s34 + v_Tbl + "_gkey" + s34 + ").ToString() %>' runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "        <input id=" + s34 + "tx_" + v_Tbl + "_mkey02" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "tx_" + v_Tbl + "_mkey02" + s34 + " value='<%# DataBinder.Eval(Container.DataItem," + s34 + v_Tbl + "_mkey" + s34 + ").ToString() %>' runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
      for (int in_rec = 0; in_rec < tb_DBSET.Rows.Count; in_rec++)
      {
        dr_ROW = tb_DBSET.Rows[in_rec];
        if (DAC.GetStringValue(dr_ROW["DBGRD"]) != "0")
        {
          if (DAC.GetStringValue(dr_ROW["DBJIA"]) == "A")
          {
            j_Tbl = v_Tbl + "_";
          }
          else
          {
            j_Tbl = DAC.GetStringValue(dr_ROW["DBJIN"]);
            j_Tbl = "";
          }
          //
          if (v_Str_YNET_updateF2S == "")
          {
            v_Str_YNET_updateF2S = v_Str_YNET_updateF2S + "               (DAC.GetStringValue(mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]) != st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ") " + s1310;
            v_Str_YNET_dim = v_Str_YNET_dim + "st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "=" + s34 + s34;
          }
          else
          {
            v_Str_YNET_updateF2S = v_Str_YNET_updateF2S + "            || (DAC.GetStringValue(mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]) != st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ") " + s1310;
            v_Str_YNET_dim = v_Str_YNET_dim + ",st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "=" + s34 + s34;
          }
          //
          if (v_Str_YNET_updateF2B == "")
          {
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "           " + v_Tbl + "Dao.Insertbalog(conn, thistran, " + s34 + v_Tbl + s34 + ", st_ActKey, st_" + v_Tbl + "_gkey);" + s1310;
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "           " + v_Tbl + "Dao.Insertbtlog(conn, thistran, " + s34 + v_Tbl + s34 + ", DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]), " + s34 + "M" + s34 + ",UserGkey, DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]) + " + s34 + " " + s34 + " + DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]) +" + s34 + " " + s34 + " + DAC.GetStringValue(mod_row[" + s34 + j_Tbl + "gkey" + s34 + "]));" + s1310;
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row.BeginEdit();" + s1310;
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "] = st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ";      //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          }
          else
          {
            v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "] = st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ";      //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          }
          //
          if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "DROPDOWNLIST")
          {
            v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:TextBox visible=" + s34 + "false" + s34 + "  runat=" + s34 + "server" + s34 + " ID=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + "/>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:dropdownlist Width=" + s34 + "70px" + s34 + "  runat=" + s34 + "server" + s34 + " ID=" + s34 + "dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + "/>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
            //
            v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      DropDownList dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as DropDownList;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      TextBox tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as TextBox;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = sFN.DropDownListFromTable(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02," + s34 + DAC.GetStringValue(dr_ROW["DBJIN"]) + s34 + ", " + s34 + DAC.GetStringValue(dr_ROW["DBJIK"]) + s34 + ", " + s34 + DAC.GetStringValue(dr_ROW["DBJIF"]) + s34 + ", " + s34 + s34 + ", " + s34 + DAC.GetStringValue(dr_ROW["DBJIF"]) + s34 + ");" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      st_datavalue = DAC.GetStringValue(rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]).Trim();" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = sFN.SetDropDownList(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, st_datavalue);" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {clsGV.Drpdown_Set(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, true);} else {clsGV.Drpdown_Set(ref dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, false);}" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
            //
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + " + s34 + "$dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " ;" + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + " = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "CHECKBOX")
          {
            v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:checkbox width=" + s34 + "70px" + s34 + " runat=" + s34 + "server" + s34 + " id=" + s34 + "ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + "/>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
            v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
            //
            v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      CheckBox ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as CheckBox;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      st_datavalue = DAC.GetStringValue(rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]).Trim();" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      if (st_datavalue == " + s34 + "1" + s34 + ") ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Checked = true; else ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Checked = false;" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Enabled = true;} else {ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Enabled = false;}" + s1310;
            v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
            //
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) +" + s34 + "$ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
            v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + " = ((CheckBox)FindControl(st_ctrlname)).Checked ? " + s34 + "1" + s34 + " : " + s34 + "0" + s34 + " ;" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "TEXTBOX")
          {
            if (DAC.GetStringValue(dr_ROW["DBFLD"]).ToUpper().IndexOf("GKEY") >= 0 || DAC.GetStringValue(dr_ROW["DBFLD"]).ToUpper().IndexOf("MKEY") >= 0)
            {
              v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <input id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " value='<%# DataBinder.Eval(Container.DataItem," + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + ").ToString() %>' runat=" + s34 + "server" + s34 + "/> " + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
              //
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl +DAC.GetGridViewRowId(in_g)+ " + s34 + "$tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "= ((TextBox)FindControl(st_ctrlname)).Text.Trim();" + s1310;
            }
            else if (DAC.GetStringValue(dr_ROW["DBUHO"]).ToUpper() == "DATETIME")
            {
              v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:TextBox id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " runat=" + s34 + "server" + s34 + " MaxLength=" + s34 + DAC.GetStringValue(dr_ROW["DBLEN"]) + s34 + " Width=" + s34 + "80px" + s34 + " TEXT='<%# sFN.DateToDateString(Convert.ToDateTime(DataBinder.Eval(Container.DataItem," + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + ")),sys_DateFormat) %>'></asp:TextBox>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
              //
              v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      TextBox tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as TextBox;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      if (rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]==DBNull.Value)" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      {" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "        st_datavalue=" + s34 + s34 + ";" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      }" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      else" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      { " + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "        st_datavalue=sFN.DateToDateString(  DAC.GetDateTimeValue( rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]),sys_DateFormat);" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      }" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Text = st_datavalue;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, true);} else {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, false);}" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
              //
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + " + s34 + "$tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "= ((TextBox)FindControl(st_ctrlname)).Text.Trim();" + s1310;
            }
            else
            {
              v_Str_YNET_grid = v_Str_YNET_grid + "    <asp:TemplateField>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <b><%#" + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + ".Text%></b>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </HeaderTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      <ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "        <asp:TextBox id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + " runat=" + s34 + "server" + s34 + " MaxLength=" + s34 + DAC.GetStringValue(dr_ROW["DBLEN"]) + s34 + " Width=" + s34 + "60px" + s34 + " TEXT='<%# DataBinder.Eval(Container.DataItem," + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + ").ToString() %>'></asp:TextBox>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "      </ItemTemplate>" + s1310;
              v_Str_YNET_grid = v_Str_YNET_grid + "    </asp:TemplateField>" + s1310;
              //
              v_Str_YNET_code = v_Str_YNET_code + "    //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    if (e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") != null)" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    {" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      TextBox tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02 = e.Row.FindControl(" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ") as TextBox;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      st_datavalue = DAC.GetStringValue(rowView[" + s34 + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + "]).Trim();" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02.Text = st_datavalue;" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "      if (hh_GridCtrl.Value == " + s34 + "modall" + s34 + ") {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, true);} else {clsGV.TextBox_Set(ref tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02, false);}" + s1310;
              v_Str_YNET_code = v_Str_YNET_code + "    }" + s1310;
              //
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + " + s34 + "$tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "02" + s34 + ";" + s1310;
              v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        st_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "= ((TextBox)FindControl(st_ctrlname)).Text.Trim();" + s1310;
            }
          }
        }
      }
      v_Str_YNET_grid = v_Str_YNET_grid + "  </Columns>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "</asp:GridView>" + s1310;
      v_Str_YNET_grid = v_Str_YNET_grid + "<asp:Literal ID=" + s34 + "li_Msg" + s34 + " runat=" + s34 + "server" + s34 + "></asp:Literal>" + s1310;
      //
      v_Str_YNET_code = v_Str_YNET_code + "  }" + s1310;
      v_Str_YNET_code = v_Str_YNET_code + "}" + s1310;
      //
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        //" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        bl_Mod = true;" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      }" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      else" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      {" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "        bl_Mod = false;" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      }" + s1310;
      v_Str_YNET_updateF1 = v_Str_YNET_updateF1 + "      //" + s1310;
      //
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + v_Str_YNET_updateF2S;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          )" + s1310;
      v_Str_YNET_updateF2 = v_Str_YNET_updateF2 + "          {" + s1310;
      //
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            mod_row.EndEdit();" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "            st_ActKey=DAC.get_guidkey();  //" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "          }" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "        }  //sel_rows.Length == 1" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "      }  //bl_Mod" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    }  //for" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    da_ADP.Update(tb_" + v_Tbl + ");" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    thistran.Commit();" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "    bl_updateok=true;" + s1310;
      v_Str_YNET_updateF2B = v_Str_YNET_updateF2B + "  }  //try" + s1310;
      //
      v_Str_YNET_updateL = "" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  catch (Exception e)" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  {" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    thistran.Rollback();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    bl_updateok = false;" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    st_errmsg += e.Message;" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  }" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  finally" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  {" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    thistran.Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    " + v_Tbl + "Dao.Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    tb_" + v_Tbl + ".Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "    da_ADP.Dispose();" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  }" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "  return bl_updateok;" + s1310;
      v_Str_YNET_updateL = v_Str_YNET_updateL + "}" + s1310;
      //
      Fhdsw.Write(v_Str_YNET_grid + s1310 + s1310 + v_Str_YNET_code + s1310 + s1310 + v_Str_YNET_update0 + v_Str_YNET_dim + ";" + s1310 + v_Str_YNET_update + v_Str_YNET_updateF1 + v_Str_YNET_updateF2 + v_Str_YNET_updateF2B + v_Str_YNET_updateL);
      Fhdsw.Close();
      Fhdsw.Dispose();
      dbset_DAO.Dispose();
      cmd.Dispose();
      tb_DBSET.Dispose();
      //

      Session["fm_httpx_HyFile1_Text"] = "GridView Update Source & HTML:" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      Session["fm_httpx_HyFile1_url"] = v_doc_http;

    }

    protected void bt_14_Click(object sender, EventArgs e)
    {
      Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
      Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
      Session["fmDBSET_gr_GridView_DBSET_GridGkey"] = gr_GridView_DBSET.DataKeys[gr_GridView_DBSET.SelectedIndex].Value.ToString();
      //
      Session["fm_httpx_exit"] = "~\\forms\\dax\\dx_dbset.aspx";
      Session["fm_httpx_HyFile1_Text"] = "";
      Session["fm_httpx_HyFile1_url"] = "";
      Session["fm_httpx_HyFile2_Text"] = "";
      Session["fm_httpx_HyFile2_url"] = "";
      //
      act_SOURCE();
      //
      Response.Redirect("~\\forms\\Download\\fm_httpx.aspx");
    }

    protected void act_SOURCE()
    {
      StreamWriter Fhdsw;
      DAC dbset_DAO = new DAC(conn);
      OleDbCommand cmd = new OleDbCommand();
      DataTable tb_DBSET = new DataTable();
      DataRow dr_ROW;
      //
      string v_Til, v_Tbl, v_FormID, vPkey;
      int in_rec;

      tb_DBSET.Clear();
      cmd.CommandText = "SELECT * FROM DASET WHERE DAVER=? AND DAREN=? AND DAAPX=? AND DANUM=? ";
      DAC.AddParam(cmd, "DAVER", tx_DBSET_DBVER.Text);
      DAC.AddParam(cmd, "DAREN", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]));
      DAC.AddParam(cmd, "DAAPX", tx_DBSET_DBAPX.Text);
      DAC.AddParam(cmd, "DANUM", tx_DBSET_DBNUM.Text);
      tb_DBSET = dbset_DAO.Select(cmd);
      dr_ROW = tb_DBSET.Rows[0];
      v_Til = DAC.GetStringValue(dr_ROW["DANAM"]);
      v_Tbl = DAC.GetStringValue(dr_ROW["DATBL"]);
      v_FormID = tx_DBSET_DBAPX.Text;
      vPkey = "DBREK";
      //
      string v_doc_file = "";
      string v_doc_http = "";
      string v_doc_guid = sFN.Get_guid5();
      //
      v_doc_file = sys_DocFilePath + tx_DBSET_DBAPX.Text + "_source_" + v_doc_guid + ".txt";
      v_doc_http = tx_DBSET_DBAPX.Text + "_source_" + v_doc_guid + ".txt";
      //
      tb_DBSET.Clear();
      Fhdsw = File.CreateText(v_doc_file);
      cmd.CommandText = "SELECT * FROM DBSET WHERE DBVER=? AND DBREN=? AND DBAPX=? AND DBNUM=? AND DBTBL=? ORDER BY DBROW,DBCOL ";
      DAC.AddParam(cmd, "DBVER", tx_DBSET_DBVER.Text);
      DAC.AddParam(cmd, "DBREN", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]));
      DAC.AddParam(cmd, "DBAPX", tx_DBSET_DBAPX.Text);
      DAC.AddParam(cmd, "DBNUM", tx_DBSET_DBNUM.Text);
      DAC.AddParam(cmd, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
      tb_DBSET = dbset_DAO.Select(cmd);
      //
      for (in_rec = 0; in_rec < tb_DBSET.Rows.Count; in_rec++)
      {
        dr_ROW = tb_DBSET.Rows[in_rec];
        if (DAC.GetStringValue(dr_ROW["DBPRY"]) == "1")
        {
          vPkey = DAC.GetStringValue(dr_ROW["DBFLD"]);
        }
      }
      //程式名稱
      string str_loadDropDownList, str_ClearText, str_SetSer_altf, str_SetSer, str_SetSer_alta;
      string str_SetEdt, str_SetEdt_alta, str_BindText, str_ins_row, str_mod_row, str_server_edit;
      string sname, fname;
      str_SetSer_alta = "";
      str_loadDropDownList = "";
      str_ClearText = "";
      str_SetSer_altf = "";
      str_SetSer = "";
      str_SetEdt = "";
      str_SetEdt_alta = "";
      str_BindText = "";
      str_ins_row = "";
      str_mod_row = "";
      str_server_edit = "";
      for (in_rec = 0; in_rec < tb_DBSET.Rows.Count; in_rec++)
      {
        dr_ROW = tb_DBSET.Rows[in_rec];
        //
        if (DAC.GetStringValue(dr_ROW["DBJIA"]).ToUpper() == "A")
        {
          fname = v_Tbl + "_" + DAC.GetStringValue(dr_ROW["DBFLD"]);
        }
        else
        {
          fname = DAC.GetStringValue(dr_ROW["DBFLD"]);
        }
        //
        if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "DropDownList".ToUpper())
        {
          sname = "dr_" + fname;
          str_loadDropDownList = str_loadDropDownList + "        " + sname + "= sFN.DropDownListFromTable(ref " + sname + ", " + s34 + DAC.GetStringValue(dr_ROW["DBUHO"]) + s34 + ", " + s34 + "key" + s34 + ", " + s34 + "name" + s34 + ", " + s34 + s34 + ", " + s34 + "order_key" + s34 + ");" + s1310;
          str_ClearText = str_ClearText + "      " + sname + ".SelectedIndex = -1;" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_SetSer = str_SetSer + "      " + "clsGV.Drpdown_Set(ref dr_" + fname + ", ref tx_" + fname + ", false); " + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_BindText = str_BindText + "      " + sname + " = sFN.SetDropDownList(ref " + sname + ", DAC.GetStringValue(CurRow[" + s34 + fname + s34 + "]));" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_SetEdt = str_SetEdt + "      " + "clsGV.Drpdown_Set(ref dr_" + fname + ", ref tx_" + fname + ", true); " + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          //
          if (DAC.GetStringValue(dr_ROW["DBJIA"]).ToUpper() == "A")
          {
            if (DAC.GetStringValue(dr_ROW["DBINS"]) == "1")
            {
              str_ins_row = str_ins_row + "                ins_row[" + s34 + fname + s34 + "] =" + "dr_" + fname + ".SelectedValue;       // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }
            if (DAC.GetStringValue(dr_ROW["DBMOD"]) == "1")
            {
              str_mod_row = str_mod_row + "                  mod_row[" + s34 + fname + s34 + "] =" + "dr_" + fname + ".SelectedValue;       // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }
          }
        }
        //
        if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "TEXTBOX".ToUpper())
        {
          sname = "tx_" + fname;
          if (DAC.GetStringValue(dr_ROW["DBDEF"]).ToUpper().IndexOf("ALTER") >= 0)
          {
            str_SetSer_altf = str_SetSer_altf + "      " + "clsGV.SetTextBoxEditAlert(ref lb_" + fname + ", ref tx_" + fname + ", false);" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            str_SetSer_alta = str_SetSer_alta + "      " + "clsGV.SetControlShowAlert(ref lb_" + fname + ", ref tx_" + fname + ", true);  // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            str_SetEdt_alta = str_SetEdt_alta + "      " + "clsGV.SetTextBoxEditAlert(ref lb_" + fname + ", ref tx_" + fname + ", true);  // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          }
          str_SetSer = str_SetSer + "      " + "clsGV.TextBox_Set(ref tx_" + fname + ", false); " + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_SetEdt = str_SetEdt + "      " + "clsGV.TextBox_Set(ref tx_" + fname + ", true);" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_ClearText = str_ClearText + "      " + sname + ".Text =" + s34 + s34 + ";" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          //
          if (DAC.GetStringValue(dr_ROW["DBUHO"]).ToUpper() == "DATETIME")
          {
            //str_BindText = str_BindText + "      " + sname + ".Text = DAC.GetStringValue(CurRow[" + s34 + fname + s34 + "]);" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA").ToString + s1310;
            str_BindText = str_BindText + "      if (CurRow[" + s34 + fname + s34 + "] == DBNull.Value) {tx_" + fname + ".Text = " + s34 + s34 + ";} else {tx_" + fname + ".Text = DAC.GetDateTimeValue(CurRow[" + s34 + fname + s34 + "]).ToString(sys_DateFormat);}  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          }
          else
          {
            str_BindText = str_BindText + "      " + sname + ".Text = DAC.GetStringValue(CurRow[" + s34 + fname + s34 + "]);" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          }
          //
          if (DAC.GetStringValue(dr_ROW["DBJIA"]).ToUpper() == "A")
          {
            if (DAC.GetStringValue(dr_ROW["DBINS"]) == "1")
            {
              if (DAC.GetStringValue(dr_ROW["DBUHO"]).ToUpper() == "DATETIME")
              {
                str_ins_row = str_ins_row + "                if (tx_" + fname + ".Text.Trim() == " + s34 + s34 + ") { ins_row[" + s34 + fname + s34 + "] =DBNull.Value;} else {ins_row[" + s34 + fname + s34 + "] = sFN.DateStringToDateTime(tx_" + fname + ".Text);}       //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              }
              else
              {
                str_ins_row = str_ins_row + "                ins_row[" + s34 + fname + s34 + "] =" + "tx_" + fname + ".Text.Trim()" + ";       // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              }
            }

            if (DAC.GetStringValue(dr_ROW["DBMOD"]) == "1")
            {
              if (DAC.GetStringValue(dr_ROW["DBUHO"]).ToUpper() == "DATETIME")
              {
                str_mod_row = str_mod_row + "                  if (tx_" + fname + ".Text.Trim() == " + s34 + s34 + ") { mod_row[" + s34 + fname + s34 + "] =DBNull.Value;} else {mod_row[" + s34 + fname + s34 + "] = sFN.DateStringToDateTime(tx_" + fname + ".Text);}       //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              }
              else
              {
                str_mod_row = str_mod_row + "                  mod_row[" + s34 + fname + s34 + "] =" + "tx_" + fname + ".Text.Trim()" + ";       // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
              }
            }

            if (DAC.GetStringValue(dr_ROW["DBTYP"]) == "INT")
            {
              str_server_edit = str_server_edit + "      ret = DataCheck.cIsStrIntChk(ret, tx_" + fname + ".Text, lb_" + fname + ".Text, ref sMsg); //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }
            else if (DAC.GetStringValue(dr_ROW["DBTYP"]) == "MONEY")
            {
              str_server_edit = str_server_edit + "      ret = DataCheck.cIsStrDecimalChk(ret, tx_" + fname + ".Text, lb_" + fname + ".Text, ref sMsg); //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }
            else if (DAC.GetStringValue(dr_ROW["DBTYP"]) == "DATETIME")
            {
              str_server_edit = str_server_edit + "      ret = DataCheck.cIsStrDatetimeChk(ret, tx_" + fname + ".Text, lb_" + fname + ".Text, ref sMsg); //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }
            else if (DAC.GetStringValue(dr_ROW["DBEMP"]) == "2")
            {
              str_server_edit = str_server_edit + "      ret = DataCheck.cIsStrEmptyChk(ret, tx_" + fname + ".Text, lb_" + fname + ".Text, ref sMsg);  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }

          }
        }
        //
        if (DAC.GetStringValue(dr_ROW["DBUCO"]).ToUpper() == "CheckBox".ToUpper())
        {
          sname = "ck_" + fname;
          str_ClearText = str_ClearText + "      " + sname + ".Checked = false;" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_SetSer = str_SetSer + "      " + sname + ".Enabled = false;" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_SetEdt = str_SetEdt + "      " + sname + ".Enabled = true;" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          str_BindText = str_BindText + "      " + sname + ".Checked = DAC.GetBooleanValueString(DAC.GetStringValue(CurRow[" + s34 + fname + s34 + "]));" + "  //" + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
          //
          if (DAC.GetStringValue(dr_ROW["DBJIA"]).ToUpper() == "A")
          {
            if (DAC.GetStringValue(dr_ROW["DBINS"]) == "1")
            {
              str_ins_row = str_ins_row + "                ins_row[" + s34 + fname + s34 + "] =" + sname + ".Checked ? " + s34 + "1" + s34 + " : " + s34 + "0" + s34 + ";       // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }
            if (DAC.GetStringValue(dr_ROW["DBMOD"]) == "1")
            {
              str_mod_row = str_mod_row + "                  mod_row[" + s34 + fname + s34 + "] =" + sname + ".Checked ? " + s34 + "1" + s34 + " : " + s34 + "0" + s34 + ";       // " + DAC.GetStringValue(dr_ROW["DBTNA"]) + s1310;
            }
          }
        }
      }
      //
      //Import程式
      string v_Str, v_Apx;
      v_Apx = tx_DBSET_DBAPX.Text;
      v_Str = "";
      v_Str = v_Str + "using System;" + s1310;
      v_Str = v_Str + "using System.Collections.Generic;" + s1310;
      v_Str = v_Str + "using System.Linq;" + s1310;
      v_Str = v_Str + "using System.Web;" + s1310;
      v_Str = v_Str + "using System.Web.UI;" + s1310;
      v_Str = v_Str + "using System.Web.UI.HtmlControls;" + s1310;
      v_Str = v_Str + "using System.Web.UI.WebControls;" + s1310;
      v_Str = v_Str + "using System.Data;" + s1310;
      v_Str = v_Str + "using System.Data.OleDb;" + s1310;
      v_Str = v_Str + "using System.Data.Common;" + s1310;
      v_Str = v_Str + "using YNet;" + s1310;
      v_Str = v_Str + s1310;
      v_Str = v_Str + "    string st_object_func = " + s34 + v_Apx + s34 + ";" + s1310;
      v_Str = v_Str + "    string st_ContentPlaceHolder=" + s34 + "ctl00$ContentPlaceHolder1$" + s34 + ";" + s1310;
      v_Str = v_Str + "    //" + s1310;
      v_Str = v_Str + "    string st_dd_apx = " + s34 + v_Apx + s34 + ";         //UNdcnews   與apx 相關" + s1310;
      v_Str = v_Str + "    string st_dd_table = " + s34 + v_Tbl + s34 + ";       //dcnews     與table 相關 " + s1310;
      v_Str = v_Str + "    string st_ren_head = " + s34 + s34 + ";       //DC         與單號相關 " + s1310;
      v_Str = v_Str + "    string st_ren_yymmtext = " + s34 + s34 + ";   //           與單號相關 " + s1310;
      v_Str = v_Str + "    string st_ren_cls = " + s34 + s34 + ";        //ren        與單號cls相關 " + s1310;
      v_Str = v_Str + "    string st_ren_cos = " + s34 + s34 + ";        //1          與單號cos相關 " + s1310;
      v_Str = v_Str + "    int in_ren_len = 0;            //6          與單號流水號 " + s1310;
      v_Str = v_Str + "    //" + s1310;
      v_Str = v_Str + "    protected void Page_Load(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      li_Msg.Text = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      li_AccMsg.Text = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      //檢查權限&狀態" + s1310;
      v_Str = v_Str + "      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        if (!IsPostBack)" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "         " + s1310 + str_loadDropDownList + s1310;
      v_Str = v_Str + "          CmdQueryS.CommandText=" + s34 + " AND 1=1 " + s34 + ";" + s1310;
      v_Str = v_Str + "          Session[" + s34 + "fm" + v_Tbl + "_CmdQueryS" + s34 + "] = CmdQueryS;" + s1310;
      v_Str = v_Str + "          Set_Control();" + s1310;
      v_Str = v_Str + "          if (DAC.GetStringValue(Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_GridGkey" + s34 + "]) != " + s34 + s34 + ")" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            gr_GridView_" + v_Tbl + ".PageIndex=DAC.GetInt16Value(Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_PageIndex" + s34 + "]);" + s1310;
      v_Str = v_Str + "            gr_GridView_" + v_Tbl + ".SelectedIndex=DAC.GetInt16Value(Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_SelectedIndex" + s34 + "]);" + s1310;
      v_Str = v_Str + "            hh_GridGkey.Value = DAC.GetStringValue(Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_GridGkey" + s34 + "]);" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "          //" + s1310;
      v_Str = v_Str + "          BindNew(true);" + s1310;
      v_Str = v_Str + "          SetSerMod();" + s1310;
      v_Str = v_Str + "          Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_PageIndex" + s34 + "] = gr_GridView_" + v_Tbl + ".PageIndex;" + s1310;
      v_Str = v_Str + "          Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_SelectedIndex" + s34 + "] = gr_GridView_" + v_Tbl + ".SelectedIndex;" + s1310;
      v_Str = v_Str + "        }" + s1310;
      v_Str = v_Str + "        else" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          if ((hh_GridCtrl.Value.ToString().ToLower() == " + s34 + "ins" + s34 + ") || (hh_GridCtrl.Value.ToString().ToLower() == " + s34 + "mod" + s34 + "))" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            BindNew(false);" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "          else" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            BindNew(true);" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "        }" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + s1310;
      //
      v_Str = v_Str + "    private void Set_Control()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + ".PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, " + s34 + "pagesize" + s34 + "));  //GridView 取pagesize" + s1310;
      v_Str = v_Str + "      this.Page.Title = FunctionName;" + s1310;
      v_Str = v_Str + "      sFN.SetFormLables(this, PublicVariable.LangType,st_ContentPlaceHolder, ApVer," + s34 + v_Apx + s34 + "," + s34 + v_Tbl + s34 + ");" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + s1310;
      //
      v_Str = v_Str + "    private void ClearText()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "     //" + s1310 + str_ClearText + s1310;
      v_Str = v_Str + "     //" + s1310;
      v_Str = v_Str + "      hh_mkey.Value = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    private void SetSerMod()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      //" + s1310 + str_SetSer_altf;
      v_Str = v_Str + "      //" + s1310 + str_SetSer;
      v_Str = v_Str + "      //" + s1310 + str_SetSer_alta;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder , " + s34 + "ser" + s34 + ");" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_SAV" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_CAN" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_QUT" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + ".Enabled = true;" + s1310;
      v_Str = v_Str + "      //gr_GridView_" + v_Tbl + ".Columns[0].Visible=true;" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    private void SetEditMod()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      // " + s1310 + str_SetEdt;
      v_Str = v_Str + "      // " + s1310 + str_SetEdt_alta;
      v_Str = v_Str + "      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, " + s34 + "mod" + s34 + ");" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_SAV" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_CAN" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_QUT" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + ".Enabled = false;" + s1310;
      v_Str = v_Str + "      //gr_GridView_" + v_Tbl + ".Columns[0].Visible = false;" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    private void SetEditModAll()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      // " + s1310 + str_SetEdt;
      v_Str = v_Str + "      // " + s1310 + str_SetEdt_alta;
      v_Str = v_Str + "      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, " + s34 + "mod" + s34 + ");" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_SAV" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_CAN" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);" + s1310;
      v_Str = v_Str + "      sFN.SetLinkButton(this, " + s34 + "bt_QUT" + s34 + ", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + ".Enabled = true;" + s1310;
      v_Str = v_Str + "      //gr_GridView_" + v_Tbl + ".Columns[0].Visible = false;" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    private void BindText(DataRow CurRow)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      //" + s1310 + str_BindText;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      hh_mkey.Value = DAC.GetStringValue(CurRow[" + s34 + v_Tbl + "_" + "mkey" + s34 + "]);" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    private void BindNew(bool bl_showdata)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      string SelDataKey = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      DataRow[] SelDataRow;" + s1310;
      v_Str = v_Str + "      DataRow CurRow;" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      try" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        CmdQueryS = (OleDbCommand)Session[" + s34 + "fm" + v_Tbl + "_CmdQueryS" + s34 + "];" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "      catch" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        CmdQueryS.CommandText = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      DataTable tb_" + v_Tbl + " = new DataTable();" + s1310;
      v_Str = v_Str + "      DAC_" + v_Tbl + " " + v_Tbl + "Dao = new DAC_" + v_Tbl + "(conn);" + s1310;

      v_Str = v_Str + "      OleDbDataAdapter ad_DataDataAdapter;" + s1310;
      v_Str = v_Str + "      string st_addselect = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      string st_addjoin = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      string st_addunion = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      ad_DataDataAdapter = " + v_Tbl + "Dao.GetDataAdapter(ApVer, " + s34 + "UN" + v_Tbl + s34 + ", " + s34 + v_Tbl + s34 + ", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " + s34 + "order by xxx " + s34 + ");" + s1310;
      v_Str = v_Str + "      ad_DataDataAdapter.Fill(tb_" + v_Tbl + ");" + s1310;

      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      if (tb_" + v_Tbl + ".Rows.Count > 0)" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        bt_05.OnClientClick =" + s34 + "return btnDEL_c()" + s34 + ";" + s1310;
      v_Str = v_Str + "        bt_04.OnClientClick = " + s34 + "return btnMOD_c()" + s34 + ";" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "      else" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        bt_05.OnClientClick = " + s34 + "return btnDEL0_c()" + s34 + ";" + s1310;
      v_Str = v_Str + "        bt_04.OnClientClick = " + s34 + "return btnMOD0_c()" + s34 + ";" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + ".DataSource = tb_" + v_Tbl + ";" + s1310;
      v_Str = v_Str + "      //fmsn101_GV1_SelectedIndex" + s1310;
      v_Str = v_Str + "      //fmsn101_GV1_PageIndex" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + " = clsGV.BindGridView(gr_GridView_" + v_Tbl + ", tb_" + v_Tbl + ", hh_GridCtrl, ref hh_GridGkey," + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + s34 + ");" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + ".DataBind();" + s1310;
      v_Str = v_Str + "      SelDataKey = " + s34 + v_Tbl + "_" + "gkey='" + s34 + "+hh_GridGkey.Value+" + s34 + "'" + s34 + ";" + s1310;
      v_Str = v_Str + "      SelDataRow = tb_" + v_Tbl + ".Select(SelDataKey);" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      if (bl_showdata)" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        if (SelDataRow.Length == 1)" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          CurRow = SelDataRow[0];" + s1310;
      v_Str = v_Str + "          Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_GridGkey" + s34 + "]=hh_GridGkey.Value;" + s1310;
      v_Str = v_Str + "          BindText(CurRow);" + s1310;
      v_Str = v_Str + "        }" + s1310;
      v_Str = v_Str + "        else" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          hh_GridCtrl.Value = " + s34 + "init" + s34 + ";" + s1310;
      v_Str = v_Str + "          ClearText();" + s1310;
      v_Str = v_Str + "        }" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "      tb_" + v_Tbl + ".Dispose();" + s1310;
      v_Str = v_Str + "      " + v_Tbl + "Dao.Dispose();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void gr_GridView_" + v_Tbl + "_PageIndexChanging(object sender, GridViewPageEventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      gr_GridView_" + v_Tbl + ".PageIndex = e.NewPageIndex;" + s1310;
      v_Str = v_Str + "      Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_PageIndex" + s34 + "] = gr_GridView_" + v_Tbl + ".PageIndex;" + s1310;
      v_Str = v_Str + "      hh_GridGkey.Value = gr_GridView_" + v_Tbl + ".DataKeys[gr_GridView_" + v_Tbl + ".SelectedIndex].Value.ToString();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void gr_GridView_" + v_Tbl + "_PageIndexChanged(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "     if (" + "gr_GridView_" + v_Tbl + ".Enabled)" + s1310;
      v_Str = v_Str + "     {" + s1310;
      v_Str = v_Str + "       hh_GridCtrl.Value = " + s34 + "ser" + s34 + ";" + s1310;
      v_Str = v_Str + "       BindNew(true);" + s1310;
      v_Str = v_Str + "       SetSerMod();" + s1310;
      v_Str = v_Str + "       Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_PageIndex" + s34 + "] = gr_GridView_" + v_Tbl + ".PageIndex;" + s1310;
      v_Str = v_Str + "       Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_SelectedIndex" + s34 + "] = gr_GridView_" + v_Tbl + ".SelectedIndex;" + s1310;
      v_Str = v_Str + "     }" + s1310;
      v_Str = v_Str + "     else" + s1310;
      v_Str = v_Str + "     {" + s1310;
      v_Str = v_Str + "       li_Msg.Text = " + s34 + "<script> alert('" + s34 + " + StringTable.GetString(" + s34 + "請先處理資料輸入" + s34 + ") + " + s34 + "'); </script>" + s34 + ";" + s1310;
      v_Str = v_Str + "     }" + s1310;
      v_Str = v_Str + "   }" + s1310;
      v_Str = v_Str + "   " + s1310;
      //
      v_Str = v_Str + "    protected void gr_GridView_" + v_Tbl + "_SelectedIndexChanged(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      BindNew(true);" + s1310;
      v_Str = v_Str + "      Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_PageIndex" + s34 + "] = gr_GridView_" + v_Tbl + ".PageIndex;" + s1310;
      v_Str = v_Str + "      Session[" + s34 + "fm" + v_Tbl + "_gr_GridView_" + v_Tbl + "_SelectedIndex" + s34 + "] = gr_GridView_" + v_Tbl + ".SelectedIndex;" + s1310;
      v_Str = v_Str + "      hh_GridGkey.Value = gr_GridView_" + v_Tbl + ".DataKeys[gr_GridView_" + v_Tbl + ".SelectedIndex].Value.ToString();" + s1310;
      v_Str = v_Str + "      SetSerMod();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void bt_02_Click(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      actNEW();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void actNEW()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      if (sFN.checkAccessFunc(UserGkey, st_object_func, 2, UserLoginGkey, ref li_AccMsg))" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        hh_GridCtrl.Value = " + s34 + "ins" + s34 + ";" + s1310;
      v_Str = v_Str + "        Set_Control();" + s1310;
      v_Str = v_Str + "        ClearText();" + s1310;
      v_Str = v_Str + "        //定義guidkey" + s1310;
      v_Str = v_Str + "        hh_ActKey.Value = DAC.get_guidkey();" + s1310;
      v_Str = v_Str + "        BindNew(false);" + s1310;
      v_Str = v_Str + "        SetEditMod();" + s1310;
      v_Str = v_Str + "        li_Msg.Text = " + s34 + "<script> document.all('ContentPlaceHolder1_tx_xxxxxx').focus(); </script>" + s34 + ";" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void bt_04_Click(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      actMOD();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void actMOD()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        hh_GridCtrl.Value =" + s34 + "mod" + s34 + ";" + s1310;
      v_Str = v_Str + "        Set_Control();" + s1310;
      v_Str = v_Str + "        //取Act guidkey" + s1310;
      v_Str = v_Str + "        hh_ActKey.Value = DAC.get_guidkey();" + s1310;
      v_Str = v_Str + "        BindNew(true);" + s1310;
      v_Str = v_Str + "        SetEditMod();" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void bt_11_Click(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      actMODALL();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void actMODALL()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        hh_GridCtrl.Value = " + s34 + "modall" + s34 + ";" + s1310;
      v_Str = v_Str + "        Set_Control();" + s1310;
      v_Str = v_Str + "        //取Act guidkey" + s1310;
      v_Str = v_Str + "        hh_ActKey.Value = DAC.get_guidkey();" + s1310;
      v_Str = v_Str + "        BindNew(true);" + s1310;
      v_Str = v_Str + "        SetEditModAll();" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void bt_QUT_Click(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      Response.Redirect(" + s34 + "~/Master/" + s34 + "+ Page.Theme + " + s34 + "/MainForm.aspx" + s34 + ");" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void bt_CAN_Click(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      actCAN();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void actCAN()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      hh_GridCtrl.Value = " + s34 + "ser" + s34 + ";" + s1310;
      v_Str = v_Str + "      Set_Control();" + s1310;
      v_Str = v_Str + "      ClearText();" + s1310;
      v_Str = v_Str + "      BindNew(true);" + s1310;
      v_Str = v_Str + "      SetSerMod();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      //
      v_Str = v_Str + "    protected void bt_05_Click(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      actDEL();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void actDEL()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      if (sFN.checkAccessFunc(UserGkey, st_object_func, 5, UserLoginGkey, ref li_AccMsg))" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        bool bl_delok = false;" + s1310;
      v_Str = v_Str + "        Set_Control();" + s1310;
      v_Str = v_Str + "        hh_ActKey.Value = DAC.get_guidkey();" + s1310;
      v_Str = v_Str + "        //" + s1310;
      v_Str = v_Str + "        DAC_" + v_Tbl + " " + v_Tbl + "Dao = new DAC_" + v_Tbl + "(conn);" + s1310;
      v_Str = v_Str + "        string st_addselect = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "        string st_addjoin = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "        string st_addunion = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "        string st_SelDataKey = " + s34 + v_Tbl + "_gkey='" + s34 + " + hh_GridGkey.Value + " + s34 + "' and " + v_Tbl + "_mkey='" + s34 + "+hh_mkey.Value+" + s34 + "' " + s34 + ";" + s1310;
      v_Str = v_Str + "        DataTable tb_" + v_Tbl + " = new DataTable();" + s1310;
      v_Str = v_Str + "        DbDataAdapter da_ADP = " + v_Tbl + "Dao.GetDataAdapter(ApVer, " + s34 + "UN" + v_Tbl + s34 + ", " + s34 + v_Tbl + s34 + ", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " + s34 + s34 + ");" + s1310;
      v_Str = v_Str + "        da_ADP.Fill(tb_" + v_Tbl + ");" + s1310;
      v_Str = v_Str + "        DataRow[] DelRow = tb_" + v_Tbl + ".Select(st_SelDataKey);" + s1310;
      v_Str = v_Str + "        if (DelRow.Length == 1)" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          conn.Open();" + s1310;
      v_Str = v_Str + "          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);" + s1310;
      v_Str = v_Str + "          da_ADP.DeleteCommand.Transaction = thistran;" + s1310;
      v_Str = v_Str + "          try" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            " + v_Tbl + "Dao.Insertbalog(conn, thistran," + s34 + v_Tbl + s34 + ",hh_ActKey.Value, hh_GridGkey.Value);" + s1310;
      v_Str = v_Str + "            " + v_Tbl + "Dao.Insertbtlog(conn, thistran," + s34 + v_Tbl + s34 + ", DAC.GetStringValue(DelRow[0][" + s34 + v_Tbl + "_gkey" + s34 + "]), " + s34 + "D" + s34 + ", UserName, DAC.GetStringValue(DelRow[0][" + s34 + v_Tbl + "_gkey" + s34 + "]));" + s1310;
      v_Str = v_Str + "            DelRow[0].Delete();" + s1310;
      v_Str = v_Str + "            da_ADP.Update(tb_" + v_Tbl + ");" + s1310;
      v_Str = v_Str + "            thistran.Commit();" + s1310;
      v_Str = v_Str + "            bl_delok = true;" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "          catch (Exception e)" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            thistran.Rollback();" + s1310;
      v_Str = v_Str + "            bl_delok = false;" + s1310;
      v_Str = v_Str + "            lb_ErrorMessage.Visible = true;" + s1310;
      v_Str = v_Str + "            lb_ErrorMessage.Text = e.Message;" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "          finally" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            thistran.Dispose();" + s1310;
      v_Str = v_Str + "            " + v_Tbl + "Dao.Dispose();" + s1310;
      v_Str = v_Str + "            tb_" + v_Tbl + ".Dispose();" + s1310;
      v_Str = v_Str + "            da_ADP.Dispose();" + s1310;
      v_Str = v_Str + "            conn.Close();" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "        }" + s1310;
      v_Str = v_Str + "        else" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          bl_delok = false;" + s1310;
      v_Str = v_Str + "          lb_ErrorMessage.Visible = true;" + s1310;
      v_Str = v_Str + "          lb_ErrorMessage.Text = StringTable.GetString(" + s34 + "資料已變更,請重新選取!" + s34 + ");" + s1310;
      v_Str = v_Str + "        }" + s1310;
      v_Str = v_Str + "        tb_" + v_Tbl + ".Clear();" + s1310;
      v_Str = v_Str + "        //" + s1310;
      v_Str = v_Str + "        if (bl_delok)" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          gr_GridView_" + v_Tbl + " = clsGV.SetGridCursor(" + s34 + "del" + s34 + ", gr_GridView_" + v_Tbl + ", -2);" + s1310;
      v_Str = v_Str + "          BindNew(true);" + s1310;
      v_Str = v_Str + "          SetSerMod();" + s1310;
      v_Str = v_Str + "        }" + s1310;
      v_Str = v_Str + "        //bl_delok" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void bt_SAV_Click(object sender, EventArgs e)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      actSAV();" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    protected void actSAV()" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      string st_ckerrmsg = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      string st_dberrmsg = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      string st_tempgkey = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      bool bl_insok = false, bl_updateok = false;" + s1310;
      v_Str = v_Str + "      //" + s1310;
      v_Str = v_Str + "      Set_Control();" + s1310;
      v_Str = v_Str + "      if (ServerEditCheck(ref st_ckerrmsg))" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        DAC_" + v_Tbl + " " + v_Tbl + "Dao = new DAC_" + v_Tbl + "(conn);" + s1310;
      v_Str = v_Str + "        if (hh_GridCtrl.Value.ToLower() == " + s34 + "modall" + s34 + ")" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          if (UpdateDataAll(hh_ActKey.Value, ref st_dberrmsg))" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            SetSerMod();" + s1310;
      v_Str = v_Str + "            hh_GridCtrl.Value = " + s34 + "rekey" + s34 + ";" + s1310;
      v_Str = v_Str + "            BindNew(true);" + s1310;
      v_Str = v_Str + "            hh_GridCtrl.Value = " + s34 + "ser" + s34 + ";" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "          else" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            lb_ErrorMessage.Visible = true;" + s1310;
      v_Str = v_Str + "            lb_ErrorMessage.Text = st_dberrmsg;" + s1310;
      v_Str = v_Str + "          }" + s1310;
      v_Str = v_Str + "        }  //" + s1310;
      v_Str = v_Str + "        else" + s1310;
      v_Str = v_Str + "        {" + s1310;
      v_Str = v_Str + "          string st_addselect = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "          string st_addjoin = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "          string st_addunion = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "          string st_SelDataKey = " + s34 + v_Tbl + "_gkey='" + s34 + "+hh_GridGkey.Value+" + s34 + "'" + s34 + ";" + s1310;
      v_Str = v_Str + "          if (hh_GridCtrl.Value.ToLower() == " + s34 + "ins" + s34 + ")" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            //自動編號" + s1310;
      v_Str = v_Str + "            //DateTime dt_idat=Convert.ToDateTime(tx_riza_RIDAT.Text);" + s1310;
      v_Str = v_Str + "            //st_ren_yymmtext =sFN.strzeroi(dt_idat.Year,4)+sFN.strzeroi(dt_idat.Month,2);" + s1310;
      v_Str = v_Str + "            //st_ren_cls=st_ren_yymmtext;" + s1310;
      v_Str = v_Str + "            //tx_riza_RIREN.Text = rizaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);" + s1310;
      v_Str = v_Str + "            //conn.Close();" + s1310;
      v_Str = v_Str + "            " + s1310;
      v_Str = v_Str + "            //檢查重複" + s1310;
      v_Str = v_Str + "            if (" + v_Tbl + "Dao.IsExists(" + s34 + v_Tbl + s34 + ", " + s34 + "_Primary_key_" + s34 + ", Primary_key.Text, " + s34 + s34 + "))" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              bl_insok = false;" + s1310;
      v_Str = v_Str + "              st_dberrmsg = StringTable.GetString(Primary_key.Text + " + s34 + ",已存在." + s34 + ");" + s1310;
      v_Str = v_Str + "              " + s1310;
      v_Str = v_Str + "             // " + v_Tbl + "Dao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_riza_RIREN.Text);" + s1310;
      v_Str = v_Str + "             // st_dberrmsg = StringTable.GetString(tx_riza_RIREN.Text + " + s34 + ",已重新取號." + s34 + ");" + s1310;
      v_Str = v_Str + "             // //tx_riza_RIREN.Text = " + v_Tbl + "Dao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);";
      v_Str = v_Str + "             // tx_riza_RIREN.Text =" + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "             // conn.Close();" + s1310;
      v_Str = v_Str + "             " + s1310;
      v_Str = v_Str + "            }" + s1310;
      v_Str = v_Str + "            else" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              DataTable tb_" + v_Tbl + "_ins = new DataTable();" + s1310;
      v_Str = v_Str + "              DbDataAdapter da_ADP_ins = " + v_Tbl + "Dao.GetDataAdapter(ApVer, " + s34 + "UN" + v_Tbl + s34 + ", " + s34 + v_Tbl + s34 + ", st_addselect, false, st_addjoin, CmdQueryS_" + v_Tbl + ", st_addunion, " + s34 + s34 + ");" + s1310;
      v_Str = v_Str + "              da_ADP_ins.Fill(tb_" + v_Tbl + "_ins);" + s1310;
      v_Str = v_Str + "              conn.Open();" + s1310;
      v_Str = v_Str + "              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);" + s1310;
      v_Str = v_Str + "              da_ADP_ins.InsertCommand.Transaction = thistran;" + s1310;
      v_Str = v_Str + "              try" + s1310;
      v_Str = v_Str + "              {" + s1310;
      v_Str = v_Str + "                DataRow ins_row = tb_" + v_Tbl + "_ins.NewRow();" + s1310;
      v_Str = v_Str + "                st_tempgkey = DAC.get_guidkey();" + s1310;
      v_Str = v_Str + "                ins_row[" + s34 + v_Tbl + "_gkey" + s34 + "] = st_tempgkey;       // " + s1310;
      v_Str = v_Str + "                ins_row[" + s34 + v_Tbl + "_mkey" + s34 + "] = DAC.get_guidkey(); //" + s1310 + str_ins_row;
      v_Str = v_Str + "                " + s1310;
      v_Str = v_Str + "                ins_row[" + s34 + v_Tbl + "_trusr" + s34 + "] = UserGkey;  //" + s1310;
      v_Str = v_Str + "                tb_" + v_Tbl + "_ins.Rows.Add(ins_row);" + s1310;
      v_Str = v_Str + "                //" + s1310;
      v_Str = v_Str + "                //" + s1310;
      v_Str = v_Str + "                da_ADP_ins.Update(tb_" + v_Tbl + "_ins);" + s1310;
      v_Str = v_Str + "                // " + v_Tbl + "Dao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_riza_RIREN.Text.Trim());" + s1310;
      v_Str = v_Str + "                " + v_Tbl + "Dao.Insertbalog(conn, thistran, " + s34 + v_Tbl + s34 + ", hh_ActKey.Value, hh_GridGkey.Value);" + s1310;
      v_Str = v_Str + "                " + v_Tbl + "Dao.Insertbtlog(conn, thistran, " + s34 + v_Tbl + s34 + ", DAC.GetStringValue(ins_row[" + s34 + v_Tbl + "_gkey" + s34 + "]), " + s34 + "I" + s34 + ", UserName, DAC.GetStringValue(ins_row[" + s34 + v_Tbl + "_gkey" + s34 + "]));" + s1310;
      v_Str = v_Str + "                thistran.Commit();" + s1310;
      v_Str = v_Str + "                bl_insok = true;" + s1310;
      v_Str = v_Str + "              }" + s1310;
      v_Str = v_Str + "              catch (Exception e)" + s1310;
      v_Str = v_Str + "              {" + s1310;
      v_Str = v_Str + "                thistran.Rollback();" + s1310;
      v_Str = v_Str + "                bl_insok = false;" + s1310;
      v_Str = v_Str + "                st_dberrmsg = e.Message;" + s1310;
      v_Str = v_Str + "              }" + s1310;
      v_Str = v_Str + "              finally" + s1310;
      v_Str = v_Str + "              {" + s1310;
      v_Str = v_Str + "                thistran.Dispose();" + s1310;
      v_Str = v_Str + "                " + v_Tbl + "Dao.Dispose();" + s1310;
      v_Str = v_Str + "                tb_" + v_Tbl + "_ins.Dispose();" + s1310;
      v_Str = v_Str + "                da_ADP_ins.Dispose();" + s1310;
      v_Str = v_Str + "                conn.Close();" + s1310;
      v_Str = v_Str + "              }" + s1310;
      v_Str = v_Str + "            }" + s1310;
      v_Str = v_Str + "            if (bl_insok)" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              hh_GridGkey.Value = st_tempgkey;" + s1310;
      v_Str = v_Str + "              hh_GridCtrl.Value = " + s34 + "rekey" + s34 + ";" + s1310;
      v_Str = v_Str + "              BindNew(true);" + s1310;
      v_Str = v_Str + "              hh_GridCtrl.Value = " + s34 + "ser" + s34 + ";" + s1310;
      v_Str = v_Str + "              SetSerMod();" + s1310;
      v_Str = v_Str + "            }" + s1310;
      v_Str = v_Str + "            else" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              lb_ErrorMessage.Text = st_dberrmsg;" + s1310;
      v_Str = v_Str + "              lb_ErrorMessage.Visible = true;" + s1310;
      v_Str = v_Str + "            } //bl_insok" + s1310;
      v_Str = v_Str + "          }  //ins" + s1310;
      v_Str = v_Str + "          else if (hh_GridCtrl.Value.ToLower() == " + s34 + "mod" + s34 + ")" + s1310;
      v_Str = v_Str + "          {" + s1310;
      v_Str = v_Str + "            if (" + v_Tbl + "Dao.IsExists(" + s34 + v_Tbl + s34 + ", " + s34 + "_Primary_key_" + s34 + ", Primary_key.Text, " + s34 + "gkey<>'" + s34 + "+hh_GridGkey.Value+" + s34 + "'" + s34 + "))" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              bl_updateok = false;" + s1310;
      v_Str = v_Str + "              st_dberrmsg = StringTable.GetString(Primary_key.Text+" + s34 + ",已存在." + s34 + ");" + s1310;
      v_Str = v_Str + "            }" + s1310;
      v_Str = v_Str + "            else" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              DataTable tb_" + v_Tbl + "_mod = new DataTable();" + s1310;
      v_Str = v_Str + "              DbDataAdapter da_ADP_mod = " + v_Tbl + "Dao.GetDataAdapter(ApVer, " + s34 + "UN" + v_Tbl + s34 + ", " + s34 + v_Tbl + s34 + ", st_addselect, false, st_addjoin, CmdQueryS_" + v_Tbl + ", st_addunion, " + s34 + s34 + ");" + s1310;
      v_Str = v_Str + "              da_ADP_mod.Fill(tb_" + v_Tbl + "_mod);" + s1310;
      v_Str = v_Str + "              st_SelDataKey = " + s34 + v_Tbl + "_gkey='" + s34 + " + hh_GridGkey.Value + " + s34 + "' and " + v_Tbl + "_mkey='" + s34 + "+hh_mkey.Value+" + s34 + "' " + s34 + ";" + s1310;
      v_Str = v_Str + "              DataRow[] mod_rows = tb_" + v_Tbl + "_mod.Select(st_SelDataKey);" + s1310;
      v_Str = v_Str + "              DataRow mod_row;" + s1310;
      v_Str = v_Str + "              if (mod_rows.Length != 1)" + s1310;
      v_Str = v_Str + "              {" + s1310;
      v_Str = v_Str + "                bl_updateok = false;" + s1310;
      v_Str = v_Str + "                st_dberrmsg = StringTable.GetString(" + s34 + "資料已變更,請重新選取!" + s34 + ");" + s1310;
      v_Str = v_Str + "              }" + s1310;
      v_Str = v_Str + "              else" + s1310;
      v_Str = v_Str + "              {" + s1310;
      v_Str = v_Str + "                conn.Open();" + s1310;
      v_Str = v_Str + "                OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);" + s1310;
      v_Str = v_Str + "                da_ADP_mod.UpdateCommand.Transaction = thistran;" + s1310;
      v_Str = v_Str + "                try" + s1310;
      v_Str = v_Str + "                {" + s1310;
      v_Str = v_Str + "                  mod_row = mod_rows[0];" + s1310;
      v_Str = v_Str + "                  mod_row.BeginEdit();" + s1310;
      v_Str = v_Str + "                  " + s1310 + str_mod_row;
      v_Str = v_Str + "                  " + s1310;
      v_Str = v_Str + "                  mod_row[" + s34 + v_Tbl + "_mkey" + s34 + "] = DAC.get_guidkey();        //" + s1310;
      v_Str = v_Str + "                  mod_row[" + s34 + v_Tbl + "_trusr" + s34 + "] =UserGkey;  //" + s1310;
      v_Str = v_Str + "                  mod_row.EndEdit();" + s1310;
      v_Str = v_Str + "                  da_ADP_mod.Update(tb_" + v_Tbl + "_mod);" + s1310;
      v_Str = v_Str + "                  " + v_Tbl + "Dao.Insertbalog(conn, thistran, " + s34 + v_Tbl + s34 + ", hh_ActKey.Value, hh_GridGkey.Value);" + s1310;
      v_Str = v_Str + "                  " + v_Tbl + "Dao.Insertbtlog(conn, thistran, " + s34 + v_Tbl + s34 + ", DAC.GetStringValue(mod_row[" + s34 + v_Tbl + "_gkey" + s34 + "]), " + s34 + "M" + s34 + ", UserName, DAC.GetStringValue(mod_row[" + s34 + v_Tbl + "_gkey" + s34 + "]));" + s1310;
      v_Str = v_Str + "                  thistran.Commit();" + s1310;
      v_Str = v_Str + "                  bl_updateok = true;" + s1310;
      v_Str = v_Str + "                }" + s1310;
      v_Str = v_Str + "                catch (Exception e)" + s1310;
      v_Str = v_Str + "                {" + s1310;
      v_Str = v_Str + "                  thistran.Rollback();" + s1310;
      v_Str = v_Str + "                  bl_updateok = false;" + s1310;
      v_Str = v_Str + "                  st_dberrmsg = e.Message;" + s1310;
      v_Str = v_Str + "                }" + s1310;
      v_Str = v_Str + "                finally" + s1310;
      v_Str = v_Str + "                {" + s1310;
      v_Str = v_Str + "                  thistran.Dispose();" + s1310;
      v_Str = v_Str + "                  " + v_Tbl + "Dao.Dispose();" + s1310;
      v_Str = v_Str + "                  tb_" + v_Tbl + "_mod.Dispose();" + s1310;
      v_Str = v_Str + "                  da_ADP_mod.Dispose();" + s1310;
      v_Str = v_Str + "                  conn.Close();" + s1310;
      v_Str = v_Str + "                }" + s1310;
      v_Str = v_Str + "              } //mod_rows.Length=1" + s1310;
      v_Str = v_Str + "            } //IsExists" + s1310;
      v_Str = v_Str + "            if (bl_updateok)" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              //hh_GridCtrl.Value = " + s34 + "rekey" + s34 + ";" + s1310;
      v_Str = v_Str + "              BindNew(true);" + s1310;
      v_Str = v_Str + "              hh_GridCtrl.Value = " + s34 + "ser" + s34 + ";" + s1310;
      v_Str = v_Str + "              SetSerMod();" + s1310;
      v_Str = v_Str + "            }" + s1310;
      v_Str = v_Str + "            else" + s1310;
      v_Str = v_Str + "            {" + s1310;
      v_Str = v_Str + "              lb_ErrorMessage.Text = st_dberrmsg;" + s1310;
      v_Str = v_Str + "              lb_ErrorMessage.Visible = true;" + s1310;
      v_Str = v_Str + "            } //bl_updateok" + s1310;
      v_Str = v_Str + "          }   //mod" + s1310;
      v_Str = v_Str + "        }  //ins & mod" + s1310;
      v_Str = v_Str + "        " + v_Tbl + "Dao.Dispose();" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "      else" + s1310;
      v_Str = v_Str + "      {" + s1310;
      v_Str = v_Str + "        lb_ErrorMessage.Visible = true;" + s1310;
      v_Str = v_Str + "        lb_ErrorMessage.Text = st_ckerrmsg;" + s1310;
      v_Str = v_Str + "      }" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;
      //
      v_Str = v_Str + "    private bool ServerEditCheck(ref string sMsg)" + s1310;
      v_Str = v_Str + "    {" + s1310;
      v_Str = v_Str + "      bool ret;" + s1310;
      v_Str = v_Str + "      ret = true;" + s1310;
      v_Str = v_Str + "      sMsg = " + s34 + s34 + ";" + s1310;
      v_Str = v_Str + "      clsDataCheck DataCheck = new clsDataCheck();" + s1310;
      v_Str = v_Str + "      " + s1310 + str_server_edit;
      v_Str = v_Str + "      DataCheck.Dispose();" + s1310;
      v_Str = v_Str + "      return ret;" + s1310;
      v_Str = v_Str + "    }" + s1310;
      v_Str = v_Str + "    " + s1310;

      Fhdsw.Write(v_Str);
      //
      Fhdsw.Close();
      Fhdsw.Dispose();
      dbset_DAO.Dispose();
      cmd.Dispose();
      tb_DBSET.Dispose();
      //
      Session["fm_httpx_HyFile1_Text"] = "Souce Function:" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      Session["fm_httpx_HyFile1_url"] = v_doc_http;

    }

    protected void act_HTML()
    {
      //
      string v_Str_YNET = "";
      string vOROW = "";
      string j_Tbl = "";
      string v_Tbl = DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      string v_Apx = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]);
      //
      //
      StreamWriter Fhdsw;
      DAC dbset_DAO = new DAC(conn);
      OleDbCommand cmd = new OleDbCommand();
      DataTable tb_DBSET = new DataTable();
      DataRow dr_ROW;
      //
      //begin http
      string v_doc_file = "";
      string v_doc_http = "";
      string v_doc_guid = sFN.Get_guid5();
      //
      v_doc_file = sys_DocFilePath + tx_DBSET_DBAPX.Text + "_HTML_" + v_doc_guid + ".txt";
      v_doc_http = tx_DBSET_DBAPX.Text + "_HTML_" + v_doc_guid + ".txt";
      Fhdsw = File.CreateText(v_doc_file);
      //END http
      //Fhdsw = File.CreateText(sys_DocFilePath + tx_DBSET_DBAPX.Text + "_HTML" + "_" + sFN.Get_guid5() + ".txt");
      cmd.CommandText = "SELECT * FROM DBSET WHERE DBVER=? AND DBREN=? AND DBAPX=? AND DBNUM=? AND DBTBL=? ORDER BY DBROW,DBCOL ";
      DAC.AddParam(cmd, "DBVER", tx_DBSET_DBVER.Text);
      DAC.AddParam(cmd, "DBREN", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]));
      DAC.AddParam(cmd, "DBAPX", tx_DBSET_DBAPX.Text);
      DAC.AddParam(cmd, "DBNUM", tx_DBSET_DBNUM.Text);
      DAC.AddParam(cmd, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
      tb_DBSET = dbset_DAO.Select(cmd);
      //
      vOROW = "#";
      v_Str_YNET = "";
      v_Str_YNET = v_Str_YNET + "<asp:Literal ID=" + s34 + "li_AccMsg" + s34 + " runat=" + s34 + "server" + s34 + "></asp:Literal>" + s1310;
      v_Str_YNET = v_Str_YNET + "<input id=" + s34 + "hh_GridGkey" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "hh_GridGkey" + s34 + " runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET = v_Str_YNET + "<input id=" + s34 + "hh_GridCtrl" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "hh_GridCtrl" + s34 + " runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET = v_Str_YNET + "<input id=" + s34 + "hh_ActKey" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "hh_ActGuidKey" + s34 + " runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET = v_Str_YNET + "<input id=" + s34 + "hh_mkey" + s34 + " type=" + s34 + "hidden" + s34 + " name=" + s34 + "hh_mkey" + s34 + " runat=" + s34 + "server" + s34 + " />" + s1310;
      v_Str_YNET = v_Str_YNET + "<table>" + s1310;
      v_Str_YNET = v_Str_YNET + "  <tr>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_CAN" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "CANCEL" + s34 + " AccessKey=" + s34 + "C" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "C上一步" + s34 + " Width=" + s34 + "80px" + s34 + " OnClick=" + s34 + "bt_CAN_Click" + s34 + "></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_SAV" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "SAVE" + s34 + " AccessKey=" + s34 + "S" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "S完成" + s34 + " Width=" + s34 + "80px" + s34 + " OnClick=" + s34 + "bt_SAV_Click" + s34 + "></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_02" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "NEW" + s34 + " AccessKey=" + s34 + "N" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "N新增" + s34 + " Width=" + s34 + "80px" + s34 + " OnClick=" + s34 + "bt_02_Click" + s34 + " ></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_03" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "INSERT" + s34 + " AccessKey=" + s34 + "I" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "I插入" + s34 + " Width=" + s34 + "80px" + s34 + " ></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_04" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "MODIFY" + s34 + " AccessKey=" + s34 + "M" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "M修改" + s34 + " Width=" + s34 + "80px" + s34 + " OnClick=" + s34 + "bt_04_Click" + s34 + " ></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_05" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "DELETE" + s34 + " AccessKey=" + s34 + "X" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "X刪除" + s34 + " Width=" + s34 + "80px" + s34 + " OnClick=" + s34 + "bt_05_Click" + s34 + " ></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_06" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "COPY" + s34 + " AccessKey=" + s34 + "O" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "O複製" + s34 + " Width=" + s34 + "80px" + s34 + " ></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_07" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "PRINT" + s34 + " AccessKey=" + s34 + "P" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "P列印" + s34 + " Width=" + s34 + "80px" + s34 + "></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_08" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "SERCH" + s34 + " AccessKey=" + s34 + "F" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "F查詢" + s34 + " Width=" + s34 + "80px" + s34 + "></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_09" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "TRANS" + s34 + " AccessKey=" + s34 + "T" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "T轉單" + s34 + " Width=" + s34 + "80px" + s34 + "></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_10" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "EXCEL" + s34 + " AccessKey=" + s34 + "E" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "Excel" + s34 + " Width=" + s34 + "80px" + s34 + "></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_11" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "MODALL" + s34 + " AccessKey=" + s34 + "B" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "L整批修改" + s34 + " Width=" + s34 + "80px" + s34 + " ></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "    <td>" + s1310;
      v_Str_YNET = v_Str_YNET + "      <asp:LinkButton ID=" + s34 + "bt_QUT" + s34 + " runat=" + s34 + "server" + s34 + " CausesValidation=" + s34 + "False" + s34 + " CommandName=" + s34 + "QUIT" + s34 + " AccessKey=" + s34 + "Q" + s34 + " CssClass=" + s34 + "LinkButton80" + s34 + " Text=" + s34 + "Q離開" + s34 + " Width=" + s34 + "80px" + s34 + " OnClick=" + s34 + "bt_QUT_Click" + s34 + "></asp:LinkButton>" + s1310;
      v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
      v_Str_YNET = v_Str_YNET + "  </tr>" + s1310;
      v_Str_YNET = v_Str_YNET + "</table>" + s1310;
      v_Str_YNET = v_Str_YNET + "<table>" + s1310;

      //
      for (int in_rec = 0; in_rec < tb_DBSET.Rows.Count; in_rec++)
      {
        dr_ROW = tb_DBSET.Rows[in_rec];
        if ((DAC.GetStringValue(dr_ROW["DBROW"]) != "0") && !(DAC.GetStringValue(dr_ROW["DBROW"]) == "99999"))
        {
          if ((vOROW != DAC.GetStringValue(dr_ROW["DBROW"])) && (vOROW != "#"))
          {
            v_Str_YNET = v_Str_YNET + "  </tr>" + s1310;
          }
          //
          if (vOROW != DAC.GetStringValue(dr_ROW["DBROW"]))
          {
            v_Str_YNET = v_Str_YNET + "  <tr>" + s1310;
            vOROW = DAC.GetStringValue(dr_ROW["DBROW"]);
          }
          if (DAC.GetStringValue(dr_ROW["DBJIA"]) == "A")
          {
            j_Tbl = v_Tbl + "_";
          }
          else
          {
            j_Tbl = DAC.GetStringValue(dr_ROW["DBJIN"]);
            j_Tbl = "";
          }
          //
          if (DAC.GetStringValue(dr_ROW["DBUCO"]).Trim().ToUpper() == "DROPDOWNLIST")
          {
            v_Str_YNET = v_Str_YNET + "    <td > " + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:Label id=" + s34 + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + " runat=" + s34 + "server" + s34 + " Text=" + s34 + DAC.GetStringValue(dr_ROW["DBTNA"]) + s34 + " " + "></asp:Label>" + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:dropdownlist id=" + s34 + "dr_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]).Trim() + s34 + " width=" + s34 + "80px" + s34 + "  runat=" + s34 + "server" + s34 + "/>" + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:TextBox      id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]).Trim() + s34 + " width=" + s34 + "0px" + s34 + " Visible=" + s34 + "false" + s34 + "  runat=" + s34 + "server" + s34 + "/>" + s1310;
            v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).Trim().ToUpper() == "CHECKBOX")
          {
            v_Str_YNET = v_Str_YNET + "    <td > " + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:checkbox id=" + s34 + "ck_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]).Trim() + s34 + " width=" + s34 + "100px" + s34 + "  runat=" + s34 + "server" + s34 + " TEXT=" + s34 + DAC.GetStringValue(dr_ROW["DBTNA"]).Trim() + s34 + "/>" + s1310;
            v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).Trim().ToUpper() == "WEBNUMERICEDITOR")
          {
            v_Str_YNET = v_Str_YNET + "    <td > " + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:Label id=" + s34 + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + " runat=" + s34 + "server" + s34 + " Text=" + s34 + DAC.GetStringValue(dr_ROW["DBTNA"]) + s34 + " " + "></asp:Label>" + s1310;
            v_Str_YNET = v_Str_YNET + "      <ig:WebNumericEditor id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]).Trim() + s34 + " width=" + s34 + "120px" + s34 + " runat=" + s34 + "server" + s34 + " MinDecimalPlaces=" + s34 + "2" + s34 + " StyleSetName=" + s34 + "Appletini" + s34 + " StyleSetPath=" + s34 + "../../../ig_res" + s34 + "></ig:WebNumericEditor>" + s1310;
            v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).Trim().ToUpper() == "WEBDATETIMEEDITOR")
          {
            v_Str_YNET = v_Str_YNET + "    <td > " + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:Label id=" + s34 + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + " runat=" + s34 + "server" + s34 + " Text=" + s34 + DAC.GetStringValue(dr_ROW["DBTNA"]) + s34 + " " + "></asp:Label>" + s1310;
            v_Str_YNET = v_Str_YNET + "      <ig:WebDateTimeEditor id=" + s34 + "tx_"+ j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]).Trim() + s34 + " Width=" + s34 + "90px" + s34 + " StyleSetName=" + s34 + "Appletini" + s34 + " StyleSetPath=" + s34 + "../../../ig_res" + s34 + " runat=" + s34 + "server" + s34 + "></ig:WebDateTimeEditor>" + s1310;
            v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).Trim().ToUpper() == "WEBDATEPICKER")
          {
            v_Str_YNET = v_Str_YNET + "    <td >" + s1310;
            v_Str_YNET = v_Str_YNET + "      <table>" + s1310;
            v_Str_YNET = v_Str_YNET + "        <tr>" + s1310;
            v_Str_YNET = v_Str_YNET + "          <td>" + s1310;
            v_Str_YNET = v_Str_YNET + "            <asp:Label id=" + s34 + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + " runat=" + s34 + "server" + s34 + " Text=" + s34 + DAC.GetStringValue(dr_ROW["DBTNA"]) + s34 + " " + "></asp:Label>" + s1310;
            v_Str_YNET = v_Str_YNET + "          </td>" + s1310;
            v_Str_YNET = v_Str_YNET + "          <td>" + s1310;
            v_Str_YNET = v_Str_YNET + "            <ig:WebDatePicker id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + " Width=" + s34 + "120px" + s34 + " runat=" + s34 + "server" + s34 + " DisplayModeFormat=" + s34 + "d" + s34 + " Font-Size=" + s34 + "Medium" + s34 + "></ig:WebDatePicker>" + s1310;
            v_Str_YNET = v_Str_YNET + "          </td>" + s1310;
            v_Str_YNET = v_Str_YNET + "        </tr>" + s1310;
            v_Str_YNET = v_Str_YNET + "      </table>" + s1310;            
            v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
          }
          else if (DAC.GetStringValue(dr_ROW["DBUCO"]).Trim().ToUpper() == "TEXTBOX")
          {
            v_Str_YNET = v_Str_YNET + "    <td > " + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:Label id=" + s34 + "lb_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + " runat=" + s34 + "server" + s34 + " Text=" + s34 + DAC.GetStringValue(dr_ROW["DBTNA"]) + s34 + " " + "></asp:Label>" + s1310;
            v_Str_YNET = v_Str_YNET + "      <asp:TextBox id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + s34 + " width=" + s34 + "80px" + s34 + " runat=" + s34 + "server" + s34 + " MaxLength=" + s34 + DAC.GetStringValue(dr_ROW["DBLEN"]) + s34 + "></asp:TextBox>" + s1310;
            if (DAC.GetInt16Value(dr_ROW["DBUED"]) != 0)
            {
              v_Str_YNET = v_Str_YNET + "      <asp:TextBox id=" + s34 + "tx_" + j_Tbl + DAC.GetStringValue(dr_ROW["DBFLD"]) + "_N" + s34 + " width=" + s34 + "80px" + s34 + " runat=" + s34 + "server" + s34 + " MaxLength=" + s34 + DAC.GetStringValue(dr_ROW["DBLEN"]) + s34 + "></asp:TextBox>" + s1310;
            }
            v_Str_YNET = v_Str_YNET + "    </td>" + s1310;
          }

        }
      }
      v_Str_YNET = v_Str_YNET + "  </tr>" + s1310;
      v_Str_YNET = v_Str_YNET + "</table>" + s1310;
      v_Str_YNET = v_Str_YNET + "<asp:Label ID=" + s34 + "lb_ErrorMessage" + s34 + " runat=" + s34 + "server" + s34 + " Text=" + s34 + "" + s34 + " EnableViewState=" + s34 + "false" + s34 + " Visible=" + s34 + "false" + s34 + " CssClass=" + s34 + "ErrorMessage" + s34 + "></asp:Label>" + s1310;
      Fhdsw.Write(v_Str_YNET);
      Fhdsw.Close();
      Fhdsw.Dispose();
      dbset_DAO.Dispose();
      cmd.Dispose();
      tb_DBSET.Dispose();
      //begin http
      Session["fm_httpx_HyFile2_Text"] = "GridView HTML Table:" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);
      Session["fm_httpx_HyFile2_url"] = v_doc_http;
      //end http

    }

    protected void bt_07_Click(object sender, EventArgs e)
    {
      Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
      Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
      Session["fmDBSET_gr_GridView_DBSET_GridGkey"] = gr_GridView_DBSET.DataKeys[gr_GridView_DBSET.SelectedIndex].Value.ToString();
      act_PRN();
    }

    protected void act_PRN()
    {
      //
      OleDbCommand cmd_Query = new OleDbCommand();
      cmd_Query.CommandText = "";
      cmd_Query.CommandText += " and a.DBVER='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]) + "' ";
      cmd_Query.CommandText += " and a.DBAPX='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]) + "' ";
      cmd_Query.CommandText += " and a.DBTBL='" + DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]) + "' ";

      //cmd_Query.CommandText = " and a.DBVER=? and a.DBAPX=? and a.DBTBL=? ";
      //DAC.AddParam(cmd_Query, "DBVER", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]));
      //DAC.AddParam(cmd_Query, "DBAPX", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]));
      //DAC.AddParam(cmd_Query, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));

      //
      DataTable tb_dbset = new DataTable();
      OleDbDataAdapter db_Adap = new OleDbDataAdapter();
      OleDbCommand cmd_Select = new OleDbCommand();
      DAC dbset_DAO = new DAC(conn);
      //
      //cmd_Select.CommandText = @"select gkey,code,cname,ename,mdate,trcrd,trmod,trusr
      //                           from sn100 with (nolock) ";
      cmd_Select = dbset_DAO.GetSelectCommand(ApVer, "UNDBSET", "DBSET", "", false, "", cmd_Query, "", "a.dbrow,a.dbcol");
      cmd_Select.Connection = conn;
      db_Adap.SelectCommand = cmd_Select;
      db_Adap.Fill(tb_dbset);
      DataRow mod_row;
      string st_def = "";
      string st_typ = "";
      for (int in_rec = 0; in_rec < tb_dbset.Rows.Count; in_rec++)
      {
        mod_row = tb_dbset.Rows[in_rec];
        mod_row.BeginEdit();
        st_typ = DAC.GetStringValue(mod_row["DBSET_DBTYP"]).ToUpper();
        st_def = DAC.GetStringValue(mod_row["DBSET_DBDEF"]);
        st_def = st_def.Replace("ALTER", "");
        st_def = st_def.Replace("<>", "");
        st_def = st_def.Replace("ALERT", "");
        if (st_def.Trim() == "")
        {
          if ((st_typ.IndexOf("INT") >= 0) || (st_typ == "MONEY"))
          {
            st_def = "0";
          }
          else if (st_typ.IndexOf("CHAR") >= 0)
          {
            st_def = "''";
          }
        }
        mod_row["DBSET_DBDEF"] = st_def;
        mod_row.EndEdit();
      }
      //
      string st_mdbFileName = "";
      string st_reportFileName = "";
      db_Adap.FillSchema(tb_dbset, SchemaType.Mapped);
      //
      YTableTr tableTr = new YTableTr();
      tableTr.st_datapath = sys_ReportDataFullPath;
      tableTr.st_rptpath = sys_ReportRptFullPath;
      tableTr.st_rpt_guid = DAC.get_guidkey().Substring(28);
      tableTr.st_accessTableName = "DBSETP" + "01";
      tableTr.SetConn();
      tableTr.TableTrAdo(tb_dbset, "M", false, tableTr.st_accessTableName + "_" + tableTr.st_rpt_guid);
      st_mdbFileName = tableTr.st_mdbfullname;
      //
      tableTr.Dispose();
      tb_dbset.Dispose();
      cmd_Select.Dispose();
      db_Adap.Dispose();
      cmd_Query.Dispose();
      //
      //
      YCrExport cr = new YCrExport();

      cr.AddParameter("company_name", PublicVariable.CompanyName);
      cr.AddParameter("report_name", "資料架構說明");
      cr.AddParameter("table_name", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
      cr.AddParameter("function_name", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]));
      cr.AddParameter("function_cname", DAC.GetStringValue(Session["fmDASET_tx_DASET_DANAM"]));
      st_reportFileName = "DBSETP" + "01";
      //
      cr.LoadReport(Server.MapPath(sys_ReportRpt) + st_reportFileName + ".rpt", st_mdbFileName);
      cr.WebExportAsPdf(Response, Uri.EscapeUriString(FunctionName + DateTime.Now.ToString("_yyyyMMdd")));
      cr.CloseReport();
    }

    protected void bt_10_Click(object sender, EventArgs e)
    {
      Session["fmDBSET_gr_GridView_DBSET_PageIndex"] = gr_GridView_DBSET.PageIndex;
      Session["fmDBSET_gr_GridView_DBSET_SelectedIndex"] = gr_GridView_DBSET.SelectedIndex;
      //Session["fmDBSET_gr_GridView_DBSET_GridGkey"] = gr_GridView_DBSET.DataKeys[gr_GridView_DBSET.SelectedIndex].Value.ToString();
      Response.Redirect("~\\forms\\dax\\dx_ddimport.aspx");
    }

  }
}