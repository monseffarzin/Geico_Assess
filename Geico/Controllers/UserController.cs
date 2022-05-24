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
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using GeicoBusiness.Help;
using GeicoBusiness.Bus;

namespace Geico.Controllers
{
  public class UserController : Controller
  {
    private GeicoEntities db = new GeicoEntities();

    public ActionResult Index(int? TransType)
    {
      return View();
    }

    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public ActionResult Users_Read([DataSourceRequest] DataSourceRequest request)
    {
      int code = 0;
      if (!String.IsNullOrEmpty((string)Session["HeaderCode"]))
      {
        code = Tools.StrToInt((string)Session["HeaderCode"], 0);
      }
      BusUser busUser = new BusUser();
      var res = busUser.read( (bool)Session["isAdmin"]? -1: (int)Session["userCounter"]);
      DataSourceResult result = res.ToDataSourceResult(request, u => new
      {
        typeno = u.typeno,
        USERNAME = u.USERNAME,
        pass = u.pass,
        FullName = u.FullName,
        Id= u.Id,
      }); 
      return this.Json(result, JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Users_Create([DataSourceRequest] DataSourceRequest request, User user)
    {
      if (ModelState.IsValid)
      {
        var busUser = new BusUser();
        user = busUser.Create(user);
        return Json(new[] { user }.ToDataSourceResult(request, ModelState));
      }
      else
      {
        throw new Exception(Tools.getModelErr(ModelState));
      }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Users_Update([DataSourceRequest] DataSourceRequest request, User user)
    {
      if (ModelState.IsValid)
      {
        var busUser = new BusUser();
        user = busUser.Update(user);
        return Json(new[] { user }.ToDataSourceResult(request, ModelState));
      }
      else
      {
        throw new Exception(Tools.getModelErr(ModelState));
      }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Users_Destroy([DataSourceRequest] DataSourceRequest request, User user)
    {
      var busUser = new BusUser();
      user = busUser.Destroy(user);
      return Json(new[] { user }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
    {
      var fileContents = Convert.FromBase64String(base64);
      return File(fileContents, contentType, fileName);
    }

    [HttpPost]
    public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
    {
      var fileContents = Convert.FromBase64String(base64);
      return File(fileContents, contentType, fileName);
    }

    protected override void Dispose(bool disposing)
    {
      db.Dispose();
      base.Dispose(disposing);
    }

  }
}
