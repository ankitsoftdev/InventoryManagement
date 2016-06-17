using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Transactions;
using ViewModel.Common;
using ServiceLayer.Transactions;
using ViewModel.Ledger;
namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class PurchaseOrderController : InventoryBaseController
    {
        //
        // GET: /Transactions/PurchaseOrder/
        PurchaseOrderServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseOrderServiceLayer(_dashboardData.DbConnectionString);

            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            PurchaseOrderInfo modelPurchaseOrder = FindPurchaseOrder(Id, _dashboardData);

            return View(BindDDL(modelPurchaseOrder));
        }

        private PurchaseOrderInfo FindPurchaseOrder(int Id, HDMEntity.DashBoard _dashboardData)
        {
            _db = new PurchaseOrderServiceLayer(_dashboardData.DbConnectionString);
            PurchaseOrderInfo modelPurchaseOrder;


            if (Id != 0)
            {
                modelPurchaseOrder = _db.Find(Id);

            }
            else
            {
                modelPurchaseOrder = new PurchaseOrderInfo();
                modelPurchaseOrder.Posting_Date = DateTime.Now;
                modelPurchaseOrder.Request_Delivery_Date = DateTime.Now;
                modelPurchaseOrder.Order_Date = DateTime.Now;
                modelPurchaseOrder.Document_Date = DateTime.Now;
                modelPurchaseOrder.Order_No = _db.GEN_OrderNo();

            }
            return modelPurchaseOrder;
        }
        [HttpPost]
        public ActionResult Create(PurchaseOrderInfo modelPurchaseOrder)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseOrderServiceLayer(_dashboardData.DbConnectionString);

            if (ModelState.IsValid == true || ModelState.IsValid == false)
            {
                if (modelPurchaseOrder.Id != 0)
                {
                    _db.Update(modelPurchaseOrder); 
                }
                else
                {

                    _db.Create(modelPurchaseOrder); 
                }

                var lst = _db.List();

                return View("List", lst);
            }
            return View(BindDDL(modelPurchaseOrder));
        }
        public ActionResult OrderDetailsList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            PurchaseOrderInfo modelPurchaseOrder = new PurchaseOrderInfo();
            _db = new PurchaseOrderServiceLayer(_dashboardData.DbConnectionString);
         
            if (Id != 0)
            {
                var Challan_No = _db.Find(Id).Id;
                modelPurchaseOrder.OrderDetails = _db.OrderDetails(Challan_No);
            }
            else
            {
                modelPurchaseOrder.OrderDetails.Add(new PurchaseOrderInfo_Tra());
            }


            return View(BindDDL(modelPurchaseOrder));
        }
        public ActionResult Delete(int Id)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseOrderServiceLayer(_dashboardData.DbConnectionString);
            var res = _db.Delete(Id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplierDetails(int SupplierId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            ServiceLayer.AccountMaster.CustomerServicesLayer objcustomer = new ServiceLayer.AccountMaster.CustomerServicesLayer(_dashboardData.DbConnectionString);
            var data = objcustomer.FindCustomer(SupplierId, "Supplier");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private PurchaseOrderInfo BindDDL(PurchaseOrderInfo modelPurchaseOrder)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            if (modelPurchaseOrder != null)
            {
                modelPurchaseOrder.ItemList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Stock_Item");
                modelPurchaseOrder.SelsPersonList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Employee");
                modelPurchaseOrder.SupplierList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Supplier");
            }
            return modelPurchaseOrder;
        }




       
        public PartialViewResult ViewPurodr(int Id=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
           
            PurchaseOrderInfo modelPurchaseOrder = FindPurchaseOrder(Id, _dashboardData);

            return PartialView(modelPurchaseOrder);
        }

        public ActionResult ViewOrderDetailsList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            PurchaseOrderInfo modelPurchaseOrder = new PurchaseOrderInfo();
            _db = new PurchaseOrderServiceLayer(_dashboardData.DbConnectionString);

            if (Id != 0)
            {
                var Challan_No = _db.Find(Id).Id;
                modelPurchaseOrder.OrderDetails = _db.OrderDetails(Challan_No);
            }
            else
            {

            
                modelPurchaseOrder.OrderDetails.Add(new PurchaseOrderInfo_Tra());
            }


            return View(modelPurchaseOrder);
        }
        public ActionResult ApproveOrderList(int Id)
        {
            PurchaseOrderInfo info = new PurchaseOrderInfo();
            _db = new PurchaseOrderServiceLayer();
            info = _db.Find(Id);
            info.OrderDetails = _db.OrderDetails(info.Id);
            PurchaseOrderItem OrderdItem = new ServiceLayer.Transactions.PurchaseServiceLayer().GetPurchaseOrderItemList(info);
            TempData["PurchaseOrderDetails"] = OrderdItem;

            // return View("Transactions/Purchase/Create", OrderdItem);
            // return View(@Url.Action("Create", "Purchase", new { area = "Transactions" }), OrderdItem);
            return RedirectToAction("Create", "Purchase", new { OrderedItem = OrderdItem });
        }
    }
}
