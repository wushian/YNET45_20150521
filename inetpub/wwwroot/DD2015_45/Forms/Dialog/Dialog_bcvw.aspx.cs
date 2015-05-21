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
  public partial class Dialog_bcvw : FormBase
  {
    string st_object_func = "UNbcvw";
    string st_ContentPlaceHolder = "";
    int in_PageSize = 10;
    //
    public string st_dd_apx = "UNbcvw";         //UNdcnews   與apx 相關
    public string st_dd_table = "bcvw";       //dcnews     與table 相關 

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
          iField.Value = DAC.GetStringValue(Request["iField"]);
          oField.Value = DAC.GetStringValue(Request["oField"]);
          oWindow_Id.Value = DAC.GetStringValue(Request["oWindow_Id"]);
          iIndex.Value = DAC.GetStringValue(Request["iIndex"]);
          iInput.Value = DAC.GetStringValue(Request["iInput"]).Trim();
          if (iInput.Value != "")
          {
            if (iIndex.Value == "BCNUM")
            {
              tx_bcvw_BCNUM.Text = iInput.Value;
            }
            if (iIndex.Value == "BCNAM")
            {
              tx_bcvw_BCNAM.Text = iInput.Value;
            }
            if (iIndex.Value == "BCTL1")
            {
              tx_bcvw_BCTEL.Text = iInput.Value;
            }
            if (iIndex.Value == "BCGSM")
            {
              tx_bcvw_BCTEL.Text = iInput.Value;
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
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNbcvw", "bcvw");
    }

    private void ClearText()
    {
      //
      tx_bcvw_BCNUM.Text = "";  // 編號
      tx_bcvw_BCNAM.Text = "";  // 姓名
      tx_bcvw_BCTEL.Text = "";  // 
      //
      hh_mkey.Value = "";
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_bcvw_BCNUM, true);  //編　　號
      clsGV.TextBox_Set(ref tx_bcvw_BCNAM, true);  //名　　稱
      clsGV.TextBox_Set(ref tx_bcvw_BCTEL, true);  //連絡電話
      //
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_bcvw.Enabled = false;
      gr_GridView_bcvw.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["bcvw_mkey"]);
      //
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      DataTable tb_bcvw = new DataTable();
      DAC bcvwDao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      //
      CmdQueryS.CommandText = "";
      if (tx_bcvw_BCNUM.Text != "")
      {
        CmdQueryS.CommandText += " and a.BCNUM like ? ";
        DAC.AddParam(CmdQueryS, "BCNUM", tx_bcvw_BCNUM.Text + "%");
      }
      if (tx_bcvw_BCNAM.Text != "")
      {
        CmdQueryS.CommandText += " and a.BCNAM like ? ";
        DAC.AddParam(CmdQueryS, "BCNAM", "%" + tx_bcvw_BCNAM.Text + "%");
      }
      if (tx_bcvw_BCTEL.Text != "")
      {
        CmdQueryS.CommandText += " and (a.BCTL1 like ?  or a.BCGSM like ? )";
        DAC.AddParam(CmdQueryS, "BCTL1", "%" + tx_bcvw_BCTEL.Text + "%");
        DAC.AddParam(CmdQueryS, "BCGSM", "%" + tx_bcvw_BCTEL.Text + "%");
      }
      //
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = bcvwDao.GetDataAdapter(ApVer, "UNbcvw", "bcvw", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " bcvw_BCNUM ");
      ad_DataDataAdapter.Fill(tb_bcvw);
      //
      gr_GridView_bcvw.DataSource = tb_bcvw;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_bcvw = clsGV.BindGridView(gr_GridView_bcvw, tb_bcvw, hh_GridCtrl, ref hh_GridGkey, "fmbcvw_gr_GridView_bcvw");
      gr_GridView_bcvw.DataBind();
      SelDataKey = "bcvw_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_bcvw.Select(SelDataKey);
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
      tb_bcvw.Dispose();
      bcvwDao.Dispose();
    }

    protected void gr_GridView_bcvw_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_bcvw.PageIndex = e.NewPageIndex;
    }

    protected void gr_GridView_bcvw_SelectedIndexChanged(object sender, EventArgs e)
    {
      Session["fmbcvw_gr_GridView_bcvw_PageIndex"] = gr_GridView_bcvw.PageIndex + 1;
      Session["fmbcvw_gr_GridView_bcvw_SelectedIndex"] = gr_GridView_bcvw.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_bcvw.DataKeys[gr_GridView_bcvw.SelectedIndex].Value.ToString();
      BindNew(true);
      SetEditMod();
    }

    protected void gr_GridView_bcvw_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_bcvw.Enabled)
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

    protected void gr_GridView_bcvw_RowDataBound(object sender, GridViewRowEventArgs e)
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