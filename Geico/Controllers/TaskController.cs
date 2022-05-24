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
  public class TaskController : Controller
  {
    private GeicoEntities db = new GeicoEntities();

    public ActionResult Index(int? TransType)
    {
      return View();
    }

    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public ActionResult Tasks_Read([DataSourceRequest] DataSourceRequest request)
    {
      int code = 0;
      if (!String.IsNullOrEmpty((string)Session["HeaderCode"]))
      {
        code = Tools.StrToInt((string)Session["HeaderCode"], 0);
      }
      BusTask busTask = new BusTask();
      var res = busTask.read(code);
      DataSourceResult result = res.ToDataSourceResult(request, Task => new
      {
        Id = Task.Id,
        Name = Task.Name,
        Description = Task.Description,
        Due = Task.Due,
        Start = Task.Start,
        End = Task.End,
        Priority = Task.Priority,
        Status = Task.Status,
      }); 
      return this.Json(result, JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Tasks_Create([DataSourceRequest] DataSourceRequest request, Task Task)
    {
      if (ModelState.IsValid)
      {
        var busTask = new BusTask();
        Task = busTask.Create(Task);
        return Json(new[] { Task }.ToDataSourceResult(request, ModelState));
      }
      else
      {
        throw new Exception(Tools.getModelErr(ModelState));
      }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Tasks_Update([DataSourceRequest] DataSourceRequest request, Task Task)
    {
      if (ModelState.IsValid)
      {
        var busTask = new BusTask();
        Task = busTask.Update(Task);
        return Json(new[] { Task }.ToDataSourceResult(request, ModelState));
      }
      else
      {
        throw new Exception(Tools.getModelErr(ModelState));
      }
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Tasks_Destroy([DataSourceRequest] DataSourceRequest request, Task Task)
    {
      var busTask = new BusTask();
      Task = busTask.Destroy(Task);
      return Json(new[] { Task }.ToDataSourceResult(request, ModelState));
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
    [HttpPost]
    public ActionResult checkPriorityTasks()
    {
      var busTask = new BusTask();
      var result = busTask.checkPriorityTasks();
      Session["PriorityTaks"] = result;
      return Json(new[] { result = result }, JsonRequestBehavior.AllowGet);
      //return new EmptyResult();
    }

    protected override void Dispose(bool disposing)
    {
      db.Dispose();
      base.Dispose(disposing);
    }
    

    //public ActionResult winTaskFindContent()
    //{
    //    ViewBag.model = db.vTasks.Where(g => g.Code == "@#$&").FirstOrDefault();
    //    return PartialView(@"_EditTask");
    //}

  }
}
