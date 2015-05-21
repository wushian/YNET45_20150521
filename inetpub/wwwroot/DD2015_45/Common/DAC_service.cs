using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.ComponentModel;
using YNetLib_45;

namespace DD2015_45 
{
  [DataObject]
  public class DAC_service : DAC
  {
    public DAC_service()
      : base()
    {
    }

    public DAC_service(OleDbConnection conn)
      : base(conn)
    {
    }


    public string get_baur_grname(string iFunc, string iContEdit, string iField, string oField, string st_inputnum)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_baur = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      st_xmlhd = "";
      st_xmlhd += "<iInput>" + st_inputnum + "</iInput>" + "<iField>" + iField + "</iField><oField>" + oField + "</oField>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_baur.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "";
      cmd.CommandText += "select gkey as baur_gkey,BCNUM as baur_BCNUM,BCNAM as baur_BCNAM ";
      cmd.CommandText += "from baur where BCNUM=? or BCNAM=? ";
      AddParam(cmd, "BCNUM", st_inputnum);
      AddParam(cmd, "BCNAM", st_inputnum);
      tb_baur = Select(cmd);
      if (tb_baur.Rows.Count == 1)
      {
        dw_Row = tb_baur.Rows[0];
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_BDAUR" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["baur_BCNUM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + "baur_BCNAM" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["baur_BCNAM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "</rTable>";
      }
      //
      if (st_ret == "")
      {
        tb_baur.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as baur_gkey,BCNUM as baur_BCNUM,BCNAM as baur_BCNAM ";
        cmd.CommandText += "from baur where BCNUM like ? ";
        AddParam(cmd, "BCNUM", st_inputnum + "%");
        tb_baur = Select(cmd);
        if (tb_baur.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCNUM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_baur.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as baur_gkey,BCNUM as baur_BCNUM,BCNAM as baur_BCNAM ";
        cmd.CommandText += "from baur where BCNAM like ? ";
        AddParam(cmd, "BCNAM", st_inputnum + "%");
        tb_baur = Select(cmd);
        if (tb_baur.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCNAM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      //
      tb_baur.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;
    }

    public string get_bpud_grname(string iFunc, string iContEdit, string iField, string oField, string st_inputnum, string old_PTN, string oCus, string oDate)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_bdlr = new DataTable();
      DataTable tb_bpud = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      decimal de_de1 = 0, de_upc = 0, de_dcx = 0, de_amt = 0;
      string st_dcx = "1100";
      //
      st_xmlhd = "";
      st_xmlhd += "<iInput>" + st_inputnum + "</iInput>" + "<iField>" + iField + "</iField><oField>" + oField + "</oField>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_bdlr.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "";
      cmd.CommandText += "select A.BDNUM as bdlr_BDNUM,A.BDDEX as bdlr_BDDEX ";
      cmd.CommandText += "from bdlr A ";
      cmd.CommandText += "where A.BDNUM=? ";
      AddParam(cmd, "bdnum", oCus);
      tb_bdlr = Select(cmd);
      //
      tb_bpud.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "";
      cmd.CommandText += "select A.gkey as bpud_gkey,A.BPNUM as bpud_BPNUM,A.BPTNA as bpud_BPNAM,A.BPCLA as bpud_BPCLA,A.BPUNI as bpud_BPUNI,A.BPDE1 as bpud_BPDE1,A.BPDE2 as bpud_BPDE2,H.BKNAM AS bpud_BPUNIN ";
      cmd.CommandText += "from bpud A ";
      cmd.CommandText += "left outer join pdbpuni H on A.BPUNI=H.BKNUM ";
      cmd.CommandText += "where A.BPNUM=? or A.BPNUB=? or A.BPTNA=? or A.BPNCR=? ";
      AddParam(cmd, "bpnum", st_inputnum);
      AddParam(cmd, "bpnub", st_inputnum);
      AddParam(cmd, "bptna", st_inputnum);
      AddParam(cmd, "bpncr", st_inputnum);
      tb_bpud = Select(cmd);
      de_upc = 0;
      de_dcx = 0;
      de_amt = 0;
      if (tb_bpud.Rows.Count == 1)
      {
        dw_Row = tb_bpud.Rows[0];
        //取單價
        de_de1 = DAC.GetDecimalValue(dw_Row["bpud_BPDE1"]);
        de_upc = DAC.GetDecimalValue(dw_Row["bpud_BPDE1"]);
        de_dcx = 100;
        de_amt = DAC.GetDecimalValue(dw_Row["bpud_BPDE1"]);
        if (tb_bdlr.Rows.Count == 1)
        {
          st_dcx = DAC.GetStringValue(tb_bdlr.Rows[0]["bdlr_BDDEX"]);
          if (st_dcx.Length == 1)
          {
            if ((st_dcx.Substring(0, 1) == "1") || (st_dcx.Substring(0, 1) == "2"))
            {
              de_upc = DAC.GetDecimalValue(dw_Row["bpud_BPDE" + st_dcx.Substring(0, 1)]);
              de_amt = de_upc;
              if (st_dcx.Substring(0, 1) != "1")
              {
                if (de_de1 != 0)
                {
                  de_dcx = sFN.Round(de_upc / de_de1 * 100, 2);
                }
              }
            }
          }
          else if (st_dcx.Length == 4)
          {
            de_dcx = DAC.GetDecimalValue(st_dcx.Substring(1));
            if (de_dcx == 0)
            {
              de_dcx = 100;
            }
            if ((st_dcx.Substring(0, 1) == "1") || (st_dcx.Substring(0, 1) == "2"))
            {
              de_upc = sFN.Round(DAC.GetDecimalValue(dw_Row["bpud_BPDE" + st_dcx.Substring(0, 1)]) * de_dcx / 100, 2);
              de_amt = de_upc;
              //
              if (de_de1 != 0)
              {
                de_dcx = sFN.Round(de_upc / de_de1 * 100, 2);
              }
            }
          }
        }
        //
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBPTN" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPNUM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBNAM" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPNAM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBCLA" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPCLA"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBUNI" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPUNI"]) + "</rValue><rText>" + DAC.GetStringValue(dw_Row["bpud_BPUNIN"]) + "</rText>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBUNIN" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPUNIN"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBDPC" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPDE1"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBUPC" + "</rField><rValue>" + de_upc.ToString() + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBDCX" + "</rField><rValue>" + de_dcx.ToString() + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iFunc + "_RBAMT" + "</rField><rValue>" + de_amt.ToString() + "</rValue>";
        st_ret += "</rData>";

        st_ret += "</rTable>";
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM,BPCLA as bpud_BPCLA,BPUNI as bpud_BPUNI ";
        cmd.CommandText += "from bpud where bpnum like ? ";
        AddParam(cmd, "bpnum", st_inputnum + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPNUM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM,BPCLA as bpud_BPCLA,BPUNI as bpud_BPUNI ";
        cmd.CommandText += "from bpud where bpnub like ? ";
        AddParam(cmd, "bpnub", st_inputnum + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPNUB" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM,BPCLA as bpud_BPCLA,BPUNI as bpud_BPUNI ";
        cmd.CommandText += "from bpud where bptna like ? ";
        AddParam(cmd, "bptna", "%" + st_inputnum + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPTNA" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM,BPCLA as bpud_BPCLA,BPUNI as bpud_BPUNI ";
        cmd.CommandText += "from bpud where bpcla like ? ";
        AddParam(cmd, "bpcla", "%" + st_inputnum + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPCLA" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      //
      tb_bdlr.Dispose();
      tb_bpud.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;

    }

    public string get_bdlr_grname(string iFunc, string iContEdit, string iField, string oField, string st_inputnum, string old_NUM)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_bdlr = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      st_xmlhd = "";
      st_xmlhd += "<iInput>" + st_inputnum + "</iInput>" + "<iField>" + iField + "</iField><oField>" + oField + "</oField>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_bdlr.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "";
      cmd.CommandText += "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM ";
      cmd.CommandText += "from bdlr where BDNUM=? or BDNAM=? ";
      AddParam(cmd, "BDNUM", st_inputnum);
      AddParam(cmd, "BDNAM", st_inputnum);
      tb_bdlr = Select(cmd);
      if (tb_bdlr.Rows.Count == 1)
      {
        dw_Row = tb_bdlr.Rows[0];
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        //st_ret += "<rField>" + iFunc + "_BDAUR" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bdlr_BDNUM"]) + "</rValue>";
        st_ret += "<rField>" + iField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bdlr_BDNUM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        //st_ret += "<rField>" +oField "bdlr_BDNAM" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bdlr_BDNAM"]) + "</rValue>";
        st_ret += "<rField>" + oField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bdlr_BDNAM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "</rTable>";
      }
      //
      if (st_ret == "")
      {
        tb_bdlr.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM ";
        cmd.CommandText += "from bdlr where BDNUM like ? ";
        AddParam(cmd, "BDNUM", st_inputnum + "%");
        tb_bdlr = Select(cmd);
        if (tb_bdlr.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BDNUM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bdlr.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText += "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM ";
        cmd.CommandText += "from bdlr where BDNAM like ? ";
        AddParam(cmd, "BDNAM", st_inputnum + "%");
        tb_bdlr = Select(cmd);
        if (tb_bdlr.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BDNAM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      //
      tb_bdlr.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;
    }


    public string get_es101_cname(string iFunc, string iContEdit, string st_num, string iField, string oField)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_es101 = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      st_xmlhd = "";
      st_xmlhd += "<iField>" + iField + "</iField><oField>" + oField + "</oField><iInput>" + st_num + "</iInput>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_es101.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "select gkey as es101_gkey,no as es101_no,cname as es101_cname,ename as es101_ename from es101 where gkey=? or no=? or cname=? or ename=? ";
      AddParam(cmd, "gkey", st_num);
      AddParam(cmd, "no", st_num);
      AddParam(cmd, "cname", st_num);
      AddParam(cmd, "ename", st_num);
      tb_es101 = Select(cmd);
      if (tb_es101.Rows.Count == 1)
      {
        dw_Row = tb_es101.Rows[0];
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["es101_no"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + oField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["es101_cname"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "</rTable>";

      }
      if (st_ret == "")
      {
        tb_es101.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as es101_gkey,no as es101_no,cname as es101_cname,ename as es101_ename from es101 where no like ? ";
        AddParam(cmd, "no", st_num + "%");
        tb_es101 = Select(cmd);
        if (tb_es101.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "no" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_es101.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as es101_gkey,no as es101_no,cname as es101_cname,ename as es101_ename from es101 where cname like ? ";
        AddParam(cmd, "cname", "%" + st_num + "%");
        tb_es101 = Select(cmd);
        if (tb_es101.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "cname" + "</rIndex>";
          st_ret.PadRight(10);
        }
      }
      //
      if (st_ret == "")
      {
        tb_es101.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as es101_gkey,no as es101_no,cname as es101_cname,ename as es101_ename from es101 where ename like ? ";
        AddParam(cmd, "ename", "%" + st_num + "%");
        tb_es101 = Select(cmd);
        if (tb_es101.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "ename" + "</rIndex>";
          st_ret.PadRight(10);
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      tb_es101.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;
    }

    public string get_bdlr_cname(string iFunc, string iContEdit, string st_num, string iField, string oField)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_bdlr = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      //
      st_xmlhd = "";
      st_xmlhd += "<iField>" + iField + "</iField><oField>" + oField + "</oField><iInput>" + st_num + "</iInput>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_bdlr.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM from bdlr where BDNUM=? or  BDSHT=? or BDCJ5=? or BDTEL=? or BDNAM=? ";
      AddParam(cmd, "BDNUM", st_num);
      AddParam(cmd, "BDSHT", st_num);
      AddParam(cmd, "BDCJ5", st_num);
      AddParam(cmd, "BDTEL", st_num);
      AddParam(cmd, "BDNAM", st_num);
      tb_bdlr = Select(cmd);
      if (tb_bdlr.Rows.Count == 1)
      {
        dw_Row = tb_bdlr.Rows[0];
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bdlr_BDNUM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + oField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bdlr_BDNAM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "</rTable>";

      }
      if (st_ret == "")
      {
        tb_bdlr.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM from bdlr where BDNUM LIKE ?  ";
        AddParam(cmd, "BDNUM", st_num + "%");
        tb_bdlr = Select(cmd);
        if (tb_bdlr.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BDNUM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bdlr.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM from bdlr where BDSHT LIKE ?  ";
        AddParam(cmd, "BDSHT", st_num + "%");
        tb_bdlr = Select(cmd);
        if (tb_bdlr.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BDSHT" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bdlr.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM from bdlr where BDCJ5 LIKE ?  ";
        AddParam(cmd, "BDCJ5", st_num + "%");
        tb_bdlr = Select(cmd);
        if (tb_bdlr.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BDCJ5" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bdlr.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM from bdlr where BDTEL LIKE ?  ";
        AddParam(cmd, "BDTEL", st_num + "%");
        tb_bdlr = Select(cmd);
        if (tb_bdlr.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BDTEL" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bdlr.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bdlr_gkey,BDNUM as bdlr_BDNUM,BDNAM as bdlr_BDNAM from bdlr where BDNAM LIKE ?  ";
        AddParam(cmd, "BDNAM", "%" + st_num + "%");
        tb_bdlr = Select(cmd);
        if (tb_bdlr.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BDNAM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      tb_bdlr.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;
    }

    /// <summary>
    /// st_rix=('ri3'=tx_ri3a_RIGSM) 'ri4'
    /// </summary>
    /// <param name="st_rix"></param>
    /// <param name="st_num"></param>
    /// <param name="iField"></param>
    /// <param name="oField1"></param>
    /// <returns></returns>
    public string get_bcvw_cname_ri(string iFunc, string iContEdit, string st_num, string iField, string oField)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_bcvw = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      //
      st_xmlhd = "";
      st_xmlhd += "<iField>" + iField + "</iField><oField>" + oField + "</oField><iInput>" + st_num + "</iInput>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_bcvw.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "select gkey as bcvw_gkey,BCNUM as bcvw_BCNUM,BCNAM as bcvw_BCNAM,BCGSM as bcvw_BCGSM,BCTL1 as bcvw_BCTL1,BCZIP as bcvw_BCZIP,BCADR as bcvw_BCADR from bcvw where BCNUM=? or BCNAM=? or BCGSM=? or BCTL1=? ";
      AddParam(cmd, "BCNUM", st_num);
      AddParam(cmd, "BCNAM", st_num);
      AddParam(cmd, "BCGSM", st_num);
      AddParam(cmd, "BCTL1", st_num);
      tb_bcvw = Select(cmd);
      if (tb_bcvw.Rows.Count == 1)
      {
        dw_Row = tb_bcvw.Rows[0];
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bcvw_BCNUM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + oField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bcvw_BCNAM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iContEdit + "tx_" + iFunc + "_RIGSM" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bcvw_BCGSM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iContEdit + "tx_" + iFunc + "_RITEL" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bcvw_BCTL1"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iContEdit + "tx_" + iFunc + "_RIZIP" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bcvw_BCZIP"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iContEdit + "tx_" + iFunc + "_RIADR" + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bcvw_BCADR"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "</rTable>";
      }
      //
      if (st_ret == "")
      {
        tb_bcvw.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bcvw_gkey,BCNUM as bcvw_BCNUM from bcvw where BCNUM like ? ";
        AddParam(cmd, "BCNUM", st_num + "%");
        tb_bcvw = Select(cmd);
        if (tb_bcvw.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCNUM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bcvw.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bcvw_gkey,BCNUM as bcvw_BCNUM from bcvw where BCNAM like ? ";
        AddParam(cmd, "BCNAM", "%" + st_num + "%");
        tb_bcvw = Select(cmd);
        if (tb_bcvw.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCNAM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bcvw.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bcvw_gkey,BCNUM as bcvw_BCNUM from bcvw where BCGSM like ? ";
        AddParam(cmd, "BCGSM", "%" + st_num + "%");
        tb_bcvw = Select(cmd);
        if (tb_bcvw.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCGSM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bcvw.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bcvw_gkey,BCNUM as bcvw_BCNUM from bcvw where BCTL1 like ? ";
        AddParam(cmd, "BCTL1", "%" + st_num + "%");
        tb_bcvw = Select(cmd);
        if (tb_bcvw.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCTL1" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      tb_bcvw.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;
    }


    public string get_bpud_cname(string iFunc, string iContEdit, string st_num, string iField, string oField)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_bpud = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      //
      st_xmlhd = "";
      st_xmlhd += "<iField>" + iField + "</iField><oField>" + oField + "</oField><iInput>" + st_num + "</iInput>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_bpud.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM from bpud where BPNUM=? or BPNUB=? or BPNCR=? or BPTNA=? or BPCLA=? or BPENA=? ";
      AddParam(cmd, "BPNUM", st_num);
      AddParam(cmd, "BPNUB", st_num);
      AddParam(cmd, "BPNCR", st_num);
      AddParam(cmd, "BPTNA", st_num);
      AddParam(cmd, "BPCLA", st_num);
      AddParam(cmd, "BPENA", st_num);
      tb_bpud = Select(cmd);
      if (tb_bpud.Rows.Count == 1)
      {
        dw_Row = tb_bpud.Rows[0];
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPNUM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + oField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["bpud_BPNAM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "</rTable>";

      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM from bpud where BPNUM like ? ";
        AddParam(cmd, "BPNUM", st_num + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPNUM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM from bpud where BPNUB like ? ";
        AddParam(cmd, "BPNUB", st_num + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPNUB" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM from bpud where BPNCR like ? ";
        AddParam(cmd, "BPNCR", st_num + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPNCR" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM from bpud where BPTNA like ? ";
        AddParam(cmd, "BPTNA", "%" + st_num + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPTNA" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM from bpud where BPCLA like ? ";
        AddParam(cmd, "BPCLA", "%" + st_num + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPCLA" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_bpud.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as bpud_gkey,BPNUM as bpud_BPNUM,BPTNA as bpud_BPNAM from bpud where BPENA like ? ";
        AddParam(cmd, "BPENA", "%" + st_num + "%");
        tb_bpud = Select(cmd);
        if (tb_bpud.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BPENA" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      tb_bpud.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;
    }


    public string get_baur_tname(string iFunc, string iContEdit, string st_num, string iField, string oField)
    {
      string st_ret = "";
      string st_xmlhd = "";
      OleDbCommand cmd = NewCommand();
      DataTable tb_baur = new DataTable();
      DataRow dw_Row;
      clsFN sFN = new clsFN();
      //
      st_xmlhd = "";
      st_xmlhd += "<iField>" + iField + "</iField><oField>" + oField + "</oField><iInput>" + st_num + "</iInput>";
      st_xmlhd += "<iFunc>" + iFunc + "</iFunc><iContEdit>" + iContEdit + "</iContEdit>";
      //
      tb_baur.Clear();
      cmd.Parameters.Clear();
      cmd.CommandText = "select gkey as baur_gkey,BCNUM as baur_BCNUM,BCNAM as baur_BCNAM from baur where BCNUM=? or BCNAM=? ";
      AddParam(cmd, "BCNUM", st_num);
      AddParam(cmd, "BCNAM", st_num);
      tb_baur = Select(cmd);
      if (tb_baur.Rows.Count == 1)
      {
        dw_Row = tb_baur.Rows[0];
        st_ret = "<rFlag>1</rFlag>";
        st_ret += "<rTable>";
        st_ret += "<rData>";
        st_ret += "<rField>" + iField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["baur_BCNUM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "<rData>";
        st_ret += "<rField>" + oField + "</rField><rValue>" + DAC.GetStringValue(dw_Row["baur_BCNAM"]) + "</rValue>";
        st_ret += "</rData>";
        st_ret += "</rTable>";
      }
      //
      if (st_ret == "")
      {
        tb_baur.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as baur_gkey,BCNUM as baur_BCNUM from baur where BCNUM like ? ";
        AddParam(cmd, "BCNUM", st_num + "%");
        tb_baur = Select(cmd);
        if (tb_baur.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCNUM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        tb_baur.Clear();
        cmd.Parameters.Clear();
        cmd.CommandText = "select gkey as baur_gkey,BCNUM as baur_BCNUM from baur where BCNAM like ? ";
        AddParam(cmd, "BCNAM", "%" + st_num + "%");
        tb_baur = Select(cmd);
        if (tb_baur.Rows.Count > 0)
        {
          st_ret = "<rFlag>2</rFlag><rIndex>" + "BCNAM" + "</rIndex>";
        }
      }
      //
      if (st_ret == "")
      {
        st_ret = "0";
        st_ret = "<rFlag>0</rFlag>";
      }
      tb_baur.Dispose();
      sFN.Dispose();
      return st_xmlhd + st_ret;
    }
  }
}