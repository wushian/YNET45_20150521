using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;
using YNetLib_45;

namespace DD2015_45.Forms.bas
{
  public partial class fm_pds : FormBase
  {
    string st_func = "";
    string st_aspx = "";
    protected void Page_Load(object sender, EventArgs e)
    {
      st_func = DAC.GetStringValue(Request["func"]);
      if (st_func == "UNpdbpdp1")
      {
        Session["pdx_title"] = "分類一";            //titel名稱
        Session["pdx_object_func"] = "UNpdbpdp1";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbpdp1";          //與apx 相關
        Session["pdx_dd_table"] = "pdbpdp1";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbpdp2")
      {
        Session["pdx_title"] = "分類二";            //titel名稱
        Session["pdx_object_func"] = "UNpdbpdp2";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbpdp2";          //與apx 相關
        Session["pdx_dd_table"] = "pdbpdp2";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbpdp3")
      {
        Session["pdx_title"] = "分類三";            //titel名稱
        Session["pdx_object_func"] = "UNpdbpdp3";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbpdp3";          //與apx 相關
        Session["pdx_dd_table"] = "pdbpdp3";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbpdp4")
      {
        Session["pdx_title"] = "分類四";            //titel名稱
        Session["pdx_object_func"] = "UNpdbpdp4";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbpdp4";          //與apx 相關
        Session["pdx_dd_table"] = "pdbpdp4";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbplab")
      {
        Session["pdx_title"] = "商品品牌";            //titel名稱
        Session["pdx_object_func"] = "UNpdbplab";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbplab";          //與apx 相關
        Session["pdx_dd_table"] = "pdbplab";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbpuni")
      {
        Session["pdx_title"] = "單位設定";            //titel名稱
        Session["pdx_object_func"] = "UNpdbpuni";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbpuni";          //與apx 相關
        Session["pdx_dd_table"] = "pdbpuni";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbpmdc")
      {
        Session["pdx_title"] = "產地設定";            //titel名稱
        Session["pdx_object_func"] = "UNpdbpmdc";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbpmdc";          //與apx 相關
        Session["pdx_dd_table"] = "pdbpmdc";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbpcl2")
      {
        Session["pdx_title"] = "商品屬性";            //titel名稱
        Session["pdx_object_func"] = "UNpdbpcl2";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbpcl2";          //與apx 相關
        Session["pdx_dd_table"] = "pdbpcl2";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else if (st_func == "UNpdbddex")
      {
        Session["pdx_title"] = "商品屬性";            //titel名稱
        Session["pdx_object_func"] = "UNpdbddex";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdbddex";          //與apx 相關
        Session["pdx_dd_table"] = "pdbddex";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      else
      {
        Session["pdx_title"] = "分類設定";            //titel名稱
        Session["pdx_object_func"] = "UNpdx";  //與權限相關
        Session["pdx_dd_apx"] = "UNpdx";          //與apx 相關
        Session["pdx_dd_table"] = "pdx";          //與table 相關 
        Session["pdx_ren_head"] = "";               //與單號相關 
        Session["pdx_ren_yymmtext"] = "";           //與單號相關 
        Session["pdx_ren_cls"] = "";              //與單號cls相關 
        Session["pdx_ren_cos"] = "1";               //與單號cos相關 
        Session["pdx_ren_len"] = 0;                 //與單號流水號 
        Session["pdx_sub_t01"] = "";              //與單號流水號 
        st_aspx = @"~/Forms/bas/fm_pdx.aspx";
      }
      if (st_aspx != "")
      {
        Response.Redirect(st_aspx);
      }
    }
 
  }
}