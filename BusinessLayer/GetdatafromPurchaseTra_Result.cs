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
    
    public partial class GetdatafromPurchaseTra_Result
    {
        public long Id { get; set; }
        public Nullable<long> Pur_Id { get; set; }
        public long Item_Id { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<long> Tax_Id { get; set; }
        public Nullable<long> GoDown_Id { get; set; }
        public string Purchase_Serial_No { get; set; }
        public string Sale_Serial_No { get; set; }
        public long Created_By { get; set; }
        public long Modified_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
    }
}