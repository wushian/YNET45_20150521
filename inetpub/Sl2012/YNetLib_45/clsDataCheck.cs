using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Drawing;

namespace YNetLib_45
{
  public class clsDataCheck : IDisposable
  {
    private string st_ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    #region IDisposable 成員
    public void Dispose()
    {

    }
    #endregion

    public bool cIsTaiwanId(string vData)
    {
      bool vOK = false;
      int vAC = 0, vI = 0, vT = 0;
      string V1 = "", V4 = "", V41 = "";
      int V2 = 0, V3 = 0, V5 = 0;
      int[] vC = new int[26];
      vC[0] = 10;
      vC[1] = 11;
      vC[2] = 12;
      vC[3] = 13;
      vC[4] = 14;
      vC[5] = 15;
      vC[6] = 16;
      vC[7] = 17;
      vC[8] = 34;
      vC[9] = 18;
      vC[10] = 19;
      vC[11] = 20;
      vC[12] = 21;
      vC[13] = 22;
      vC[14] = 35;
      vC[15] = 23;
      vC[16] = 24;
      vC[17] = 25;
      vC[18] = 26;
      vC[19] = 27;
      vC[20] = 28;
      vC[21] = 29;
      vC[22] = 32;
      vC[23] = 30;
      vC[24] = 31;
      vC[25] = 33;
      vOK = false;
      if (vData.Length == 10)
      {
        try
        {
          V1 = vData.Substring(0, 1);
          V2 = Convert.ToInt32(V1[0]);
          V3 = V2 - 65;
          V4 = vC[V3].ToString().Substring(0, 1);
          V41 = vC[V3].ToString().Substring(1, 1);
          vAC = Convert.ToInt32(V4) + Convert.ToInt32(V41) * 9;
          vI = 9;
          vT = 0;
          while (vI > 1)
          {
            vI = vI - 1;
            V5 = Convert.ToInt32(vData.Substring(9 - vI, 1)) * vI;
            vT = vT + V5;
          }
          vT = vT + Convert.ToInt32(vData.Substring(9, 1));
          vT = vT + vAC;
          vOK = ((vT % 10) == 0);
          vOK = true;
        }
        catch
        {
          vOK = false;
        }
        finally
        {
        }
      }
      else
      {
        vOK = false;
      }
      return vOK;
    }

    public bool cIsTaiwanIdChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsTaiwanId(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_TaiwanId") + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsTaiwanINVChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsTaiwanINV(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_TaiwanINV") + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }


    public bool cIsTaiwanINV(string idvalue)
    {
      bool vRet = false;
      string ss = "", sc = "", tmp = "";
      int sr = 0, i = 0, sum = 0, s1 = 0, s2 = 0;
      if (idvalue.Length == 8)
      {
        ss = "0123456789";
        vRet = true;
        for (i = 0; i <= 7; i++)
        {
          sc = idvalue.Substring(i, 1);
          sr = ss.IndexOf(sc, 0);
          if (sr < 0) { vRet = false; };
        }
        if (vRet)
        {
          tmp = "12121241";
          sum = 0;
          for (i = 0; i <= 7; i++)
          {
            s1 = Convert.ToInt32(idvalue.Substring(i, 1));
            s2 = Convert.ToInt32(tmp.Substring(i, 1));
            sum = sum + inv_idcal(s1 * s2);
          }
          if (!inv_idvalid(sum))
          {
            if (idvalue.Substring(6, 1) == "7")
            {
              vRet = inv_idvalid(sum + 1);
            }
            else
            {
              vRet = inv_idvalid(sum);
            }
          }
          else
          {
            vRet = inv_idvalid(sum);
          }
        }
      }
      else
      {
        vRet = false;
      }
      return vRet;
    }

    public bool inv_idvalid(int n)
    {
      return (n % 10 == 0);
    }

    public int inv_idcal(int n)
    {
      int sum = 0;
      while (n != 0)
      {
        sum = sum + (n % 10);
        n = (n - n % 10) / 10;
      }
      return sum;
    }

