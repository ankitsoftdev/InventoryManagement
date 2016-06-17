using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Transactions
{
  public  class StockMaster
    {
        public long Id { get; set; }
        public string Branch_Name { get; set; }
        public long Branch_Id { get; set; }
        public String Item_Code { get; set; }
        public string Name { get; set; }
        public long Item_Id { get; set; }
        public string Alias_Name { get; set; }
        public string GroupName { set; get; }
        public string CategoryName { set; get; }
        public string UnitName { set; get; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Selling_Rate { get; set; }
    }
  public enum StockTag
  { 
  All=1,
      Repurchase=2,
      OutOfStock=3
  };
}
