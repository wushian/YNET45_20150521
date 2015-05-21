using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using YNetLib_45;

namespace DD2015_45.wwwService
{
  /// <summary>
  ///Svrwww 的摘要描述
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
  [System.Web.Script.Services.ScriptService]
  public class Svrwww : System.Web.Services.WebService
  {
    [WebMethod]
    public string getName(string st_UserGkey, string st_CusGkey, string vNum, string iField, string oField)
    {
      return vNum + " " + iField + " " + oField;
    }

    [WebMethod]
    public string service_get_bdlr_grname(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string iField, string oField, string st_input, string old_NUM)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_bdlr_grname(iFunc, iContEdit, iField, oField, st_inputnum, old_NUM);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }


    [WebMethod]
    public string service_get_bpud_grname(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string iField, string oField, string st_input, string old_PTN, string oCus, string oDate)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_bpud_grname(iFunc, iContEdit, iField, oField, st_inputnum, old_PTN, oCus, oDate);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }

    [WebMethod]
    public string service_get_baur_grname(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string iField, string oField, string st_input)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_baur_grname(iFunc, iContEdit, iField, oField, st_inputnum);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }

    [WebMethod]
    public string service_get_es101_cname(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string st_input, string iField, string oField)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_es101_cname(iFunc, iContEdit, st_inputnum, iField, oField);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }

    [WebMethod]
    public string service_get_bdlr_cname(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string st_input, string iField, string oField1)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_bdlr_cname(iFunc, iContEdit, st_inputnum, iField, oField1);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }

    [WebMethod]
    public string service_get_bpud_cname(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string st_input, string iField, string oField1)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_bpud_cname(iFunc, iContEdit, st_inputnum, iField, oField1);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }

    /// <summary>
    /// st_rix=('ri3'=tx_ri3a_RIGSM) 'ri4'
    /// </summary>
    /// <param name="st_UserGkey"></param>
    /// <param name="st_CusGkey"></param>
    /// <param name="st_rix"></param>
    /// <param name="st_input"></param>
    /// <param name="iField"></param>
    /// <param name="oField1"></param>
    /// <returns></returns>
    [WebMethod]
    public string service_get_bcvw_cname_ri(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string st_input, string iField, string oField)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_bcvw_cname_ri(iFunc, iContEdit, st_inputnum, iField, oField);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="st_UserGkey"></param>
    /// <param name="st_CusGkey"></param>
    /// <param name="st_fun"></param>
    /// <param name="st_input"></param>
    /// <param name="iField"></param>
    /// <param name="oField1"></param>
    /// <returns></returns>
    [WebMethod]
    public string service_get_bpud_tname(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string st_input, string iField, string oField1)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_bpud_cname(iFunc, iContEdit, st_inputnum, iField, oField1);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }

    [WebMethod]
    public string service_get_baur(string st_UserGkey, string st_CusGkey, string iFunc, string iContEdit, string st_input, string iField, string oField1)
    {
      string st_inputnum = "";
      string st_outputnum = "";
      DAC_service DAC_service = new DAC_service();
      //decode
      st_inputnum = Server.UrlDecode(st_input);
      st_outputnum = DAC_service.get_baur_tname(iFunc, iContEdit, st_inputnum, iField, oField1);
      //encode
      //st_outputnum = Server.UrlEncode(st_outputnum);
      return st_outputnum;
    }
  }
}
