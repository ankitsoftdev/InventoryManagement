using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Common;
using ServiceLayer.Masters;
using ServiceLayer.Common;
namespace InventoryManagement.Areas.Masters.Controllers
{
    public class GoDownController : InventoryBaseController
    {
        //
        // GET: /Masters/GoDown/
        GoDownServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new GoDownServiceLayer(_dashboardData.DbConnectionString);
            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {

            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new GoDownServiceLayer(_dashboardData.DbConnectionString);
            GoDown_Info modelGoDown;
            if (Id != 0)
            {
                modelGoDown = _db.Find(Id);
            }
            else
            {
                modelGoDown = new GoDown_Info();
                modelGoDown.Code = _db.GEN_GodownCode();
            }
            ddlbind(modelGoDown);
            return View(modelGoDown);
        }
        [HttpPost]
        public ActionResult Create(GoDown_Info modelGoDown)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new GoDownServiceLayer(_dashboardData.DbConnectionString);
            if (ModelState.IsValid)
            {
                if (modelGoDown.Id != 0)
                {
                    _db.Update(modelGoDown);
                }
                else
                {

                    _db.Create(modelGoDown);
                }
                var lst = _db.List();
                return View("List", lst);
            }
            ddlbind(modelGoDown);
            return View(modelGoDown);
        }
        public void ddlbind(GoDown_Info modelGoDown)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new GoDownServiceLayer(_dashboardData.DbConnectionString);
            CommonServiceLayer _objCommon = new CommonServiceLayer(_dashboardData.DbConnectionString);
            modelGoDown.GoDownList = _objCommon.DDLBind("GoDown_Master", "").ToList();
            if (modelGoDown.Id != 0)
            {
               modelGoDown.CountryList = _objCommon.DDLCountryState(1, 0);
               modelGoDown.StateList = _objCommon.DDLCountryState(2, 0);
               modelGoDown.CityList = _objCommon.ListCityDDL(0).ToList();
               

            }
            else
            {
                modelGoDown.CountryList = _objCommon.DDLCountryState(1, 0);
                modelGoDown.StateList = new List<DDLBind>();
                modelGoDown.CityList = new List<DDLBind>();
            }

        }
        public ActionResult IsNameExists(int Id, string Name)
        {

            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new GoDownServiceLayer(_dashboardData.DbConnectionString);

            return Json(!_db.IsNameExists(Id, Name), JsonRequestBehavior.AllowGet);
        }
    }
}
