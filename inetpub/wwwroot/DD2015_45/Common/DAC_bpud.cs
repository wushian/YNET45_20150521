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
  public class DAC_bpud : DAC
  {
    public DAC_bpud()
      : base()
    {
    }

    public DAC_bpud(OleDbConnection conn)
      : base(conn)
    {
    }

  }
}