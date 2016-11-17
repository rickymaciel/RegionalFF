﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RegionalFF
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            if (HttpContext.Current.User == null || HttpContext.Current.Request.Cookies.AllKeys.Contains("GlobalRegionalFF") == false || HttpContext.Current.Request.Cookies["GlobalRegionalFF"] == null)
            {
                routes.MapRoute(
                  name: "Default",
                  url: "{controller}/{action}/{id}",
                  defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
                    );
            }
            else
            {
                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }
}
