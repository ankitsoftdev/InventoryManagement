using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
namespace ViewModel.Transactions
{
   public class DeliveryNoteInfo
    {
       public DeliveryNoteInfo()
       {
           DeliveryDetails = new List<DeliveryNoteInfo_Tra>();
           CustomerList = new List<DDLBind>();
           ItemList = new List<DDLBind>();
           //TaxList = new List<DDLBind>();
           SalesOrderList = new List<DDLBind>();
       }
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long Customer_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image_Path { get; set; }
        public string Contact_No { get; set; }
        public string Delivery_Note_No { get; set; }
        public long Sales_Order_Id { get; set; }
        public string Sales_Order_No { get; set; }
        public Nullable<decimal> Amount { get; set; }
         
        public String Delivery_Date { get; set; }
        public string Remarks { get; set; }
        public virtual List<DDLBind> ItemList { get; set; }
        //public virtual List<DDLBind> SelsPersonList { get; set; }
        public virtual List<DDLBind> CustomerList { get; set; }
        //public virtual List<ViewModel.Common.DDLBind> TaxList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> SalesOrderList { get; set; }
        public List<DeliveryNoteInfo_Tra> DeliveryDetails { get; set; }
    }
   public class DeliveryNoteInfo_Tra
   {
       public DeliveryNoteInfo_Tra()
       {
           Status = true;
       }
       public long Id { get; set; }
       public string Debit_No { get; set; }
       public long Item_Id { get; set; }
       public string Item_Name { get; set; }
       public Nullable<decimal> Order_Quantity { get; set; }
       public Nullable<decimal> Delivered_Quantity { get; set; }
       public Nullable<long> Tax_Id { get; set; }
       public Nullable<long> GoDown_Id { get; set; }
       public string GoDown_Name { get; set; }
       public string Unit_Name { get; set; }
       public string Sales_Serial_No { get; set; }
       public string Purchase_SerialNo { get; set; }
       public Nullable<decimal> Rate { get; set; }
       public Nullable<decimal> Amount { get; set; }
       public string Remarks { get; set; }
       public long Pur_Tra_Id { get; set; }
       public string Pur { get; set; }
       public bool Status { get; set; }
     
   }
   public  class DeliveryNoteList
   {
       public long Id { get; set; }
       public long Sales_Order_Id { get; set; }
       public string Sales_Order_No { get; set; }
       public string Customer_Name { get; set; }
       public string Contact_No { get; set; }
       public string Email_Id { get; set; }
       public string Delivery_Note_No { get; set; }
       public string Remarks { get; set; }
       public string Delivery_Date { get; set; }
       public string Order_Date { get; set; }
   }
   public class DeliveryNoteMasterInfo
   {
       public DeliveryNoteMasterInfo()
       {
          
       }
       public long Customer_Id { get; set; }
       public string Customer_Code { get; set; }
       public string Name { get; set; }
       public string Contact_No { get; set; }
       public string Address { get; set; }
       public string Email { get; set; }
       public string   Order_Date { get; set; }
       public string Image_Url { get; set; }
     
      
   }
}
