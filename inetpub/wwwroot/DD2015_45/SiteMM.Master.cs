using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using YNetLib_45;

namespace DD2015_45
{
  public partial class SiteMM : System.Web.UI.MasterPage
  {
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      //btnLogout.Visible = false;

      // 查詢字串表，替換頁面字串
      //StringReplacer.TravelControl(PublicVariable.UserLanguage, this.Controls);
    }

    protected override void OnLoad(EventArgs e)
    {
      hh_Company.Value = PublicVariable.CompanyName;
      base.OnLoad(e);

      // 引用 JQuery 及其他需要使用的 JS 檔案
      Page.ClientScript.RegisterClientScriptInclude("JQuery", ResolveClientUrl("~/Scripts/jquery-1.7.1.min.js"));
      Page.ClientScript.RegisterClientScriptInclude("Utility", ResolveClientUrl("~/Utility/Utility.js"));
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (Session[PublicVariable.UserId] != null)
      {
        if (SiteWebMenu.Items.Count == 0)
        {
          if (clsFN.chkLoginState())
          {
            clsFN sFN = new clsFN();
            string st_UserId = "";
            st_UserId = DAC.GetStringValue(Session[PublicVariable.UserId]);
            SiteWebMenu = sFN.SetMenu_WebDataMenu(SiteWebMenu, PublicVariable.LangType, PublicVariable.sys_HttpAppRootPath, st_UserId);
            Session["sys_SiteWebMenu"] = SiteWebMenu;
            //
            sFN.Dispose();
          }
          else
          {
            HttpContext.Current.Response.Redirect("~/login.aspx");
          }
        }
      }
      else
      {
        SiteWebMenu = null;
        Session["sys_SiteWebMenu"] = null;
      }

    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
      FormsAuthentication.SignOut();
      if (Session[PublicVariable.UserId] != null)
      {
        Session.RemoveAll();
        Response.Redirect("~/Login.aspx");
      }
    }

    public virtual string FunctionName
    {
      get
      {
        return lbFunctionName.Text;
      }
      set
      {
        if (value.Length > 0)
        {
          lbFunctionName.Text = StringTable.GetString(value);
          lbFunctionName.Visible = true;
        }
        else
        {
          lbFunctionName.Visible = true;
        }
      }
    }

    public string GetImageUrl(string fileName)
    {
      return ResolveClientUrl("~/Picture/" + Page.Theme + "/" + fileName);
    }
  }
}