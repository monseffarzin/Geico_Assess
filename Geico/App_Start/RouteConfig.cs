using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Geico
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          //defaults: new { controller = "AA", action = "Index", id = UrlParameter.Optional }
          //defaults: new { controller = "Header", action = "Index", id = UrlParameter.Optional }
          defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          //defaults: new { controller = "Report", action = "Test", id = UrlParameter.Optional }
      );
    }
  }
}
