using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementErp_Main.Helper
{
    public class AccountBaseController :Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            //String Tagname = "Account";
            //if (filterContext.HttpContext.Request.QueryString["Tagname"] != null)
            //    Tagname = filterContext.HttpContext.Request.QueryString["Tagname"].ToString();
            //String LayoutTag = Tagname;
            //ViewBag.Layout = "~/Areas/LandingDashBoard/Views/Shared/_ContentLayout.cshtml";


            //ViewBag.TagType = Request.QueryString["TagType"];
            if (Session["dashboard"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "LogIn", action = "FrontLogin", area = "" }));
            }
            else
            {
            var k = (HDMEntity.DashBoard)Session["dashboard"];
            ViewBag.Msg = k;
            ViewBag.LoginInfo = k;
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
        }

    }
}