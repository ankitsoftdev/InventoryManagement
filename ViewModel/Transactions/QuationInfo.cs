using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
namespace ViewModel.Transactions
{
   public class QuationInfo
    {
       public QuationInfo()
       {
           QuationItemList = new List<QuationInfo_Tra>();
           UnitList = new List<DDLBind>();
           ItemList = new List<DDLBind>();
       }
        public long Id { get; set; }
        public long Customer_Id { get; set; }
        public decimal Grand_Total { get; set; }
        public decimal Discount { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Quation_Chalan_No { get; set; }
        public long Fncl_Year_Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact_No { get; set; }
        public string Tag_Type { get; set; }
        public string Image_Path { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public virtual List<ViewModel.Common.DDLBind> ItemList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> CustomerList { get; set; }
        public virtual List<ViewModel.Common.DDLBind> UnitList { get; set; }
        public virtual List<QuationInfo_Tra> QuationItemList { get; set; }

       
    }
   public class QuationInfo_Tra
   {
       public QuationInfo_Tra()
       {
           Status = true;
       }
       public long Id { get; set; }
       public string Quation_Chalan_No { get; set; }
       public long Item_Id { get; set; }
       public int UnitPlace { get; set; }
       public string Item_Name { get; set; }
       public long Unit_Id { get; set; }
       public string Unit_Name { get; set; }
       public Nullable<decimal> Qty { get; set; }
       public Nullable<decimal> Quat_Rate { get; set; }
       public Nullable<decimal> Final_Rate { get; set; }
       public decimal Total_Amount { get; set; }
       public bool Status { get; set; }
     
   }
   public partial class QuationList
   {
       public long Id { get; set; }
       public long Approved_By { get; set; }
       
       public string Alias { get; set; }
       public long Customer_Id { get; set; }
       public string Name { get; set; }
       public string Contact_No { get; set; }
       public string Quation_Date { get; set; }
       public string Email { get; set; }
       public string Customer_Code { get; set; }
       public long Fncl_Year_Id { get; set; }
       public string Address { get; set; }
       public string Status { get; set; }
       public string Quation_Chalan_No { get; set; }
       public string Remarks { get; set; }
       public string Quation_Type { get; set; }
       public string Tag_Type { get; set; }
       public decimal Amount { get; set; }
       public decimal Discount { get; set; }
   }
}
