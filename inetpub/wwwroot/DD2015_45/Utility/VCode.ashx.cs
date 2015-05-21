using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace DD2015_45.Utility
{
  /// <summary>
  /// VCode 的摘要描述
  /// </summary>
  public class VCode : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      string Text = context.Request["T"];
      if (Text.Length < 4)
        Text = (Text + "0000").Substring(0, 4);

      System.Drawing.Image img = System.Drawing.Image.FromFile(context.Server.MapPath("~/Picture/VCodePaper.dib"));
      Font f = new Font("Times New Roman", 18, FontStyle.Bold);
      Graphics g = Graphics.FromImage(img);

      g.DrawString(Text.Substring(0, 1), f, Brushes.Red, new PointF(0, 0));
      g.DrawString(Text.Substring(1, 1), f, Brushes.Blue, new PointF(20, 0));
      g.DrawString(Text.Substring(2, 1), f, Brushes.Green, new PointF(40, 0));
      g.DrawString(Text.Substring(3, 1), f, Brushes.Yellow, new PointF(60, 0));

      MemoryStream result = new MemoryStream();
      img.Save(result, System.Drawing.Imaging.ImageFormat.Png);
      img.Dispose();
      g.Dispose();

      context.Response.ContentType = "image/gif";
      context.Response.BinaryWrite(result.ToArray());
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }

    public static string GetStringValue(object o)
    {
      return (o != DBNull.Value && o != null) ? o.ToString() : "";
    }

  }
}