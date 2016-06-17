

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServiceLayer.CommonReport
{
   public  class ReportServices
    {

       public ReportServices()
       {

       }
   
      public List<ViewModel.CommonReport.UnitReport> GetUnitReports()
       {
          return new DataLayer.Masters.UnitsDblayer().List().Select(x => new ViewModel.CommonReport.UnitReport
           {
               Name =x.Name ,
               AliasName =x.AliasName ,
               Unit_Volume =x.Unit_Volume ,
               Remarks =x.Remarks 
           }).ToList();
       }
      public List<ViewModel.CommonReport .Financial_Year> GetFinancialYearReports()
      {
          return new DataLayer.Masters.FinancialYearDbLayer().List().Select(x => new ViewModel.CommonReport.Financial_Year
          {
              Name =x.Name ,
              FromPeriod =Convert.ToDateTime(x.FromPeriod) ,
              ToPeriod =Convert.ToDateTime(x.ToPeriod) ,
              Remarks =x.Remarks 
          }).ToList();
      }
      public List<ViewModel.CommonReport.TaxMaster> GetTaxMaster()
      {
          return new DataLayer.Masters.TaxDbLayer().List().Select(x => new ViewModel.CommonReport.TaxMaster
          {
              Name =x.Name ,
              Code =x.Code ,
              Rate=x.Rate
          }).ToList();
      }
      public List<ViewModel.CommonReport.Supplier_CustomerInfo> GetSupplierCustomerInfo(string TagName="")
      {
          return new DataLayer.AccountMaster.CustomerDbLayer().GetCustomer(TagName).Select(x=> new ViewModel.CommonReport.Supplier_CustomerInfo {
              Id = x.Id,
              Name = x.Name,
              Code = x.Code,
              Alias_Name = x.Alias_Name,
            
              GroupName = x.GroupName,
              Opening_Balance = Convert.ToDecimal(x.Opening_Balance),
              MaintainRecord_BillbyBill = x.MaintainRecord_BillbyBill ,
              CreditPeriodTime = Convert.ToInt32(x.CreditPeriodTime ),
              address = x.Address.address + "," + x.Address.CountryName + ',' + x.Address.StateName+","+x.Address.CityName+","+x.Address.Pin_Code,
             
              //Pin_Code = x.Address.Pin_Code,
              Mobile = x.Address.ContactInfo.Mobile,
              Email_Id = x.Address.ContactInfo.Email_Id,
              PANNumber = x.Account_Detail.PANNumber,
          }).ToList();
      }
      public List<ViewModel.CommonReport.LedgerGroupCount> GetLedgerGroup()
      {
         return (from line in new DataLayer.Masters.LedgerGroupDbLayer().List()
                        group line by line.Under_Name  into g
                        select new ViewModel.CommonReport .LedgerGroupCount
                        {
                            Name = g.First().Under_Name,
                            //Price = g.Sum(_ => _.Price).ToString(),
                            Count = g.Count(),
                        }).ToList();
          
      }
      public List<ViewModel.CommonReport.LedgerGroupCount> GetStockGroup()
      {
          return (from line in new DataLayer.InventoryMaster.StockGroupDbLayer().List().ToList()
                  group line by line.Under_Name into g
                  select new ViewModel.CommonReport.LedgerGroupCount
                  {
                      Name = g.First().Under_Name,
                      //Price = g.Sum(_ => _.Price).ToString(),
                      Count = g.Count(),
                  }).ToList();

      }
    }
}
