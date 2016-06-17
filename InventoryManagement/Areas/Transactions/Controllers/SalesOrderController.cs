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
    public class SalesOrderController : InventoryBaseController
    {
        //
        // GET: /Transactions/SalesOrder/
        SalesOrderServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;

            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);

           var lst = _db.List();
           return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            SalesOrderInfo modelSalesOrder = FindSalesOrder(Id, _dashboardData);

            return View(BindDDL(modelSalesOrder));
        }

        private SalesOrderInfo FindSalesOrder(int Id, HDMEntity.DashBoard _dashboardData)
        {
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);
            SalesOrderInfo modelSalesOrder;


            if (Id != 0)
            {
                modelSalesOrder = _db.Find(Id);

            }
            else
            {
                modelSalesOrder = new SalesOrderInfo();
                modelSalesOrder.Posting_Date = DateTime.Now;
                modelSalesOrder.Request_Delivery_Date = DateTime.Now;
                modelSalesOrder.Order_Date = DateTime.Now;
                modelSalesOrder.Document_Date = DateTime.Now;
                modelSalesOrder.Order_No = _db.GEN_OrderNo();

            }
            return modelSalesOrder;
        }
        [HttpPost]
        public ActionResult Create(SalesOrderInfo modelSalesOrder)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);

            if (ModelState.IsValid == true || ModelState.IsValid==false)
            {
                if (modelSalesOrder.Id != 0)
                {
                    _db.Update(modelSalesOrder);
                }
                else
                {
                   
                  _db.Create(modelSalesOrder);


                }

                var lst = _db.List();

                return View("List", lst);
            }
            return View(BindDDL(modelSalesOrder));
        }
        [HttpGet]
        public ActionResult SalesOrderByQuation(int QuationId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);
            SalesOrderInfo modelSalesOrder = new SalesOrderInfo();

            if (QuationId != 0)
            {

                modelSalesOrder = _db.FindByQuationId(QuationId);
                
            }
           

            return View(BindDDL(modelSalesOrder));
        }
        [HttpPost]
        public ActionResult SalesOrderByQuation(SalesOrderInfo modelSalesOrder)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);

            bool result = false;
            if (modelSalesOrder != null)
            {

             result=   _db.CreateByQuation(modelSalesOrder);
             var lst = _db.List();

             return View("List", lst);
            }
            

            return View(BindDDL(modelSalesOrder));
        }
        public ActionResult OrderDetailsByQuationId(int QuationId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            SalesOrderInfo modelSalesOrder = new SalesOrderInfo();
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);
            if (QuationId != 0)
            {

                modelSalesOrder.OrderDetails = _db.OrderDetailsByQuationId(QuationId);
            }
            else
            {
                modelSalesOrder.OrderDetails.Add(new SalesOrderInfo_Tra());
            }
            

            return View(BindDDL(modelSalesOrder));
        }
        public ActionResult OrderDetailsList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            SalesOrderInfo modelSalesOrder = new SalesOrderInfo();
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);
           // ServiceLayer.Common.CommonServiceLayer _objCommon = new ServiceLayer.Common.CommonServiceLayer();
            if (Id != 0)
            {
                var Challan_No = _db.Find(Id).Id;
                modelSalesOrder.OrderDetails = _db.OrderDetails(Challan_No);
            }
            else
            {
                modelSalesOrder.OrderDetails.Add(new SalesOrderInfo_Tra());
            }


            return View(BindDDL(modelSalesOrder));
        }
        public ActionResult Delete(int Id)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);
          var res = _db.Delete(Id);
          return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustomerDetails(int CustomerId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            ServiceLayer.AccountMaster.CustomerServicesLayer objcustomer=new ServiceLayer.AccountMaster.CustomerServicesLayer(_dashboardData.DbConnectionString);
            var data = objcustomer.FindCustomer(CustomerId, "Customers");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private SalesOrderInfo BindDDL(SalesOrderInfo modelSalesOrder)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            if (modelSalesOrder != null)
            {
                modelSalesOrder.ItemList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Stock_Item");
                modelSalesOrder.SelsPersonList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Employee");
                modelSalesOrder.CustomerList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Customer");
            }
            return modelSalesOrder;
        }

        public PartialViewResult ViewSalesOdr(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            SalesOrderInfo modelSalesOrder = FindSalesOrder(Id, _dashboardData);

            return PartialView(modelSalesOrder);
        }
        public ActionResult ViewSalesOrderDetailsList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            SalesOrderInfo modelSalesOrder = new SalesOrderInfo();
            _db = new SalesOrderServiceLayer(_dashboardData.DbConnectionString);
           
            if (Id != 0)
            {
                var Challan_No = _db.Find(Id).Id;
                modelSalesOrder.OrderDetails = _db.OrderDetails(Challan_No);
            }
            else
            {
                modelSalesOrder.OrderDetails.Add(new SalesOrderInfo_Tra());
            }


            return PartialView(modelSalesOrder);
        }
  
    }
}
