using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Transactions
{
   public class MasterDetailsInfo
    {
       public MasterDetailsInfo()
       {
           StockDetails = new ItemAvailablty();
       }
        public int Customer_Id { get; set; }
        public string  Customer_Code { get; set; }
        public int Pending_Order { get; set; }
        public decimal Due_Amount { get; set; }
        public decimal Bal_Amount { get; set; }
        public virtual ItemAvailablty StockDetails { get; set; }
    }
    public class ItemAvailablty
    {
        public int Item_Id { get; set; }
        public string Unit_Name { get; set; }
        public decimal Selling_Rate { get; set; }
        public decimal Purchase_Rate { get; set; }
        public int Item_Code { get; set; }
        public int Available_Qty { get; set; }
        public int Min_Qty { get; set; }
        public string Image_Url { get; set; }
    }
}
