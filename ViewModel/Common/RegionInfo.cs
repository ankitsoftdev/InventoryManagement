using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace ViewModel.Common
{
  public  class RegionInfo
    {
      public RegionInfo()
      {
          GoDownList = new List<DDLBind>();
          UnderList = new List<DDLBind>();
      }
        public long Id { get; set; }
      [Required(ErrorMessage="Required")]
        public string Name { get; set; }
      public string Alias_Name { get; set; }
      [Required(ErrorMessage = "Required")]
      public long GoDown_Id { get; set; }
        public Nullable<long> Under_Id { get; set; }
        public string Remarks { get; set; }
        public string Under_Name { get; set; }
        public string GoDown_Name { get; set; }
        public virtual List<ViewModel.Common.DDLBind> GoDownList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> UnderList { get; set; }   
    
  
      
    }
}
