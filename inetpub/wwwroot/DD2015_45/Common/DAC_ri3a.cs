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
  public class DAC_ri3a : DAC
  {
    private clsFN daoFN = new clsFN();


    public DAC_ri3a()
      : base()
    {
    }

    public DAC_ri3a(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE ri3a ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_ri3a(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNri3a", "ri3a", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    public DataTable SelectTableForTextEdit_ri3a(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNri3a", "ri3a", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }

    /// <summary>
    /// SELECT TABLE ri3a ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_ri3ba(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNri3a", "ri3a", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

  }
}