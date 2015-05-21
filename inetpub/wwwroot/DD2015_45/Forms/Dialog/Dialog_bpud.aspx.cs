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
  public partial class Dialog_bpud : FormBase
  {

    string st_object_func = "UNbpud";
    string st_ContentPlaceHolder = "";
    int in_PageSize = 10;
    //
    public string st_dd_apx = "UNbpud";         //UNdcnews   與apx 相關
    public string st_dd_table = "bpud";       //dcnews     與table 相關 

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
            if (iIndex.Value == "BPNUM")
            {
              tx_bpud_BPNUM.Text = iInput.Value;
            }
            if (iIndex.Value == "BPNUB")
            {
              tx_bpud_BPNUB.Text = iInput.Value;
            }
            if (iIndex.Value == "BPTNA")
            {
              tx_bpud_BPTNA.Text = iInput.Value;
            }
            if (iIndex.Value == "BPCLA")
            {
              tx_bpud_BPCLA.Text = iInput.Value;
            }
            if (iIndex.Value == "BPENA")
            {
              tx_bpud_BPENA.Text = iInput.Value;
            }
            if (iIndex.Value == "BPNCR")
            {
              tx_bpud_BPNCR.Text = iInput.Value;
            }
          }
          //
          Set_Control();
          SetEditMod();
          BindNew(true);
        }
        else
        {
          //BindNew(true);
        }
      }
    }
    //
    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      in_PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      this.Page.Title = FunctionName;
      sFN.SetFormLables(this, PublicVariable.LangType, st_ContentPlaceHolder, ApVer, "UNbpud", "bpud");
    }

    private void ClearText()
    {
      //
      tx_bpud_BPNUM.Text = "";  //  
      tx_bpud_BPNUB.Text = "";  //  
      tx_bpud_BPTNA.Text = "";  // 
      tx_bpud_BPCLA.Text = "";  // 
      tx_bpud_BPENA.Text = "";  // 
      tx_bpud_BPNCR.Text = "";  // 
      //
      hh_mkey.Value = "";
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_bpud_BPNUM, true);  //編　　號
      clsGV.TextBox_Set(ref tx_bpud_BPNUB, true);  //名　　稱
      clsGV.TextBox_Set(ref tx_bpud_BPTNA, true);
      clsGV.TextBox_Set(ref tx_bpud_BPCLA, true);
      clsGV.TextBox_Set(ref tx_bpud_BPENA, true);
      clsGV.TextBox_Set(ref tx_bpud_BPNCR, true);
      //
      sFN.SetWebImageButton(this, "bt_08", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_find, true);
      gr_GridView_bpud.Enabled = false;
      gr_GridView_bpud.Columns[0].Visible = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["bpud_mkey"]);
      //
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      DataTable tb_bpud = new DataTable();
      DAC bpudDao = new DAC(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      //
      CmdQueryS.CommandText = "";
      if (tx_bpud_BPNUM.Text != "")
      {
        CmdQueryS.CommandText += " and a.BPNUM like ? ";
        DAC.AddParam(CmdQueryS, "BPNUM", tx_bpud_BPNUM.Text + "%");
      }
      if (tx_bpud_BPNUB.Text != "")
      {
        CmdQueryS.CommandText += " and a.BPNUB like ? ";
        DAC.AddParam(CmdQueryS, "BPNUB", tx_bpud_BPNUB.Text + "%");
      }
      if (tx_bpud_BPNCR.Text != "")
      {
        CmdQueryS.CommandText += " and a.BPNCR like ? ";
        DAC.AddParam(CmdQueryS, "BPNCR", tx_bpud_BPNCR.Text + "%");
      }
      if (tx_bpud_BPTNA.Text != "")
      {
        CmdQueryS.CommandText += " and a.BPTNA like ? ";
        DAC.AddParam(CmdQueryS, "BPTNA", "%" + tx_bpud_BPTNA.Text + "%");
      }
      if (tx_bpud_BPCLA.Text != "")
      {
        CmdQueryS.CommandText += " and a.BPCLA like ? ";
        DAC.AddParam(CmdQueryS, "BPCLA", "%" + tx_bpud_BPCLA.Text + "%");
      }
      if (tx_bpud_BPENA.Text != "")
      {
        CmdQueryS.CommandText += " and a.BPENA like ? ";
        DAC.AddParam(CmdQueryS, "BPENA", "%" + tx_bpud_BPENA.Text + "%");
      }
      //
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = bpudDao.GetDataAdapter(ApVer, "UNbpud", "bpud", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " bpud_BPNUM ", "SEL");
      ad_DataDataAdapter.Fill(tb_bpud);
      gr_GridView_bpud.DataSource = tb_bpud;
      gr_GridView_bpud = clsGV.BindGridView(gr_GridView_bpud, tb_bpud, hh_GridCtrl, ref hh_GridGkey, "fmbpud_gr_GridView_bpud");
      gr_GridView_bpud.DataBind();
      SelDataKey = "bpud_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_bpud.Select(SelDataKey);
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
      tb_bpud.Dispose();
      bpudDao.Dispose();
    }

    protected void gr_GridView_bpud_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_bpud.PageIndex = e.NewPageIndex;
    }

    protected void gr_GridView_bpud_SelectedIndexChanged(object sender, EventArgs e)
    {
      Session["fmbpud_gr_GridView_bpud_PageIndex"] = gr_GridView_bpud.PageIndex + 1;
      Session["fmbpud_gr_GridView_bpud_SelectedIndex"] = gr_GridView_bpud.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_bpud.DataKeys[gr_GridView_bpud.SelectedIndex].Value.ToString();
      BindNew(true);
      SetEditMod();
    }

    protected void gr_GridView_bpud_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_bpud.Enabled)
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

    protected void gr_GridView_bpud_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void bt_08_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      BindNew(true);
    }

  }
}