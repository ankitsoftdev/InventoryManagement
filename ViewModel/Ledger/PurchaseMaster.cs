using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ViewModel.Common;
namespace ViewModel.Ledger
{
    public class PurchaseMaster
    {
        public PurchaseMaster()
        {
            //SupplierList = new List<Common.DDLBind>();
            QuationList = new List<Common.DDLBind>();
            ItemList = new List<DDLBind>();
            ItemDetails = new List<Purchase_Tra>();
            UnitList = new List<DDLBind>();
            TaxList = new List<DDLBind>();
            listTax = new List<TaxDetails>();
            bindTax = new List<BindDdlTax>();
            itemddlbind = new List<ItemDDl>();
            purchase_tra = new Purchase_Tra();
        }
        public long orderId { get; set; }
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long Branch_Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public long Supplier_Id { get; set; }

        public String Supplier_Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime Purchase_Date { get; set; }
        public String Challan_Number { get; set; }
        public long Finance_Id { get; set; }
        public bool Is_Quotation { get; set; }
        public long Quotation_Id { get; set; }
        public String Remarks { get; set; }
        public decimal Grand_Total { get; set; }
        public decimal Discount { get; set; }
        public bool Is_Challan_Gen { get; set; }
        public int TaxId { get; set; }
        public decimal Tax_Amount { get; set; }
        public decimal Tax_Rate { get; set; }
        public decimal Discount_Rate { get; set; }
        public string Image_Path { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        //public virtual List<ViewModel.Common.DDLBind> SupplierList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> QuationList { get; set; }
        public virtual List<Purchase_Tra> ItemDetails { get; set; }
        public virtual List<ViewModel.Common.DDLBind> ItemList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> UnitList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> TaxList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> GoDownList { get; set; }

        public virtual List<BindDdlTax> bindTax { get; set; }
        public List<TaxDetails> listTax { get; set; }
        public List<ItemDDl> itemddlbind { get; set; }
        public Ledger_Common ledger_common { get; set; }
        public Purchase_Tra purchase_tra { get; set; }
    }
    public class Purchase_Tra
    {
        public Purchase_Tra()
        {
            Status = true;
            listpurchase_tra = new List<Purchase_Tra>();
            ledger_common = new Ledger_Common();
            taxdetails = new TaxDetails();
        }
        public long Id { get; set; }
        public String Challan_Number { get; set; }
        public string Unit_Name { get; set; }
        public long Item_Id { get; set; }
        public decimal Qty { get; set; }
        public Decimal Rate { get; set; }
        public long Unit_Id { get; set; }
        public long Tax_Id { get; set; }
        public long GoDownId { get; set; }
        public decimal Total_Amount { get; set; }
        public bool Status { get; set; }
        public string ItemName { get; set; }
        public string GoDownName { get; set; }
        public string ProductCode { get; set; }
        public string SerialNo { get; set; }
        public decimal GRTotal { get; set; }
        public List<Purchase_Tra> listpurchase_tra { get; set; }
        public List<DDLBind> ItemList { get; set; }
        public List<DDLBind> GoDownList { get; set; }
        public Ledger_Common ledger_common { get; set; }
        public TaxDetails taxdetails { get; set; }
        public decimal Grand_Total { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class Challan_Details
    {
        public long Id { get; set; }
        public String Supplier_Name { get; set; }
        public long SupplierId { set; get; }
        public string Purchase_Date { get; set; }
        public String Challan_Number { get; set; }
        public String Item_Name { get; set; }
        public long ItemId { set; get; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public string Unit_Name { get; set; }
        public string Tax { get; set; }
        public decimal Total_Amount { get; set; }
        public decimal TotalQty { get; set; }

        public String Remarks { get; set; }
        public decimal Grand_Total { get; set; }
        public decimal Discount { get; set; }
        public decimal ReturnQuantity { set; get; }

    }

    public class PurchaseList
    {
        public long Id { get; set; }
        public long Branch_Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Alias_Name { get; set; }
        public string Challan_No { get; set; }
        public string Purchase_Date { get; set; }
        public string Address { get; set; }
        public string Email_Id { get; set; }
        public string Contact_No { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public bool Is_Challan_Gen { get; set; }
    }

    public enum PurchaseTag
    {
        ItemWise = 1,
        QuotationWise = 2

    }
    public class PurchaseOrderItem
    {
        public PurchaseOrderItem()
        {
            listPurchaseOrderItem = new List<PurchaseOrderItem>();
        }
        public List<PurchaseOrderItem> listPurchaseOrderItem { get; set; }
        public long Id { get; set; }
        public long orderId { get; set; }
        public long SupplierId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string ProductCode { get; set; }
        public string SerialNo { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAMount { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
