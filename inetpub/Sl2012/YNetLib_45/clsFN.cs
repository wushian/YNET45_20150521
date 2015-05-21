using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;

namespace YNetLib_45
{
  public class clsFN : DAC
  {


    public clsFN()
      : base()
    {
    }

    public string Get_guid5()
    {
      string gkey = Guid.NewGuid().ToString("N").Substring(27, 5);
      return gkey;
    }

    public void DocDEL_Pic(string PicFilePath, string st_func, string st_ren, string st_index)
    {
      string st_picname = st_ren;
      string st_picpath = PicFilePath + st_func + @"\";
      if (st_index != "")
      {
        st_picname = st_picname + "_" + st_index;
      }
      string vPICNAME = GetPicName(st_picname, st_picpath, true);
      if (vPICNAME != "")
      {
        File.Delete(st_picpath + vPICNAME);
      }
    }

    public bool DocMOD_PIC(HtmlInputFile attimg, string st_PicFilePath, string st_PicExtension, string st_func, string st_ren, string st_index, ref string st_errMsg)
    {
      bool bl_modok = false;
      string st_uploadfile = "", st_uploadfile_Ext = "";
      string st_picname = st_ren;
      if (st_index != "")
      {
        st_picname = st_picname + "_" + st_index;
      }
      string st_picpath = st_PicFilePath + st_func + @"\";
      string st_savefile = "";
      try
      {
        st_uploadfile = attimg.PostedFile.FileName.ToUpper();
        st_uploadfile_Ext = GetFileExtName(st_uploadfile).ToUpper();
        if (st_PicExtension.ToUpper().IndexOf(st_uploadfile_Ext) >= 0)
        {
          st_savefile = st_picpath + st_picname + "." + st_uploadfile_Ext;
          attimg.PostedFile.SaveAs(st_savefile);
          bl_modok = true;
        }
        else
        {
          bl_modok = false;
          st_errMsg = "請使用圖形格式(" + st_PicExtension + " ";
        }
      }
      catch (Exception ex)
      {
        bl_modok = false;
        st_errMsg = ex.Message;
      }
      return bl_modok;
    }


    public CheckBoxList CheckBoxListFromTable(ref CheckBoxList ck_CheckBoxList, string st_tablename, string st_selectvalue, string st_selecttext, string st_wherekey, string st_orderby)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectTableForDropDownList(st_tablename, st_selectvalue, st_selecttext, st_wherekey, st_orderby);
      ck_CheckBoxList.DataSource = tb_ListSource;
      ck_CheckBoxList.DataValueField = "class_value";
      ck_CheckBoxList.DataTextField = "class_text";
      ck_CheckBoxList.DataBind();
      tb_ListSource.Dispose();
      return ck_CheckBoxList;
    }


    public DataTable SelectTableForDropDownList(string st_tablename, string st_selectvalue, string st_selecttext, string st_wherekey, string st_orderby)
    {
      OleDbCommand cmd = DAC.NewCommand();
      cmd.CommandText = "select " + st_selectvalue + " as class_value," + st_selecttext + " as class_text from " + st_tablename + " with (nolock) ";
      if (st_wherekey != "") cmd.CommandText += " where " + st_wherekey;
      if (st_orderby != "") cmd.CommandText += " order by " + st_orderby;
      return Select(cmd);
    }

    public DataTable SelectTableForDropDownList_(string st_tablename, string st_selectvalue, string st_selecttext, string st_joinsql, string st_wherekey, string st_orderby, string st_blankname)
    {
      OleDbCommand cmd = DAC.NewCommand();
      cmd.CommandText = "select top 1 '_' as class_value,'" + st_blankname + "' as class_text from " + st_tablename + " with (nolock) ";
      cmd.CommandText += " union ";
      cmd.CommandText += "select " + st_selectvalue + " as class_value," + st_selecttext + " as class_text from " + st_tablename + " with (nolock) ";
      if (st_joinsql != "") cmd.CommandText += " " + st_joinsql + " ";
      if (st_wherekey != "") cmd.CommandText += " where " + st_wherekey;
      if (st_orderby != "") cmd.CommandText += " order by class_value"; // Only in this way use
      return Select(cmd);
    }

    public DataTable SelectClassesForDropDownList(string st_type, string st_text, string st_where, string st_orderby)
    {
      OleDbCommand cmd = DAC.NewCommand();
      cmd.CommandText = "select class_type,class_value," + st_text + " as class_text from classes with (nolock) ";
      cmd.CommandText += " where class_type=? ";
      AddParam(cmd, "class_type", st_type);
      if (st_where != "")
      {
        cmd.CommandText += " and " + st_where + " ";
      }
      if (st_orderby != "") cmd.CommandText += " order by " + st_orderby;
      return Select(cmd);
    }

    public DataTable SelectClassesForDropDownList_(string st_type, string st_text, string st_where, string st_orderby, string st_blankname)
    {
      OleDbCommand cmd = DAC.NewCommand();
      cmd.CommandText += "select top 1 class_type, '_' as class_value,'(未選擇)' as class_text from classes with (nolock) ";
      cmd.CommandText += "union ";
      cmd.CommandText += "select class_type,class_value, " + st_text + "  as class_text from classes with (nolock) ";
      cmd.CommandText += " where class_type=? ";
      AddParam(cmd, "class_type", st_type);
      if (st_where != "")
      {
        cmd.CommandText += " and " + st_where + " ";
      }
      if (st_orderby != "") cmd.CommandText += " order by class_value "; // Only in this way use
      return Select(cmd);
    }

    //public DataTable SelectClassesForDropDownList(string st_type, string st_text, string st_where, string st_orderby)
    //{
    //  OleDbCommand cmd = DAC.NewCommand();
    //  cmd.CommandText = "select class_type,class_value," + st_text + " as class_text from classes with (nolock) ";
    //  cmd.CommandText += " where class_type=? ";
    //  AddParam(cmd, "class_type", st_type);
    //  if (st_where != "")
    //  {
    //    cmd.CommandText += " and " + st_where + " ";
    //  }
    //  if (st_orderby != "") cmd.CommandText += " order by " + st_orderby;
    //  return Select(cmd);
    //}

