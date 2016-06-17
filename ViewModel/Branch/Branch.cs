using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Branch
{
   public class Branch
    {
        public int Id { get; set; }
        public String Name{ get; set; }
        public String AliasName { get; set; }
       public ViewModel.Common.Address Address { get; set; }
    }
}
