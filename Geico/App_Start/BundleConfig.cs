using System.Web;
using System.Web.Optimization;

namespace Geico
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js"
                  , "~/Scripts/Kendo-2020.2.617/jquery-{version}.js"
                  , "~/Scripts/Kendo-2020.2.617/jquery.min.js"
                  , "~/Scripts/Kendo-2020.2.617/jszip.min.js"
                  , "~/Scripts/Kendo-2020.2.617/kendo.all.min.js"
                  , "~/Scripts/Kendo-2020.2.617/kendo.aspnetmvc.min.js"
                  , "~/Scripts/jquery.min.js"
                  , "~/Scripts/all.js"
                  , "~/Scripts/farzin1.js"
                  , "~/Scripts/PersianDatePicker.js"
                  //,"~/Scripts/bootstrap.min.js"
                  , "~/Scripts/respond.js"
                  ));

      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"
                  ));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.validate*"
                  , "~/Scripts/jquery.validate.min.js"
                  , "~/Scripts/jquery.validate.unobtrusive.min.js"
                  , "~/Scripts/jquery.unobtrusive-ajax.min.js"
                  , "~/Scripts/jquery.validate.unobtrusive.bootstrap.min.js"
                ));

      //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
      //            "~/Scripts/bootstrap.min.js"
      //            , "~/Scripts/respond.js"
      //          ));            
      //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
      //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
      bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css"
                     //, "~/Content/all.css"
                     , "~/Content/Kendo-2020.2.617/kendo.bootstrap-v4.min.css"
                     , "~/Content/Kendo-2020.2.617/kendo.bootstrap.min.css"
                     ));
      bundles.Add(new StyleBundle("~/Content/site").Include(
               //"~/Content/Site1$.css"
               //"~/Content/Site$.css",
               //"~/Content/grid$.css",
               //"~/Content/Site1.css",
               //"~/Content/Site.css",
               "~/Content/grid.css",
               "~/Content/persianDatepicker-default.css"
               //, "~/Content/persianDatepicker-dark.css"
               ));
    }
  }
}
