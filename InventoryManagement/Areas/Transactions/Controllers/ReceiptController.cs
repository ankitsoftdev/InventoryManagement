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
    public class ReceiptController : InventoryBaseController
    {
        //
        // GET: /Transactions/Receipt/
        ReceiptServiceLayer _db;
        public ActionResult Index(string Tag="")
        {
            ViewBag.Tag = Tag;
            return View();
        }
        public ActionResult List(string Tag = "", string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
            ViewBag.Tag = Tag;
            var lst = _db.List(Tag);
            return View(lst);
        }
        public ActionResult Create(string Tag = "", int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
           ReceiptInfo modelReceipt;
            ViewBag.Tag = Tag;

            var lst = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind(Tag, "").ToList();
            if (Id != 0)
            {
                modelReceipt = _db.Find(Id);

                

            }
            else
            {
                modelReceipt = new ReceiptInfo();
                modelReceipt.Receipt_Date = DateTime.Today;
                modelReceipt.Tag_Type = Tag;
            }
           
            BindDDL(modelReceipt);
            return View(modelReceipt);
        }
        [HttpPost]
        public ActionResult Create(ReceiptInfo modelReceipt)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
            ViewBag.Tag = modelReceipt.Tag_Type;
            if (ModelState.IsValid == true || ModelState.IsValid == false)
            {
                if (modelReceipt.Id != 0)
                {
                    _db.Update(modelReceipt);
                }
                else
                {
                    _db.Create(modelReceipt);

                }

                var lst = _db.List(modelReceipt.Tag_Type);

                return View("List", lst);
            }
            return View(modelReceipt);
        }
        public ActionResult GetBalance(int UserId)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
            var bal = _db.GetBalance(UserId);
            return Json(bal, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ViewReceipt(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
            ReceiptInfo modelReceipt;


            if (Id >= 0)
            {
                modelReceipt = _db.Find(Id);
                //modelReceiptNote = new ReceiptNoteInfo();

            }
            else
            {
                modelReceipt = new ReceiptInfo();
                modelReceipt.Receipt_Date = DateTime.Now;



                //  modelPayment.Receipt_Note_No = _db.GEN_ReceiptNo();

            }

            return PartialView(modelReceipt);
        }
        public ActionResult GetChequeDetails(int Id = 0, string Cheque_No = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
            var bal = _db.GetChequeDetails(Id, Cheque_No);
            return Json(!bal, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReceiptDetails(int UserId = 0, string Tag = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
          //  List<ReceiptDetails> lst = new List<ViewModel.Transactions.ReceiptDetails>();
            ViewBag.Tag = Tag;
            var lst = _db.ReceiptDetails (UserId, Tag).Take(10).ToList();
            return View(lst);
        }
        public ActionResult BillDetails(int UserId = 0, string Tag = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
            ViewBag.Tag = Tag;
            //var lst = _db.BillDetails(UserId, Tag);
            return View();
        }
        public ActionResult PendingBillList(int UserId = 0, int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);
            ReceiptInfo modelReceipt = new ReceiptInfo();
            if (UserId > 0)
            {
                modelReceipt.BillList = _db.PendingBillList(UserId, Id);
            }
            return View(modelReceipt);
            //return Json(lst, JsonRequestBehavior.AllowGet);
        }
        private void BindDDL(ReceiptInfo modelReceipt)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new ReceiptServiceLayer(_dashboardData.DbConnectionString);


            modelReceipt.User_List = _db.DDLBind("Customer", "");
            //modelPayment.Bill_List = _db.PendingBillList(modelPayment.User_Id);
            modelReceipt.BillList = new List<BillList>();

        }
    }
}
