using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Transactions;
using ViewModel.Transactions;
namespace ServiceLayer.Transactions
{
  public  class DeliveryNoteServiceLayer
    {
      DeliveryNoteDbLayer _db;
       public DeliveryNoteServiceLayer()
      {
          _db = new DeliveryNoteDbLayer();
      }
       public DeliveryNoteServiceLayer(string ConnectionString)
       {
 _db = new DeliveryNoteDbLayer(ConnectionString);
       }
       public List<ViewModel.Transactions.DeliveryNoteList> List()
       {
           return _db.List();
       }
       public bool Create(DeliveryNoteInfo modelDeliveryNote)
       {
           return _db.Create(modelDeliveryNote);
       }
       public bool Update(DeliveryNoteInfo modelDeliveryNote)
       {
           return _db.Update(modelDeliveryNote);
       }
       public DeliveryNoteInfo Find(int Id)
       {
           return _db.Find(Id: Id);
       }
       public List<ViewModel.Common.DDLBind> DDLBind(long CustomerId = 0, long Id = 0)
       {
           return _db.DDLBind(CustomerId, Id);
       }
       public List<ViewModel.Transactions.DeliveryNoteInfo_Tra> DeliveryDetails(long Debit_No)
       {
           return _db.DeliveryDetails(Debit_No);
       }
       public String GEN_DebitNo()
       {
           return _db.GEN_DebitNo();
       }

       public bool Delete(int Id)
       {
          return _db.Delete(Id);
       }
      public List<ViewModel.Transactions.DeliveryNoteInfo_Tra> DeliveryNoteDetails(int Id=0,int OrderId=0)
       {
           return _db.DeliveryNoteDetails(Id, OrderId);
       }
    }
}
