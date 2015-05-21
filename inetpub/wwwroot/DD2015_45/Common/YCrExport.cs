using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Threading;

namespace DD2015_45
{
  /// <summary>
  /// 將 Crystal Report 報表匯出成檔案或是匯出至 HttpResponse
  /// </summary>
  public class YCrExport
  {
    /// <summary>
    /// 增加一個報表參數值設定
    /// </summary>
    /// <param name="key">報表參數名稱</param>
    /// <param name="value">報表參數的值</param>
    public void AddParameter(string key, object value)
    {
      if (_parameters.Contains(key))
        _parameters[key] = value;
      else
        _parameters.Add(key, value);
    }

    /// <summary>
    /// 移除一個報表參數設定
    /// </summary>
    /// <param name="key">要移除的報表參數名稱</param>
    public void RemoveParameter(string key)
    {
      if (_parameters.Contains(key))
        _parameters.Remove(key);
    }

    /// <summary>
    /// 清除報表參數設定
    /// </summary>
    public void ClearParameters()
    {
      _parameters.Clear();
    }

    /// <summary>
    /// 載入報表格式檔，並設定資料來源，報表參數
    /// </summary>
    /// <param name="reportFileName">報表格式檔名稱</param>
    /// <param name="dataFileName">報表資料檔名稱</param>
    public void LoadReport(string reportFileName, string dataFileName)
    {
      _reportFileName = reportFileName;
      _dataFileName = dataFileName;

      // 載入報表格式檔
      CrDocument.Load(_reportFileName);

      // 設定資料來源
      if (CrDocument.DataSourceConnections.Count > 0)
      {
        // 主報表資料來源
        CrDocument.DataSourceConnections[0].SetConnection(_dataFileName,
          Path.GetFileName(CrDocument.DataSourceConnections[0].DatabaseName),
          true);

        // 子報表資料來源
        foreach (ReportDocument subreport in CrDocument.Subreports)
        {
          if (subreport.DataSourceConnections.Count > 0)
            subreport.DataSourceConnections[0].SetConnection(_dataFileName,
              Path.GetFileName(subreport.DataSourceConnections[0].DatabaseName),
              true);
        }
      }

      // 設定報表參數
      SetParamValue();
    }

    /// <summary>
    /// 關閉報表，以釋放報表檔案及資料檔案資源
    /// </summary>
    public void CloseReport()
    {
      CrDocument.Close();
      //CrDocument.Database.Tables.Dispose();
      //CrDocument.Database.Links.Dispose();
      //CrDocument.Database.Dispose();
      CrDocument.Dispose();
      CrDocument = null;
    }

    /// <summary>
    /// 存成 Word 檔
    /// </summary>
    /// <param name="outFileName">檔案名稱</param>
    public void SaveAsDoc(string outFileName)
    {
      CrDocument.ExportToDisk(ExportFormatType.WordForWindows, outFileName);
    }

    /// <summary>
    /// 存成 Excel 檔
    /// </summary>
    /// <param name="outFileName">檔案名稱</param>
    public void SaveAsXls(string outFileName)
    {
      CrDocument.ExportToDisk(ExportFormatType.Excel, outFileName);
    }

    /// <summary>
    /// 存成 PDF 檔
    /// </summary>
    /// <param name="outFileName">檔案名稱</param>
    public void SaveAsPdf(string outFileName)
    {
      CrDocument.ExportToDisk(ExportFormatType.PortableDocFormat, outFileName);
    }

    /// <summary>
    /// 存成 HTML 檔
    /// </summary>
    /// <param name="outFileName">檔案名稱</param>
    public void SaveAsHtml(string outFileName)
    {
      CrDocument.ExportToDisk(ExportFormatType.HTML40, outFileName);
    }

    /// <summary>
    /// 以 Word 格式匯出至 HttpResponse，讓 Web 使用者可以直接開啟或儲存檔案
    /// </summary>
    /// <param name="response">匯出目的的 HttpResponse 物件</param>
    /// <param name="outFileName">檔案名稱</param>
    public void WebExportAsDoc(HttpResponse response)
    {
      WebExportAsDoc(response, Path.ChangeExtension(Path.GetFileName(_reportFileName), null));
    }

    public void WebExportAsDoc(HttpResponse response, string outFileName)
    {
      CrDocument.ExportToHttpResponse(ExportFormatType.WordForWindows, response, true, outFileName);
    }

    /// <summary>
    /// 以 Excel 格式匯出至 HttpResponse，讓 Web 使用者可以直接開啟或儲存檔案
    /// </summary>
    /// <param name="response">匯出目的的 HttpResponse 物件</param>
    /// <param name="outFileName">檔案名稱</param>
    public void WebExportAsXls(HttpResponse response)
    {
      WebExportAsXls(response, Path.ChangeExtension(Path.GetFileName(_reportFileName), null));
    }

    public void WebExportAsXls(HttpResponse response, string outFileName)
    {
      CrDocument.ExportToHttpResponse(ExportFormatType.Excel, response, true, outFileName);
    }

    /// <summary>
    /// 以 PDF 格式匯出至 HttpResponse，讓 Web 使用者可以直接開啟或儲存檔案
    /// </summary>
    /// <param name="response">匯出目的的 HttpResponse 物件</param>
    /// <param name="outFileName">檔案名稱</param>
    public void WebExportAsPdf(HttpResponse response)
    {
      WebExportAsPdf(response, Path.ChangeExtension(Path.GetFileName(_reportFileName), null));
    }

    public void WebExportAsPdf(HttpResponse response, string outFileName)
    {
      CrDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, response, true, outFileName);
    }

    public MemoryStream ExportStreamAsPdf()
    {
      return CrDocument.ExportToStream(ExportFormatType.PortableDocFormat) as MemoryStream;
    }

    /// <summary>
    /// 報表物件，可作為 ReportViewer 的 ReportSource
    /// </summary>
    public ReportDocument Document
    {
      get
      {
        return CrDocument;
      }
    }

    #region 私用變數函數

    private string _reportFileName = "";
    private string _dataFileName = "";

    private SortedList _parameters = new SortedList();
    private ReportDocument CrDocument = new ReportDocument();

    /// <summary>
    /// 設定報表參數
    /// </summary>
    private void SetParamValue()
    {
      ParameterField paramField = null;
      string paramName = null;
      object paramValue = null;

      for (int i = 0; i < _parameters.Count; i++)
      {
        paramName = _parameters.GetKey(i).ToString();
        paramValue = _parameters[paramName];

        // 設定主報表參數
        paramField = CrDocument.ParameterFields.Find(paramName, "");
        if (paramField != null)
          CrDocument.SetParameterValue(paramName, paramValue);

        // 設定子報表參數
        foreach (ReportDocument subreport in CrDocument.Subreports)
        {
          paramField = CrDocument.ParameterFields.Find(paramName, subreport.Name);
          if (paramField != null)
            CrDocument.SetParameterValue(paramName, paramValue, subreport.Name);
        }
      }
    }

    #endregion
  }
}