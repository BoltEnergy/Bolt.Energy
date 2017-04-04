using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BoltAdmin_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UserAccount_edit",
                url: "UserAccount/{_id}",
                defaults: new { controller = "Admin", action = "Edit", _id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{_id}",
                defaults: new { controller = "Admin", action = "login", _id = UrlParameter.Optional }
            );
        }
    }
}