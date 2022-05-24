using GeicoBusiness.Bus;
using GeicoBusiness.Help;
using GeicoData.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Geico
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
    protected void Session_Start()
    {
      Session["Authenticated"] = "false";
      Session["GoodFind"] = "false";
      System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
      var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
      Session["AppVersion"] = fieVersionInfo.FileVersion;
      Session["HeaderCode"] = "";
      var ini = new HelperEncodeDecodeIni();
      Session["LastUser"] = ini.iniRead(Tools.pathSetting + @"Inventory.ini", "LastUser", "Main", true);

      GeicoEntities db = new GeicoEntities();
      string[] fullnames = new string[] { };
      List<User> users = db.Users.ToList();
      var usersArray = users.ToArray();
      Session["UserList"] = users;
      for (int i = 0; i<usersArray.Length; i++)
      {
        Array.Resize(ref fullnames, fullnames.Length + 1);
        fullnames[fullnames.Length - 1] = usersArray[i].FullName;
      }
      Session["FullnameList"] = fullnames;
      initiate();
    }
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    protected void initiate()
    {
      GeicoEntities db = new GeicoEntities();

      var busType = new BusType();
      Session["Priority"] = busType.getTypeList(1);
      Session["Status"] = busType.getTypeList(2);
      Session["priorityHighId"] = db.Types.Where(t => t.Name.ToLower() == "high" && t.TypeNo == 1 && t.Control == 1).FirstOrDefault().Id;
      Session["priorityHighLimit"] = 1; // set here limit of high priority tasks
    }

    void Application_Error(object sender, EventArgs e)
    {
      Exception err = Server.GetLastError();
      HttpContext.Current.Session.Add("LastError", err.Message);
    }
  }
}
