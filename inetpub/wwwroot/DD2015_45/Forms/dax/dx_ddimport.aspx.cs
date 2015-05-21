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
using YNetLib_45;

namespace DD2015_45.Forms.dax
{
  public partial class dx_ddimport : FormBase
  {
    string st_object_func = "UNdbset";
    //string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    Int64 in_rec = 0, in_ins = 0, in_upt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      //檢查權限&狀態
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 10, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {

        }
      }

    }

    protected void bt_SUR_Click(object sender, EventArgs e)
    {
      string sErrMsg = "";
      bool bl_test = act_SUR(ref sErrMsg, false);
      if (bl_test)
      {
        if (sErrMsg == "")
        {
          if (act_SUR(ref sErrMsg, true))
          {
            li_Msg.Text = "<script> alert('資料轉入完成!') </script>";
            Response.Redirect("dx_dbset.aspx");
          }
          else
          {
            lb_ErrorMessage.Visible = true;
            lb_ErrorMessage.Text = sErrMsg;
          }
        }
        else
        {
          lb_ErrorMessage.Visible = true;
          lb_ErrorMessage.Text = sErrMsg;
        }
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = sErrMsg;
      }
      //
    }

    private bool act_SUR(ref string sErrMsg, bool bl_commit)
    {
      string st_BakFilName = "";
      string st_BakFilFullName = "";
      bool bl_FileOk = false;
      bool bl_final = false;
      in_rec = 0; in_ins = 0; in_upt = 0;

      try
      {
        st_BakFilName = "bk_ddimport_" + sFN.Get_guid5();
        st_BakFilFullName = sys_DocFilePath + st_BakFilName + ".xlsx";
        attFbutton.PostedFile.SaveAs(st_BakFilFullName);
        bl_FileOk = true;
      }
      catch (Exception e)
      {
        bl_FileOk = false;
        sErrMsg += e.Message + (char)13 + (char)10;
      }
      //
      if (bl_FileOk)
      {
        clsDataCheck DataCheck = new clsDataCheck();

        string st_connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + st_BakFilFullName + "';Extended Properties=" + (char)34 + "Excel 12.0;HDR=YES" + (char)34;
        OleDbConnection Conn_Excel = new OleDbConnection(st_connstring);
        OleDbCommand cmd_excel = new OleDbCommand();
        OleDbDataReader rd_Excel = null;

        OleDbConnection connr = DAC.NewReaderConnr();
        OleDbConnection conni = DAC.NewReaderConnr();
        DAC_dbset dbsetDao = new DAC_dbset(conni);
        DAC dacDao = new DAC(connr);
        try
        {
          Conn_Excel.Open();
          cmd_excel.CommandText = "SELECT * FROM [" + txtSHEET.Text.Trim() + "$]";
          rd_Excel = dbsetDao.OleDbReader(Conn_Excel, cmd_excel);
          //
          //int in_index = 0;
          int in_row, in_col, in_typeL;
          string st_row, st_col, st_field, st_fieldN, st_type, st_control, st_pk;
          //
          CmdQueryS.CommandText=" AND A.DBVER=? AND A.DBAPX=? AND A.DBTBL=? AND A.DBNUM=? ";
          DAC.AddParam(CmdQueryS, "DBVER", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]));
          DAC.AddParam(CmdQueryS, "DBAPX", DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]));
          DAC.AddParam(CmdQueryS, "DBTBL", DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]));
          DAC.AddParam(CmdQueryS, "DBNUM", DAC.GetStringValue(Session["fmDASET_tx_DASET_DANUM"]));
          //
          conni.Open();
          DataTable tb_dbset = new DataTable();
          DataRow[] dr_dbsets;
          DataRow ins_row;
          DbDataAdapter da_ADP_dbset = dbsetDao.GetDataAdapter(ApVer, "UNdbset", "dbset", "", false, "", CmdQueryS, "", " A.DBFLD ", "SEL INS");
          da_ADP_dbset.Fill(tb_dbset);
          //
          OleDbTransaction thistran = conni.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP_dbset.InsertCommand.Transaction = thistran;
          try
          {
            while (rd_Excel.Read())
            {
              in_rec += 1;
              st_row = sFN.GetEXCEL_READERValue(rd_Excel, "A").Trim();
              st_col = sFN.GetEXCEL_READERValue(rd_Excel, "B").Trim();
              in_row = DAC.GetInt32Value(sFN.GetEXCEL_READERValue(rd_Excel, "A").Trim());
              in_col = DAC.GetInt32Value(sFN.GetEXCEL_READERValue(rd_Excel, "B").Trim());
              st_field = sFN.GetEXCEL_READERValue(rd_Excel, "C").Trim();
              st_fieldN = sFN.GetEXCEL_READERValue(rd_Excel, "D").Trim();
              st_type = sFN.GetEXCEL_READERValue(rd_Excel, "E").Trim();
              in_typeL = DAC.GetInt32Value(sFN.GetEXCEL_READERValue(rd_Excel, "F").Trim());
              st_control = sFN.GetEXCEL_READERValue(rd_Excel, "G").Trim();
              st_pk = sFN.GetEXCEL_READERValue(rd_Excel, "H").Trim();
              if (st_pk != "1") { st_pk = "0"; };
              //取 st_row
              if ((st_field != "") && (st_type != "") && (st_control != ""))
              {
                dr_dbsets = tb_dbset.Select("DBSET_DBFLD='" + st_field + "'");
                if (dr_dbsets.Length == 0)
                {
                  ins_row = tb_dbset.NewRow();
                  ins_row["dbset_gkey"] = DAC.get_guidkey(); // 
                  ins_row["dbset_mkey"] = DAC.get_guidkey(); //
                  //
                  ins_row["DBSET_DBVER"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAVER"]);       // 版本編號
                  ins_row["DBSET_DBNUM"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DANUM"]);      // 客戶編號
                  ins_row["DBSET_DBAPX"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAAPX"]);       // 程式名稱
                  ins_row["DBSET_DBITM"] = 0;       // 項　　次
                  ins_row["DBSET_DBFLD"] = st_field;       // 欄位名稱
                  ins_row["DBSET_DBTNA"] = st_fieldN;      // 繁體名稱
                  ins_row["DBSET_DBTYP"] = st_type;        // 資料型態
                  ins_row["DBSET_DBLEN"] = in_typeL;       // 資料長度
                  ins_row["DBSET_DBENA"] = st_fieldN;       // 英文名稱
                  ins_row["DBSET_DBCNA"] = st_fieldN;       // 簡體名稱
                  ins_row["DBSET_DBJIA"] = "A";       // JoinAlias
                  ins_row["DBSET_DBJIN"] = "";        // JoinTable
                  ins_row["DBSET_DBJIF"] = "";        // ret field
                  ins_row["DBSET_DBJIK"] = "";        // Join Key
                  ins_row["DBSET_DBROW"] = in_row;       // ROW 位置
                  ins_row["DBSET_DBCOL"] = in_col;       // COL 位置
                  ins_row["DBSET_DBUCO"] = st_control;   // 使用元件
                  ins_row["DBSET_DBWID"] = 10;           // 元件寬度
                  ins_row["DBSET_DBUED"] = 0;            // EDIT寬度
                  ins_row["DBSET_DBUTB"] = "";       // 參考Table
                  if (st_type == "DATETIME")
                  {
                    ins_row["DBSET_DBUHO"] = st_type;       // 參考Class
                  }
                  else
                  {
                    ins_row["DBSET_DBUHO"] = "";       // 參考Class
                  }
                  ins_row["DBSET_DBGRD"] = "0";        // GridList
                  ins_row["DBSET_DBDEF"] = "<>";       // Default
                  ins_row["DBSET_DBPRY"] = st_pk;      // Pri  Key
                  ins_row["DBSET_DBINS"] = "1";        // 是否新增
                  ins_row["DBSET_DBMOD"] = "1";        // 是否更正
                  ins_row["DBSET_DBEMP"] = "1";        // 是否空白
                  ins_row["DBSET_DBSER"] = "0";        // 查詢鍵值
                  ins_row["DBSET_DBSOR"] = "0";        // 排序鍵值
                  ins_row["DBSET_DBUFX"] = "";         // DBUFX
                  ins_row["DBSET_DBTBL"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DATBL"]);       // TABLE名
                  ins_row["DBSET_DBTYD"] = "";       // 資料型態
                  ins_row["DBSET_DBREN"] = DAC.GetStringValue(Session["fmDASET_tx_DASET_DAREN"]);       // B檔序號
                  //
                  ins_row["dbset_trusr"] = UserGkey;  //
                  tb_dbset.Rows.Add(ins_row);
                }
                else
                {
                  sErrMsg += "資料行" + in_rec.ToString() + " Field " + st_field + " 已存在." + (char)13 + (char)10;
                }
              }
              else if ((st_field == "") && (st_col != "") && (st_row != ""))
              {
                sErrMsg += "資料行" + in_rec.ToString() + " Field 不可空白." + (char)13 + (char)10;
              }
              else if ((st_type == "") && (st_col != "") && (st_row != ""))
              {
                sErrMsg += "資料行" + in_rec.ToString() + " Type 不可空白." + (char)13 + (char)10;
              }
              else if ((st_control == "") && (st_col != "") && (st_row != ""))
              {
                sErrMsg += "資料行" + in_rec.ToString() + " Control 不可空白." + (char)13 + (char)10;
              }
            }  //while excel
            da_ADP_dbset.Update(tb_dbset);

            if (bl_commit)
            {
              thistran.Commit();
            }
            else
            {
              thistran.Rollback();
            }
            bl_final = true;
          }
          catch (Exception e)
          {
            thistran.Rollback();
            sErrMsg += e.Message + (char)13 + (char)10;
            bl_final = false;
          }
          finally
          {
            tb_dbset.Dispose();
            da_ADP_dbset.Dispose();

            conni.Close();
            connr.Close();
          }

        }
        catch (Exception e)
        {
          sErrMsg += e.Message + (char)13 + (char)10;
        }
        finally
        {
          if (Conn_Excel.State == ConnectionState.Open)
          {
            Conn_Excel.Close();
          }
          //
          if (rd_Excel != null)
          {
            if (!rd_Excel.IsClosed)
            {
              rd_Excel.Close();
            }
          }
          //
          cmd_excel.Dispose();
          dbsetDao.Dispose();
          DataCheck.Dispose();
        }
        //
      }
      //
      return bl_final;
    }

    private void act_SUR_go(bool bl_commit)
    {

    }

    protected void bt_SERQ_Click(object sender, EventArgs e)
    {
        Response.Redirect("dx_dbset.aspx");
    }
  }
}