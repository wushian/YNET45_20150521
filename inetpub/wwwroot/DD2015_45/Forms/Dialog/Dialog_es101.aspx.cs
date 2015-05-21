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
  public partial class Dialog_es101 : FormBase
  {
    string st_object_func = "UNes101";
    string st_ContentPlaceHolder = "";
    int in_PageSize = 10;
    //
    public string st_dd_apx = "UNes101";         //UNdcnews   與apx 相關
    public string st_dd_table = "es101";       //dcnews     與table 相關 

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
            if (iIndex.Value == "no")
            {
              tx_es101_no.Text = iInput.Value;
            }
            if (iIndex.Value == "cname")
            {
              tx_es101_cname.Text = iInput.Value;
            }
            if (iIndex.Value == "ename")
            {
              tx_es101_ename.Text = iInput.Value;
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
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNes101", "es101");
    }

    private void ClearText()
    {
      //
      tx_es101_no.Text = "";  //員工編號
      tx_es101_cname.Text = "";  //中文姓名
      tx_es101_ename.Text = "";  //英文姓名
      //
      hh_mkey.Value = "";
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_es101_no, true);  //員工編號
      clsGV.TextBox_Set(ref tx_es101_cname, true);  //中文姓名
      clsGV.TextBox_Set(ref tx_es101_ename, true);  //英文姓名
      // 
      clsGV.SetTextBoxEditAlert(ref lb_es101_no, ref tx_es101_no, true);  // 員工編號
      clsGV.SetTextBoxEditAlert(ref lb_es101_cname, ref tx_es101_cname, true);  // 中文姓名
      clsGV.SetTextBoxEditAlert(ref lb_es101_ename, ref tx_es101_ename, true);  // 英文姓名
      sFN.SetButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetLinkButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetLinkButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetLinkButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //
      gr_GridView_es101.Enabled = false;
      gr_GridView_es101.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
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
      DataTable tb_es101 = new DataTable();
      DAC es101Dao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      //
      CmdQueryS.CommandText = "";
      if (tx_es101_no.Text != "")
      {
        CmdQueryS.CommandText += " and a.no like ? ";
        DAC.AddParam(CmdQueryS, "no", tx_es101_no.Text + "%");
      }
      if (tx_es101_cname.Text != "")
      {
        CmdQueryS.CommandText += " and a.cname like ? ";
        DAC.AddParam(CmdQueryS, "cname", "%" + tx_es101_cname.Text + "%");
      }
      if (tx_es101_ename.Text != "")
      {
        CmdQueryS.CommandText += " and a.ename like ? ";
        DAC.AddParam(CmdQueryS, "ename", "%" + tx_es101_ename.Text + "%");
      }
      //
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = es101Dao.GetDataAdapter(ApVer, "UNes101", "es101", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " es101_no ");
      ad_DataDataAdapter.Fill(tb_es101);
      //
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
      gr_GridView_es101.PageIndex = e.NewPageIndex;
    }

    protected void gr_GridView_es101_SelectedIndexChanged(object sender, EventArgs e)
    {
      Session["fmes101_gr_GridView_es101_PageIndex"] = gr_GridView_es101.PageIndex + 1;
      Session["fmes101_gr_GridView_es101_SelectedIndex"] = gr_GridView_es101.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_es101.DataKeys[gr_GridView_es101.SelectedIndex].Value.ToString();
      BindNew(true);
      SetEditMod();
    }

    protected void gr_GridView_es101_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_es101.Enabled)
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

    protected void gr_GridView_es101_RowDataBound(object sender, GridViewRowEventArgs e)
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