using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Linq;
using System.Configuration;

namespace GeicoBusiness.Help
{
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //////////////////////////           TOOL Class             /////////////////////////////////////////////////////////////////////
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  public static class Tools
  {
    public static char delimiterFile { get; set; } = '`';
    public static char delimiterSignature { get; set; } = '~';
    private static string _filePrefix = "1000000000" + delimiterFile;
    private static string _signaturePrefix = "1000000000" + delimiterSignature;
    public static int inoutBaseType = 1;
    public static bool inoutLabeling = false;
    public static bool swReportPrepared = false;
    public static int  barcodeLength = 11;
    public static string pathSetting = @"C:\work\";
    public static string getModelErr(System.Web.Mvc.ModelStateDictionary ModelState)
    {
      string result = "";
      var ModelStateArray = ModelState.ToArray();
      for (int i = 0; i < ModelStateArray.Length; i++)
      {
        if (ModelStateArray[i].Value.Errors.Count > 0)
        {
          result += (result != "" ? ";  " : "") + ModelStateArray[i].Value.Errors[0].ErrorMessage;
        }
      }
      return result;
    }

    public static Dictionary<string, string> GetMimeTypes()
    {
      return new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheet.sheet" }
        };
    }


    public static byte[] readFileToArray(string varFilePath)//, char delimiter)
    {
      byte[] file = new byte[] { };
      //string[] justFile = new string[] { };
      if (!string.IsNullOrEmpty(varFilePath))
      {
        //justFile = varFilePath.ToString().Split(delimiter);
        using (var stream = new FileStream(varFilePath, FileMode.Open, FileAccess.Read))
        {
          using (var reader = new BinaryReader(stream))
          {
            file = reader.ReadBytes((int)stream.Length);
          }
        }
      }
      return file;
    }

    public static string GetBase64ForImageMemory(byte[] imageBytes)//string imgPath)
    {
      //byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
      string base64String = Convert.ToBase64String(imageBytes);
      return base64String;
    }
    public static string GetBase64ForImage(string imgPath)
    {
      byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
      string base64String = Convert.ToBase64String(imageBytes);
      return base64String;
    }
    public static int sessionInt(object sessionVar)
    {
      int ret = 0;
      try
      {
        if (!int.TryParse(Convert.ToString(sessionVar ?? 0), out ret))
        {
          throw new System.ArgumentException("Session Variable was not correct");
        }
      }
      catch (Exception ex)
      {
        throw ex; 
      }
      return ret;
    }
    public static string filePrefix
    {
      get { return _filePrefix; }
      set { _filePrefix = value; }
    }
    public static string signaturePrefix
    {
      get { return _signaturePrefix; }
      set { _signaturePrefix = value; }
    }
    public static short getFractionDigitNumber(string No)
    {
      string tempNo = No.ToString();
      string fraction = "";
      if (tempNo.IndexOf(".") >= 0)
      {
        fraction = tempNo.Split('.')[1];
      }
      return (short)fraction.Length;
    }

