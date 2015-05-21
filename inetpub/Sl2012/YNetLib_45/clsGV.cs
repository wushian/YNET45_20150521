using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Drawing;
using YNetLib_45;

namespace YNetLib_45
{
  public class clsGV : IDisposable
  {
    public static GridView SetGridCursor(string st_GridCtrl, GridView gr_GridView, int in_RecCount)
    {
      //BindNew時Init        ,DataGrid1=uLDB.uSetGridCursor("Ini",DataGrid1,Member.Rows.Count)
      //Delete 資料調整Cursor,DataGrid1=uLDB.uSetGridCursor("Del",DataGrid1,-2) ,Delete時不檢查vRecc,以-2代表


      //DELETE
      st_GridCtrl = st_GridCtrl.ToLower();
      if (st_GridCtrl == "del")
      {
        if (gr_GridView.SelectedIndex == 0)
        {
          //刪除第一筆資料  
          if (gr_GridView.Rows.Count > gr_GridView.SelectedIndex + 1)
          {
            //有下一筆,移到當頁下一筆
            gr_GridView.SelectedIndex = gr_GridView.SelectedIndex;
          }
          else if (gr_GridView.PageCount > gr_GridView.PageIndex + 1)
          {
            //有下一頁,移到下一頁,第一筆
            gr_GridView.PageIndex = gr_GridView.PageIndex + 1;
            gr_GridView.SelectedIndex = 0;
          }
          else if (gr_GridView.PageIndex > 0)
          {
            //有上一頁,移到上一頁,最後一筆
            gr_GridView.PageIndex = gr_GridView.PageIndex - 1;
            gr_GridView.SelectedIndex = gr_GridView.PageSize - 1;
          }
          else
          {
            //只有一頁,資料歸零
            gr_GridView.SelectedIndex = -1;
            gr_GridView.PageIndex = 0;
          }
        }
        else
        {
          //Index不變,Page不變
          if (gr_GridView.SelectedIndex - 1 >= 0)
          {
            gr_GridView.SelectedIndex = gr_GridView.SelectedIndex - 1;
          }
          else
          {
            gr_GridView.SelectedIndex = gr_GridView.SelectedIndex;
          }
        }
      }

      //
      if ((st_GridCtrl == "init") && (gr_GridView.AllowPaging == true))
      {
        if (in_RecCount > 0) //not eof
        {
          if ((gr_GridView.SelectedIndex == -1) || (gr_GridView.SelectedIndex + 1 > in_RecCount))
          {
            gr_GridView.SelectedIndex = 0;
          }
          //
          if (in_RecCount <= gr_GridView.PageSize)
          {
            gr_GridView.PageIndex = 0;
          }
          else
          {
            if (gr_GridView.PageIndex * gr_GridView.PageSize + gr_GridView.SelectedIndex + 1 > in_RecCount)
            {
              if (gr_GridView.PageCount > 0)
              {
                gr_GridView.PageIndex = gr_GridView.PageCount - 1;
                gr_GridView.SelectedIndex = gr_GridView.Rows.Count - 1;
              }
              else
              {
                gr_GridView.PageIndex = 0;
                gr_GridView.SelectedIndex = -1;
              }
            }
          }
        }
        else
        {
          gr_GridView.SelectedIndex = -1;
          gr_GridView.PageIndex = 0;
        }
      }
      return gr_GridView;
    }

    public static GridView SetGridCursorAfter(GridView gr_GridView)
    {
      int vIX = 0;
      vIX = gr_GridView.DataKeys.Count;
      if (vIX != 0)
      {
        if (vIX < gr_GridView.SelectedIndex + 1)
        {
          gr_GridView.SelectedIndex = gr_GridView.Rows.Count - 1;
        }
      }
      else
      {
        gr_GridView.SelectedIndex = -1;
      }
      return gr_GridView;
    }

    public static HtmlInputHidden GetGridHiddenKey(GridView gr_GridView, HtmlInputHidden hh_GridGkey)
    {
      if (gr_GridView.SelectedIndex == -1)
      {
        if (gr_GridView.Rows.Count > 0)
        {
          hh_GridGkey.Value = gr_GridView.DataKeys[0].Value.ToString();
        }
        else
        {
          hh_GridGkey.Value = "";
        }
      }
      else
      {
        hh_GridGkey.Value = gr_GridView.DataKeys[gr_GridView.SelectedIndex].Value.ToString();
      }
      return hh_GridGkey;
    }

