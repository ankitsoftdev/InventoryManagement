using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Category
{
 public   class StockCategory
 {
     public StockCategory()
     {
         Category = new Common.Ledger_Common();
     }
        public ViewModel.Common.Ledger_Common Category { get; set; }
    }
 public class StockGroup
 {
     public StockGroup()
     {
         Group = new Common.Ledger_Common();
     }
     public ViewModel.Common.Ledger_Common Group { get; set; }
 }
    
}
