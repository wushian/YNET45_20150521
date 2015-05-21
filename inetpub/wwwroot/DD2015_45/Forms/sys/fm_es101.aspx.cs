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
  public partial class fm_es101 :FormBase 
  {
    string st_object_func = "UNes101";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    //int in_PageSize = 10;
    //
    public string st_dd_apx = "UNes101";         //UNdcnews   與apx 相關
    public string st_dd_table = "es101";       //dcnews     與table 相關 
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
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {

          dr_es101_sex = sFN.DropDownListFromClasses(ref dr_es101_sex, "UNbcvp_BCSEX", "class_text", "class_value");

          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmes101_CmdQueryS"] = CmdQueryS;
          Set_Control();
          BindNew(true);
          SetSerMod();
          //
          Session["fmes101_gr_GridView_es101_PageIndex"] = gr_GridView_es101.PageIndex;
          Session["fmes101_gr_GridView_es101_SelectedIndex"] = gr_GridView_es101.SelectedIndex;
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
      this.Page.Title = FunctionName;
      gr_GridView_es101.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      sFN.SetFormControlsText(this.Form, PublicVariable.LangType, ApVer, "UNes101", "es101");
    }

    private void ClearText()
    {
      //
      tx_es101_no.Text = "";  //員工編號
      tx_es101_cname.Text = "";  //中文姓名
      tx_es101_ename.Text = "";  //英文姓名
      dr_es101_sex.SelectedIndex = -1;  //性　　別
      tx_es101_tel2.Text = "";  //行動電話
      tx_es101_email1.Text = "";  //e-mail　
      tx_es101_blood.Text = "";  //血　　型
      tx_es101_indate.Text = "";  //到職日期
      tx_es101_es001gkey.Text = "";  //部　　門
      tx_es101_es004gkey.Text = "";  //職　　稱
      tx_es101_redate.Text = "";  //調職日期
      tx_es101_nopay.Text = "";  //離職日期
      tx_es101_addr2.Text = "";  //通訊地址
      tx_es101_remark.Text = "";  //備　　註

      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      //
      clsGV.SetTextBoxEditAlert(ref lb_es101_no, ref tx_es101_no, false);  //員工編號
      clsGV.SetTextBoxEditAlert(ref lb_es101_cname, ref tx_es101_cname, false);  //中文姓名
      clsGV.SetTextBoxEditAlert(ref lb_es101_ename, ref tx_es101_ename, false);  //英文姓名
      clsGV.SetTextBoxEditAlert(ref lb_es101_tel2, ref tx_es101_tel2, false);  //行動電話
      //
      clsGV.TextBox_Set(ref tx_es101_no, false);   //員工編號
      clsGV.TextBox_Set(ref tx_es101_cname, false);   //中文姓名
      clsGV.TextBox_Set(ref tx_es101_ename, false);   //英文姓名
      clsGV.Drpdown_Set(ref dr_es101_sex, ref tx_es101_sex, false);   //性　　別
      clsGV.TextBox_Set(ref tx_es101_tel2, false);   //行動電話
      clsGV.TextBox_Set(ref tx_es101_email1, false);   //e-mail　
      clsGV.TextBox_Set(ref tx_es101_blood, false);   //血　　型
      clsGV.TextBox_Set(ref tx_es101_indate, false);   //到職日期
      clsGV.TextBox_Set(ref tx_es101_es001gkey, false);   //部　　門
      clsGV.TextBox_Set(ref tx_es101_es004gkey, false);   //職　　稱
      clsGV.TextBox_Set(ref tx_es101_redate, false);   //調職日期
      clsGV.TextBox_Set(ref tx_es101_nopay, false);   //離職日期
      clsGV.TextBox_Set(ref tx_es101_addr2, false);   //通訊地址
      clsGV.TextBox_Set(ref tx_es101_remark, false);   //備　　註
      //
      clsGV.SetControlShowAlert(ref lb_es101_no, ref tx_es101_no, true);  // 員工編號
      clsGV.SetControlShowAlert(ref lb_es101_cname, ref tx_es101_cname, true);  // 中文姓名
      clsGV.SetControlShowAlert(ref lb_es101_ename, ref tx_es101_ename, true);  // 英文姓名
      clsGV.SetControlShowAlert(ref lb_es101_tel2, ref tx_es101_tel2, true);  // 行動電話
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_es101.Enabled = true;
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_es101_no, true);  //員工編號
      clsGV.TextBox_Set(ref tx_es101_cname, true);  //中文姓名
      clsGV.TextBox_Set(ref tx_es101_ename, true);  //英文姓名
      clsGV.Drpdown_Set(ref dr_es101_sex, ref tx_es101_sex, true);   //性　　別
      clsGV.TextBox_Set(ref tx_es101_tel2, true);  //行動電話
      clsGV.TextBox_Set(ref tx_es101_email1, true);  //e-mail　
      clsGV.TextBox_Set(ref tx_es101_blood, true);  //血　　型
      clsGV.TextBox_Set(ref tx_es101_indate, true);  //到職日期
      clsGV.TextBox_Set(ref tx_es101_es001gkey, true);  //部　　門
      clsGV.TextBox_Set(ref tx_es101_es004gkey, true);  //職　　稱
      clsGV.TextBox_Set(ref tx_es101_redate, true);  //調職日期
      clsGV.TextBox_Set(ref tx_es101_nopay, true);  //離職日期
      clsGV.TextBox_Set(ref tx_es101_addr2, true);  //通訊地址
      clsGV.TextBox_Set(ref tx_es101_remark, true);  //備　　註
      // 
      clsGV.SetTextBoxEditAlert(ref lb_es101_no, ref tx_es101_no, true);  // 員工編號
      clsGV.SetTextBoxEditAlert(ref lb_es101_cname, ref tx_es101_cname, true);  // 中文姓名
      clsGV.SetTextBoxEditAlert(ref lb_es101_ename, ref tx_es101_ename, true);  // 英文姓名
      clsGV.SetTextBoxEditAlert(ref lb_es101_tel2, ref tx_es101_tel2, true);  // 行動電話
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_es101.Enabled = false;
    }

    private void SetEditModAll()
    {
      // 
      clsGV.TextBox_Set(ref tx_es101_no, true);  //員工編號
      clsGV.TextBox_Set(ref tx_es101_cname, true);  //中文姓名
      clsGV.TextBox_Set(ref tx_es101_ename, true);  //英文姓名
      clsGV.Drpdown_Set(ref dr_es101_sex, ref tx_es101_sex, true);   //性　　別
      clsGV.TextBox_Set(ref tx_es101_tel2, true);  //行動電話
      clsGV.TextBox_Set(ref tx_es101_email1, true);  //e-mail　
      clsGV.TextBox_Set(ref tx_es101_blood, true);  //血　　型
      clsGV.TextBox_Set(ref tx_es101_indate, true);  //到職日期
      clsGV.TextBox_Set(ref tx_es101_es001gkey, true);  //部　　門
      clsGV.TextBox_Set(ref tx_es101_es004gkey, true);  //職　　稱
      clsGV.TextBox_Set(ref tx_es101_redate, true);  //調職日期
      clsGV.TextBox_Set(ref tx_es101_nopay, true);  //離職日期
      clsGV.TextBox_Set(ref tx_es101_addr2, true);  //通訊地址
      clsGV.TextBox_Set(ref tx_es101_remark, true);  //備　　註
      // 
      clsGV.SetTextBoxEditAlert(ref lb_es101_no, ref tx_es101_no, true);  // 員工編號
      clsGV.SetTextBoxEditAlert(ref lb_es101_cname, ref tx_es101_cname, true);  // 中文姓名
      clsGV.SetTextBoxEditAlert(ref lb_es101_ename, ref tx_es101_ename, true);  // 英文姓名
      clsGV.SetTextBoxEditAlert(ref lb_es101_tel2, ref tx_es101_tel2, true);  // 行動電話
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_es101.Enabled = true;
      //gr_GridView_es101.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      //
      tx_es101_no.Text = DAC.GetStringValue(CurRow["es101_no"]);  //員工編號
      tx_es101_cname.Text = DAC.GetStringValue(CurRow["es101_cname"]);  //中文姓名
      tx_es101_ename.Text = DAC.GetStringValue(CurRow["es101_ename"]);  //英文姓名
      dr_es101_sex = sFN.SetDropDownList(ref dr_es101_sex, DAC.GetStringValue(CurRow["es101_sex"]));  //性　　別
      tx_es101_tel2.Text = DAC.GetStringValue(CurRow["es101_tel2"]);  //行動電話
      tx_es101_email1.Text = DAC.GetStringValue(CurRow["es101_email1"]);  //e-mail　
      tx_es101_blood.Text = DAC.GetStringValue(CurRow["es101_blood"]);  //血　　型
      if (CurRow["es101_indate"] == DBNull.Value) { tx_es101_indate.Text = ""; } else { tx_es101_indate.Text = DAC.GetDateTimeValue(CurRow["es101_indate"]).ToString(sys_DateFormat); }  //到職日期
      tx_es101_es001gkey.Text = DAC.GetStringValue(CurRow["es101_es001gkey"]);  //部　　門
      tx_es101_es004gkey.Text = DAC.GetStringValue(CurRow["es101_es004gkey"]);  //職　　稱
      if (CurRow["es101_redate"] == DBNull.Value) { tx_es101_redate.Text = ""; } else { tx_es101_redate.Text = DAC.GetDateTimeValue(CurRow["es101_redate"]).ToString(sys_DateFormat); }  //調職日期
      if (CurRow["es101_nopay"] == DBNull.Value) { tx_es101_nopay.Text = ""; } else { tx_es101_nopay.Text = DAC.GetDateTimeValue(CurRow["es101_nopay"]).ToString(sys_DateFormat); }  //離職日期
      tx_es101_addr2.Text = DAC.GetStringValue(CurRow["es101_addr2"]);  //通訊地址
      tx_es101_remark.Text = DAC.GetStringValue(CurRow["es101_remark"]);  //備　　註
      //
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["es101_mkey"]);
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
        CmdQueryS = (OleDbCommand)Session["fmes101_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = "";
      }
      //
      DataTable tb_es101 = new DataTable();
      DAC es101Dao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = es101Dao.GetDataAdapter(ApVer, "UNes101", "es101", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " es101_no ");
      ad_DataDataAdapter.Fill(tb_es101);
      //
      if (tb_es101.Rows.Count > 0)
      {
        //bt_05.OnClientClick = "return btnDEL_c()";
        //bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        //bt_05.OnClientClick = "return btnDEL0_c()";
        //bt_04.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_es101.DataSource = tb_es101;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_es101 = clsGV.BindGridView(gr_GridView_es101, tb_es101, hh_GridCtrl, ref hh_GridGkey, "fmes101_gr_GridView_es101");
      gr_GridView_es101.DataBind();
      SelDataKey = "es101_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_es101.Select(SelDataKey);
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
      tb_es101.Dispose();
      es101Dao.Dispose();
    }

    protected void gr_GridView_es101_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      //Session["fmes101_gr_GridView_es101_PageIndex"] = gr_GridView_es101.PageIndex + 1;
      //Session["fmes101_gr_GridView_es101_SelectedIndex"] = gr_GridView_es101.SelectedIndex;
      //hh_GridGkey.Value = gr_GridView_es101.DataKeys[gr_GridView_es101.SelectedIndex].Value.ToString();
      //gr_GridView_es101.PageIndex = e.NewPageIndex;
      //
      gr_GridView_es101.PageIndex = e.NewPageIndex;
      Session["fmes101_gr_GridView_es101_PageIndex"] = gr_GridView_es101.PageIndex;
      hh_GridGkey.Value = gr_GridView_es101.DataKeys[gr_GridView_es101.SelectedIndex].Value.ToString(); //ss101_gkey
    }

    protected void gr_GridView_es101_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmes101_gr_GridView_es101_PageIndex"] = gr_GridView_es101.PageIndex;
      Session["fmes101_gr_GridView_es101_SelectedIndex"] = gr_GridView_es101.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_es101.DataKeys[gr_GridView_es101.SelectedIndex].Value.ToString(); //ss101_gkey
      SetSerMod();
    }

    protected void gr_GridView_es101_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_es101.Enabled)
      {
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        Session["fmes101_gr_GridView_es101_PageIndex"] = gr_GridView_es101.PageIndex;
        Session["fmes101_gr_GridView_es101_SelectedIndex"] = gr_GridView_es101.SelectedIndex;
        SetSerMod();
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
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
        //定義guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(false);
        SetEditMod();
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_es101_no').focus(); </script>";
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
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(true);
        SetEditMod();
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_es101_cname').focus(); </script>";
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
        DAC es101Dao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "es101_gkey='" + hh_GridGkey.Value + "' and es101_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_es101 = new DataTable();
        DbDataAdapter da_ADP = es101Dao.GetDataAdapter(ApVer, "UNes101", "es101", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_es101);
        DataRow[] DelRow = tb_es101.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            es101Dao.Insertbalog(conn, thistran, "es101", hh_ActKey.Value, hh_GridGkey.Value);
            es101Dao.Insertbtlog(conn, thistran, "es101", DAC.GetStringValue(DelRow[0]["es101_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0]["es101_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_es101);
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
            es101Dao.Dispose();
            tb_es101.Dispose();
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
        tb_es101.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_es101 = clsGV.SetGridCursor("del", gr_GridView_es101, -2);
          BindNew(true);
          SetSerMod();
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
        DAC es101Dao = new DAC(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {
          if (UpdateDataAll(hh_ActKey.Value, ref st_dberrmsg))
          {
            hh_GridCtrl.Value = "rekey";
            BindNew(true);
            SetSerMod();
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
          string st_SelDataKey = "es101_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //自動編號
            //DateTime dt_idat=Convert.ToDateTime(tx_riza_RIDAT.Text);
            //st_ren_yymmtext =sFN.strzeroi(dt_idat.Year,4)+sFN.strzeroi(dt_idat.Month,2);
            //st_ren_cls=st_ren_yymmtext;
            //tx_riza_RIREN.Text = rizaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            //conn.Close();

            //檢查重複
            if (es101Dao.IsExists("es101", "no", tx_es101_no.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_es101_no.Text + ",已存在.");

              // es101Dao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_riza_RIREN.Text);
              // st_dberrmsg = StringTable.GetString(tx_riza_RIREN.Text + ",已重新取號.");
              // //tx_riza_RIREN.Text = es101Dao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_riza_RIREN.Text ="";
              // conn.Close();

            }
            else
            {
              DataTable tb_es101_ins = new DataTable();
              DbDataAdapter da_ADP_ins = es101Dao.GetDataAdapter(ApVer, "UNes101", "es101", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_es101_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_es101_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["es101_gkey"] = st_tempgkey;       // 
                ins_row["es101_mkey"] = DAC.get_guidkey(); //
                ins_row["es101_no"] = tx_es101_no.Text.Trim();       // 員工編號
                ins_row["es101_cname"] = tx_es101_cname.Text.Trim();       // 中文姓名
                ins_row["es101_ename"] = tx_es101_ename.Text.Trim();       // 英文姓名
                ins_row["es101_sex"] = dr_es101_sex.SelectedValue;       // 性　　別
                ins_row["es101_tel2"] = tx_es101_tel2.Text.Trim();       // 行動電話
                ins_row["es101_email1"] = tx_es101_email1.Text.Trim();       // e-mail　
                ins_row["es101_blood"] = tx_es101_blood.Text.Trim();       // 血　　型
                if (tx_es101_indate.Text.Trim() == "") { ins_row["es101_indate"] = DBNull.Value; } else { ins_row["es101_indate"] = sFN.DateStringToDateTime(tx_es101_indate.Text); }       //到職日期
                ins_row["es101_es001gkey"] = tx_es101_es001gkey.Text.Trim();       // 部　　門
                ins_row["es101_es004gkey"] = tx_es101_es004gkey.Text.Trim();       // 職　　稱
                if (tx_es101_redate.Text.Trim() == "") { ins_row["es101_redate"] = DBNull.Value; } else { ins_row["es101_redate"] = sFN.DateStringToDateTime(tx_es101_redate.Text); }       //調職日期
                if (tx_es101_nopay.Text.Trim() == "") { ins_row["es101_nopay"] = DBNull.Value; } else { ins_row["es101_nopay"] = sFN.DateStringToDateTime(tx_es101_nopay.Text); }       //離職日期
                ins_row["es101_addr2"] = tx_es101_addr2.Text.Trim();       // 通訊地址
                ins_row["es101_remark"] = tx_es101_remark.Text.Trim();       // 備　　註

                ins_row["es101_trusr"] = UserGkey;  //
                tb_es101_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_es101_ins);
                //es101Dao.UpDateRenW(conn, thistran, st_dd_apx, st_ren_cls, st_ren_cos, tx_riza_RIREN.Text.Trim());
                es101Dao.Insertbalog(conn, thistran, "es101", hh_ActKey.Value, hh_GridGkey.Value);
                es101Dao.Insertbtlog(conn, thistran, "es101", DAC.GetStringValue(ins_row["es101_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["es101_gkey"]));
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
                es101Dao.Dispose();
                tb_es101_ins.Dispose();
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
            if (es101Dao.IsExists("es101", "no", tx_es101_no.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_es101_no.Text + ",已存在.");
            }
            else
            {
              DataTable tb_es101_mod = new DataTable();
              DbDataAdapter da_ADP_mod = es101Dao.GetDataAdapter(ApVer, "UNes101", "es101", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_es101_mod);
              st_SelDataKey = "es101_gkey='" + hh_GridGkey.Value + "' and es101_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_es101_mod.Select(st_SelDataKey);
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

                  mod_row["es101_no"] = tx_es101_no.Text.Trim();       // 員工編號
                  mod_row["es101_cname"] = tx_es101_cname.Text.Trim();       // 中文姓名
                  mod_row["es101_ename"] = tx_es101_ename.Text.Trim();       // 英文姓名
                  mod_row["es101_sex"] = dr_es101_sex.SelectedValue;       // 性　　別
                  mod_row["es101_tel2"] = tx_es101_tel2.Text.Trim();       // 行動電話
                  mod_row["es101_email1"] = tx_es101_email1.Text.Trim();       // e-mail　
                  mod_row["es101_blood"] = tx_es101_blood.Text.Trim();       // 血　　型
                  if (tx_es101_indate.Text.Trim() == "") { mod_row["es101_indate"] = DBNull.Value; } else { mod_row["es101_indate"] = sFN.DateStringToDateTime(tx_es101_indate.Text); }       //到職日期
                  mod_row["es101_es001gkey"] = tx_es101_es001gkey.Text.Trim();       // 部　　門
                  mod_row["es101_es004gkey"] = tx_es101_es004gkey.Text.Trim();       // 職　　稱
                  if (tx_es101_redate.Text.Trim() == "") { mod_row["es101_redate"] = DBNull.Value; } else { mod_row["es101_redate"] = sFN.DateStringToDateTime(tx_es101_redate.Text); }       //調職日期
                  if (tx_es101_nopay.Text.Trim() == "") { mod_row["es101_nopay"] = DBNull.Value; } else { mod_row["es101_nopay"] = sFN.DateStringToDateTime(tx_es101_nopay.Text); }       //離職日期
                  mod_row["es101_addr2"] = tx_es101_addr2.Text.Trim();       // 通訊地址
                  mod_row["es101_remark"] = tx_es101_remark.Text.Trim();       // 備　　註

                  mod_row["es101_mkey"] = DAC.get_guidkey();        //
                  mod_row["es101_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_es101_mod);
                  es101Dao.Insertbalog(conn, thistran, "es101", hh_ActKey.Value, hh_GridGkey.Value);
                  es101Dao.Insertbtlog(conn, thistran, "es101", DAC.GetStringValue(mod_row["es101_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["es101_gkey"]));
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
                  es101Dao.Dispose();
                  tb_es101_mod.Dispose();
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
        es101Dao.Dispose();
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

      ret = DataCheck.cIsStrEmptyChk(ret, tx_es101_no.Text, lb_es101_no.Text, ref sMsg, PublicVariable.LangType, sFN);  //員工編號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_es101_cname.Text, lb_es101_cname.Text, ref sMsg, PublicVariable.LangType, sFN);  //中文姓名
      //ret = DataCheck.cIsStrEmptyChk(ret, tx_es101_email1.Text, lb_es101_email1.Text, ref sMsg, PublicVariable.LangType, sFN);  //e-mail　
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_es101_indate.Text, lb_es101_indate.Text, ref sMsg, PublicVariable.LangType, sFN); //到職日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_es101_redate.Text, lb_es101_redate.Text, ref sMsg, PublicVariable.LangType, sFN); //調職日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_es101_nopay.Text, lb_es101_nopay.Text, ref sMsg, PublicVariable.LangType, sFN); //離職日期
      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_es101_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      //string st_datavalue = "";
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
      }
    }


    protected bool UpdateDataAll(string st_ActKey, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_es101_gkey = "", st_es101_mkey = "", st_es101_no = "", st_es101_cname = "", st_es101_ename = "", st_es101_tel2 = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      //
      st_ctrl = st_ContentPlaceHolder + "gr_GridView_es101$ctl";
      CmdQueryS.CommandText = " and 1=1 ";
      DataTable tb_es101 = new DataTable();
      DAC es101Dao = new DAC(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      DbDataAdapter da_ADP = es101Dao.GetDataAdapter(ApVer, "UNes101", "es101", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
      da_ADP.Fill(tb_es101);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_es101.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_es101_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_es101_gkey02";
            st_es101_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //mkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_es101_mkey02";
            st_es101_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //員工編號
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_es101_no02";
            st_es101_no = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //中文姓名
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_es101_cname02";
            st_es101_cname = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //英文姓名
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_es101_ename02";
            st_es101_ename = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //行動電話
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_es101_tel202";
            st_es101_tel2 = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_es101.Select("es101_gkey='" + st_es101_gkey + "'");
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (
                   (DAC.GetStringValue(mod_row["es101_no"]) != st_es101_no)
                || (DAC.GetStringValue(mod_row["es101_cname"]) != st_es101_cname)
                || (DAC.GetStringValue(mod_row["es101_ename"]) != st_es101_ename)
                || (DAC.GetStringValue(mod_row["es101_tel2"]) != st_es101_tel2)
              )
              {
                es101Dao.Insertbalog(conn, thistran, "es101", st_ActKey, st_es101_gkey);
                es101Dao.Insertbtlog(conn, thistran, "es101", DAC.GetStringValue(mod_row["es101_gkey"]), "M", UserGkey, DAC.GetStringValue(mod_row["es101_gkey"]) + " " + DAC.GetStringValue(mod_row["es101_gkey"]) + " " + DAC.GetStringValue(mod_row["es101_gkey"]));
                mod_row.BeginEdit();
                mod_row["es101_no"] = st_es101_no;      //員工編號
                mod_row["es101_cname"] = st_es101_cname;      //中文姓名
                mod_row["es101_ename"] = st_es101_ename;      //英文姓名
                mod_row["es101_tel2"] = st_es101_tel2;      //行動電話
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();  //
              }
            }  //sel_rows.Length == 1
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_es101);
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
        es101Dao.Dispose();
        tb_es101.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }

    protected void bt_08_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }

  }
}