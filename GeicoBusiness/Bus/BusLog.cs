using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeicoData.Models;
using GeicoBusiness.Help;

namespace GeicoBusiness.Bus
{
  public class BusLog
  {
    public void logDB(ExceptionContext filterContext, int userId, string userEmail)
    {
      var objLogDB = new LogDB()
      {
        Date_Time = DateTime.Now,
        UserName = userId.ToString() + " | " + userEmail, 
        Controller = filterContext.RouteData.Values["controller"].ToString(),
        Action = filterContext.RouteData.Values["action"].ToString(),
        Message = Convert.ToString(filterContext.Exception),
        Comment = "BaseController::logDB"
      };
      LogDBData logDBData = new LogDBData();
      logDBData.AddLogDB(objLogDB);
    }
  }
}