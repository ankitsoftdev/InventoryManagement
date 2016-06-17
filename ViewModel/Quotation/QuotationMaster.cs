using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Quotation
{
  public  class QuotationMaster
    {
        public int Company_Id { get; set; }
        public int Branch_Id { get; set; }
        public int Id { get; set; }
        public int Fin_Id { get; set; }
        public String QuotationNumber { get; set; }
        public String Name { get; set; }
        public String Alias_Name { get; set; }
        public ViewModel.Common.Address Address { get; set; }
        public bool Approved { get; set; }
        public int  Approved_By { get; set; }
        public string Remarks { get; set; }
        public string Contact_No { get; set; }
    }
}
