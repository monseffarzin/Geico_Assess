using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace GeicoBusiness.Help
{
  public class HelperEncodeDecodeIni
  //Encrypt & Decrypt Usage
  //public class EncryptDecrypter
  {
    public HelperEncodeDecodeIni()
    {
    }
    public string encode(string dataText)
    {
      //first get the bytes for the original
      byte[] data = System.Text.UnicodeEncoding.UTF8.GetBytes(dataText);
      Encoder myEncoder = new Encoder(data);
      StringBuilder sb = new StringBuilder();
      sb.Append(myEncoder.GetEncoded());

      return sb.ToString();
    }
    public string decode(string dataText)
    {
      char[] data = dataText.ToCharArray();
      Decoder myDecoder = new Decoder(data);
      StringBuilder sb = new StringBuilder();
      byte[] temp = myDecoder.GetDecoded();
      sb.Append(System.Text.UTF8Encoding.UTF8.GetChars(temp));

      return sb.ToString();
    }
    public bool iniWrite(string file, string Key, string value, string section, bool encodeSw=false)
    {
      var MyIni = new IniFile(file);  // @"C:\work\Settings.ini");
      if (encodeSw) { value = encode(value);  }
      MyIni.Write(Key, value, section);// "LastUser1", encode("Manager"));

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
      return true;
    }
    public string iniRead(string file, string Key,string section, bool decodeSw=false)
    {
      string result = "";
      var MyIni = new IniFile(file);
      result = MyIni.Read(Key, section);
      if (decodeSw) { result = decode(result); }
      return result;
    }
  }
}