using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.Transactions;
using ViewModel.Transactions;
using HotelManagementErp_Main.Helper;
namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class DeliveryNoteController : InventoryBaseController
    {
        //
        // GET: /Transactions/DebitNote/
        DeliveryNoteServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);

            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);
           DeliveryNoteInfo modelDeliveryNote;


            if (Id != 0)
            {
                modelDeliveryNote = _db.Find(Id);
                modelDeliveryNote.SalesOrderList = _db.DDLBind(CustomerId: modelDeliveryNote.Customer_Id, Id:Id); 
            }
            else
            {
                modelDeliveryNote = new DeliveryNoteInfo();
                modelDeliveryNote.Delivery_Date =  DateTime.Now.ToShortDateString();
               modelDeliveryNote.Delivery_Note_No=_db.GEN_DebitNo();
               

            }

            return View(BindDDL(modelDeliveryNote));
        }
        [HttpPost]
        public ActionResult Create(DeliveryNoteInfo modelDeliveryNote)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);

            if (ModelState.IsValid == true || ModelState.IsValid == false)
            {
                if (modelDeliveryNote.Id != 0)
                {
                    _db.Update(modelDeliveryNote);
                    
                }
                else
                {

                   _db.Create(modelDeliveryNote);


                }

                var lst = _db.List();

                return View("List", lst);
            }
            return View(BindDDL(modelDeliveryNote));
        }
     
        public ActionResult DeliveryNoteDetailsList(int Id = 0,int OrderId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            DeliveryNoteInfo modelDeliveryNote=new DeliveryNoteInfo();
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);
        
            if (OrderId != 0 || Id!=0)
            {
              
                modelDeliveryNote.DeliveryDetails = _db.DeliveryNoteDetails(Id: Id, OrderId: OrderId);
                modelDeliveryNote.Sales_Order_Id = OrderId;
                modelDeliveryNote.Id = Id;
             
              //  modelDeliveryNote.DeliveryDetails.Add(new DeliveryNoteInfo_Tra());
            }
            else
            {
                modelDeliveryNote.DeliveryDetails.Add(new DeliveryNoteInfo_Tra());
            }


            return View(BindDDL(modelDeliveryNote));
        }
        public ActionResult Delete(int Id)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);
            var res = _db.Delete(Id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ViewDelivery(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);
            DeliveryNoteInfo modelDeliveryNote;


            if (Id != 0)
            {
                modelDeliveryNote = _db.Find(Id);

               // modelDeliveryNote = new DeliveryNoteInfo();
            }
            else
            {
                modelDeliveryNote = new DeliveryNoteInfo();
                modelDeliveryNote.Delivery_Date = DateTime.Now.ToShortDateString();

               

                modelDeliveryNote.Delivery_Note_No = _db.GEN_DebitNo();

            }

            return PartialView(modelDeliveryNote);
        }
        public ActionResult ViewDeliverynoteDetailsList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            DeliveryNoteInfo modelDeliveryNote = new DeliveryNoteInfo();
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);

            if (Id != 0)
            {

                modelDeliveryNote.DeliveryDetails = _db.DeliveryNoteDetails(OrderId: 0, Id: Id);
                //modelReceiptNote.ReceiptDetails.Add(new ReceiptNoteTraInfo());
            }
            else
            {


                modelDeliveryNote.DeliveryDetails.Add(new DeliveryNoteInfo_Tra());
            }


            return View(modelDeliveryNote);
        }
        public ActionResult MasterDetails(int CustomerId = 0,int OrderId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
           _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);
             DeliveryNoteMasterInfo modelMasterDetails = new DeliveryNoteMasterInfo();
             if (CustomerId != 0)
             {
                 var objcustomer =  new ServiceLayer.Masters.LedgerMasterServiceLayer (_dashboardData.DbConnectionString).Find( CustomerId);
                 var objorder = new ServiceLayer.Transactions.SalesOrderServiceLayer(_dashboardData.DbConnectionString).Find(OrderId);
                 modelMasterDetails = new DeliveryNoteMasterInfo
                 {
                     Address = objcustomer.Address,
                     Contact_No = objcustomer.Contact_No,
                     Customer_Code = objcustomer.Code,
                     Email = objcustomer.Email_Id,
                     Name = objcustomer.Name,
                     Order_Date = objorder.Posting_Date.ToShortDateString()
                 };
             }
            else
             {
                 modelMasterDetails = new DeliveryNoteMasterInfo {Address="N/A",Contact_No="N/A",Customer_Code="N/A",Email="N/A",Name="N/A",Order_Date="N/A" };
             }
         
          
            return View(modelMasterDetails);
        }
        public ActionResult  BindSalesOrderNo(int CustomerId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new DeliveryNoteServiceLayer(_dashboardData.DbConnectionString);
            var data = _db.DDLBind(CustomerId: CustomerId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private DeliveryNoteInfo BindDDL(DeliveryNoteInfo modelDeliveryNote)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            if (modelDeliveryNote != null)
            {
                modelDeliveryNote.ItemList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Stock_Item");
                //modelDeliveryNote.SelsPersonList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Employee");
                modelDeliveryNote.CustomerList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Customer");
               // modelDeliveryNote.SalesOrderList = new List<ViewModel.Common.DDLBind>();
            }
            return modelDeliveryNote;
        }

    }
}
