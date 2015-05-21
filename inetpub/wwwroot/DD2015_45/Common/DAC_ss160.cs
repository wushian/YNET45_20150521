using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.ComponentModel;
using YNetLib_45;

namespace DD2015_45 
{
  [DataObject]
  public class DAC_ss160 : DAC
  {

    public DAC_ss160()
      : base()
    {
    }

    public DAC_ss160(OleDbConnection conn)
      : base(conn)
    {
    }

    /// <summary>
    /// SELECT TABLE 資料
    /// </summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public DataTable SelectDataTable(string st_addSelect, string st_addJoin, bool bl_lock, OleDbCommand CmdQuery, string st_OrderBy)
    {
      return (SelectTableWhereKeyByOrder("YN01", "UNsys_ss160", "sys_ss160", st_addSelect, st_addJoin, CmdQuery, st_OrderBy));
    }
  }

}