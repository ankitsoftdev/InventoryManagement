using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.LadgerGroup
{
    public   class LedgerGroup
    {
        public LedgerGroup()
        {
            Ledger = new Common.Ledger_Common();
        }
     public ViewModel.Common.Ledger_Common Ledger { get; set; }
    }
}
