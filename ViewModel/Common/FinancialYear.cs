using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ViewModel.Common
{
   public class FinancialYear
    {
        public long Id { get; set; }
       [Required(ErrorMessage="Required")]
       [Remote("IsNameExists", "FinancialYear", "Masters", AdditionalFields = "Id,Name", ErrorMessage = "Name Already Exists.")]
        public String Name { get; set; }
        public string Remarks { get; set; }
       [Required(ErrorMessage = "Required")]
       public string FromPeriod { get; set; }
       [Required(ErrorMessage = "Required")]
        public string ToPeriod { get; set; }
    }
}
