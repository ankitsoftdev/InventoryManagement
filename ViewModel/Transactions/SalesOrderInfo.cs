using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
namespace ViewModel.Transactions
{
   public class SalesOrderInfo
    {
       public SalesOrderInfo()
       {
           OrderDetails = new List<SalesOrderInfo_Tra>();
           ItemList = new List<DDLBind>();
           SelsPersonList = new List<DDLBind>();
           CustomerList = new List<DDLBind>();
       }
        public long Id { get; set; }

        public long Customer_Id { get; set; }
        public string Order_No { get; set; }
        public long Sales_Person { get; set; }
        public long Quation_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Email { get; set; }
        public string Contact_No { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public System.DateTime Order_Date { get; set; }
        public System.DateTime Document_Date { get; set; }
        public System.DateTime Request_Delivery_Date { get; set; }
        public string External_DocumentNo { get; set; }
        public string Remarks { get; set; }
        public bool Order_Status { get; set; }
        public bool Is_CreditNote { get; set; }
        public virtual List<SalesOrderInfo_Tra> OrderDetails { get; set; }
        public virtual List<DDLBind> ItemList { get; set; }
        public virtual List<DDLBind> SelsPersonList { get; set; }
        public virtual List<DDLBind> CustomerList { get; set; }
        public string Image_Path { get; set; }
    }
   public class SalesOrdeList
   {
       public long Id { get; set; }
       public long Customer_Id { get; set; }
       public string Customer_Name { get; set; }
       public string Address { get; set; }
       public string Contact_No { get; set; }
       public string Email { get; set; }
       public string Order_No { get; set; }
       public long Sales_Person { get; set; }
       public string Sales_Person_Name { get; set; }
       public Nullable<decimal> Amount { get; set; }
       public string Order_Date { get; set; }
       public string Request_Delivery_Date { get; set; }
       public string Remarks { get; set; }
       public string Status { get; set; }

   }

   public class SalesOrderInfo_Tra

   {
       public SalesOrderInfo_Tra()
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
}
