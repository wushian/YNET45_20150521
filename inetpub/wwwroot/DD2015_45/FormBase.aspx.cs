using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using YNetLib_45;

namespace DD2015_45
{
  public partial class FormBase : System.Web.UI.Page
  {

    protected OleDbConnection conn;
    protected OleDbCommand CmdQueryS = DAC.NewCommand();
    protected clsFN sFN = new clsFN();

    protected void Page_PreInit(object sender, EventArgs e)
    {
      MaintainScrollPositionOnPostBack = true;
      conn = DAC.NewConnection();
      conn.ConnectionString = PublicVariable.ConnectionString;
      this.Theme = PublicVariable.Theme;

      //if (IsSetMasterPage())
      //  this.MasterPageFile = "~/Master/" + this.Theme + "/SiteM.Master";
    }

    protected virtual bool IsSetMasterPage()
    {
      return true;
    }

    //protected Dictionary<IButtonControl, TabData> tabButtons = new Dictionary<IButtonControl, TabData>();

    /// <summary>
    /// 取得使用者選取的語言
    /// </summary>
    public string UserLanguage
    {
      get
      {
        //return Request.Cookies[PublicVariable.UserLanguage].Value;
        //return "zh-CN";
        return PublicVariable.UserLanguage;
      }
    }

    /// <summary>
    /// LangType "c","t","e","v"
    /// </summary>
    public string LangType
    {
      get
      {
        return PublicVariable.LangType;
      }
    }

    /// <summary>
    /// 顯示子功能名稱
    /// </summary>
    public string FunctionName
    {
      get
      {
        try
        {
          return (Page.Master as SiteMM).FunctionName;
        }
        catch
        {
          return "";
        }
      }
      set
      {
        try
        {
          (Page.Master as SiteMM).FunctionName = value;
        }
        catch
        {

        }
      }
    }

    /// <summary>
    /// 目前登入的使用者編號
    /// </summary>
    public string UserId
    {
      get
      {
        return Session[PublicVariable.UserId] != null ? Session[PublicVariable.UserId].ToString() : "";
      }
    }

    /// <summary>
    /// 目前登入的使用者Gkey
    /// </summary>
    public string UserGkey
    {
      get
      {
        return Session[PublicVariable.UserGkey] != null ? Session[PublicVariable.UserGkey].ToString() : "";
      }
    }

    /// <summary>
    /// 目前登入的使用者es101 no
    /// </summary>
    public string UserNo
    {
      get
      {
        return Session[PublicVariable.UserNo] != null ? Session[PublicVariable.UserNo].ToString() : "";
      }
    }

    /// <summary>
    /// 目前登入的使用者name
    /// </summary>
    public string UserName
    {
      get
      {
        return Session[PublicVariable.UserName] != null ? Session[PublicVariable.UserName].ToString() : "";
      }
    }

    /// <summary>
    /// 目前登入的使用者Cname
    /// </summary>
    public string UserCname
    {
      get
      {
        return Session[PublicVariable.UserCname] != null ? Session[PublicVariable.UserCname].ToString() : "";
      }
    }

    /// <summary>
    /// 目前登入的使用者Ename
    /// </summary>
    public string UserEname
    {
      get
      {
        return Session[PublicVariable.UserEname] != null ? Session[PublicVariable.UserEname].ToString() : "";
      }
    }

    /// <summary>
    /// 目前登入的使用者UserLoginGkey
    /// </summary>
    public string UserLoginGkey
    {
      get
      {
        return Session[PublicVariable.UserLoginGkey] != null ? Session[PublicVariable.UserLoginGkey].ToString() : "";
      }
    }

    /// <summary>
    /// 目前的版本編號
    /// </summary>
    public string ApVer
    {
      get
      {
        return PublicVariable.ApVer;
      }
    }

    /// <summary>
    /// 應用系統日期格式
    /// </summary>
    public string sys_DateFormat
    {
      get
      {
        return PublicVariable.sys_DateFormat;
      }
    }

    /// <summary>
    /// 應用系統日期時間格式
    /// </summary>
    public string sys_DateTimeFormat
    {
      get
      {
        return PublicVariable.sys_DateTimeFormat;
      }
    }

    /// <summary>
    /// 圖形檔 FULL PATH
    /// </summary>
    public string sys_DocFilePath
    {
      get
      {
        //return @"C:\inetpub\wwwroot\ezww03\Doc\";
        return PublicVariable.sys_DocFilePath;
      }
    }

    /// <summary>
    /// 圖形檔 URL ROOT
    /// </summary>
    public string sys_HttpFilePath
    {
      get
      {
        //return @"/ezww03/DOC/";
        return PublicVariable.sys_HttpFilePath;
      }
    }

