using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Transactions
{
    public  class SalesReturnInfo
    {
        public SalesReturnInfo()
        {
            ReturnDetails = new List<SalesReturnInfo_Tra>();
            ItemList = new List<DDLBind>();
            CustomerList = new List<DDLBind>();
        }
        public long Id { get; set; }
        public long Sales_Id { get; set; }
        public long Customer_Id { get; set; }
        public string VC_No { get; set; }
        public string Customer_Name { get; set; }
        public string Email { get; set; }
        public string Contact_No { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public decimal Discount { get; set; }
        public System.DateTime Return_Date { get; set; }
        public string Remarks { get; set; }
        public bool Order_Status { get; set; }
        public bool Is_RejectionNote { get; set; }
        public List<SalesReturnInfo_Tra> ReturnDetails { get; set; }
        public virtual List<DDLBind> ItemList { get; set; }
        public virtual List<DDLBind> CustomerList { get; set; }
        public string Image_Path { get; set; }
    }
       public class SalesReturnList
   {
       public long Id { get; set; }
       public long Customer_Id { get; set; }
       public string Customer_Name { get; set; }
       public string Address { get; set; }
       public string Contact_No { get; set; }
       public string Email { get; set; }
       public string VC_No { get; set; }
       public Nullable<decimal> Amount { get; set; }
       public string Return_Date { get; set; }
       public string Remarks { get; set; }
     

   }

   public class SalesReturnInfo_Tra

   {
       public SalesReturnInfo_Tra()
       {
           Status = true;
       }
       public long Id { get; set; }
       public long  SalesRet_Id  { get; set; }
       public long Item_Id { get; set; }
       public string Item_Name { get; set; }
       public Nullable<decimal> Quantity { get; set; }
       public Nullable<decimal> Rate { get; set; }
       public Nullable<decimal> Total_Amount { get; set; }
       public string Unit_Name { get; set; }
       public string Sale_Serial_No { get; set; }
       public int No_of_Decimal { get; set; }
       public bool Status { get; set; }
       
   }
}

