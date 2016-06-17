using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Transactions;
using ViewModel.Transactions;
namespace ServiceLayer.Transactions
{
   public class SalesReturnServiceLayer
    {
       SalesReturnDbLayer _db;
       public SalesReturnServiceLayer()
       {
           _db = new SalesReturnDbLayer();
       }

       public SalesReturnServiceLayer(string ConnectionString)
           
       {
           _db = new SalesReturnDbLayer(ConnectionString);
       }
       public List<SalesReturnList> List(string search="")
       {
           return _db.List(search);
       }
       public bool Create(SalesReturnInfo ModelSales)
       {
           return _db.Create(ModelSales);
          
       }
       public bool Update(SalesReturnInfo ModelSales)
       {
           return _db.Update(ModelSales);

       }
    public string      GEN_VC_No()
       {
           return _db.GEN_VC_No();
       }
    public List<SalesReturnInfo_Tra> ReturnDetails(long Id)
    {
        return _db.ReturnDetais(Id);
    }
       public SalesReturnInfo Find(long Id)
       {
           return _db.Find(Id);
       }
       public bool Delete(long Id)
       {
           return _db.Delete(Id);
       }
    }
}
