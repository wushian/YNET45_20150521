using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD2015_45
{
  public class PublicVariable
  {
    private static Random r = new Random();

    // SESSION 變數名稱
    public static string UserId = "UserId";
    public static string UserNo = "UserNo";
    public static string UserGkey = "UserGkey";
    public static string UserName = "UserName";
    public static string UserCname = "UserCname";
    public static string UserEname = "UserEname";
    public static string UserLoginGkey = "UserLoginGkey";
    //
    //public static string EmployeeId = "EmployeeId";
    //public static string EmployeeName = "EmployeeName";
    //public static string EmployeeFunction = "EmployeeFunction";
    //public static string PenaltyMessage = "PenaltyMessage";
    //public static string SubShop = "*";
    //public static string SysType = "CUS";
    //public static string ConnCus = "ConnCus";
    //
    public static string st_find = "";
    public static string st_save = "";
    public static string st_quit = "";
    public static string st_cancel = "";
    public static string st_choose = "選";

    #region 系統設定變數，由 Global.asax 讀取 web.config 中的設定

    /// <summary>
    /// 公司名稱
    /// </summary>
    public static string CompanyName = "";

    /// <summary>
    /// 版權宣告文字
    /// </summary>
    public static string CopyrightText = "";

    /// <summary>
    /// 資料庫連接字串
    /// </summary>
    public static string ConnectionString = "";

    /// <summary>
    /// 文件實體root資料夾
    /// </summary>
    public static string sys_DocFilePath = "PicFilePath";

    /// <summary>
    /// 文件HTTP root資料夾
    /// </summary>
    public static string sys_HttpFilePath = "PicFilePath";

    /// <summary>
    /// 應用程式HTTP root目錄 \wwwez03\
    /// </summary>
    public static string sys_HttpAppRootPath = "";

    /// <summary>
    /// UI 語言
    /// </summary>
    public static string UserLanguage = "ZH-TW";

    /// <summary>
    /// t=繁體 c=簡體 e=英文 v=語言
    /// </summary>
    public static string LangType = "t";

    /// <summary>
    /// 版本編號
    /// </summary>
    public static string ApVer = "YN01";

    /// <summary>
    /// 佈景主題名稱
    /// </summary>
    public static string Theme = "Default";

    /// <summary>
    /// 日期格式
    /// </summary>
    public static string sys_DateFormat = "yyyy/MM/dd";

    /// <summary>
    /// 日期時間格式
    /// </summary>
    public static string sys_DateTimeFormat = "yyyy/MM/dd hh:mm:ss";

    /// <summary>
    /// Report mdb  Data
    /// </summary>
    public static string sys_ReportData = "~/ReportData/";

    /// <summary>
    /// Report rpt File
    /// </summary>
    public static string sys_ReportRpt = "~/Report/";


    /// <summary>
    /// mailhost
    /// </summary>
    public static string sys_mailhost = "sys_mailhost";

    /// <summary>
    /// sys_mailport
    /// </summary>
    public static string sys_mailport = "sys_mailport";

    /// <summary>
    /// sys_mailaddress
    /// </summary>
    public static string sys_mailaddress = "sys_mailaddress";

    /// <summary>
    /// sys_mailuser
    /// </summary>
    public static string sys_mailuser = "sys_mailuser";

    /// <summary>
    /// sys_mailpwd
    /// </summary>
    public static string sys_mailpwd = "sys_mailpwd";

    #endregion

    #region 應用程式系統參數,讀取資料庫設定

    /// <summary>
    /// 進貨_金額小數位數
    /// </summary>
    public static Int16 cin_IAMT = 0;

    /// <summary>
    /// IQTY 進貨_數量小數
    /// </summary>
    public static Int16 cin_IQTY = 0;

    /// <summary>
    /// IUPC 進貨_單價小數位數
    /// </summary>
    public static Int16 cin_IUPC = 0;

    /// <summary>
    /// 銷售_金額小數位數
    /// </summary>
    public static Int16 cin_SAMT = 0;

    /// <summary>
    /// SQTY 銷售_數量小數
    ///</summary>
    public static Int16 cin_SQTY = 0;

    /// <summary>
    /// SUPC 銷售_單價小數位數
    /// </summary>
    public static Int16 cin_SUPC = 0;

    ///  <summary>
    /// TDE1 商品_定價小數位數
    /// </summary>
    public static Int16 cin_TDE1 = 0;


    ///  <summary>
    /// RRAT 外幣匯率_小數位數
    /// </summary>
    public static Int16 cin_RRAT = 0;

    ///  <summary>
    /// RMNY 外幣金額_小數位數
    /// </summary>
    public static Int16 cin_RMNY = 0;


    #endregion

    /// <summary>
    /// 取得下一組驗證碼
    /// </summary>
    /// <returns></returns>
    public static string NextVCode()
    {
      return String.Format("{0:0000}", r.Next(10000));
    }
  }
}