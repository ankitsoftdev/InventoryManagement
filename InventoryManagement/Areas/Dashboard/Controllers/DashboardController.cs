using HotelManagementErp_Main.Helper;
using ServiceLayer.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Areas.Dashboard.Controllers
{
    public class DashboardController : InventoryBaseController
    {
        //
        // GET: /Dashboard/Dashboard/
        DashboardSLayer _db;
        public DashboardController()
        {
           
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Charts()
        {
            return View();
        }


        public ActionResult Graph()
        {
            return View();
        }


        public ActionResult PieChart()
        {

            return View();
           
        }

        public ActionResult PieChartData()
        {

            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DashboardSLayer(_dashboardData.DbConnectionString);
           
            var data = _db.PieGraph().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SalesPurchaseYearlyData()
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DashboardSLayer(_dashboardData.DbConnectionString);
           
            var data = _db.SelesPurchaseGraph().ToList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [ChildActionOnly]

        //Get Top 10 Quations
        public ActionResult QuationsList()
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DashboardSLayer(_dashboardData.DbConnectionString);
           
            var data = _db.QuationsList().ToList();
            return PartialView(data);
        }

         [ChildActionOnly]
        //Get Top 10 Sales Order List
        public ActionResult SalesOrderList()
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DashboardSLayer(_dashboardData.DbConnectionString);
            var data = _db.SalesOrderList().ToList();
            return PartialView(data);
        }

         [ChildActionOnly]
        //Get Top 10 Purchase List
        public ActionResult PurchaseOrderList()
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DashboardSLayer(_dashboardData.DbConnectionString);
            var data = _db.PurchaseOrderList().ToList();
            return PartialView(data);
        }


        [ChildActionOnly]
        public ActionResult StockList()
        {

            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DashboardSLayer(_dashboardData.DbConnectionString);
            return PartialView(_db.ManageStock().ToList());
        }

    }
}
