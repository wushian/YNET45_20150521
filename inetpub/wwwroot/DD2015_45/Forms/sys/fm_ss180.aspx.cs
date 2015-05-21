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
  public partial class fm_ss180 : FormBase
  {
    string st_object_func = "sys_ss180";
    bool st_accmod = false;
    public string st_s101_gkey = "";
    public string st_tree_gkey = "";
    OleDbCommand CmdQueryS_ss101 = DAC.NewCommand();
    OleDbCommand CmdQueryS_ss180 = DAC.NewCommand();
    //
    protected void Page_Load(object sender, EventArgs e)
    {
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {

        st_s101_gkey = Server.HtmlDecode(DAC.GetStringValue(Request["s101_gkey"]));
        st_tree_gkey = Server.HtmlDecode(DAC.GetStringValue(Request["obj_name"]));
        hh_treekey.Value = st_tree_gkey;
        st_accmod = sFN.chkAccessFunc_(UserGkey, st_object_func, 4, UserLoginGkey);
        if (UserGkey == "Admin") { st_accmod = true; }
        bn_ACC.Visible = st_accmod;
        if (!IsPostBack)
        {
          dr_ss101_usertype = sFN.DropDownListFromClasses(ref dr_ss101_usertype, "es101_usertype", "class_text", "class_value");
          dr_es101_no = sFN.DropDownListFromTable(ref dr_es101_no, "es101", "gkey", "no+cname", "", "no");
          CmdQueryS_ss101.CommandText = " AND 1=1 ";
          Session["fmss180_CmdQueryS_ss101"] = CmdQueryS_ss101;
          //
          Set_Control();
          SetSerMod();
          //有object進來
          if (st_tree_gkey != "")
          {
            gr_GridView_ss101.PageIndex = DAC.GetInt16Value(Session["fm_ss180_gr_GridView_ss101_PageIndex"]);
            gr_GridView_ss101.SelectedIndex = DAC.GetInt16Value(Session["fm_ss180_gr_GridView_ss101_SelectedIndex"]);
            hh_GridGkey.Value = DAC.GetStringValue(Session["fm_ss180_gr_GridView_ss101_GridGkey"]);
          }
          BindNew(true, 0);
          Session["fm_ss180_gr_GridView_ss101_PageIndex"] = gr_GridView_ss101.PageIndex;
          Session["fm_ss180_gr_GridView_ss101_SelectedIndex"] = gr_GridView_ss101.SelectedIndex;
          //reset gkey

          //
        }
        else
        {
          if ((hh_GridCtrl.Value.ToString().ToLower() == "ins") || (hh_GridCtrl.Value.ToString().ToLower() == "mod"))
          {
            BindNew(false, 0);
          }
          else if (hh_GridCtrl.Value.ToString().ToLower() == "modall")
          {
            BindNew(false, 0);
          }
          else
          {
            //BindNew(true, 0);
            //SetSerMod();
          }
        }
        //
        TreeView1.Nodes.Clear();
        TreeView1 = sFN.SetTreeView_sys_menu(TreeView1, "fm_ss180.aspx?obj_name=", LangType, UserId);
        TreeView1.CollapseAll();
        if (st_tree_gkey != "") sFN.TreeViewExpandByValue(ref TreeView1, st_tree_gkey);
        //
      }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {


    }

    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle("sys_ss180", PublicVariable.LangType);   //取Page Title
      this.Page.Title = FunctionName;
    }

    private void ClearText()
    {
      dr_es101_no.SelectedIndex = -1;  //員工編號
      tx_es101_ename.Text = "";  //英文姓名
      tx_ss101_usercode.Text = "";  //帳號
      tx_ss101_password.Text = "";  //密碼
      dr_ss101_usertype.SelectedIndex = -1;  //帳號類別
      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      tx_ss101_password.TextMode = TextBoxMode.Password;
      //
      clsGV.SetTextBoxEditAlert(ref lb_ss101_usercode, ref tx_ss101_usercode, false);  //帳號
      clsGV.SetTextBoxEditAlert(ref lb_ss101_password, ref tx_ss101_password, false);  //密碼
      //
      clsGV.Drpdown_Set(ref dr_es101_no, ref tx_es101_no, false);   //員工編號
      clsGV.TextBox_Set(ref tx_es101_ename, false);      //英文姓名
      clsGV.TextBox_Set(ref tx_ss101_usercode, false);   //帳號
      clsGV.TextBox_Set(ref tx_ss101_password, false);   //密碼
      clsGV.Drpdown_Set(ref dr_ss101_usertype, ref tx_ss101_usertype, false);   //帳號類別
      //
      clsGV.SetControlShowAlert(ref lb_ss101_usercode, ref tx_ss101_usercode, true);  // 帳號
      clsGV.SetControlShowAlert(ref lb_ss101_password, ref tx_ss101_password, true);  // 密碼
      //
      bt_NEW.Visible = true;
      bt_MOD.Visible = true;
      //bt_MODALL.Visible = true;
      //bt_SER.Visible = false;
      bt_DEL.Visible = true;
      //bt_PRN.Visible = false;
      bt_SAV.Visible = false;
      bt_CAN.Visible = false;
      bt_QUT.Visible = true;
      //
      gr_GridView_ss101.Enabled = true;
    }

    private void SetEditMod()
    {
      tx_ss101_password.TextMode = TextBoxMode.SingleLine;
      // 
      clsGV.Drpdown_Set(ref dr_es101_no, ref tx_es101_no, true);   //員工編號
      clsGV.TextBox_Set(ref tx_es101_ename, false);  //英文姓名
      clsGV.TextBox_Set(ref tx_ss101_usercode, true);  //帳號
      clsGV.TextBox_Set(ref tx_ss101_password, true);  //密碼
      clsGV.Drpdown_Set(ref dr_ss101_usertype, ref tx_ss101_usertype, true);   //帳號類別
      // 
      clsGV.SetTextBoxEditAlert(ref lb_ss101_usercode, ref tx_ss101_usercode, true);  // 帳號
      clsGV.SetTextBoxEditAlert(ref lb_ss101_password, ref tx_ss101_password, true);  // 密碼
      //
      bt_NEW.Visible = false;
      bt_MOD.Visible = false;
      //bt_MODALL.Visible = false;
      //bt_SER.Visible = false;
      bt_DEL.Visible = false;
      //bt_PRN.Visible = false;
      bt_SAV.Visible = true;
      bt_CAN.Visible = true;
      bt_QUT.Visible = false;
      //
      //bt_DEL.OnClientClick = " return false;";
      //bt_MOD.OnClientClick = " return false;";
      //
      gr_GridView_ss101.Enabled = false;
    }

    private void SetEditModALL()
    {
      bt_NEW.Visible = false;
      bt_MOD.Visible = false;
      //bt_MODALL.Visible = false;
      //bt_SER.Visible = false;
      bt_DEL.Visible = false;
      //bt_PRN.Visible = false;
      bt_SAV.Visible = true;
      bt_CAN.Visible = true;
      bt_QUT.Visible = false;
      //
      //bt_DEL.OnClientClick = " return false;";
      //bt_MOD.OnClientClick = " return false;";
      //
      gr_GridView_ss101.Enabled = true;
    }

    private void BindText(DataRow CurRow)
    {
      dr_es101_no = sFN.SetDropDownList(ref dr_es101_no, DAC.GetStringValue(CurRow["ss101_es101gkey"]));  //員工編號
      tx_es101_ename.Text = DAC.GetStringValue(CurRow["es101_ename"]);  //英文姓名
      tx_ss101_usercode.Text = DAC.GetStringValue(CurRow["ss101_usercode"]);  //帳號
      tx_ss101_password.Text = DAC.GetStringValue(CurRow["ss101_password"]);  //密碼
      hh_mkey.Value = DAC.GetStringValue(CurRow["ss101_mkey"]);
      dr_ss101_usertype = sFN.SetDropDownList(ref dr_ss101_usertype, DAC.GetStringValue(CurRow["ss101_usertype"]));  //帳號類別
    }

    private void BindNew(bool bl_showdata, Int16 n_addnew)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      CmdQueryS_ss101.CommandText = " ";
      DataTable tb_ss101 = new DataTable();
      DAC_ss180 ss180Dao = new DAC_ss180(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = ss180Dao.GetDataAdapter("YN01", "UNss101", "ss101", st_addselect, false, st_addjoin, CmdQueryS_ss101, st_addunion, "ss101_gkey");
      ad_DataDataAdapter.Fill(tb_ss101);
      if (tb_ss101.Rows.Count > 0)
      {
        //bt_DEL.OnClientClick = "return btnDEL_c()";
        //bt_MOD.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        //bt_DEL.OnClientClick = "return btnDEL0_c()";
        //bt_MOD.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_ss101.DataSource = tb_ss101;
      gr_GridView_ss101 = clsGV.BindGridView(gr_GridView_ss101, tb_ss101, hh_GridCtrl, ref hh_GridGkey, "fm_ss180_gr_GridView_ss101");
      gr_GridView_ss101.DataBind();

      SelDataKey = "ss101_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_ss101.Select(SelDataKey);
      if (SelDataRow.Length == 1)
      {
        CurRow = SelDataRow[0];
        hh_es101gkey.Value = DAC.GetStringValue(CurRow["ss101_es101gkey"]);
        Session["fm_ss180_gr_GridView_ss101_GridGkey"] = hh_GridGkey.Value;
      }
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          BindText(CurRow);
          //
          BindNew_ss180(hh_es101gkey.Value, hh_treekey.Value);
        }
        else
        {
          hh_GridCtrl.Value = "init";
          ClearText();
        }
      }
      //
      if (n_addnew > 0)
      {
        for (int vr = 1; vr <= n_addnew; vr++)
        {
          CurRow = tb_ss101.NewRow();
          CurRow["ss101_usertype"] = "0";      //帳號
          CurRow["ss101_usercode"] = "";      //帳號
          CurRow["ss101_password"] = "";      //密碼
          CurRow["ss101_es101gkey"] = "_";    //系統使用
          CurRow["ss101_gkey"] = DAC.get_guidkey();    //系統使用
          CurRow["ss101_mkey"] = DAC.get_guidkey();    //系統使用
          CurRow["ss101_trusr"] = st_s101_gkey;    //系統使用
          tb_ss101.Rows.Add(CurRow);
          ad_DataDataAdapter.Update(tb_ss101);
        }
        gr_GridView_ss101.DataBind();
      }
      tb_ss101.Dispose();
      ss180Dao.Dispose();
      ad_DataDataAdapter.Dispose();
    }

    private void BindNew_ss180(string st_es101gkey, string st_sys_menu_obj_name)
    {
      DataTable tb_ss180 = new DataTable();
      DAC_ss180 ss180Dao = new DAC_ss180(conn);
      //
      CmdQueryS_ss180.Parameters.Clear();
      CmdQueryS_ss180.CommandText = " and m.obj_name in (select obj_name from sys_menu where parent_code=? or obj_name=? )  and a.es101gkey=? ";
      DAC.AddParam(CmdQueryS_ss180, "parent_code", st_sys_menu_obj_name);
      DAC.AddParam(CmdQueryS_ss180, "obj_name", st_sys_menu_obj_name);
      DAC.AddParam(CmdQueryS_ss180, "es101gkey", st_es101gkey);
      //
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      string st_addjoin = "";
      string st_addunion = "";
      st_addunion += "select '0' as sys_ss180_mark ,a.buttonno as sys_ss160_buttonno ,a.button as sys_ss160_button ,a.button_e as sys_ss160_button_e ,a.button_t as sys_ss160_button_t ,a.button_c as sys_ss160_button_c ,a.button_v as sys_ss160_button_v ,a.tip_e as sys_ss160_tip_e ,a.tip_t as sys_ss160_tip_t ,a.tip_c as sys_ss160_tip_c ,a.tip_v as sys_ss160_tip_v ,";
      st_addunion += "m.prg_code as sys_menu_prg_code,m.obj_name as sys_menu_obj_name,";
      st_addunion += "m.chinesesimpname as sys_menu_chinesesimpname,";
      st_addunion += "m.chinesebigname  as sys_menu_chinesebigname,";
      st_addunion += "m.englishname     as sys_menu_englishname,";
      st_addunion += "m.vietnamname     as sys_menu_vietnamname,";
      //
      st_addunion += "'" + st_es101gkey + "' as  sys_ss180_es101gkey ,a.gkey as  sys_ss180_ss160gkey ,'*' as sys_ss180_gkey,a.mkey as sys_ss180_mkey ,a.trcls as sys_ss180_trcls,a.trcrd as sys_ss180_trcrd,a.trmod as sys_ss180_trmod,a.trusr as sys_ss180_trusr,";
      st_addunion += "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      st_addunion += "from sys_ss160 a ";
      st_addunion += "left outer join sys_menu m on a.ss155gkey=m.obj_name ";
      st_addunion += "where a.ss155gkey in (select obj_name from sys_menu where parent_code='" + st_tree_gkey + "' or obj_name='" + st_tree_gkey + "' ) " + " and a.gkey not in (select ss160gkey from sys_ss180 where es101gkey='" + hh_es101gkey.Value + "') ";
      ad_DataDataAdapter = ss180Dao.GetDataAdapter("YN01", "UNsys_ss180", "sys_ss180", st_addselect, false, st_addjoin, CmdQueryS_ss180, st_addunion, "sys_menu_prg_code,sys_ss160_buttonno");
      ad_DataDataAdapter.Fill(tb_ss180);
      //
      DataRow CurRow;
      string st_prg = "";
      if (tb_ss180.Rows.Count > 0)
      {
        for (int vr = 0; vr < tb_ss180.Rows.Count; vr++)
        {
          CurRow = tb_ss180.Rows[vr];
          CurRow.BeginEdit();
          if (st_prg != DAC.GetStringValue(CurRow["sys_menu_prg_code"]))
          {
            CurRow["sys_menu_prg_code_"] = CurRow["sys_menu_prg_code"];
            CurRow["sys_menu_obj_name_"] = CurRow["sys_menu_obj_name"];
            //CurRow["sys_menu_chinesesimp_"] = CurRow["sys_menu_chinesesimp"];
            if (LangType == "c")
            {
              CurRow["sys_menu_chinesesimp_"] = CurRow["sys_menu_chinesesimp"];
            }
            else if (LangType == "t")
            {
              CurRow["sys_menu_chinesesimp_"] = CurRow["sys_menu_chinesebign"];
            }
            else if (LangType == "e")
            {
              CurRow["sys_menu_chinesesimp_"] = CurRow["sys_menu_englishname"];
            }
            else if (LangType == "v")
            {
              CurRow["sys_menu_chinesesimp_"] = CurRow["sys_menu_vietnamname"];
            }
            else
            {
              CurRow["sys_menu_chinesesimp_"] = CurRow["sys_menu_englishname"];
            }
            st_prg = DAC.GetStringValue(CurRow["sys_menu_prg_code"]);
          }
          if (DAC.GetStringValue(CurRow["sys_ss180_gkey"]) == "*")
          {
            CurRow["sys_ss180_gkey"] = DAC.get_guidkey();
          }
          CurRow.EndEdit();
        }
      }
      //
      gr_GridView_sys_ss180.DataSource = tb_ss180;
      //gr_GridView_sys_ss180 = clsGV.BindGridView(gr_GridView_sys_ss180, tb_ss180, hh_GridCtrl, ref hh_GridGkey, "fm_ss180_gr_GridView_ss180");
      gr_GridView_sys_ss180.DataBind();
      //
      tb_ss180.Dispose();
      ss180Dao.Dispose();
      ad_DataDataAdapter.Dispose();
    }

    private bool ServerEditCheck(ref string sMsg)
    {
      Boolean ret;
      ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();
      //
      DataCheck.Dispose();
      return ret;
    }

    protected void bt_MOD_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actMOD();
    }

    protected void actMOD()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        DAC.checkAccessFunc(UserGkey, "ss180_MOD");
        hh_GridCtrl.Value = "mod";
        Set_Control();
        SetEditMod();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(true, 0);
      }
    }

    protected void bt_QUT_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Response.Redirect("~/Master/" + Page.Theme + "/MainForm.aspx");
    }

    protected void bt_CAN_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actCAN();
    }

    protected void actCAN()
    {
      hh_GridCtrl.Value = "ser";
      Set_Control();
      ClearText();
      BindNew(true, 0);
      SetSerMod();
    }
    //
    protected void gr_GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        ////員工編號
        //if (e.Row.FindControl("dr_es101_no02") != null)
        //{
        //  DropDownList dr_es101_no02 = e.Row.FindControl("dr_es101_no02") as DropDownList;
        //  TextBox tx_es101_no02 = e.Row.FindControl("tx_es101_no02") as TextBox;
        //  dr_es101_no02 = sFN.DropDownListFromTable(ref dr_es101_no02, "es101", "gkey", "no+' '+cname", "", "no");
        //  st_datavalue = DAC.GetStringValue(rowView["ss101_es101gkey"]).Trim();
        //  //if (st_datavalue=="_")
        //  //{
        //  //  dr_es101_no02.Items.Clear();
        //  //  dr_es101_no02 = sFN.DropDownListFromTable(ref dr_es101_no02, "es101", "gkey", "no+' '+cname", " gkey not in (select es101gkey from ss101 ) ", "no");
        //  //}
        //  dr_es101_no02.Items.Add(new ListItem("*", "_"));
        //  dr_es101_no02 = sFN.SetDropDownList(ref dr_es101_no02, st_datavalue);
        //  if ((hh_GridCtrl.Value == "modall") || (hh_GridCtrl.Value == "ins")) { clsGV.Drpdown_Set(ref dr_es101_no02, ref tx_es101_no02, true); } else { clsGV.Drpdown_Set(ref dr_es101_no02, ref tx_es101_no02, false); }
        //}
        ////員工姓名
        //if (e.Row.FindControl("tx_es101_cname02") != null)
        //{
        //  TextBox tx_es101_cname02 = e.Row.FindControl("tx_es101_cname02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["es101_cname"]).Trim();
        //  tx_es101_cname02.Text = st_datavalue;
        //  if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_es101_cname02, true); } else { clsGV.TextBox_Set(ref tx_es101_cname02, false); }
        //}
        ////英文姓名
        //if (e.Row.FindControl("tx_es101_ename02") != null)
        //{
        //  TextBox tx_es101_ename02 = e.Row.FindControl("tx_es101_ename02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["es101_ename"]).Trim();
        //  tx_es101_ename02.Text = st_datavalue;
        //  if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_es101_ename02, true); } else { clsGV.TextBox_Set(ref tx_es101_ename02, false); }
        //}
        ////帳號
        //if (e.Row.FindControl("tx_ss101_usercode02") != null)
        //{
        //  TextBox tx_ss101_usercode02 = e.Row.FindControl("tx_ss101_usercode02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["ss101_usercode"]).Trim();
        //  tx_ss101_usercode02.Text = st_datavalue;
        //  if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_ss101_usercode02, true); } else { clsGV.TextBox_Set(ref tx_ss101_usercode02, false); }
        //}
        ////密碼
        //if (e.Row.FindControl("tx_ss101_password02") != null)
        //{
        //  TextBox tx_ss101_password02 = e.Row.FindControl("tx_ss101_password02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["ss101_password"]).Trim();
        //  tx_ss101_password02.Text = st_datavalue;
        //  if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_ss101_password02, true); } else { clsGV.TextBox_Set(ref tx_ss101_password02, false); }
        //}

        //帳號類別
        if (e.Row.FindControl("dr_ss101_usertype02") != null)
        {
          DropDownList dr_ss101_usertype02 = e.Row.FindControl("dr_ss101_usertype02") as DropDownList;
          TextBox tx_ss101_usertype02 = e.Row.FindControl("tx_ss101_usertype02") as TextBox;
          dr_ss101_usertype02 = sFN.DropDownListFromClasses(ref dr_ss101_usertype02, "es101_usertype", "class_text", "class_value");
          st_datavalue = DAC.GetStringValue(rowView["ss101_usertype"]).Trim();
          dr_ss101_usertype02 = sFN.SetDropDownList(ref dr_ss101_usertype02, st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.Drpdown_Set(ref dr_ss101_usertype02, ref tx_ss101_usertype02, true); } else { clsGV.Drpdown_Set(ref dr_ss101_usertype02, ref tx_ss101_usertype02, false); }
        }

      }
      sFN.Dispose();
    }

    //

    protected bool UpdateDataAllx(string st_ActKey, string ss101gkey1, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_ss101_gkey = "", st_ss101_usercode = "", st_ss101_password = "", st_ss101_es101gkey = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      //
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView_ss101$ctl";
      CmdQueryS_ss101.CommandText = " ";
      DataTable tb_ss101 = new DataTable();
      DAC_ss180 ss180Dao = new DAC_ss180(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      DbDataAdapter da_ADP = ss180Dao.GetDataAdapter("YN01", "UNss101", "ss101", st_addselect, false, st_addjoin, CmdQueryS_ss101, st_addunion, "");
      da_ADP.Fill(tb_ss101);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_ss101.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + "0" + in_g.ToString() + "$tx_ss101_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //ss101_gkey
            st_ctrlname = st_ctrl + "0" + in_g.ToString() + "$tx_ss101_gkey02";
            st_ss101_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //es101gkey
            //st_ctrlname = st_ctrl + "0" + in_g.ToString() + "$tx_ss101_es101gkey02";
            //st_es101gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //員工編號
            st_ctrlname = st_ctrl + "0" + in_g.ToString() + "$dr_es101_no02";
            st_ss101_es101gkey = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;
            //帳號
            st_ctrlname = st_ctrl + "0" + in_g.ToString() + "$tx_ss101_usercode02";
            st_ss101_usercode = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //密碼
            st_ctrlname = st_ctrl + "0" + in_g.ToString() + "$tx_ss101_password02";
            st_ss101_password = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_ss101.Select("ss101_gkey='" + st_ss101_gkey + "'");
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (
                   (DAC.GetStringValue(mod_row["ss101_usercode"]) != st_ss101_usercode)
                || (DAC.GetStringValue(mod_row["ss101_password"]) != st_ss101_password)
                || (DAC.GetStringValue(mod_row["ss101_es101gkey"]) != st_ss101_es101gkey)
                || (st_ss101_es101gkey == "_")
              )
              {
                if (st_ss101_es101gkey == "_")
                {
                  mod_row.Delete();
                }
                else
                {
                  ss180Dao.Insertbalog(conn, thistran, "ss101", st_ActKey, UserName);
                  ss180Dao.Insertbtlog(conn, thistran, "ss101", DAC.GetStringValue(mod_row["ss101_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["ss101_gkey"]) + " " + DAC.GetStringValue(mod_row["es101_no"]) + " " + DAC.GetStringValue(mod_row["ss101_es101gkey"]));
                  mod_row.BeginEdit();
                  mod_row["ss101_usercode"] = st_ss101_usercode;      //帳號
                  mod_row["ss101_password"] = st_ss101_password;      //密碼
                  mod_row["ss101_es101gkey"] = st_ss101_es101gkey;      //系統使用
                  mod_row.EndEdit();
                }
                //
              }
            }  //sel_rows.Length == 1
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_ss101);
        thistran.Commit();
        bl_updateok = true;
      }  //try
      catch (Exception e)
      {
        thistran.Rollback();
        bl_updateok = false;
        st_errmsg = e.Message;
      }
      finally
      {
        thistran.Dispose();
        ss180Dao.Dispose();
        tb_ss101.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }

    protected void bt_NEW_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actNew();
    }

    protected void actNew()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 2, UserLoginGkey, ref li_AccMsg))
      {
        DAC.checkAccessFunc(UserGkey, "ss180_NEW");
        hh_GridCtrl.Value = "ins";
        Set_Control();
        ClearText();
        SetEditMod();
        //定義guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(false, 0);
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_dr_es101_no').focus(); </script>";
      }
    }

    protected void gr_GridView_ss101_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_ss101.PageIndex = e.NewPageIndex;
      Session["fm_ss180_gr_GridView_ss101_PageIndex"] = gr_GridView_ss101.PageIndex;
      hh_GridGkey.Value = gr_GridView_ss101.DataKeys[gr_GridView_ss101.SelectedIndex].Value.ToString(); //ss101_gkey
    }

    protected void gr_GridView_ss101_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_ss101.Enabled)
      {
        SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true, 0);
        Session["fm_ss180_gr_GridView_ss101_PageIndex"] = gr_GridView_ss101.PageIndex;
        Session["fm_ss180_gr_GridView_ss101_SelectedIndex"] = gr_GridView_ss101.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }
    protected void gr_GridView_ss101_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true, 0);
      Session["fm_ss180_gr_GridView_ss101_PageIndex"] = gr_GridView_ss101.PageIndex;
      Session["fm_ss180_gr_GridView_ss101_SelectedIndex"] = gr_GridView_ss101.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_ss101.DataKeys[gr_GridView_ss101.SelectedIndex].Value.ToString(); //ss101_gkey
      SetSerMod();
    }

    protected void bt_DEL_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
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
        DAC_ss180 ss180Dao = new DAC_ss180(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "ss101_gkey='" + hh_GridGkey.Value + "'";
        DataTable tb_ss101 = new DataTable();
        DbDataAdapter da_ADP = ss180Dao.GetDataAdapter("YN01", "UNss101", "ss101", st_addselect, false, st_addjoin, CmdQueryS_ss101, st_addunion, "");
        da_ADP.Fill(tb_ss101);
        DataRow[] dr_DelRow = tb_ss101.Select(st_SelDataKey);
        if (dr_DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            dr_DelRow[0].Delete();
            da_ADP.Update(tb_ss101);
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
            ss180Dao.Dispose();
            tb_ss101.Dispose();
            da_ADP.Dispose();
            conn.Close();
          }
        }
        tb_ss101.Clear();
        if (bl_delok)
        {
          gr_GridView_ss101 = clsGV.SetGridCursor("del", gr_GridView_ss101, -2);
        }
        //
        SetSerMod();
        BindNew(true, 0);
      }
    }

    protected void bt_SAV_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actSAV();
    }
    //
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
        DAC_ss180 ss180Dao = new DAC_ss180(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {
          //if (UpdateDataAllx(hh_ActKey.Value, UserGkey, ref st_dberrmsg))
          //{
          //  SetSerMod();
          //  hh_GridCtrl.Value = "rekey";
          //  BindNew(true, 0);
          //  hh_GridCtrl.Value = "ser";
          //}
          //else
          //{
          //  lb_ErrorMessage.Visible = true;
          //  lb_ErrorMessage.Text = st_dberrmsg;
          //}
        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "ss101_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //檢查重複
            if (ss180Dao.IsExists("ss101", "es101gkey", dr_es101_no.Items[dr_es101_no.SelectedIndex].Value, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(dr_es101_no.Items[dr_es101_no.SelectedIndex].Text + ",已存在.");
            }
            else
            {
              DataTable tb_ss101_ins = new DataTable();
              DbDataAdapter da_ADP_ins = ss180Dao.GetDataAdapter("YN01", "UNss101", "ss101", st_addselect, false, st_addjoin, CmdQueryS_ss101, st_addunion, "");
              da_ADP_ins.Fill(tb_ss101_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_ss101_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["ss101_gkey"] = st_tempgkey;      // 
                ins_row["ss101_usercode"] = tx_ss101_usercode.Text;      //帳號
                ins_row["ss101_password"] = tx_ss101_password.Text;      //密碼
                ins_row["ss101_es101gkey"] = dr_es101_no.Items[dr_es101_no.SelectedIndex].Value;        //系統使用
                ins_row["ss101_usertype"] = dr_ss101_usertype.SelectedValue;       // 帳號類別
                ins_row["ss101_mkey"] = DAC.get_guidkey();        //
                ins_row["ss101_trusr"] = UserName;  //
                tb_ss101_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_ss101_ins);
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
                ss180Dao.Dispose();
                tb_ss101_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_GridCtrl.Value = "rekey";
              BindNew(true, 0);
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
            if (ss180Dao.IsExists("ss101", "es101gkey", dr_es101_no.Items[dr_es101_no.SelectedIndex].Value, " gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(dr_es101_no.Items[dr_es101_no.SelectedIndex].Text + ",已存在.");
            }
            else
            {
              DataTable tb_ss101_mod = new DataTable();
              DbDataAdapter da_ADP_mod = ss180Dao.GetDataAdapter("YN01", "UNss101", "ss101", st_addselect, false, st_addjoin, CmdQueryS_ss101, st_addunion, "");
              da_ADP_mod.Fill(tb_ss101_mod);
              st_SelDataKey = "ss101_gkey='" + hh_GridGkey.Value + "'";
              DataRow[] mod_rows = tb_ss101_mod.Select(st_SelDataKey);
              DataRow mod_row;
              if (mod_rows.Length != 1)
              {
                bl_updateok = false;
                st_dberrmsg = StringTable.GetString("資料選取不正確.");
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
                  mod_row["ss101_usercode"] = tx_ss101_usercode.Text;      //帳號
                  mod_row["ss101_password"] = tx_ss101_password.Text;      //密碼
                  mod_row["ss101_es101gkey"] = dr_es101_no.Items[dr_es101_no.SelectedIndex].Value;        //系統使用
                  mod_row["ss101_usertype"] = dr_ss101_usertype.SelectedValue;       // 帳號類別
                  mod_row["ss101_mkey"] = DAC.get_guidkey();        //
                  mod_row["ss101_trusr"] = UserName;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_ss101_mod);
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
                  ss180Dao.Dispose();
                  tb_ss101_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_GridCtrl.Value = "rekey";
              BindNew(true, 0);
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
        ss180Dao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }
    }

    protected void gr_GridView_sys_ss180_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //作業編號             
        //if (e.Row.FindControl("tx_sys_menu_obj_name_02") != null)
        //{
        //  TextBox tx_sys_menu_obj_name_02 = e.Row.FindControl("tx_sys_menu_obj_name_02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["sys_menu_obj_name_"]).Trim();
        //  tx_sys_menu_obj_name_02.Text = st_datavalue;
        //  clsGV.TextBox_Set(ref tx_sys_menu_obj_name_02, false);
        //}

        //作業名稱
        //if (e.Row.FindControl("tx_sys_menu_chinesesimp_02") != null)
        //{
        //  TextBox tx_sys_menu_chinesesimp_02 = e.Row.FindControl("tx_sys_menu_chinesesimp_02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["sys_menu_chinesesimp_"]).Trim();
        //  tx_sys_menu_chinesesimp_02.Text = st_datavalue;
        //  clsGV.TextBox_Set(ref tx_sys_menu_chinesesimp_02, false);
        //}
        //1=有權限
        if (e.Row.FindControl("ck_sys_ss180_mark02") != null)
        {
          CheckBox ck_sys_ss180_mark02 = e.Row.FindControl("ck_sys_ss180_mark02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_ss180_mark"]).Trim();
          if (st_datavalue == "1") ck_sys_ss180_mark02.Checked = true; else ck_sys_ss180_mark02.Checked = false;
          //ck_sys_ss180_mark02.Enabled = true;
          ck_sys_ss180_mark02.Enabled = st_accmod;

        }
        ////按鈕編號
        //if (e.Row.FindControl("tx_sys_ss160_buttonno02") != null)
        //{
        //  TextBox tx_sys_ss160_buttonno02 = e.Row.FindControl("tx_sys_ss160_buttonno02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["sys_ss160_buttonno"]).Trim();
        //  tx_sys_ss160_buttonno02.Text = st_datavalue;
        //  clsGV.TextBox_Set(ref tx_sys_ss160_buttonno02, false);
        //}
        ////按鈕名稱
        //if (e.Row.FindControl("tx_sys_ss160_button02") != null)
        //{
        //  TextBox tx_sys_ss160_button02 = e.Row.FindControl("tx_sys_ss160_button02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["sys_ss160_button"]).Trim();
        //  tx_sys_ss160_button02.Text = st_datavalue;
        //  clsGV.TextBox_Set(ref tx_sys_ss160_button02, false);
        //}
      }
      sFN.Dispose();
    }
    //
    protected bool UpdateDataAll_ss180(string st_ActKey, string st_es101gkey, string st_obj_code, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_sys_ss180_gkey = "", st_sys_ss180_mark = "", st_sys_ss160_buttonno = "", st_sys_ss160_button = "", st_sys_menu_obj_name = "", st_sys_menu_chinesesimp = "", st_sys_ss180_ss160gkey = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView_sys_ss180$ctl";
      //
      CmdQueryS_ss180.Parameters.Clear();
      CmdQueryS_ss180.CommandText = " and m.obj_name in (select obj_name from sys_menu where parent_code=? or obj_name=? )  and a.es101gkey=? ";
      DAC.AddParam(CmdQueryS_ss180, "parent_code", st_obj_code);
      DAC.AddParam(CmdQueryS_ss180, "obj_name", st_obj_code);
      DAC.AddParam(CmdQueryS_ss180, "es101gkey", st_es101gkey);
      //
      DataTable tb_sys_ss180 = new DataTable();
      DAC_ss180 sys_ss180Dao = new DAC_ss180(conn);
      string st_addselect = "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      string st_addjoin = "";
      string st_addunion = "";
      st_addunion += "select '0' as sys_ss180_mark ,a.buttonno as sys_ss160_buttonno ,a.button as sys_ss160_button ,a.button_e as sys_ss160_button_e ,a.button_t as sys_ss160_button_t ,a.button_c as sys_ss160_button_c ,a.button_v as sys_ss160_button_v ,a.tip_e as sys_ss160_tip_e ,a.tip_t as sys_ss160_tip_t ,a.tip_c as sys_ss160_tip_c ,a.tip_v as sys_ss160_tip_v ,";
      //st_addunion += "m.prg_code as sys_menu_prg_code,m.obj_name as sys_menu_obj_name,m.chinesesimpname as sys_menu_chinesesimpname,";
      st_addunion += "m.prg_code as sys_menu_prg_code,m.obj_name as sys_menu_obj_name,";
      st_addunion += "m.chinesesimpname as sys_menu_chinesesimpname,";
      st_addunion += "m.chinesebigname  as sys_menu_chinesebigname,";
      st_addunion += "m.englishname     as sys_menu_englishname,";
      st_addunion += "m.vietnamname     as sys_menu_vietnamname,";
      //
      st_addunion += "'" + st_es101gkey + "' as  sys_ss180_es101gkey ,a.gkey as  sys_ss180_ss160gkey ,'*' as sys_ss180_gkey,a.mkey as sys_ss180_mkey ,a.trcls as sys_ss180_trcls,a.trcrd as sys_ss180_trcrd,a.trmod as sys_ss180_trmod,a.trusr as sys_ss180_trusr,";
      st_addunion += "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      st_addunion += "from sys_ss160 a ";
      st_addunion += "left outer join sys_menu m on a.ss155gkey=m.obj_name ";
      st_addunion += "where a.ss155gkey in (select obj_name from sys_menu where parent_code='" + st_tree_gkey + "' or obj_name='" + st_tree_gkey + "' ) " + " and a.gkey not in (select ss160gkey from sys_ss180 where es101gkey='" + hh_es101gkey.Value + "') ";
      //st_addunion += "where a.ss155gkey='" + st_tree_gkey + "' and a.gkey not in (select ss160gkey from sys_ss180 where es101gkey='" + hh_GridGkey.Value + "') ";
      DbDataAdapter da_ADP = sys_ss180Dao.GetDataAdapter("YN01", "UNsys_ss180", "sys_ss180", st_addselect, false, st_addjoin, CmdQueryS_ss180, st_addunion, "sys_menu_prg_code,sys_ss160_buttonno");
      da_ADP.Fill(tb_sys_ss180);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_sys_ss180.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss180_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss180_gkey02";
            st_sys_ss180_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //1=有權限
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_sys_ss180_mark02";
            st_sys_ss180_mark = DAC.GetStringValueBool(((CheckBox)FindControl(st_ctrlname)).Checked);
            //按鈕編號
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_buttonno02";
            st_sys_ss160_buttonno = ((Label)FindControl(st_ctrlname)).Text.Trim();
            //按鈕名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_button02";
            st_sys_ss160_button = ((Label)FindControl(st_ctrlname)).Text.Trim();
            //ss160_gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss180_ss160gkey02";
            st_sys_ss180_ss160gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //obj_name
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss180_ss160gkey02";
            st_sys_menu_obj_name = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //chinesesimpname
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_menu_chinesesimp_02";
            st_sys_menu_chinesesimp = ((Label)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_sys_ss180.Select("sys_ss180_gkey='" + st_sys_ss180_gkey + "'");
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (DAC.GetStringValue(mod_row["sys_ss180_mark"]) != st_sys_ss180_mark)
              {
                sys_ss180Dao.Insertbalog(conn, thistran, "sys_ss180", st_ActKey, UserName);
                sys_ss180Dao.Insertbtlog(conn, thistran, "sys_ss180", DAC.GetStringValue(mod_row["sys_ss180_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["sys_ss180_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_ss180_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_ss180_gkey"]));
                mod_row.BeginEdit();
                mod_row["sys_ss180_mark"] = st_sys_ss180_mark;        //1=有權限
                mod_row["sys_ss180_mkey"] = DAC.get_guidkey();         //
                mod_row["sys_ss180_trusr"] = UserName;  //
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();
                //
              }
            }  //sel_rows.Length == 1
            else
            {
              mod_row = tb_sys_ss180.NewRow();
              //
              mod_row["sys_ss180_mark"] = st_sys_ss180_mark;        //1=有權限
              mod_row["sys_ss180_ss160gkey"] = st_sys_ss180_ss160gkey;      //ss160gkey
              //mod_row["sys_ss180_menu_obj_name"] = st_sys_menu_obj_name;      //menu_obj_name
              mod_row["sys_ss180_es101gkey"] = st_es101gkey;      //使用者gkey
              //
              mod_row["sys_ss180_gkey"] = DAC.get_guidkey();
              mod_row["sys_ss180_mkey"] = DAC.get_guidkey();
              mod_row["sys_ss180_trusr"] = UserName;
              tb_sys_ss180.Rows.Add(mod_row);
              sys_ss180Dao.Insertbalog(conn, thistran, "sys_ss180", st_ActKey, UserName);
              sys_ss180Dao.Insertbtlog(conn, thistran, "sys_ss180", DAC.GetStringValue(mod_row["sys_ss180_gkey"]), "I", UserName, st_sys_ss160_buttonno + " " + st_sys_ss160_button);
              st_ActKey = DAC.get_guidkey();

            }  //sel_rows.Length == 0 insert
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_sys_ss180);
        thistran.Commit();
        bl_updateok = true;
        //
        hh_GridCtrl.Value = "ser";
      }  //try
      catch (Exception e)
      {
        thistran.Rollback();
        bl_updateok = false;
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = e.Message;
      }
      finally
      {
        thistran.Dispose();
        sys_ss180Dao.Dispose();
        tb_sys_ss180.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;

    }

    protected void bn_ACC_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actACC();
    }

    protected void actACC()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        string st_dberrmsg = "";
        //
        Set_Control();
        DAC_ss180 sys_ss180Dao = new DAC_ss180(conn);
        hh_ActKey.Value = DAC.get_guidkey();
        if (UpdateDataAll_ss180(hh_ActKey.Value, hh_es101gkey.Value, hh_treekey.Value, ref st_dberrmsg))
        {
          SetSerMod();
          BindNew_ss180(hh_GridGkey.Value, hh_treekey.Value);
        }
        else
        {
          lb_ErrorMessage.Visible = true;
          lb_ErrorMessage.Text = st_dberrmsg;
        }
        sys_ss180Dao.Dispose();
      }
    }

  }
}