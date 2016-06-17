using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelManagementErp_Main.Helper
{
    public class SubdomainRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
                          RouteDirection routeDirection)
        {
         
            String Company = "Fortune.Marketing.Pvt.Ltd";
            String UserName = "Ankit";
            String Role = "Administrator";
            //string url = httpContext.Request.Headers["HOST"];
            string url = httpContext.Request.CurrentExecutionFilePath;

            int index = url.IndexOf(".", System.StringComparison.Ordinal);

            if (index < 0)
            {
                return false;
            }

            string sub = url.Split('.')[0];
            if (sub == "www" || sub == "localhost" || sub == "mail" /* || sub = "some-blacklist-subdomain" */)
            {
                return false;
            }
               var k = (HDMEntity.DashBoard)httpContext.Session["dashboard"];
            //Add a custom parameter named "user". Anythink you like :)
               if (!values.Keys.Any(m => m.Contains("Domain")) && k != null)
               {
                 
                   values["Domain"]= k.HotelName;
                   values["User"] = k.Employee_Name;
                   values["Role"] = String.Concat<String>(k.Roles.ToList());
                 
               }
               else
               {
                   values.Remove("Domain");
                   values.Add("Domain", Company);
                   values.Remove("User");
                   values.Add("User", UserName);
                   values.Remove("Role");
                   values.Add("Role", Role);
               }
            return true;
        }
      
    }
    public class SubdomainRoute : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request == null || httpContext.Request.Url == null)
            {
                return null;
            }

            var host = httpContext.Request.Url.Host;
            var index = host.IndexOf(".");
            string[] segments = httpContext.Request.Url.PathAndQuery.TrimStart('/').Split('/');

            if (index < 0)
            {
                return null;
            }

            var subdomain = host.Substring(0, index);
            string[] blacklist = { "www", "yourdomain", "mail" };

            if (blacklist.Contains(subdomain))
            {
                return null;
            }

            string controller = (segments.Length > 0) ? segments[0] : "Home";
            string action = (segments.Length > 1) ? segments[1] : "Index";

            var routeData = new RouteData(this, new MvcRouteHandler());
            routeData.Values.Add("controller", controller); //Goes to the relevant Controller  class
            routeData.Values.Add("action", action); //Goes to the relevant action method on the specified Controller
            routeData.Values.Add("subdomain", subdomain); //pass subdomain as argument to action method
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }
    }
}