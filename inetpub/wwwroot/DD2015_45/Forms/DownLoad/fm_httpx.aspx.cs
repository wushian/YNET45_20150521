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
using System.IO;
using YNetLib_45;

namespace DD2015_45.Forms.Download
{
  public partial class fm_httpx : FormBase
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      DAC http_DAO = new DAC();
      HyFile1.Text = DAC.GetStringValue(Session["fm_httpx_HyFile1_Text"]);
      HyFile1.NavigateUrl = sys_HttpAppRootPath + "/ReportData/" + DAC.GetStringValue(Session["fm_httpx_HyFile1_url"]);
      //
      if (DAC.GetStringValue(Session["fm_httpx_HyFile2_url"]) != "")
      {
        HyFile2.Visible = true;
        HyFile2.Text = DAC.GetStringValue(Session["fm_httpx_HyFile2_Text"]);
        //  HyFile2.NavigateUrl = "XML\" + Session("UNEXCEL_XML_EXPORT_HyFile2").ToString
        HyFile2.NavigateUrl = sys_HttpAppRootPath + "/ReportData/" + DAC.GetStringValue(Session["fm_httpx_HyFile2_url"]);
      }
      else
      {
        HyFile2.Visible = false;
      }
      //
      if (DAC.GetStringValue(Session["fm_httpx_HyFile3_url"]) != "")
      {
        HyFile3.Visible = true;
        HyFile3.Text = DAC.GetStringValue(Session["fm_httpx_HyFile3_Text"]);
        HyFile3.NavigateUrl = sys_HttpAppRootPath + "/ReportData/" + DAC.GetStringValue(Session["fm_httpx_HyFile3_url"]);
      }
      else
      {
        HyFile3.Visible = false;
      }
      //
      if (DAC.GetStringValue(Session["fm_httpx_HyFile4_url"]) != "")
      {
        HyFile4.Visible = true;
        HyFile4.Text = DAC.GetStringValue(Session["fm_httpx_HyFile4_Text"]);
        HyFile4.NavigateUrl = sys_HttpAppRootPath + "/ReportData/" + DAC.GetStringValue(Session["fm_httpx_HyFile4_url"]);
      }
      else
      {
        HyFile4.Visible = false;
      }
    }

    protected void bt_QUT_Click(object sender, EventArgs e)
    {
      if (DAC.GetStringValue(Session["fm_httpx_exit"]) != "")
      {
        Session["fm_httpx_HyFile1_Text"] = "";
        Session["fm_httpx_HyFile1_url"] = "";
        Session["fm_httpx_HyFile2_Text"] = "";
        Session["fm_httpx_HyFile2_url"] = "";
        Session["fm_httpx_HyFile3_Text"] = "";
        Session["fm_httpx_HyFile3_url"] = "";
        Session["fm_httpx_HyFile4_Text"] = "";
        Session["fm_httpx_HyFile4_url"] = "";
        Response.Redirect(DAC.GetStringValue(Session["fm_httpx_exit"]));
      }
      else
      {
        Response.Redirect("~/Master/Default/MainForm.aspx");
      }
    }


  }
}