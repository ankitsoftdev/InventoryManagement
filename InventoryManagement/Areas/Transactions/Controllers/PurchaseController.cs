using HotelManagementErp_Main.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Ledger;
using ServiceLayer.Transactions;
using ViewModel.Transactions;
using ViewModel.Common;
namespace InventoryManagement.Areas.Transactions.Controllers
{
    public class PurchaseController : InventoryBaseController
    {
        //
        // GET: /Transactions/Purchase/
        PurchaseServiceLayer _db;
        public ActionResult Index()
        {
            _db = new PurchaseServiceLayer();
            return View();
        }
        public ActionResult List()
        {
            _db = new PurchaseServiceLayer();
            var lst = _db.List();

            return View(lst);
        }

        public ActionResult ViewDetails(int Id = 0, string Challan_No = "")
        {
            _db = new PurchaseServiceLayer();
            var lst = _db.ViewDetails(Id, Challan_No);


            return View(lst);
        }
         
        public ActionResult Create(int Id = 0 )
        {
            //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            PurchaseMaster ModelPurMaster=new PurchaseMaster ();
            ServiceLayer.InventoryMaster.StockServiceLayer _stockSL = new ServiceLayer.InventoryMaster.StockServiceLayer();
            ModelPurMaster.purchase_tra.ledger_common = new Ledger_Common();
            Ledger_Common ddl = BindDDL(ModelPurMaster.purchase_tra.ledger_common); 
           //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            _db = new PurchaseServiceLayer();
            if (Id != 0)
            {
                ModelPurMaster = _db.Find(Id);

                ModelPurMaster.TaxList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Supplier", "");
                //  ModelPurMaster.ItemDetails.Add(new Purchase_Tra());
                //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                long Challan_No = _db.Find(ModelPurMaster.Id).Id;
                ModelPurMaster.purchase_tra.listpurchase_tra = _db.ItemDetails(Challan_No);
                ModelPurMaster.purchase_tra.Grand_Total = ModelPurMaster.Grand_Total;
                //ModelPurMaster.listTax.Add(new ViewModel.Common.TaxDetails());
                ViewBag.ChalanNo =  ModelPurMaster.purchase_tra.listpurchase_tra.Count>0? ModelPurMaster.purchase_tra.listpurchase_tra[0].Challan_Number :null;
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            }
            else
            {
                PurchaseOrderItem PurchaseOrder = (PurchaseOrderItem)TempData["PurchaseOrderDetails"];
                if (PurchaseOrder != null)
                {

                    PurchaseOrder.listPurchaseOrderItem.ForEach((x) => ModelPurMaster.purchase_tra.listpurchase_tra.Add(new Purchase_Tra { Id = x.Id, Item_Id = x.ItemId, ItemName = ddl.bindDDl.SingleOrDefault(i => i.Id == x.ItemId).Name, Qty = x.Quantity, Rate = x.Rate, ProductCode = x.ProductCode, SerialNo = x.SerialNo,Unit_Name=x.UnitName }));
                    ModelPurMaster.purchase_tra.Grand_Total = PurchaseOrder.TotalAMount;
                    ModelPurMaster.Supplier_Id = PurchaseOrder != null ? PurchaseOrder.SupplierId : ModelPurMaster.Supplier_Id;
                    ModelPurMaster.orderId = PurchaseOrder != null ? PurchaseOrder.orderId : ModelPurMaster.orderId;
                }
                // ModelPurMaster.ItemDetails.Add(new Purchase_Tra());
               

                ModelPurMaster.UnitList = new List<ViewModel.Common.DDLBind>();
                ModelPurMaster.Challan_Number = _db.GEN_ChallanNo();
                ModelPurMaster.Purchase_Date = DateTime.Today;
                ModelPurMaster.TaxList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Supplier", "");
                //ModelPurMaster = new ServiceLayer.Transactions.PurchaseServiceLayer().BindOrderItemWithPurchaseMaster(OrderedItem);
                ViewBag.ChalanNo = null;
               // var ddl = BindDDL(ModelPurMaster.purchase_tra.ledger_common);
            }

           
            // BindDDL(ModelPurMaster.ledger_common); 
             BindDDL(ModelPurMaster.purchase_tra.ledger_common);
            //Tax Details Code

            ModelPurMaster.purchase_tra.taxdetails.listTaxDetails.Add(new ViewModel.Common.TaxDetails());

            if (Id > 0)
            {
                ModelPurMaster.purchase_tra.taxdetails.listTaxDetails = _db.GetTaxList(Id); 
            }
                

            ModelPurMaster.purchase_tra.taxdetails.TaxDeductionList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Tax_Deduction_Master", "");
            ModelPurMaster.purchase_tra.taxdetails.Grand_Total = ModelPurMaster.purchase_tra.Grand_Total;
            ModelPurMaster.purchase_tra.Id = ModelPurMaster.Id;
            return View(ModelPurMaster);
        }
        [HttpPost]
        public ActionResult Create(PurchaseMaster ModelPurMaster)
        {
            _db = new PurchaseServiceLayer();

            if (ModelState.IsValid == true)
            { 
                if (ModelPurMaster.Id != 0)
                {
                    _db.Update(ModelPurMaster);
                }
                else
                {
                    _db.Create(ModelPurMaster);

                }

                var lst = _db.List();

                return RedirectToAction("index");
            }
            return View(ModelPurMaster);
        }

