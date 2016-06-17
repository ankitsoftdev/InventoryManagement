using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.Transactions;
using ViewModel.Transactions;
namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class StockMasterController : InventoryBaseController
    {
        //
        // GET: /Transactions/StockMaster/
        StockMasterServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult List()
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockMasterServiceLayer(_dashboardData.DbConnectionString);
           // List<ViewModel.Transactions.StockMaster> lst = new List<ViewModel.Transactions.StockMaster>();
            var lst = _db.List();

            return View(lst);
        }
        public ActionResult ItemAvailabltyDetails(int ItemId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockMasterServiceLayer(_dashboardData.DbConnectionString);
            var data = _db.Find(ItemId);

            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public ActionResult MasterDetails(int ItemId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockMasterServiceLayer(_dashboardData.DbConnectionString);
           // var data = _db.Find(ItemId);
           MasterDetailsInfo modelMasterDetail=new  MasterDetailsInfo();
           return View(modelMasterDetail);
        }
    }
}
