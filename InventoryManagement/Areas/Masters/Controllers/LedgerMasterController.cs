using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Common;
using ServiceLayer.Masters;
using HotelManagementErp_Main.Helper;
namespace InventoryManagement.Areas.Masters.Controllers
{
    public class LedgerMasterController : InventoryBaseController
    {
        //
        // GET: /Masters/LedgerMaster/
        LedgerMasterServiceLayer _db;
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult List()
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);
            var ledgrlist = _db.List();
            return View(ledgrlist);
        }
        [HttpGet]
        public ActionResult Create(int Id=0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);
            LedgerMasterInfo modelledmaster;
           
            
            if(Id!=0)
            {
                modelledmaster = _db.Find(Id);
            }
            else
            {
                modelledmaster = new LedgerMasterInfo();
            }
           // modelledmaster.GroupList = _db.DDLBind("Ledger_Group", "");
            ddlbind(modelledmaster);
            return View(modelledmaster);
        }
        [HttpPost]
        public ActionResult Create(LedgerMasterInfo modelledmaster)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);
            if(ModelState.IsValid )
            {
                if(modelledmaster.Id!=0)
                {
                    _db.Update(modelledmaster);
                    
                }
                else
                {
                    _db.Create(modelledmaster);
                }
                var ledgrlist = _db.List();
                return View("List",ledgrlist);
            }
            modelledmaster.GroupList = _db.DDLBind("Ledger_Group", "");
            return View(modelledmaster);

        }
        public ActionResult Delete(int Id = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);
            bool result = false;
            if (Id > 0)
            {
                result = _db.Delete(Id);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ddlBindState(int CountryId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);
        
            var StateList = _db.DDLCountryState(2, CountryId).ToList().OrderBy(m => m.Name);
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ddlBindCity(int StateId = 0)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);
            var StateList = _db.ListCityDDL(StateId).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult IsNameExists(int Id, string Name)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);
        
         
            return Json(!_db.IsNameExists(Id, Name), JsonRequestBehavior.AllowGet);
        }
        private void ddlbind(LedgerMasterInfo modelledmaster)
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new LedgerMasterServiceLayer(_dashboardData.DbConnectionString);

            if (modelledmaster.Id != 0)
            {
                modelledmaster.CountryList = _db.DDLCountryState(1, 0).ToList();
                modelledmaster.StateList = modelledmaster.Country_Id > 0 ? _db.DDLCountryState(2, modelledmaster.Country_Id??0).ToList() : new List<DDLBind>();
                //modelledmaster.StateList = _db.DDLCountryState(2, 0).ToList();
                modelledmaster.CityList = modelledmaster.State_Id > 0 ? _db.ListCityDDL(modelledmaster.State_Id??0).ToList() : new List<DDLBind>();
                modelledmaster.GroupList = _db.DDLBind("Ledger_Group", "");

            }
            else
            {
                modelledmaster.CountryList = _db.DDLCountryState(1, 0).ToList();
                modelledmaster.StateList = new List<DDLBind>();
                modelledmaster.CityList = new List<DDLBind>();
                modelledmaster.GroupList = _db.DDLBind("Ledger_Group", "");
            }

        }
    }
}
