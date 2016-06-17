using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.CommonReport
{
    public class Reports
    {
    }
    public class UnitReport
    {
        public String Name { get; set; }
        public String AliasName { get; set; }
        public string Unit_Volume { get; set; }
        public String Remarks { get; set; }
    }
    public class Financial_Year
    {
        public String Name { get; set; }
        public string Remarks { get; set; }
        public DateTime FromPeriod { get; set; }
        public DateTime ToPeriod
        {
            get;
            set;
        }

    }
    public class TaxMaster
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
      
    }
    public class Supplier_CustomerInfo
    {
       
        public long Id { get; set; }
        public String Title { get; set; }
        public String Name { get; set; }
        public string Code { get; set; }
        public string Alias_Name { get; set; }

        public long Group_Id { get; set; }

        public decimal Opening_Balance { get; set; }
        public bool MaintainRecord_BillbyBill { get; set; }
        public int CreditPeriodTime { get; set; }
        public string GroupName { set; get; }

        public String PANNumber { get; set; }
        public String AccountNumber { get; set; }
        public String Bank_Name { get; set; }
        public String IFSC_Code { get; set; }
        public String BranchName { get; set; }

        public String address { get; set; }
        public string CountryName { set; get; }
        public string StateName { set; get; }
        public string CityName { set; get; }
        public String Pin_Code { get; set; }
        //public Contact ContactInfo { get; set; }
       

        public String Phone_Number { get; set; }
        public String Mobile { get; set; }
        public String Email_Id { get; set; }

    }
    public class LedgerGroupCount
    {
        public string  Name { set; get; }
        public long Count { set; get; }
    }
   

}
