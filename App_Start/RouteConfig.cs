using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChatGptApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ChatRoute",
                url: "chat/chatgpt",
                defaults: new { controller = "ChatGpt", action = "DoChat", id = UrlParameter.Optional }
            );

            routes.MapRoute("ChatRouteSaveItem", "chat/chatgpt/saveitem", new
            {
                controller = "ChatGpt",
                action = "SaveItem"
            });

            routes.MapRoute("ChatRouteSaveImage", "chat/chatgpt/saveimage", new
            {
                controller = "ChatGpt",
                action = "SaveImage"
            });
        }
    }
}
