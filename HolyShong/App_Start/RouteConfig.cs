﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HolyShong
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //首頁
            //routes.MapRoute(
            //    name: "Home",
            //    url: "",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

            //主分類(StoreCategory導向頁面)(範例路由3)
            routes.MapRoute(
               name: "StoreCategory",
               url: "StoreCategory/{StoreCategoryName}",
               defaults: new { controller = "Home", action = "Search", StoreCategoryName="台灣美食" }
            );



            //預設
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
