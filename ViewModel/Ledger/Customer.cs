using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Ledger
{
  public  class Supplier
    {
     public Supplier()
     {
         Address = new Common.Address();
         Account_Detail = new Common.PersonAccountDetails();
     }
      public long Id { get; set; }
      public String Title { get; set; }
      public String Name { get; set; }
      public string Tagname { get; set; }
      public string Code { get; set; }
      public string Alias_Name { get; set; }
      public ViewModel.Common.PersonAccountDetails Account_Detail { get; set; }
      public ViewModel.Common.Address Address { get; set; }
      public long Group_Id { get; set; }
      public decimal Opening_Balance { get; set; }
      public bool MaintainRecord_BillbyBill { get; set; }
      public int CreditPeriodTime { get; set; }
      public string GroupName{set;get;}
      public string Image_Path { get; set; }
      
    }
    
}
