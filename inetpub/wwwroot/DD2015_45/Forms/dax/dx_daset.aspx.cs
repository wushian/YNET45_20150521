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

namespace DD2015_45.Forms.dax
{
  public partial class dx_daset : FormBase
  {
    string st_object_func = "UNdaset";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    
    //
    public string st_dd_apx = "UNDASET";         //UNdcnews   與apx 相關
    public string st_dd_table = "DASET";       //dcnews     與table 相關 
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
          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmDASET_CmdQueryS"] = CmdQueryS;
          Set_Control();
           
          if (DAC.GetStringValue(Session["fmDASET_gr_GridView_DASET_GridGkey"]) != "")
          {
            gr_GridView_DASET.PageIndex = DAC.GetInt16Value(Session["fmDASET_gr_GridView_DASET_PageIndex"]);
            gr_GridView_DASET.SelectedIndex = DAC.GetInt16Value(Session["fmDASET_gr_GridView_DASET_SelectedIndex"]);
            hh_GridGkey.Value = DAC.GetStringValue(Session["fmDASET_gr_GridView_DASET_GridGkey"]);
          }
          //
          BindNew(true);
          SetSerMod();
          Session["fmDASET_gr_GridView_DASET_PageIndex"] = gr_GridView_DASET.PageIndex;
          Session["fmDASET_gr_GridView_DASET_SelectedIndex"] = gr_GridView_DASET.SelectedIndex;
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
      gr_GridView_DASET.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      //sFN.SetFormLables(this, LangType, st_ContentPlaceHolder, ApVer, "UNDASET", "DASET");
      sFN.SetFormControlsText(this.Form, PublicVariable.LangType, ApVer, "UNDASET", "DASET");
    }

