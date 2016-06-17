using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Transactions;
using ServiceLayer.Transactions;

namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class SalesReturnController : InventoryBaseController
    {
        //
        // GET: /Transactions/SalesReturn/
        SalesReturnServiceLayer _db;
        public ActionResult Index()
        {
            return View();
            
            
        }
        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesReturnServiceLayer(_dashboardData.DbConnectionString);

            var lst = _db.List(search);
            return View(lst);
        }
        public ActionResult Create(long Id=0)

        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesReturnServiceLayer(_dashboardData.DbConnectionString);
            SalesReturnInfo ModelSalesRet;
            if(Id>0)
            {
                ModelSalesRet = _db.Find(Id);
            }
            else
            {
                ModelSalesRet = new SalesReturnInfo();
                ModelSalesRet.VC_No = _db.GEN_VC_No();
                ModelSalesRet.Return_Date = DateTime.Today;
            }
            DDLBind(ModelSalesRet);
            return View(ModelSalesRet);
        }
        [HttpPost]
        public ActionResult Create(SalesReturnInfo ModelSalesRet)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesReturnServiceLayer(_dashboardData.DbConnectionString);
            if(ModelState.IsValid)
            {
                if(ModelSalesRet.Id>0)
                {
                    _db.Update(ModelSalesRet);
                }
                else
                {
                    _db.Create(ModelSalesRet);
                }
                var lst = _db.List();
                return View("List", lst);
            }
            DDLBind(ModelSalesRet);
            return View(ModelSalesRet);
        }
        public ActionResult SalesReturnDetails(long Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesReturnServiceLayer(_dashboardData.DbConnectionString);
            SalesReturnInfo ModelSalesRet = new SalesReturnInfo();
          

            if (Id> 0)
            {
                ModelSalesRet.ReturnDetails = _db.ReturnDetails(Id);
              
            }
            else
            {
                ModelSalesRet.ReturnDetails.Add(new SalesReturnInfo_Tra());
            }

            DDLBind(ModelSalesRet);
            return View(ModelSalesRet);
        }

        private void DDLBind(SalesReturnInfo ModelSalesRet)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesReturnServiceLayer(_dashboardData.DbConnectionString);
            ModelSalesRet.ItemList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Stock_Item");
            ModelSalesRet.CustomerList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Customer");

        }
        public ActionResult Delete(long Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesReturnServiceLayer(_dashboardData.DbConnectionString);
          

            bool result=false;
            if (Id > 0)
            {
               result=_db.Delete(Id);

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
