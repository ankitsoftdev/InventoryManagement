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
    
    public partial class Branch_Master
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public Nullable<long> City_Id { get; set; }
        public long Country_Id { get; set; }
        public long State_Id { get; set; }
        public string Email_Id { get; set; }
        public string Contact_No { get; set; }
        public string Pin_Code { get; set; }
        public long Created_By { get; set; }
        public long Modified_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
    
        public virtual Company_Master Company_Master { get; set; }
        public virtual Country_State Country_State { get; set; }
        public virtual Country_State Country_State1 { get; set; }
    }
}
