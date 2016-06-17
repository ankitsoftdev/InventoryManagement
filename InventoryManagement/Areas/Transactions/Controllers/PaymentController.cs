using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ServiceLayer.Transactions;
using HotelManagementErp_Main.Helper;
using ViewModel.Transactions;
using System.Net;
using Newtonsoft.Json;
namespace InventoryManagement.Areas.Transactions.Controllers
{

    public class PaymentController : InventoryBaseController
    {
        //
        // GET: /Transactions/Payment/
        PaymentServiceLayer _db;
        
        public ActionResult Index(string Tag="")

        {

            ViewBag.Tag = Tag;
         
            return View();
        }
        public ActionResult List(string Tag = "", string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            ViewBag.Tag = Tag;
            var lst = _db.List(Tag);
            return View(lst);
        }
        public ActionResult Create(string Tag="",int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
             PaymentInfo modelPayment;
             ViewBag.Tag = Tag;
             var lst = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind(Tag, "").ToList();
            if (Id != 0)
            {
                modelPayment = _db.Find(Id);

                //  ModelPurMaster.ItemDetails.Add(new Purchase_Tra());

            }
            else
            {
                modelPayment = new PaymentInfo();
                modelPayment.Payment_Date = DateTime.Today;
                modelPayment.Tag_Type = Tag;
            }
           // modelPayment.User_List = lst;
            BindDDL(modelPayment);
            return View(modelPayment);
        }
        [HttpPost]
        public ActionResult Create(PaymentInfo modelPayment)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            ViewBag.Tag = modelPayment.Tag_Type;
            if (ModelState.IsValid == true|| ModelState.IsValid==false)
            {
                if (modelPayment.Id != 0)
                {
                    _db.Update(modelPayment);
                }
                else
                {
                    _db.Create(modelPayment);

                }

                var lst = _db.List(modelPayment.Tag_Type);

                return View("List", lst);
            }
            return View(modelPayment);
        }
        public ActionResult GetBalance(int UserId)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            var bal = _db.GetBalance(UserId);
            return Json(bal, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ViewPayment(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            PaymentInfo modelPayment;


            if (Id >= 0)
            {
                modelPayment = _db.Find(Id);
                //modelReceiptNote = new ReceiptNoteInfo();

            }
            else
            {
                modelPayment = new PaymentInfo();
                modelPayment.Payment_Date = DateTime.Now;

              

              //  modelPayment.Receipt_Note_No = _db.GEN_ReceiptNo();

            }

            return PartialView(modelPayment);
        }
        public ActionResult GetChequeDetails(int Id=0,string Cheque_No="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            var bal = _db.GetChequeDetails(Id,Cheque_No);
            return Json(!bal, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PaymentDetails(int UserId=0,string Tag="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            ViewBag.Tag = Tag;
            var lst = _db.PaymentDetails(UserId, Tag);
            return View(lst);
        }
        public ActionResult BillDetails(int UserId = 0, string Tag = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            ViewBag.Tag = Tag;
            var lst = _db.BillDetails(UserId, Tag);
            return View(lst);
        }
        public ActionResult PendingBillList(int UserId = 0,int Id=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);
            PaymentInfo modelPayment = new PaymentInfo();
            if (UserId > 0)
            {
                modelPayment.BillList = _db.PendingBillList(UserId,Id);
            }
            return View(modelPayment);
            //return Json(lst, JsonRequestBehavior.AllowGet);
        }
        private void BindDDL(PaymentInfo modelPayment)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PaymentServiceLayer(_dashboardData.DbConnectionString);


            modelPayment.User_List = _db.DDLBind("Supplier", "");
            //modelPayment.Bill_List = _db.PendingBillList(modelPayment.User_Id);
            modelPayment.BillList = new List<BillList>();
           
        }
        }
    }

