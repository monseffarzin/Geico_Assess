using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Geico.Helper
{
  [Serializable]
  public class PrinterAssembleInfo
  {
    public PrinterAssembleInfo()
    {
      this.IsRun = false;
      this.IsRunView = IsRun ? "已开启" : "未开启";
      this.Status = "空";
      this.ControlView = "Run";
      this.Note = new Dictionary<DateTime, string>();
      this.isNoteFormShow = false;
    }
    public string Id { get; set; }

    //نام مستعار چاپگر
    public string PrinterAlias { get; set; }

    //نام چاپگر انتخاب شده
    public string PrinterName { get; set; }

    //نام جعبه انتخاب شده برای این چاپگر
    public string PrinterBoxName { get; set; }

    //نام گزارش
    public string ReportName { get; set; }

    //اجرا شود
    public bool IsRun { get; set; }

    //شروع پایان
    public string IsRunView { get; set; }

    //مثال وضعیت فعلی: 13/20
    public string Status { get; set; }

    //نمایش دکمه سوئیچ Run/Stop/Wait
    public string ControlView { get; set; }

    //جزئیات
    public Dictionary<DateTime, string> Note { get; set; }

    //آیا پنجره ثبت جزئیات باز است یا خیر
    public bool isNoteFormShow { get; set; }

    public PrinterAssembleInfo Clone()
    {
      MemoryStream stream = new MemoryStream();
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(stream, this);
      stream.Position = 0;
      return formatter.Deserialize(stream) as PrinterAssembleInfo;
    }
  }

  public static class ModelExt
  {
    public static void AddNote(this PrinterAssembleInfo info, string str)
    {
      if (info != null)
      {
        info.Note.Add(DateTime.Now, str);
        Ext.Delay(1);//1 میلی ثانیه استراحت
      }
    }
  }
}
