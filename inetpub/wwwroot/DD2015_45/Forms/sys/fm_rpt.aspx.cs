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
  public partial class fm_rpt : FormBase
  {

    public string st_gkey = "";
    string st_object_func = "sys_rpt";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";

    protected void Page_Load(object sender, EventArgs e)
    {
      //檢查權限&狀態
      li_Msg.Text = "";
      li_AccMsg.Text = "";

      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        st_gkey = Server.HtmlDecode(DAC.GetStringValue(Request["obj_name"]));
        hh_qkey.Value = st_gkey;
        if (!IsPostBack)
        {
          dr_sys_rpt_rpt_Page = sFN.DropDownListFromClasses(ref dr_sys_rpt_rpt_Page, "rpt_Page", "class_text", "class_type");
          dr_sys_rpt_rpt_comp = sFN.DropDownListFromClasses(ref dr_sys_rpt_rpt_comp, "rpt_comp", "class_text", "class_type");
          dr_sys_rpt_rpt_title = sFN.DropDownListFromClasses(ref dr_sys_rpt_rpt_title, "rpt_title", "class_text", "class_type");

          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmsys_rpt_CmdQueryS"] = CmdQueryS;
          Set_Control();
          SetSerMod();
          BindNew(true);
          Session["fmsys_rpt_gr_GridView_sys_rpt_PageIndex"] = gr_GridView_sys_rpt.PageIndex;
          Session["fmsys_rpt_gr_GridView_sys_rpt_SelectedIndex"] = gr_GridView_sys_rpt.SelectedIndex;
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
      TreeView1.Nodes.Clear();
      TreeView1 = sFN.SetTreeView_sys_menu(TreeView1, "fm_rpt.aspx?obj_name=", LangType, UserId);
      TreeView1.CollapseAll();
      if (st_gkey != "") sFN.TreeViewExpandByValue(ref TreeView1, st_gkey);
    }

    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      gr_GridView_sys_rpt.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNsys_rpt", "sys_rpt");
    }

    private void ClearText()
    {
      //
      tx_sys_rpt_obj_name.Text = "";  //功能物件
      tx_sys_rpt_rpt_source.Text = "";  //資料來源
      dr_sys_rpt_rpt_Page.SelectedIndex = -1;  //使用頁面
      tx_sys_rpt_rpt_file.Text = "";  //報表檔名
      tx_sys_rpt_rpt_name.Text = "";  //報表名稱
      dr_sys_rpt_rpt_comp.SelectedIndex = -1;  //公司類別
      dr_sys_rpt_rpt_title.SelectedIndex = -1;  //表頭設定
      tx_sys_rpt_rpt_dates1.Text = "";  //開始日01
      tx_sys_rpt_rpt_datee1.Text = "";  //結束日01
      tx_sys_rpt_rpt_dates2.Text = "";  //開始日02
      tx_sys_rpt_rpt_datee2.Text = "";  //結束日02

      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      //
      //
      clsGV.TextBox_Set(ref tx_sys_rpt_obj_name, false);   //功能物件
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_source, false);   //資料來源
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_Page, ref tx_sys_rpt_rpt_Page, false);   //使用頁面
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_file, false);   //報表檔名
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_name, false);   //報表名稱
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_comp, ref tx_sys_rpt_rpt_comp, false);   //公司類別
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_title, ref tx_sys_rpt_rpt_title, false);   //表頭設定
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates1, false);   //開始日01
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee1, false);   //結束日01
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates2, false);   //開始日02
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee2, false);   //結束日02
      //
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_sys_rpt.Enabled = true;
      //gr_GridView_sys_rpt.Columns[0].Visible=true;
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_sys_rpt_obj_name, false);  //功能物件
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_source, true);  //資料來源
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_Page, ref tx_sys_rpt_rpt_Page, true);   //使用頁面
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_file, false);  //報表檔名
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_name, true);  //報表名稱
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_comp, ref tx_sys_rpt_rpt_comp, true);   //公司類別
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_title, ref tx_sys_rpt_rpt_title, true);   //表頭設定
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates1, true);  //開始日01
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee1, true);  //結束日01
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates2, true);  //開始日02
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee2, true);  //結束日02
      // 
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_sys_rpt.Enabled = false;
      //gr_GridView_sys_rpt.Columns[0].Visible = false;
    }

    private void SetEditModAll()
    {
      // 
      clsGV.TextBox_Set(ref tx_sys_rpt_obj_name, false);  //功能物件
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_source, false);  //資料來源
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_Page, ref tx_sys_rpt_rpt_Page, false);   //使用頁面
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_file, false);  //報表檔名
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_name, false);  //報表名稱
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_comp, ref tx_sys_rpt_rpt_comp, false);   //公司類別
      clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_title, ref tx_sys_rpt_rpt_title, false);   //表頭設定
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates1, false);  //開始日01
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee1, false);  //結束日01
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates2, false);  //開始日02
      clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee2, false);  //結束日02
      // 
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);

      //
      gr_GridView_sys_rpt.Enabled = true;
      //gr_GridView_sys_rpt.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      //
      tx_sys_rpt_obj_name.Text = DAC.GetStringValue(CurRow["sys_rpt_obj_name"]);  //功能物件
      tx_sys_rpt_rpt_source.Text = DAC.GetStringValue(CurRow["sys_rpt_rpt_source"]);  //資料來源
      dr_sys_rpt_rpt_Page = sFN.SetDropDownList(ref dr_sys_rpt_rpt_Page, DAC.GetStringValue(CurRow["sys_rpt_rpt_Page"]));  //使用頁面
      tx_sys_rpt_rpt_file.Text = DAC.GetStringValue(CurRow["sys_rpt_rpt_file"]);  //報表檔名
      tx_sys_rpt_rpt_name.Text = DAC.GetStringValue(CurRow["sys_rpt_rpt_name"]);  //報表名稱
      dr_sys_rpt_rpt_comp = sFN.SetDropDownList(ref dr_sys_rpt_rpt_comp, DAC.GetStringValue(CurRow["sys_rpt_rpt_comp"]));  //公司類別
      dr_sys_rpt_rpt_title = sFN.SetDropDownList(ref dr_sys_rpt_rpt_title, DAC.GetStringValue(CurRow["sys_rpt_rpt_title"]));  //表頭設定
      tx_sys_rpt_rpt_dates1.Text = DAC.GetStringValue(CurRow["sys_rpt_rpt_dates1"]);  //開始日01
      tx_sys_rpt_rpt_datee1.Text = DAC.GetStringValue(CurRow["sys_rpt_rpt_datee1"]);  //結束日01
      tx_sys_rpt_rpt_dates2.Text = DAC.GetStringValue(CurRow["sys_rpt_rpt_dates2"]);  //開始日02
      tx_sys_rpt_rpt_datee2.Text = DAC.GetStringValue(CurRow["sys_rpt_rpt_datee2"]);  //結束日02
      //
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["sys_rpt_mkey"]);
      //
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      CmdQueryS.CommandText = " and a.obj_name='" + st_gkey + "' ";
      DataTable tb_sys_rpt = new DataTable();
      DAC_rpt sys_rptDao = new DAC_rpt(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = sys_rptDao.GetDataAdapter(ApVer, "UNsys_rpt", "sys_rpt", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "sys_rpt_rpt_file");
      ad_DataDataAdapter.Fill(tb_sys_rpt);
      //
      if (tb_sys_rpt.Rows.Count > 0)
      {
        bt_05.OnClientClick = "return btnDEL_c()";
        bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        bt_05.OnClientClick = "return btnDEL0_c()";
        bt_04.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_sys_rpt.DataSource = tb_sys_rpt;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_sys_rpt = clsGV.BindGridView(gr_GridView_sys_rpt, tb_sys_rpt, hh_GridCtrl, ref hh_GridGkey, "fmsys_rpt_gr_GridView_sys_rpt");
      gr_GridView_sys_rpt.DataBind();
      SelDataKey = "sys_rpt_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_sys_rpt.Select(SelDataKey);
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
      tb_sys_rpt.Dispose();
      sys_rptDao.Dispose();
    }

    protected void gr_GridView_sys_rpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_sys_rpt.PageIndex = e.NewPageIndex;
      Session["fmsys_rpt_gr_GridView_sys_rpt_PageIndex"] = gr_GridView_sys_rpt.PageIndex;
      hh_GridGkey.Value = gr_GridView_sys_rpt.DataKeys[gr_GridView_sys_rpt.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_sys_rpt_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_sys_rpt.Enabled)
      {
        SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        Session["fmsys_rpt_gr_GridView_sys_rpt_PageIndex"] = gr_GridView_sys_rpt.PageIndex;
        Session["fmsys_rpt_gr_GridView_sys_rpt_SelectedIndex"] = gr_GridView_sys_rpt.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_sys_rpt_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmsys_rpt_gr_GridView_sys_rpt_PageIndex"] = gr_GridView_sys_rpt.PageIndex;
      Session["fmsys_rpt_gr_GridView_sys_rpt_SelectedIndex"] = gr_GridView_sys_rpt.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_sys_rpt.DataKeys[gr_GridView_sys_rpt.SelectedIndex].Value.ToString();
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
        SetEditMod();
        //定義guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(false);
        //tx_sys_rpt_rpt_source.Text = st_gkey;
        tx_sys_rpt_obj_name.Text = st_gkey;
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_sys_rpt_rpt_source').focus(); </script>";
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
        SetEditMod();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(true);
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_sys_rpt_rpt_source').focus(); </script>";
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
        SetEditModAll();
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
        DAC_rpt sys_rptDao = new DAC_rpt(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "sys_rpt_gkey='" + hh_GridGkey.Value + "' and sys_rpt_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_sys_rpt = new DataTable();
        DbDataAdapter da_ADP = sys_rptDao.GetDataAdapter(ApVer, "UNsys_rpt", "sys_rpt", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_sys_rpt);
        DataRow[] DelRow = tb_sys_rpt.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            sys_rptDao.Insertbalog(conn, thistran, "sys_rpt", hh_ActKey.Value, hh_GridGkey.Value);
            sys_rptDao.Insertbtlog(conn, thistran, "sys_rpt", DAC.GetStringValue(DelRow[0]["sys_rpt_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0]["sys_rpt_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_sys_rpt);
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
            sys_rptDao.Dispose();
            tb_sys_rpt.Dispose();
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
        tb_sys_rpt.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_sys_rpt = clsGV.SetGridCursor("del", gr_GridView_sys_rpt, -2);
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
        DAC_rpt sys_rptDao = new DAC_rpt(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {
          if (UpdateDataAll(hh_ActKey.Value, UserId, ref st_dberrmsg))
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
          string st_SelDataKey = "sys_rpt_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //檢查重複
            if (sys_rptDao.IsExists("sys_rpt", "rpt_file", tx_sys_rpt_rpt_file.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_sys_rpt_rpt_file.Text + ",已存在.");
            }
            else
            {
              DataTable tb_sys_rpt_ins = new DataTable();
              DbDataAdapter da_ADP_ins = sys_rptDao.GetDataAdapter(ApVer, "UNsys_rpt", "sys_rpt", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_sys_rpt_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_sys_rpt_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["sys_rpt_gkey"] = st_tempgkey;       // 
                ins_row["sys_rpt_mkey"] = DAC.get_guidkey(); //
                ins_row["sys_rpt_obj_name"] = tx_sys_rpt_obj_name.Text.Trim();       // 功能物件
                ins_row["sys_rpt_rpt_source"] = tx_sys_rpt_rpt_source.Text.Trim();       // 資料來源
                ins_row["sys_rpt_rpt_Page"] = dr_sys_rpt_rpt_Page.SelectedValue;       // 使用頁面
                ins_row["sys_rpt_rpt_file"] = tx_sys_rpt_rpt_file.Text.Trim();       // 報表檔名
                ins_row["sys_rpt_rpt_name"] = tx_sys_rpt_rpt_name.Text.Trim();       // 報表名稱
                ins_row["sys_rpt_rpt_comp"] = dr_sys_rpt_rpt_comp.SelectedValue;       // 公司類別
                ins_row["sys_rpt_rpt_title"] = dr_sys_rpt_rpt_title.SelectedValue;       // 表頭設定
                ins_row["sys_rpt_rpt_dates1"] = tx_sys_rpt_rpt_dates1.Text.Trim();       // 開始日01
                ins_row["sys_rpt_rpt_datee1"] = tx_sys_rpt_rpt_datee1.Text.Trim();       // 結束日01
                ins_row["sys_rpt_rpt_dates2"] = tx_sys_rpt_rpt_dates2.Text.Trim();       // 開始日02
                ins_row["sys_rpt_rpt_datee2"] = tx_sys_rpt_rpt_datee2.Text.Trim();       // 結束日02

                ins_row["sys_rpt_trusr"] = UserGkey;  //
                tb_sys_rpt_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_sys_rpt_ins);
                sys_rptDao.Insertbalog(conn, thistran, "sys_rpt", hh_ActKey.Value, hh_GridGkey.Value);
                sys_rptDao.Insertbtlog(conn, thistran, "sys_rpt", DAC.GetStringValue(ins_row["sys_rpt_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["sys_rpt_gkey"]));
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
                sys_rptDao.Dispose();
                tb_sys_rpt_ins.Dispose();
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
            if (sys_rptDao.IsExists("sys_rpt", "rpt_file", tx_sys_rpt_rpt_file.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_sys_rpt_rpt_file.Text + ",已存在.");
            }
            else
            {
              DataTable tb_sys_rpt_mod = new DataTable();
              DbDataAdapter da_ADP_mod = sys_rptDao.GetDataAdapter(ApVer, "UNsys_rpt", "sys_rpt", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_sys_rpt_mod);
              st_SelDataKey = "sys_rpt_gkey='" + hh_GridGkey.Value + "' and sys_rpt_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_sys_rpt_mod.Select(st_SelDataKey);
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

                  mod_row["sys_rpt_rpt_source"] = tx_sys_rpt_rpt_source.Text.Trim();       // 資料來源
                  mod_row["sys_rpt_rpt_Page"] = dr_sys_rpt_rpt_Page.SelectedValue;       // 使用頁面
                  mod_row["sys_rpt_rpt_file"] = tx_sys_rpt_rpt_file.Text.Trim();       // 報表檔名
                  mod_row["sys_rpt_rpt_name"] = tx_sys_rpt_rpt_name.Text.Trim();       // 報表名稱
                  mod_row["sys_rpt_rpt_comp"] = dr_sys_rpt_rpt_comp.SelectedValue;       // 公司類別
                  mod_row["sys_rpt_rpt_title"] = dr_sys_rpt_rpt_title.SelectedValue;       // 表頭設定
                  mod_row["sys_rpt_rpt_dates1"] = tx_sys_rpt_rpt_dates1.Text.Trim();       // 開始日01
                  mod_row["sys_rpt_rpt_datee1"] = tx_sys_rpt_rpt_datee1.Text.Trim();       // 結束日01
                  mod_row["sys_rpt_rpt_dates2"] = tx_sys_rpt_rpt_dates2.Text.Trim();       // 開始日02
                  mod_row["sys_rpt_rpt_datee2"] = tx_sys_rpt_rpt_datee2.Text.Trim();       // 結束日02

                  mod_row["sys_rpt_mkey"] = DAC.get_guidkey();        //
                  mod_row["sys_rpt_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_sys_rpt_mod);
                  sys_rptDao.Insertbalog(conn, thistran, "sys_rpt", hh_ActKey.Value, hh_GridGkey.Value);
                  sys_rptDao.Insertbtlog(conn, thistran, "sys_rpt", DAC.GetStringValue(mod_row["sys_rpt_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["sys_rpt_gkey"]));
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
                  sys_rptDao.Dispose();
                  tb_sys_rpt_mod.Dispose();
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
        sys_rptDao.Dispose();
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
      tx_sys_rpt_rpt_file.Text = st_gkey + tx_sys_rpt_rpt_source.Text;
      ret = DataCheck.cIsStrEmptyChk(ret, tx_sys_rpt_obj_name.Text, lb_sys_rpt_obj_name.Text, ref sMsg, PublicVariable.LangType, sFN);  //功能物件
      ret = DataCheck.cIsStrEmptyChk(ret, tx_sys_rpt_rpt_source.Text, lb_sys_rpt_rpt_source.Text, ref sMsg, PublicVariable.LangType, sFN);  //資料來源
      ret = DataCheck.cIsStrEmptyChk(ret, tx_sys_rpt_rpt_file.Text, lb_sys_rpt_rpt_file.Text, ref sMsg, PublicVariable.LangType, sFN);  //報表檔名
      ret = DataCheck.cIsStrEmptyChk(ret, tx_sys_rpt_rpt_name.Text, lb_sys_rpt_rpt_name.Text, ref sMsg, PublicVariable.LangType, sFN);  //報表名稱
      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_sys_rpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //資料來源
        if (e.Row.FindControl("tx_sys_rpt_rpt_source02") != null)
        {
          TextBox tx_sys_rpt_rpt_source02 = e.Row.FindControl("tx_sys_rpt_rpt_source02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_source"]).Trim();
          tx_sys_rpt_rpt_source02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_source02, true); } else { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_source02, false); }
        }
        //使用頁面
        if (e.Row.FindControl("dr_sys_rpt_rpt_Page02") != null)
        {
          DropDownList dr_sys_rpt_rpt_Page02 = e.Row.FindControl("dr_sys_rpt_rpt_Page02") as DropDownList;
          TextBox tx_sys_rpt_rpt_Page02 = e.Row.FindControl("tx_sys_rpt_rpt_Page02") as TextBox;
          dr_sys_rpt_rpt_Page02 = sFN.DropDownListFromClasses(ref dr_sys_rpt_rpt_Page02, "rpt_Page", "class_text", "class_type");

          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_Page"]).Trim();
          dr_sys_rpt_rpt_Page02 = sFN.SetDropDownList(ref dr_sys_rpt_rpt_Page02, st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_Page02, ref tx_sys_rpt_rpt_Page02, true); } else { clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_Page02, ref tx_sys_rpt_rpt_Page02, false); }
        }
        //報表檔名
        if (e.Row.FindControl("tx_sys_rpt_rpt_file02") != null)
        {
          TextBox tx_sys_rpt_rpt_file02 = e.Row.FindControl("tx_sys_rpt_rpt_file02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_file"]).Trim();
          tx_sys_rpt_rpt_file02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_file02, true); } else { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_file02, false); }
        }
        //報表名稱
        if (e.Row.FindControl("tx_sys_rpt_rpt_name02") != null)
        {
          TextBox tx_sys_rpt_rpt_name02 = e.Row.FindControl("tx_sys_rpt_rpt_name02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_name"]).Trim();
          tx_sys_rpt_rpt_name02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_name02, true); } else { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_name02, false); }
        }
        //公司類別
        if (e.Row.FindControl("dr_sys_rpt_rpt_comp02") != null)
        {
          DropDownList dr_sys_rpt_rpt_comp02 = e.Row.FindControl("dr_sys_rpt_rpt_comp02") as DropDownList;
          TextBox tx_sys_rpt_rpt_comp02 = e.Row.FindControl("tx_sys_rpt_rpt_comp02") as TextBox;
          dr_sys_rpt_rpt_comp02 = sFN.DropDownListFromClasses(ref dr_sys_rpt_rpt_comp02, "rpt_comp", "class_text", "class_type");

          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_comp"]).Trim();
          dr_sys_rpt_rpt_comp02 = sFN.SetDropDownList(ref dr_sys_rpt_rpt_comp02, st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_comp02, ref tx_sys_rpt_rpt_comp02, true); } else { clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_comp02, ref tx_sys_rpt_rpt_comp02, false); }
        }
        //表頭設定
        if (e.Row.FindControl("dr_sys_rpt_rpt_title02") != null)
        {
          DropDownList dr_sys_rpt_rpt_title02 = e.Row.FindControl("dr_sys_rpt_rpt_title02") as DropDownList;
          TextBox tx_sys_rpt_rpt_title02 = e.Row.FindControl("tx_sys_rpt_rpt_title02") as TextBox;
          dr_sys_rpt_rpt_title02 = sFN.DropDownListFromClasses(ref dr_sys_rpt_rpt_title02, "rpt_title", "class_text", "class_type");

          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_title"]).Trim();
          dr_sys_rpt_rpt_title02 = sFN.SetDropDownList(ref dr_sys_rpt_rpt_title02, st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_title02, ref tx_sys_rpt_rpt_title02, true); } else { clsGV.Drpdown_Set(ref dr_sys_rpt_rpt_title02, ref tx_sys_rpt_rpt_title02, false); }
        }
        //開始日01
        if (e.Row.FindControl("tx_sys_rpt_rpt_dates102") != null)
        {
          TextBox tx_sys_rpt_rpt_dates102 = e.Row.FindControl("tx_sys_rpt_rpt_dates102") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_dates1"]).Trim();
          tx_sys_rpt_rpt_dates102.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates102, true); } else { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates102, false); }
        }
        //結束日01
        if (e.Row.FindControl("tx_sys_rpt_rpt_datee102") != null)
        {
          TextBox tx_sys_rpt_rpt_datee102 = e.Row.FindControl("tx_sys_rpt_rpt_datee102") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_datee1"]).Trim();
          tx_sys_rpt_rpt_datee102.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee102, true); } else { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee102, false); }
        }
        //開始日02
        if (e.Row.FindControl("tx_sys_rpt_rpt_dates202") != null)
        {
          TextBox tx_sys_rpt_rpt_dates202 = e.Row.FindControl("tx_sys_rpt_rpt_dates202") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_dates2"]).Trim();
          tx_sys_rpt_rpt_dates202.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates202, true); } else { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_dates202, false); }
        }
        //結束日02
        if (e.Row.FindControl("tx_sys_rpt_rpt_datee202") != null)
        {
          TextBox tx_sys_rpt_rpt_datee202 = e.Row.FindControl("tx_sys_rpt_rpt_datee202") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_rpt_rpt_datee2"]).Trim();
          tx_sys_rpt_rpt_datee202.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee202, true); } else { clsGV.TextBox_Set(ref tx_sys_rpt_rpt_datee202, false); }
        }
      }
    }


    protected bool UpdateDataAll(string st_ActKey, string ss101gkey1, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_sys_rpt_gkey = "", st_sys_rpt_mkey = "", st_sys_rpt_rpt_source = "", st_sys_rpt_rpt_Page = "", st_sys_rpt_rpt_file = "", st_sys_rpt_rpt_name = "", st_sys_rpt_rpt_comp = "", st_sys_rpt_rpt_title = "", st_sys_rpt_rpt_dates1 = "", st_sys_rpt_rpt_datee1 = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      //
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView_sys_rpt$ctl";
      CmdQueryS.CommandText = " and a.obj_name='" + hh_qkey.Value + "'";
      DataTable tb_sys_rpt = new DataTable();
      DAC_rpt sys_rptDao = new DAC_rpt(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      DbDataAdapter da_ADP = sys_rptDao.GetDataAdapter(ApVer, "UNsys_rpt", "sys_rpt", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
      da_ADP.Fill(tb_sys_rpt);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_sys_rpt.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_gkey02";
            st_sys_rpt_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //mkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_mkey02";
            st_sys_rpt_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //資料來源
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_rpt_source02";
            st_sys_rpt_rpt_source = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //使用頁面
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$dr_sys_rpt_rpt_Page02";
            st_sys_rpt_rpt_Page = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;
            //報表檔名
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_rpt_file02";
            st_sys_rpt_rpt_file = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //報表名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_rpt_name02";
            st_sys_rpt_rpt_name = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //公司類別
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$dr_sys_rpt_rpt_comp02";
            st_sys_rpt_rpt_comp = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;
            //表頭設定
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$dr_sys_rpt_rpt_title02";
            st_sys_rpt_rpt_title = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;
            //開始日01
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_rpt_dates102";
            st_sys_rpt_rpt_dates1 = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //結束日01
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_rpt_rpt_datee102";
            st_sys_rpt_rpt_datee1 = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_sys_rpt.Select("sys_rpt_gkey='" + st_sys_rpt_gkey + "'");
            st_sys_rpt_rpt_file = st_gkey + st_sys_rpt_rpt_source;
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (
                   (DAC.GetStringValue(mod_row["sys_rpt_rpt_source"]) != st_sys_rpt_rpt_source)
                || (DAC.GetStringValue(mod_row["sys_rpt_rpt_Page"]) != st_sys_rpt_rpt_Page)
                || (DAC.GetStringValue(mod_row["sys_rpt_rpt_file"]) != st_sys_rpt_rpt_file)
                || (DAC.GetStringValue(mod_row["sys_rpt_rpt_name"]) != st_sys_rpt_rpt_name)
                || (DAC.GetStringValue(mod_row["sys_rpt_rpt_comp"]) != st_sys_rpt_rpt_comp)
                || (DAC.GetStringValue(mod_row["sys_rpt_rpt_title"]) != st_sys_rpt_rpt_title)
                || (DAC.GetStringValue(mod_row["sys_rpt_rpt_dates1"]) != st_sys_rpt_rpt_dates1)
                || (DAC.GetStringValue(mod_row["sys_rpt_rpt_datee1"]) != st_sys_rpt_rpt_datee1)
              )
              {
                sys_rptDao.Insertbalog(conn, thistran, "sys_rpt", st_ActKey, st_sys_rpt_gkey);
                sys_rptDao.Insertbtlog(conn, thistran, "sys_rpt", DAC.GetStringValue(mod_row["sys_rpt_gkey"]), "M", ss101gkey1, DAC.GetStringValue(mod_row["sys_rpt_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_rpt_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_rpt_gkey"]));
                mod_row.BeginEdit();
                mod_row["sys_rpt_rpt_source"] = st_sys_rpt_rpt_source;      //資料來源
                mod_row["sys_rpt_rpt_Page"] = st_sys_rpt_rpt_Page;      //使用頁面
                mod_row["sys_rpt_rpt_file"] = st_sys_rpt_rpt_file;      //報表檔名
                mod_row["sys_rpt_rpt_name"] = st_sys_rpt_rpt_name;      //報表名稱
                mod_row["sys_rpt_rpt_comp"] = st_sys_rpt_rpt_comp;      //公司類別
                mod_row["sys_rpt_rpt_title"] = st_sys_rpt_rpt_title;      //表頭設定
                mod_row["sys_rpt_rpt_dates1"] = st_sys_rpt_rpt_dates1;      //開始日01
                mod_row["sys_rpt_rpt_datee1"] = st_sys_rpt_rpt_datee1;      //結束日01
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();  //
              }
            }  //sel_rows.Length == 1
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_sys_rpt);
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
        sys_rptDao.Dispose();
        tb_sys_rpt.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }
  }
}