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
  public class DAC_common : DAC
  {
    private clsFN daoFN = new clsFN();

    public DAC_common()
      : base()
    {
    }

    public DAC_common(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE ,use COMMAND Adapter
    /// 必須自定義 Connr及WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectDropsownFromTable_pdbpuni(OleDbCommand WhereQuery, string st_orderKey)
    {
      OleDbConnection Connr = DAC.NewReaderConnr();
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNpdbpuni", "pdbpuni", "", false, "", WhereQuery, "", st_orderKey);
      return (Select(Connr, cmds));
    }

    /// <summary>
    /// SELECT TABLE ,use COMMAND Adapter
    /// 必須自定義 Connr及WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectDropsownFromTable_pdbplab(OleDbCommand WhereQuery, string st_orderKey)
    {
      OleDbConnection Connr = DAC.NewReaderConnr();
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNpdbplab", "pdbplab", "", false, "", WhereQuery, "", st_orderKey);
      return (Select(Connr, cmds));
    }
  }
}