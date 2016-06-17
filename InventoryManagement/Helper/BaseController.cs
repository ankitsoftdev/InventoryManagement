﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
namespace HotelManagementErp_Main.Helper
{
    /// <summary>
    /// Base Controller 
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Controller Data Boinding
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {


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

            if (!Request.IsAjaxRequest())
            {
                ViewBag.layout = "~/Areas/Inventory/Views/Shared/_ContentLayout.cshtml";
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        private  string Serialize(dynamic value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(value);
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
    }
}