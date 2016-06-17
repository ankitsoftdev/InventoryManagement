using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace InventoryManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "WebAPI/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            var formatters = GlobalConfiguration.Configuration.Formatters;

            formatters.Remove(formatters.XmlFormatter);
        }
    }
}
