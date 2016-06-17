using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ViewModel.Common
{
    public class Unit
    {
        public Unit()
        {
            SubUnitList = new List<SubUnit>();
            AddSubUnit = false;
        }
        public long Id { get; set; }
        public long Company_Id { get; set; }
      
         [Required(ErrorMessage = "Required")]
         [Remote("IsNameExists", "Units", "Masters", AdditionalFields = "Id,Name", ErrorMessage = "Name Already Exists.")]
        public String Name { get; set; }
         public int No_of_Decimal { get; set; }
        public String AliasName { get; set; }
        public decimal Ratio { get; set; }
        public bool AddSubUnit { get; set; }
        public string Unit_Volume { get; set; }
        public String Remarks { get; set; }
        public List<SubUnit> SubUnitList { get; set; }


    }
    public class SubUnit
    {
        public SubUnit()
        {
            Ratio1="1";
            Ratio2 = "1";
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias_Name { get; set; }
        public string Ratio1 { get; set; }
        public string Ratio2 { get; set; }

    }
}
