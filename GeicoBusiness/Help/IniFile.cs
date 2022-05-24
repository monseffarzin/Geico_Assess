using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;



namespace GeicoBusiness.Help
{
  public class IniFile
  {
    // USAGE:
    //var MyIni = new ToolIni(@"C:\Users\U612702\source\repos\Settings.ini");
    //var De_En = new EncryptDecrypter();
    //MyIni.Write("DefaultVolume", De_En.encode("100"), "General");
    //    MyIni.Write("HomePage", "http://www.google.com");
    //    var DefaultVolume = De_En.decode(MyIni.Read("DefaultVolume", "General"));
    //    if (MyIni.KeyExists("DefaultVolume", "DefaultVolume"))
    //    {
    //        MyIni.DeleteKey("DefaultVolume", "DefaultVolume");
    //    }
    //MyIni.DeleteSection();
    string Path;
    string EXE = Assembly.GetExecutingAssembly().GetName().Name;

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    public IniFile(string IniPath = null)
    {
      Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
    }

    public string Read(string Key, string Section = null)
    {
      var RetVal = new StringBuilder(255);
      GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
      return RetVal.ToString();
    }

    public void Write(string Key, string Value, string Section = null)
    {
      try
      {
        WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
      }
      catch(Exception e)
      {
        HttpContext.Current.Session.Add("LastError", e.Message);
      }
    }

    public void DeleteKey(string Key, string Section = null)
    {
      Write(Key, null, Section ?? EXE);
    }

    public void DeleteSection(string Section = null)
    {
      Write(null, null, Section ?? EXE);
    }

    public bool KeyExists(string Key, string Section = null)
    {
      return Read(Key, Section).Length > 0;
    }
  }

}