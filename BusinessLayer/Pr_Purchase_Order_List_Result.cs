//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    
    public partial class Pr_Purchase_Order_List_Result
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public long Supplier_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
        public string Contact_No { get; set; }
        public string Email_Id { get; set; }
        public System.DateTime Order_Date { get; set; }
        public string Order_No { get; set; }
        public System.DateTime Request_Delivery_Date { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public long Sales_Person { get; set; }
    }
}
