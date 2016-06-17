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
    public class QuationController : InventoryBaseController
    {
        //
        // GET: /Transactions/Quation/
        QuationServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new QuationServiceLayer(_dashboardData.DbConnectionString);
           
            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create( int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new QuationServiceLayer(_dashboardData.DbConnectionString);
            QuationInfo modelQuation;
         
         
            if (Id != 0)
            {
                modelQuation = _db.Find(Id);

            }
            else
            {
                modelQuation = new QuationInfo();
                modelQuation.Quation_Chalan_No = _db.GEN_ChallanNo();
               
            }
            modelQuation.CustomerList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Customer", "").ToList();
            return View(modelQuation);
        }
        [HttpPost]
        public ActionResult Create(QuationInfo modelQuation)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new QuationServiceLayer(_dashboardData.DbConnectionString);
          
            if (ModelState.IsValid == true || ModelState.IsValid==false )
            {
                if (modelQuation.Id != 0)
                {
                    _db.Update(modelQuation);
                }
                else
                {
                    
                    _db.Create(modelQuation);


                }

                var lst = _db.List();

                return View("List", lst);
            }
            modelQuation.CustomerList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Customer", "").ToList();
            return View(modelQuation);
        }
       
        public ActionResult QuationItemList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            QuationInfo modelQuation = new QuationInfo();
            _db = new QuationServiceLayer();
            ServiceLayer.Common.CommonServiceLayer _objCommon = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString);
            if (Id != 0)
            {
                var Challan_No = _db.Find(Id).Id;
                modelQuation.QuationItemList = _db.QuationDetails(Challan_No);
            }
            else
            {
                modelQuation.QuationItemList.Add(new QuationInfo_Tra());
            }
            modelQuation.ItemList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("Stock_Item");

            return View(modelQuation);
        }
        public ActionResult Delete(int Id)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new QuationServiceLayer(_dashboardData.DbConnectionString);
            var res = _db.Delete(Id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ViewQuation(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new QuationServiceLayer(_dashboardData.DbConnectionString);
            QuationInfo modelQuation;


            if (Id != 0)
            {
                modelQuation = _db.Find(Id);

            }
            else
            {
                modelQuation = new QuationInfo();
                
                
                modelQuation.Quation_Chalan_No = _db.GEN_ChallanNo();

            }

            return PartialView(modelQuation);
        }
        public ActionResult ViewQuationDetailsList(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new QuationServiceLayer(_dashboardData.DbConnectionString);
            QuationInfo modelQuation=new QuationInfo();
            if (Id != 0)
            {
                var Challan_No = _db.Find(Id).Id;
                modelQuation.QuationItemList = _db.QuationDetails(Challan_No);
            }
            else
            {


                modelQuation.QuationItemList.Add(new QuationInfo_Tra());
            }


            return View(modelQuation);
        }
        public ActionResult ApproveQuation(int QuationId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new QuationServiceLayer(_dashboardData.DbConnectionString);
          //  var res= _db.ApproveQuation(QuationId);
            return RedirectToAction("SalesOrderByQuation", "SalesOrder", new { QuationId = QuationId });
        }
        private QuationInfo BindDDL(QuationInfo modelQuation)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            if (modelQuation != null)
            {
               
                modelQuation.ItemList = new ServiceLayer.InventoryMaster.StockServiceLayer(_dashboardData.DbConnectionString).DDLBind();
               
            }
            return modelQuation;
        }
    }
}
