using HotelManagementErp_Main.Helper;
using ServiceLayer.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Ledger;

namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class SalesController : InventoryBaseController
    {
        SalesServiceLayer _db;
       // PurchaseServiceLayer _db;
        public ActionResult Index()
        {
            _db = new SalesServiceLayer();
            return View();
        }
        public ActionResult List()
        {
            _db = new SalesServiceLayer();
            var lst = _db.List();

            return View(lst);
        }

        public ActionResult Create(long Id = 0)
        {
            SalesMaster ModelSalMaster;
            _db = new SalesServiceLayer();
            var Challan_No = "";
            if (Id != 0)
            {
                ModelSalMaster = _db.Find(Id);
                //  ModelPurMaster.ItemDetails.Add(new Purchase_Tra());

                // Sales Purchase_tra code here
                  Challan_No = ModelSalMaster.Challan_Number;

                ModelSalMaster.sales_tra.listpurchase_tra = _db.ItemDetails(Id);
                //ModelSalMaster.sales_tra.listpurchase_tra = new PurchaseServiceLayer.ItemDetails(Challan_No);
                ModelSalMaster.sales_tra.Grand_Total = ModelSalMaster.Grand_Total;
                ModelSalMaster.listSalesTax.Add(new SalesTax());
                //Challan_No = ModelSalMaster.ItemDetails[0].Challan_Number;
                Challan_No = ModelSalMaster.sales_tra.listpurchase_tra.Count>0? ModelSalMaster.sales_tra.listpurchase_tra[0].Challan_Number:null;

            }
            else
            {
                ModelSalMaster = new SalesMaster(); 
                ModelSalMaster.UnitList = new List<ViewModel.Common.DDLBind>();
                ModelSalMaster.Challan_Number = _db.GEN_ChallanNo();
                ModelSalMaster.Purchase_Date = DateTime.Today;
                Challan_No = null;
            }
            ModelSalMaster.ItemList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Customer", "");
            ModelSalMaster.sales_tra.ledger_common.itemddlbind=new ServiceLayer.InventoryMaster.StockServiceLayer().ddlBindSaleItem();
            ModelSalMaster.sales_tra.taxdetails.listTaxDetails.Add(new ViewModel.Common.TaxDetails());

            if (Id>0)
            {
                //ModelSalMaster.sales_tra.taxdetails.listTaxDetails = new ServiceLayer.Transactions.PurchaseServiceLayer().GetTaxList(Challan_No);
                ModelSalMaster.sales_tra.taxdetails.listTaxDetails = _db.GetTaxList(Id);
            }
            ModelSalMaster.sales_tra.taxdetails.TaxDeductionList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Tax_Deduction_Master", "");
            ModelSalMaster.sales_tra.taxdetails.Grand_Total = ModelSalMaster.sales_tra.Grand_Total;
            ModelSalMaster.sales_tra.Id = ModelSalMaster.Id;
            return View(ModelSalMaster);
        }
        [HttpPost]
        public ActionResult Create(SalesMaster ModelSalMaster)
        {
            _db = new SalesServiceLayer();
            if (ModelState.IsValid == true )
            {
                if (ModelSalMaster.Id != 0)
                {
                   _db.Update(ModelSalMaster);
                }
                else
                {
                    _db.Create(ModelSalMaster);

                }

                var lst = _db.List();

                return View("List", lst);
            }
            return View(ModelSalMaster);
        }
        public ActionResult ViewDetails(int Id)
        {
            _db = new SalesServiceLayer();
            var lst = _db.ViewDetails(Id);


            return View(lst);
        }
        public ActionResult GentChallanOfSales(int Id)
        {
            _db = new SalesServiceLayer();

            var data = _db.Gen_ChallanOfSales(Id);

            var lst = _db.List();

            return View("List", lst);
        }
        public ActionResult GetUnitName(int Id = 0)
        {
            _db = new SalesServiceLayer();
            ServiceLayer.InventoryMaster.StockServiceLayer _stockSL = new ServiceLayer.InventoryMaster.StockServiceLayer();
            ServiceLayer.Masters.UnitsServiceLayer _unitSL = new ServiceLayer.Masters.UnitsServiceLayer();
            var stock = _stockSL.Find(Id);
            var data = _unitSL.DDLBind().SingleOrDefault(x => x.Id == stock.Unit_Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStockItemDetails(int Id = 0)
        {
            _db = new SalesServiceLayer();
            ServiceLayer.Transactions.StockMasterServiceLayer _objStockMaster=new StockMasterServiceLayer();
            var data = _objStockMaster.StockMasterDDL(Id);

            
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustomer(string searchText = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new SalesServiceLayer();

            var data = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("CUSTOMER", searchText);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ItemList(int Id = 0, int Gtotal=0)
        //{
        //    SalesMaster ModelSalMaster = new SalesMaster();
        //    _db = new SalesServiceLayer();
        //    ServiceLayer.InventoryMaster.StockServiceLayer _stockSL = new ServiceLayer.InventoryMaster.StockServiceLayer();
        //    if (Id != 0)
        //    {
        //        var Challan_No = _db.Find(Id).Challan_Number;

        //        ModelSalMaster.sales_tra.listpurchase_tra = _db.ItemDetails(Challan_No);
        //        ModelSalMaster.Grand_Total = Gtotal;
        //        ModelSalMaster.listSalesTax.Add(new SalesTax());
        //        ViewBag.ChalanNo = ModelSalMaster.ItemDetails[0].Challan_Number;
        //    }
        //    else
        //    {
        //        //ModelSalMaster.ItemDetails.Add(new Sales_Tra());
        //        ViewBag.ChalanNo = null;
        //    }
        //    ModelSalMaster.bindItemddl = new ServiceLayer.InventoryMaster.StockServiceLayer().ddlBindSaleItem();
        //    return View(ModelSalMaster);
        //}

        public ActionResult _TaxList(string ChallanNo, decimal GTotal)
        {
            SalesMaster model = new SalesMaster();
            model.listSalesTax.Add( new  SalesTax());

            if (!string.IsNullOrEmpty(ChallanNo))
            {
               // model.listSalesTax = new ServiceLayer.InventoryMaster.StockServiceLayer().GetTaxList(ChallanNo);

            }
            return PartialView(model);

        }
        #region PurchageReturn
        public ActionResult SalesReturn(List<Challan_Details> PurchageReturn)
        {
            _db = new SalesServiceLayer();
            _db.PurchageReturn(PurchageReturn);
            return RedirectToAction("List");
        }
        #endregion PurchageReturn

        public ActionResult Delete(int Id)
        {
            _db = new SalesServiceLayer();
            int i = _db.DeleteSalesMaster(Id);
            return RedirectToAction("Index");
        }

        public JsonResult GetSerial_no(int Id, int Qty, string SrNo)
        { 
            _db = new SalesServiceLayer();
            var lst = _db.GetSerial_No(Id, Qty, SrNo);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}
