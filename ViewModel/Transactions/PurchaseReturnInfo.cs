using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
using System.ComponentModel.DataAnnotations;
namespace ViewModel.Transactions
{
   public class PurchaseReturnInfo
    {
       public PurchaseReturnInfo()
       {
           ReturnDetails = new List<PurchaseReturnInfo_Tra>();
           ItemList = new List<DDLBind>();
           SupplierList = new List<DDLBind>();
           BillList = new List<DDLBind>();
       }
        public long Id { get; set; }
        public long Purchase_Id { get; set; }
       [Required(ErrorMessage="Required")]
        public long Supplier_Id { get; set; }
        public long Item_Id { get; set; }
        public Decimal Qty { get; set; }
        
        public decimal Rate { get; set; }
        public string Unit_Name { get; set; }
        public string Amt { get; set; }
        public string VC_No { get; set; }
        public string Customer_Name { get; set; }
        public string Email { get; set; }
        public string Contact_No { get; set; }
        public Nullable<decimal> Amount { get;set; }
        public decimal Discount { get; set; }
        public System.DateTime Return_Date { get; set; }
        public string Remarks { get; set; }
        public bool Is_Rejection { get; set; }
        public virtual List<PurchaseReturnInfo_Tra> ReturnDetails { get; set; }
        public virtual List<DDLBind> ItemList { get; set; }
        public virtual List<DDLBind> SupplierList { get; set; }
        public virtual List<DDLBind> BillList { get; set; }
        
    }
    public class PurchaseReturnList
    {
        public long Id { get; set; }
        public long Supplier_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
        public string Contact_No { get; set; }
        public string Email { get; set; }
        public string VC_No { get; set; }
        
        public Nullable<decimal> Amount { get; set; }
        public string Return_Date { get; set; }
  
        public string Remarks { get; set; }
        public string Status { get; set; }

    }

    public class PurchaseReturnInfo_Tra
    {
        public PurchaseReturnInfo_Tra()
        {
            Status = true;
        }
        public long Id { get; set; }
        public long PurRet_Id { get; set; }
        public long Item_Id { get; set; }
        public string Item_Name { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Unit_Name { get; set; }
        public string Remarks { get; set; }
        public string Sale_Serial_No { get; set; }
        public bool Status { get; set; }
        public int No_of_Decimal { get; set; }
        

    }
}
