using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;


namespace YNetLib_45
{
  class clsDB : DAC
  {
    public clsDB()
      : base()
    {
    }
    public DataTable SelectClassesForDropDownList(string class_type, string orderBy)
    {
      OleDbCommand cmd = DAC.NewCommand();
      cmd.CommandText = "select class_type,class_value from classes with (nolock) ";
      if (orderBy != "") cmd.CommandText += " order by " + orderBy;
      return Select(cmd);
    }
  }
}
