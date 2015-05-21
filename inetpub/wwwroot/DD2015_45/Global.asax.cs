using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using DD2015_45;
using System.Web.SessionState;
using System.Configuration;
using  YNetLib_45;

namespace DD2015_45
{
  public class Global : HttpApplication
  {
    protected void Application_Start(object sender, EventArgs e)
    {
      if (ConfigurationManager.AppSettings["ConnectionString"] != null)
        PublicVariable.ConnectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["ConnectionString"]].ConnectionString;
      else
        PublicVariable.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

      //DAC.ConnectionType = DAC.connTypeMSSQL;
      DAC.ConnectionType = DAC.connTypeOLEDB;
      DAC.ConnectionString = PublicVariable.ConnectionString;

      if (ConfigurationManager.AppSettings["UserLanguage"] != null)
        PublicVariable.UserLanguage = ConfigurationManager.AppSettings["UserLanguage"];

      if (ConfigurationManager.AppSettings["LangType"] != null)
        PublicVariable.LangType = ConfigurationManager.AppSettings["LangType"];
      if (PublicVariable.LangType.ToLower() == "t")
      {
        PublicVariable.st_find = "F查詢";
        PublicVariable.st_save = "S存檔";
        PublicVariable.st_quit = "Q離開";
        PublicVariable.st_cancel = "N上一步";
        PublicVariable.st_choose = "選";
      }
      else if (PublicVariable.LangType.ToLower() == "c")
      {
        PublicVariable.st_find = "F查询";
        PublicVariable.st_save = "S存档";
        PublicVariable.st_quit = "Q离开";
        PublicVariable.st_cancel = "N上一步";
        PublicVariable.st_choose = "选";
      }
      else if (PublicVariable.LangType.ToLower() == "e")
      {
        PublicVariable.st_find = "Find";
        PublicVariable.st_save = "Save";
        PublicVariable.st_quit = "Quit";
        PublicVariable.st_cancel = "Cancel";
        PublicVariable.st_choose = "　";
      }
      else
      {
        PublicVariable.st_find = "Find";
        PublicVariable.st_save = "Save";
        PublicVariable.st_quit = "Quit";
        PublicVariable.st_cancel = "Cancel";
        PublicVariable.st_choose = "　";
      }
      //
      if (ConfigurationManager.AppSettings["CompanyName"] != null)
        PublicVariable.CompanyName = ConfigurationManager.AppSettings["CompanyName"];

      if (ConfigurationManager.AppSettings["CopyrightText"] != null)
        PublicVariable.CopyrightText = ConfigurationManager.AppSettings["CopyrightText"];

      if (ConfigurationManager.AppSettings["Theme"] != null)
        PublicVariable.Theme = ConfigurationManager.AppSettings["Theme"];

      if (ConfigurationManager.AppSettings["sys_DateFormat"] != null)
        PublicVariable.sys_DateFormat = ConfigurationManager.AppSettings["sys_DateFormat"];

      if (ConfigurationManager.AppSettings["sys_DateTimeFormat"] != null)
        PublicVariable.sys_DateTimeFormat = ConfigurationManager.AppSettings["sys_DateTimeFormat"];

      if (ConfigurationManager.AppSettings["sys_DocFilePath"] != null)
        PublicVariable.sys_DocFilePath = ConfigurationManager.AppSettings["sys_DocFilePath"];

      if (ConfigurationManager.AppSettings["sys_HttpFilePath"] != null)
        PublicVariable.sys_HttpFilePath = ConfigurationManager.AppSettings["sys_HttpFilePath"];

      if (ConfigurationManager.AppSettings["sys_HttpAppRootPath"] != null)
        PublicVariable.sys_HttpAppRootPath = ConfigurationManager.AppSettings["sys_HttpAppRootPath"];

      if (ConfigurationManager.AppSettings["ApVer"] != null)
        PublicVariable.ApVer = ConfigurationManager.AppSettings["ApVer"];

      if (ConfigurationManager.AppSettings["sys_mailhost"] != null)
        PublicVariable.sys_mailhost = ConfigurationManager.AppSettings["sys_mailhost"];

      if (ConfigurationManager.AppSettings["sys_mailport"] != null)
        PublicVariable.sys_mailport = ConfigurationManager.AppSettings["sys_mailport"];

      if (ConfigurationManager.AppSettings["sys_mailaddress"] != null)
        PublicVariable.sys_mailaddress = ConfigurationManager.AppSettings["sys_mailaddress"];

      if (ConfigurationManager.AppSettings["sys_mailaddress"] != null)
        PublicVariable.sys_mailuser = ConfigurationManager.AppSettings["sys_mailuser"];

      if (ConfigurationManager.AppSettings["sys_mailpwd"] != null)
        PublicVariable.sys_mailpwd = ConfigurationManager.AppSettings["sys_mailpwd"];

      // 啟動檔案定時清除
      // 要停止檔案定時清除，必須停止網站應用程式，或是設定 Enabled = false
      WDeleteFileService dfs = WDeleteFileService.GetInstance();
      if (!dfs.Enabled)
      {
        dfs.DeletePatterns.Clear();
        dfs.DeletePatterns.Add(Server.MapPath("~/ReportData/") + "*.mdb");
        dfs.DeletePatterns.Add(Server.MapPath("~/ReportData/") + "*.htm");
        dfs.Interval = 60;           // 每幾秒清除一次檔案
        dfs.FileAgeToDelete = 600;    // 要清除最後寫入時間是幾秒之前的檔案
        dfs.Enabled = true;
      }

      // 啟動郵件傳送服務
      // 郵件伺服器相關設定，在 Web.Config 中的 System.Net 節
      WMailSender mailSender = WMailSender.GetInstance();
      if (!mailSender.Enabled)
      {
        mailSender.Interval = 10;
        mailSender.Enabled = true;
      }
    }

    protected void Session_Start(object sender, EventArgs e)
    {

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {

    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {

    }

    protected void Application_Error(object sender, EventArgs e)
    {

    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {
    }

    public static string FormatDateTime(object dateTime, string format)
    {
      DateTime aDateTime;
      try
      {
        aDateTime = Convert.ToDateTime(dateTime);
      }
      catch
      {
        aDateTime = new DateTime();
      }

      if (aDateTime.Ticks == 0)
        return "";
      else
        return aDateTime.ToString(format);
    }

    public static string FormatDateTime(object dateTime)
    {
      return FormatDateTime(dateTime, "yyyy-MM-dd HH:mm:ss");
    }

    public static string FormatDate(object dateTime)
    {
      return FormatDateTime(dateTime, "yyyy-MM-dd");
    }

    public static string FormatTime(object dateTime)
    {
      return FormatDateTime(dateTime, "HH:mm:ss");
    }
  }
}