    public bool cIsPassword(string vData)
    {
      int by_cnt = 0;
      string st_ch = "";
      if (vData.Length >= 6)
      {
        for (var i = 0; i < vData.Length; i++)
        {
          st_ch = vData.Substring(i, 1).ToUpper();
          if (st_ABC.IndexOf(st_ch) >= 0)
          {
            by_cnt = by_cnt + 1;
          }
        }
        if (by_cnt >= 2)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }
    //
    public bool cIsDecimal(string vData)
    {
      Decimal ck_int = 0;
      try
      {
        ck_int = Convert.ToDecimal(vData);
        return true;
      }
      catch
      {
        return false;
      }
    }
    //
    public bool cIsInt(string vData)
    {
      int ck_int = 0;
      try
      {
        ck_int = Convert.ToInt32(vData);
        return true;
      }
      catch
      {
        return false;
      }
    }
    //
    public bool cIsDate(string vData)
    {
      DateTime ck_datetime;
      try
      {
        ck_datetime = Convert.ToDateTime(vData);
        return true;
      }
      catch
      {
        return false;
      }
    }
    //
    public bool cIsEmpty(string vData)
    {
      if (vData == "")
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    //
    public bool cIsPasswordChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      if (!cIsPassword(vData))
      {
        sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Not_be_blank") + "." + "\r\n";
        ret = false;
      }
      return ret;
    }


    public bool cIsStrEmptyChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      if (cIsEmpty(vData))
      {
        sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Not_be_blank") + "." + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsStrEmptyChk(bool ret, string vData, string vLabel_text, ref Label lb_label, ref string sMsg)
    {
      if (cIsEmpty(vData))
      {
        lb_label.ForeColor = Color.Red;
        ret = false;
      }
      else
      {
        lb_label.ForeColor = default(Color);
      }
      return ret;
    }

    public bool cIsStrIntChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsInt(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_an_integer ") + "." + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsStrIntChk(bool ret, string vData, string vLabel_text, ref Label lb_label, ref string sMsg)
    {
      if (!cIsInt(vData))
      {
        lb_label.ForeColor = Color.Red;
        ret = false;
      }
      else
      {
        lb_label.ForeColor = default(Color);
      }
      return ret;
    }

    public bool cIsStrDatetimeChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (vData == "")
        {
          //pass
        }
        else if (!cIsDate(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_date ") + "." + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsStrDatetimeChk(bool ret, string vData, string vLabel_text, ref Label lb_label, ref string sMsg)
    {

      if (vData == "")
      {
        //pass
      }
      else if (!cIsDate(vData))
      {
        lb_label.ForeColor = Color.Red;
        ret = false;
      }
      else
      {
        lb_label.ForeColor = default(Color);
      }
      return ret;
    }

    public bool cIsStrDecimalChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsDecimal(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_numeric") + "." + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsEmail(string vData)
    {
      bool cCheck = true;
      cCheck = true;
      if (vData.IndexOf("@") <= 1)
      {
        cCheck = false;
      }
      if (vData.IndexOf(".") <= 2)
      {
        cCheck = false;
      }
      return cCheck;
    }

    public bool cIsEmailChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsEmail(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_email") + "." + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsTime(string vData)
    {
      clsFN vFN = new clsFN();
      string st_hh = "";
      string st_mm = "";
      bool cCheck = true;
      if ((vData.Length != 4) || (!vFN.IsAllNum(vData)))
      {
        cCheck = false;
      }
      else if (vData.IndexOf(".") >= 0)
      {
        cCheck = false;
      }
      else
      {
        st_hh = vData.Substring(0, 2);
        st_mm = vData.Substring(2, 2);
        if ((Convert.ToInt32(st_hh) < 0) || (Convert.ToInt32(st_hh) > 24))
        {
          cCheck = false;
        }
        if ((Convert.ToInt32(st_mm) < 0) || (Convert.ToInt32(st_mm) > 59))
        {
          cCheck = false;
        }
      }
      vFN.Dispose();
      return cCheck;
    }

    public bool cIsTimeChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsTime(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_time") + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsNum(string vData)
    {
      clsFN vFN = new clsFN();
      bool cCheck;
      cCheck = vFN.IsAllNum(vData);
      vFN.Dispose();
      return cCheck;
    }

    public bool cIsNumChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsNum(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_numeric") + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }

    public bool cIsTelNum(string vData)
    {
      clsFN vFN = new clsFN();
      bool cCheck;
      cCheck = vFN.IsTelNum(vData);
      vFN.Dispose();
      return cCheck;
    }

    public bool cIsTelNumChk(bool ret, string vData, string vLabel_text, ref string sMsg, string st_lang, clsFN sFN)
    {
      try
      {
        if (!cIsTelNum(vData))
        {
          sMsg += vLabel_text + "," + sFN.TSTRING(st_lang, "lib_Must_be_a_Phone_number") + "." + "\r\n";
          ret = false;
        }
      }
      catch (Exception e)
      {
        sMsg += vLabel_text + "," + e.Message + "\r\n";
        ret = false;
      }
      return ret;
    }
  }
}
