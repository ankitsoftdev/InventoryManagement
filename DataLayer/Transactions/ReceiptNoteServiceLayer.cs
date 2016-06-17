using DataLayer.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Transactions;

namespace ServiceLayer.Transactions
{
   public class ReceiptNoteServiceLayer
    {
       ReceiptNoteDbLayer _db;
       public ReceiptNoteServiceLayer()
       {
           _db = new ReceiptNoteDbLayer();
       }
       public ReceiptNoteServiceLayer(string ConnectionString)
       {
 _db = new ReceiptNoteDbLayer(ConnectionString);
       }
       public List<ReceiptNoteList> List()
       {
           return _db.List();
       }
       public bool Create(ReceiptNoteInfo modelReceiptNote)
       {
           return _db.Create(modelReceiptNote);
       }
       public bool Update(ReceiptNoteInfo modelReceiptNote)
       {
           return _db.Update(modelReceiptNote);
       }
       public List<ReceiptNoteTraInfo> ReceiptNoteDetails(int Id, int OrderId)
       {
           return _db.ReceiptNoteDetails(Id, OrderId);
       }
       public List<ViewModel.Common.DDLBind> DDLBind(long SuuplierId = 0,long Id=0)
       {
           return _db.DDLBind(SuuplierId, Id).ToList();
       }
       public ReceiptNoteInfo Find(int Id)
       {
           return _db.Find(Id);
       }
       public String GEN_ReceiptNo()
       {
           return _db.GEN_ReceiptNo();
       }
       public bool Delete(int Id)
       {
           return _db.Delete(Id);
       }
      
    }
}
