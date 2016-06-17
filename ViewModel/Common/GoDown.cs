using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace ViewModel.Common
{
    public class GoDown_Info
    {
        public GoDown_Info()
        {
            GoDownList = new List<DDLBind>();
            CountryList = new List<DDLBind>();
            StateList = new List<DDLBind>();
            CityList = new List<DDLBind>();
        }
        public long Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Required")]
        [Remote("IsNameExists", "GoDown", "Masters", AdditionalFields = "Id,Name", ErrorMessage = "Name all Ready Exists..")]
        public string Name { get; set; }
        public string Under_Name { get; set; }
        public string Address { get; set; }
        public long Country_Id { get; set; }
        public long State_Id { get; set; }
        public long City_Id { get; set; }
        public Nullable<long> Under_Id { get; set; }
        public string Remarks { get; set; }
        public virtual List<ViewModel.Common.DDLBind> GoDownList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> CountryList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> StateList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> CityList { get; set; }

    }
}
