using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using ViewModel.Common;
using ServiceLayer.Masters;
namespace InventoryManagement.Areas.Masters.Controllers
{
    public class LedgerGroupController : InventoryBaseController
    {
        //
        // GET: /Masters/Ledger/
        LedgerGroupServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerGroupServiceLayer(_dashboardData.DbConnectionString);
            var lst = _db.List();
           
            return View(lst);
        }
        public ActionResult Create(int Id=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerGroupServiceLayer(_dashboardData.DbConnectionString);
            ViewModel.LadgerGroup.LedgerGroup Lggrp;
           
            if(Id!=0)
            {
                Lggrp = _db.Find(Id);
                Lggrp.Ledger.ListUnder = _db.DDLBind("LEDGERGROUP", "");
            }
            else
            {
                Lggrp = new ViewModel.LadgerGroup.LedgerGroup();
                Lggrp.Ledger.ListUnder = _db.DDLBind("LEDGERGROUP", "");
            }
            return View(Lggrp);
        }
        [HttpPost]
        public ActionResult Create(ViewModel.LadgerGroup.LedgerGroup modelLedggrp)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerGroupServiceLayer(_dashboardData.DbConnectionString);
            if (ModelState.IsValid)
            {
                if (modelLedggrp.Ledger.Id != 0)
                {
                    _db.Update(modelLedggrp);


                }
                else
                {
                    _db.create(modelLedggrp);
                }
                var lst = _db.List();
                return View("List", lst);
            }
            else
            {
                modelLedggrp.Ledger.ListUnder = _db.DDLBind("LEDGERGROUP", "");
                return View(modelLedggrp);
            }
        }
        public ActionResult IsNameExists(ViewModel.LadgerGroup.LedgerGroup model)
            {

                var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
                _db = new LedgerGroupServiceLayer(_dashboardData.DbConnectionString);

            return Json(!_db.IsNameExists(model.Ledger.Id,model.Ledger.Name), JsonRequestBehavior.AllowGet);
        }
       public ActionResult GetLedger(string searchText="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerGroupServiceLayer(_dashboardData.DbConnectionString);
           var data = _db.DDLBind("LEDGERGROUP", searchText).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
