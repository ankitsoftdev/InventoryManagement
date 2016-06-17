using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Common;
using ViewModel.Transactions;
using ServiceLayer.Transactions;
namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class PurchaseReturnController : InventoryBaseController
    {
        //
        // GET: /Transactions/PurchaseReturn/
        PurchaseReturnServiceLayer _db;
        public ActionResult Index()
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            return View();
        }
        public ActionResult List(string search="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            var lst = _db.List(search);
            return View(lst);
        }
        public ActionResult Create(long Id=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            PurchaseReturnInfo ModelPurRet;
            if(Id>0)
            {
                ModelPurRet = _db.Find(Id);
            }
            else
            {
                ModelPurRet = new PurchaseReturnInfo();
                ModelPurRet.VC_No = _db.GEN_VC_No();
                ModelPurRet.Return_Date = DateTime.Today;
            }
            DDLBind(ModelPurRet);
            return View(ModelPurRet);
        }

        [HttpPost]
        public ActionResult Create(PurchaseReturnInfo ModelPurRet)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            if(ModelState.IsValid || ModelState.IsValid==false)
            {
                if(ModelPurRet.Id>0)
                {
                    _db.Update(ModelPurRet);
              
                }
                else
                {
                    _db.Create(ModelPurRet);
                }
                var lst = _db.List();
                return View("List", lst);
            }
            DDLBind(ModelPurRet);
            return View(ModelPurRet);
        }
        public ActionResult PurchaseReturnDetails(long Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            PurchaseReturnInfo ModelPurRet = new PurchaseReturnInfo();


            if (Id != 0)
            {
                ModelPurRet.ReturnDetails = _db.ReturnDetails(Id);

            }
            else
            {
                ModelPurRet.ReturnDetails.Add(new PurchaseReturnInfo_Tra());
            }

            DDLBind(ModelPurRet);
            return View(ModelPurRet);
        }
        public ActionResult CheckSerialNo(string SerialNo)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            var result = _db.CheckSerialNo(SerialNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemDetail(long Id)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            var item = new ServiceLayer.InventoryMaster.StockServiceLayer(_dashboardData.DbConnectionString).Find(Id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);


            bool result = false;
            if (Id > 0)
            {
                result = _db.Delete(Id);

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBillList(long Supplier_Id)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db=new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            var l = _db.GetBillList(Supplier_Id);
            return Json(l, JsonRequestBehavior.AllowGet);
        }
        private void DDLBind(PurchaseReturnInfo ModelPurRet)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseReturnServiceLayer(_dashboardData.DbConnectionString);
            ModelPurRet.ItemList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Stock_Item");
            ModelPurRet.SupplierList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Supplier");
            ModelPurRet.BillList = new List<DDLBind>();

        }
    }
}
