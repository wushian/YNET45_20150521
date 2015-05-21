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
  public partial class fm_ss160 : FormBase
  {

    public string st_gkey = "";
    //OleDbCommand CmdQueryS = DAC.NewCommand();
    string st_object_func = "sys_ss160";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";

    protected void bt_01_Click(object sender, EventArgs e)
    {
      lb_ErrorMessage.Text = "test~";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      li_Msg.Text = "";
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        st_gkey = Server.HtmlDecode(DAC.GetStringValue(Request["obj_name"]));
        hh_qkey.Value = st_gkey;
        if (!IsPostBack)
        {
          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmSS160_CmdQueryS"] = CmdQueryS;
          Set_Control();
          SetSerMod();
          BindNew(true);
        }
        else
        {
          if ((hh_GridCtrl.Value.ToString().ToLower() == "ins") || (hh_GridCtrl.Value.ToString().ToLower() == "mod") || (hh_GridCtrl.Value.ToString().ToLower() == "modall"))
          {
            //BindNew(false);
          }
          else
          {
            //BindNew(true);
          }
        }
        TreeView1.Nodes.Clear();
        TreeView1 = sFN.SetTreeView_sys_menu(TreeView1, "fm_ss160.aspx?obj_name=", LangType, UserId);
        TreeView1.CollapseAll();
        if (st_gkey != "") sFN.TreeViewExpandByValue(ref TreeView1, st_gkey);
      }
    }

    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      gr_GridView1.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNsys_ss160", "sys_ss160");
    }

    private void ClearText()
    {
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      if ((UserId == "Admin") && (bt_04.Visible == false))
      {
        sFN.SetLinkButton(this, "bt_04", PublicVariable.LangType, st_ContentPlaceHolder, "M更正", true);
      }
      //
      gr_GridView1.Enabled = true;
      gr_GridView1.Columns[0].Visible = true;
    }

    private void SetEditMod()
    {
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //

      gr_GridView1.Enabled = false;
      gr_GridView1.Columns[0].Visible = true;
    }

    private void SetEditModALL()
    {
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);


      //
      gr_GridView1.Enabled = true;
      //gr_GridView1.Columns[0].Visible = false;
    }

    private void BindText(DataRow dr_CurRow)
    {
      clsFN sFN = new clsFN();
      //
      hh_mkey.Value = DAC.GetStringValue(dr_CurRow["sys_ss160_mkey"]);
      //
      sFN.Dispose();
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow, AddRow;
      //
      CmdQueryS.CommandText = " and a.ss155gkey='" + hh_qkey.Value + "' ";
      DataTable tb_ss160 = new DataTable();
      DAC_ss160 ss160Dao = new DAC_ss160(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      int in_chkcount = 0;
      string st_addselect = @" '1' as sys_ss160_checked ";
      string st_addjoin = "";
      string st_addunion = @"select a.buttonno as  sys_ss160_buttonno ,a.button as  sys_ss160_button ,a.button_e as  sys_ss160_button_e ,a.button_t as  sys_ss160_button_t ,a.button_c as  sys_ss160_button_c ,a.button_v as  sys_ss160_button_v ,a.tip_e as  sys_ss160_tip_e ,a.tip_t as  sys_ss160_tip_t ,a.tip_c as  sys_ss160_tip_c ,a.tip_v as  sys_ss160_tip_v ,a.ss155gkey as  sys_ss160_ss155gkey ,a.gkey as sys_ss160_gkey,a.mkey as sys_ss160_mkey ,a.trcls as sys_ss160_trcls,a.trcrd as sys_ss160_trcrd,a.trmod as sys_ss160_trmod,a.trusr as sys_ss160_trusr, '0' as sys_ss160_checked  
                             from sys_ss160_s a  with (nolock)  where 1=1  and a.buttonno not in (select buttonno from sys_ss160 where  ss155gkey='" + hh_qkey.Value + "'   )";
      ad_DataDataAdapter = ss160Dao.GetDataAdapter("YN01", "UNsys_ss160", "sys_ss160", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "sys_ss160_buttonno");
      ad_DataDataAdapter.Fill(tb_ss160);
      //
      for (int vr = 0; vr < tb_ss160.Rows.Count; vr++)
      {
        AddRow = tb_ss160.Rows[vr];
        if (DAC.GetStringValue(AddRow["sys_ss160_checked"]) == "0")
        {
          AddRow.BeginEdit();
          AddRow["sys_ss160_checked"] = "0";
          AddRow["sys_ss160_gkey"] = DAC.get_guidkey();
          AddRow["sys_ss160_ss155gkey"] = hh_qkey.Value;
          AddRow["sys_ss160_mkey"] = DAC.get_guidkey();
          AddRow["sys_ss160_trcls"] = "insert";
          AddRow["sys_ss160_trcrd"] = DateTime.Now;
          AddRow["sys_ss160_trmod"] = DateTime.Now;
          AddRow["sys_ss160_trusr"] = "Admin";
          AddRow.EndEdit();
        }
        else
        {
          in_chkcount += 1;
        }
      }
      //
      //if (in_chkcount == 0)
      //{
      //  tb_ss160.Rows.Clear(); 
      //}
      if (tb_ss160.Rows.Count > 0)
      {
        //bt_05.OnClientClick = "return btnDEL_c()";
        //bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        //bt_05.OnClientClick = "return btnDEL0_c()";
        //bt_04.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView1.DataSource = tb_ss160;
      gr_GridView1 = clsGV.BindGridView(gr_GridView1, tb_ss160, hh_GridCtrl, ref hh_GridGkey, "fmss160_GV1");
      gr_GridView1.DataBind();
      SelDataKey = "sys_ss160_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_ss160.Select(SelDataKey);
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
      tb_ss160.Dispose();
      ss160Dao.Dispose();
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

    protected void bt_04_Click(object sender, EventArgs e)
    {
      actMODALL();
    }

    protected void actMODALL()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "modall";
        Set_Control();
        SetEditModALL();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(true);
      }
    }

    protected void bt_QUT_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Master/" + Page.Theme + "/MainForm.aspx");
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
    //
    protected void bt_DEL_Click(object sender, EventArgs e)
    {
      actDEL();
    }
    //
    protected void actDEL()
    {
    }

    protected void gr_GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        if (e.Row.FindControl("ck_sys_ss160_checked") != null)
        {
          DataRowView rowView = (DataRowView)e.Row.DataItem;
          //使用
          if (e.Row.FindControl("ck_sys_ss160_checked") != null)
          {
            CheckBox ck_sys_ss160_checked = e.Row.FindControl("ck_sys_ss160_checked") as CheckBox;
            st_datavalue = DAC.GetStringValue(rowView["sys_ss160_checked"]).Trim();
            if (st_datavalue == "1") ck_sys_ss160_checked.Checked = true; else ck_sys_ss160_checked.Checked = false;
            if (hh_GridCtrl.Value == "modall") { ck_sys_ss160_checked.Enabled = true; } else { ck_sys_ss160_checked.Enabled = false; }
          }
          //編號
          if (e.Row.FindControl("tx_sys_ss160_buttonno02") != null)
          {
            TextBox tx_sys_ss160_buttonno02 = e.Row.FindControl("tx_sys_ss160_buttonno02") as TextBox;
            tx_sys_ss160_buttonno02.Text = DAC.GetStringValue(rowView["sys_ss160_buttonno"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_buttonno02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_buttonno02, false); }
            if (DAC.GetInt16Value(tx_sys_ss160_buttonno02.Text) <= 10) clsGV.TextBox_Set(ref tx_sys_ss160_buttonno02, false);
          }
          //按鈕名稱
          if (e.Row.FindControl("tx_sys_ss160_button02") != null)
          {
            TextBox tx_sys_ss160_button02 = e.Row.FindControl("tx_sys_ss160_button02") as TextBox;
            tx_sys_ss160_button02.Text = DAC.GetStringValue(rowView["sys_ss160_button"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_button02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_button02, false); }
          }
          //英文
          if (e.Row.FindControl("tx_sys_ss160_button_e02") != null)
          {
            TextBox tx_sys_ss160_button_e02 = e.Row.FindControl("tx_sys_ss160_button_e02") as TextBox;
            tx_sys_ss160_button_e02.Text = DAC.GetStringValue(rowView["sys_ss160_button_e"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_button_e02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_button_e02, false); }
          }
          //繁體
          if (e.Row.FindControl("tx_sys_ss160_button_t02") != null)
          {
            TextBox tx_sys_ss160_button_t02 = e.Row.FindControl("tx_sys_ss160_button_t02") as TextBox;
            tx_sys_ss160_button_t02.Text = DAC.GetStringValue(rowView["sys_ss160_button_t"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_button_t02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_button_t02, false); }
          }
          //簡體
          if (e.Row.FindControl("tx_sys_ss160_button_c02") != null)
          {
            TextBox tx_sys_ss160_button_c02 = e.Row.FindControl("tx_sys_ss160_button_c02") as TextBox;
            tx_sys_ss160_button_c02.Text = DAC.GetStringValue(rowView["sys_ss160_button_c"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_button_c02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_button_c02, false); }
          }
          //越南
          if (e.Row.FindControl("tx_sys_ss160_button_v02") != null)
          {
            TextBox tx_sys_ss160_button_v02 = e.Row.FindControl("tx_sys_ss160_button_v02") as TextBox;
            tx_sys_ss160_button_v02.Text = DAC.GetStringValue(rowView["sys_ss160_button_v"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_button_v02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_button_v02, false); }
          }
          //英文Tip
          if (e.Row.FindControl("tx_sys_ss160_tip_e02") != null)
          {
            TextBox tx_sys_ss160_tip_e02 = e.Row.FindControl("tx_sys_ss160_tip_e02") as TextBox;
            tx_sys_ss160_tip_e02.Text = DAC.GetStringValue(rowView["sys_ss160_tip_e"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_tip_e02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_tip_e02, false); }
          }
          //繁體Tip
          if (e.Row.FindControl("tx_sys_ss160_tip_t02") != null)
          {
            TextBox tx_sys_ss160_tip_t02 = e.Row.FindControl("tx_sys_ss160_tip_t02") as TextBox;
            tx_sys_ss160_tip_t02.Text = DAC.GetStringValue(rowView["sys_ss160_tip_t"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_tip_t02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_tip_t02, false); }
          }
          //簡體Tip
          if (e.Row.FindControl("tx_sys_ss160_tip_c02") != null)
          {
            TextBox tx_sys_ss160_tip_c02 = e.Row.FindControl("tx_sys_ss160_tip_c02") as TextBox;
            tx_sys_ss160_tip_c02.Text = DAC.GetStringValue(rowView["sys_ss160_tip_c"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_tip_c02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_tip_c02, false); }
          }
          //越南Tip
          if (e.Row.FindControl("tx_sys_ss160_tip_v02") != null)
          {
            TextBox tx_sys_ss160_tip_v02 = e.Row.FindControl("tx_sys_ss160_tip_v02") as TextBox;
            tx_sys_ss160_tip_v02.Text = DAC.GetStringValue(rowView["sys_ss160_tip_v"]).Trim();
            if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss160_tip_v02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss160_tip_v02, false); }
          }
        }
      }
      sFN.Dispose();
    }
    //
    protected void bt_SAV_Click(object sender, EventArgs e)
    {
      actSAV();
    }
    //
    protected void actSAV()
    {
      string st_ckerrmsg = "";
      string st_dberrmsg = "";
      Set_Control();
      if (ServerEditCheck(ref st_ckerrmsg))
      {
        DAC_ss160 ss160Dao = new DAC_ss160(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {
          if (UpdateDataAll(hh_ActKey.Value, UserGkey, ref st_dberrmsg))
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
        }
        ss160Dao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }
    }

    protected bool UpdateDataAll(string st_ActKey, string ss101gkey1, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_sys_ss160_gkey = "", st_sys_ss160_mkey = "", st_sys_ss160_checked = "", st_sys_ss160_button = "", st_sys_ss160_button_e = "", st_sys_ss160_button_t = "", st_sys_ss160_button_c = "", st_sys_ss160_button_v = "", st_sys_ss160_tip_e = "", st_sys_ss160_tip_t = "", st_sys_ss160_tip_c = "", st_sys_ss160_tip_v = "";
      Int16 in_sys_ss160_buttonno = 0;
      //
      DataRow mod_row;
      DataRow[] sel_rows;
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView1$ctl";
      CmdQueryS.CommandText = " and a.ss155gkey='" + hh_qkey.Value + "' ";
      DataTable tb_ss160 = new DataTable();
      DAC_ss160 ss160Dao = new DAC_ss160(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";

      DbDataAdapter da_ADP = ss160Dao.GetDataAdapter("YN01", "UNsys_ss160", "sys_ss160", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "sys_ss160_buttonno");
      da_ADP.Fill(tb_ss160);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView1.Rows.Count + 10; in_g++)
        {
          //gkey

          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_gkey02";
            st_sys_ss160_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //mkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_mkey02";
            st_sys_ss160_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //
            //使用
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_sys_ss160_checked";
            st_sys_ss160_checked = DAC.GetStringValueBool(((CheckBox)FindControl(st_ctrlname)).Checked);
            //編號
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_buttonno02";
            in_sys_ss160_buttonno = DAC.GetInt16Value(((TextBox)FindControl(st_ctrlname)).Text);
            //按鈕名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_button02";
            st_sys_ss160_button = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //英文
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_button_e02";
            st_sys_ss160_button_e = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //繁體
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_button_t02";
            st_sys_ss160_button_t = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //簡體
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_button_c02";
            st_sys_ss160_button_c = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //越南
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_button_v02";
            st_sys_ss160_button_v = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //英文Tip
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_tip_e02";
            st_sys_ss160_tip_e = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //繁體Tip
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_tip_t02";
            st_sys_ss160_tip_t = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //簡體Tip
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_tip_c02";
            st_sys_ss160_tip_c = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //越南Tip
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_tip_v02";
            st_sys_ss160_tip_v = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
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
            sel_rows = tb_ss160.Select("sys_ss160_gkey='" + st_sys_ss160_gkey + "'");
            if (sel_rows.Length == 0)
            {
              if (st_sys_ss160_checked == "1")
              { //原沒有資料 起用
                mod_row = tb_ss160.NewRow();
                mod_row["sys_ss160_buttonno"] = in_sys_ss160_buttonno;
                mod_row["sys_ss160_button"] = st_sys_ss160_button;
                mod_row["sys_ss160_button_e"] = st_sys_ss160_button_e;
                mod_row["sys_ss160_button_t"] = st_sys_ss160_button_t;
                mod_row["sys_ss160_button_c"] = st_sys_ss160_button_c;
                mod_row["sys_ss160_button_v"] = st_sys_ss160_button_v;
                mod_row["sys_ss160_tip_e"] = st_sys_ss160_tip_e;
                mod_row["sys_ss160_tip_t"] = st_sys_ss160_tip_t;
                mod_row["sys_ss160_tip_c"] = st_sys_ss160_tip_c;
                mod_row["sys_ss160_tip_v"] = st_sys_ss160_tip_v;
                mod_row["sys_ss160_ss155gkey"] = hh_qkey.Value;
                //
                mod_row["sys_ss160_gkey"] = DAC.get_guidkey();
                mod_row["sys_ss160_mkey"] = DAC.get_guidkey();
                mod_row["sys_ss160_trusr"] = UserName;
                tb_ss160.Rows.Add(mod_row);
                ss160Dao.Insertbalog(conn, thistran, "sys_ss160", st_ActKey, UserName);
                ss160Dao.Insertbtlog(conn, thistran, "sys_ss160", DAC.GetStringValue(mod_row["sys_ss160_gkey"]), "I", UserName, DAC.GetStringValue(mod_row["sys_ss160_buttonno"]) + " " + DAC.GetStringValue(mod_row["sys_ss160_button"]));
                st_ActKey = DAC.get_guidkey();
              }
            }
            else if (sel_rows.Length == 1)
            {
              if (st_sys_ss160_checked == "1")
              { //原有資料 起用 有更正
                mod_row = sel_rows[0];
                if ((DAC.GetInt16Value(mod_row["sys_ss160_buttonno"]) != in_sys_ss160_buttonno)
                    || (DAC.GetStringValue(mod_row["sys_ss160_button"]) != st_sys_ss160_button)
                    || (DAC.GetStringValue(mod_row["sys_ss160_button_e"]) != st_sys_ss160_button_e)
                    || (DAC.GetStringValue(mod_row["sys_ss160_button_t"]) != st_sys_ss160_button_t)
                    || (DAC.GetStringValue(mod_row["sys_ss160_button_c"]) != st_sys_ss160_button_c)
                    || (DAC.GetStringValue(mod_row["sys_ss160_button_v"]) != st_sys_ss160_button_v)
                    || (DAC.GetStringValue(mod_row["sys_ss160_tip_e"]) != st_sys_ss160_tip_e)
                    || (DAC.GetStringValue(mod_row["sys_ss160_tip_t"]) != st_sys_ss160_tip_t)
                    || (DAC.GetStringValue(mod_row["sys_ss160_tip_c"]) != st_sys_ss160_tip_c)
                    || (DAC.GetStringValue(mod_row["sys_ss160_tip_v"]) != st_sys_ss160_tip_v))
                {
                  mod_row.BeginEdit();
                  ss160Dao.Insertbalog(conn, thistran, "sys_ss160", st_ActKey, UserName);
                  ss160Dao.Insertbtlog(conn, thistran, "sys_ss160", DAC.GetStringValue(mod_row["sys_ss160_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["sys_ss160_buttonno"]) + " " + DAC.GetStringValue(mod_row["sys_ss160_button"]));
                  st_ActKey = DAC.get_guidkey();
                  //
                  //mod_row["sys_ss160_checked"] = st_sys_ss160_checked;
                  mod_row["sys_ss160_buttonno"] = in_sys_ss160_buttonno;
                  mod_row["sys_ss160_button"] = st_sys_ss160_button;
                  mod_row["sys_ss160_button_e"] = st_sys_ss160_button_e;
                  mod_row["sys_ss160_button_t"] = st_sys_ss160_button_t;
                  mod_row["sys_ss160_button_c"] = st_sys_ss160_button_c;
                  mod_row["sys_ss160_button_v"] = st_sys_ss160_button_v;
                  mod_row["sys_ss160_tip_e"] = st_sys_ss160_tip_e;
                  mod_row["sys_ss160_tip_t"] = st_sys_ss160_tip_t;
                  mod_row["sys_ss160_tip_c"] = st_sys_ss160_tip_c;
                  mod_row["sys_ss160_tip_v"] = st_sys_ss160_tip_v;
                  //
                  mod_row["sys_ss160_gkey"] = st_sys_ss160_gkey;
                  mod_row["sys_ss160_mkey"] = DAC.get_guidkey();
                  mod_row.EndEdit();
                  //
                }
              }
              else
              { //原有資料 不啟用 刪除
                mod_row = sel_rows[0];
                ss160Dao.Insertbalog(conn, thistran, "sys_ss160", st_ActKey, UserName);
                ss160Dao.Insertbtlog(conn, thistran, "sys_ss160", DAC.GetStringValue(mod_row["sys_ss160_gkey"]), "D", UserName, DAC.GetStringValue(mod_row["sys_ss160_buttonno"]) + " " + DAC.GetStringValue(mod_row["sys_ss160_button"]));
                st_ActKey = DAC.get_guidkey();
                mod_row.Delete();
              }
              //
            } //sel_rows.Length == 1
          } //bl_Mod

        }
        //
        da_ADP.Update(tb_ss160);
        //
        thistran.Commit();
        bl_updateok = true;
      }
      catch (Exception e)
      {
        thistran.Rollback();
        bl_updateok = false;
        st_errmsg = e.Message;
      }
      finally
      {
        thistran.Dispose();
        ss160Dao.Dispose();
        tb_ss160.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }

  }
}