using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Company
{
    public class Company : ViewModel.Common.Address
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Reg_Number { get; set; }
        public String TIN_Number { get; set; }
        public String Pan_Number { get; set; }
        public String WebSite { get; set; }
        public String Logo { get; set; }
        public ViewModel.Common.Address Address { get; set; }

    }
}
