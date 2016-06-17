using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ViewModel.Common
{
    public class Tax
    {
        public long Id { get; set; }


        public long Company_Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public String Code { get; set; }
        [Required(ErrorMessage = "Required")]
        [Remote("IsNameExists", "Tax", "Masters", AdditionalFields = "Id,Name", ErrorMessage = "Name Already Exists.")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal Rate { get; set; }
        public string Alias { get; set; }

        [Required(ErrorMessage = "Required")]
        public long Type_Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Type_Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Rate_Type { get; set; }
        public string Rate_Type_Name { get; set; }
        public string Remarks { get; set; }
    }
    public class TaxDetails
    {
        public TaxDetails()
        {
            TaxDeductionList = new List<DDLBind>();
            listTaxDetails = new List<TaxDetails>();
        }
        public long TId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rate_Type { get; set; }
        public string Tag_Type { get; set; }
        public string Value { get; set; }
        public decimal Grand_Total { get; set; }
        public List<ViewModel.Common.DDLBind> TaxDeductionList { get; set; }
        public List<TaxDetails> listTaxDetails { get; set; }
    }


}
