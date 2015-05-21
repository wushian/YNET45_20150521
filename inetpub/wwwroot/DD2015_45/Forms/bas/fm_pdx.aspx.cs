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
  public partial class fm_pdx : FormBase
  {
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    string st_object_func = "";    //"UNpdbpdp1" 與權限相關
    public string st_dd_apx = "";  //UNpdx   與apx 相關
    public string st_dd_table = "";  //dcnews     與table 相關 
    string st_ren_head = "";                //DC         與單號相關 
    string st_ren_yymmtext = "";   //""         與單號相關 
    string st_ren_cls = "";        //ren        與單號cls相關 
    string st_ren_cos = "";        //1          與單號cos相關 
    int in_ren_len = 0;            //6          與單號流水號 
    string st_sub_t01 = "";        //*          T01預設值 *=OPEN 

    protected void Page_Load(object sender, EventArgs e)
    {
      //檢查權限&狀態
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      //
      st_object_func = DAC.GetStringValue(Session["pdx_object_func"]);    //與權限相關
      st_dd_apx = DAC.GetStringValue(Session["pdx_dd_apx"]);              //與apx 相關
      st_dd_table = DAC.GetStringValue(Session["pdx_dd_table"]);           //與table 相關 
      st_ren_head = DAC.GetStringValue(Session["pdx_ren_head"]);            //與單號相關 
      st_ren_yymmtext = DAC.GetStringValue(Session["pdx_ren_yymmtext"]);    //與單號相關 
      st_ren_cls = DAC.GetStringValue(Session["pdx_ren_cls"]);            //與單號cls相關 
      st_ren_cos = DAC.GetStringValue(Session["pdx_ren_cos"]);             //與單號cos相關 
      in_ren_len = DAC.GetInt16Value(Session["pdx_ren_len"]);              //與單號流水號 
      st_sub_t01 = DAC.GetStringValue(Session["pdx_sub_t01"]);             //小類功能分類   (公司介紹CM),購物須知(BC),入會說明(MD),連絡我們(CU)
      if (st_sub_t01 == "*") { st_sub_t01 = ""; }
      gr_GridView_pdpdx.DataKeyNames = new string[] { st_dd_table + "_gkey" };
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fm" + st_dd_table + "_CmdQueryS"] = CmdQueryS;
          Set_Control();
          SetSerMod();
          if (DAC.GetStringValue(Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_GridGkey"]) != "")
          {
            gr_GridView_pdpdx.PageIndex = DAC.GetInt16Value(Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_PageIndex"]);
            gr_GridView_pdpdx.SelectedIndex = DAC.GetInt16Value(Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_SelectedIndex"]);
            hh_GridGkey.Value = DAC.GetStringValue(Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_GridGkey"]);
          }
          //
          BindNew(true);
          Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_PageIndex"] = gr_GridView_pdpdx.PageIndex;
          Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_SelectedIndex"] = gr_GridView_pdpdx.SelectedIndex;


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

    protected void Page_PreRender(object sender, EventArgs e)
    {

    }

    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      this.Page.Title = FunctionName;
      gr_GridView_pdpdx.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      sFN.SetFormControlsText(this.Form, PublicVariable.LangType, ApVer, st_dd_apx, st_dd_table);
    }

    private void ClearText()
    {
      //
      tx_pdpdx_BKNUM.Text = "";  //類別編號
      tx_pdpdx_BKACC.Text = "";  //會計科目
      tx_pdpdx_BKDSC.Text = "";  //類別說明
      tx_pdpdx_BKNAM.Text = "";  //類別名稱
      tx_pdpdx_BKNAC.Text = "";  //類別名稱
      tx_pdpdx_BKNAE.Text = "";  //類別名稱
      tx_pdpdx_BKNAV.Text = "";  //類別名稱

      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      //
      clsGV.SetTextBoxEditAlert(ref lb_pdpdx_BKNUM, ref tx_pdpdx_BKNUM, false);  //類別編號
      clsGV.SetTextBoxEditAlert(ref lb_pdpdx_BKNAM, ref tx_pdpdx_BKNAM, false);  //類別名稱
      //
      clsGV.TextBox_Set(ref tx_pdpdx_BKNUM, false);   //類別編號
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAM, false);   //類別名稱
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAC, false);   //類別名稱
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAE, false);   //類別名稱
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAV, false);   //類別名稱
      //
      clsGV.TextBox_Set(ref tx_pdpdx_BKACC, false);   //會計科目
      clsGV.TextBox_Set(ref tx_pdpdx_BKDSC, false);   //類別說明
      //
      clsGV.SetControlShowAlert(ref lb_pdpdx_BKNUM, ref tx_pdpdx_BKNUM, true);  // 類別編號
      clsGV.SetControlShowAlert(ref lb_pdpdx_BKNAM, ref tx_pdpdx_BKNAM, true);  // 類別名稱
      //
      //bt_NEW.Visible = true;
      //bt_MOD.Visible = true;
      ////bt_MODALL.Visible = true;
      //bt_SER.Visible = true;
      //bt_DEL.Visible = true;
      //bt_PRN.Visible = true;
      //bt_SAV.Visible = false;
      //bt_CAN.Visible = false;
      //bt_QUT.Visible = true;
      ////
      //bt_DEL.ClientSideEvents.Click = "ClientCommand";
      //bt_MOD.ClientSideEvents.Click = "ClientCommand";
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      gr_GridView_pdpdx.Enabled = true;
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_pdpdx_BKNUM, true);  //類別編號
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAM, true);  //類別名稱
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAC, true);  //類別名稱
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAE, true);  //類別名稱
      clsGV.TextBox_Set(ref tx_pdpdx_BKNAV, true);  //類別名稱
      //
      clsGV.TextBox_Set(ref tx_pdpdx_BKACC, true);  //會計科目
      clsGV.TextBox_Set(ref tx_pdpdx_BKDSC, true);  //類別說明
      // 
      clsGV.SetTextBoxEditAlert(ref lb_pdpdx_BKNUM, ref tx_pdpdx_BKNUM, true);  // 類別編號
      clsGV.SetTextBoxEditAlert(ref lb_pdpdx_BKNAM, ref tx_pdpdx_BKNAM, true);  // 類別名稱
      //
      //bt_NEW.Visible = false;
      //bt_MOD.Visible = false;
      ////bt_MODALL.Visible = false;
      //bt_SER.Visible = false;
      //bt_DEL.Visible = false;
      //bt_PRN.Visible = false;
      //bt_SAV.Visible = true;
      //bt_CAN.Visible = true;
      //bt_QUT.Visible = false;
      ////
      //bt_DEL.ClientSideEvents.Click = "";
      //bt_MOD.ClientSideEvents.Click = "";
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      gr_GridView_pdpdx.Enabled = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      tx_pdpdx_BKNUM.Text = DAC.GetStringValue(CurRow[st_dd_table + "_BKNUM"]);  //類別編號
      tx_pdpdx_BKNAM.Text = DAC.GetStringValue(CurRow[st_dd_table + "_BKNAM"]);  //類別名稱
      tx_pdpdx_BKNAC.Text = DAC.GetStringValue(CurRow[st_dd_table + "_BKNAC"]);  //類別名稱
      tx_pdpdx_BKNAE.Text = DAC.GetStringValue(CurRow[st_dd_table + "_BKNAE"]);  //類別名稱
      tx_pdpdx_BKNAV.Text = DAC.GetStringValue(CurRow[st_dd_table + "_BKNAV"]);  //類別名稱
      tx_pdpdx_BKACC.Text = DAC.GetStringValue(CurRow[st_dd_table + "_BKACC"]);  //會計科目
      tx_pdpdx_BKDSC.Text = DAC.GetStringValue(CurRow[st_dd_table + "_BKDSC"]);  //類別說明
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow[st_dd_table + "_mkey"]);
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
        CmdQueryS = (OleDbCommand)Session["fm" + st_dd_table + "_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = "";
      }
      //
      DataTable tb_pdpdx = new DataTable();
      DAC pdpdxDao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = pdpdxDao.GetDataAdapter(ApVer, st_dd_apx, st_dd_table, st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " a.BKNUM ");
      ad_DataDataAdapter.Fill(tb_pdpdx);
      //
      if (tb_pdpdx.Rows.Count > 0)
      {
        //bt_05.ClientSideEvents.Click = "ClientCommand";
        //bt_04.ClientSideEvents.Click = "ClientCommand";
      }
      else
      {
        //bt_05.ClientSideEvents.Click = "";
        //bt_04.ClientSideEvents.Click = "";
      }
      gr_GridView_pdpdx.DataSource = tb_pdpdx;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_pdpdx = clsGV.BindGridView(gr_GridView_pdpdx, tb_pdpdx, hh_GridCtrl, ref hh_GridGkey, "fm" + st_dd_table + "_gr_GridView_" + st_dd_table);
      gr_GridView_pdpdx.DataBind();
      SelDataKey = st_dd_table + "_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_pdpdx.Select(SelDataKey);
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_GridGkey"] = hh_GridGkey.Value;
          BindText(CurRow);
        }
        else
        {
          hh_GridCtrl.Value = "init";
          ClearText();
        }
      }
      tb_pdpdx.Dispose();
      pdpdxDao.Dispose();
    }

    protected void gr_GridView_pdpdx_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_pdpdx.PageIndex = e.NewPageIndex;
      Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_PageIndex"] = gr_GridView_pdpdx.PageIndex;
      hh_GridGkey.Value = gr_GridView_pdpdx.DataKeys[gr_GridView_pdpdx.SelectedIndex].Value.ToString();

    }

    protected void gr_GridView_pdpdx_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_pdpdx.Enabled)
      {
        SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_PageIndex"] = gr_GridView_pdpdx.PageIndex;
        Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_SelectedIndex"] = gr_GridView_pdpdx.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_pdpdx_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_PageIndex"] = gr_GridView_pdpdx.PageIndex;
      Session["fm" + st_dd_table + "_gr_GridView_" + st_dd_table + "_SelectedIndex"] = gr_GridView_pdpdx.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_pdpdx.DataKeys[gr_GridView_pdpdx.SelectedIndex].Value.ToString();
      SetSerMod();
    }

    protected void bt_02_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
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
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_pdpdx_BKNUM').focus(); </script>";
      }
    }

    protected void bt_04_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
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
        tx_pdpdx_BKNUM.Enabled = false;
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_pdpdx_BKNAM').focus(); </script>";
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

    protected void bt_05_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
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
        DAC pdpdxDao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = st_dd_table + "_gkey='" + hh_GridGkey.Value + "' and " + st_dd_table + "_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_pdpdx = new DataTable();
        DbDataAdapter da_ADP = pdpdxDao.GetDataAdapter(ApVer, st_dd_apx, st_dd_table, st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_pdpdx);
        DataRow[] DelRow = tb_pdpdx.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            pdpdxDao.Insertbalog(conn, thistran, st_dd_table, hh_ActKey.Value, hh_GridGkey.Value);
            pdpdxDao.Insertbtlog(conn, thistran, st_dd_table, DAC.GetStringValue(DelRow[0][st_dd_table + "_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0][st_dd_table + "_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_pdpdx);
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
            pdpdxDao.Dispose();
            tb_pdpdx.Dispose();
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
        tb_pdpdx.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_pdpdx = clsGV.SetGridCursor("del", gr_GridView_pdpdx, -2);
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
      string st_tempgkey = "";
      bool bl_insok = false, bl_updateok = false;
      //
      Set_Control();
      if (ServerEditCheck(ref st_ckerrmsg))
      {
        DAC pdpdxDao = new DAC(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = st_dd_table + "_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //檢查重複
            if (pdpdxDao.IsExists(st_dd_table, "BKNUM", tx_pdpdx_BKNUM.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_pdpdx_BKNUM.Text + ",已存在.");
            }
            else
            {
              DataTable tb_pdpdx_ins = new DataTable();
              DbDataAdapter da_ADP_ins = pdpdxDao.GetDataAdapter(ApVer, st_dd_apx, st_dd_table, st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_pdpdx_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_pdpdx_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row[st_dd_table + "_gkey"] = st_tempgkey;       // 
                ins_row[st_dd_table + "_mkey"] = DAC.get_guidkey(); //
                ins_row[st_dd_table + "_BKNUM"] = tx_pdpdx_BKNUM.Text.Trim();       // 類別編號
                ins_row[st_dd_table + "_BKNAM"] = tx_pdpdx_BKNAM.Text.Trim();       // 類別名稱
                ins_row[st_dd_table + "_BKNAC"] = tx_pdpdx_BKNAC.Text.Trim();       // 類別名稱
                ins_row[st_dd_table + "_BKNAE"] = tx_pdpdx_BKNAE.Text.Trim();       // 類別名稱
                ins_row[st_dd_table + "_BKNAV"] = tx_pdpdx_BKNAV.Text.Trim();       // 類別名稱

                ins_row[st_dd_table + "_BKACC"] = tx_pdpdx_BKACC.Text.Trim();       // 會計科目
                ins_row[st_dd_table + "_BKDSC"] = tx_pdpdx_BKDSC.Text.Trim();       // 類別說明
                ins_row[st_dd_table + "_BKCLS"] = st_object_func;       // 類別代號

                ins_row[st_dd_table + "_trusr"] = UserGkey;  //
                tb_pdpdx_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_pdpdx_ins);
                pdpdxDao.Insertbalog(conn, thistran, st_dd_table, hh_ActKey.Value, hh_GridGkey.Value);
                pdpdxDao.Insertbtlog(conn, thistran, st_dd_table, DAC.GetStringValue(ins_row[st_dd_table + "_gkey"]), "I", UserName, DAC.GetStringValue(ins_row[st_dd_table + "_gkey"]));
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
                pdpdxDao.Dispose();
                tb_pdpdx_ins.Dispose();
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
            if (pdpdxDao.IsExists(st_dd_table, "BKNUM", tx_pdpdx_BKNUM.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_pdpdx_BKNUM.Text + ",已存在.");
            }
            else
            {
              DataTable tb_pdpdx_mod = new DataTable();
              DbDataAdapter da_ADP_mod = pdpdxDao.GetDataAdapter(ApVer, st_dd_apx, st_dd_table, st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_pdpdx_mod);
              st_SelDataKey = st_dd_table + "_gkey='" + hh_GridGkey.Value + "' and " + st_dd_table + "_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_pdpdx_mod.Select(st_SelDataKey);
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

                  mod_row[st_dd_table + "_BKNUM"] = tx_pdpdx_BKNUM.Text.Trim();       // 類別編號
                  mod_row[st_dd_table + "_BKNAM"] = tx_pdpdx_BKNAM.Text.Trim();       // 類別名稱
                  mod_row[st_dd_table + "_BKNAC"] = tx_pdpdx_BKNAC.Text.Trim();       // 類別名稱
                  mod_row[st_dd_table + "_BKNAE"] = tx_pdpdx_BKNAE.Text.Trim();       // 類別名稱
                  mod_row[st_dd_table + "_BKNAV"] = tx_pdpdx_BKNAV.Text.Trim();       // 類別名稱
                  mod_row[st_dd_table + "_BKACC"] = tx_pdpdx_BKACC.Text.Trim();       // 會計科目
                  mod_row[st_dd_table + "_BKDSC"] = tx_pdpdx_BKDSC.Text.Trim();       // 類別說明

                  mod_row[st_dd_table + "_mkey"] = DAC.get_guidkey();        //
                  mod_row[st_dd_table + "_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_pdpdx_mod);
                  pdpdxDao.Insertbalog(conn, thistran, st_dd_table, hh_ActKey.Value, hh_GridGkey.Value);
                  pdpdxDao.Insertbtlog(conn, thistran, st_dd_table, DAC.GetStringValue(mod_row[st_dd_table + "_BKNUM"]), "M", UserName, DAC.GetStringValue(mod_row[st_dd_table + "_BKNUM"]) + " " + DAC.GetStringValue(mod_row[st_dd_table + "_BKNAM"]));
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
                  pdpdxDao.Dispose();
                  tb_pdpdx_mod.Dispose();
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
        pdpdxDao.Dispose();
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

      ret = DataCheck.cIsStrEmptyChk(ret, tx_pdpdx_BKNUM.Text, lb_pdpdx_BKNUM.Text, ref sMsg, PublicVariable.LangType, sFN);  //類別編號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_pdpdx_BKNAM.Text, lb_pdpdx_BKNAM.Text, ref sMsg, PublicVariable.LangType, sFN);  //類別名稱
      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_pdpdx_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      //string st_datavalue = "";
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
      }
    }

 
  }
}