using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Timers;
using System.IO;

namespace YNetLib_45
{
  /// <summary>
  /// 檔案定時清除服務
  /// </summary>
  public class WDeleteFileService
  {
    private WDeleteFileService()
    {
      _deleteFilePatterns = new List<string>();
      _timer = new Timer();
      _timer.Enabled = false;
      _timer.Elapsed += new ElapsedEventHandler(DoDeleteFile);
    }

    public static WDeleteFileService GetInstance()
    {
      if (_instance == null)
        _instance = new WDeleteFileService();
      return _instance;
    }

    /// <summary>
    /// 檔案刪除間隔時間，單位為秒。預設值為 600 秒
    /// </summary>
    public int Interval
    {
      get
      {
        return _interval;
      }
      set
      {
        _interval = value;
      }
    }

    /// <summary>
    /// 要刪除多久之前的檔案，單位為秒。預設值為 600 秒
    /// </summary>
    public int FileAgeToDelete
    {
      get
      {
        return Convert.ToInt32(_fileAge.TotalSeconds);
      }
      set
      {
        _fileAge = new TimeSpan(value * TimeSpan.TicksPerSecond);
      }
    }

    /// <summary>
    /// 設定或取得檔案定時清除服務是否啟動
    /// </summary>
    public bool Enabled
    {
      get
      {
        return _enabled;
      }
      set
      {
        if (value)
        {
          if (_enabled)
            return;

          _timer.Interval = _interval * 1000;
          _timer.AutoReset = true;
          _timer.Enabled = true;
          _enabled = true;
        }
        else
        {
          if (!_enabled)
            return;

          _enabled = false;
          _timer.Enabled = false;
        }
      }
    }

    /// <summary>
    /// <para>欲清除的檔案路徑及檔案名稱，檔案名稱可以使用萬用字元</para>
    /// <para>需要多個檔案路徑及檔案名稱時，請將所需的檔案路徑及檔案名稱一一加入</para>
    /// <para>例如：c:\temp\*.mdb 表示要清除 c:\temp 目錄下的 *.mdb</para>
    /// </summary>
    public List<string> DeletePatterns
    {
      get
      {
        return _deleteFilePatterns;
      }
    }

    /// <summary>
    /// 實際執行刪除的動作
    /// </summary>
    private void DoDeleteFile(object sender, ElapsedEventArgs e)
    {
      if (!_enabled)
      {
        _timer.Enabled = false;
        return;
      }

      _timer.Enabled = false;
      try
      {
        foreach (string pattern in _deleteFilePatterns)
        {
          string namepart = Path.GetFileName(pattern);
          string pathPart = pattern.Substring(0, pattern.Length - namepart.Length);
          try
          {
            DirectoryInfo dirInfo = new DirectoryInfo(pathPart);
            FileInfo[] matchFiles = dirInfo.GetFiles(namepart);
            foreach (FileInfo file in matchFiles)
            {
              try
              {
                if (DateTime.Now - file.LastWriteTime > _fileAge)
                  file.Delete();
              }
              catch
              {
                // do nothing
              }
            }
          }
          catch
          {
            // do nothing
          }
        }
      }
      finally
      {
        _timer.Enabled = true;
      }
    }

    private static WDeleteFileService _instance = null;
    private List<string> _deleteFilePatterns = null;
    private Timer _timer = null;
    private int _interval = 600;
    private TimeSpan _fileAge = new TimeSpan(TimeSpan.TicksPerSecond * 600);
    private bool _enabled = false;

    #region UNIT TEST
    public static void Main()
    {
      WDeleteFileService delSrv = WDeleteFileService.GetInstance();
      delSrv.DeletePatterns.Add(@"c:\temp\*.*");
      delSrv.Interval = 5;
      delSrv.FileAgeToDelete = 1;
      delSrv.Enabled = true;
      Console.WriteLine("Press ENTER to end program");
      Console.ReadLine();
      delSrv.Enabled = false;
    }
    #endregion
  }
}
