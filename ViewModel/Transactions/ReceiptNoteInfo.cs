using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Transactions
{
   public  class ReceiptNoteInfo
    {
       public ReceiptNoteInfo()
       {
           SupplierList = new List<DDLBind>();
           PurchaseOrderList = new List<DDLBind>();
           ReceiptDetails = new List<ReceiptNoteTraInfo>();
       }
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string Receipt_Note_No { get; set; }
        public long Purchase_Order_Id { get; set; }
        public long Supplier_Id { get; set; }
        public string Purchase_Order_No { get; set; }
        public string Receipt_Date { get; set; }
        public string Order_Date { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
        public string Supplier_Name { get; set; }
        public string Contact_No { get; set; }
        public string Email { get; set; }
      
        public string Image_Path { get; set; }
        public List<DDLBind> SupplierList { get; set; }

        public List<DDLBind> PurchaseOrderList { get; set; }

        public List<ReceiptNoteTraInfo> ReceiptDetails { get; set; }
       
       
    }

    public class ReceiptNoteTraInfo
    {
        public ReceiptNoteTraInfo()
        {
            Status = true;
        }
        public long Id { get; set; }
        public string Receipt_No { get; set; }
        public long Item_Id { get; set; }
        public string Item_Name { get; set; }
        public Nullable<decimal> Order_Quantity { get; set; }
        public Nullable<decimal> Received_Quantity { get; set; }
        public long Pur_Tra_Id { get; set; }
        public Nullable<int> GoDown_Id { get; set; }
        public string GoDown_Name { get; set; }
        public string Unit_Name { get; set; }
        public string Sales_Serial_No { get; set; }
        public string Purchase_SerialNo { get; set; }
        public Nullable<long> Tax_Id { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
    }
    public class ReceiptNoteList
    {
        public long  Id { get; set; }
        public long Purchase_Order_Id { get; set; }
        public string Purchase_Order_No { get; set; }
        public string Supplier_Name { get; set; }
        public string Contact_No { get; set; }
        public string Email_Id { get; set; }
        public string Receipt_Note_No { get; set; }
        public string Remarks { get; set; }
        public System.DateTime Receipt_Date { get; set; }
        public System.DateTime Order_Date { get; set; }
    }
    public class ReceiptNoteMasterInfo
    {
        public ReceiptNoteMasterInfo()
        {

        }
        public long Supplier_Id { get; set; }
        public string Supplier_Code { get; set; }
        public string Name { get; set; }
        public string Contact_No { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Order_Date { get; set; }
        public string Image_Url { get; set; }


    }
}
