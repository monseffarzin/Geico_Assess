using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using GeicoData.Models;
using Geico.Filters;
using GeicoBusiness.Help;

namespace Geico.Controllers
{
  [UserAuthenticationFilter]
  public class HomeController : BaseController
  {
    private GeicoEntities db = new GeicoEntities();
    public ActionResult Index()
    {

      return View();
    }
    public ActionResult IndexOLD()
    {
      Session["HeaderBaseType"] = 1;
      return View();
    }
    [HttpPost]
    public ActionResult logout()
    {
      var ret = true;
      Session["Authenticated"] = "false";
      Session["fullname"] = "";
      Session["userId"] = "";
      Session["IsAdmin"] = false;
      return Json(new { response = ret}, JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    public ActionResult login(string username, string password)
    {
      var user = db.Users.Where(u => u.FullName == username && u.pass == password).FirstOrDefault();
      var ret = false;
      var fullname = "";
      if (user == null)
      {
        Session["Authenticated"] = "false";
        Session["fullname"] = "";
        Session["userId"] = "";
        Session["IsAdmin"] = false;
      }
      else
      {
        var ini = new HelperEncodeDecodeIni();
        ini.iniWrite(Tools.pathSetting + @"Inventory.ini", "LastUser", user.FullName, "Main", true);
        Session["Authenticated"] = "true";
        Session["fullname"] = user.FullName;
        Session["userId"] = user.Id;
        Session["IsAdmin"] = (Tools.getValueFromCommand("typeno", "Select * from Users where Id=" + Convert.ToString((int)Session["userId"]), Tools.GetConnectionStringByProvider("InventoryConnectionString"))=="2");
        fullname = user.FullName;
        ret = true;
      }
      return Json(new { response = ret, username = fullname }, JsonRequestBehavior.AllowGet);
    }
    public ActionResult Access()
    {
      return View();
    }

    [HttpPost]
    public EmptyResult setSessionVar(string sessionVarValue)
    {
      string[] temp = sessionVarValue.ToLower().Split('|');
      try
      {
         Session[temp[0]] = temp[1];
        return new EmptyResult();
      }
      catch (Exception ex)
      {
        return new EmptyResult();
      }
    }
    [HttpPost]
    public ActionResult initiateUser()
    {
      return Json(new { response = true, data = Session["FullnameList"] }, JsonRequestBehavior.AllowGet);
    }
  }
}
