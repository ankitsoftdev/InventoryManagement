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
    public class UnitsController : InventoryBaseController
    {
        //
        // GET: /Masters/Units/
        UnitsServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;
            _db = new UnitsServiceLayer(info.DbConnectionString);
            var lst = _db.List(search);

            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new UnitsServiceLayer(_dashboardData.DbConnectionString);
            Unit unit;

            if (Id != 0)
            {
                unit = _db.Find(Id);
            }
            else
            {
                unit = new Unit();
            }
            return View(unit);
        }
        [HttpPost]
        public ActionResult Create(Unit  unit)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new UnitsServiceLayer(_dashboardData.DbConnectionString);
            if (ModelState.IsValid)
            {
                if (unit.Id!=0)
                {
                    _db.Update(unit);
                }
                else
                {
                   _db.Create(unit);
                }
                
                var lst = _db.List();

                return View("List",lst);
            }

            else
                return View(unit);
        }

        public ActionResult IsNameExists(int Id, string Name)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;

            _db = new UnitsServiceLayer(_dashboardData.DbConnectionString);

            return Json(!_db.IsNameExists(Id, Name), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SubUnit(long Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new UnitsServiceLayer(_dashboardData.DbConnectionString);
            Unit unit=new Unit();
            if(Id!=0)
            {
                var lst=_db.FindSubUnit(Id);
                if (lst.Count >0 )
                {
                    unit.SubUnitList = lst;
                }
                else
                unit.SubUnitList.Add(new SubUnit());
                    
            }
            else
            {
                unit.SubUnitList.Add(new SubUnit());
                
            }
            return PartialView(unit);
        }
        public ActionResult DeleteUnit(int Id=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new UnitsServiceLayer(_dashboardData.DbConnectionString);
            var data = _db.Delete(Id);
            var lst = _db.List();
            return PartialView("List",lst);
        }

    }
}
