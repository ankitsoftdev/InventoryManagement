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
    
    public partial class Country_State
    {
        public Country_State()
        {
            this.Branch_Master = new HashSet<Branch_Master>();
            this.Branch_Master1 = new HashSet<Branch_Master>();
            this.City_Master = new HashSet<City_Master>();
            this.City_Master1 = new HashSet<City_Master>();
            this.Company_Master = new HashSet<Company_Master>();
            this.Company_Master1 = new HashSet<Company_Master>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public long Parent_Id { get; set; }
        public string Display_Name { get; set; }
        public bool Is_Visible { get; set; }
        public long Tag_Id { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
    
        public virtual ICollection<Branch_Master> Branch_Master { get; set; }
        public virtual ICollection<Branch_Master> Branch_Master1 { get; set; }
        public virtual ICollection<City_Master> City_Master { get; set; }
        public virtual ICollection<City_Master> City_Master1 { get; set; }
        public virtual ICollection<Company_Master> Company_Master { get; set; }
        public virtual ICollection<Company_Master> Company_Master1 { get; set; }
    }
}