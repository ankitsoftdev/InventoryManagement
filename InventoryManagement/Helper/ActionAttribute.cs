using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementErp_Main.Helper
{
    public class ValidRequestFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            if (filterContext.HttpContext.Session["dashboard"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "LogIn", action = "FrontLogin", area = "" }));
            }
            else
            {
                
                var k = (HDMEntity.DashBoard)filterContext.HttpContext.Session["dashboard"];
               filterContext.Controller.ViewBag.Msg = k;
                filterContext.Controller.ViewBag.LoginInfo = k;
            }

            base.OnActionExecuting(filterContext);
   
        }
        public class NoCacheAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.SetNoStore();

                base.OnResultExecuting(filterContext);
            }
        }
    }
}