using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayerLog;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
namespace HotelManagementErp_Main.Helper
{
    /// <summary>
    /// Base Controller 
    /// </summary>
    public class MinibarBaseController : Controller
    {
        /// <summary>
        /// Controller Data Boinding
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String Tagname = "MiniBar";
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