    public static string Base64Encode(string plainText)
    {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText ?? "");
      return System.Convert.ToBase64String(plainTextBytes);
    }
    public static string Base64Decode(string base64EncodedData)
    {
      var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData ?? "");
      return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool CreateCommand(string queryString, string connectionString)
    {
      bool response = false;
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          SqlCommand command = new SqlCommand(queryString, connection);
          command.Connection.Open();
          command.ExecuteNonQuery();
          response = true;
        }
      }
      catch (Exception e)
      {
        consoleLog(e);
        //Utils.Errorlog("Applicaiton Error", "", ex, 0);
        //Server.ClearError();
      }
      return response;
    }
    public static string getValueFromCommand(string fldName,string queryString, string connectionString)
    {
      try
      {
        using (var connection = new SqlConnection(connectionString)) 
        using (var command = new SqlCommand(queryString, connection))
        {
          connection.Open();

          using (var reader = command.ExecuteReader())
          {
            if (reader.Read()) // Don't assume we have any rows.
            {
              int ord = reader.GetOrdinal(fldName);
              return Convert.ToString(reader.GetValue(ord)); 
            }
          }
        }
      }
      catch (Exception e)
      {
        consoleLog(e);
        //Utils.Errorlog("Applicaiton Error", "", ex, 0);
        //Server.ClearError();
      }
      return null;
    }
    public static void AppendFile(string data,
            string fileNamePath = @"Inventory.Log")
    {
      try
      {
        File.AppendAllText(Tools.pathSetting + fileNamePath, data);
      }
      catch (Exception e)
      {
        consoleLog(e);
      }
    }
    public static void writeExe()
    {
      try
      {
        Trace.WriteLine(
          System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        Trace.WriteLine(
          System.Reflection.Assembly.GetExecutingAssembly().Location);
        Trace.WriteLine(
          System.Reflection.Assembly.GetCallingAssembly().Location);
      }
      catch (Exception e)
      {
        consoleLog(e);
      }
    }
    public static void consoleLog(Exception ex)
    {
      Console.WriteLine("\nMessage ---\n{0}", ex.Message);
      Console.WriteLine(
          "\nHelpLink ---\n{0}", ex.HelpLink);
      Console.WriteLine("\nSource ---\n{0}", ex.Source);
      Console.WriteLine(
          "\nStackTrace ---\n{0}", ex.StackTrace);
      Console.WriteLine(
          "\nTargetSite ---\n{0}", ex.TargetSite);
    }
    public static string renameFileAutomatic(string filefullName)
    {
      string newFullName = "";
      if (!Path.IsPathRooted(filefullName))
        filefullName = Path.GetFullPath(filefullName);
      if (File.Exists(filefullName))
      {
        String filename = Path.GetFileName(filefullName);
        String path = filefullName.Substring(0, filefullName.Length - filename.Length);
        String filenameWOExt = Path.GetFileNameWithoutExtension(filefullName);
        String ext = Path.GetExtension(filefullName);
        int n = 1;
        do
        {
          newFullName = Path.Combine(path, String.Format("{0} ({1}){2}", filenameWOExt, (n++), ext));
        }
        while (File.Exists(newFullName));
        File.Move(filefullName, newFullName);
      }
      return newFullName;
    }
    public static bool checkExistFile(string filefullName, int maxWaitTime = 0)
    {
      bool result = false;
      DateTime firstDate = DateTime.Now;
      TimeSpan dateDifference;
      do
      {
        DateTime secondDate = DateTime.Now;
        dateDifference = secondDate.Subtract(firstDate);
        if (File.Exists(filefullName)) { result = true; }
      }
      while ((!File.Exists(filefullName)) && (dateDifference.TotalMilliseconds < maxWaitTime));
      return result;
    }

    public static void correctFile(string filefullName, bool waitForNewFile = false)
    {
      int maxWaitTime = 10000;
      DateTime firstDate = DateTime.Now;
      TimeSpan dateDifference;
      do
      {
        renameFileAutomatic(filefullName);
        DateTime secondDate = DateTime.Now;
        dateDifference = secondDate.Subtract(firstDate);
      }
      while ((File.Exists(filefullName) && (dateDifference.TotalMilliseconds <= maxWaitTime)));
      if (waitForNewFile) { checkExistFile(filefullName, maxWaitTime); }
    }
    public static bool runBatch(string batDir, string batchFileName)
    {
      bool result = false;
      Process proc = null;
      try
      {
        proc = new Process();
        proc.StartInfo.WorkingDirectory = batDir;
        proc.StartInfo.FileName = batchFileName; 
        proc.StartInfo.CreateNoWindow = false;
        proc.Start();
        proc.WaitForExit();
        result = true;
      }
      catch (Exception ex)
      {
        AppendFile(ex.StackTrace.ToString());
      }
      return result;
    }

    public static int StrToIntDef(string s, int @default)
    {
      int number;
      if (int.TryParse(s, out number))
        return number;
      return @default;
    }
    public static string GetConnectionStringByProvider(string providerName)
    {
      string returnValue = null;
      ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
      if (settings != null)
      {
        foreach (ConnectionStringSettings cs in settings)
        {
          if (cs.Name == providerName)
          {
            returnValue = cs.ConnectionString;
            break;
          }
        }
      }
      return returnValue;
    }

    public static int StrToInt(string value, int defaultValue)
    {
      int number = defaultValue;
      try
      {
        if (int.TryParse(value, out number))
          Console.WriteLine("'{0}' --> {1}", value, number);
        else
          Console.WriteLine("Unable to parse '{0}'.", value);
      }
      catch (Exception e)
      {

      }
      return number;
    }

    public static bool backupFile(string sourceFile)
    {
      bool result = false;
      try
      {
        System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
        if (fi.Exists)
        {
          string fullPath_ = Path.GetFullPath(sourceFile);
          string fullPath = Path.GetDirectoryName(sourceFile);
          string filename = Path.GetFileName(sourceFile);
          if (!Directory.Exists(fullPath + "/bak"))
          {
            DirectoryInfo di = Directory.CreateDirectory(fullPath + "/bak");
          }
          DateTime created = fi.CreationTime;
          if (File.Exists(fullPath + "/bak/" + filename + created.ToString(" yyyy-MM-dd HH-mm-ss")))
          {
            renameFileAutomatic(fullPath + "/bak/" + filename + created.ToString(" yyyy-MM-dd HH-mm-ss"));
          }
          fi.MoveTo(fullPath + "/bak/" + filename + created.ToString(" yyyy-MM-dd HH-mm-ss"));
          Console.WriteLine("File Renamed.");
          result = true;
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("The process failed: {0}", e.ToString());
      }
      return result;
    }
  }
}