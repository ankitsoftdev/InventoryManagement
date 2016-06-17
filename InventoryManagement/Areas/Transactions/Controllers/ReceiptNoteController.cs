using HotelManagementErp_Main.Helper;
using ServiceLayer.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Transactions;

namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class ReceiptNoteController : InventoryBaseController
    {
          ReceiptNoteServiceLayer _db;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);
         //   List<ReceiptNoteList> lst = new List<ReceiptNoteList>();
            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);
            ReceiptNoteInfo modelReceiptNote = new ReceiptNoteInfo();


            if (Id != 0)
            {
                modelReceiptNote = _db.Find(Id);
                modelReceiptNote.PurchaseOrderList = _db.DDLBind(SuuplierId: modelReceiptNote.Supplier_Id, Id: Id);
            }
            else
            {
                modelReceiptNote = new ReceiptNoteInfo();
                modelReceiptNote.Receipt_Date = DateTime.Now.ToShortDateString();
                modelReceiptNote.Receipt_Note_No = _db.GEN_ReceiptNo();


            }

            return View(BindDDL(modelReceiptNote));
        }
        //public ActionResult Create()
        //{
        //    _db = new ReceiptNoteServiceLayer();
        //    ReceiptNoteInfo data = new ReceiptNoteInfo();
        //    return View(BindDDL(data));
        //}

        [HttpPost]
        public ActionResult Create(ReceiptNoteInfo modelReceiptInfo)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);
           // ReceiptNoteInfo data = new ReceiptNoteInfo();
            if (ModelState.IsValid == true || ModelState.IsValid == false)
            {
                if (modelReceiptInfo.Id != 0)
                {
                   _db.Update(modelReceiptInfo);

                }
                else
                {

                   _db.Create(modelReceiptInfo);


                }

                var lst = _db.List();

                return View("List", lst);
            }
            return View(BindDDL(modelReceiptInfo));
        }

        private ReceiptNoteInfo BindDDL(ReceiptNoteInfo data)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            if (data != null)
            {
                data.SupplierList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Supplier");
              //  data.PurchaseOrderList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Stock_Item");
               
            }
            return data;
        }


        public ActionResult BindPurchaseOrderNO(int SupplierId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);
            var data = _db.DDLBind(SupplierId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ViewReceipt(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);
            ReceiptNoteInfo modelReceiptNote;


            if (Id != 0)
            {
                modelReceiptNote = _db.Find(Id);
                //modelReceiptNote = new ReceiptNoteInfo();

            }
            else
            {
                modelReceiptNote = new ReceiptNoteInfo();
                modelReceiptNote.Receipt_Date = DateTime.Now.ToShortDateString();
                
                modelReceiptNote.Order_Date = DateTime.Now.ToShortDateString();
                
                modelReceiptNote.Receipt_Note_No = _db.GEN_ReceiptNo();

            }

            return PartialView(modelReceiptNote);
        }
        public ActionResult ReceiptNoteDetailsList(int Id = 0, int OrderId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            ReceiptNoteInfo modelReceiptNote = new ReceiptNoteInfo();
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);

            if (OrderId != 0 || Id != 0)
            {

                modelReceiptNote.ReceiptDetails = _db.ReceiptNoteDetails(Id: Id, OrderId: OrderId);
                modelReceiptNote.Purchase_Order_Id = OrderId;
                modelReceiptNote.Id = Id;

                //  modelDeliveryNote.DeliveryDetails.Add(new DeliveryNoteInfo_Tra());
            }
            else
            {
                modelReceiptNote.ReceiptDetails.Add(new ReceiptNoteTraInfo());
            }


            return View(BindDDL(modelReceiptNote));
        }
        public ActionResult ViewReceiptDetailsList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            ReceiptNoteInfo modelReceiptNote =new ReceiptNoteInfo();
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);

            if (Id != 0)
            {
                var Challan_No = _db.Find(Id).Receipt_Note_No;
                modelReceiptNote.ReceiptDetails = _db.ReceiptNoteDetails(OrderId: 0, Id: Id);
                //modelReceiptNote.ReceiptDetails.Add(new ReceiptNoteTraInfo());
            }
            else
            {


                modelReceiptNote.ReceiptDetails.Add(new ReceiptNoteTraInfo());
            }


            return View(modelReceiptNote);
        }
        public ActionResult MasterDetails(int SuuplierId = 0, int OrderId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptNoteServiceLayer(_dashboardData.DbConnectionString);
            ReceiptNoteMasterInfo data = new ReceiptNoteMasterInfo();
            if(SuuplierId!=0 && OrderId!=0)
            {
                var objsupplier = new ServiceLayer.AccountMaster.CustomerServicesLayer(_dashboardData.DbConnectionString).GetCustomer("SUPPLIER").FirstOrDefault(x => x.Id == SuuplierId);
                var objorder = new ServiceLayer.Transactions.PurchaseOrderServiceLayer(_dashboardData.DbConnectionString).Find(OrderId);
                data = new ReceiptNoteMasterInfo { Name = objsupplier.Name, Contact_No = objsupplier.Address.ContactInfo.Mobile, Email = objsupplier.Address.ContactInfo.Email_Id, Address = objsupplier.Address.address, Order_Date = objorder.Posting_Date.ToShortDateString() };
            }
            else
            {
                data = new ReceiptNoteMasterInfo { Address = "N/A", Contact_No = "N/A", Supplier_Code = "N/A", Email = "N/A", Name = "N/A", Order_Date = "N/A" };
            }
            return PartialView(data);
        }


    }
}
