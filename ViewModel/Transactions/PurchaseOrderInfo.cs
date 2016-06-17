using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
namespace ViewModel.Transactions
{
    public class PurchaseOrderInfo
    {

        public PurchaseOrderInfo()
       {
           OrderDetails = new List<PurchaseOrderInfo_Tra>();
           ItemList = new List<DDLBind>();
           SelsPersonList = new List<DDLBind>();
           SupplierList = new List<DDLBind>();
       }
        public long Id { get; set; }
        public long orderId { get; set; }
        public long Supplier_Id { get; set; }
        public string Order_No { get; set; }
        public long Sales_Person { get; set; }
        public long Quation_Id { get; set; }
        public string Supplier_Name { get; set; }
        public string Email { get; set; }
        public string Contact_No { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public System.DateTime Order_Date { get; set; }
        public System.DateTime Document_Date { get; set; }
        public System.DateTime Request_Delivery_Date { get; set; }
        public string External_DocumentNo { get; set; }
        public string Remarks { get; set; }
        public string Image_Path { get; set; }
        public bool Order_Status { get; set; }
        public bool Is_ReceiptNote { get; set; }
        public virtual List<PurchaseOrderInfo_Tra> OrderDetails { get; set; }
        public virtual List<DDLBind> ItemList { get; set; }
        public virtual List<DDLBind> SelsPersonList { get; set; }
        public virtual List<DDLBind> SupplierList { get; set; }

        
    }
    public class PurchaseOrderInfo_Tra
    {
        public PurchaseOrderInfo_Tra()
        {
            Status = true;
        }
        public long Id { get; set; }
        public string Order_No { get; set; }
        public long Item_Id { get; set; }
        public string Item_Name { get; set; }
        public Nullable<decimal> Oreder_Quantity { get; set; }
        public Nullable<decimal> Ship_Quantity { get; set; }
        public Nullable<decimal> Invoice_Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Unit_Name { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }

    }
    public class PurchaseOrdeList
    {
        public long Id { get; set; }
        public long Supplier_Id { get; set; }
        public string Supplier_Name { get; set; }
        public string Address { get; set; }
        public string Contact_No { get; set; }
        public string Email { get; set; }
        public string Order_No { get; set; }
        public long Sales_Person { get; set; }
        public string Sales_Person_Name { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime Order_Date { get; set; }
        public System.DateTime Request_Delivery_Date { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public bool Is_RecieptNoe { get; set; }
      

    }
}
