//using Business;
//using Data;
//using Entitites;

using GeicoBusiness.Bus;
using GeicoBusiness.Help;
using GeicoData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geico.Controllers
{
  public class BaseController : Controller
  {
    protected const string NoUserRole = "no user role available";
    protected override void OnException(ExceptionContext filterContext)
    {
      var busLog = new BusLog();
      busLog.logDB(filterContext, Tools.sessionInt(Session["userId"]), (string)Session["userEmail"]);
      Exception exception = filterContext.Exception;
      filterContext.ExceptionHandled = true;
      var Result = this.View("Error", new HandleErrorInfo(exception,
                              filterContext.RouteData.Values["controller"].ToString(),
                              filterContext.RouteData.Values["action"].ToString()));
      filterContext.Result = Result;
    }
  }
}

