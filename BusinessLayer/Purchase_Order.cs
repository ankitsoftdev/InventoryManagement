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
    using System.Collections.Generic;
    
    public partial class Purchase_Order
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string Pur_Order_VC_No { get; set; }
        public long Sales_Person { get; set; }
        public Nullable<long> Supplier_Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime Posting_Date { get; set; }
        public System.DateTime Order_Date { get; set; }
        public System.DateTime Request_Delivery_Date { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public bool Is_Receipt_Note { get; set; }
        public long Created_By { get; set; }
        public long Modified_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
    }
}
