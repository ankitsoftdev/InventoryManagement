using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace ViewModel.Common
{
 public   class Address
    {
     public Address()  
     {
         CountryList = new List<DDLBind>();
         StateList = new List<DDLBind>();
         CityList = new List<DDLBind>();
         ContactInfo = new Contact();
     }
        public String address { get; set; } 
        public long Country_Id { get; set; }
        public long State_Id { get; set; }
        public long City_Id { get; set; }
        public String Pin_Code { get; set; }
        public Contact ContactInfo { get; set; }
        public string CountryName { set; get; }
        public string StateName { set; get; }
        public string CityName { set; get; }
        public  List<ViewModel.Common.DDLBind> CountryList { get; set; }
        public  List<ViewModel.Common.DDLBind> StateList { get; set; }
        public  List<ViewModel.Common.DDLBind> CityList { get; set; }
 

    }

 public class Contact
 {
   
     public String Phone_Number { get; set; }
     public String Mobile { get; set; }
     //[Required(ErrorMessage="Required")]
     //[Remote("IsEmailExists", "Customer", "AccountMaster", AdditionalFields = "Email_Id", ErrorMessage = "Email Id Already Exists.")]
     public String Email_Id { get; set; }

 }
}
