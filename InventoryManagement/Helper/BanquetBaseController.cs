using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace HotelManagementErp_Main.Helper
{
    public class BanquetBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String Tagname = "Banquet";
            if (filterContext.HttpContext.Request.QueryString["Tagname"] != null)
                Tagname = filterContext.HttpContext.Request.QueryString["Tagname"].ToString();
            String LayoutTag = Tagname;
            ViewBag.Layout = ServiceLayer.DashBoardLayoutInformation.Layout_Info.GetDashBoardLayout(LayoutTag);
            ViewBag.Tagname = LayoutTag;

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