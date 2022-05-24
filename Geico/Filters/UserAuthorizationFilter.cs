using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using Business;
//using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GeicoData.Models;

namespace Geico.Filters
{


    public class UserAuthorizationFilter : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;
        private GeicoEntities Db;
        //private readonly IBusUser _busUser;

        public UserAuthorizationFilter(params string[] roles)
        {
            this.allowedRoles = roles;
            Db = new GeicoEntities();
            //_busUser = new BusUser();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorized = false;
            string standardId = Convert.ToString(httpContext.User.Identity.Name).Split('\\').Last();
            //var user = _busUser.GetUserByStandardID(standardId);
            var user = Db.Users.Where(u => u.FullName == standardId).FirstOrDefault();
            if (user != null)
            {
                //if (allowedRoles.Contains(user.Role.RoleName))
                //{
                    authorized = true;
                //}
            }

            return authorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    {"controller", "Home" },
                    {"action", "Unauthorized" }
                });
        }
    }
}

