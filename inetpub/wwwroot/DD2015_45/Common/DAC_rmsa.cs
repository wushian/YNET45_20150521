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
  public class DAC_rmsa : DAC
  {
    private clsFN daoFN = new clsFN();
    public DAC_rmsa()
      : base()
    {
    }

    public DAC_rmsa(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE rmsa ,use COMMAND Adapter
    /// 必須自定義 daoConn及daocmd_WhereQuery
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_rmsa(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrmsa", "rmsa", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectTable_rmsa_ba(OleDbCommand WhereQuery, string st_addSelect, bool bl_lock, string st_addJoin, string st_addUnion, string st_orderKey)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrmsa", "rmsa", st_addSelect, bl_lock, st_addJoin, WhereQuery, st_addUnion, st_orderKey);
      return (Select(cmds));
    }

    ///
    public DataTable SelectTableForTextEdit_rmsa(OleDbCommand WhereQuery)
    {
      OleDbCommand cmds = daoFN.GetSelectCommand("YN01", "UNrmsa", "rmsa", "", false, "", WhereQuery, "", "");
      return (Select(cmds));
    }




  }
}