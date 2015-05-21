using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YNetLib_45;

namespace DD2015_45.Forms.Dialog
{
  public partial class WinPicDialog : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      ig_Pic.ImageUrl = Server.HtmlDecode(DAC.GetStringValue(Request["Pic"]));
    }
  }
}