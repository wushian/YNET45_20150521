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
  public partial class fm_ss102 : FormBase
  {

    string st_object_func = "sys_ss102";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    //int in_PageSize = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
      //檢查權限&狀態
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          dr_ss102_kind = sFN.DropDownListFromClasses(ref dr_ss102_kind, "ss102_kind", "class_value+' '+class_text", "class_value");
          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmss102_CmdQueryS"] = CmdQueryS;
          Set_Control();
          SetSerMod();
          BindNew(true);
          Session["fmss102_gr_GridView_ss102_PageIndex"] = gr_GridView_ss102.PageIndex;
          Session["fmss102_gr_GridView_ss102_SelectedIndex"] = gr_GridView_ss102.SelectedIndex;
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
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      gr_GridView_ss102.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNss102", "ss102");

    }

    private void ClearText()
    {
      //
      dr_ss102_kind.SelectedIndex = -1;  //模塊
      tx_ss102_code.Text = "";  //代號
      tx_ss102_name.Text = "";  //名稱
      tx_ss102_status.Text = "";  //參數值
      tx_ss102_remark.Text = "";  //備注
      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      //
      clsGV.SetTextBoxEditAlert(ref lb_ss102_code, ref tx_ss102_code, false);  //代號
      clsGV.SetTextBoxEditAlert(ref lb_ss102_name, ref tx_ss102_name, false);  //名稱
      clsGV.SetTextBoxEditAlert(ref lb_ss102_status, ref tx_ss102_status, false);  //參數值
      //
      clsGV.Drpdown_Set(ref dr_ss102_kind, ref tx_ss102_kind, false);   //模塊
      clsGV.TextBox_Set(ref tx_ss102_code, false);   //代號
      clsGV.TextBox_Set(ref tx_ss102_name, false);   //名稱
      clsGV.TextBox_Set(ref tx_ss102_status, false);   //參數值
      clsGV.TextBox_Set(ref tx_ss102_remark, false);   //備注
      //
      clsGV.SetControlShowAlert(ref lb_ss102_code, ref tx_ss102_code, true);  // 代號
      clsGV.SetControlShowAlert(ref lb_ss102_name, ref tx_ss102_name, true);  // 名稱
      clsGV.SetControlShowAlert(ref lb_ss102_status, ref tx_ss102_status, true);  // 參數值
      //
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_ss102.Enabled = true;
      gr_GridView_ss102.Columns[0].Visible = true;
    }

    private void SetEditMod()
    {
      // 
      clsGV.Drpdown_Set(ref dr_ss102_kind, ref tx_ss102_kind, true);   //模塊
      clsGV.TextBox_Set(ref tx_ss102_code, true);  //代號
      clsGV.TextBox_Set(ref tx_ss102_name, true);  //名稱
      clsGV.TextBox_Set(ref tx_ss102_status, true);  //參數值
      clsGV.TextBox_Set(ref tx_ss102_remark, true);  //備注
      // 
      clsGV.SetTextBoxEditAlert(ref lb_ss102_code, ref tx_ss102_code, true);  // 代號
      clsGV.SetTextBoxEditAlert(ref lb_ss102_name, ref tx_ss102_name, true);  // 名稱
      clsGV.SetTextBoxEditAlert(ref lb_ss102_status, ref tx_ss102_status, true);  // 參數值
      // 
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);

      gr_GridView_ss102.Enabled = false;
      gr_GridView_ss102.Columns[0].Visible = true;
    }

    private void SetEditModAll()
    {
      // 
      clsGV.Drpdown_Set(ref dr_ss102_kind, ref tx_ss102_kind, false);   //模塊
      clsGV.TextBox_Set(ref tx_ss102_code, false);  //代號
      clsGV.TextBox_Set(ref tx_ss102_name, false);  //名稱
      clsGV.TextBox_Set(ref tx_ss102_status, false);  //參數值
      clsGV.TextBox_Set(ref tx_ss102_remark, false);  //備注
      // 
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_ss102.Enabled = true;
      gr_GridView_ss102.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      //clsFN sFN = new clsFN();
      //
      dr_ss102_kind = sFN.SetDropDownList(ref dr_ss102_kind, DAC.GetStringValue(CurRow["ss102_kind"]));  //模塊
      tx_ss102_code.Text = DAC.GetStringValue(CurRow["ss102_code"]);  //代號
      tx_ss102_name.Text = DAC.GetStringValue(CurRow["ss102_name"]);  //名稱
      tx_ss102_status.Text = DAC.GetStringValue(CurRow["ss102_status"]);  //參數值
      tx_ss102_remark.Text = DAC.GetStringValue(CurRow["ss102_remark"]);  //備注
      //
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["ss102_mkey"]);
      //
      //sFN.Dispose();
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmss102_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = "";
      }
      //
      DataTable tb_ss102 = new DataTable();
      DAC_ss102 ss102Dao = new DAC_ss102(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = ss102Dao.GetDataAdapter(ApVer, "UNss102", "ss102", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " a.kind,a.code ");
      ad_DataDataAdapter.Fill(tb_ss102);
      //
      if (tb_ss102.Rows.Count > 0)
      {
        bt_05.OnClientClick = "return btnDEL_c()";
        bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        bt_05.OnClientClick = "return btnDEL0_c()";
        bt_04.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_ss102.DataSource = tb_ss102;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_ss102 = clsGV.BindGridView(gr_GridView_ss102, tb_ss102, hh_GridCtrl, ref hh_GridGkey, "fmss102_gr_GridView_ss102");
      gr_GridView_ss102.DataBind();
      SelDataKey = "ss102_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_ss102.Select(SelDataKey);
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
      tb_ss102.Dispose();
      ss102Dao.Dispose();
    }

    protected void gr_GridView_ss102_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_ss102.PageIndex = e.NewPageIndex;
      Session["fmss102_gr_GridView_ss102_PageIndex"] = gr_GridView_ss102.PageIndex;
      hh_GridGkey.Value = gr_GridView_ss102.DataKeys[gr_GridView_ss102.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_ss102_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_ss102.Enabled)
      {
        SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        Session["fmss102_gr_GridView_ss102_PageIndex"] = gr_GridView_ss102.PageIndex;
        Session["fmss102_gr_GridView_ss102_SelectedIndex"] = gr_GridView_ss102.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_ss102_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmss102_gr_GridView_ss102_PageIndex"] = gr_GridView_ss102.PageIndex;
      Session["fmss102_gr_GridView_ss102_SelectedIndex"] = gr_GridView_ss102.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_ss102.DataKeys[gr_GridView_ss102.SelectedIndex].Value.ToString();
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
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_dr_ss102_kind').focus(); </script>";
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
        DAC_ss102 ss102Dao = new DAC_ss102(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "ss102_gkey='" + hh_GridGkey.Value + "' and ss102_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_ss102 = new DataTable();
        DbDataAdapter da_ADP = ss102Dao.GetDataAdapter(ApVer, "UNss102", "ss102", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_ss102);
        DataRow[] DelRow = tb_ss102.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            ss102Dao.Insertbalog(conn, thistran, "ss102", hh_ActKey.Value, hh_GridGkey.Value);
            ss102Dao.Insertbtlog(conn, thistran, "ss102", DAC.GetStringValue(DelRow[0]["ss102_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0]["ss102_gkey"]) + " " + DAC.GetStringValue(DelRow[0]["ss102_kind"]) + " " + DAC.GetStringValue(DelRow[0]["ss102_name"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_ss102);
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
            ss102Dao.Dispose();
            tb_ss102.Dispose();
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
        tb_ss102.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_ss102 = clsGV.SetGridCursor("del", gr_GridView_ss102, -2);
          SetSerMod();
          BindNew(true);
        }
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
        DAC_ss102 ss102Dao = new DAC_ss102(conn);
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
          string st_SelDataKey = "ss102_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //檢查重複
            if (ss102Dao.IsExists("ss102", "code", tx_ss102_code.Text.Trim(), ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_ss102_code.Text.Trim() + ",已存在.");
            }
            else
            {
              DataTable tb_ss102_ins = new DataTable();
              DbDataAdapter da_ADP_ins = ss102Dao.GetDataAdapter(ApVer, "UNss102", "ss102", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_ss102_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_ss102_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["ss102_gkey"] = st_tempgkey;       // 
                ins_row["ss102_mkey"] = DAC.get_guidkey(); //
                ins_row["ss102_kind"] = dr_ss102_kind.SelectedValue;       // 模塊
                ins_row["ss102_code"] = tx_ss102_code.Text.Trim();       // 代號
                ins_row["ss102_name"] = tx_ss102_name.Text.Trim();       // 名稱
                ins_row["ss102_status"] = tx_ss102_status.Text.Trim();       // 參數值
                ins_row["ss102_remark"] = tx_ss102_remark.Text.Trim();       // 備注

                ins_row["ss102_trusr"] = UserGkey;  //
                tb_ss102_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_ss102_ins);
                ss102Dao.Insertbalog(conn, thistran, "ss102", hh_ActKey.Value, hh_GridGkey.Value);
                ss102Dao.Insertbtlog(conn, thistran, "ss102", DAC.GetStringValue(ins_row["ss102_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["ss102_gkey"]) + " " + DAC.GetStringValue(ins_row["ss102_kind"]) + " " + DAC.GetStringValue(ins_row["ss102_name"]));

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
                ss102Dao.Dispose();
                tb_ss102_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              //
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
            if (ss102Dao.IsExists("ss102", "code", tx_ss102_code.Text.Trim(), "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_ss102_code.Text.Trim() + ",已存在.");
            }
            else
            {
              DataTable tb_ss102_mod = new DataTable();
              DbDataAdapter da_ADP_mod = ss102Dao.GetDataAdapter(ApVer, "UNss102", "ss102", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_ss102_mod);
              st_SelDataKey = "ss102_gkey='" + hh_GridGkey.Value + "' and ss102_mkey='" + hh_mkey.Value + "'";
              DataRow[] mod_rows = tb_ss102_mod.Select(st_SelDataKey);
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

                  //mod_row["ss102_kind"] =dr_ss102_kind[dr_ss102_kind.SelectedIndex].SelectValue;       // 模塊
                  //mod_row["ss102_kind"] =dr_ss102_kind[dr_ss102_kind.SelectedIndex].SelectdValue;       // 模塊
                  mod_row["ss102_kind"] = dr_ss102_kind.SelectedValue;
                  mod_row["ss102_code"] = tx_ss102_code.Text.Trim();       // 代號
                  mod_row["ss102_name"] = tx_ss102_name.Text.Trim();       // 名稱
                  mod_row["ss102_status"] = tx_ss102_status.Text.Trim();       // 參數值
                  mod_row["ss102_remark"] = tx_ss102_remark.Text.Trim();       // 備注

                  mod_row["ss102_mkey"] = DAC.get_guidkey();        //
                  mod_row["ss102_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_ss102_mod);
                  ss102Dao.Insertbalog(conn, thistran, "ss102", hh_ActKey.Value, hh_GridGkey.Value);
                  ss102Dao.Insertbtlog(conn, thistran, "ss102", DAC.GetStringValue(mod_row["ss102_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["ss102_gkey"]) + " " + DAC.GetStringValue(mod_row["ss102_kind"]) + " " + DAC.GetStringValue(mod_row["ss102_name"]));
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
                  ss102Dao.Dispose();
                  tb_ss102_mod.Dispose();
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
        ss102Dao.Dispose();
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
      clsDataCheck DataCheck = new clsDataCheck();
      ret = true;
      sMsg = "";
      ret = DataCheck.cIsStrEmptyChk(ret, tx_ss102_code.Text, lb_ss102_code.Text, ref sMsg, PublicVariable.LangType, sFN);
      ret = DataCheck.cIsStrEmptyChk(ret, tx_ss102_name.Text, lb_ss102_name.Text, ref sMsg, PublicVariable.LangType, sFN);
      DataCheck.Dispose();
      return ret;
    }

    protected bool UpdateDataAll(string st_ActKey, string ss101gkey1, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_ss102_gkey = "", st_ss102_mkey = "", st_ss102_kind = "", st_ss102_code = "", st_ss102_name = "", st_ss102_status = "", st_ss102_remark = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      //
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView_ss102$ctl";
      CmdQueryS.CommandText = " and 1=1 ";
      DataTable tb_ss102 = new DataTable();
      DAC_ss102 ss102Dao = new DAC_ss102(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      DbDataAdapter da_ADP = ss102Dao.GetDataAdapter(ApVer, "UNss102", "ss102", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
      da_ADP.Fill(tb_ss102);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_ss102.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_ss102_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_ss102_gkey02";
            st_ss102_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //mkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_ss102_mkey02";
            st_ss102_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //模塊
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$dr_ss102_kind02";
            st_ss102_kind = ((DropDownList)FindControl(st_ctrlname)).SelectedValue;
            //代號
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_ss102_code02";
            st_ss102_code = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_ss102_name02";
            st_ss102_name = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //參數值
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_ss102_status02";
            st_ss102_status = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //備注
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_ss102_remark02";
            st_ss102_remark = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_ss102.Select("ss102_gkey='" + st_ss102_gkey + "'");
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (
                   (DAC.GetStringValue(mod_row["ss102_kind"]) != st_ss102_kind)
                || (DAC.GetStringValue(mod_row["ss102_code"]) != st_ss102_code)
                || (DAC.GetStringValue(mod_row["ss102_name"]) != st_ss102_name)
                || (DAC.GetStringValue(mod_row["ss102_status"]) != st_ss102_status)
                || (DAC.GetStringValue(mod_row["ss102_remark"]) != st_ss102_remark)
              )
              {
                ss102Dao.Insertbalog(conn, thistran, "ss102", st_ActKey, st_ss102_gkey);
                ss102Dao.Insertbtlog(conn, thistran, "ss102", DAC.GetStringValue(mod_row["ss102_gkey"]), "M", ss101gkey1, DAC.GetStringValue(mod_row["ss102_gkey"]) + " " + DAC.GetStringValue(mod_row["ss102_gkey"]) + " " + DAC.GetStringValue(mod_row["ss102_gkey"]));
                mod_row.BeginEdit();
                mod_row["ss102_kind"] = st_ss102_kind;      //模塊
                mod_row["ss102_code"] = st_ss102_code;      //代號
                mod_row["ss102_name"] = st_ss102_name;      //名稱
                mod_row["ss102_status"] = st_ss102_status;      //參數值
                mod_row["ss102_remark"] = st_ss102_remark;      //備注
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();  //
              }
            }  //sel_rows.Length == 1
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_ss102);
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
        ss102Dao.Dispose();
        tb_ss102.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }

    protected void gr_GridView_ss102_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      //clsFN sFN = new clsFN();
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //模塊
        if (e.Row.FindControl("dr_ss102_kind02") != null)
        {
          DropDownList dr_ss102_kind02 = e.Row.FindControl("dr_ss102_kind02") as DropDownList;
          TextBox tx_ss102_kind02 = e.Row.FindControl("tx_ss102_kind02") as TextBox;
          dr_ss102_kind02 = sFN.DropDownListFromClasses(ref dr_ss102_kind02, "ss102_kind", "class_value+' '+class_text", "class_value");
          st_datavalue = DAC.GetStringValue(rowView["ss102_kind"]).Trim();
          dr_ss102_kind02 = sFN.SetDropDownList(ref dr_ss102_kind02, st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.Drpdown_Set(ref dr_ss102_kind02, ref tx_ss102_kind02, true); } else { clsGV.Drpdown_Set(ref dr_ss102_kind02, ref tx_ss102_kind02, false); }
        }
        //代號
        if (e.Row.FindControl("tx_ss102_code02") != null)
        {
          TextBox tx_ss102_code02 = e.Row.FindControl("tx_ss102_code02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["ss102_code"]).Trim();
          tx_ss102_code02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_ss102_code02, true); } else { clsGV.TextBox_Set(ref tx_ss102_code02, false); }
        }
        //名稱
        if (e.Row.FindControl("tx_ss102_name02") != null)
        {
          TextBox tx_ss102_name02 = e.Row.FindControl("tx_ss102_name02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["ss102_name"]).Trim();
          tx_ss102_name02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_ss102_name02, true); } else { clsGV.TextBox_Set(ref tx_ss102_name02, false); }
        }
        //參數值
        if (e.Row.FindControl("tx_ss102_status02") != null)
        {
          TextBox tx_ss102_status02 = e.Row.FindControl("tx_ss102_status02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["ss102_status"]).Trim();
          tx_ss102_status02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_ss102_status02, true); } else { clsGV.TextBox_Set(ref tx_ss102_status02, false); }
        }
        //備注
        if (e.Row.FindControl("tx_ss102_remark02") != null)
        {
          TextBox tx_ss102_remark02 = e.Row.FindControl("tx_ss102_remark02") as TextBox;
          st_datavalue = DAC.GetStringValue(rowView["ss102_remark"]).Trim();
          tx_ss102_remark02.Text = st_datavalue;
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_ss102_remark02, true); } else { clsGV.TextBox_Set(ref tx_ss102_remark02, false); }
        }
      }
      //sFN.Dispose();
    }

  }
}