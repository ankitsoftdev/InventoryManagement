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
    
    public partial class Sales_Return
    {
        public long Id { get; set; }
        public long Sales_Id { get; set; }
        public System.DateTime DATE { get; set; }
        public string SalesRet_VC_No { get; set; }
        public int Fncl_Year_Id { get; set; }
        public Nullable<decimal> Total_Amount { get; set; }
        public Nullable<decimal> Discount_Amount { get; set; }
        public int Return_Type { get; set; }
        public string Remarks { get; set; }
        public long Created_By { get; set; }
        public long Modified_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public long Customer_Id { get; set; }
    
        public virtual Sales_Master Sales_Master { get; set; }
    }
}