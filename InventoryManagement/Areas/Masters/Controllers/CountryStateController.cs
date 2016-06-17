using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.Common;
using ViewModel.Common;
namespace InventoryManagement.Areas.Masters.Controllers
{
    public class CountryStateController : InventoryBaseController
    {
        //
        // GET: /Masters/CountryState/
        CommonServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ddlBindState(int CountryId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new CommonServiceLayer(_dashboardData.DbConnectionString);
            var StateList = _db.DDLCountryState(2, CountryId).ToList().OrderBy(m => m.Name);
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ddlBindCity(int StateId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new CommonServiceLayer(_dashboardData.DbConnectionString);
            var CityList = _db.ListCityDDL(StateId).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
    }
}