    private void ClearText()
    {
      //
      tx_DASET_DAREN.Text = "";  //序　　號
      tx_DASET_DAVER.Text = "";  //版本編號
      tx_DASET_DANUM.Text = "";  //客戶編號
      tx_DASET_DAAPX.Text = "";  //程式名稱
      tx_DASET_DANAM.Text = "";  //中文名稱
      tx_DASET_DANAC.Text = "";  //簡體名稱
      tx_DASET_DANAE.Text = "";  //英文名稱
      tx_DASET_DATBL.Text = "";  //TABLE 名稱
      tx_DASET_DARMK.Text = "";  //備　　註
      tx_DASET_DATYP.Text = "";  //檔案形態

      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {

      //
      clsGV.TextBox_Set(ref tx_DASET_DAREN, false);   //序　　號
      clsGV.TextBox_Set(ref tx_DASET_DAVER, false);   //版本編號
      clsGV.TextBox_Set(ref tx_DASET_DANUM, false);   //客戶編號
      clsGV.TextBox_Set(ref tx_DASET_DAAPX, false);   //程式名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAM, false);   //中文名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAC, false);   //簡體名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAE, false);   //英文名稱
      clsGV.TextBox_Set(ref tx_DASET_DATBL, false);   //TABLE 名稱
      clsGV.TextBox_Set(ref tx_DASET_DARMK, false);   //備　　註
      clsGV.TextBox_Set(ref tx_DASET_DATYP, false);   //檔案形態

      //
      //
      sFN.SetButtons(this, LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_DASET.Enabled = true;
      //gr_GridView_DASET.Columns[0].Visible=true;
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_DASET_DAREN, true);  //序　　號
      clsGV.TextBox_Set(ref tx_DASET_DAVER, true);  //版本編號
      clsGV.TextBox_Set(ref tx_DASET_DANUM, true);  //客戶編號
      clsGV.TextBox_Set(ref tx_DASET_DAAPX, true);  //程式名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAM, true);  //中文名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAC, true);  //簡體名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAE, true);  //英文名稱
      clsGV.TextBox_Set(ref tx_DASET_DATBL, true);  //TABLE 名稱
      clsGV.TextBox_Set(ref tx_DASET_DARMK, true);  //備　　註
      clsGV.TextBox_Set(ref tx_DASET_DATYP, true);  //檔案形態

      // 
      sFN.SetButtons(this, LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_DASET.Enabled = false;
      //gr_GridView_DASET.Columns[0].Visible = false;
    }

    private void SetEditModAll()
    {
      // 
      clsGV.TextBox_Set(ref tx_DASET_DAREN, true);  //序　　號
      clsGV.TextBox_Set(ref tx_DASET_DAVER, true);  //版本編號
      clsGV.TextBox_Set(ref tx_DASET_DANUM, true);  //客戶編號
      clsGV.TextBox_Set(ref tx_DASET_DAAPX, true);  //程式名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAM, true);  //中文名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAC, true);  //簡體名稱
      clsGV.TextBox_Set(ref tx_DASET_DANAE, true);  //英文名稱
      clsGV.TextBox_Set(ref tx_DASET_DATBL, true);  //TABLE 名稱
      clsGV.TextBox_Set(ref tx_DASET_DARMK, true);  //備　　註
      clsGV.TextBox_Set(ref tx_DASET_DATYP, true);  //檔案形態

      // 
      sFN.SetButtons(this, LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetLinkButton(this, "bt_SAV", LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetLinkButton(this, "bt_CAN", LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetLinkButton(this, "bt_QUT", LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      gr_GridView_DASET.Enabled = true;
      //gr_GridView_DASET.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      tx_DASET_DAREN.Text = DAC.GetStringValue(CurRow["DASET_DAREN"]);  //序　　號
      tx_DASET_DAVER.Text = DAC.GetStringValue(CurRow["DASET_DAVER"]);  //版本編號
      tx_DASET_DANUM.Text = DAC.GetStringValue(CurRow["DASET_DANUM"]);  //客戶編號
      tx_DASET_DAAPX.Text = DAC.GetStringValue(CurRow["DASET_DAAPX"]);  //程式名稱
      tx_DASET_DANAM.Text = DAC.GetStringValue(CurRow["DASET_DANAM"]);  //中文名稱
      tx_DASET_DANAC.Text = DAC.GetStringValue(CurRow["DASET_DANAC"]);  //簡體名稱
      tx_DASET_DANAE.Text = DAC.GetStringValue(CurRow["DASET_DANAE"]);  //英文名稱
      tx_DASET_DATBL.Text = DAC.GetStringValue(CurRow["DASET_DATBL"]);  //TABLE 名稱
      tx_DASET_DARMK.Text = DAC.GetStringValue(CurRow["DASET_DARMK"]);  //備　　註
      tx_DASET_DATYP.Text = DAC.GetStringValue(CurRow["DASET_DATYP"]);  //檔案形態
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["DASET_mkey"]);
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
        CmdQueryS = (OleDbCommand)Session["fmDASET_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = "";
      }
      //
      CmdQueryS.CommandText = CmdQueryS.CommandText + " and a.DAAPX<>'UNDASET' and a.DAAPX<>'UNDBSET' ";
      DataTable tb_DASET = new DataTable();
      DAC DASETDao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = DASETDao.GetDataAdapter(ApVer, "UNDASET", "DASET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "  A.DAREN ");
      ad_DataDataAdapter.Fill(tb_DASET);
      //
      if (tb_DASET.Rows.Count > 0)
      {
        bt_05.OnClientClick = "return btnDEL_c()";
        bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        bt_05.OnClientClick = "return btnDEL0_c()";
        bt_04.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_DASET.DataSource = tb_DASET;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_DASET = clsGV.BindGridView(gr_GridView_DASET, tb_DASET, hh_GridCtrl, ref hh_GridGkey, "fmDASET_gr_GridView_DASET");
      gr_GridView_DASET.DataBind();
      SelDataKey = "DASET_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_DASET.Select(SelDataKey);
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          Session["fmDASET_gr_GridView_DASET_GridGkey"] = hh_GridGkey.Value;
          BindText(CurRow);
        }
        else
        {
          hh_GridCtrl.Value = "init";
          ClearText();
        }
      }
      tb_DASET.Dispose();
      DASETDao.Dispose();
    }

    protected void gr_GridView_DASET_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_DASET.PageIndex = e.NewPageIndex;
      Session["fmDASET_gr_GridView_DASET_PageIndex"] = gr_GridView_DASET.PageIndex;
      hh_GridGkey.Value = gr_GridView_DASET.DataKeys[gr_GridView_DASET.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_DASET_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_DASET.Enabled)
      {
        SetSerMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        Session["fmDASET_gr_GridView_DASET_PageIndex"] = gr_GridView_DASET.PageIndex;
        Session["fmDASET_gr_GridView_DASET_SelectedIndex"] = gr_GridView_DASET.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void gr_GridView_DASET_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmDASET_gr_GridView_DASET_PageIndex"] = gr_GridView_DASET.PageIndex;
      Session["fmDASET_gr_GridView_DASET_SelectedIndex"] = gr_GridView_DASET.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_DASET.DataKeys[gr_GridView_DASET.SelectedIndex].Value.ToString();
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
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_DASET_DAREN').focus(); </script>";
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
        tx_DASET_DAREN.Enabled = false;
        tx_DASET_DAVER.Enabled = false;
        tx_DASET_DAAPX.Enabled = false;
        tx_DASET_DANUM.Enabled = false;
        tx_DASET_DATBL.Enabled = false;
        BindNew(true);
        li_Msg.Text = "<script> document.all('ContentPlaceHolder1_tx_DASET_DANAM').focus(); </script>";
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
        DAC DASETDao = new DAC(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "DASET_gkey='" + hh_GridGkey.Value + "' and DASET_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_DASET = new DataTable();
        DbDataAdapter da_ADP = DASETDao.GetDataAdapter(ApVer, "UNDASET", "DASET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_DASET);
        DataRow[] DelRow = tb_DASET.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            DASETDao.Insertbalog(conn, thistran, "DASET", hh_ActKey.Value, hh_GridGkey.Value);
            DASETDao.Insertbtlog(conn, thistran, "DASET", DAC.GetStringValue(DelRow[0]["DASET_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0]["DASET_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_DASET);
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
            DASETDao.Dispose();
            tb_DASET.Dispose();
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
        tb_DASET.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_DASET = clsGV.SetGridCursor("del", gr_GridView_DASET, -2);
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
        DAC DASETDao = new DAC(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {

        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "DASET_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //自動編號
            //DateTime dt_idat=Convert.ToDateTime(tx_riza_RIDAT.Text);
            //st_ren_yymmtext =sFN.strzeroi(dt_idat.Year,4)+sFN.strzeroi(dt_idat.Month,2);
            //st_ren_cls=st_ren_yymmtext;
            //tx_riza_RIREN.Text = rizaDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);
            //conn.Close();

            //檢查重複
            if (DASETDao.IsExists("DASET", "DAREN", tx_DASET_DAREN.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_DASET_DAREN.Text + ",已存在.");

              // DASETDao.UpDateRenW(st_dd_apx, st_ren_cls, st_ren_cos, tx_riza_RIREN.Text);
              // st_dberrmsg = StringTable.GetString(tx_riza_RIREN.Text + ",已重新取號.");
              // //tx_riza_RIREN.Text = DASETDao.GetRenW(conn, st_dd_apx, st_ren_cls, st_ren_cos, st_ren_head, st_ren_yymmtext, in_ren_len, false);             // tx_riza_RIREN.Text ="";
              // conn.Close();

            }
            else
            {
              DataTable tb_DASET_ins = new DataTable();
              DbDataAdapter da_ADP_ins = DASETDao.GetDataAdapter(ApVer, "UNDASET", "DASET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_DASET_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_DASET_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["DASET_gkey"] = st_tempgkey;       // 
                ins_row["DASET_mkey"] = DAC.get_guidkey(); //
                ins_row["DASET_DAREN"] = tx_DASET_DAREN.Text.Trim();       // 序　　號
                ins_row["DASET_DAVER"] = tx_DASET_DAVER.Text.Trim();       // 版本編號
                ins_row["DASET_DANUM"] = tx_DASET_DANUM.Text.Trim();       // 客戶編號
                ins_row["DASET_DAAPX"] = tx_DASET_DAAPX.Text.Trim();       // 程式名稱
                ins_row["DASET_DANAM"] = tx_DASET_DANAM.Text.Trim();       // 中文名稱
                ins_row["DASET_DANAC"] = tx_DASET_DANAC.Text.Trim();       // 簡體名稱
                ins_row["DASET_DANAE"] = tx_DASET_DANAE.Text.Trim();       // 英文名稱
                ins_row["DASET_DATBL"] = tx_DASET_DATBL.Text.Trim();       // TABLE 名稱
                ins_row["DASET_DARMK"] = tx_DASET_DARMK.Text.Trim();       // 備　　註
                ins_row["DASET_DATYP"] = tx_DASET_DATYP.Text.Trim();       // 檔案形態

                ins_row["DASET_trusr"] = UserGkey;  //
                tb_DASET_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_DASET_ins);
                DASETDao.Insertbalog(conn, thistran, "DASET", hh_ActKey.Value, hh_GridGkey.Value);
                DASETDao.Insertbtlog(conn, thistran, "DASET", DAC.GetStringValue(ins_row["DASET_DAREN"]), "I", UserName, DAC.GetStringValue(ins_row["DASET_DAREN"]));
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
                DASETDao.Dispose();
                tb_DASET_ins.Dispose();
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
            if (DASETDao.IsExists("DASET", "DAREN", tx_DASET_DAREN.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_DASET_DAREN.Text + ",已存在.");
            }
            else
            {
              DataTable tb_DASET_mod = new DataTable();
              DbDataAdapter da_ADP_mod = DASETDao.GetDataAdapter(ApVer, "UNDASET", "DASET", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_DASET_mod);
              st_SelDataKey = "DASET_gkey='" + hh_GridGkey.Value + "' and DASET_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_DASET_mod.Select(st_SelDataKey);
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

                  mod_row["DASET_DAREN"] = tx_DASET_DAREN.Text.Trim();       // 序　　號
                  mod_row["DASET_DAVER"] = tx_DASET_DAVER.Text.Trim();       // 版本編號
                  mod_row["DASET_DANUM"] = tx_DASET_DANUM.Text.Trim();       // 客戶編號
                  mod_row["DASET_DAAPX"] = tx_DASET_DAAPX.Text.Trim();       // 程式名稱
                  mod_row["DASET_DANAM"] = tx_DASET_DANAM.Text.Trim();       // 中文名稱
                  mod_row["DASET_DANAC"] = tx_DASET_DANAC.Text.Trim();       // 簡體名稱
                  mod_row["DASET_DANAE"] = tx_DASET_DANAE.Text.Trim();       // 英文名稱
                  mod_row["DASET_DATBL"] = tx_DASET_DATBL.Text.Trim();       // TABLE 名稱
                  mod_row["DASET_DARMK"] = tx_DASET_DARMK.Text.Trim();       // 備　　註
                  mod_row["DASET_DATYP"] = tx_DASET_DATYP.Text.Trim();       // 檔案形態

                  mod_row["DASET_mkey"] = DAC.get_guidkey();        //
                  mod_row["DASET_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_DASET_mod);
                  DASETDao.Insertbalog(conn, thistran, "DASET", hh_ActKey.Value, hh_GridGkey.Value);
                  DASETDao.Insertbtlog(conn, thistran, "DASET", DAC.GetStringValue(mod_row["DASET_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["DASET_gkey"]));
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
                  DASETDao.Dispose();
                  tb_DASET_mod.Dispose();
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
        DASETDao.Dispose();
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

      ret = DataCheck.cIsStrEmptyChk(ret, tx_DASET_DATYP.Text, lb_DASET_DATYP.Text, ref sMsg,LangType,sFN);  //檔案形態
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DASET_DATBL.Text, lb_DASET_DATBL.Text, ref sMsg, LangType, sFN);  //檔案形態
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DASET_DAREN.Text, lb_DASET_DAREN.Text, ref sMsg, LangType, sFN);  //檔案形態
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DASET_DAVER.Text, tx_DASET_DAVER.Text, ref sMsg, LangType, sFN);  //檔案形態
      ret = DataCheck.cIsStrEmptyChk(ret, tx_DASET_DANUM.Text, tx_DASET_DANUM.Text, ref sMsg, LangType, sFN);  //檔案形態


      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_DASET_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      //string st_datavalue = "";
    }

    protected void bt_11_Click(object sender, EventArgs e)
    {
      Session["fmDASET_tx_DASET_DAREN"] = tx_DASET_DAREN.Text;  //序　　號
      Session["fmDASET_tx_DASET_DAVER"] = tx_DASET_DAVER.Text;  //版本編號
      Session["fmDASET_tx_DASET_DANUM"] = tx_DASET_DANUM.Text;  //客戶編號
      Session["fmDASET_tx_DASET_DAAPX"] = tx_DASET_DAAPX.Text;  //程式名稱
      Session["fmDASET_tx_DASET_DANAM"] = tx_DASET_DANAM.Text;  //中文名稱
      Session["fmDASET_tx_DASET_DANAC"] = tx_DASET_DANAC.Text;  //簡體名稱
      Session["fmDASET_tx_DASET_DANAE"] = tx_DASET_DANAE.Text;  //英文名稱
      Session["fmDASET_tx_DASET_DATBL"] = tx_DASET_DATBL.Text;  //TABLE 名稱
      Session["fmDASET_tx_DASET_DARMK"] = tx_DASET_DARMK.Text;  //備　　註
      Session["fmDASET_tx_DASET_DATYP"] = tx_DASET_DATYP.Text;  //檔案形態
      //
      Session["fmDASET_gr_GridView_DASET_PageIndex"] = gr_GridView_DASET.PageIndex;
      Session["fmDASET_gr_GridView_DASET_SelectedIndex"] = gr_GridView_DASET.SelectedIndex;
      Session["fmDASET_gr_GridView_DASET_GridGkey"] = gr_GridView_DASET.DataKeys[gr_GridView_DASET.SelectedIndex].Value.ToString();
      //
      Response.Redirect("dx_dbset.aspx");
    }

    protected void bt_07_Click(object sender, EventArgs e)
    {

    }

    protected void bt_09_Click(object sender, EventArgs e)
    {
      Session["fmDASET_tx_DASET_DAREN"] = tx_DASET_DAREN.Text;  //序　　號
      Session["fmDASET_tx_DASET_DAVER"] = tx_DASET_DAVER.Text;  //版本編號
      Session["fmDASET_tx_DASET_DANUM"] = tx_DASET_DANUM.Text;  //客戶編號
      Session["fmDASET_tx_DASET_DAAPX"] = tx_DASET_DAAPX.Text;  //程式名稱
      Session["fmDASET_tx_DASET_DANAM"] = tx_DASET_DANAM.Text;  //中文名稱
      Session["fmDASET_tx_DASET_DANAC"] = tx_DASET_DANAC.Text;  //簡體名稱
      Session["fmDASET_tx_DASET_DANAE"] = tx_DASET_DANAE.Text;  //英文名稱
      Session["fmDASET_tx_DASET_DATBL"] = tx_DASET_DATBL.Text;  //TABLE 名稱
      Session["fmDASET_tx_DASET_DARMK"] = tx_DASET_DARMK.Text;  //備　　註
      Session["fmDASET_tx_DASET_DATYP"] = tx_DASET_DATYP.Text;  //檔案形態
      //
      Session["fmDASET_gr_GridView_DASET_PageIndex"] = gr_GridView_DASET.PageIndex;
      Session["fmDASET_gr_GridView_DASET_SelectedIndex"] = gr_GridView_DASET.SelectedIndex;
      Session["fmDASET_gr_GridView_DASET_GridGkey"] = gr_GridView_DASET.DataKeys[gr_GridView_DASET.SelectedIndex].Value.ToString();
      //
      Response.Redirect("dx_daset_mdd.aspx");
    }

  }
}