    public DropDownList DropDownListFromClasses(ref DropDownList dr_DownList, string st_type, string st_text, string st_orderby)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectClassesForDropDownList(st_type, st_text, "", st_orderby);
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList;
    }

    public DropDownList DropDownListFromClasses_(ref DropDownList dr_DownList, string st_type, string st_text, string st_orderby, string st_blankname)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectClassesForDropDownList_(st_type, st_text, "", "class_value", st_blankname);
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList;
    }

    public DropDownList DropDownListFromClasses(ref DropDownList dr_DownList, string st_type, string st_text, string st_wehere, string st_orderby)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectClassesForDropDownList(st_type, st_text, st_wehere, st_orderby);
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList;
    }

    public Infragistics.Web.UI.ListControls.WebDropDown DropDownListFromTable(ref Infragistics.Web.UI.ListControls.WebDropDown dr_DownList_ig, string st_tablename, string st_selectvalue, string st_selecttext, string st_wherekey, string st_orderby)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectTableForDropDownList(st_tablename, st_selectvalue, st_selecttext, st_wherekey, st_orderby);
      DataView dv = tb_ListSource.DefaultView;
      dr_DownList_ig.DataSource = dv;
      dr_DownList_ig.TextField = "class_text";
      dr_DownList_ig.ValueField = "class_value";
      dr_DownList_ig.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList_ig;
    }

    public Infragistics.Web.UI.ListControls.WebDropDown DropDownListFromTable(ref Infragistics.Web.UI.ListControls.WebDropDown dr_DownList_ig, string st_tablename, string st_selectvalue, string st_selecttext, string st_joinsql, string st_wherekey, string st_orderby)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectTableForDropDownList(st_tablename, st_selectvalue, st_selecttext, st_wherekey, st_orderby);
      DataView dv = tb_ListSource.DefaultView;
      dr_DownList_ig.DataSource = dv;
      dr_DownList_ig.TextField = "class_text";
      dr_DownList_ig.ValueField = "class_value";
      dr_DownList_ig.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList_ig;
    }
    public Infragistics.Web.UI.ListControls.WebDropDown DropDownListFromTable_(ref Infragistics.Web.UI.ListControls.WebDropDown dr_DownList_ig, string st_tablename, string st_selectvalue, string st_selecttext, string st_joinsql, string st_wherekey, string st_orderby, string st_blankname)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectTableForDropDownList_(st_tablename, st_selectvalue, st_selecttext, st_joinsql, st_wherekey, st_orderby, st_blankname);
      DataView dv = tb_ListSource.DefaultView;
      dr_DownList_ig.DataSource = dv;
      dr_DownList_ig.TextField = "class_text";
      dr_DownList_ig.ValueField = "class_value";
      dr_DownList_ig.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList_ig;
    }

    public Infragistics.Web.UI.ListControls.WebDropDown DropDownListFromClasses_(ref Infragistics.Web.UI.ListControls.WebDropDown dr_DownList_ig, string st_type, string st_text, string st_orderby, string st_blankname)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectClassesForDropDownList_(st_type, st_text, "", "class_value", st_blankname);
      dr_DownList_ig.DataSource = tb_ListSource;
      dr_DownList_ig.ValueField = "class_value";
      dr_DownList_ig.TextField = "class_text";
      dr_DownList_ig.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList_ig;
    }


    public DropDownList DropDownListFromTable(ref DropDownList dr_DownList, string st_tablename, string st_selectvalue, string st_selecttext, string st_wherekey, string st_orderby)
    {
      DataTable tb_ListSource;
      tb_ListSource = SelectTableForDropDownList(st_tablename, st_selectvalue, st_selecttext, st_wherekey, st_orderby);
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList;
    }

    public DropDownList DropDownListFromTable_(ref DropDownList dr_DownList, string st_tablename, string st_selectvalue, string st_selecttext, string st_wherekey, string st_orderby)
    {
      DataTable tb_ListSource;
      DataRow dr_Row;
      tb_ListSource = SelectTableForDropDownList(st_tablename, st_selectvalue, st_selecttext, st_wherekey, st_orderby);
      dr_Row = tb_ListSource.NewRow();
      dr_Row["class_value"] = "_";
      dr_Row["class_text"] = " ";
      tb_ListSource.Rows.Add(dr_Row);
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList;
    }

    public DropDownList DropDownListFromInt(ref DropDownList dr_DownList, int in_start, int int_end, int int_add)
    {
      OleDbCommand cmd = DAC.NewCommand();
      DataTable tb_ListSource;
      DataRow dr_Row;
      cmd.CommandText = "select 0 as class_value,0 as class_text from classes with (nolock) where 1=0 ";
      tb_ListSource = Select(cmd);
      if (int_add < in_start)
      {
        dr_Row = tb_ListSource.NewRow();
        dr_Row["class_value"] = int_add.ToString();
        dr_Row["class_text"] = int_add.ToString();
        tb_ListSource.Rows.Add(dr_Row);
      }
      for (int vi = in_start; vi <= int_end; vi++)
      {
        dr_Row = tb_ListSource.NewRow();
        dr_Row["class_value"] = vi.ToString();
        dr_Row["class_text"] = vi.ToString();
        tb_ListSource.Rows.Add(dr_Row);
      }
      if (int_add > int_end)
      {
        dr_Row = tb_ListSource.NewRow();
        dr_Row["class_value"] = int_add.ToString();
        dr_Row["class_text"] = int_add.ToString();
        tb_ListSource.Rows.Add(dr_Row);
      }
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList;
    }

    public DropDownList DropDownListFromIntStringZero(ref DropDownList dr_DownList, int in_start, int int_end, int int_add, int in_strlen)
    {
      OleDbCommand cmd = DAC.NewCommand();
      DataTable tb_ListSource;
      DataRow dr_Row;
      cmd.CommandText = "select class_value as class_value,class_text as class_text from classes with (nolock) where 1=0 ";
      tb_ListSource = Select(cmd);
      if (int_add < in_start)
      {
        dr_Row = tb_ListSource.NewRow();
        dr_Row["class_value"] = strzeroi(int_add, in_strlen);
        dr_Row["class_text"] = strzeroi(int_add, in_strlen);
        tb_ListSource.Rows.Add(dr_Row);
      }
      for (int vi = in_start; vi <= int_end; vi++)
      {
        dr_Row = tb_ListSource.NewRow();
        dr_Row["class_value"] = strzeroi(vi, in_strlen);
        dr_Row["class_text"] = strzeroi(vi, in_strlen);
        tb_ListSource.Rows.Add(dr_Row);
      }
      if (int_add > int_end)
      {
        dr_Row = tb_ListSource.NewRow();
        dr_Row["class_value"] = strzeroi(int_add, in_strlen);
        dr_Row["class_text"] = strzeroi(int_add, in_strlen);
        tb_ListSource.Rows.Add(dr_Row);
      }
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      tb_ListSource.Dispose();
      return dr_DownList;
    }

    public DropDownList SetDropDownList(ref DropDownList dr_DownList, string st_value)
    {
      int in_idx = -1;
      for (int i = 0; i < dr_DownList.Items.Count; i++)
      {
        if (st_value == dr_DownList.Items[i].Value) in_idx = i;
      }
      dr_DownList.SelectedIndex = in_idx;
      return dr_DownList;
    }

    public DropDownList DropDownList_YYYYMM(ref DropDownList dr_DownList, DateTime dt_now, int in_Monthsago, int in_nextMonths, string st_TYPE, int in_DefIndex)
    {
      //sTYPE=YYYYMM  sTXT       sVAL 
      //      YYMMFM  2007/01/01 2007/01       
      //      YYMMLM  2007/01/31 2007/01       
      //      YYMMFD  2007/01/01 2007/01/01       
      //      YYMMLD  2007/01/31 2007/01/31       
      OleDbCommand cmd = DAC.NewCommand();
      DataTable tb_ListSource;
      DataRow dr_Row;
      DateTime dr_sRDT;
      string st_TEXT = "", st_VALUE = "", st_Ident = "-";
      cmd.CommandText = "select class_value as class_value,class_text as class_text from classes with (nolock) where 1=0 ";
      tb_ListSource = Select(cmd);
      for (int vi = in_Monthsago; vi <= in_nextMonths; vi++)
      {
        if (st_TYPE.Substring(4, 1) == "F")
        {
          dr_sRDT = DateToFirstDate(dt_now.AddMonths(vi));
        }
        else
        {
          dr_sRDT = DateToLastDate(dt_now.AddMonths(vi));
        }
        st_VALUE = strzeroi(dr_sRDT.Year, 4) + st_Ident + strzeroi(dr_sRDT.Month, 2);
        st_VALUE = st_VALUE + st_Ident + strzeroi(dr_sRDT.Day, 2);
        //
        st_TEXT = strzeroi(dr_sRDT.Year, 4) + st_Ident + strzeroi(dr_sRDT.Month, 2);
        if (st_TYPE.Substring(5, 1) == "D")
        {
          st_TEXT = st_TEXT + st_Ident + strzeroi(dr_sRDT.Day, 2);
        }
        //
        dr_Row = tb_ListSource.NewRow();
        dr_Row["class_value"] = st_VALUE;
        dr_Row["class_text"] = st_TEXT;
        tb_ListSource.Rows.Add(dr_Row);
      }
      dr_DownList.DataSource = tb_ListSource;
      dr_DownList.DataValueField = "class_value";
      dr_DownList.DataTextField = "class_text";
      dr_DownList.DataBind();
      if (in_DefIndex < dr_DownList.Items.Count)
      {
        dr_DownList.SelectedIndex = in_DefIndex;
      }
      //
      tb_ListSource.Dispose();
      cmd.Dispose();
      return dr_DownList;
    }

    public Infragistics.Web.UI.ListControls.WebDropDown setWebDropDownListSelectedFromString(ref Infragistics.Web.UI.ListControls.WebDropDown WebDropDown, string st_selectedstring)
    {
      string st_value;
      st_selectedstring += ",";
      for (int vdi = 0; vdi < WebDropDown.Items.Count; vdi++)
      {
        st_value = WebDropDown.Items[vdi].Value;
        WebDropDown.Items[vdi].Selected = false;
        //
        if (st_selectedstring.IndexOf(st_value + ",") >= 0)
        {
          WebDropDown.Items[vdi].Selected = true;
        }
        else
        {
          WebDropDown.Items[vdi].Selected = false;
        }
      }
      return WebDropDown;
    }

    public string getWebDropDownListSelectedString(Infragistics.Web.UI.ListControls.WebDropDown WebDropDown)
    {
      string st_value;
      st_value = "";
      for (int in_si = 0; in_si < WebDropDown.SelectedItems.Count; in_si++)
      {
        if (st_value == "")
        {
          st_value += WebDropDown.SelectedItems[in_si].Value;
        }
        else
        {
          st_value += "," + WebDropDown.SelectedItems[in_si].Value;
        }
      }
      return st_value;
    }


    public DateTime DateToFirstDate(DateTime dt_Date)
    {
      //取得當月份第一天 Ex:2008/01/12 RETURN>2008/01/01
      DateTime dt_Odate;
      int in_datx = 0;
      dt_Odate = dt_Date;
      in_datx = dt_Odate.Day;
      dt_Odate = dt_Odate.AddDays(-in_datx + 1);
      return dt_Odate;
    }

    public DateTime DateToLastDate(DateTime dt_Date)
    {
      //取得當月份最後一天 Ex:2008/01/12 RETURN>2008/01/31
      DateTime dt_Odate;
      int in_datx = 0;
      dt_Odate = dt_Date.AddMonths(1);
      in_datx = dt_Odate.Day;
      dt_Odate = dt_Odate.AddDays(-in_datx);
      return dt_Odate;
    }

    public string strzeroi(int in_input, int in_strlen)
    {
      string st_str1 = "", st_Str2 = "";
      int in_len1 = 0, in_len2 = 0, in_len3 = 0;
      st_str1 = in_input.ToString();
      st_Str2 = "";
      in_len1 = st_str1.Length;
      in_len2 = in_strlen - in_len1;
      in_len3 = 0;
      while (in_len3 < in_len2)
      {
        st_Str2 = st_Str2 + "0";
        in_len3 = in_len3 + 1;
      }
      return (st_Str2 + st_str1);
    }

    public string datetostr(DateTime dt_idate)
    {
      return strzeroi(dt_idate.Year, 4) + "-" + strzeroi(dt_idate.Month, 2) + "-" + strzeroi(dt_idate.Day, 2);
    }

    public Menu SetMenu(Menu MenuM)
    {
      DataTable tb_menu = new DataTable();
      DataRow mRow;
      DataRow[] mRowA, mRowB, mRowC;

      OleDbCommand cmd = DAC.NewCommand();
      MenuItem mu_ItemB, mu_ItemC;
      //
      string st_prg_code = "";
      string st_prg_text = "";
      string st_prg_url = "";
      MenuM.Items.Clear();
      //
      tb_menu.Clear();
      cmd.CommandText = "SELECT prg_code,prg_name,chinesebigname,chinesesimpname,englishname,vietnamname,obj_name,openurl  from sys_menu order by prg_code ";
      tb_menu = Select(cmd);
      mRowA = tb_menu.Select(" len(prg_code)=2 ", "prg_code");
      //
      for (int va = 0; va < mRowA.Length; va++)
      {
        mRow = mRowA[va];
        st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
        st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]);
        st_prg_url = "#";
        MenuM.Items.Add(new MenuItem(st_prg_text, st_prg_code, "", st_prg_url));
        mu_ItemB = MenuM.Items[va];
        //
        mRowB = tb_menu.Select(" len(prg_code)=4 and prg_code like '" + st_prg_code + "%' ", "prg_code");
        for (int vb = 0; vb < mRowB.Length; vb++)
        {
          mRow = mRowB[vb];
          st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
          st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]);
          st_prg_url = "#";
          mu_ItemB.ChildItems.Add(new MenuItem(st_prg_text, st_prg_code, "", st_prg_url));
          mu_ItemC = mu_ItemB.ChildItems[vb];
          //
          mRowC = tb_menu.Select(" len(prg_code)=6 and prg_code like '" + st_prg_code + "%' ", "prg_code");
          for (int vc = 0; vc < mRowC.Length; vc++)
          {
            mRow = mRowC[vc];
            st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
            st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]);
            st_prg_url = DAC.GetStringValue(mRow["openurl"]);
            mu_ItemC.ChildItems.Add(new MenuItem(st_prg_text, st_prg_code, "", st_prg_url));
          }
          //
        }
        //
      }
      //
      return (MenuM);
    }

    public string SetMenu_Webli(string st_LangType, string httpAppRoot, string st_UserId)
    {
      DataTable tb_menu = new DataTable();
      DataRow mRow;
      DataRow[] mRowA, mRowB, mRowC;

      OleDbCommand cmd = DAC.NewCommand();
      //
      httpAppRoot = httpAppRoot + @"\";
      string st_li = "";
      st_li += "<ul id='MenuBar1' class='MenuBarHorizontal'>";
      string st_prg_code = "";
      string st_prg_text = "";
      string st_prg_url = "";
      //
      tb_menu.Clear();
      cmd.CommandText = "SELECT prg_code,prg_name,chinesebigname,chinesesimpname,englishname,vietnamname,obj_name,openurl  from sys_menu where prgflag<>'B' order by prg_code ";
      tb_menu = Select(cmd);
      mRowA = tb_menu.Select(" len(prg_code)=2 ", "prg_code");
      //
      for (int va = 0; va < mRowA.Length; va++)
      {
        mRow = mRowA[va];
        st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
        if (st_LangType == "t") { st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]); }
        else if (st_LangType == "c") { st_prg_text = DAC.GetStringValue(mRow["chinesesimpname"]); }
        else if (st_LangType == "e") { st_prg_text = DAC.GetStringValue(mRow["englishname"]); }
        else { st_prg_text = DAC.GetStringValue(mRow["vietnamname"]); }
        st_prg_url = "#";
        //
        if (st_UserId == "Admin")
        {
          mRowB = tb_menu.Select(" len(prg_code)=4 and prg_code like '" + st_prg_code + "%' ", "prg_code");
        }
        else
        {
          mRowB = tb_menu.Select(" (not (prg_code like '0002%')) and len(prg_code)=4 and prg_code like '" + st_prg_code + "%' ", "prg_code");
        }
        for (int vb = 0; vb < mRowB.Length; vb++)
        {
          mRow = mRowB[vb];
          st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
          if (st_LangType == "t") { st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]); }
          else if (st_LangType == "c") { st_prg_text = DAC.GetStringValue(mRow["chinesesimpname"]); }
          else if (st_LangType == "e") { st_prg_text = DAC.GetStringValue(mRow["englishname"]); }
          else { st_prg_text = DAC.GetStringValue(mRow["vietnamname"]); }
          st_prg_url = "#";
          st_li += "<li><a href='" + st_prg_url + "'  class='MenuBarItemSubmenu'>" + st_prg_text + "</a>";
          st_li += "<ul>";
          //
          mRowC = tb_menu.Select(" len(prg_code)=6 and prg_code like '" + st_prg_code + "%' ", "prg_code");
          for (int vc = 0; vc < mRowC.Length; vc++)
          {
            mRow = mRowC[vc];
            st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
            if (st_LangType == "t") { st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]); }
            else if (st_LangType == "c") { st_prg_text = DAC.GetStringValue(mRow["chinesesimpname"]); }
            else if (st_LangType == "e") { st_prg_text = DAC.GetStringValue(mRow["englishname"]); }
            else { st_prg_text = DAC.GetStringValue(mRow["vietnamname"]); }
            st_prg_url = DAC.GetStringValue(mRow["openurl"]);
            st_li += "<li><a href='" + httpAppRoot + st_prg_url + "'>" + st_prg_text + "</a>";
            //mu_ItemC.ChildItems.Add(new MenuItem(st_prg_text, st_prg_code, "", st_prg_url));
          }
          st_li += "  </ul>";
          st_li += "</li>";
        }
        //st_li += "  <ul>";
      }
      st_li += "</ul>";
      tb_menu.Dispose();
      //
      return (st_li);
    }

    public Infragistics.Web.UI.NavigationControls.WebDataMenu SetMenu_WebDataMenu(Infragistics.Web.UI.NavigationControls.WebDataMenu mu_WebMenu, string st_LangType, string httpAppRoot, string st_UserId)
    {
      DataTable tb_menu = new DataTable();
      DataRow mRow;
      DataRow[] mRowA, mRowB, mRowC;

      OleDbCommand cmd = DAC.NewCommand();
      //
      httpAppRoot = httpAppRoot + @"\";
      string st_prg_code = "";
      string st_prg_text = "";
      string st_prg_url = "";
      //
      tb_menu.Clear();
      cmd.CommandText = "SELECT prg_code,prg_name,chinesebigname,chinesesimpname,englishname,vietnamname,obj_name,openurl  from sys_menu where prgflag<>'B' order by prg_code ";
      tb_menu = Select(cmd);
      mRowA = tb_menu.Select(" len(prg_code)=2 ", "prg_code");
      //
      //mu_WebMenu.Items.Add(new Infragistics.Web.UI.NavigationControls.DataMenuItem("功能表", "", "", ""));
      for (int va = 0; va < mRowA.Length; va++)
      {
        mRow = mRowA[va];
        st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
        if (st_LangType == "t") { st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]); }
        else if (st_LangType == "c") { st_prg_text = DAC.GetStringValue(mRow["chinesesimpname"]); }
        else if (st_LangType == "e") { st_prg_text = DAC.GetStringValue(mRow["englishname"]); }
        else { st_prg_text = DAC.GetStringValue(mRow["vietnamname"]); }
        st_prg_url = DAC.GetStringValue(mRow["openurl"]).Trim();
        if (st_prg_url != "")
        {
          st_prg_url = httpAppRoot + st_prg_url;
        }
        //
        if (st_UserId == "Admin")
        {
          mRowB = tb_menu.Select(" len(prg_code)=4 and prg_code like '" + st_prg_code + "%' ", "prg_code");
        }
        else
        {
          mRowB = tb_menu.Select(" (not (prg_code like '0002%')) and len(prg_code)=4 and prg_code like '" + st_prg_code + "%' ", "prg_code");
        }
        //
        Infragistics.Web.UI.NavigationControls.DataMenuItem DataMenuItemB = new Infragistics.Web.UI.NavigationControls.DataMenuItem(st_prg_text, st_prg_url, "_self", "");
        for (int vb = 0; vb < mRowB.Length; vb++)
        {
          mRow = mRowB[vb];
          st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
          if (st_LangType == "t") { st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]); }
          else if (st_LangType == "c") { st_prg_text = DAC.GetStringValue(mRow["chinesesimpname"]); }
          else if (st_LangType == "e") { st_prg_text = DAC.GetStringValue(mRow["englishname"]); }
          else { st_prg_text = DAC.GetStringValue(mRow["vietnamname"]); }
          st_prg_url = DAC.GetStringValue(mRow["openurl"]);
          if (st_prg_url != "")
          {
            st_prg_url = httpAppRoot + st_prg_url;
          }
          //
          Infragistics.Web.UI.NavigationControls.DataMenuItem DataMenuItemC = new Infragistics.Web.UI.NavigationControls.DataMenuItem(st_prg_text, st_prg_url, "_self", "");
          mRowC = tb_menu.Select(" len(prg_code)=6 and prg_code like '" + st_prg_code + "%' ", "prg_code");
          for (int vc = 0; vc < mRowC.Length; vc++)
          {
            mRow = mRowC[vc];
            st_prg_code = DAC.GetStringValue(mRow["prg_code"]);
            if (st_LangType == "t") { st_prg_text = DAC.GetStringValue(mRow["chinesebigname"]); }
            else if (st_LangType == "c") { st_prg_text = DAC.GetStringValue(mRow["chinesesimpname"]); }
            else if (st_LangType == "e") { st_prg_text = DAC.GetStringValue(mRow["englishname"]); }
            else { st_prg_text = DAC.GetStringValue(mRow["vietnamname"]); }
            st_prg_url = DAC.GetStringValue(mRow["openurl"]);
            if (st_prg_url != "")
            {
              st_prg_url = httpAppRoot + st_prg_url;
            }
            //
            Infragistics.Web.UI.NavigationControls.DataMenuItem DataMenuItemD = new Infragistics.Web.UI.NavigationControls.DataMenuItem(st_prg_text, st_prg_url, "_self", "");
            DataMenuItemC.Items.Add(DataMenuItemD);
          }
          ////DataMenuItemB.Items.Add(DataMenuItemC);
          //mu_WebMenu.Items[0].Items.Add(DataMenuItemC);
          mu_WebMenu.Items.Add(DataMenuItemC);
        }
        ////mu_WebMenu.Items[0].Items.Add(DataMenuItemB);
      }
      tb_menu.Dispose();
      //
      return (mu_WebMenu);
    }


    public TreeView SetTreeView_sys_menu(TreeView TreeViewM, string st_func, string st_LangType, string st_UserId)
    {
      DataTable tb_menu = new DataTable();
      DataRow mRow;
      DataRow[] mRowA, mRowB, mRowC;

      OleDbCommand cmd = DAC.NewCommand();
      TreeNode tr_NodeB, tr_NodeC, tr_NodeD;
      //
      string st_gkey = "";
      string st_sysno = "";
      string st_sysname_t = "";
      TreeViewM.Nodes.Clear();
      //
      tb_menu.Clear();
      cmd.CommandText = @"select prg_code,prg_name,obj_name,pic_name,win_class,fram_class,prvl_class,parent_code,prg_serialno,ftflag,startqty,endqty,yearly,season,
                                  chinesesimpname,chinesebigname,englishname,vietnamname,sysflag,pictype,initflag,opentype,makerdf,prgflag,openurl,pagesize  from sys_menu ";
      tb_menu = Select(cmd);
      //
      if (st_UserId == "Admin")
      {
        mRowA = tb_menu.Select(" prvl_class='1' ", "prg_code");
      }
      else
      {
        mRowA = tb_menu.Select(" (not (prg_code='00')) and prvl_class='1' ", "prg_code");
      }

      //
      for (int va = 0; va < mRowA.Length; va++)
      {
        mRow = mRowA[va];
        st_gkey = DAC.GetStringValue(mRow["obj_name"]);
        st_sysno = DAC.GetStringValue(mRow["prg_code"]);
        if (st_LangType == "c")
        {
          st_sysname_t = DAC.GetStringValue(mRow["chinesesimpname"]);
        }
        else if (st_LangType == "t")
        {
          st_sysname_t = DAC.GetStringValue(mRow["chinesebigname"]);
        }
        else if (st_LangType == "e")
        {
          st_sysname_t = DAC.GetStringValue(mRow["englishname"]);
        }
        else if (st_LangType == "v")
        {
          st_sysname_t = DAC.GetStringValue(mRow["vietnamname"]);
        }
        else
        {
          st_sysname_t = DAC.GetStringValue(mRow["englishname"]);
        }

        TreeViewM.Nodes.Add(new TreeNode(st_sysname_t, st_gkey, "", st_func + HttpContext.Current.Server.HtmlEncode(st_gkey), "_self"));
        tr_NodeB = TreeViewM.Nodes[va];
        if (st_UserId == "Admin")
        {
          mRowB = tb_menu.Select(" prvl_class='2' and parent_code= '" + st_gkey + "' ", "prg_code");
        }
        else
        {
          mRowB = tb_menu.Select(" (not (  (prg_code='0001') or (prg_code='0002') )) and prvl_class='2' and parent_code= '" + st_gkey + "' ", "prg_code");
        }
        for (int vb = 0; vb < mRowB.Length; vb++)
        {
          mRow = mRowB[vb];
          st_gkey = DAC.GetStringValue(mRow["obj_name"]);
          st_sysno = DAC.GetStringValue(mRow["prg_code"]);
          //st_sysname_t = DAC.GetStringValue(mRow["chinesesimpname"]);
          if (st_LangType == "c")
          {
            st_sysname_t = DAC.GetStringValue(mRow["chinesesimpname"]);
          }
          else if (st_LangType == "t")
          {
            st_sysname_t = DAC.GetStringValue(mRow["chinesebigname"]);
          }
          else if (st_LangType == "e")
          {
            st_sysname_t = DAC.GetStringValue(mRow["englishname"]);
          }
          else if (st_LangType == "v")
          {
            st_sysname_t = DAC.GetStringValue(mRow["vietnamname"]);
          }
          else
          {
            st_sysname_t = DAC.GetStringValue(mRow["englishname"]);
          }
          //
          tr_NodeB.ChildNodes.Add(new TreeNode(st_sysname_t, st_gkey, "", st_func + HttpContext.Current.Server.HtmlEncode(st_gkey), "_self"));
          tr_NodeC = tr_NodeB.ChildNodes[vb];
          //
          mRowC = tb_menu.Select(" prvl_class=3 and parent_code='" + st_gkey + "' ", "prg_code");
          for (int vc = 0; vc < mRowC.Length; vc++)
          {
            mRow = mRowC[vc];
            st_gkey = DAC.GetStringValue(mRow["obj_name"]);
            st_sysno = DAC.GetStringValue(mRow["prg_code"]);
            //st_sysname_t = DAC.GetStringValue(mRow["chinesesimpname"]);
            if (st_LangType == "c")
            {
              st_sysname_t = DAC.GetStringValue(mRow["chinesesimpname"]);
            }
            else if (st_LangType == "t")
            {
              st_sysname_t = DAC.GetStringValue(mRow["chinesebigname"]);
            }
            else if (st_LangType == "e")
            {
              st_sysname_t = DAC.GetStringValue(mRow["englishname"]);
            }
            else if (st_LangType == "v")
            {
              st_sysname_t = DAC.GetStringValue(mRow["vietnamname"]);
            }
            else
            {
              st_sysname_t = DAC.GetStringValue(mRow["englishname"]);
            }
            //
            tr_NodeC.ChildNodes.Add(new TreeNode(st_sysname_t, st_gkey, "", st_func + HttpContext.Current.Server.HtmlEncode(st_gkey), "_self"));
            tr_NodeD = tr_NodeC.ChildNodes[vc];
            //
          }
          //
        }
        //
      }
      //
      return (TreeViewM);
    }


    public TreeView SetTreeView_ss155(TreeView TreeViewM)
    {
      DataTable tb_menu = new DataTable();
      DataRow mRow;
      DataRow[] mRowA, mRowB, mRowC, mRowD;

      OleDbCommand cmd = DAC.NewCommand();
      TreeNode tr_NodeB, tr_NodeC, tr_NodeD;
      //
      string st_gkey = "";
      string st_sysno = "";
      string st_sysname_t = "";
      TreeViewM.Nodes.Clear();
      //
      tb_menu.Clear();
      cmd.CommandText = "select gkey,sysno,sysname,serialno,layer,parentgkey,is_window,sysname_e,sysname_t,sysname_c,sysname_v,group_id from ss155 order by layer,sysno ";
      tb_menu = Select(cmd);
      mRowA = tb_menu.Select(" layer=1 ", "serialno");
      //
      for (int va = 0; va < mRowA.Length; va++)
      {
        mRow = mRowA[va];
        st_gkey = DAC.GetStringValue(mRow["gkey"]);
        st_sysno = DAC.GetStringValue(mRow["sysno"]);
        st_sysname_t = DAC.GetStringValue(mRow["sysname_t"]);
        TreeViewM.Nodes.Add(new TreeNode(st_sysname_t, st_gkey, "", "fm_ss160.aspx?gkey=" + HttpContext.Current.Server.HtmlEncode(st_gkey), "_self"));
        tr_NodeB = TreeViewM.Nodes[va];
        //
        mRowB = tb_menu.Select(" layer=2 and parentgkey= '" + st_gkey + "' ", "serialno");
        for (int vb = 0; vb < mRowB.Length; vb++)
        {
          mRow = mRowB[vb];
          st_gkey = DAC.GetStringValue(mRow["gkey"]);
          st_sysno = DAC.GetStringValue(mRow["sysno"]);
          st_sysname_t = DAC.GetStringValue(mRow["sysname_t"]);
          tr_NodeB.ChildNodes.Add(new TreeNode(st_sysname_t, st_gkey, "", "fm_ss160.aspx?gkey=" + HttpContext.Current.Server.HtmlEncode(st_gkey), "_self"));
          tr_NodeC = tr_NodeB.ChildNodes[vb];
          //
          mRowC = tb_menu.Select(" layer=3 and parentgkey='" + st_gkey + "' ", "serialno");
          for (int vc = 0; vc < mRowC.Length; vc++)
          {
            mRow = mRowC[vc];
            st_gkey = DAC.GetStringValue(mRow["gkey"]);
            st_sysno = DAC.GetStringValue(mRow["sysno"]);
            st_sysname_t = DAC.GetStringValue(mRow["sysname_t"]);
            tr_NodeC.ChildNodes.Add(new TreeNode(st_sysname_t, st_gkey, "", "fm_ss160.aspx?gkey=" + HttpContext.Current.Server.HtmlEncode(st_gkey), "_self"));
            tr_NodeD = tr_NodeC.ChildNodes[vc];
            //
            mRowD = tb_menu.Select(" layer=4 and parentgkey='" + st_gkey + "' ", "serialno");
            for (int vd = 0; vd < mRowD.Length; vd++)
            {
              mRow = mRowD[vd];
              st_gkey = DAC.GetStringValue(mRow["gkey"]);
              st_sysno = DAC.GetStringValue(mRow["sysno"]);
              st_sysname_t = DAC.GetStringValue(mRow["sysname_t"]);
              tr_NodeD.ChildNodes.Add(new TreeNode(st_sysname_t, st_gkey, "", "fm_ss160.aspx?gkey=" + HttpContext.Current.Server.HtmlEncode(st_gkey), "_self"));
              //
            }
            //
          }
          //
        }
        //
      }
      //
      return (TreeViewM);
    }

    public void TreeViewExpandByValue(ref TreeView tr_View, String st_value)
    {
      tr_View.CollapseAll();
      TreeNode oNode = null;
      for (int int_n = 0; int_n <= tr_View.Nodes.Count - 1; int_n++)
      {
        oNode = FindNodeByValue(tr_View.Nodes[int_n], st_value);
        if (oNode != null)
        {
          int_n = tr_View.Nodes.Count;
        }
      }
      //
      if (oNode != null)
      {
        oNode.Select();
        ExpendParentNode(oNode);
      }
    }
    //
    public void ExpendParentNode(TreeNode tr_Node)
    {
      TreeNode tn_Node;
      tn_Node = tr_Node.Parent;
      if (tn_Node != null)
      {
        tn_Node.Expand();
        ExpendParentNode(tn_Node);
      }
    }
    //
    public TreeNode FindNodeByValue(TreeNode tr_Node, String st_value)
    {
      TreeNode tn_Node;
      TreeNode tn_ChildNode;
      if (tr_Node.Value == st_value) return (tr_Node);
      for (int vn = 0; vn <= tr_Node.ChildNodes.Count - 1; vn++)
      {
        tn_Node = tr_Node.ChildNodes[vn];
        tn_ChildNode = FindNodeByValue(tn_Node, st_value);
        if (tn_ChildNode != null) return (tn_ChildNode);
      }
      return null;
    }

    public static bool chkLoginState()
    {
      string chkstring = "";
      try
      {
        chkstring = HttpContext.Current.Session["UserId"].ToString();
        HttpContext.Current.Session["UserId"] = chkstring;
      }
      catch
      {
        chkstring = "";
      }
      return chkstring == "" ? false : true;
    }

    public static bool chkLoginState(string st_SessionString, string st_UserLoginGkey)
    {
      string chkstring = "";
      try
      {
        chkstring = HttpContext.Current.Session["UserId"].ToString();
        HttpContext.Current.Session["UserId"] = chkstring;
      }
      catch
      {
        chkstring = "";
      }
      return chkstring == "" ? false : true;
    }


    /// <summary>
    /// 取得 object的有開放的按鈕表_TABLE
    /// </summary>
    public DataTable chkPrgFuncSet(DataTable tb_Func, string st_MenuFunc)
    {
      tb_Func.Clear();
      OleDbCommand cmd = NewCommand();
      cmd.CommandText = @"select ss155gkey,buttonno,button,button_e,button_t,button_c,button_v,tip_e,tip_t,tip_c,tip_v 
                          from sys_ss160 where ss155gkey like ? order by ss155gkey,buttonno ";
      AddParam(cmd, "ss155gkey", st_MenuFunc);
      tb_Func = Select(cmd);
      cmd.Dispose();
      return tb_Func;
    }

    /// <summary>
    /// 取得 object的有開放的按鈕表_string
    /// x123456789012345678901234567890 button_no
    /// x111001100110011001100110000000 1=有權限 0=沒有權限
    /// </summary>
    public string chkPrgFuncButtons(string st_MenuFunc)
    {
      string st_acc = "x";
      DataTable tb_Func = new DataTable();
      DataRow[] dr_Row;
      tb_Func = chkPrgFuncSet(tb_Func, st_MenuFunc);
      for (byte in_btnno = 1; in_btnno <= 30; in_btnno++)
      {
        dr_Row = tb_Func.Select("buttonno=" + in_btnno.ToString() + " ");
        if (dr_Row.Length > 0)
        {
          st_acc += "1";
        }
        else
        {
          st_acc += "0";
        }
      }
      tb_Func.Dispose();
      return st_acc;
    }

    /// <summary>
    /// 取得object的所有button_no的權限TABLE
    /// </summary>
    public bool chkAccessIT(string st_UserGkey)
    {
      bool bl_ret = false;
      DataTable tb_it = new DataTable();
      tb_it.Clear();
      OleDbCommand cmd = NewCommand();
      cmd.CommandText = @"select a.usertype as usertype from ss101 a where a.usertype='A' AND  a.es101gkey=? 
                          union 
                          select a.usertype as usertype from sys_ss165 a left outer join sys_ss175 b on a.gkey=b.ss165gkey where a.usertype='A' and b.es101gkey=? ";
      AddParam(cmd, "es101gkey1", st_UserGkey);
      AddParam(cmd, "es101gkey2", st_UserGkey);
      tb_it = Select(cmd);
      cmd.Dispose();
      for (int vri = 0; vri < tb_it.Rows.Count; vri++)
      {
        if (DAC.GetStringValue(tb_it.Rows[vri]["usertype"]) == "A")
        {
          bl_ret = true;
        }
      }
      tb_it.Dispose();
      return bl_ret;
    }

    /// <summary>
    /// 取得object的所有button_no的權限TABLE
    /// </summary>
    public DataTable chkAccessFuncSet(DataTable tb_Func, string st_UserGkey, string st_MenuFunc)
    {
      tb_Func.Clear();
      OleDbCommand cmd = NewCommand();
      cmd.CommandText = @"select b.ss155gkey as ss155gkey,b.buttonno as buttonno,a.ss160gkey as ss160gkey,a.mark as mark,a.es101gkey as es101gkey  
                           from sys_ss180 a
                           left outer join sys_ss160 b on a.ss160gkey=b.gkey 
                           where a.es101gkey=? and mark='1'
                                 and b.ss155gkey=?  
                           union
                           select b.ss155gkey as ss155gkey,b.buttonno as buttonno,a.ss160gkey as ss160gkey,a.mark as mark,a.ss165gkey as ss165gkey
                           from  sys_ss170 a 
                           left outer join sys_ss160 b on a.ss160gkey=b.gkey  
                           where a.ss165gkey in
                           (
                             select gkey  from sys_ss165 where ? in (select es101gkey from  sys_ss175 where ss165gkey=sys_ss165.gkey  )
                           )
                           and a.mark='1' 
                           and b.ss155gkey=? 
                           order by ss155gkey,buttonno,es101gkey";
      AddParam(cmd, "UserGkey1", st_UserGkey);
      AddParam(cmd, "MenuFunc1", st_MenuFunc);
      AddParam(cmd, "UserGkey2", st_UserGkey);
      AddParam(cmd, "MenuFunc2", st_MenuFunc);
      tb_Func = Select(cmd);
      cmd.Dispose();
      return tb_Func;
    }

    /// <summary>
    /// 取得object的所有button_no的權限string
    /// x123456789012345678901234567890 button_no
    /// x111001100110011001100110000000 1=有權限 0=沒有權限
    /// </summary>
    public string chkAccessFuncButtons(string st_UserGkey, string st_MenuFunc)
    {
      bool bl_isit = false;
      string st_acc = "x";
      DataTable tb_Func = new DataTable();
      DataRow[] dr_Row;
      bl_isit = chkAccessIT(st_UserGkey);
      if (!bl_isit)
      {
        tb_Func = chkAccessFuncSet(tb_Func, st_UserGkey, st_MenuFunc);
      }
      for (byte in_btnno = 1; in_btnno <= 30; in_btnno++)
      {
        if (bl_isit)
        {
          st_acc += "1";
        }
        else
        {
          dr_Row = tb_Func.Select("buttonno=" + in_btnno.ToString() + " ");
          if (dr_Row.Length > 0)
          {
            st_acc += "1";
          }
          else
          {
            st_acc += "0";
          }
        }
      }
      tb_Func.Dispose();
      return st_acc;
    }

    /// <summary>
    /// 取得object的button_no權限,1=有權限 0=沒有權限
    /// </summary>
    public bool chkAccessFuncButton(string st_UserGkey, string st_MenuFunc, byte in_buttonno)
    {
      string st_acc = "";
      st_acc = chkAccessFuncButtons(st_UserGkey, st_MenuFunc);
      return ((st_acc.Substring(in_buttonno, 1) == "1") || st_UserGkey=="Admin") ? true : false;
    }

    /// <summary>
    /// 檢查user是否有登入 及 object的button_no權限,1=有權限 0=沒有權限
    /// </summary>
    public bool chkAccessFunc_(string st_UserGkey, string st_MenuFunc, byte in_buttonno, string st_UserLoginGkey)
    {
      if (chkLoginState(st_UserGkey, st_UserLoginGkey))
      {
        return chkAccessFuncButton(st_UserGkey, st_MenuFunc, in_buttonno);
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// 檢查user是否有登入 及 object的button_no權限,1=有權限 0=沒有權限
    /// </summary>
    public bool checkAccessFunc(string st_UserGkey, string st_MenuFunc, byte in_buttonno, string st_UserLoginGkey, ref Literal li_AccMsg)
    {
      bool bl_ret = false;
      if (!chkLoginState(st_UserGkey, st_UserLoginGkey))
      {
        HttpContext.Current.Response.Redirect("~/Default.aspx");
      }
      else
      {
        if (st_UserGkey == "Admin")
        {
          bl_ret = true;
        }
        else if (!chkAccessFunc_(st_UserGkey, st_MenuFunc, in_buttonno, st_UserLoginGkey))
        {
          if (in_buttonno == 1)
          {
            li_AccMsg.Text = "<script> alert('" + "您沒有權限處理此程式" + "'); history.go(-1); </script>";
          }
          else
          {
            li_AccMsg.Text = "<script> alert('" + "您沒有權限處理此功能" + "'); </script>";
          }
        }
        else
        {
          bl_ret = true;
        }
      }
      return bl_ret;
    }

    public string GetFileExtName(string filename)
    {
      string st_ret = "", st_chr;
      int in_len;
      in_len = filename.Length;
      if (filename.IndexOf(".") > 0)
      {
        for (int i = in_len - 1; i >= 0; i--)
        {
          st_chr = filename.Substring(i, 1);
          if (st_chr != ".")
          {
            st_ret = st_chr + st_ret;
          }
          else
          {
            i = -1;
          }
        }
      }
      return st_ret;
    }

    public string ImageFitX(ref System.Web.UI.WebControls.Image cImage, string st_filename, decimal xImageW, decimal xImageH)
    {
      decimal vWt, vHt;
      decimal vWo, vHo;
      string st_errmsg = "";
      //
      vWt = xImageW; //目標寬度
      vHt = xImageH; //目標高度
      //
      Bitmap Imagex = new Bitmap(st_filename, true);
      vWo = Imagex.Width;  //原圖寬度
      vHo = Imagex.Height; //原圖高度
      try
      {
        if ((vWt >= vWo) && (vHt >= vHo))       // 目標寬度>原圖寬度 && 目標高度>原圖高度
        {
          xImageW = vWo; //原圖寬度
          xImageH = vHo; //原圖高度
        }
        else if ((vWt <= vWo) && (vHt >= vHo))  // 目標寬度<=原圖寬度 && 目標高度>原圖高度
        {
          xImageW = vWt;                 //目標寬度
          xImageH = vHo * (vWt / vWo);   //原圖高度*縮寬比
        }
        else if ((vWt >= vWo) && (vHt <= vHo))  // 目標寬度>=原圖寬度 && 目標高度<=原圖高度
        {
          xImageW = vWo * vHt / vHo;     //原圖寬度*縮高比
          xImageH = vHt;                 //目標高度
        }
        else         // 目標寬度<原圖寬度 && 目標高度<原圖高度
        {
          if ((vHo / vHt) >= (vWo / vWt))   //高度比較接近,使用縮高比
          {
            xImageW = vWo * vHt / vHo;     //原圖寬度*縮高比
            xImageH = vHt;                 //目標高度
          }
          else
          {
            xImageW = vWt;                  //目標寬度
            xImageH = vHo * vWt / vWo;      //原圖寬度*縮寬比
          }
        }
        cImage.Width = new System.Web.UI.WebControls.Unit(Convert.ToInt32(xImageW));
        cImage.Height = new System.Web.UI.WebControls.Unit(Convert.ToInt32(xImageH));
      }
      catch (Exception ex)
      {
        st_errmsg = ex.Message;
      }
      finally
      {
        Imagex.Dispose();
      }

      return st_errmsg;
    }

    public bool ImageFitWH(string st_filename, ref int rWIDTH, ref int rHEIGHT)
    {
      bool vOk = true;
      Bitmap Imagex = new Bitmap(st_filename, true);
      rWIDTH = Imagex.Width;
      rHEIGHT = Imagex.Height;
      Imagex.Dispose();
      return vOk;
    }

    public string GetPicName(string vPTN, string vServerPath, bool vShow)
    {
      string vPicPath, vPic;
      vPic = "";
      if (vShow)
      {
        vPicPath = vServerPath;
        if (File.Exists(vPicPath + vPTN + ".JPG")) { vPic = vPTN + ".JPG"; }
        else if (File.Exists(vPicPath + vPTN + ".JPEG")) { vPic = vPTN + ".JPEG"; }
        else if (File.Exists(vPicPath + vPTN + ".TIF")) { vPic = vPTN + ".TIF"; }
        else if (File.Exists(vPicPath + vPTN + ".BMP")) { vPic = vPTN + ".BMP"; }
        else if (File.Exists(vPicPath + vPTN + ".GIF")) { vPic = vPTN + ".GIF"; }
        else if (File.Exists(vPicPath + vPTN + ".PNG")) { vPic = vPTN + ".PNG"; }
        else if (File.Exists(vPicPath + vPTN + ".PIC")) { vPic = vPTN + ".PIC"; }
        else { vPic = ""; }
      }
      return vPic;
    }

    public bool WriteTextFile(string st_filename, string st_serverpath, string st_string)
    {
      bool bl_ret = false;
      if (File.Exists(st_serverpath + st_filename + @".txt"))
      { File.Delete(st_serverpath + st_filename + @".txt"); }
      //
      FileStream Fhdsw = new FileStream(st_serverpath + st_filename + @".txt", FileMode.CreateNew, FileAccess.Write, FileShare.None);
      StreamWriter Fhdsww = new StreamWriter(Fhdsw, Encoding.UTF8);
      try
      {
        Fhdsww.WriteLine(st_string);
        Fhdsww.Flush();
        bl_ret = true;
      }
      catch
      {
        bl_ret = false;
      }
      finally
      {
        Fhdsww.Close();
        Fhdsww.Dispose();
        Fhdsw.Dispose();
      }
      return bl_ret;
    }

    public string ReadTextFile(string st_filename, string st_serverpath)
    {
      string st_string = "";
      string st_filefullname = st_serverpath + st_filename + @".txt";
      if (File.Exists(st_filefullname))
      {
        FileStream Fhdsr = new FileStream(st_serverpath + st_filename + @".txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader Fhdsrr = new StreamReader(Fhdsr, Encoding.UTF8);
        try
        {
          st_string = Fhdsrr.ReadToEnd();
        }
        catch
        {
          st_string = "";
        }
        finally
        {
          Fhdsrr.Close();
          Fhdsrr.Dispose();
          Fhdsr.Dispose();
        }
      }
      return st_string;
    }

    public string SqlDbDate(string st_Date)
    {
      return "'" + st_Date + "'";
    }

    public string SqlDbDate(DateTime dt_Date)
    {
      return "'" + dt_Date.ToString("yyyy-MM-dd") + "'";
    }

    public string DateTimeToDateString(DateTime dt_Date, string st_DateFormat)
    {
      return dt_Date.ToString(st_DateFormat);
    }

    public string StringToDateString(string st_Date, string st_DateFormat)
    {
      return GetDateTimeValue(st_Date).ToString(st_DateFormat);
    }

    /// <summary>
    /// 將 DateTime 轉為Date string
    /// </summary>
    public DateTime DateTimeToDate(DateTime dt_DateTime)
    {
      return DAC.GetDateTimeValue(DateToDateString(dt_DateTime, "yyyy-MM-dd"));
    }

    /// <summary>
    /// 將 DateTime 轉為Date string
    /// </summary>
    public string DateToDateString(DateTime dt_Date, string st_DateFormat)
    {
      return GetDateTimeValue(dt_Date).ToString(st_DateFormat);
    }

    /// <summary>
    /// 將 DateTime_string轉為DateTime  ex:string 2014-01-01 13:59 
    /// </summary>
    public DateTime DateStringToDateTime(string st_DateTime)
    {
      return GetDateTimeValue(st_DateTime);
    }


    /// <summary>
    /// select WebDataGrid one row from mkey
    /// </summary>
    /// <param name="WebDataGrid"></param>
    /// <param name="st_mkeyset"></param>
    public void WebDataGrid_SelectRow(ref Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, string mkeyField, string mkeyValue)
    {
      string st_scolkey = "", st_smkey = "";
      int in_colindex = -1;
      //
      WebDataGrid.Behaviors.Selection.SelectedCells.Clear();
      WebDataGrid.Behaviors.Selection.SelectedRows.Clear();
      //  
      while (WebDataGrid.Behaviors.Selection.SelectedCells.Count > 0)
      {
        WebDataGrid.Behaviors.Selection.SelectedCells.RemoveAt(0);
      }
      while (WebDataGrid.Behaviors.Selection.SelectedRows.Count > 0)
      {
        WebDataGrid.Behaviors.Selection.SelectedRows.RemoveAt(0);
      }
      //
      for (var vi = 0; vi < WebDataGrid.Columns.Count; vi++)
      {
        st_scolkey = WebDataGrid.Columns[vi].Key;
        if (st_scolkey == mkeyField)
        {
          in_colindex = vi;
        }
      }
      //

      int in_selpag = -1;
      int in_selrec = -1;
      if (in_colindex >= 0)
      {
        for (var vp = 0; vp < WebDataGrid.Behaviors.Paging.PageCount; vp++)
        {
          WebDataGrid.Behaviors.Paging.PageIndex = vp;
          //for (var vr = 0; vr < WebDataGrid.Rows.Count; vr++)
          for (var vr = 0; vr < WebDataGrid.Behaviors.Paging.PageSize; vr++)
          {
            try
            {
              WebDataGrid.Behaviors.Selection.SelectedRows.Remove(WebDataGrid.Rows[vr]);
              st_smkey = DAC.GetStringValue(WebDataGrid.Rows[vr].Items[in_colindex].Value);
              if (st_smkey == mkeyValue)
              {
                in_selrec = vr;
                in_selpag = vp;
              }
              //
            }
            catch
            {
            }
          }
        }
      }
      if (in_selrec >= 0)
      {
        WebDataGrid.Behaviors.Paging.PageIndex = in_selpag;
        WebDataGrid.Behaviors.Selection.SelectedRows.Add(WebDataGrid.Rows[in_selrec]);
      }

    }


    public string WebDataGrid_NextKey(Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, string mkeyField, string mkeyValue)
    {
      string st_scolkey = "", st_smkey = "", st_nextKey = "*";
      int in_colindex = -1;
      for (var vi = 0; vi < WebDataGrid.Columns.Count; vi++)
      {
        st_scolkey = WebDataGrid.Columns[vi].Key;
        if (st_scolkey == mkeyField)
        {
          in_colindex = vi;
        }
      }
      //
      int in_selpag = -1;
      int in_selrec = -1;
      if (in_colindex >= 0)
      {
        for (var vp = 0; vp < WebDataGrid.Behaviors.Paging.PageCount; vp++)
        {
          WebDataGrid.Behaviors.Paging.PageIndex = vp;
          for (var vr = 0; vr < WebDataGrid.Behaviors.Paging.PageSize; vr++)
          {
            try
            {
              st_smkey = DAC.GetStringValue(WebDataGrid.Rows[vr].Items[in_colindex].Value);
              if (st_smkey == mkeyValue)
              {
                in_selrec = vr;
                in_selpag = vp;
              }
            }
            catch
            {
            }
          }
        }
        //
        in_selrec = in_selrec - 1;
        if (in_selrec < 0)
        {
          in_selpag = in_selpag - 1;
        }
        if (in_selpag < 0)
        {
          in_selpag = 0;
          if (in_selrec < 0)
          {
            in_selrec = 1;
          }
        }
        WebDataGrid.Behaviors.Paging.PageIndex = in_selpag;
        //
        if (in_selrec < 0)
        {
          in_selrec = WebDataGrid.Rows.Count - 1;
        }
        //
        if (in_selrec > WebDataGrid.Rows.Count - 1)
        {
          in_selrec = WebDataGrid.Rows.Count - 1;
        }

        if (in_selrec < 0)
        {
          st_nextKey = "*";
        }
        else
        {
          st_nextKey = DAC.GetStringValue(WebDataGrid.Rows[in_selrec].Items[in_colindex].Value);
        }
      }
      return st_nextKey;
    }

    public void WebDataGrid_SetEdit(ref Infragistics.Web.UI.GridControls.WebDataGrid WebDataGrid, bool bl_Edit)
    {
      if (bl_Edit)
      {
        WebDataGrid.Behaviors.EditingCore.Enabled = true;
        //WebDataGrid.Behaviors.EditingCore.Behaviors.RowAdding.Enabled = false;
        WebDataGrid.Behaviors.EditingCore.Behaviors.CellEditing.Enabled = true;
        WebDataGrid.Behaviors.EditingCore.Behaviors.RowDeleting.Enabled = true;
        try
        {
          WebDataGrid.Columns.FromKey("EDIT").Hidden = false;
        }
        catch 
        {
        }
      }
      else
      {
        WebDataGrid.Behaviors.EditingCore.Enabled = false;
        //WebDataGrid.Behaviors.EditingCore.Behaviors.RowAdding.Enabled = false;
        WebDataGrid.Behaviors.EditingCore.Behaviors.CellEditing.Enabled = false;
        WebDataGrid.Behaviors.EditingCore.Behaviors.RowDeleting.Enabled = false;
        try
        {
          WebDataGrid.Columns.FromKey("EDIT").Hidden = true;
        }
        catch
        {
        }
      }

    }


    /// <summary>
    /// 多國語 LinkButtons設定,SetLinkButtons(this,PublicVariable.LangType,st_object_func,string,UserGkey,"ctl00$ContentPlaceHolder1$", "ser")
    /// </summary>
    public void SetButtons(Page pg_This, string st_LangType, string st_object_func, string UserGkey, string st_ctl_name, string st_edittype)
    {
      string st_btn_name = "";
      string st_accstr = "";
      string st_prgstr = "";
      DataTable tb_Func = new DataTable();
      DataRow[] dr_Funcs;
      DataRow dr_Func;
      //
      st_prgstr = chkPrgFuncButtons(st_object_func);
      tb_Func = chkPrgFuncSet(tb_Func, st_object_func);
      st_accstr = chkAccessFuncButtons(UserGkey, st_object_func);
      //
      if (st_edittype.ToLower() == "mod")
      {
        for (int vi = 1; vi <= 30; vi++)
        {
          st_btn_name = st_ctl_name + "bt_" + strzeroi(vi, 2);
          LinkButton bt_set = pg_This.FindControl(st_btn_name) as LinkButton;
          try
          {
            bt_set.Visible = false;
          }
          catch
          {
          }
        }
      }
      else
      {
        for (int vi = 1; vi <= 30; vi++)
        {
          st_btn_name = st_ctl_name + "bt_" + strzeroi(vi, 2);
          LinkButton bt_set = pg_This.FindControl(st_btn_name) as LinkButton;
          try
          {
            if ((st_prgstr.Substring(vi, 1) == "1") && (UserGkey == "Admin"))
            {
              bt_set.Visible = true;
              dr_Funcs = tb_Func.Select("buttonno=" + vi.ToString());
              if (dr_Funcs.Length == 1)
              {
                dr_Func = dr_Funcs[0];
                bt_set.Text = DAC.GetStringValue(dr_Func["button_" + st_LangType]);
                bt_set.ToolTip = DAC.GetStringValue(dr_Func["tip_" + st_LangType]);
              }
            }
            else if ((st_prgstr.Substring(vi, 1) == "1") && (st_accstr.Substring(vi, 1) == "1"))
            {
              bt_set.Visible = true;
              dr_Funcs = tb_Func.Select("buttonno=" + vi.ToString());
              if (dr_Funcs.Length == 1)
              {
                dr_Func = dr_Funcs[0];
                bt_set.Text = DAC.GetStringValue(dr_Func["button_" + st_LangType]);
                bt_set.ToolTip = DAC.GetStringValue(dr_Func["tip_" + st_LangType]);
              }
            }
            else
            {
              bt_set.Visible = false;
            }
          }
          catch
          {
          }
        }
        //
      }
      tb_Func.Dispose();
    }

    /// <summary>
    /// 多國語LinkButton設定,SetLinkButton(this,"bt_SAV",PublicVariable.LangType,"ctl00$ContentPlaceHolder1$",PublicVariable.st_save,visible)
    /// </summary>
    public void SetLinkButton(Page pg_This, string st_btn_name, string st_LangType, string st_ctl_name, string st_textvalue, bool bl_visible)
    {
      string st_find_name;
      st_find_name = st_ctl_name + st_btn_name;
      LinkButton bt_LinkButton = pg_This.FindControl(st_find_name) as LinkButton;
      try { bt_LinkButton.Text = st_textvalue; bt_LinkButton.Visible = bl_visible; }
      catch { };
    }


    public void SetWebImageButtons(Page pg_This, string st_LangType, string st_object_func, string UserGkey, string st_ctl_name, string st_edittype)
    {
      string st_btn_name = "";
      string st_accstr = "";
      string st_prgstr = "";
      DataTable tb_Func = new DataTable();
      DataRow[] dr_Funcs;
      DataRow dr_Func;
      //
      st_prgstr = chkPrgFuncButtons(st_object_func);
      tb_Func = chkPrgFuncSet(tb_Func, st_object_func);
      st_accstr = chkAccessFuncButtons(UserGkey, st_object_func);
      //
      if (st_edittype.ToLower() == "mod")
      {
        for (int vi = 1; vi <= 30; vi++)
        {
          st_btn_name = st_ctl_name + "bt_" + strzeroi(vi, 2);
          Infragistics.WebUI.WebDataInput.WebImageButton bt_set = pg_This.FindControl(st_btn_name) as Infragistics.WebUI.WebDataInput.WebImageButton;
          try
          {
            bt_set.Visible = false;
            bt_set.ClientSideEvents.Click = "";
            bt_set = SetWebImageButtonDetail(bt_set);
          }
          catch
          {
          }
        }
      }
      else
      {
        for (int vi = 1; vi <= 30; vi++)
        {
          st_btn_name = st_ctl_name + "bt_" + strzeroi(vi, 2);
          Infragistics.WebUI.WebDataInput.WebImageButton bt_set = pg_This.FindControl(st_btn_name) as Infragistics.WebUI.WebDataInput.WebImageButton;
          try
          {
            if ((st_prgstr.Substring(vi, 1) == "1") && (UserGkey == "Admin"))
            {
              bt_set.Visible = true;
              dr_Funcs = tb_Func.Select("buttonno=" + vi.ToString());
              bt_set.ClientSideEvents.Click = "ClientCommand";
              if (dr_Funcs.Length == 1)
              {
                dr_Func = dr_Funcs[0];
                bt_set.Text = DAC.GetStringValue(dr_Func["button_" + st_LangType]);
                bt_set.ToolTip = DAC.GetStringValue(dr_Func["tip_" + st_LangType]);
              }
            }
            else if ((st_prgstr.Substring(vi, 1) == "1") && (st_accstr.Substring(vi, 1) == "1"))
            {
              bt_set.Visible = true;
              dr_Funcs = tb_Func.Select("buttonno=" + vi.ToString());
              if (dr_Funcs.Length == 1)
              {
                dr_Func = dr_Funcs[0];
                bt_set.Text = DAC.GetStringValue(dr_Func["button_" + st_LangType]);
                bt_set.ToolTip = DAC.GetStringValue(dr_Func["tip_" + st_LangType]);
              }
            }
            else
            {
              bt_set.Visible = false;
            }
            bt_set = SetWebImageButtonDetail(bt_set);
          }
          catch
          {
          }
        }
        //
      }
      tb_Func.Dispose();
    }

    public void SetWebImageButton(Page pg_This, string st_btn_name, string st_LangType, string st_ctl_name, string st_textvalue, bool bl_visible)
    {
      string st_find_name;
      st_find_name = st_ctl_name + st_btn_name;
      Infragistics.WebUI.WebDataInput.WebImageButton bt_WebImageButton = pg_This.FindControl(st_find_name) as Infragistics.WebUI.WebDataInput.WebImageButton;
      try
      {
        bt_WebImageButton = SetWebImageButtonDetail(bt_WebImageButton);
        bt_WebImageButton.Text = st_textvalue;
        bt_WebImageButton.Visible = bl_visible;
      }
      catch { };
    }

    public Infragistics.WebUI.WebDataInput.WebImageButton SetWebImageButtonDetail(Infragistics.WebUI.WebDataInput.WebImageButton bt_WebImageButton)
    {
      bt_WebImageButton.FocusAppearance.InnerBorder.ColorTop = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.FocusAppearance.InnerBorder.ColorLeft = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.FocusAppearance.InnerBorder.ColorBottom = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.FocusAppearance.InnerBorder.ColorRight = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.FocusAppearance.InnerBorder.WidthLeft = Unit.Pixel(1);
      bt_WebImageButton.FocusAppearance.InnerBorder.WidthTop = Unit.Pixel(1);
      bt_WebImageButton.FocusAppearance.InnerBorder.WidthRight = Unit.Pixel(1);
      bt_WebImageButton.FocusAppearance.InnerBorder.WidthRight = Unit.Pixel(1);
      bt_WebImageButton.FocusAppearance.InnerBorder.StyleBottom = BorderStyle.Solid;
      bt_WebImageButton.FocusAppearance.InnerBorder.StyleTop = BorderStyle.Solid;
      bt_WebImageButton.FocusAppearance.InnerBorder.StyleRight = BorderStyle.Solid;
      bt_WebImageButton.FocusAppearance.InnerBorder.StyleLeft = BorderStyle.Solid;
      //
      bt_WebImageButton.PressedAppearance.InnerBorder.ColorTop = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.PressedAppearance.InnerBorder.ColorLeft = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.PressedAppearance.InnerBorder.ColorBottom = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.PressedAppearance.InnerBorder.ColorRight = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.PressedAppearance.InnerBorder.WidthLeft = Unit.Pixel(1);
      bt_WebImageButton.PressedAppearance.InnerBorder.WidthTop = Unit.Pixel(1);
      bt_WebImageButton.PressedAppearance.InnerBorder.WidthRight = Unit.Pixel(1);
      bt_WebImageButton.PressedAppearance.InnerBorder.WidthRight = Unit.Pixel(1);
      bt_WebImageButton.PressedAppearance.InnerBorder.StyleBottom = BorderStyle.Solid;
      bt_WebImageButton.PressedAppearance.InnerBorder.StyleTop = BorderStyle.Solid;
      bt_WebImageButton.PressedAppearance.InnerBorder.StyleRight = BorderStyle.Solid;
      bt_WebImageButton.PressedAppearance.InnerBorder.StyleLeft = BorderStyle.Solid;
      bt_WebImageButton.PressedAppearance.ContentShift = Infragistics.WebUI.WebControls.ButtonContentShiftType.DownRight;

      //
      bt_WebImageButton.HoverAppearance.InnerBorder.ColorTop = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.HoverAppearance.InnerBorder.ColorLeft = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.HoverAppearance.InnerBorder.ColorBottom = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.HoverAppearance.InnerBorder.ColorRight = Color.FromArgb(0, 37, 108, 180);
      bt_WebImageButton.HoverAppearance.InnerBorder.WidthLeft = Unit.Pixel(1);
      bt_WebImageButton.HoverAppearance.InnerBorder.WidthTop = Unit.Pixel(1);
      bt_WebImageButton.HoverAppearance.InnerBorder.WidthRight = Unit.Pixel(1);
      bt_WebImageButton.HoverAppearance.InnerBorder.WidthRight = Unit.Pixel(1);
      bt_WebImageButton.HoverAppearance.InnerBorder.StyleBottom = BorderStyle.Solid;
      bt_WebImageButton.HoverAppearance.InnerBorder.StyleTop = BorderStyle.Solid;
      bt_WebImageButton.HoverAppearance.InnerBorder.StyleRight = BorderStyle.Solid;
      bt_WebImageButton.HoverAppearance.InnerBorder.StyleLeft = BorderStyle.Solid;
      bt_WebImageButton.HoverAppearance.ContentShift = Infragistics.WebUI.WebControls.ButtonContentShiftType.UpLeft;

      return (bt_WebImageButton);
    }


    /// <summary>
    /// 取 sys_Menu 設定資料
    /// </summary>
    public string Get_FormVariable(string st_object_func, string st_Filed)
    {
      DataTable tb_menu;
      DataRow mRow;
      OleDbCommand cmd = DAC.NewCommand();
      string st_ret = "";
      //
      cmd.CommandText = "SELECT " + st_Filed + "  from sys_menu where obj_name=? ";
      AddParam(cmd, "obj_name", st_object_func);
      tb_menu = Select(cmd);
      if (tb_menu.Rows.Count > 0)
      {
        mRow = tb_menu.Rows[0];
        st_ret = DAC.GetStringValue(mRow[st_Filed]);
      }
      tb_menu.Dispose();
      return st_ret;
    }

    /// <summary>
    /// 多國語 取 sys_Menu 的Title資料
    /// sFN.SetFormTitle(st_object_func,PublicVariable.LangType);
    /// </summary>
    public string SetFormTitle(string st_object_func, string st_LangType)
    {
      string st_Filed = "chinesebigname";
      DataTable tb_menu;
      DataRow mRow;
      OleDbCommand cmd = DAC.NewCommand();
      string st_ret = "";
      //
      if (st_LangType == "t") { st_Filed = "chinesebigname"; }
      else if (st_LangType == "c") { st_Filed = "chinesesimpname"; }
      else if (st_LangType == "e") { st_Filed = "englishname"; }
      else if (st_LangType == "v") { st_Filed = "vietnamname"; };
      cmd.CommandText = "SELECT " + st_Filed + "  from sys_menu where obj_name=? ";
      AddParam(cmd, "obj_name", st_object_func);
      tb_menu = Select(cmd);
      if (tb_menu.Rows.Count > 0)
      {
        mRow = tb_menu.Rows[0];
        st_ret = DAC.GetStringValue(mRow[st_Filed]);
      }
      tb_menu.Dispose();
      return st_ret;
    }


    /// <summary>
    /// 取得object的所有sys_dbset的設定
    /// </summary>
    public DataTable get_dbApx(DataTable tb_Apx, string st_ver, string st_apx, string st_tbl)
    {
      tb_Apx.Clear();

      OleDbCommand cmd = NewCommand();
      cmd.CommandText = @"select 'dbset' as flag,b.DBFLD as  sys_dbset_DBFLD ,b.DBCNA as  sys_dbset_DBCNA ,b.DBTNA as  sys_dbset_DBTNA ,b.DBENA as  sys_dbset_DBENA ,b.DBCNA as  sys_dbset_DBVNA ,'' as  sys_dbset_DBDEF ,'0' as  sys_dbset_DBSOR ,b.DBAPX as  sys_dbset_DBAPX ,b.DBTBL as  sys_dbset_DBTBL ,b.DBVER as  sys_dbset_DBVER , 
                                 b.DBJIA as  sys_dbset_DBJIA,
                                 space(40) as fieldKey 
                                 from dbset b  with (nolock)  
                          where 1=1  AND b.DBVER=? AND b.DBAPX=? and b.DBTBL=?
                                and b.DBVER+b.DBAPX+b.DBTBL+b.DBFLD NOT in (select x.DBVER+x.DBAPX+x.DBTBL+x.DBFLD from sys_dbset x )
                          union
                          select 'sys_dbset' as flag,b.DBFLD as  sys_dbset_DBFLD ,b.DBCNA as  sys_dbset_DBCNA ,b.DBTNA as  sys_dbset_DBTNA ,b.DBENA as  sys_dbset_DBENA ,b.DBCNA as  sys_dbset_DBVNA ,b.DBDEF as  sys_dbset_DBDEF ,b.DBSOR as  sys_dbset_DBSOR ,b.DBAPX as  sys_dbset_DBAPX ,b.DBTBL as  sys_dbset_DBTBL ,b.DBVER as  sys_dbset_DBVER ,
                                o.DBJIA as  sys_dbset_DBJIA,
                                space(40) as fieldKey 
                          from sys_dbset b  with (nolock)  
                          left outer join dbset o on b.DBAPX=o.DBAPX AND b.DBTBL=o.DBTBL AND b.DBFLD=o.DBFLD
                          where 1=1  AND b.DBVER=? AND b.DBAPX=? and b.DBTBL=? 
                          union 
                          select 'dbset1' as flag,b.DBFLD as  sys_dbset_DBFLD ,b.DBCNA as  sys_dbset_DBCNA ,b.DBTNA as  sys_dbset_DBTNA ,b.DBENA as  sys_dbset_DBENA ,b.DBCNA as  sys_dbset_DBVNA ,'' as  sys_dbset_DBDEF ,'0' as  sys_dbset_DBSOR ,b.DBAPX as  sys_dbset_DBAPX ,b.DBTBL as  sys_dbset_DBTBL ,b.DBVER as  sys_dbset_DBVER , 
                                 b.DBJIA as  sys_dbset_DBJIA,
                                 space(40) as fieldKey 
                                 from dbset b  with (nolock)  
                          where 1=1  AND b.DBVER=? AND b.DBAPX=? and b.DBTBL=?
                                and b.DBVER+b.DBAPX+b.DBTBL+b.DBFLD NOT in (select x.DBVER+x.DBAPX+x.DBTBL+x.DBFLD from sys_dbset x )
                          union
                          select 'sys_dbset1' as flag,b.DBFLD as  sys_dbset_DBFLD ,b.DBCNA as  sys_dbset_DBCNA ,b.DBTNA as  sys_dbset_DBTNA ,b.DBENA as  sys_dbset_DBENA ,b.DBCNA as  sys_dbset_DBVNA ,b.DBDEF as  sys_dbset_DBDEF ,b.DBSOR as  sys_dbset_DBSOR ,b.DBAPX as  sys_dbset_DBAPX ,b.DBTBL as  sys_dbset_DBTBL ,b.DBVER as  sys_dbset_DBVER ,
                                o.DBJIA as  sys_dbset_DBJIA,
                                space(40) as fieldKey 
                          from sys_dbset b  with (nolock)  
                          left outer join dbset o on b.DBAPX=o.DBAPX AND b.DBTBL=o.DBTBL AND b.DBFLD=o.DBFLD
                          where 1=1  AND b.DBVER=? AND b.DBAPX=? and b.DBTBL=? ";

      AddParam(cmd, "DBVER01", st_ver);
      AddParam(cmd, "DBAPX01", st_apx);
      AddParam(cmd, "DBTBL01", st_tbl);
      AddParam(cmd, "DBVER02", st_ver);
      AddParam(cmd, "DBAPX02", st_apx);
      AddParam(cmd, "DBTBL02", st_tbl);
      AddParam(cmd, "DBVER03", st_ver);
      AddParam(cmd, "DBAPX03", st_apx);
      AddParam(cmd, "DBTBL03", st_tbl);
      AddParam(cmd, "DBVER04", st_ver);
      AddParam(cmd, "DBAPX04", st_apx);
      AddParam(cmd, "DBTBL04", st_tbl);

      tb_Apx = Select(cmd);
      //
      string st_flag = "", st_field = "", st_dbjia = "", st_fieldKey = "";
      DataRow dr_Func;
      for (var vr = 0; vr < tb_Apx.Rows.Count; vr++)
      {
        dr_Func = tb_Apx.Rows[vr];
        st_flag = DAC.GetStringValue(dr_Func["flag"]);
        st_field = DAC.GetStringValue(dr_Func["sys_dbset_DBFLD"]);
        st_dbjia = DAC.GetStringValue(dr_Func["sys_dbset_DBJIA"]);
        if ((st_flag == "dbset1") || (st_flag == "sys_dbset1"))
        {
          st_fieldKey = st_field;
        }
        else
        {
          if (st_dbjia.ToLower() == "a")
          {
            st_fieldKey = st_tbl + "_" + st_field;
          }
          else
          {
            st_fieldKey = st_field;
          }
        }
        //
        dr_Func.BeginEdit();
        dr_Func["fieldKey"] = st_fieldKey;
        dr_Func.EndEdit();
      }
      cmd.Dispose();
      return tb_Apx;
    }


    /// <summary>
    /// 取得object的所有sys_dbset的設定
    /// </summary>
    public DataTable get_dbSet(DataTable tb_Func, string st_ver, string st_apx, string st_tbl)
    {
      tb_Func.Clear();

      OleDbCommand cmd = NewCommand();
      cmd.CommandText = @"select 'dbset' as flag,b.DBFLD as  sys_dbset_DBFLD ,b.DBCNA as  sys_dbset_DBCNA ,b.DBTNA as  sys_dbset_DBTNA ,b.DBENA as  sys_dbset_DBENA ,b.DBCNA as  sys_dbset_DBVNA ,'' as  sys_dbset_DBDEF ,'0' as  sys_dbset_DBSOR ,b.DBAPX as  sys_dbset_DBAPX ,b.DBTBL as  sys_dbset_DBTBL ,b.DBVER as  sys_dbset_DBVER , 
                                 b.DBJIA as  sys_dbset_DBJIA
                                 from dbset b  with (nolock)  
                          where 1=1  AND b.DBVER=? AND b.DBAPX=? and b.DBTBL=?
                                and b.DBVER+b.DBAPX+b.DBTBL+b.DBFLD NOT in (select x.DBVER+x.DBAPX+x.DBTBL+x.DBFLD from sys_dbset x )
                          union
                          select 'sys_dbset' as flag,b.DBFLD as  sys_dbset_DBFLD ,b.DBCNA as  sys_dbset_DBCNA ,b.DBTNA as  sys_dbset_DBTNA ,b.DBENA as  sys_dbset_DBENA ,b.DBCNA as  sys_dbset_DBVNA ,b.DBDEF as  sys_dbset_DBDEF ,b.DBSOR as  sys_dbset_DBSOR ,b.DBAPX as  sys_dbset_DBAPX ,b.DBTBL as  sys_dbset_DBTBL ,b.DBVER as  sys_dbset_DBVER ,
                                o.DBJIA as  sys_dbset_DBJIA
                          from sys_dbset b  with (nolock)  
                          left outer join dbset o on b.DBAPX=o.DBAPX AND b.DBTBL=o.DBTBL AND b.DBFLD=o.DBFLD
                          where 1=1  AND b.DBVER=? AND b.DBAPX=? and b.DBTBL=? ";

      AddParam(cmd, "DBVER01", st_ver);
      AddParam(cmd, "DBAPX01", st_apx);
      AddParam(cmd, "DBTBL01", st_tbl);
      AddParam(cmd, "DBVER02", st_ver);
      AddParam(cmd, "DBAPX02", st_apx);
      AddParam(cmd, "DBTBL02", st_tbl);
      tb_Func = Select(cmd);
      cmd.Dispose();
      return tb_Func;
    }

    /// <summary>
    /// 多國語  sFN.SetWebDataGridHeadText(this, PublicVariable.LangType,"ctl00$ContentPlaceHolder1$","YN01","UNrate","rate");
    /// </summary>
    public void SetWebDataGridHeadText(ref Infragistics.Web.UI.GridControls.WebDataGrid WebGrid, string st_LangType, string st_ver, string st_apx, string st_tbl)
    {
      string st_column_key = "";
      DataTable tb_Func = new DataTable();
      DataRow[] dr_Funcs;
      DataRow dr_Func;
      //
      tb_Func = get_dbSet(tb_Func, st_ver, st_apx, st_tbl);
      for (int vi = 0; vi <= WebGrid.Columns.Count - 1; vi++)
      {
        st_column_key = WebGrid.Columns[vi].Key;
        dr_Funcs = tb_Func.Select(" sys_dbset_DBFLD like '%" + st_column_key + "%' ");
        dr_Funcs = tb_Func.Select("'" + st_column_key + "' like '%'+sys_dbset_DBFLD+'%' ");
        if (dr_Funcs.Length > 0)
        {
          dr_Func = dr_Funcs[0];
          WebGrid.Columns[vi].Header.Text = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
        }
      }
      tb_Func.Dispose();
    }


    public void SetFormControlsText(Control ctrls, string st_LangType, string st_ver, string st_apx, string st_tbl)
    {
      DataTable tb_dbSet = new DataTable();
      tb_dbSet = get_dbApx(tb_dbSet, st_ver, st_apx, st_tbl);
      SetFormControlText(ctrls, tb_dbSet, st_LangType, st_ver, st_apx, st_tbl);
      tb_dbSet.Dispose();
    }

    public void SetFormControlText(Control ctrls, DataTable tb_dbApx, string st_LangType, string st_ver, string st_apx, string st_tbl)
    {
      bool getText = false;
      string st_id = "", st_key = "", st_val = "";
      //
      if ((ctrls is Label) || (ctrls is CheckBox))
      {
        try
        {
          st_id = ctrls.ID;
          if (st_id == null) st_id = "*";
        }
        catch
        {
          st_id = "*";
        }
        if (st_id.Length > 3)
        {
          //去掉 lb_  
          if ((st_id.Substring(0, 3) == "lb_") || (st_id.Substring(0, 3) == "ck_"))
          {
            st_key = st_id.Substring(3);  //去掉 lb_
            if (st_key.Length > 3)
            {
              st_val = SetFormControlGetText(ref getText, tb_dbApx, st_LangType, st_id);
              if (getText)
              {
                if (st_id.Substring(0, 3) == "lb_")
                {
                  ((Label)ctrls).Text = st_val;
                }
                else if (st_id.Substring(0, 3) == "ck_")
                {
                  ((CheckBox)ctrls).Text = st_val;
                }
              }
            }  //st_key.Length>3
          } //st_id.Substring(0,3)=="lb_" or =="ck_"
        } //st_id.Length > 3
      } //ctrls is Label or  CheckBox
      else if (ctrls is GridView)
      {
        for (int vt = 0; vt <= ((GridView)ctrls).Columns.Count - 1; vt++)
        {
          getText = false;
          st_id = ((GridView)ctrls).Columns[vt].HeaderText;
          if (st_id.Length > 3)
          {
            if (st_id.Substring(0, 3).ToLower() == "cho")
            {
              switch (st_LangType.ToUpper())
              {
                case "C": st_val = "选"; break;
                case "T": st_val = "選"; break;
                case "E": st_val = "chk"; break;
                default: st_val = "chk"; break;
              }
              getText = true;
            }
            else
            {
              st_id = "lb_" + st_id;
              st_val = SetFormControlGetText(ref getText, tb_dbApx, st_LangType, st_id);
            }
          }
          if (getText)
          {
            ((GridView)ctrls).Columns[vt].HeaderText = st_val;
          }
        }

      }
      else if (ctrls is Infragistics.Web.UI.LayoutControls.WebTab)
      {
        try
        {
          st_id = ctrls.ID;
          if (st_id == null) st_id = "*";
        }
        catch
        {
          st_id = "*";
        }
        if (st_id.Length > 3)
        {
          if ((st_id.Substring(0, 6).ToLower() == "webtab"))
          {
            for (var vt = 0; vt <= ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs.Count - 1; vt++)
            {
              st_key = ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Key;
              if (st_key.Length >= 3)
              {
                if (st_key.ToUpper().Substring(0, 3) == "EDI")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "编辑"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "編輯"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "edit"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "edit"; break;
                  }
                }
                else if (st_key.ToUpper().Substring(0, 3) == "QUE")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "查询"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "查詢"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Query"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Query"; break;
                  }
                }
                else if (st_key.ToUpper().Substring(0, 3) == "GEN")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "一般"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "一般"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "General"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "General"; break;
                  }
                }
                else if (st_key.ToUpper().Substring(0, 3) == "ADV")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "进阶"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "進階"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "General"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "General"; break;
                  }
                }
                else if (st_key.ToUpper().Substring(0, 3) == "DAT")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "資料"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "资料"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Data"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Data"; break;
                  }
                }
                else if (st_key.ToUpper().Substring(0, 4) == "TDOC")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "繁体说明"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "繁體說明"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Traditional Chinese"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Traditional Chinese"; break;
                  }
                }
                else if (st_key.ToUpper().Substring(0, 4) == "CDOC")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "简体说明"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "簡體說明"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Simplified Chinese"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "Simplified Chinese"; break;
                  }
                }
                else if (st_key.ToUpper().Substring(0, 4) == "EDOC")
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "英文说明"; break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "英文說明"; break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "English"; break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = "English"; break;
                  }
                }
                else
                {
                  switch (st_LangType.ToUpper())
                  {
                    case "C": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = lookups("SELECT chinesebigname,chinesesimpname,englishname,vietnamname from sys_menu where obj_name='" + st_key + "'", "chinesesimpname"); break;
                    case "T": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = lookups("SELECT chinesebigname,chinesesimpname,englishname,vietnamname from sys_menu where obj_name='" + st_key + "'", "chinesebigname"); break;
                    case "E": ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = lookups("SELECT chinesebigname,chinesesimpname,englishname,vietnamname from sys_menu where obj_name='" + st_key + "'", "englishname"); break;
                    default: ((Infragistics.Web.UI.LayoutControls.WebTab)ctrls).Tabs[vt].Text = lookups("SELECT chinesebigname,chinesesimpname,englishname,vietnamname from sys_menu where obj_name='" + st_key + "'", "vietnamname"); break;
                  }
                }
              }
            } //for Tabs.Count 
          } //st_id.Substring(0, 6).ToLower() == "webtab")
        } //st_id.Length > 3
      } //ctrls is WebTab
      //
      //
      if (ctrls.HasControls() && (!(ctrls is GridView)))
      {
        foreach (Control con in ctrls.Controls)
        {
          SetFormControlText(con, tb_dbApx, st_LangType, st_ver, st_apx, st_tbl);
        }
      }
    }

    public string SetFormControlGetText(ref bool getText, DataTable tb_dbApx, string st_LangType, string st_id)
    {
      string st_key = st_id.Substring(3);  //去掉 lb_
      string st_keyt = "", st_key1 = "", st_val = "";
      DataRow[] dr_Funcs;
      DataRow dr_Func;
      //
      if (st_key.Length > 3)
      {
        st_keyt = st_key;
        dr_Funcs = tb_dbApx.Select("fieldKey='" + st_keyt + "'");
        if (dr_Funcs.Length>0)
        {
          dr_Func = dr_Funcs[0];
          st_val = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
          getText = true;
        } //dr_Funcs.Length
        if (!getText)
        {
          for (int in_idx = st_key.Length - 1; in_idx >= 3; in_idx--)
          {
            st_keyt = st_key.Substring(0, in_idx);
            dr_Funcs = tb_dbApx.Select("fieldKey='" + st_keyt + "'");
            if (dr_Funcs.Length >0 )
            {
              dr_Func = dr_Funcs[0];
              st_val = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
              getText = true;
              in_idx = 0;
            } //dr_Funcs.Length

          }
        }
        //
        if (!getText)
        {
          if (st_key.IndexOf("_") >= 3)
          {
            st_key1 = st_key.Substring(st_key.IndexOf("_") + 1);
            for (int in_idx = st_key1.Length; in_idx >= 2; in_idx--)
            {
              st_keyt = st_key1.Substring(0, in_idx);
              dr_Funcs = tb_dbApx.Select("sys_dbset_DBFLD='" + st_keyt + "'");
              if (dr_Funcs.Length >0)
              {
                dr_Func = dr_Funcs[0];
                st_val = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
                getText = true;
                in_idx = 0;
              } //dr_Funcs.Length
            }
          }
        }
        //
      }  //st_key.Length>3
      return st_val;
    }


    /// <summary>
    /// 多國語  sFN.SetFormLables(this, PublicVariable.LangType,"ctl00$ContentPlaceHolder1$","YN01","UNrate","rate");
    /// </summary>
    public void SetFormLables(Page pg_This, string st_LangType, string st_ctl_name, string st_ver, string st_apx, string st_tbl)
    {
      string st_lab_name = "";
      string st_sql_field = "";
      string st_field = "";
      string st_dbjia = "";
      DataTable tb_Func = new DataTable();
      DataRow dr_Func;
      //
      tb_Func = get_dbSet(tb_Func, st_ver, st_apx, st_tbl);
      for (int vr = 0; vr < tb_Func.Rows.Count; vr++)
      {
        dr_Func = tb_Func.Rows[vr];
        st_field = DAC.GetStringValue(dr_Func["sys_dbset_DBFLD"]);
        st_dbjia = DAC.GetStringValue(dr_Func["sys_dbset_DBJIA"]);
        if (st_dbjia.ToLower() == "a")
        {
          st_sql_field = st_tbl + "_" + st_field;
        }
        else
        {
          st_sql_field = st_field;
        }
        //
        for (int vi = 0; vi <= 5; vi++)
        {
          if (vi == 0)
          {
            st_lab_name = st_ctl_name + "lb_" + st_sql_field;
          }
          else
          {
            st_lab_name = st_ctl_name + "lb_" + st_sql_field + "_" + strzeroi(vi, 2);
          }
          Label lb_set = pg_This.FindControl(st_lab_name) as Label;
          try
          {
            if (lb_set.Visible)
            {
              lb_set.Text = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
            }
            else
            {
              lb_set.Text = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
            }
          }
          catch
          {
          }
        }  //for 

      }  //rows
      tb_Func.Dispose();
    }
    //
    /// <summary>
    /// 多國語  sFN.SetFormLables(this, PublicVariable.LangType,"ctl00$ContentPlaceHolder1$","YN01","UNrate","rate",lb_lb_rate_PDRAT);
    /// </summary>
    public void SetFormLable(Page pg_This, string st_LangType, string st_ctl_name, string st_ver, string st_apx, string st_tbl, Label lb_lable)
    {
      string st_field = "";
      DataTable tb_Func = new DataTable();
      DataRow dr_Func;
      //
      tb_Func = get_dbSet(tb_Func, st_ver, st_apx, st_tbl);
      for (int vr = 0; vr < tb_Func.Rows.Count; vr++)
      {
        dr_Func = tb_Func.Rows[vr];
        st_field = DAC.GetStringValue(dr_Func["sys_dbset_DBFLD"]);
        if (lb_lable.Text.IndexOf(st_field) >= 0)
        {
          lb_lable.Text = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
          vr = tb_Func.Rows.Count;
        }
      }  //rows
      tb_Func.Dispose();
    }

    public string SetFormLable(Page pg_This, string st_LangType, string st_getfield, string st_ver, string st_apx, string st_tbl)
    {
      string st_field = "";
      string st_ret = "";
      DataTable tb_Func = new DataTable();
      DataRow dr_Func;
      //
      tb_Func = get_dbSet(tb_Func, st_ver, st_apx, st_tbl);
      for (int vr = 0; vr < tb_Func.Rows.Count; vr++)
      {
        dr_Func = tb_Func.Rows[vr];
        st_field = DAC.GetStringValue(dr_Func["sys_dbset_DBFLD"]);
        if (st_field.IndexOf(st_getfield) >= 0)
        {
          st_ret = DAC.GetStringValue(dr_Func["sys_dbset_DB" + st_LangType.ToUpper() + "NA"]);
          vr = tb_Func.Rows.Count;
        }
      }  //rows
      tb_Func.Dispose();
      return st_ret;
    }

    public bool IsAllNumCharUpper(string st_str)
    {
      int in_I = 0;
      string vXR;
      string st_RSTR;
      bool bl_RET = true;
      st_RSTR = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_#*.";
      bl_RET = true;
      for (in_I = 0; in_I < st_str.Length; in_I++)
      {
        vXR = st_str.Substring(in_I, 1);
        if (st_RSTR.IndexOf(vXR) < 0)
        {
          bl_RET = false;
        }
      }
      return bl_RET;
    }

    /// <summary>
    /// st_str 是否全在 st_RSTR 中
    /// </summary>
    /// <param name="st_str">456</param>
    /// <param name="st_RSTR">0123456789.</param>
    /// <returns></returns>
    public bool IsCharSet(string st_str, string st_RSTR)
    {
      int in_I = 0;
      string vXR;
      bool bl_RET = true;
      bl_RET = true;
      for (in_I = 0; in_I < st_str.Length; in_I++)
      {
        vXR = st_str.Substring(in_I, 1);
        if (st_RSTR.IndexOf(vXR) < 0)
        {
          bl_RET = false;
        }
      }
      return bl_RET;
    }

    public bool IsAllNum(string st_str)
    {
      int in_I = 0;
      string vXR;
      string st_RSTR;
      bool bl_RET = true;
      st_RSTR = "0123456789.";
      bl_RET = true;
      for (in_I = 0; in_I < st_str.Length; in_I++)
      {
        vXR = st_str.Substring(in_I, 1);
        if (st_RSTR.IndexOf(vXR) < 0)
        {
          bl_RET = false;
        }
      }
      return bl_RET;
    }

    public bool IsTelNum(string st_str)
    {
      int in_I = 0;
      string vXR;
      string st_RSTR;
      bool bl_RET = true;
      st_RSTR = "0123456789-()#EXT ext.";
      bl_RET = true;
      for (in_I = 0; in_I < st_str.Length; in_I++)
      {
        vXR = st_str.Substring(in_I, 1);
        if (st_RSTR.IndexOf(vXR) < 0)
        {
          bl_RET = false;
        }
      }
      return bl_RET;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="st_string"></param>
    /// <returns></returns>
    public string GetCJ5P(string st_string)
    {
      string vSSTR = "", vSSTV = "", vRET = "";
      int in_len = st_string.Length;
      if (in_len >= 4)
      {
        in_len = 4;
      }
      //
      for (var vi = 0; vi < in_len; vi++)
      {
        vSSTR = st_string.Substring(vi, 1);
        vSSTV = lookups("SELECT BCPHO FROM BCOD WHERE BCCOD='" + st_string.Substring(vi, 1) + "'", "BCPHO");
        if (vSSTV == "")
        {
          vSSTV = vSSTR;
        }
        else
        {
          vSSTV = vSSTV.Substring(0, 1);
        }
        vRET = vRET + vSSTV;
        if (vi >= 4)
        {
          vi = 5;
        }
      }
      vRET = vRET.Trim();
      return vRET;
    }

    /// <summary>
    /// 取得TSTRING的設定
    /// </summary>
    public string TSTRING(string st_lan, string st_id)
    {
      string st_val = "";
      OleDbConnection connr;
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      // 
      connr = NewReaderConnr();
      connr.Open();
      cmdr.Connection = connr;
      cmdr.CommandText = @"select blank,blan" + st_lan + " as blname from tstring where blank=? ";
      AddParam(cmdr, "id", st_id);
      rd = OleDbReader(connr, cmdr);
      try
      {
        if (rd.Read())
        {
          st_val = GetStringValue(rd["blname"]);
        }
      }
      finally
      {
        rd.Close();
        rd.Dispose();
        cmdr.Dispose();
        connr.Close();
        connr.Dispose();
      }
      if (st_val == "")
      {
        st_val = st_id;
      }
      return st_val;
    }

    /// <summary>
    /// 取得TSTRING的設定
    /// </summary>
    public string TSTRING(OleDbConnection connr, OleDbDataReader rd, OleDbCommand cmdr, string st_lan, string st_id)
    {
      string st_val = "";
      cmdr.Connection = connr;
      cmdr.CommandText = @"select blank,blan" + st_lan + " as blname from tstring where blank=? ";
      cmdr.Parameters.Clear();
      AddParam(cmdr, "id", st_id);
      rd = OleDbReader(connr, cmdr);
      try
      {
        if (rd.Read())
        {
          st_val = GetStringValue(rd["blname"]);
        }
      }
      finally
      {
        rd.Close();
      }
      if (st_val == "")
      {
        st_val = st_id;
      }
      return st_val;
    }


    /// <summary>
    /// 取得 SQL:st_sql 中的 st_field 的值
    /// </summary>
    public Int32 lookupint32(string st_sql, string st_field)
    {
      Int32 st_val = 0;
      OleDbConnection connr;
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      // 
      connr = NewReaderConnr();
      connr.Open();
      cmdr.Connection = connr;
      cmdr.CommandText = st_sql;
      rd = cmdr.ExecuteReader();
      try
      {
        if (rd.Read())
        {
          st_val = GetInt32Value(rd[st_field]);
        }
      }
      catch
      {
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return st_val;
    }

    public Decimal lookupDecimal(string st_sql, string st_field)
    {
      Decimal st_val = 0;
      OleDbConnection connr;
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      // 
      connr = NewReaderConnr();
      connr.Open();
      cmdr.Connection = connr;
      cmdr.CommandText = st_sql;
      rd = cmdr.ExecuteReader();
      try
      {
        if (rd.Read())
        {
          st_val = GetDecimalValue(rd[st_field]);
        }
      }
      catch
      {
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return st_val;
    }

    public Double lookupDouble(string st_sql, string st_field)
    {
      Double st_val = 0;
      OleDbConnection connr;
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      // 
      connr = NewReaderConnr();
      connr.Open();
      cmdr.Connection = connr;
      cmdr.CommandText = st_sql;
      rd = cmdr.ExecuteReader();
      try
      {
        if (rd.Read())
        {
          st_val = GetDoubleValue(rd[st_field]);
        }
      }
      catch
      {
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return st_val;
    }


    /// <summary>
    /// 取得 SQL:st_sql 中的 st_field 的值
    /// </summary>
    public string lookups(string st_sql, string st_field)
    {
      string st_val = "";
      OleDbConnection connr;
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      // 
      connr = NewReaderConnr();
      connr.Open();
      cmdr.Connection = connr;
      cmdr.CommandText = st_sql;
      rd = cmdr.ExecuteReader();
      try
      {
        if (rd.Read())
        {
          st_val = GetStringValue(rd[st_field]);
        }
      }
      catch
      {
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return st_val;
    }

    /// <summary>
    /// 取得 SQL:st_sql 中的 st_field 的值
    /// </summary>
    public string lookups(string st_selectsql, OleDbCommand cmd_QueryKey, string st_retfild)
    {
      string st_val = "";
      OleDbConnection connr;
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      // 
      connr = NewReaderConnr();
      connr.Open();
      cmdr.Connection = connr;
      if (cmd_QueryKey.CommandText != "")
      {
        cmdr.CommandText = st_selectsql + " where 1=1 " + cmd_QueryKey.CommandText;
      }
      for (int in_Pi = 0; in_Pi < cmd_QueryKey.Parameters.Count; in_Pi++)
      {
        DoAddParam(cmdr, cmd_QueryKey.Parameters[in_Pi].ParameterName, cmd_QueryKey.Parameters[in_Pi].Value);
      }
      rd = cmdr.ExecuteReader();
      try
      {
        if (rd.Read())
        {
          st_val = GetStringValue(rd[st_retfild]);
        }
      }
      catch
      {
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return st_val;
    }

    public decimal GetRate(string st_Curremcy, DateTime dt_Date)
    {
      decimal de_rate = 0;
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      OleDbConnection connr = NewReaderConnr();
      connr.Open();
      cmdr.CommandText = "select *  from rate with (nolock) where PDMNY=? and PDSDT<=? order by PDSDT ";
      AddParam(cmdr, "PDMNY", st_Curremcy);
      AddParam(cmdr, "PDSDT", dt_Date);
      rd = OleDbReader(connr, cmdr);
      try
      {
        while (rd.Read())
        {
          de_rate = DAC.GetDecimalValue(rd["PDRAT"]);
        }
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return de_rate;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="st_code"></param>
    /// <returns></returns>
    public string GetSs102(string st_code)
    {
      string st_status = "";
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      OleDbConnection connr = NewReaderConnr();
      connr.Open();
      cmdr.CommandText = "select status from ss102 with (nolock) where code=? ";
      AddParam(cmdr, "code", st_code);
      rd = OleDbReader(connr, cmdr);
      try
      {
        if (rd.Read())
        {
          st_status = DAC.GetStringValue(rd["status"]);
        }
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return st_status;
    }

    public Int16 GetSs102Int16(string st_code)
    {
      return DAC.GetInt16Value(GetSs102(st_code));
    }


    /// <summary>
    /// 由gkey,mkey或pkey 取得單號或欄位
    /// </summary>
    /// <param name="st_table">RI3A</param>
    /// <param name="st_renField">RIREN</param>
    /// <param name="st_gkeyField">gkey</param>
    /// <param name="st_keyValue">888828adef2666372778</param>
    /// <returns></returns>
    public string GetRenFromGkey(string st_table, string st_renField, string st_gkeyField, string st_keyValue)
    {
      OleDbCommand cmdr = NewCommand();
      OleDbDataReader rd;
      OleDbConnection connr = NewReaderConnr();
      string st_retRen = "";
      //
      connr.Open();
      cmdr.CommandText = "select " + st_renField + " as retField  from " + st_table + " with (nolock) where " + st_gkeyField + "= ? ";
      AddParam(cmdr, "gkeyField", st_keyValue);
      rd = OleDbReader(connr, cmdr);
      try
      {
        while (rd.Read())
        {
          st_retRen = DAC.GetStringValue(rd["retField"]);
        }
      }
      finally
      {
        rd.Close();
        connr.Close();
        //
        rd.Dispose();
        cmdr.Dispose();
        connr.Dispose();
      }
      return st_retRen;
    }

    public string chr(byte by_char)
    {
      return "" + (char)by_char;
    }

    //public decimal Round(decimal vVAlue, byte vLN)
    //{
    //  double vRET = 0;
    //  vRET = Round(Convert.ToDouble(vVAlue), vLN);
    //  return Convert.ToDecimal(vRET);
    //}

    //public double Round(double vVAlue, byte vLN)
    //{
    //  double vRET = 0;
    //  string vS1 = "";
    //  int vPOS = 0;
    //  //
    //  if (vVAlue < 0)
    //  {
    //    vS1 = (vVAlue * Math.Pow(10, vLN) - 0.5).ToString();
    //    vPOS = vS1.IndexOf(".");
    //    if (vPOS > -1)
    //    {
    //      vS1 = vS1.Substring(0, vPOS);
    //    }
    //    else if (vS1.IndexOf(".") == 0)
    //    {
    //      vS1 = "0";
    //    }
    //    vRET = Math.Floor(Convert.ToDouble(vS1)) / Math.Pow(10, vLN);
    //  }
    //  else if (vVAlue > 0)
    //  {
    //    vS1 = (vVAlue * Math.Pow(10, vLN) + 0.5).ToString();
    //    vPOS = vS1.IndexOf(".");
    //    if (vPOS > -1)
    //    {
    //      vS1 = vS1.Substring(0, vPOS);
    //    }
    //    else if (vS1.IndexOf(".") == 0)
    //    {
    //      vS1 = "0";
    //    }
    //    vRET = Math.Floor(Convert.ToDouble(vS1)) / Math.Pow(10, vLN);
    //  }
    //  else
    //  {
    //    vRET = vVAlue;
    //  }
    //  return vRET;
    //}

    public string GetRenFromDate(DateTime dt_date, string sRenFormat)
    {
      string st_ren = "";
      if (sRenFormat.ToUpper() == "YYYYMMDD")
      {
        st_ren = strzeroi(dt_date.Year, 4) + strzeroi(dt_date.Month, 2) + strzeroi(dt_date.Day, 2);
      }
      else if (sRenFormat.ToUpper() == "YYYYMM")
      {
        st_ren = strzeroi(dt_date.Year, 4) + strzeroi(dt_date.Month, 2);
      }
      else if (sRenFormat.ToUpper() == "YYMM")
      {
        st_ren = strzeroi(dt_date.Year, 4).Substring(2, 2) + strzeroi(dt_date.Month, 2);
      }
      return st_ren;
    }

    public string GetEXCEL_READERValue(OleDbDataReader sEXCEL_READER, int in_Col)
    {
      string st_ret = "";
      try
      {
        st_ret = DAC.GetStringValue(sEXCEL_READER[in_Col]);
      }
      catch
      {
        st_ret = "";
      }
      return st_ret;
    }

    public string GetEXCEL_READERValue(OleDbDataReader sEXCEL_READER, string st_Col)
    {
      string st_ret = "";
      int in_ri = Convert.ToInt32(st_Col[0]) - 65;
      try
      {
        st_ret = DAC.GetStringValue(sEXCEL_READER[in_ri]);
      }
      catch
      {
        st_ret = "";
      }
      return st_ret;
    }

    public string GetWeekCName(int in_week)
    {
      string st_ret = "日一二三四五六";
      if ((in_week >= 0) && (in_week <= 6))
      {
        st_ret = st_ret.Substring(in_week, 1);
      }
      else
      {
        st_ret = "xx";
      }
      return st_ret;

    }
    public string GetWeekCName(string st_week)
    {
      int in_week = 0;
      in_week = Convert.ToInt32(st_week);
      string st_ret = "日一二三四五六";
      if ((in_week >= 0) && (in_week <= 6))
      {
        st_ret = st_ret.Substring(in_week, 1);
      }
      else
      {
        st_ret = st_week;
      }
      return st_ret;

    }

  }
}
