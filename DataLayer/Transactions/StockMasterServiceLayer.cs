using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Transactions;
using ViewModel.Transactions;
namespace ServiceLayer.Transactions
{
   public class StockMasterServiceLayer
    {
       StockMasterDbLayer _db;
       public StockMasterServiceLayer()
       {
           _db = new StockMasterDbLayer();
       }
       public StockMasterServiceLayer(string ConnectionString)
       {
 _db = new StockMasterDbLayer(ConnectionString);
       }
       public List<ViewModel.Transactions.StockMaster> List()
       {
           return _db.List();
       }
       public List<ViewModel.Ledger.StockMasterDDL> StockMasterDDL(int Id = 0)
       {
           return _db.StockMasterDDL(Id);
       }
       public ViewModel.Transactions.StockMaster Find(int ItemId)
       {
           return _db.Find(ItemId);
       }
    }
}
