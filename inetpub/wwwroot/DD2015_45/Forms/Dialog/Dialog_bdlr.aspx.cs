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
namespace DD2015_45.Forms.Dialog
{
  public partial class Dialog_bdlr : FormBase
  {

    string st_object_func = "UNbdsr";
    string st_ContentPlaceHolder = "";
    int in_PageSize = 10;
    //
    public string st_dd_apx = "UNbdsr";         //UNdcnews   與apx 相關
    public string st_dd_table = "bdlr";       //dcnews     與table 相關 

    protected void Page_Load(object sender, EventArgs e)
    {
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      //檢查權限&狀態
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          iFunc.Value = DAC.GetStringValue(Request["iFunc"]);
          oNewMod.Value = DAC.GetStringValue(Request["oNewMod"]).ToLower();
          oDataGrid_id.Value = DAC.GetStringValue(Request["oDataGrid_id"]);
          iField.Value = DAC.GetStringValue(Request["iField"]);
          oField.Value = DAC.GetStringValue(Request["oField"]);
          oWindow_Id.Value = DAC.GetStringValue(Request["oWindow_Id"]);
          iIndex.Value = DAC.GetStringValue(Request["iIndex"]);
          iInput.Value = DAC.GetStringValue(Request["iInput"]).Trim();
          oReturn.Value = "0"; //close dialog only 
          if (iInput.Value != "")
          {
            if (iIndex.Value == "BDNUM")
            {
              tx_bdlr_BDNUM.Text = iInput.Value;
            }
            if (iIndex.Value == "BDNAM")
            {
              tx_bdlr_BDNAM.Text = iInput.Value;
            }
            if (iIndex.Value == "BDTEL")
            {
              tx_bdlr_BDTEL.Text = iInput.Value;
            }
          }
          //
          Set_Control();
          SetEditMod();
          BindNew(true);
        }
        else
        {
          BindNew(true);
        }
      }
    }
    //
    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      in_PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNbdsr", "bdlr");
    }

    private void ClearText()
    {
      //
      tx_bdlr_BDNUM.Text = "";  // 編號
      tx_bdlr_BDNAM.Text = "";  // 姓名
      tx_bdlr_BDTEL.Text = "";  // 
      //
      hh_mkey.Value = "";
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_bdlr_BDNUM, true);  //編　　號
      clsGV.TextBox_Set(ref tx_bdlr_BDNAM, true);  //名　　稱
      clsGV.TextBox_Set(ref tx_bdlr_BDTEL, true);  //連絡電話
      //
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_bdlr.Enabled = false;
      gr_GridView_bdlr.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["bdlr_mkey"]);
      //
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      DataTable tb_bdlr = new DataTable();
      DAC bdlrDao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      //
      CmdQueryS.CommandText = "";
      if (tx_bdlr_BDNUM.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDNUM like ? ";
        DAC.AddParam(CmdQueryS, "BDNUM", tx_bdlr_BDNUM.Text + "%");
      }
      if (tx_bdlr_BDNAM.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDNAM like ? ";
        DAC.AddParam(CmdQueryS, "BDNAM", "%" + tx_bdlr_BDNAM.Text + "%");
      }
      if (tx_bdlr_BDTEL.Text != "")
      {
        CmdQueryS.CommandText += " and a.BDTEL like ? ";
        DAC.AddParam(CmdQueryS, "BDTEL", "%" + tx_bdlr_BDTEL.Text + "%");
      }
      //
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = bdlrDao.GetDataAdapter(ApVer, "UNbdsr", "bdlr", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " bdlr_BDNUM ");
      ad_DataDataAdapter.Fill(tb_bdlr);
      //
      gr_GridView_bdlr.DataSource = tb_bdlr;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_bdlr = clsGV.BindGridView(gr_GridView_bdlr, tb_bdlr, hh_GridCtrl, ref hh_GridGkey, "fmbdlr_gr_GridView_bdlr");
      gr_GridView_bdlr.DataBind();
      SelDataKey = "bdlr_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_bdlr.Select(SelDataKey);
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
      tb_bdlr.Dispose();
      bdlrDao.Dispose();
    }

    protected void gr_GridView_bdlr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_bdlr.PageIndex = e.NewPageIndex;
    }

    protected void gr_GridView_bdlr_SelectedIndexChanged(object sender, EventArgs e)
    {
      Session["fmbdlr_gr_GridView_bdlr_PageIndex"] = gr_GridView_bdlr.PageIndex + 1;
      Session["fmbdlr_gr_GridView_bdlr_SelectedIndex"] = gr_GridView_bdlr.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_bdlr.DataKeys[gr_GridView_bdlr.SelectedIndex].Value.ToString();
      BindNew(true);
      SetEditMod();
    }

    protected void gr_GridView_bdlr_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_bdlr.Enabled)
      {
        SetEditMod();
        hh_GridCtrl.Value = "ser";
        BindNew(true);
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
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

    protected void gr_GridView_bdlr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      //string st_datavalue = "";
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
      }
    }

    protected void bt_08_Click(object sender, EventArgs e)
    {
      BindNew(true);
    }
  }
}