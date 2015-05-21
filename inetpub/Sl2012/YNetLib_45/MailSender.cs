using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Timers;

namespace YNetLib_45
{
  public class WMailSender
  {
    private static WMailSender _instance = null;
    private Queue<MailMessage> _queue = null;
    private bool _enabled = false;
    private int _interval = 30;
    private Timer _timer = null;
    private string _host = "";

    private WMailSender()
    {
      _queue = new Queue<MailMessage>();

      _timer = new Timer();
      _timer.Enabled = false;
      _timer.Elapsed += new ElapsedEventHandler(SendMailLoop);
    }

    public static WMailSender GetInstance()
    {
      if (_instance == null)
        _instance = new WMailSender();
      return _instance;
    }

    public string Host
    {
      set
      {
        _host = value;
      }
      get
      {
        return _host;
      }
    }

    public void Send(MailMessage message)
    {
      _queue.Enqueue(message);
    }

    private void SendMailLoop(object sender, ElapsedEventArgs e)
    {
      if (!_enabled)
      {
        _timer.Enabled = false;
        return;
      }

      _timer.Enabled = false;

      SmtpClient _client = new SmtpClient();
      //_client.DeliveryMethod = SmtpDeliveryMethod.Network;

      try
      {
        while (_queue.Count > 0)
        {
          MailMessage message = _queue.Dequeue();
          try
          {
            _client.Send(message);
          }
          catch
          {
            // 如果傳送失敗，需重傳
            _queue.Enqueue(message);
          }
        }
      }
      finally
      {
        _timer.Enabled = true;
      }
    }

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
  }
}
