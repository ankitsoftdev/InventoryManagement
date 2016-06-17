using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Transactions;
using ViewModel.Transactions;
namespace ServiceLayer.Transactions
{
   public class PurchaseOrderServiceLayer
    {
       PurchaseOrderDbLayer _db;
       public PurchaseOrderServiceLayer()
       {
           _db = new PurchaseOrderDbLayer();
       }
       public PurchaseOrderServiceLayer(string ConnectionString)
       {
_db = new PurchaseOrderDbLayer(ConnectionString);
       }
       public List<ViewModel.Transactions.PurchaseOrdeList> List()
       {
           return _db.List();

       }
       public bool Create(PurchaseOrderInfo modelPurchaseOrder)
       {
           return _db.Create(modelPurchaseOrder);
       }
       public bool Update(PurchaseOrderInfo modelPurchaseOrder)
       {
           return _db.Update(modelPurchaseOrder);
       }
       public List<ViewModel.Transactions.PurchaseOrderInfo_Tra> OrderDetails(long   OrderNo)
       {
           return _db.OrderDetails(OrderNo);
       }
       public String GEN_OrderNo()
       {
           return _db.GEN_OrderNo();
       }
       public PurchaseOrderInfo Find(long Id)
       {
           return _db.Find(Id);
       }
       public bool Delete(long Id)
       {
           return _db.Delete(Id);
       }
    }
}
