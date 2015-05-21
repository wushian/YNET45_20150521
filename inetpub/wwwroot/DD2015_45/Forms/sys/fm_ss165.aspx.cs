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
  public partial class fm_ss165 : FormBase
  {

    OleDbCommand CmdQueryS_ss165 = DAC.NewCommand();
    OleDbCommand CmdQueryS_ss175 = DAC.NewCommand();
    OleDbCommand CmdQueryS_ss170 = DAC.NewCommand();
    public string st_tree_gkey = "";
    public string st_tree_act_type = "";
    bool st_accmod = false;
    //
    string st_object_func = "sys_ss165";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    //int in_PageSize = 10;
    //
    protected void Page_Load(object sender, EventArgs e)
    {
      //檢查Db Session狀態

      li_Msg.Text = "";
      li_AccMsg.Text = "";
      //檢查權限&狀態
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        st_accmod = sFN.chkAccessFunc_(UserGkey, st_object_func, 4, UserLoginGkey);
        if (UserGkey == "Admin") { st_accmod = true; }
        bn_ACC.Visible = st_accmod;
        //
        st_tree_gkey = Server.HtmlDecode(DAC.GetStringValue(Request["obj_name"]));
        hh_treekey.Value = st_tree_gkey;
        //
        if (!IsPostBack)
        {
          CmdQueryS_ss165.CommandText = " AND 1=1 ";
          Session["fmsys_ss165_CmdQueryS"] = CmdQueryS_ss165;
          //
          dr_es101_no = sFN.DropDownListFromTable(ref dr_es101_no, "es101", "gkey", "no+cname", "", "no");
          dr_sys_ss165_usertype = sFN.DropDownListFromClasses(ref dr_sys_ss165_usertype, "es101_usertype", "class_text", "class_value");
          //
          try
          {
            st_tree_act_type = Server.HtmlDecode(DAC.GetStringValue(Request["act_type"]));
            hh_tree_act_type.Value = st_tree_act_type;
            if (st_tree_gkey != "")
            {
              gr_GridView_sys_ss165.PageIndex = DAC.GetInt16Value(Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"]);
              gr_GridView_sys_ss165.SelectedIndex = DAC.GetInt16Value(Session["fmsys_ss165_gr_GridView_sys_ss165_SelectedIndex"]);
              hh_GridGkey_ss165.Value = DAC.GetStringValue(Session["fmsys_ss165_gr_GridView_sys_ss165_GridGkey"]);
            }
          }
          catch
          {
            hh_tree_act_type.Value = "";
          }
          Set_Control_ss165();
          Set_Control_ss175();
          SetSerMod_ss175();
          SetDisable_ss175();
          BindNew_ss165(true);
          SetSerMod_ss165();
          //
          Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"] = gr_GridView_sys_ss165.PageIndex;
          Session["fmsys_ss165_gr_GridView_sys_ss165_SelectedIndex"] = gr_GridView_sys_ss165.SelectedIndex;
          //
        }
        else
        {
          if ((hh_GridCtrl_ss165.Value.ToString().ToLower() == "ins") || (hh_GridCtrl_ss165.Value.ToString().ToLower() == "mod"))
          {
            BindNew_ss165(false);
          }
          else
          {
            //BindNew_ss165(true);
            SetSerMod_ss165();
          }
        }
        tr_funcView.Nodes.Clear();
        tr_funcView = sFN.SetTreeView_sys_menu(tr_funcView, "fm_ss165.aspx?act_type=a&obj_name=", LangType, UserId);
        tr_funcView.CollapseAll();
        if (st_tree_gkey != "") sFN.TreeViewExpandByValue(ref tr_funcView, st_tree_gkey);
      }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
      if ((hh_GridCtrl_ss165.Value.ToString().ToLower() == "ins") || (hh_GridCtrl_ss165.Value.ToString().ToLower() == "mod"))
      {
        BindNew_ss165(false);
      }
      else
      {
        BindNew_ss165(true);
        SetSerMod_ss165();
      }

    }

    private void Set_Control_ss165()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      gr_GridView_sys_ss165.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      gr_GridView_sys_ss175.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNsys_ss165", "sys_ss165");
    }

    private void ClearText_ss165()
    {
      tx_sys_ss165_groupno.Text = "";  //群組編號
      tx_sys_ss165_groupname.Text = "";  //群組名稱
      dr_sys_ss165_usertype.SelectedIndex = -1;  //帳號類別

      //
      hh_mkey_ss165.Value = "";
    }

    private void SetDisable_ss165()
    {
      //
      clsGV.TextBox_Set(ref tx_sys_ss165_groupno, false);   //群組編號
      clsGV.TextBox_Set(ref tx_sys_ss165_groupname, false);   //群組名稱
      clsGV.Drpdown_Set(ref dr_sys_ss165_usertype, ref tx_sys_ss165_usertype, false);   //帳號類別

      //
      bt_NEW_ss165.Visible = false;
      bt_MOD_ss165.Visible = false;
      //bt_MODALL.Visible = true;
      //bt_SER.Visible = true;
      bt_DEL_ss165.Visible = false;
      //bt_PRN.Visible = true;
      bt_SAV_ss165.Visible = false;
      bt_CAN_ss165.Visible = false;
      bt_DTL_ss165.Visible = false;
      bt_QUT_ss165.Visible = false;
      //
      gr_GridView_sys_ss165.Columns[0].Visible = false;
      gr_GridView_sys_ss165.Enabled = false;
      //gr_GridView_sys_ss165.Columns[0].Visible=true;
    }

    private void SetSerMod_ss165()
    {
      //
      clsGV.TextBox_Set(ref tx_sys_ss165_groupno, false);   //群組編號
      clsGV.TextBox_Set(ref tx_sys_ss165_groupname, false);   //群組名稱
      clsGV.Drpdown_Set(ref dr_sys_ss165_usertype, ref tx_sys_ss165_usertype, false);   //帳號類別

      //
      bt_NEW_ss165.Visible = true;
      bt_MOD_ss165.Visible = true;
      //bt_MODALL.Visible = true;
      //bt_SER.Visible = true;
      bt_DEL_ss165.Visible = true;
      //bt_PRN.Visible = true;
      bt_SAV_ss165.Visible = false;
      bt_CAN_ss165.Visible = false;
      bt_DTL_ss165.Visible = true;
      bt_QUT_ss165.Visible = true;
      //
      gr_GridView_sys_ss165.Columns[0].Visible = true;
      gr_GridView_sys_ss165.Enabled = true;
      //gr_GridView_sys_ss165.Columns[0].Visible=true;
    }

    private void SetEditMod_ss165()
    {
      // 
      clsGV.TextBox_Set(ref tx_sys_ss165_groupno, true);  //群組編號
      clsGV.TextBox_Set(ref tx_sys_ss165_groupname, true);  //群組名稱
      clsGV.Drpdown_Set(ref dr_sys_ss165_usertype, ref tx_sys_ss165_usertype, true);   //帳號類別

      // 
      bt_NEW_ss165.Visible = false;
      bt_MOD_ss165.Visible = false;
      //bt_MODALL.Visible = false;
      //bt_SER.Visible = false;
      bt_DEL_ss165.Visible = false;
      //bt_PRN.Visible = false;
      bt_SAV_ss165.Visible = true;
      bt_CAN_ss165.Visible = true;
      bt_DTL_ss165.Visible = false;
      bt_QUT_ss165.Visible = false;
      //
      bt_DEL_ss165.OnClientClick = " return false;";
      bt_MOD_ss165.OnClientClick = " return false;";
      //
      gr_GridView_sys_ss165.Columns[0].Visible = false;
      gr_GridView_sys_ss165.Enabled = false;
      //gr_GridView_sys_ss165.Columns[0].Visible = true;
    }

    private void BindText_ss165(DataRow CurRow)
    {
      tx_sys_ss165_groupno.Text = DAC.GetStringValue(CurRow["sys_ss165_groupno"]);  //群組編號
      tx_sys_ss165_groupname.Text = DAC.GetStringValue(CurRow["sys_ss165_groupname"]);  //群組名稱
      dr_sys_ss165_usertype = sFN.SetDropDownList(ref dr_sys_ss165_usertype, DAC.GetStringValue(CurRow["sys_ss165_usertype"]));  //帳號類別
      //
      hh_mkey_ss165.Value = DAC.GetStringValue(CurRow["sys_ss165_mkey"]);
    }

    private void BindNew_ss165(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      try
      {
        CmdQueryS_ss165 = (OleDbCommand)Session["fmsys_ss165_CmdQueryS"];
      }
      catch
      {
        CmdQueryS_ss165.CommandText = "";
      }
      //
      DataTable tb_sys_ss165 = new DataTable();
      DAC_ss165 ss165Dao = new DAC_ss165(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = ss165Dao.GetDataAdapter("YN01", "UNsys_ss165", "sys_ss165", st_addselect, false, st_addjoin, CmdQueryS_ss165, st_addunion, "");
      ad_DataDataAdapter.Fill(tb_sys_ss165);
      //
      if (tb_sys_ss165.Rows.Count > 0)
      {
        bt_DEL_ss165.OnClientClick = "return btnDEL_c()";
        bt_MOD_ss165.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        bt_DEL_ss165.OnClientClick = "return btnDEL0_c()";
        bt_MOD_ss165.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_sys_ss165.DataSource = tb_sys_ss165;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_sys_ss165 = clsGV.BindGridView(gr_GridView_sys_ss165, tb_sys_ss165, hh_GridCtrl_ss165, ref hh_GridGkey_ss165, "fmsys_ss165_gr_GridView_sys_ss165");
      gr_GridView_sys_ss165.DataBind();
      SelDataKey = "sys_ss165_gkey='" + hh_GridGkey_ss165.Value + "'";
      SelDataRow = tb_sys_ss165.Select(SelDataKey);
      //
      Session["fmsys_ss165_gr_GridView_sys_ss165_GridGkey"] = hh_GridGkey_ss165.Value;
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          BindText_ss165(CurRow);
          if ((hh_GridCtrl_ss175.Value.ToLower() == "mod") || (hh_GridCtrl_ss175.Value.ToLower() == "ins"))
          {
            BindNew_ss175(false);
          }
          else
          {
            BindNew_ss175(true);
          }
          BindNew_ss170(hh_GridGkey_ss165.Value, hh_treekey.Value);
        }
        else
        {
          hh_GridCtrl_ss165.Value = "init";
          ClearText_ss165();
        }
      }
      tb_sys_ss165.Dispose();
      ss165Dao.Dispose();
    }

    protected void gr_GridView_sys_ss165_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_sys_ss165.Enabled)
      {
        hh_GridCtrl_ss165.Value = "ser";
        BindNew_ss165(true);
        SetSerMod_ss165();
        Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"] = gr_GridView_sys_ss165.PageIndex;
        Session["fmsys_ss165_gr_GridView_sys_ss165_SelectedIndex"] = gr_GridView_sys_ss165.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_sys_ss165_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_sys_ss165.PageIndex = e.NewPageIndex;
      Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"] = gr_GridView_sys_ss165.PageIndex;
      hh_GridGkey_ss165.Value = gr_GridView_sys_ss165.DataKeys[gr_GridView_sys_ss165.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_sys_ss165_SelectedIndexChanged(object sender, EventArgs e)
    {
      Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"] = gr_GridView_sys_ss165.PageIndex;
      Session["fmsys_ss165_gr_GridView_sys_ss165_SelectedIndex"] = gr_GridView_sys_ss165.SelectedIndex;
      hh_GridGkey_ss165.Value = gr_GridView_sys_ss165.DataKeys[gr_GridView_sys_ss165.SelectedIndex].Value.ToString();
      BindNew_ss165(true);
      SetSerMod_ss165();
    }

    protected void bt_NEW_ss165_Click(object sender, EventArgs e)
    {
      actNEW_ss165();
    }

    protected void actNEW_ss165()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 2, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl_ss165.Value = "ins";
        Set_Control_ss165();
        ClearText_ss165();
        //定義guidkey
        hh_ActKey_ss165.Value = DAC.get_guidkey();
        BindNew_ss165(false);
        SetEditMod_ss165();
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_sys_ss165_groupno').focus(); </script>";
      }
    }

    protected void bt_MOD_ss165_Click(object sender, EventArgs e)
    {
      actMOD_ss165();
    }
    protected void actMOD_ss165()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl_ss165.Value = "mod";
        Set_Control_ss165();
        //取Act guidkey
        hh_ActKey_ss165.Value = DAC.get_guidkey();
        BindNew_ss165(true);
        SetEditMod_ss165();
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_sys_ss165_groupname').focus(); </script>";
      }
    }

    protected void bt_QUT_ss165_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Master/" + Page.Theme + "/MainForm.aspx");
    }

    protected void bt_CAN_ss165_Click(object sender, EventArgs e)
    {
      actCAN_ss165();
    }

    protected void actCAN_ss165()
    {
      hh_GridCtrl_ss165.Value = "ser";
      Set_Control_ss165();
      ClearText_ss165();
      BindNew_ss165(true);
      SetSerMod_ss165();
    }

    protected void bt_DEL_ss165_Click(object sender, EventArgs e)
    {
      actDEL_ss165();
    }

    protected void actDEL_ss165()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 5, UserLoginGkey, ref li_AccMsg))
      {
        bool bl_delok = false;
        Set_Control_ss165();
        hh_ActKey_ss165.Value = DAC.get_guidkey();
        //
        DAC_ss165 sys_ss165Dao = new DAC_ss165(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "sys_ss165_gkey='" + hh_GridGkey_ss165.Value + "'";
        DataTable tb_sys_ss165 = new DataTable();
        DbDataAdapter da_ADP = sys_ss165Dao.GetDataAdapter("YN01", "UNsys_ss165", "sys_ss165", st_addselect, false, st_addjoin, CmdQueryS_ss165, st_addunion, "");
        da_ADP.Fill(tb_sys_ss165);
        DataRow[] DelRow = tb_sys_ss165.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            DelRow[0].Delete();
            da_ADP.Update(tb_sys_ss165);
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
            sys_ss165Dao.Dispose();
            tb_sys_ss165.Dispose();
            da_ADP.Dispose();
            conn.Close();
          }
        }
        tb_sys_ss165.Clear();
        if (bl_delok)
        {
          gr_GridView_sys_ss165 = clsGV.SetGridCursor("del", gr_GridView_sys_ss165, -2);
        }
        //
        SetSerMod_ss165();
        BindNew_ss165(true);
      }
    }

    protected void bt_SAV_ss165_Click(object sender, EventArgs e)
    {
      actSAV_ss165();
    }

    protected void actSAV_ss165()
    {
      string st_ckerrmsg = "";
      string st_dberrmsg = "";
      string st_tempgkey = "";
      bool bl_insok = false, bl_updateok = false;
      //
      Set_Control_ss165();
      if (ServerEditCheck_ss165(ref st_ckerrmsg))
      {
        DAC_ss165 sys_ss165Dao = new DAC_ss165(conn);
        if (hh_GridCtrl_ss165.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "sys_ss165_gkey='" + hh_GridGkey_ss165.Value + "'";
          if (hh_GridCtrl_ss165.Value.ToLower() == "ins")
          {
            //檢查重複
            if (sys_ss165Dao.IsExists("sys_ss165", "groupno", tx_sys_ss165_groupno.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_sys_ss165_groupno.Text + ",已存在.");
            }
            else
            {
              DataTable tb_sys_ss165_ins = new DataTable();
              DbDataAdapter da_ADP_ins = sys_ss165Dao.GetDataAdapter("YN01", "UNsys_ss165", "sys_ss165", st_addselect, false, st_addjoin, CmdQueryS_ss165, st_addunion, "");
              da_ADP_ins.Fill(tb_sys_ss165_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_sys_ss165_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["sys_ss165_gkey"] = st_tempgkey;       // 
                ins_row["sys_ss165_mkey"] = DAC.get_guidkey(); //
                ins_row["sys_ss165_groupno"] = tx_sys_ss165_groupno.Text.Trim();       // 群組編號
                ins_row["sys_ss165_groupname"] = tx_sys_ss165_groupname.Text.Trim();       // 群組名稱
                ins_row["sys_ss165_usertype"] = dr_sys_ss165_usertype.SelectedValue;       // 帳號類別
                ins_row["sys_ss165_cname"] = UserName;  // 設定人員
                ins_row["sys_ss165_trusr"] = UserGkey;  //
                tb_sys_ss165_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_sys_ss165_ins);
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
                sys_ss165Dao.Dispose();
                tb_sys_ss165_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey_ss165.Value = st_tempgkey;
              hh_GridCtrl_ss165.Value = "rekey";
              BindNew_ss165(true);
              hh_GridCtrl_ss165.Value = "ser";
              SetSerMod_ss165();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_insok
          }  //ins
          else if (hh_GridCtrl_ss165.Value.ToLower() == "mod")
          {
            if (sys_ss165Dao.IsExists("sys_ss165", "groupno", tx_sys_ss165_groupno.Text, "gkey<>'" + hh_GridGkey_ss165.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_sys_ss165_groupno.Text + ",已存在.");
            }
            else
            {
              DataTable tb_sys_ss165_mod = new DataTable();
              DbDataAdapter da_ADP_mod = sys_ss165Dao.GetDataAdapter("YN01", "UNsys_ss165", "sys_ss165", st_addselect, false, st_addjoin, CmdQueryS_ss165, st_addunion, "");
              da_ADP_mod.Fill(tb_sys_ss165_mod);
              st_SelDataKey = "sys_ss165_gkey='" + hh_GridGkey_ss165.Value + "'";
              DataRow[] mod_rows = tb_sys_ss165_mod.Select(st_SelDataKey);
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
                  mod_row["sys_ss165_groupno"] = tx_sys_ss165_groupno.Text.Trim();       // 群組編號
                  mod_row["sys_ss165_groupname"] = tx_sys_ss165_groupname.Text.Trim();       // 群組名稱
                  mod_row["sys_ss165_cname"] = UserName;       // 設定人員
                  mod_row["sys_ss165_usertype"] = dr_sys_ss165_usertype.SelectedValue;       // 帳號類別
                  mod_row["sys_ss165_mkey"] = DAC.get_guidkey();        //
                  mod_row["sys_ss165_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_sys_ss165_mod);
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
                  sys_ss165Dao.Dispose();
                  tb_sys_ss165_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_GridCtrl_ss165.Value = "rekey";
              BindNew_ss165(true);
              hh_GridCtrl_ss165.Value = "ser";
              SetSerMod_ss165();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_updateok
          }   //mod
        }  //ins & mod
        sys_ss165Dao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }
    }

    private bool ServerEditCheck_ss165(ref string sMsg)
    {
      bool ret;
      clsDataCheck DataCheck = new clsDataCheck();
      ret = true;
      sMsg = "";
      ret = DataCheck.cIsStrEmptyChk(ret, tx_sys_ss165_groupno.Text, lb_sys_ss165_groupno.Text, ref sMsg, PublicVariable.LangType, sFN);
      ret = DataCheck.cIsStrEmptyChk(ret, tx_sys_ss165_groupname.Text, lb_sys_ss165_groupname.Text, ref sMsg, PublicVariable.LangType, sFN);
      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_sys_ss165_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        ////群組編號
        //if (e.Row.FindControl("tx_sys_ss165_groupno02") != null)
        //{
        //  TextBox tx_sys_ss165_groupno02 = e.Row.FindControl("tx_sys_ss165_groupno02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["sys_ss165_groupno"]).Trim();
        //  tx_sys_ss165_groupno02.Text = st_datavalue;
        //  if (hh_GridCtrl_ss165.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss165_groupno02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss165_groupno02, false); }
        //}
        ////群組名稱
        //if (e.Row.FindControl("tx_sys_ss165_groupname02") != null)
        //{
        //  TextBox tx_sys_ss165_groupname02 = e.Row.FindControl("tx_sys_ss165_groupname02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["sys_ss165_groupname"]).Trim();
        //  tx_sys_ss165_groupname02.Text = st_datavalue;
        //  if (hh_GridCtrl_ss165.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_ss165_groupname02, true); } else { clsGV.TextBox_Set(ref tx_sys_ss165_groupname02, false); }
        //}
        ////設定人員
        //if (e.Row.FindControl("tx_sys_ss165_cname02") != null)
        //{
        //  TextBox tx_sys_ss165_cname02 = e.Row.FindControl("tx_sys_ss165_cname02") as TextBox;
        //  st_datavalue = DAC.GetStringValue(rowView["sys_ss165_cname"]).Trim();
        //  tx_sys_ss165_cname02.Text = st_datavalue;
        //  clsGV.TextBox_Set(ref tx_sys_ss165_cname02, false);
        //}
        //帳號類別
        if (e.Row.FindControl("dr_sys_ss165_usertype02") != null)
        {
          DropDownList dr_sys_ss165_usertype02 = e.Row.FindControl("dr_sys_ss165_usertype02") as DropDownList;
          TextBox tx_sys_ss165_usertype02 = e.Row.FindControl("tx_sys_ss165_usertype02") as TextBox;
          dr_sys_ss165_usertype02 = sFN.DropDownListFromClasses(ref dr_sys_ss165_usertype02, "es101_usertype", "class_text", "class_value");
          st_datavalue = DAC.GetStringValue(rowView["sys_ss165_usertype"]).Trim();
          dr_sys_ss165_usertype02 = sFN.SetDropDownList(ref dr_sys_ss165_usertype02, st_datavalue);
          if (hh_GridCtrl_ss165.Value == "modall") { clsGV.Drpdown_Set(ref dr_sys_ss165_usertype02, ref tx_sys_ss165_usertype02, true); } else { clsGV.Drpdown_Set(ref dr_sys_ss165_usertype02, ref tx_sys_ss165_usertype02, false); }
        }

      }
      sFN.Dispose();

    }

    private void Set_Control_ss175()
    {

    }

    private void ClearText_ss175()
    {
      dr_es101_no.SelectedIndex = -1;  //員工編號
      tx_es101_ename.Text = "";  //英文姓名
      //
      hh_mkey_ss175.Value = "";
    }

    private void SetDisable_ss175()
    {
      clsGV.Drpdown_Set(ref dr_es101_no, ref tx_es101_no, false);   //員工編號
      clsGV.TextBox_Set(ref tx_es101_ename, false);   //英文姓名
      //
      lb_es101_no.Visible = false;
      dr_es101_no.Visible = false;
      tx_es101_no.Visible = false;
      lb_es101_ename.Visible = false;
      tx_es101_ename.Visible = false;
      //
      bt_NEW_ss175.Visible = false;
      bt_MOD_ss175.Visible = false;
      bt_DEL_ss175.Visible = false;
      bt_SAV_ss175.Visible = false;
      bt_CAN_ss175.Visible = false;
      bt_QUT_ss175.Visible = false;
      //
      gr_GridView_sys_ss175.Columns[0].Visible = false;
      gr_GridView_sys_ss175.Enabled = false;
    }

    private void SetSerMod_ss175()
    { //
      lb_es101_no.Visible = true;
      dr_es101_no.Visible = true;
      tx_es101_no.Visible = true;
      lb_es101_ename.Visible = true;
      tx_es101_ename.Visible = true;

      clsGV.Drpdown_Set(ref dr_es101_no, ref tx_es101_no, false);   //員工編號
      clsGV.TextBox_Set(ref tx_es101_ename, false);   //英文姓名
      //
      bt_NEW_ss175.Visible = true;
      bt_MOD_ss175.Visible = true;
      bt_DEL_ss175.Visible = true;
      bt_SAV_ss175.Visible = false;
      bt_CAN_ss175.Visible = false;
      bt_QUT_ss175.Visible = true;
      //
      gr_GridView_sys_ss175.Columns[0].Visible = true;
      gr_GridView_sys_ss175.Enabled = true;
    }

    private void SetEditMod_ss175()
    {
      lb_es101_no.Visible = true;
      dr_es101_no.Visible = true;
      tx_es101_no.Visible = true;
      lb_es101_ename.Visible = true;
      tx_es101_ename.Visible = true;
      // 
      clsGV.Drpdown_Set(ref dr_es101_no, ref tx_es101_no, true);   //員工編號
      clsGV.TextBox_Set(ref tx_es101_ename, true);  //英文姓名
      // 
      bt_NEW_ss175.Visible = false;
      bt_MOD_ss175.Visible = false;
      bt_DEL_ss175.Visible = false;
      bt_SAV_ss175.Visible = true;
      bt_CAN_ss175.Visible = true;
      //
      bt_DEL_ss175.OnClientClick = " return false;";
      bt_MOD_ss175.OnClientClick = " return false;";
      //
      gr_GridView_sys_ss175.Columns[0].Visible = true;
      gr_GridView_sys_ss175.Enabled = false;
    }

    private void BindText_ss175(DataRow CurRow)
    {
      clsFN sFN = new clsFN();
      dr_es101_no = sFN.SetDropDownList(ref dr_es101_no, DAC.GetStringValue(CurRow["es101_no"]));  //員工編號
      tx_es101_ename.Text = DAC.GetStringValue(CurRow["es101_ename"]);  //英文姓名
      hh_mkey_ss175.Value = DAC.GetStringValue(CurRow["sys_ss175_mkey"]);
      sFN.Dispose();
    }

    private void BindNew_ss175(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      CmdQueryS_ss175.CommandText = " and a.ss165gkey='" + hh_GridGkey_ss165.Value + "' ";
      //
      DataTable tb_sys_ss175 = new DataTable();
      DAC_ss165 sys_ss165Dao = new DAC_ss165(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = sys_ss165Dao.GetDataAdapter("YN01", "UNsys_ss175", "sys_ss175", st_addselect, false, st_addjoin, CmdQueryS_ss175, st_addunion, "");
      ad_DataDataAdapter.Fill(tb_sys_ss175);
      //
      if (tb_sys_ss175.Rows.Count > 0)
      {
        bt_DEL_ss175.OnClientClick = "return btnDEL_c()";
        bt_MOD_ss175.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        bt_DEL_ss175.OnClientClick = "return btnDEL0_c()";
        bt_MOD_ss175.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_sys_ss175.DataSource = tb_sys_ss175;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_sys_ss175 = clsGV.BindGridView(gr_GridView_sys_ss175, tb_sys_ss175, hh_GridCtrl_ss175, ref hh_GridGkey_ss175, "fmsys_ss175_gr_GridView_sys_ss175");
      gr_GridView_sys_ss175.DataBind();
      SelDataKey = "sys_ss175_gkey='" + hh_GridGkey_ss175.Value + "'";
      SelDataRow = tb_sys_ss175.Select(SelDataKey);
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          BindText_ss175(CurRow);
        }
        else
        {
          hh_GridCtrl_ss175.Value = "init";
          ClearText_ss175();
        }
      }
      tb_sys_ss175.Dispose();
      sys_ss165Dao.Dispose();
    }
    protected void gr_GridView_sys_ss175_SelectedIndexChanged(object sender, EventArgs e)
    {
      Session["fmsys_ss175_gr_GridView_sys_ss175_PageIndex"] = gr_GridView_sys_ss175.PageIndex + 1;
      Session["fmsys_ss175_gr_GridView_sys_ss175_SelectedIndex"] = gr_GridView_sys_ss175.SelectedIndex;
      hh_GridGkey_ss175.Value = gr_GridView_sys_ss175.DataKeys[gr_GridView_sys_ss175.SelectedIndex].Value.ToString();
      BindNew_ss175(true);
      SetSerMod_ss175();
    }
    protected void gr_GridView_sys_ss175_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_sys_ss175.Enabled)
      {
        SetSerMod_ss175();
        hh_GridCtrl_ss175.Value = "ser";
        BindNew_ss175(true);
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void bt_NEW_ss175_Click(object sender, EventArgs e)
    {
      actNEW_ss175();
    }

    protected void actNEW_ss175()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 2, UserLoginGkey, ref li_AccMsg))
      {
        DAC.checkAccessFunc(UserGkey, "sys_ss175_NEW");
        hh_GridCtrl_ss175.Value = "ins";
        Set_Control_ss175();
        ClearText_ss175();
        SetEditMod_ss175();
        //定義guidkey
        hh_ActKey_ss175.Value = DAC.get_guidkey();
        BindNew_ss175(false);
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_dr_es101_no').focus(); </script>";
      }
    }

    protected void bt_MOD_ss175_Click(object sender, EventArgs e)
    {
      actMOD_ss175();
    }

    protected void actMOD_ss175()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        DAC.checkAccessFunc(UserId, "sys_ss175_MOD");
        hh_GridCtrl_ss175.Value = "mod";
        Set_Control_ss175();
        SetEditMod_ss175();
        //取Act guidkey
        hh_ActKey_ss175.Value = DAC.get_guidkey();
        BindNew_ss175(true);
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_dr_es101_no').focus(); </script>";
      }
    }

    protected void bt_CAN_ss175_Click(object sender, EventArgs e)
    {
      actCAN_ss175();
    }

    protected void actCAN_ss175()
    {
      hh_GridCtrl_ss175.Value = "ser";
      Set_Control_ss175();
      ClearText_ss175();
      BindNew_ss175(true);
      SetSerMod_ss175();
    }
    protected void bt_DEL_ss175_Click(object sender, EventArgs e)
    {
      actDEL_ss175();
    }

    protected void actDEL_ss175()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 5, UserLoginGkey, ref li_AccMsg))
      {
        bool bl_delok = false;
        DAC.checkAccessFunc(UserId, "sys_ss175_DEL");
        Set_Control_ss175();
        hh_ActKey_ss175.Value = DAC.get_guidkey();
        //
        DAC_ss165 sys_ss175Dao = new DAC_ss165(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "sys_ss175_gkey='" + hh_GridGkey_ss175.Value + "'";
        DataTable tb_sys_ss175 = new DataTable();
        DbDataAdapter da_ADP = sys_ss175Dao.GetDataAdapter("YN01", "UNsys_ss175", "sys_ss175", st_addselect, false, st_addjoin, CmdQueryS_ss175, st_addunion, "");
        da_ADP.Fill(tb_sys_ss175);
        DataRow[] DelRow = tb_sys_ss175.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            DelRow[0].Delete();
            da_ADP.Update(tb_sys_ss175);
            thistran.Commit();
            bl_delok = true;
          }
          catch (Exception e)
          {
            thistran.Rollback();
            bl_delok = false;
            lb_ErrorMessage02.Visible = true;
            lb_ErrorMessage02.Text = e.Message;
          }
          finally
          {
            thistran.Dispose();
            sys_ss175Dao.Dispose();
            tb_sys_ss175.Dispose();
            da_ADP.Dispose();
            conn.Close();
          }
        }
        tb_sys_ss175.Clear();
        if (bl_delok)
        {
          gr_GridView_sys_ss175 = clsGV.SetGridCursor("del", gr_GridView_sys_ss175, -2);
        }
        //
        SetSerMod_ss175();
        BindNew_ss175(true);
      }
    }

    protected void bt_SAV_ss175_Click(object sender, EventArgs e)
    {
      actSAV_ss175();
    }
    protected void actSAV_ss175()
    {
      string st_ckerrmsg = "";
      string st_dberrmsg = "";
      string st_tempgkey = "";
      bool bl_insok = false, bl_updateok = false;
      //
      Set_Control_ss175();
      if (ServerEditCheck_ss175(ref st_ckerrmsg))
      {
        DAC_ss165 sys_ss175Dao = new DAC_ss165(conn);
        if (hh_GridCtrl_ss175.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "sys_ss175_gkey='" + hh_GridGkey_ss175.Value + "'";
          if (hh_GridCtrl_ss175.Value.ToLower() == "ins")
          {
            //檢查重複
            if (sys_ss175Dao.IsExists("sys_ss175", "es101gkey", dr_es101_no.Items[dr_es101_no.SelectedIndex].Value, " ss165gkey='" + hh_GridGkey_ss165.Value + "'"))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(dr_es101_no.Items[dr_es101_no.SelectedIndex].Text + ",已存在.");
            }
            else
            {
              DataTable tb_sys_ss175_ins = new DataTable();
              DbDataAdapter da_ADP_ins = sys_ss175Dao.GetDataAdapter("YN01", "UNsys_ss175", "sys_ss175", st_addselect, false, st_addjoin, CmdQueryS_ss175, st_addunion, "");
              da_ADP_ins.Fill(tb_sys_ss175_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_sys_ss175_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["sys_ss175_gkey"] = st_tempgkey;       // 
                ins_row["sys_ss175_mkey"] = DAC.get_guidkey(); //
                ins_row["sys_ss175_es101gkey"] = dr_es101_no.Items[dr_es101_no.SelectedIndex].Value;       // 系統使用
                ins_row["sys_ss175_ss165gkey"] = hh_GridGkey_ss165.Value;       // ss165gkey
                ins_row["sys_ss175_trusr"] = UserGkey;  //
                tb_sys_ss175_ins.Rows.Add(ins_row);
                //
                da_ADP_ins.Update(tb_sys_ss175_ins);
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
                sys_ss175Dao.Dispose();
                tb_sys_ss175_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey_ss175.Value = st_tempgkey;
              hh_GridCtrl_ss175.Value = "rekey";
              BindNew_ss175(true);
              hh_GridCtrl_ss175.Value = "ser";
              SetSerMod_ss175();
            }
            else
            {
              lb_ErrorMessage02.Text = st_dberrmsg;
              lb_ErrorMessage02.Visible = true;
            } //bl_insok
          }  //ins
          else if (hh_GridCtrl_ss175.Value.ToLower() == "mod")
          {
            if (sys_ss175Dao.IsExists("sys_ss175", "es101gkey", dr_es101_no.Items[dr_es101_no.SelectedIndex].Value, "gkey<>'" + hh_GridGkey_ss175.Value + "' and ss165gkey='" + hh_GridGkey_ss165.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(dr_es101_no.Items[dr_es101_no.SelectedIndex].Text + ",已存在.");
            }
            else
            {
              DataTable tb_sys_ss175_mod = new DataTable();
              DbDataAdapter da_ADP_mod = sys_ss175Dao.GetDataAdapter("YN01", "UNsys_ss175", "sys_ss175", st_addselect, false, st_addjoin, CmdQueryS_ss175, st_addunion, "");
              da_ADP_mod.Fill(tb_sys_ss175_mod);
              st_SelDataKey = "sys_ss175_gkey='" + hh_GridGkey_ss175.Value + "'";
              DataRow[] mod_rows = tb_sys_ss175_mod.Select(st_SelDataKey);
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
                  mod_row["sys_ss175_es101gkey"] = dr_es101_no.Items[dr_es101_no.SelectedIndex].Value;       // 系統使用
                  mod_row["sys_ss175_ss165gkey"] = hh_GridGkey_ss165.Value;       // ss165gkey
                  mod_row["sys_ss175_mkey"] = DAC.get_guidkey();        //
                  mod_row["sys_ss175_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_sys_ss175_mod);
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
                  sys_ss175Dao.Dispose();
                  tb_sys_ss175_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              hh_GridCtrl_ss175.Value = "rekey";
              BindNew_ss175(true);
              hh_GridCtrl_ss175.Value = "ser";
              SetSerMod_ss175();
            }
            else
            {
              lb_ErrorMessage02.Text = st_dberrmsg;
              lb_ErrorMessage02.Visible = true;
            } //bl_updateok
          }   //mod
        }  //ins & mod
        sys_ss175Dao.Dispose();
      }
      else
      {
        lb_ErrorMessage02.Visible = true;
        lb_ErrorMessage02.Text = st_ckerrmsg;
      }
    }

    private bool ServerEditCheck_ss175(ref string sMsg)
    {
      bool ret;
      ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();

      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_sys_ss175_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //員工編號
        if (e.Row.FindControl("dr_es101_no02") != null)
        {
          DropDownList dr_es101_no02 = e.Row.FindControl("dr_es101_no02") as DropDownList;
          TextBox tx_es101_no02 = e.Row.FindControl("tx_es101_no02") as TextBox;
          dr_es101_no02 = sFN.DropDownListFromTable(ref dr_es101_no02, "es101", "gkey", "no+cname", "", "no");
          st_datavalue = DAC.GetStringValue(rowView["sys_ss175_es101gkey"]).Trim();
          dr_es101_no02 = sFN.SetDropDownList(ref dr_es101_no02, st_datavalue);
          if (hh_GridCtrl_ss175.Value == "modall") { clsGV.Drpdown_Set(ref dr_es101_no02, ref tx_es101_no02, true); } else { clsGV.Drpdown_Set(ref dr_es101_no02, ref tx_es101_no02, false); }
        }
        //英文姓名
        if (e.Row.FindControl("tx_es101_ename02") != null)
        {
          TextBox tx_es101_ename02 = e.Row.FindControl("tx_es101_ename02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["es101_ename"]).Trim();
          tx_es101_ename02.Text = st_datavalue;
          if (hh_GridCtrl_ss175.Value == "modall") { clsGV.TextBox_Set(ref tx_es101_ename02, true); } else { clsGV.TextBox_Set(ref tx_es101_ename02, false); }
        }
      }
      sFN.Dispose();

    }

    protected void gr_GridView_sys_ss170_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //作業編號             
        if (e.Row.FindControl("tx_sys_menu_obj_name_02") != null)
        {
          TextBox tx_sys_menu_obj_name_02 = e.Row.FindControl("tx_sys_menu_obj_name_02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_menu_obj_name_"]).Trim();
          tx_sys_menu_obj_name_02.Text = st_datavalue;
          clsGV.TextBox_Set(ref tx_sys_menu_obj_name_02, false);
        }

        //作業名稱
        if (e.Row.FindControl("tx_sys_menu_chinesesimp_02") != null)
        {
          TextBox tx_sys_menu_chinesesimp_02 = e.Row.FindControl("tx_sys_menu_chinesesimp_02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_menu_chinesesimp_"]).Trim();
          tx_sys_menu_chinesesimp_02.Text = st_datavalue;
          clsGV.TextBox_Set(ref tx_sys_menu_chinesesimp_02, false);
        }
        //1=有權限
        if (e.Row.FindControl("ck_sys_ss170_mark02") != null)
        {
          CheckBox ck_sys_ss170_mark02 = e.Row.FindControl("ck_sys_ss170_mark02") as CheckBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_ss170_mark"]).Trim();
          if (st_datavalue == "1") ck_sys_ss170_mark02.Checked = true; else ck_sys_ss170_mark02.Checked = false;
          //ck_sys_ss170_mark02.Enabled = true;
          ck_sys_ss170_mark02.Enabled = st_accmod;
        }
        //按鈕編號
        if (e.Row.FindControl("tx_sys_ss160_buttonno02") != null)
        {
          TextBox tx_sys_ss160_buttonno02 = e.Row.FindControl("tx_sys_ss160_buttonno02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_ss160_buttonno"]).Trim();
          tx_sys_ss160_buttonno02.Text = st_datavalue;
          clsGV.TextBox_Set(ref tx_sys_ss160_buttonno02, false);
        }
        //按鈕名稱
        if (e.Row.FindControl("tx_sys_ss160_button02") != null)
        {
          TextBox tx_sys_ss160_button02 = e.Row.FindControl("tx_sys_ss160_button02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_ss160_button"]).Trim();
          tx_sys_ss160_button02.Text = st_datavalue;
          clsGV.TextBox_Set(ref tx_sys_ss160_button02, false);
        }
      }
      sFN.Dispose();
    }

    private void BindNew_ss170(string st_ss165gkey, string st_sys_menu_obj_name)
    {
      DataTable tb_ss170 = new DataTable();
      DAC_ss165 ss170Dao = new DAC_ss165(conn);
      //
      CmdQueryS_ss170.Parameters.Clear();
      CmdQueryS_ss170.CommandText = " and m.obj_name in (select obj_name from sys_menu where parent_code=? or obj_name=? )  and a.ss165gkey=? ";
      DAC.AddParam(CmdQueryS_ss170, "parent_code", st_sys_menu_obj_name);
      DAC.AddParam(CmdQueryS_ss170, "obj_name", st_sys_menu_obj_name);
      DAC.AddParam(CmdQueryS_ss170, "ss165gkey", st_ss165gkey);
      //
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      string st_addjoin = "";
      string st_addunion = "";
      st_addunion += "select '0' as sys_ss170_mark ,a.buttonno as sys_ss160_buttonno ,a.button as sys_ss160_button ,a.button_e as sys_ss160_button_e ,a.button_t as sys_ss160_button_t ,a.button_c as sys_ss160_button_c ,a.button_v as sys_ss160_button_v ,a.tip_e as sys_ss160_tip_e ,a.tip_t as sys_ss160_tip_t ,a.tip_c as sys_ss160_tip_c ,a.tip_v as sys_ss160_tip_v ,";
      st_addunion += "m.prg_code as sys_menu_prg_code,m.obj_name as sys_menu_obj_name,m.chinesesimpname as sys_menu_chinesesimpname,";
      st_addunion += "a.gkey as  sys_ss170_ss160gkey ,'" + st_ss165gkey + "' as  sys_ss170_ss165gkey ,'*' as sys_ss170_gkey,a.mkey as sys_ss170_mkey ,a.trcls as sys_ss170_trcls,a.trcrd as sys_ss170_trcrd,a.trmod as sys_ss170_trmod,a.trusr as sys_ss10_trusr,";
      st_addunion += "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      st_addunion += "from sys_ss160 a ";
      st_addunion += "left outer join sys_menu m on a.ss155gkey=m.obj_name ";
      st_addunion += "where a.ss155gkey in (select obj_name from sys_menu where parent_code='" + st_tree_gkey + "' or obj_name='" + st_tree_gkey + "' ) " + " and a.gkey not in (select ss160gkey from sys_ss170 where ss165gkey='" + st_ss165gkey + "') ";
      //st_addunion += "where a.ss155gkey='" + st_tree_gkey + "' and a.gkey not in (select ss160gkey from sys_ss180 where es101gkey='" + hh_GridGkey.Value + "') ";
      ad_DataDataAdapter = ss170Dao.GetDataAdapter("YN01", "UNsys_ss170", "sys_ss170", st_addselect, false, st_addjoin, CmdQueryS_ss170, st_addunion, "sys_menu_prg_code,sys_ss160_buttonno");
      ad_DataDataAdapter.Fill(tb_ss170);
      if (tb_ss170.Rows.Count > 0)
      {
        //bt_DEL.OnClientClick = "return btnDEL_c()";
        //bt_MOD.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        //bt_DEL.OnClientClick = "return btnDEL0_c()";
        //bt_MOD.OnClientClick = "return btnMOD0_c()";
      }
      //
      DataRow CurRow;
      string st_prg = "";
      if (tb_ss170.Rows.Count > 0)
      {
        for (int vr = 0; vr < tb_ss170.Rows.Count; vr++)
        {
          CurRow = tb_ss170.Rows[vr];
          CurRow.BeginEdit();
          if (st_prg != DAC.GetStringValue(CurRow["sys_menu_prg_code"]))
          {
            CurRow["sys_menu_prg_code_"] = CurRow["sys_menu_prg_code"];
            CurRow["sys_menu_obj_name_"] = CurRow["sys_menu_obj_name"];
            CurRow["sys_menu_chinesesimp_"] = CurRow["sys_menu_chinesesimp"];
            st_prg = DAC.GetStringValue(CurRow["sys_menu_prg_code"]);
          }
          if (DAC.GetStringValue(CurRow["sys_ss170_gkey"]) == "*")
          {
            CurRow["sys_ss170_gkey"] = DAC.get_guidkey();
          }
          CurRow.EndEdit();
        }
      }
      //
      gr_GridView_sys_ss170.DataSource = tb_ss170;
      gr_GridView_sys_ss170 = clsGV.BindGridView(gr_GridView_sys_ss170, tb_ss170, hh_GridCtrl_ss170, ref hh_GridGkey_ss170, "fm_ss170_gr_GridView_ss170");
      gr_GridView_sys_ss170.DataBind();
      //
      tb_ss170.Dispose();
      ss170Dao.Dispose();
      ad_DataDataAdapter.Dispose();
    }

    protected void bn_ACC_Click(object sender, EventArgs e)
    {
      actACC();
    }

    protected void actACC()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        string st_dberrmsg = "";
        //
        Set_Control_ss165();
        //DAC_ss165 sys_ss170Dao = new DAC_ss165(conn);
        hh_ActKey_ss165.Value = DAC.get_guidkey();
        Session["fmsys_ss165_gr_GridView_sys_ss165_SelectedIndex"] = gr_GridView_sys_ss165.SelectedIndex;
        Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"] = gr_GridView_sys_ss165.PageIndex;
        if (UpdateDataAll_ss170(hh_ActKey_ss165.Value, hh_GridGkey_ss165.Value, hh_treekey.Value, ref st_dberrmsg))
        {
          SetSerMod_ss165();
          SetSerMod_ss175();
          BindNew_ss165(false);
          Response.Redirect("fm_SS165.ASPX?act_type=a&obj_name=" + st_tree_gkey);
        }
        else
        {
          lb_ErrorMessage.Visible = true;
          lb_ErrorMessage.Text = st_dberrmsg;
        }
      }
      //sys_ss170Dao.Dispose();
    }

    protected bool UpdateDataAll_ss170(string st_ActKey, string st_ss165gkey, string st_obj_code, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_sys_ss170_gkey = "", st_sys_ss170_mark = "", st_sys_ss160_buttonno = "", st_sys_ss160_button = "", st_sys_menu_obj_name = "", st_sys_menu_chinesesimp = "", st_sys_ss170_ss160gkey = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView_sys_ss170$ctl";
      //
      CmdQueryS_ss170.Parameters.Clear();
      CmdQueryS_ss170.CommandText = " and m.obj_name in (select obj_name from sys_menu where parent_code=? or obj_name=? )  and a.ss165gkey=? ";  //選擇的群組
      DAC.AddParam(CmdQueryS_ss170, "parent_code", st_obj_code);
      DAC.AddParam(CmdQueryS_ss170, "obj_name", st_obj_code);
      DAC.AddParam(CmdQueryS_ss170, "ss165gkey", st_ss165gkey); //選擇的群組
      //
      DataTable tb_sys_ss170 = new DataTable();
      DAC_ss165 sys_ss170Dao = new DAC_ss165(conn);
      string st_addselect = "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      string st_addjoin = "";
      string st_addunion = "";
      st_addunion += "select '0' as sys_ss170_mark ,a.buttonno as sys_ss160_buttonno ,a.button as sys_ss160_button ,a.button_e as sys_ss160_button_e ,a.button_t as sys_ss160_button_t ,a.button_c as sys_ss160_button_c ,a.button_v as sys_ss160_button_v ,a.tip_e as sys_ss160_tip_e ,a.tip_t as sys_ss160_tip_t ,a.tip_c as sys_ss160_tip_c ,a.tip_v as sys_ss160_tip_v ,";
      st_addunion += "m.prg_code as sys_menu_prg_code,m.obj_name as sys_menu_obj_name,m.chinesesimpname as sys_menu_chinesesimpname,";
      st_addunion += "a.gkey as  sys_ss170_ss160gkey ,'" + st_ss165gkey + "' as  sys_ss170_ss165gkey ,'*' as sys_ss170_gkey,a.mkey as sys_ss170_mkey ,a.trcls as sys_ss170_trcls,a.trcrd as sys_ss170_trcrd,a.trmod as sys_ss170_trmod,a.trusr as sys_ss170_trusr,";
      st_addunion += "space(20) as sys_menu_prg_code_,space(20) as sys_menu_obj_name_,space(20) as sys_menu_chinesesimp_ ";
      st_addunion += "from sys_ss160 a ";
      st_addunion += "left outer join sys_menu m on a.ss155gkey=m.obj_name ";
      st_addunion += "where a.ss155gkey in (select obj_name from sys_menu where parent_code='" + st_tree_gkey + "' or obj_name='" + st_tree_gkey + "' ) " + " and a.gkey not in (select ss160gkey from sys_ss170 where ss165gkey='" + st_ss165gkey + "') ";
      //st_addunion += "where a.ss155gkey='" + st_tree_gkey + "' and a.gkey not in (select ss160gkey from sys_ss180 where es101gkey='" + hh_GridGkey.Value + "') ";
      DbDataAdapter da_ADP = sys_ss170Dao.GetDataAdapter("YN01", "UNsys_ss170", "sys_ss170", st_addselect, false, st_addjoin, CmdQueryS_ss170, st_addunion, "sys_menu_prg_code,sys_ss160_buttonno");
      da_ADP.Fill(tb_sys_ss170);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_sys_ss170.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss170_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss170_gkey02";
            st_sys_ss170_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //1=有權限
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$ck_sys_ss170_mark02";
            st_sys_ss170_mark = DAC.GetStringValueBool(((CheckBox)FindControl(st_ctrlname)).Checked);
            //按鈕編號
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_buttonno02";
            st_sys_ss160_buttonno = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //按鈕名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss160_button02";
            st_sys_ss160_button = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //ss160_gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss170_ss160gkey02";
            st_sys_ss170_ss160gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //obj_name
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_ss170_ss165gkey02";
            st_sys_menu_obj_name = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //chinesesimpname
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_menu_chinesesimp_02";
            st_sys_menu_chinesesimp = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_sys_ss170.Select("sys_ss170_gkey='" + st_sys_ss170_gkey + "'");
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (DAC.GetStringValue(mod_row["sys_ss170_mark"]) != st_sys_ss170_mark)
              {
                sys_ss170Dao.Insertbalog(conn, thistran, "sys_ss170", st_ActKey, UserName);
                sys_ss170Dao.Insertbtlog(conn, thistran, "sys_ss170", DAC.GetStringValue(mod_row["sys_ss170_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["sys_ss170_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_ss170_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_ss170_gkey"]));
                mod_row.BeginEdit();
                mod_row["sys_ss170_mark"] = st_sys_ss170_mark;        //1=有權限
                mod_row["sys_ss170_mkey"] = DAC.get_guidkey();         //
                mod_row["sys_ss170_trusr"] = UserName;  //
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();
                //
              }
            }  //sel_rows.Length == 1
            else
            {
              mod_row = tb_sys_ss170.NewRow();
              //
              mod_row["sys_ss170_mark"] = st_sys_ss170_mark;        //1=有權限
              mod_row["sys_ss170_ss160gkey"] = st_sys_ss170_ss160gkey;      //程式gkey
              //mod_row["sys_ss180_menu_obj_name"] = st_sys_menu_obj_name;      //menu_obj_name
              mod_row["sys_ss170_ss165gkey"] = st_ss165gkey;      //群組gkey
              //
              mod_row["sys_ss170_gkey"] = DAC.get_guidkey();
              mod_row["sys_ss170_mkey"] = DAC.get_guidkey();
              mod_row["sys_ss170_trusr"] = UserName;
              tb_sys_ss170.Rows.Add(mod_row);
              sys_ss170Dao.Insertbalog(conn, thistran, "sys_ss170", st_ActKey, UserName);
              sys_ss170Dao.Insertbtlog(conn, thistran, "sys_ss170", DAC.GetStringValue(mod_row["sys_ss170_gkey"]), "I", UserName, st_sys_ss160_buttonno + " " + st_sys_ss160_button);
              st_ActKey = DAC.get_guidkey();

            }  //sel_rows.Length == 0 insert
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_sys_ss170);
        thistran.Commit();
        bl_updateok = true;
        hh_GridCtrl_ss165.Value = "ser";
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
        sys_ss170Dao.Dispose();
        tb_sys_ss170.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }

    protected void bt_QUT_ss175_Click(object sender, EventArgs e)
    {
      SetDisable_ss175();
      SetSerMod_ss165();
    }

    protected void bt_DTL_ss165_Click(object sender, EventArgs e)
    {
      SetDisable_ss165();
      SetSerMod_ss175();
      Session["fmsys_ss165_gr_GridView_sys_ss165_SelectedIndex"] = gr_GridView_sys_ss165.SelectedIndex;
      Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"] = gr_GridView_sys_ss165.PageIndex;
    }

    protected void tr_funcView_SelectedNodeChanged(object sender, EventArgs e)
    {
      Session["fmsys_ss165_gr_GridView_sys_ss165_SelectedIndex"] = gr_GridView_sys_ss165.SelectedIndex;
      Session["fmsys_ss165_gr_GridView_sys_ss165_PageIndex"] = gr_GridView_sys_ss165.PageIndex;
    }

    protected void gr_GridView_sys_ss175_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_sys_ss175.PageIndex = e.NewPageIndex;
    }

  }
}