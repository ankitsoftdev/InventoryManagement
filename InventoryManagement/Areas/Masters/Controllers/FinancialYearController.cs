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
    public class FinancialYearController : InventoryBaseController
    {
        //
        // GET: /Masters/FinancialYear/
        FinancialYearServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string search = "")
        {
            var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;
            _db = new FinancialYearServiceLayer(info.DbConnectionString);
            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            FinancialYear Fnclyear;
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new FinancialYearServiceLayer(_dashboardData.DbConnectionString);
            if (Id != 0)
            {
                Fnclyear = _db.Find(Id);
            }
            else
            {
                Fnclyear = new FinancialYear();
            }
            return View(Fnclyear);
        }
        [HttpPost]
        public ActionResult Create(ViewModel.Common.FinancialYear modelFincial)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new FinancialYearServiceLayer(_dashboardData.DbConnectionString);

            if (ModelState.IsValid)
            {
                if(modelFincial.Id!=0)
                {
                    _db.Update(modelFincial);
                }
                else
                {
                    _db.Create(modelFincial);
                }
                var lst = _db.List();
                return View("List",lst);
            }
            return View(modelFincial);
        }
        public ActionResult IsNameExists(int Id,string Name)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new FinancialYearServiceLayer(_dashboardData.DbConnectionString);

            return Json(!_db.IsNameExists(Id, Name), JsonRequestBehavior.AllowGet);
        }
    }
}