    public static GridView SetGridCursorReKey(DataTable tb_DataTable, GridView gr_GridView, string st_Grid_gkey, string vField)
    {
      //新增時,將DataGrid指到新增的那一筆
      int vRk = 0, vRi = 0;
      vRk = -1;
      for (vRi = 0; vRi <= tb_DataTable.Rows.Count - 1; vRi++)
      {
        if (st_Grid_gkey.Trim() == tb_DataTable.Rows[vRi][vField].ToString().Trim())
        {
          vRk = vRi;
          vRi = tb_DataTable.Rows.Count - 1;
        }
      }
      //設定 Pageindex 及 SelectIndex
      if (gr_GridView.AllowPaging)
      {
        if (vRk > -1)
        {
          gr_GridView.PageIndex = (int)((vRk) / gr_GridView.PageSize);
          gr_GridView.SelectedIndex = (vRk % gr_GridView.PageSize);
        }
      }
      else
      {
        if (vRk > -1)
        {
          gr_GridView.PageIndex = 0;
          gr_GridView.SelectedIndex = vRk;
        }
      }
      return gr_GridView;
    }

    public static GridView BindGridView(GridView gr_GridView, DataTable tb_SourceTable, HtmlInputHidden hh_Grid_Ctrl, ref HtmlInputHidden hh_Grid_gkey, string st_FromView)
    {
      string se_PageIndex = st_FromView + "_PageIndex";
      string se_SelectedIndex = st_FromView + "_SelectedIndex";
      bool bl_first = false;
      if (DAC.GetStringValue(HttpContext.Current.Session[se_SelectedIndex]) == "")
      {
        try
        {
          gr_GridView.PageIndex = DAC.GetInt16Value(HttpContext.Current.Session[se_PageIndex]) - 1;
          gr_GridView.SelectedIndex = DAC.GetInt16Value(HttpContext.Current.Session[se_SelectedIndex]);
        }
        catch
        {
          HttpContext.Current.Session[se_PageIndex] = "";
          HttpContext.Current.Session[se_SelectedIndex] = "";
          gr_GridView.PageIndex = 0;
          gr_GridView.SelectedIndex = -1;
          //gr_GridView.SelectedIndex = 0;
          //bl_first=true;
        }
        finally
        {
          HttpContext.Current.Session[se_PageIndex] = "";
          HttpContext.Current.Session[se_SelectedIndex] = "";
        }
      }
      gr_GridView = clsGV.SetGridCursor("init", gr_GridView, tb_SourceTable.Rows.Count);
      gr_GridView.DataBind();
      if (bl_first)
      {
        //HttpContext.Current.Session[se_PageIndex] = 0;
        //HttpContext.Current.Session[se_SelectedIndex] = 0;
        //gr_GridView.PageIndex = 0;
        //gr_GridView.SelectedIndex = 0;
        gr_GridView = clsGV.SetGridCursorReKey(tb_SourceTable, gr_GridView, hh_Grid_gkey.Value, gr_GridView.DataKeyNames[0]);
      }
      else if ((hh_Grid_Ctrl.Value == "rekey") && (hh_Grid_gkey.Value != ""))
      {
        //gr_GridView = clsGV.SetGridCursorReKey(tb_SourceTable, gr_GridView, hh_Grid_gkey.Value, "gkey");
        gr_GridView = clsGV.SetGridCursorReKey(tb_SourceTable, gr_GridView, hh_Grid_gkey.Value, gr_GridView.DataKeyNames[0]);
      }
      else
      {
        gr_GridView = clsGV.SetGridCursorAfter(gr_GridView);
        hh_Grid_gkey = clsGV.GetGridHiddenKey(gr_GridView, hh_Grid_gkey);
      }
      return gr_GridView;
    }
    //
    public static void TextBox_Set(ref Infragistics.Web.UI.EditorControls.WebNumericEditor tx_Control, bool bl_Enable)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (bl_Enable)
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = false;
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        //
        tx_Control.BorderStyle = default(BorderStyle);
        tx_Control.BorderColor = default(Color);
        //
        tx_Control.BorderStyle = BorderStyle.Solid;
        tx_Control.BorderColor = Color.LightBlue;
        tx_Control.BorderWidth = 1;
        tx_Control.BackColor = Color.White;
      }
      else
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = true;
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Black;
        //
        //tx_Control.BorderStyle = default(BorderStyle);
        //tx_Control.BorderStyle= BorderStyle.NotSet;
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderWidth = Unit.Empty;
        tx_Control.BorderColor = default(Color);
      }
    }
    //
    public static void TextBox_Set(ref Infragistics.Web.UI.EditorControls.WebDateTimeEditor tx_Control, bool bl_Enable)
    {
      if (bl_Enable)
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = false;
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        //
        tx_Control.BorderStyle = default(BorderStyle);
        tx_Control.BorderColor = default(Color);
        //
        tx_Control.BorderStyle = BorderStyle.Solid;
        tx_Control.BorderColor = Color.LightBlue;
        tx_Control.BorderWidth = 1;
        tx_Control.BackColor = Color.White;
      }
      else
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = true;
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Black;
        //
        //tx_Control.BorderStyle = default(BorderStyle);
        //tx_Control.BorderColor = default(Color);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderWidth = Unit.Empty;
        tx_Control.BorderColor = default(Color);

      }
    }
    //
    public static void TextBox_Set(ref Infragistics.Web.UI.EditorControls.WebDatePicker tx_Control, bool bl_Enable)
    {
      if (bl_Enable)
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = false;
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        //
        tx_Control.BorderStyle = default(BorderStyle);
        tx_Control.BorderColor = default(Color);
        //
        tx_Control.BorderStyle = BorderStyle.Solid;
        tx_Control.BorderColor = Color.LightBlue;
        tx_Control.BorderWidth = 1;
        tx_Control.BackColor = Color.White;
      }
      else
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = true;
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Black;
        //
        //tx_Control.BorderStyle = default(BorderStyle);
        //tx_Control.BorderColor = default(Color);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderWidth = Unit.Empty;
        tx_Control.BorderColor = default(Color);

      }
    }

    //
    public static void TextBox_Set(ref TextBox tx_Control, bool bl_Enable)
    {
      if (bl_Enable)
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = false;
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        //
        tx_Control.BorderColor = default(Color);
        tx_Control.BorderStyle = default(BorderStyle);
        tx_Control.BorderWidth = 1;
        tx_Control.BackColor = default(Color);

      }
      else
      {
        tx_Control.Enabled = true;
        tx_Control.ReadOnly = true;
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Black;
        //
        //tx_Control.BorderStyle = BorderStyle.None;
        //tx_Control.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderWidth = Unit.Empty;
        tx_Control.BorderColor = default(Color);
      }
    }
    //
    public static void Drpdown_Set(ref  DropDownList dr_Control, ref TextBox tx_Control, bool bl_Enable)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (bl_Enable)
      {
        dr_Control.Visible = true;
        tx_Control.Visible = false;
        //
        dr_Control.Enabled = true;
        dr_Control.BackColor = dr_Control.BackColor;
        dr_Control.ForeColor = dr_Control.ForeColor;
        dr_Control.BorderStyle = dr_Control.BorderStyle;
        dr_Control.BorderColor = dr_Control.BorderColor;
      }
      else
      {
        dr_Control.Visible = false;
        tx_Control.Visible = true;
        //
        dr_Control.Enabled = false;
        //dr_Control.BackColor = Color.Lavender;
        //dr_Control.ForeColor = Color.Black;
        //dr_Control.ControlStyle.BorderStyle = BorderStyle.None;
        //
        tx_Control.Width = dr_Control.Width;
        //tx_Control.Text = dr_Control.SelectedItem.Text;
        try
        {
          tx_Control.Text = dr_Control.Items[dr_Control.SelectedIndex].Text;
        }
        catch
        {
          tx_Control.Text = "";
        }
        TextBox_Set(ref tx_Control, false);
      }
    }

    public static void Drpdown_Set(ref  Infragistics.Web.UI.ListControls.WebDropDown dr_Control, ref TextBox tx_Control, bool bl_Enable)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (bl_Enable)
      {
        dr_Control.Visible = true;
        tx_Control.Visible = false;
        //
        dr_Control.Enabled = true;
        dr_Control.BackColor = dr_Control.BackColor;
        dr_Control.ForeColor = dr_Control.ForeColor;
        dr_Control.BorderStyle = dr_Control.BorderStyle;
        dr_Control.BorderColor = dr_Control.BorderColor;
      }
      else
      {
        dr_Control.Visible = false;
        tx_Control.Visible = true;
        //
        dr_Control.Enabled = false;
        //dr_Control.BackColor = Color.Lavender;
        //dr_Control.ForeColor = Color.Black;
        //dr_Control.ControlStyle.BorderStyle = BorderStyle.None;
        //
        tx_Control.Width = dr_Control.Width;
        tx_Control.Text = "";
        tx_Control.Text += "{";
        for (int vdi = 0; vdi < dr_Control.Items.Count; vdi++)
        {
          if (dr_Control.Items[vdi].Selected)
          {
            if (tx_Control.Text == "{")
            {
              tx_Control.Text += dr_Control.Items[vdi].Text;
            }
            else
            {
              tx_Control.Text += "," + dr_Control.Items[vdi].Text;
            }
          }
        }
        tx_Control.Text += "}";
        //tx_Control.Text = dr_Control.SelectedItem.Text;
        //tx_Control.Text = dr_Control.CurrentValue;
        TextBox_Set(ref tx_Control, false);
      }
    }

    //
    public static void SetTextBoxEditAlert(ref Label lb_Control, ref TextBox tx_Control, bool sAlert)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BorderColor = Color.Chocolate;
        tx_Control.BackColor = Color.Linen;
        tx_Control.ForeColor = Color.Red;
        tx_Control.Font.Bold = false;
        //
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = true;
        tx_Control.BorderColor = default(Color);
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }

    public static void SetTextBoxEditAlert(ref Label lb_Control, ref Infragistics.Web.UI.EditorControls.WebNumericEditor tx_Control, bool sAlert)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BorderColor = Color.Chocolate;
        tx_Control.BackColor = Color.Linen;
        tx_Control.ForeColor = Color.Red;
        tx_Control.Font.Bold = false;
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);

        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = true;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }

    public static void SetTextBoxEditAlert(ref Label lb_Control, ref Infragistics.Web.UI.EditorControls.WebDateTimeEditor tx_Control, bool sAlert)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BorderColor = Color.Chocolate;
        tx_Control.BackColor = Color.Linen;
        tx_Control.ForeColor = Color.SaddleBrown;
        tx_Control.ForeColor = Color.Red;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.Font.Bold = false;
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = true;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }

    public static void SetTextBoxEditAlert(ref Label lb_Control, ref Infragistics.Web.UI.EditorControls.WebDatePicker tx_Control, bool sAlert)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BorderColor = Color.Chocolate;
        tx_Control.BackColor = Color.Linen;
        tx_Control.ForeColor = Color.SaddleBrown;
        tx_Control.ForeColor = Color.Red;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.Font.Bold = false;
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = true;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }


    public static void SetControlShowAlert(ref Label lb_Control, ref  Infragistics.Web.UI.EditorControls.WebNumericEditor tx_Control, bool sAlert)
    {
      TextBox oControl;
      oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Red;
        tx_Control.Font.Bold = true;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = false;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }

    public static void SetControlShowAlert(ref Label lb_Control, ref  Infragistics.Web.UI.EditorControls.WebDateTimeEditor tx_Control, bool sAlert)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Red;
        tx_Control.Font.Bold = true;
        tx_Control.BorderStyle = BorderStyle.NotSet;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderColor = default(Color);
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = false;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }

    public static void SetControlShowAlert(ref Label lb_Control, ref  Infragistics.Web.UI.EditorControls.WebDatePicker tx_Control, bool sAlert)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Red;
        tx_Control.Font.Bold = true;
        tx_Control.BorderStyle = BorderStyle.NotSet;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderColor = default(Color);
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = false;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }

    public static void SetControlShowAlert(ref Label lb_Control, ref  TextBox tx_Control, bool sAlert)
    {
      //TextBox oControl;
      //oControl = (TextBox)HttpContext.Current.Session["oTEXTBOX"];
      if (sAlert)
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = Color.Lavender;
        tx_Control.ForeColor = Color.Red;
        tx_Control.Font.Bold = true;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
      else
      {
        lb_Control.BackColor = default(Color);
        lb_Control.ForeColor = default(Color);
        //
        tx_Control.BackColor = default(Color);
        tx_Control.ForeColor = default(Color);
        tx_Control.Font.Bold = false;
        //tx_Control.ControlStyle.BorderStyle = default(BorderStyle);
        tx_Control.BorderStyle = BorderStyle.NotSet;
        tx_Control.BorderColor = default(Color);
      }
    }

    public static string get_ColFromKey(Infragistics.Web.UI.GridControls.GridRecordCollection dr_Row, int in_recIndex, string st_ColKey)
    {
      string st_ret = "";
      st_ret = DAC.GetStringValue(((DataRowView)dr_Row[in_recIndex].DataItem)[st_ColKey]);
      return st_ret;
    }


    #region IDisposable 成員
    public void Dispose()
    {

    }
    #endregion
  }
}