    /// <summary>
    /// 應用程式HTTP root目錄 \ezww03\
    /// </summary>
    public string sys_HttpAppRootPath
    {
      get
      {
        //return @"/ezww03";
        return PublicVariable.sys_HttpAppRootPath;
      }
    }

    /// <summary>
    /// Report mdb  Data "~/ReportData/"
    /// </summary>
    public string sys_ReportData
    {
      get
      {
        return PublicVariable.sys_ReportData;
      }
    }

    /// <summary>
    /// mdb Full Path   @"C:\inetpub\wwwroot\dd2012\ReportData\";
    /// </summary>
    public string sys_ReportDataFullPath
    {
      get
      {
        return Server.MapPath(PublicVariable.sys_ReportData);
      }
    }

    /// <summary>
    /// Report rpt File "~/Report/"
    /// </summary>
    public string sys_ReportRpt
    {
      get
      {
        return PublicVariable.sys_ReportRpt;
      }
    }

    /// <summary>
    /// rpt Full Path   @"C:\inetpub\wwwroot\dd2012\Report\";
    /// </summary>
    public string sys_ReportRptFullPath
    {
      get
      {
        return Server.MapPath(PublicVariable.sys_ReportRpt);
      }
    }

    /// <summary>
    /// 進貨_金額小數位數
    /// </summary>
    public Int16 cin_IAMT
    {
      get
      {
        return PublicVariable.cin_IAMT;
      }
    }

    /// <summary>
    /// IQTY 進貨_數量小數
    /// </summary>
    public static Int16 cin_IQTY
    {
      get
      {
        return PublicVariable.cin_IQTY;
      }
    }

    /// <summary>
    /// IUPC 進貨_單價小數位數
    /// </summary>
    public static Int16 cin_IUPC
    {
      get
      {
        return PublicVariable.cin_IUPC;
      }
    }

    /// <summary>
    /// 銷售_金額小數位數
    /// </summary>
    public static Int16 cin_SAMT
    {
      get
      {
        return PublicVariable.cin_SAMT;
      }
    }

    /// <summary>
    /// SQTY 銷售_數量小數
    ///</summary>
    public static Int16 cin_SQTY
    {
      get
      {
        return PublicVariable.cin_SQTY;
      }
    }

    /// <summary>
    /// SUPC 銷售_單價小數位數
    /// </summary>
    public static Int16 cin_SUPC
    {
      get
      {
        return PublicVariable.cin_SUPC;
      }
    }

    ///  <summary>
    /// TDE1 商品_定價小數位數
    /// </summary>
    public static Int16 cin_TDE1
    {
      get
      {
        return PublicVariable.cin_TDE1;
      }
    }


    ///  <summary>
    ///  RRAT 外幣匯率_小數位數
    /// </summary>
    public static Int16 cin_RRAT
    {
      get
      {
        return PublicVariable.cin_RRAT;
      }
    }

    ///  <summary>
    ///  RMNY 外幣金額_小數位數
    /// </summary>
    public static Int16 cin_RMNY
    {
      get
      {
        return PublicVariable.cin_RMNY;
      }
    }



    /// <summary>
    /// 圖形檔 extension
    /// </summary>
    public string sys_PicExtension
    {
      get
      {
        return @"JPG,JPEG,TIF,PIC,GIF,BMP,PNG";
      }
    }

    /// <summary>
    /// inf_StyleSetName
    /// </summary>
    public string sys_StyleSetName
    {
      get
      {
        return "Appletini";
      }
    }

    /// <summary>
    /// inf_StyleSetName
    /// </summary>
    public string sys_StyleSetPath
    {
      get
      {
        return "../../../ig_res";
      }
    }



    public override void Dispose()
    {
      if (conn != null)
      {
        if (conn.State != ConnectionState.Closed)
          conn.Close();
        conn.Dispose();
      }
      //CmdQueryS.Dispose();
      sFN.Dispose();
      // GC.SuppressFinalize(this);
    }

    public string GetSessionString(string st_session_name)
    {
      return Session[st_session_name] != null ? DAC.GetStringValue(Session[st_session_name]) : "";
    }

    /// <summary>
    /// 在瀏覽器中使用 alert 顯示訊息
    /// </summary>
    /// <param name="message">要顯示的訊息</param>
    public void ClientShowMessage(string message)
    {
      ClientScript.RegisterStartupScript(Page.GetType(), "client_mesage",
        String.Format("alert(\"{0}\");", StringTable.GetString(message)), true);
    }

    public string GetImageUrl(string fileName)
    {
      return ResolveClientUrl("~/Picture/" + Theme + "/" + fileName);
    }

    public void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.Pager)
      {
        StringReplacer.TravelControl(PublicVariable.UserLanguage, e.Row.Controls);
      }
    }

  }
}