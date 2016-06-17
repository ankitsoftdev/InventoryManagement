using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Common;
using ServiceLayer.Masters;
namespace InventoryManagement.Areas.Masters.Controllers
{
    public class TaxController : InventoryBaseController
    {
        //
        // GET: /Masters/TaxMaster/
        TaxServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;
            _db = new TaxServiceLayer(info.DbConnectionString);
            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new TaxServiceLayer(_dashboardData.DbConnectionString);
            Tax tax;
            if (Id != 0)
            {
                tax = _db.Find(Id);
            }
            else
            {
               tax  = new Tax();
               tax.Code = _db.GEN_TaxCode();
            }
            return View(tax);
        }
        [HttpPost]
        public ActionResult Create(Tax modeltax)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new TaxServiceLayer(_dashboardData.DbConnectionString);
            if (ModelState.IsValid || ModelState.IsValid==false)
            {
                if(modeltax.Id!=0)
                {
                    _db.Update(modeltax);
                }
                else
                {
                  
                    _db.Create(modeltax);
                }
                var lst = _db.List();
            return View("List",lst);
            }
            return View(modeltax);
        }
        public ActionResult GetTaxRate(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new TaxServiceLayer(_dashboardData.DbConnectionString);
            var result = 0.0m;
            if (Id != 0)
            {
                var tax = _db.Find(Id);
                result = tax != null ? tax.Rate : 0.00m;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsNameExists(int Id, string Name)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new TaxServiceLayer(_dashboardData.DbConnectionString);

            return Json(!_db.IsNameExists(Id, Name), JsonRequestBehavior.AllowGet);
        }
    }
}
