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
    public class RegionController : InventoryBaseController
    {
        //
        // GET: /Masters/Region/
        RegionServiceLayer _db;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;
            _db = new RegionServiceLayer(info.DbConnectionString);
            var lst = _db.List();
            return View(lst);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new RegionServiceLayer(_dashboardData.DbConnectionString);

            RegionInfo modelRegionInfo;
            if (Id != 0)
            {
                modelRegionInfo = _db.Find(Id);
            }
            else
            {
                modelRegionInfo = new RegionInfo();
              
            }
            modelRegionInfo.GoDownList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("GoDown");
            modelRegionInfo.UnderList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("GoDown_Region");
            return View(modelRegionInfo);
        }
        [HttpPost]
        public ActionResult Create(RegionInfo modelRegionInfo)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new RegionServiceLayer(_dashboardData.DbConnectionString);
            if (ModelState.IsValid)
            {
                if (modelRegionInfo.Id != 0)
                {
                    _db.Update(modelRegionInfo);
                }
                else
                {

                    _db.Create(modelRegionInfo);
                }
                var lst = _db.List();
                return View("List", lst);
            }
            modelRegionInfo.GoDownList = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("GoDown");
            return View(modelRegionInfo);
        }
        public ActionResult Delete(int Id=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new RegionServiceLayer(_dashboardData.DbConnectionString);
            return Json(_db.Delete(Id), JsonRequestBehavior.AllowGet);
        }
    }
}
