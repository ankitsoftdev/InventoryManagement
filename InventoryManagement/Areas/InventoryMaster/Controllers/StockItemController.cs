using DataLayer.InventoryMaster;
using HotelManagementErp_Main.Helper;
using ServiceLayer.InventoryMaster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Stock;

namespace InventoryManagement.Areas.InventoryMaster.Controllers
{
    public class StockItemController : InventoryBaseController
    {
        //
        // GET: /InventoryMaster/StockItem/
        StockServiceLayer objStockServiceLayer;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string search = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            objStockServiceLayer = new StockServiceLayer(_dashboardData.DbConnectionString);
            var data = objStockServiceLayer.GetStock().ToList();
            return View(data);
        }
        public ActionResult Create(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            objStockServiceLayer = new StockServiceLayer(_dashboardData.DbConnectionString);

            StockItem stock;
           
            if (Id != 0)
            {
                stock = new StockItem();
                stock = objStockServiceLayer.Find(Id);
                DdlBind(stock);
            }
            else
            {
                stock = new StockItem();
                stock.Item_Code = objStockServiceLayer.GEN_ItemCode();
                DdlBind(stock);
               
            }
            return View(stock);
        }
        private void DdlBind(StockItem stock)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            stock.UnitList =new ServiceLayer.Masters.UnitsServiceLayer(_dashboardData.DbConnectionString).DDLBind();
            stock.CategoryList = new StockCategoryServiceLayer(_dashboardData.DbConnectionString).DDLBind();
            stock.GroupList = new StockGroupServicelayer(_dashboardData.DbConnectionString).DDLlBind();
        }
        [HttpPost]
        public ActionResult Create(ViewModel.Stock.StockItem modelStockitem, HttpPostedFileBase file1)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            objStockServiceLayer = new StockServiceLayer(_dashboardData.DbConnectionString);
            if (ModelState.IsValid)
             {
                if (modelStockitem.Id != 0)
                {

                    if (file1 != null)
                    {
                        var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };

                        var ext = Path.GetExtension(file1.FileName);
                        if (allowedExtensions.Contains(ext))
                        {

                            string pic = System.IO.Path.GetFileName(file1.FileName);
                            string path = System.IO.Path.Combine(Server.MapPath("~/Themes/img/StockItemImg"), pic);

                            file1.SaveAs(path);

                            modelStockitem.Image_Path = "/Themes/img/StockItemImg/" + pic;

                        }
                    }
                    objStockServiceLayer.Update(modelStockitem);
                }
                else
                {

                    if (file1 != null)
                    {
                        var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };

                        var ext = Path.GetExtension(file1.FileName);
                        if (allowedExtensions.Contains(ext))
                        {

                            string pic = System.IO.Path.GetFileName(file1.FileName);
                            string path = System.IO.Path.Combine(Server.MapPath("~/Themes/img/StockItemImg"), pic);

                            file1.SaveAs(path);

                            modelStockitem.Image_Path = "/Themes/img/StockItemImg/" + pic;

                        }
                    }
                    objStockServiceLayer.Create(modelStockitem);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            DdlBind(modelStockitem);
            return View(modelStockitem);
        }
        public JsonResult Delete(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            objStockServiceLayer = new StockServiceLayer(_dashboardData.DbConnectionString);
            var delete = objStockServiceLayer.Delete(Id);
            return Json(delete, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsNameExists(int Id, string Name)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;

            objStockServiceLayer = new StockServiceLayer(_dashboardData.DbConnectionString);

            return Json(!objStockServiceLayer.IsNameExists(Id, Name), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewStockItem(int Id )
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            objStockServiceLayer = new StockServiceLayer(_dashboardData.DbConnectionString);

            StockItem stock;

            if (Id >= 0)
            {
                stock = new StockItem();
                stock = objStockServiceLayer.Find(Id);
                DdlBind(stock);
            }
            else
            {
                stock = new StockItem();
                stock.Item_Code = objStockServiceLayer.GEN_ItemCode();
                DdlBind(stock);

            }
            return PartialView(stock);
        }
    }
}
