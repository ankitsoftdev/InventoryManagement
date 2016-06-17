using DataLayer;
using DataLayer.AccountMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using ViewModel.Ledger;
using DataLayer.Masters;
namespace ServiceLayer.AccountMaster
{
  public   class CustomerServicesLayer
    {
      CustomerDbLayer objCustomerDbLayer;
      CityDbLayer objCityDbLayer;
      
      public CustomerServicesLayer()
      {
          objCustomerDbLayer = new CustomerDbLayer();
          objCityDbLayer = new CityDbLayer();
      }
      public CustomerServicesLayer(String ConnectionString)
      {
          objCustomerDbLayer = new CustomerDbLayer(ConnectionString);
          objCityDbLayer = new CityDbLayer(ConnectionString);
        
      }
      public List<ViewModel.Ledger.Supplier> GetCustomer(string Tagname = "")
      {
          return objCustomerDbLayer.GetCustomer(Tagname);
      }
      public bool Save(ViewModel.Ledger.Supplier supplier, string Tagname="")
      {
          return objCustomerDbLayer.Insert(supplier, Tagname);
          
          
         
      }


      public IEnumerable<ViewModel.Common.DDLBind> ListCityDDL(long ParentId = 0)
      {
        
          return objCityDbLayer.ListCityDDL(ParentId);
      }
      public List<ViewModel.Common.DDLBind> DDLList(long Tag_Id, long ParentId = 0)
      {
        //  objCityDbLayer = new CityDbLayer();
          return objCityDbLayer.DDLList(Tag_Id, ParentId).ToList();
      }

      public IEnumerable<ViewModel.Common.DDLBind> DDlGroupLList(string Tag)
      {
          return objCustomerDbLayer.DDlGroupLList(Tag).ToList();
      }
      public ViewModel.Ledger.Supplier FindCustomer(long Id=0, string Tagname = "")
      {
          return objCustomerDbLayer.FindCUSTOMER_SUPPLIER(Id, Tagname);
      }
      public bool Update(ViewModel.Ledger.Supplier supplier, string Tagname="")
      {
          return objCustomerDbLayer.Update(supplier, Tagname);
         
      }
      public bool Delete(int Id = 0, string Tagname="")
      {
          return objCustomerDbLayer.Delete(Id, Tagname); 
          
      }
      [Obsolete("Unable to Use in This Project",true)]
      public List<ViewModel.Common.DDLBind> DDLBind(string Tag, string searchText)
      {
          return objCustomerDbLayer.DDLBind(Tag, searchText);
      }
      public bool IsEmailIdExists(string TagName ,int Id = 0, string Email = "")
      {
          return objCustomerDbLayer.IsEmailIdExists(TagName, Id, Email);
      }
     public string GEN_AccountsCode(string Tag)
      {
          return objCustomerDbLayer.GEN_AccountsCode(Tag);
      }
    }
    
}
