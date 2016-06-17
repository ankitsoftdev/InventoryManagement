using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.AccountMaster;
using System.IO;
namespace InventoryManagement.Areas.AccountsMaster.Controllers
{
    public class CustomerController : InventoryBaseController
    {
  
        //
        // GET: /AccountsMaster/Customer/
        CustomerServicesLayer _objCustomerServicesLayer;
        public ActionResult Index(string Tagname = "")
        {
            ViewBag.Tagname = Tagname;
            return View();
        }

        public ActionResult List(string Tagname="")
        {
            ViewBag.Tagname = Tagname;
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
            return View(_objCustomerServicesLayer.GetCustomer(Tagname).ToList());
        }
        public ActionResult Create(long Id = 0, string Tagname="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
           _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
            ViewModel.Ledger.Supplier supplier;
            ViewBag.Tagname = Tagname;
            if (Id != 0)
            {
               // supplier = new  ViewModel.Ledger.Supplier ();
                supplier = _objCustomerServicesLayer.FindCustomer(Id, Tagname);

                ddlbind(supplier, Tagname);
            }
            else
            {
               
                supplier = new ViewModel.Ledger.Supplier();
                supplier.Code = _objCustomerServicesLayer.GEN_AccountsCode(Tagname);
                ddlbind(supplier, Tagname);
             
            }
            return View(supplier);
        }
  
        public JsonResult ddlBindState(int CountryId=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
             _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
             var StateList = _objCustomerServicesLayer.DDLList(2, CountryId).ToList().OrderBy(m=>m.Name);
             return Json(new SelectList(StateList.ToArray(),"Id","Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ddlBindCity(int StateId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
            var StateList = _objCustomerServicesLayer.ListCityDDL(StateId).ToList();
            return Json(new SelectList(StateList.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file1, ViewModel.Ledger.Supplier supplier, string Tagname = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
            if (supplier.Id==0)
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

                        supplier.Image_Path = "/Themes/img/StockItemImg/" + pic;

                    }
                }
                _objCustomerServicesLayer.Save(supplier, Tagname);
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

                        supplier.Image_Path = "/Themes/img/StockItemImg/" + pic;

                    }
                }
                _objCustomerServicesLayer.Update(supplier, Tagname);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("List", new { Tagname = Tagname });
        }
        public ActionResult IsEmailExists(string Tagname,string Email_Id="",int Id=0 )
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);

            return Json(_objCustomerServicesLayer.IsEmailIdExists(Tagname,Id,Email_Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int Id = 0, string Tagname="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
            var delete = _objCustomerServicesLayer.Delete(Id, Tagname);
            return Json(delete, JsonRequestBehavior.AllowGet);
        }
        public void ddlbind(ViewModel.Ledger.Supplier supplier,string Tag="")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
            if (supplier.Id != 0)
            {
                ViewBag.CountryList = new SelectList(_objCustomerServicesLayer.DDLList(1, 0).ToList(), "Id", "Name", supplier.Address.Country_Id);
                ViewBag.StateList = new SelectList(_objCustomerServicesLayer.DDLList(2, supplier.Address.Country_Id).ToList(), "Id", "Name", supplier.Address.State_Id);
                ViewBag.CityList = new SelectList(_objCustomerServicesLayer.ListCityDDL(supplier.Address.State_Id).ToList(), "Id", "Name", supplier.Address.City_Id);
                ViewBag.GroupName = new SelectList(_objCustomerServicesLayer.DDlGroupLList(Tag).ToList(), "Id", "Name", supplier.Group_Id);

            }
            else
            {
                List<ViewModel.Common.DDLBind> ddl = new List<ViewModel.Common.DDLBind>();
                ViewBag.CountryList = new SelectList(_objCustomerServicesLayer.DDLList(1, 0).ToList(), "Id", "Name", supplier.Address.Country_Id);
                ViewBag.StateList = new SelectList(ddl, "Id", "Name", supplier.Address.State_Id);
                ViewBag.CityList = new SelectList(ddl, "Id", "Name", supplier.Address.City_Id);
                ViewBag.GroupName = new SelectList(_objCustomerServicesLayer.DDlGroupLList(Tag).ToList(), "Id", "Name", supplier.Group_Id);
            }

        }



     
        public ActionResult ViewCustomerSupp(int Id=0, string Tagname = "")
            {
                var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
                _objCustomerServicesLayer = new CustomerServicesLayer(_dashboardData.DbConnectionString);
            ViewModel.Ledger.Supplier supplier=new ViewModel.Ledger.Supplier();
            ViewBag.Tagname = Tagname;
            if (Id!=0)
            {
               // supplier = new ViewModel.Ledger.Supplier();
                supplier = _objCustomerServicesLayer.FindCustomer(Id, Tagname);

                ddlbind(supplier);
            }
                
           
            else
            {
                var datalst = _objCustomerServicesLayer.GetCustomer(Tagname).ToList();
                if (datalst.Count > 0)
                {
                 var   data = datalst.OrderBy(x => x.Id).FirstOrDefault();
                    if (data != null)
                        supplier = _objCustomerServicesLayer.FindCustomer(data.Id, Tagname);
                }
                else
                supplier = new ViewModel.Ledger.Supplier();
            }

            return PartialView(supplier);
        }
       
    }
}
