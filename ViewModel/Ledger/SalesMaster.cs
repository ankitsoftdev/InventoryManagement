using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Ledger
{
    public class SalesMaster
    {
        public SalesMaster()
        {
            QuationList = new List<Common.DDLBind>();
            ItemList = new List<DDLBind>();
            ItemDetails = new List<Sales_Tra>();
            UnitList = new List<DDLBind>();
            bindItemddl = new List<ItemDDl>();
            listSalesTax = new List<SalesTax>();
            sales_tra = new Purchase_Tra();
        }
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long Branch_Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public long Customer_Id { get; set; }

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

        public virtual List<ViewModel.Common.DDLBind> QuationList { get; set; }
        public virtual List<Sales_Tra> ItemDetails { get; set; }
        public virtual List<ViewModel.Common.DDLBind> ItemList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> UnitList { get; set; }
        public virtual List<ItemDDl> bindItemddl { get; set; }
        public List<SalesTax> listSalesTax { get; set; }
        public Purchase_Tra sales_tra { get; set; }

    }
    public class Sales_Tra
    {
        public Sales_Tra()
        {
            Status = true;
        }
        public long Id { get; set; }
        public String Challan_Number { get; set; }
        public string Unit_Name { get; set; }
        public long Item_Id { get; set; }
        public string ItemName { get; set; }
        public decimal Qty { get; set; }
        public Decimal Rate { get; set; }
        public long Unit_Id { get; set; }
        public long Tax_Id { get; set; }
        public long GoDownId { get; set; }
        //public decimal Total_Amount { get; set; }
        public bool Status { get; set; }
        public decimal AvailableQty { get; set; }
        public decimal Selling_Rate { get; set; }

    }
    public class StockMasterDDL
    {
        public long Id { get; set; }
        public decimal Qty { get; set; }
        public decimal SellingRate { get; set; }
    }
    public enum SaleTag
    {
        ItemWise = 1,
        QuotaionWise = 2
    }

    public class SalesTax
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rate_Type { get; set; }
        public string Tag_Type { get; set; }
        public string Value { get; set; }
    }
}
