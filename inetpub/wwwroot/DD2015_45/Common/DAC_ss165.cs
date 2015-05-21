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
  public class DAC_ss165 : DAC
  {
    public DAC_ss165()
      : base()
    {
    }

    public DAC_ss165(OleDbConnection conn)
      : base(conn)
    {
    }
  }
}