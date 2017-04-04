using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BoltAdmin_MVC.Models
{
    public class ValidateLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (Convert.ToString(session["UserEmail"]) !="")
                return;

            //Redirect to login.
            var redirectTarget = new RouteValueDictionary { { "action", "Login" }, { "controller", "Admin" } };
            filterContext.Result = new RedirectToRouteResult(redirectTarget);
        }
    }
}