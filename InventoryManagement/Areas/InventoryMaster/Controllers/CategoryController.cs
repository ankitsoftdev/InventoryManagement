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
    public class CategoryController : InventoryBaseController
    {
        //
        // GET: /Masters/Category/
        StockCategoryServiceLayer _db;
        public ActionResult Index()
        {
        var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockCategoryServiceLayer(_dashboardData.DbConnectionString);
           
            return View();
        }
        public ActionResult List(string search = "")
        {
            var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;
            _db = new StockCategoryServiceLayer(info.DbConnectionString);
            var lst = _db.List();

            return View(lst);

         
        }
        public ActionResult Create(long Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockCategoryServiceLayer(_dashboardData.DbConnectionString);
            StockCategory Category;
           // ViewBag.Category = new SelectList(new ServiceLayer.AccountMaster.CustomerServicesLayer().DDLBind("STOCKCATEGORY", ""), "Id", "Name");
            if (Id != 0)
            {
                Category =_db.Find(Id);
            }
            else
            {
                Category = new StockCategory();
            }
            Category.Category.ListUnder = _db.DDLBind();
            return View(Category);
        }
        [HttpPost]
        public ActionResult Create(ViewModel.Category.StockCategory ModelCategory)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
             _db = new StockCategoryServiceLayer(_dashboardData.DbConnectionString);
            if (ModelState.IsValid || ModelState.IsValid==false)
            {
                if (ModelCategory.Category.Id != 0)
                {
                    _db.Update(ModelCategory);
                }
                else
                {
                    _db.Create(ModelCategory);
                }


                var lst = _db.List();

                return View("List",lst);
                //return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.Category = new SelectList(new ServiceLayer.AccountMaster.CustomerServicesLayer().DDLBind("STOCKCATEGORY", ""), "Id", "Name");
                ModelCategory.Category.ListUnder = _db.DDLBind();
                return View(ModelCategory);
            }
            
        }

        public ActionResult IsNameExists(int Id, string Name)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new StockCategoryServiceLayer(_dashboardData.DbConnectionString);

            return Json(_db.IsNameExists(Id,Name), JsonRequestBehavior.AllowGet);
        }
    }
}