        public ActionResult GetUnitName(int Id = 0)
        {
            _db = new PurchaseServiceLayer();
            ServiceLayer.InventoryMaster.StockServiceLayer _stockSL = new ServiceLayer.InventoryMaster.StockServiceLayer();
            ServiceLayer.Masters.UnitsServiceLayer _unitSL = new ServiceLayer.Masters.UnitsServiceLayer();
            //var stock = _stockSL.Find(Id);
            //var data = _unitSL.DDLBind().SingleOrDefault(x => x.Id == stock.Unit_Id);
            var data = _stockSL.GetUnitAndAmount(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplier(string searchText = "")
        {
            var _dashboardData = ViewBag.LoginInfo as HDMEntity.DashBoard;
            _db = new PurchaseServiceLayer();

            var data = new ServiceLayer.Common.CommonServiceLayer(_dashboardData.DbConnectionString).DDLBind("SUPPLIER", searchText);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GentChallanOfPurchase(int Id)
        {
            _db = new PurchaseServiceLayer();

            var data = _db.Gen_ChallanOfPurchse(Id);

            var lst = _db.List();

            return View("List", lst);
        }
        public ActionResult ItemList(PurchaseMaster modal)
        {
           // modal.ledger_common = new Ledger_Common();
            _db = new PurchaseServiceLayer();
            ServiceLayer.InventoryMaster.StockServiceLayer _stockSL = new ServiceLayer.InventoryMaster.StockServiceLayer();
            var ddl = BindDDL(modal.purchase_tra.ledger_common);
            if (modal.Id != 0)
            {
                var Challan_No = _db.Find(modal.Id).Id;
                modal.purchase_tra.listpurchase_tra = _db.ItemDetails(Challan_No);
                modal.purchase_tra.Grand_Total = modal.Grand_Total;
                //ModelPurMaster.listTax.Add(new ViewModel.Common.TaxDetails());
                ViewBag.ChalanNo = modal.purchase_tra.listpurchase_tra[0].Challan_Number;
            }
            else
            {
                PurchaseOrderItem PurchaseOrder = (PurchaseOrderItem)TempData["PurchaseOrderDetails"];
                if (PurchaseOrder != null)
                {

                    PurchaseOrder.listPurchaseOrderItem.ForEach((x) => modal.purchase_tra.listpurchase_tra.Add(new Purchase_Tra { Id = x.Id, Item_Id = x.ItemId, ItemName = ddl.bindDDl.SingleOrDefault(i => i.Id == x.ItemId).Name, Qty = x.Quantity, Rate = x.Rate, ProductCode = x.ProductCode, SerialNo = x.SerialNo }));
                    modal.purchase_tra.Grand_Total = PurchaseOrder.TotalAMount;
                }
                else
                {
                    //  ModelPurMaster.ItemDetails.Add(new Purchase_Tra());
                    ViewBag.ChalanNo = null;
                }
            }
            // BindDDL(ModelPurMaster);
            //ModelPurMaster.ItemList = _stockSL.DDLBind();
            return View(modal.purchase_tra);
        }
        public ActionResult Challan_Details(long Challan_No = 0)
        {
            PurchaseMaster ModelPurMaster = new PurchaseMaster();
            _db = new PurchaseServiceLayer();

            if (Challan_No!=0)
            {

                ModelPurMaster.ItemDetails = _db.ItemDetails(Challan_No);
            }

            return View("ViewDetails", ModelPurMaster);
        }
        #region PurchageReturn
        public ActionResult PurchageReturn(List<Challan_Details> PurchageReturn)
        {
            _db = new PurchaseServiceLayer();
            _db.PurchageReturn(PurchageReturn);
            return RedirectToAction("List");
        }
        #endregion PurchageReturn

        private Ledger_Common BindDDL(Ledger_Common ledger_common)
        {
           // ledger_common = new Ledger_Common();
            if (ledger_common != null)
            {
                 ledger_common.bindTax = new ServiceLayer.Masters.TaxServiceLayer().DDLBind();               
                ledger_common.bindDDl = new ServiceLayer.InventoryMaster.StockServiceLayer().DDLBind();
                ledger_common.GoDownList = new ServiceLayer.Masters.GoDownServiceLayer().DDLBind();
                ledger_common.itemddlbind = new ServiceLayer.InventoryMaster.StockServiceLayer().DDLItemBind();
            }
            return ledger_common;
        }

        public JsonResult GetMaxid(int ItemId)
        {
            var maxid = new ServiceLayer.InventoryMaster.StockServiceLayer().GEN_MAXId(ItemId);
            return Json(maxid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TaxDeductionDetails(long ChallanNo, Purchase_Tra tra)
        {
            _db = new PurchaseServiceLayer();
          

          tra.taxdetails.listTaxDetails.Add(new ViewModel.Common.TaxDetails());
            if (ChallanNo!=0)
            {
                tra.taxdetails.listTaxDetails = _db.GetTaxList(ChallanNo);

            }
            else
            {

                //objPurMaster.TaxList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Tax_Deduction_Master", "");

            }

            tra.taxdetails.TaxDeductionList = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Tax_Deduction_Master", "");
            tra.taxdetails.Grand_Total = tra.Grand_Total;

            return View(tra.taxdetails);
        }

        public ActionResult DeletePurchase(int Id)
        {
            _db = new PurchaseServiceLayer();
            var i = _db.DeletePurchase(Id);
            if (i > 0)
            {
                return RedirectToAction("List");
            }
            return null;
        }

        public ActionResult _ViewPurcaseItem(int Id, long ChalanNo = 0)
        {
            _db = new PurchaseServiceLayer();
            PurchaseMaster modal = new PurchaseMaster();
            modal = _db.Find(Id);
           
            modal.ItemDetails = _db.ItemDetails(ChalanNo);
            return PartialView(modal);
        }

        public JsonResult BindTax()
        {
            var list = new ServiceLayer.Common.CommonServiceLayer().DDLBind("Tax_Deduction_Master", "");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindGodown()
        {
            var list =  new ServiceLayer.Masters.GoDownServiceLayer().DDLBind();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
