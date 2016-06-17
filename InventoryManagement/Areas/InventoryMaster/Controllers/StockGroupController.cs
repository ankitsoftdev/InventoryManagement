using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Category;
using ViewModel.Common;
using ServiceLayer.InventoryMaster;
namespace InventoryManagement.Areas.InventoryMaster.Controllers
{
    public class StockGroupController : InventoryBaseController
    {
        //
        // GET: /InventoryMaster/StockGroup/
        StockGroupServicelayer _db;
        
        public ActionResult Index()
       {
            return View();
        }

        public ActionResult List(string search = "")
        {
            var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;

            _db = new StockGroupServicelayer(info.DbConnectionString);
           
            var lst = _db.List();

            return View(lst);

           
        }
        //[ChildActionOnly]
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockGroupServicelayer(_dashboardData.DbConnectionString);
            //ViewBag.House = new SelectList(house.List(), "Id", "House_Name");
            ViewBag.Grp = new SelectList(new ServiceLayer.AccountMaster.CustomerServicesLayer(_dashboardData.DbConnectionString).DDLBind("STOCKGROUP", ""), "Id", "Name");
            ViewModel.Category.StockGroup modelgroup;
            if (Id != 0)
            {
                modelgroup = _db.Find(Id);
               
            }
            else
            {
                modelgroup = new ViewModel.Category.StockGroup();
            }
            return View(modelgroup);
        }
        public ActionResult GetStockGroup(string searchText = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockGroupServicelayer(_dashboardData.DbConnectionString);

            var data = new ServiceLayer.AccountMaster.CustomerServicesLayer().DDLBind("STOCKGROUP", searchText);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(ViewModel.Category.StockGroup modelgroup)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockGroupServicelayer(_dashboardData.DbConnectionString);
            
            
          
            if (ModelState.IsValid==false || ModelState.IsValid==true)
            {
                if(modelgroup.Group.Id!=0)
                    _db.Update(modelgroup);
                else
                _db.Create(modelgroup);

                var lst = _db.List();

                return View("List",lst);
            }
            else
            {
                ViewBag.Grp = new SelectList(new ServiceLayer.AccountMaster.CustomerServicesLayer().DDLBind("STOCKGROUP", ""), "Id", "Name");
                return View(modelgroup);
            }
           
        }
        public ActionResult IsNameExists(int Id, string Name)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockGroupServicelayer(_dashboardData.DbConnectionString);

            return Json(_db.IsNameExists(Id, Name), JsonRequestBehavior.AllowGet);
        }
    }
}
