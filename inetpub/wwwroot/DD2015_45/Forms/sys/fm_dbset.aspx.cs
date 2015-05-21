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
  public partial class fm_dbset : FormBase
  {
    public string st_gkey = "";

    string st_object_func = "sys_dbset"; //與權限相關
    //string st_dd_apx = "UNsys_dbset";
    //string st_ren_head = "";       //DC         與單號相關 
    //string st_ren_yymmtext = "";   //""         與單號相關 
    //string st_ren_cls = "";        //ren        與單號cls相關 
    //string st_ren_cos = "";        //1          與單號cos相關 
    //int in_ren_len = 0;            //6          與單號流水號 

    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";

    protected void Page_Load(object sender, EventArgs e)
    {
      //檢查權限&狀態
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      this.Page.Title = StringTable.GetString("sys_dbset 文字辭典");
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        st_gkey = Server.HtmlDecode(DAC.GetStringValue(Request["obj_name"]));
        hh_qkey.Value = st_gkey;
        if (!IsPostBack)
        {
          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmsys_dbset_CmdQueryS"] = CmdQueryS;
          Set_Control();
          SetSerMod();
          BindNew(true);
          Session["fmsys_dbset_gr_GridView_sys_dbset_PageIndex"] = gr_GridView_sys_dbset.PageIndex;
          Session["fmsys_dbset_gr_GridView_sys_dbset_SelectedIndex"] = gr_GridView_sys_dbset.SelectedIndex;
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
      TreeView1 = sFN.SetTreeView_sys_menu(TreeView1, "fm_dbset.aspx?obj_name=", LangType, UserId);
      TreeView1.CollapseAll();
      if (st_gkey != "") sFN.TreeViewExpandByValue(ref TreeView1, st_gkey);
    }

    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      gr_GridView_sys_dbset.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNsys_dbset", "sys_dbset");
    }

    private void ClearText()
    {
      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      //
      //
      //
      //bt_NEW.Visible = true;
      //bt_MOD.Visible = true;
      //bt_MODALL.Visible = true;
      //bt_SER.Visible = true;
      //bt_DEL.Visible = true;
      //bt_PRN.Visible = true;
      //bt_SAV.Visible = false;
      //bt_CAN.Visible = false;
      //bt_QUT.Visible = true;

      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_sys_dbset.Enabled = true;
      //gr_GridView_sys_dbset.Columns[0].Visible=true;
    }

    private void SetEditMod()
    {
      // 
      //bt_NEW.Visible = false;
      //bt_MOD.Visible = false;
      //bt_MODALL.Visible = false;
      //bt_SER.Visible = false;
      //bt_DEL.Visible = false;
      //bt_PRN.Visible = false;
      //bt_SAV.Visible = true;
      //bt_CAN.Visible = true;
      //bt_QUT.Visible = false;
      //
      //bt_DEL.OnClientClick = " return false;";
      //bt_MOD.OnClientClick = " return false;";
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_sys_dbset.Enabled = false;
      //gr_GridView_sys_dbset.Columns[0].Visible = false;
    }

    private void SetEditModAll()
    {
      // 
      //bt_NEW.Visible = false;
      //bt_MOD.Visible = false;
      //bt_MODALL.Visible = false;
      //bt_SER.Visible = false;
      //bt_DEL.Visible = false;
      //bt_PRN.Visible = false;
      bt_SAV.Visible = true;
      bt_CAN.Visible = true;
      bt_QUT.Visible = false;
      //
      //bt_DEL.OnClientClick = " return false;";
      //bt_MOD.OnClientClick = " return false;";
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_sys_dbset.Enabled = true;
      //gr_GridView_sys_dbset.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["sys_dbset_mkey"]);
      //
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      if (hh_qkey.Value == "")
      {
        hh_qkey.Value = "*****";
      }
      CmdQueryS.CommandText = " AND a.DBVER='" + ApVer + "' AND a.DBAPX='" + hh_qkey.Value + "' and a.DBTBL='" + hh_qkey.Value.Substring(2) + "' ";
      //
      DataTable tb_sys_dbset = new DataTable();
      DAC_dbset sys_dbsetDao = new DAC_dbset(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_useapx = hh_qkey.Value;
      if (st_useapx.IndexOf("UNdcbasic_") >= 0)
      {
        st_useapx = "UNdcbasic";
      }
      string st_addunion = @"select b.DBFLD as  sys_dbset_DBFLD ,b.DBCNA as  sys_dbset_DBCNA ,b.DBTNA as  sys_dbset_DBTNA ,b.DBENA as  sys_dbset_DBENA ,b.DBCNA as  sys_dbset_DBVNA ,'' as  sys_dbset_DBDEF ,'0' as  sys_dbset_DBSOR ,b.DBAPX as  sys_dbset_DBAPX ,b.DBTBL as  sys_dbset_DBTBL ,b.DBVER as  sys_dbset_DBVER ,b.DBAPX+b.dbfld as sys_dbset_gkey,b.DBAPX+b.dbfld as sys_dbset_mkey ,b.trcls as sys_dbset_trcls,b.trcrd as sys_dbset_trcrd,b.trmod as sys_dbset_trmod,b.trusr as sys_dbset_trusr 
                             from dbset b  with (nolock)  
                             where 1=1  AND b.DBVER='" + ApVer + "' AND b.DBAPX='" + st_useapx + "' and b.DBTBL='" + st_useapx.Substring(2) + "' and b.DBVER+b.DBAPX+b.DBTBL+b.DBFLD NOT in (select x.DBVER+x.DBAPX+x.DBTBL+x.DBFLD from sys_dbset x ) ";
      //
      ad_DataDataAdapter = sys_dbsetDao.GetDataAdapter(ApVer, "UNsys_dbset", "sys_dbset", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " sys_dbset_DBFLD  ");
      ad_DataDataAdapter.Fill(tb_sys_dbset);
      //
      if (tb_sys_dbset.Rows.Count > 0)
      {
        //bt_DEL.OnClientClick = "return btnDEL_c()";
        //bt_MOD.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        //bt_DEL.OnClientClick = "return btnDEL0_c()";
        //bt_MOD.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_sys_dbset.DataSource = tb_sys_dbset;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_sys_dbset = clsGV.BindGridView(gr_GridView_sys_dbset, tb_sys_dbset, hh_GridCtrl, ref hh_GridGkey, "fmsys_dbset_gr_GridView_sys_dbset");
      gr_GridView_sys_dbset.DataBind();
      SelDataKey = "sys_dbset_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_sys_dbset.Select(SelDataKey);
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          Session["fmsys_dbset_gr_GridView_sys_dbset_GridGkey"] = hh_GridGkey.Value;
          BindText(CurRow);
        }
        else
        {
          hh_GridCtrl.Value = "init";
          ClearText();
        }
      }
      tb_sys_dbset.Dispose();
      sys_dbsetDao.Dispose();
    }

    protected void gr_GridView_sys_dbset_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_sys_dbset.PageIndex = e.NewPageIndex;
      Session["fmsys_dbset_gr_GridView_sys_dbset_PageIndex"] = gr_GridView_sys_dbset.PageIndex;
      hh_GridGkey.Value = gr_GridView_sys_dbset.DataKeys[gr_GridView_sys_dbset.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_sys_dbset_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_sys_dbset.Enabled)
      {
        SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        Session["fmsys_dbset_gr_GridView_sys_dbset_PageIndex"] = gr_GridView_sys_dbset.PageIndex;
        Session["fmsys_dbset_gr_GridView_sys_dbset_SelectedIndex"] = gr_GridView_sys_dbset.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_sys_dbset_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmsys_dbset_gr_GridView_sys_dbset_PageIndex"] = gr_GridView_sys_dbset.PageIndex;
      Session["fmsys_dbset_gr_GridView_sys_dbset_SelectedIndex"] = gr_GridView_sys_dbset.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_sys_dbset.DataKeys[gr_GridView_sys_dbset.SelectedIndex].Value.ToString();
      SetSerMod();
    }



    protected void bt_NEW_Click(object sender, EventArgs e)
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
        //li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_xxxxxx').focus(); </script>";
      }
    }

    protected void bt_MOD_Click(object sender, EventArgs e)
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
      }
    }

    protected void bt_04_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
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
      BindNew(true);
      SetSerMod();
    }
    protected void bt_DEL_Click(object sender, EventArgs e)
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
        DAC_dbset sys_dbsetDao = new DAC_dbset(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "sys_dbset_gkey='" + hh_GridGkey.Value + "' and sys_dbset_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_sys_dbset = new DataTable();
        DbDataAdapter da_ADP = sys_dbsetDao.GetDataAdapter(ApVer, "UNsys_dbset", "sys_dbset", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_sys_dbset);
        DataRow[] DelRow = tb_sys_dbset.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            sys_dbsetDao.Insertbalog(conn, thistran, "sys_dbset", hh_ActKey.Value, hh_GridGkey.Value);
            sys_dbsetDao.Insertbtlog(conn, thistran, "sys_dbset", DAC.GetStringValue(DelRow[0]["sys_dbset_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0]["sys_dbset_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_sys_dbset);
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
            sys_dbsetDao.Dispose();
            tb_sys_dbset.Dispose();
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
        tb_sys_dbset.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_sys_dbset = clsGV.SetGridCursor("del", gr_GridView_sys_dbset, -2);
          SetSerMod();
          BindNew(true);
        }
        //bl_delok
      }
    }

    protected void bt_SAV_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actSAV();
    }

    protected void actSAV()
    {
      string st_ckerrmsg = "";
      string st_dberrmsg = "";
      //
      Set_Control();
      if (ServerEditCheck(ref st_ckerrmsg))
      {
        DAC_dbset sys_dbsetDao = new DAC_dbset(conn);
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
        sys_dbsetDao.Dispose();
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

      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_sys_dbset_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //欄位名稱
        if (e.Row.FindControl("tx_sys_dbset_DBFLD02") != null)
        {
          TextBox tx_sys_dbset_DBFLD02 = e.Row.FindControl("tx_sys_dbset_DBFLD02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_dbset_DBFLD"]).Trim();
          tx_sys_dbset_DBFLD02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_dbset_DBFLD02, false); } else { clsGV.TextBox_Set(ref tx_sys_dbset_DBFLD02, false); }
        }
        //簡體中文
        if (e.Row.FindControl("tx_sys_dbset_DBCNA02") != null)
        {
          TextBox tx_sys_dbset_DBCNA02 = e.Row.FindControl("tx_sys_dbset_DBCNA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_dbset_DBCNA"]).Trim();
          tx_sys_dbset_DBCNA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_dbset_DBCNA02, true); } else { clsGV.TextBox_Set(ref tx_sys_dbset_DBCNA02, false); }
        }
        //繁體中文
        if (e.Row.FindControl("tx_sys_dbset_DBTNA02") != null)
        {
          TextBox tx_sys_dbset_DBTNA02 = e.Row.FindControl("tx_sys_dbset_DBTNA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_dbset_DBTNA"]).Trim();
          tx_sys_dbset_DBTNA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_dbset_DBTNA02, true); } else { clsGV.TextBox_Set(ref tx_sys_dbset_DBTNA02, false); }
        }
        //英文名稱
        if (e.Row.FindControl("tx_sys_dbset_DBENA02") != null)
        {
          TextBox tx_sys_dbset_DBENA02 = e.Row.FindControl("tx_sys_dbset_DBENA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_dbset_DBENA"]).Trim();
          tx_sys_dbset_DBENA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_dbset_DBENA02, true); } else { clsGV.TextBox_Set(ref tx_sys_dbset_DBENA02, false); }
        }
        //越南名稱
        if (e.Row.FindControl("tx_sys_dbset_DBVNA02") != null)
        {
          TextBox tx_sys_dbset_DBVNA02 = e.Row.FindControl("tx_sys_dbset_DBVNA02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_dbset_DBVNA"]).Trim();
          tx_sys_dbset_DBVNA02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_dbset_DBVNA02, true); } else { clsGV.TextBox_Set(ref tx_sys_dbset_DBVNA02, false); }
        }
        //新增預設
        if (e.Row.FindControl("tx_sys_dbset_DBDEF02") != null)
        {
          TextBox tx_sys_dbset_DBDEF02 = e.Row.FindControl("tx_sys_dbset_DBDEF02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_dbset_DBDEF"]).Trim();
          tx_sys_dbset_DBDEF02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_dbset_DBDEF02, true); } else { clsGV.TextBox_Set(ref tx_sys_dbset_DBDEF02, false); }
        }
        //排序預設
        if (e.Row.FindControl("tx_sys_dbset_DBSOR02") != null)
        {
          TextBox tx_sys_dbset_DBSOR02 = e.Row.FindControl("tx_sys_dbset_DBSOR02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["sys_dbset_DBSOR"]).Trim();
          tx_sys_dbset_DBSOR02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_sys_dbset_DBSOR02, true); } else { clsGV.TextBox_Set(ref tx_sys_dbset_DBSOR02, false); }
        }
      }
    }


    protected bool UpdateDataAll(string st_ActKey, string ss101gkey1, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_sys_dbset_gkey = "", st_sys_dbset_mkey = "", st_sys_dbset_DBFLD = "", st_sys_dbset_DBCNA = "", st_sys_dbset_DBTNA = "", st_sys_dbset_DBENA = "", st_sys_dbset_DBVNA = "", st_sys_dbset_DBDEF = "", st_sys_dbset_DBSOR = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      //
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView_sys_dbset$ctl";
      //CmdQueryS.CommandText = " and A.sys_dbset_gkey='" + hh_qkey.Value + "' ";
      CmdQueryS.CommandText = " and a.DBVER='" + ApVer + "' AND a.DBAPX='" + hh_qkey.Value + "' and a.DBTBL='" + hh_qkey.Value.Substring(2) + "' ";
      DataTable tb_sys_dbset = new DataTable();
      DAC_dbset sys_dbsetDao = new DAC_dbset(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      DbDataAdapter da_ADP = sys_dbsetDao.GetDataAdapter(ApVer, "UNsys_dbset", "sys_dbset", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " sys_dbset_DBFLD  ");
      da_ADP.Fill(tb_sys_dbset);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_sys_dbset.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_gkey02";
            st_sys_dbset_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //mkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_mkey02";
            st_sys_dbset_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //欄位名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_DBFLD02";
            st_sys_dbset_DBFLD = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //簡體中文
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_DBCNA02";
            st_sys_dbset_DBCNA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //繁體中文
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_DBTNA02";
            st_sys_dbset_DBTNA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //英文名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_DBENA02";
            st_sys_dbset_DBENA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //越南名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_DBVNA02";
            st_sys_dbset_DBVNA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //新增預設
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_DBDEF02";
            st_sys_dbset_DBDEF = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //排序預設
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_sys_dbset_DBSOR02";
            st_sys_dbset_DBSOR = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_sys_dbset.Select("sys_dbset_gkey='" + st_sys_dbset_gkey + "'");
            if (sel_rows.Length == 0)
            {//原沒有資料 起用新增
              mod_row = tb_sys_dbset.NewRow();
              //
              mod_row["sys_dbset_DBFLD"] = st_sys_dbset_DBFLD;      //欄位名稱
              mod_row["sys_dbset_DBCNA"] = st_sys_dbset_DBCNA;      //簡體中文
              mod_row["sys_dbset_DBTNA"] = st_sys_dbset_DBTNA;      //繁體中文
              mod_row["sys_dbset_DBENA"] = st_sys_dbset_DBENA;      //英文名稱
              mod_row["sys_dbset_DBVNA"] = st_sys_dbset_DBVNA;      //越南名稱
              mod_row["sys_dbset_DBDEF"] = st_sys_dbset_DBDEF;      //新增預設
              mod_row["sys_dbset_DBSOR"] = st_sys_dbset_DBSOR;      //排序預設
              mod_row["sys_dbset_DBTBL"] = hh_qkey.Value.Substring(2);           //TABLE名
              mod_row["sys_dbset_DBAPX"] = hh_qkey.Value;          //程式名稱
              mod_row["sys_dbset_DBVER"] = ApVer;                   //版本編跑
              //
              mod_row["sys_dbset_gkey"] = DAC.get_guidkey();
              mod_row["sys_dbset_mkey"] = DAC.get_guidkey();
              mod_row["sys_dbset_trusr"] = UserGkey;

              tb_sys_dbset.Rows.Add(mod_row);
              sys_dbsetDao.Insertbalog(conn, thistran, "sys_ss160", st_ActKey, UserName);
              sys_dbsetDao.Insertbtlog(conn, thistran, "sys_dbset", DAC.GetStringValue(mod_row["sys_dbset_gkey"]), "I", ss101gkey1, DAC.GetStringValue(mod_row["sys_dbset_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_dbset_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_dbset_gkey"]));
              st_ActKey = DAC.get_guidkey();
            }
            else if (sel_rows.Length == 1)
            { //原有資料 起用 有更正
              mod_row = sel_rows[0];
              if (
                   (DAC.GetStringValue(mod_row["sys_dbset_DBFLD"]) != st_sys_dbset_DBFLD)
                || (DAC.GetStringValue(mod_row["sys_dbset_DBCNA"]) != st_sys_dbset_DBCNA)
                || (DAC.GetStringValue(mod_row["sys_dbset_DBTNA"]) != st_sys_dbset_DBTNA)
                || (DAC.GetStringValue(mod_row["sys_dbset_DBENA"]) != st_sys_dbset_DBENA)
                || (DAC.GetStringValue(mod_row["sys_dbset_DBVNA"]) != st_sys_dbset_DBVNA)
                || (DAC.GetStringValue(mod_row["sys_dbset_DBDEF"]) != st_sys_dbset_DBDEF)
                || (DAC.GetStringValue(mod_row["sys_dbset_DBSOR"]) != st_sys_dbset_DBSOR)
              )
              {
                sys_dbsetDao.Insertbalog(conn, thistran, "sys_dbset", st_ActKey, st_sys_dbset_gkey);
                sys_dbsetDao.Insertbtlog(conn, thistran, "sys_dbset", DAC.GetStringValue(mod_row["sys_dbset_gkey"]), "M", ss101gkey1, DAC.GetStringValue(mod_row["sys_dbset_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_dbset_gkey"]) + " " + DAC.GetStringValue(mod_row["sys_dbset_gkey"]));
                mod_row.BeginEdit();
                mod_row["sys_dbset_DBFLD"] = st_sys_dbset_DBFLD;      //欄位名稱
                mod_row["sys_dbset_DBCNA"] = st_sys_dbset_DBCNA;      //簡體中文
                mod_row["sys_dbset_DBTNA"] = st_sys_dbset_DBTNA;      //繁體中文
                mod_row["sys_dbset_DBENA"] = st_sys_dbset_DBENA;      //英文名稱
                mod_row["sys_dbset_DBVNA"] = st_sys_dbset_DBVNA;      //越南名稱
                mod_row["sys_dbset_DBDEF"] = st_sys_dbset_DBDEF;      //新增預設
                mod_row["sys_dbset_DBSOR"] = st_sys_dbset_DBSOR;      //排序預設
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();  //
              }
            }  //sel_rows.Length == 1
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_sys_dbset);
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
        sys_dbsetDao.Dispose();
        tb_sys_dbset.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }
  }
}