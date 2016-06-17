using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace ViewModel.Common
{
  public  class LedgerMasterInfo
    {
     
          public long Id { get; set; }
         //[Required(ErrorMessage="Required")]
          public string Code { get; set; }
          [Required(ErrorMessage = "Required")]
          [Remote("IsNameExists", "LedgerMaster", "Masters", AdditionalFields = "Id,Name", ErrorMessage = "Name Already Exists.")]
          public string Name { get; set; }
          public string Alias_Name { get; set; }
          public string Email_Id { get; set; }
          public string Contact_No { get; set; }
          public string Pan_No { get; set; }
          public string Pin_Code { get; set; }
          public string Address { get; set; }
          public Nullable<long> City_Id { get; set; }
          public Nullable<long> Country_Id { get; set; }
          public Nullable<long> State_Id { get; set; }
          [Required(ErrorMessage = "Required")]
          public long Group_Id { get; set; }
          public Nullable<decimal> Opeaning_Bal { get; set; }
          public bool Mnt_Bill_By_Bill { get; set; }
          public Nullable<int> Credit_Period_Time { get; set; }
          public string Image_Url { get; set; }
          public string Remarks { get; set; }
          public List<ViewModel.Common.DDLBind> CountryList { get; set; }
          public List<ViewModel.Common.DDLBind> StateList { get; set; }
          public List<ViewModel.Common.DDLBind> CityList { get; set; }
       public   List<DDLBind> GroupList { get; set; }

        
      }
    
}
