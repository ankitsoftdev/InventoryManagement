using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.   Mvc;

using ServiceLayer;
using HotelManagementErp_Main.Helper;



namespace HotelManagementErp_Main.Areas.Inventory.Controllers
{
   
    public class InventoryController : InventoryBaseController
    {
         //
        // GET: /Inventory/Inventory/
       
         public ActionResult InventoryDash()
        {
            var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;
            return View();
        }

    

        //By Shivi

        //public ActionResult PartlTransactionSummary(string search="",string Tag="Restaurant")
        //{          
        //    ViewBag.tag = "";
        //    var info = (HDMEntity.DashBoard)ViewBag.LoginInfo;
        //    SupplierService SUPMST = new SupplierService(info.Hotel_Id, info.Branch_Id, info.Employee_Id);
        //    return PartialView(SUPMST.RequestList(search).Where(m => m.Tag_Name == Tag).ToList());
        //}

      
    }
}
