using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Data.OleDb;
using System.Threading;
using ADODB;
using YNetLib_45;

namespace DD2015_45
{
  public class YTableTr : IDisposable
  {
     public string st_accessTableName="";
     public string st_rpt_guid="";
     public string st_datapath=@"C:\inetpub\Sl2012\footwear\WebSN\ReportData\";
     public string st_rptpath=@"C:\inetpub\Sl2012\footwear\WebSN\Report\";
     public string st_access_ConnectionString = "";
     public string st_mdbfullname = "";
     //
     string st_crmdbfullname="";
     OleDbConnection ConnAccess ;
     //
     public YTableTr()
     {

     }

     public void SetConn()
     {
       st_mdbfullname = st_datapath + st_rpt_guid + ".mdb";
       st_crmdbfullname = st_rptpath + "CRMDB.mdb";
       st_access_ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + st_mdbfullname + ";Persist Security Info=False;";
       ConnAccess = new OleDbConnection();
       ConnAccess.ConnectionString = st_access_ConnectionString;
     }

     public bool TableTrAdo(DataTable tb_Tableo, string st_type, bool bl_SSTR, string st_tablename)  
     { bool ret=true;
       clsFN csFN = new clsFN();
       DataColumnCollection  da_DataColumno;
       da_DataColumno = tb_Tableo.Columns;

       //CREATE DATA MDB
       string st_cresql="";
       int in_dco,in_Colen;
       string st_ColNamo="";
       string st_ColTypo="";
       st_cresql = "CREATE TABLE " + st_accessTableName + " (";
       for (in_dco = 0; in_dco < da_DataColumno.Count; in_dco++)
       {
         st_ColNamo = da_DataColumno[in_dco].ColumnName;
         st_ColTypo = da_DataColumno[in_dco].DataType.ToString().ToUpper();
         in_Colen = da_DataColumno[in_dco].MaxLength;
         if (in_dco != 0) st_cresql += ",";
         if ( st_ColTypo == "SYSTEM.DATETIME")
         {
          st_ColTypo = "CHAR";
          in_Colen = 10;
          st_cresql +=  st_ColNamo + " " + st_ColTypo + "(" + in_Colen.ToString() + ")";
         }
         else if (st_ColTypo == "SYSTEM.STRING")
         {
           st_ColTypo = "NVARCHAR";
           if (bl_SSTR)
           {
            if ((in_Colen > 250) || (in_Colen <= 0))
            {
              in_Colen = 250;
            }
           }
          st_cresql += st_ColNamo + " " + st_ColTypo + "(" + in_Colen.ToString() + ")";
        }
        else if ( st_ColTypo == "SYSTEM.DECIMAL")
        {
          st_ColTypo = "FLOAT";
          st_cresql += st_ColNamo + " " + st_ColTypo + " ";
        }
        else if  (st_ColTypo == "SYSTEM.BYTE[]")
        {
           st_ColTypo = "LONGBINARY";  //OLE
          st_cresql += st_ColNamo + " " + st_ColTypo + " ";
        }
        else
        {
          st_ColTypo = "FLOAT";
          st_cresql += st_ColNamo + " " + st_ColTypo + " ";
        }
       }
       st_cresql += ")";
       if ( st_tablename == st_accessTableName)
       {
         CreateTableAccess(st_cresql);
       }
       else
       {
         CreateTableAccess(st_cresql, false);
       }
       //
       ADODB.Recordset ado_Rst= new ADODB.Recordset();
       ADODB.Command ado_Cmt = new ADODB.Command();
       ADODB.Connection ado_Conn= new ADODB.Connection();
       ado_Conn.ConnectionString = st_access_ConnectionString;
       ado_Conn.Open();
       ado_Cmt.ActiveConnection = ado_Conn;
       string st_rststring = "SELECT * FROM " + st_accessTableName + " ";
       DataRow Rowo;
       string st_ColValo="";
       ado_Rst.Open(st_rststring, ado_Conn, CursorTypeEnum.adOpenKeyset, LockTypeEnum.adLockOptimistic);
       for (int in_Rowo = 0; in_Rowo<=tb_Tableo.Rows.Count - 1;in_Rowo++)
       {
         ado_Rst.AddNew();
         Rowo = tb_Tableo.Rows[in_Rowo];
         for (int in_Colo = 0; in_Colo<=da_DataColumno.Count - 1; in_Colo++)
         {
            st_ColValo = Rowo[in_Colo].ToString().Trim();
            st_ColNamo = da_DataColumno[in_Colo].ColumnName;
            st_ColTypo = da_DataColumno[in_Colo].DataType.ToString().ToUpper();
            //
            if (st_ColTypo == "SYSTEM.DATETIME")
            {
              if ( Rowo[in_Colo]==DBNull.Value ) 
              {
                ado_Rst.Fields[in_Colo].Value = "";
              }
              else
              {
                if ( ((DateTime)Rowo[in_Colo]) <= (DateTime.Today.AddYears(-150)) )
                {
                  st_ColValo = "";
                }
                else if (st_type == "E")
                {
                  st_ColValo = csFN.datetostr(DAC.GetDateTimeValue(Rowo[in_Colo]));
                }
                else
                {
                  st_ColValo = csFN.datetostr(DAC.GetDateTimeValue(Rowo[in_Colo]));
                }
                ado_Rst.Fields[in_Colo].Value = st_ColValo;
              }
            }
            else 
            {
              st_ColTypo = st_ColTypo.ToUpper();
              if (st_ColTypo.IndexOf("INT") > -1) 
              {
                try
                {
                  st_ColValo =DAC.GetInt32Value(st_ColValo).ToString();
                }
                catch
                {
                  st_ColValo = "0";
                }
                ado_Rst.Fields[in_Colo].Value = st_ColValo;
              }
              else if ((st_ColTypo.IndexOf("DOUBLE") > -1) || (st_ColTypo.IndexOf("DECIMAL") > -1) || (st_ColTypo.IndexOf("CURRENCY") > -1) )
              {
                try
                {
                 st_ColValo = DAC.GetDecimalValue(st_ColValo).ToString();
                }
                catch
                {
                  st_ColValo = "0";
                }
  
               ado_Rst.Fields[in_Colo].Value = st_ColValo;
              }
              else
              {
               ado_Rst.Fields[in_Colo].Value = DAC.GetStringValue(Rowo[in_Colo]);
              }
            }
            //
         }
       }
       ado_Rst.Update();
       ado_Rst.Close();
       ado_Conn.Close();
       //
       csFN.Dispose();
       return ret;
     }

     public bool CreateTableAccess(string st_cresql,bool bl_delmdb)
     {
       return  CreateTableAccessGo(st_cresql, bl_delmdb);
     }

     public bool CreateTableAccess(string st_cresql)
     {
       return  CreateTableAccessGo( st_cresql, true);
     }  

     public bool CreateTableAccessGo(string st_cresql,bool bl_Delmdb)
     { 
       bool bl_crok=false;
  

       if ( File.Exists(st_mdbfullname+ ".MDB"))
       {
         if ( bl_Delmdb)
         {
           File.Delete(st_mdbfullname);
           File.Copy(st_crmdbfullname,st_mdbfullname, true);
         }
       }
       else
       {
         File.Copy(st_crmdbfullname,st_mdbfullname,true);
       }
       //
       OleDbCommand  CommandAccess=new OleDbCommand();
       ConnAccess.Open();
       CommandAccess.CommandText=st_cresql;
       CommandAccess.Connection=ConnAccess;
       CommandAccess.ExecuteNonQuery();
       ConnAccess.Close();
       CommandAccess.Dispose();
       bl_crok = true;
       return (bl_crok);
     }
     #region IDisposable 成員

     public void Dispose()
     {
       ConnAccess.Dispose();
     }

     #endregion
  }
}