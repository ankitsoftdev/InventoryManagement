using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Transactions;
using ViewModel.Transactions;
namespace ServiceLayer.Transactions
{
   public class QuationServiceLayer
    {
       QuationDbLayer _db;
       public QuationServiceLayer()
       {
           _db = new QuationDbLayer();
       }
       public QuationServiceLayer(string ConnectionString)
       {
     _db = new QuationDbLayer(ConnectionString);
       }
       public List<ViewModel.Transactions.QuationList> List()
       {
           return _db.List();
       }
       public bool Create(QuationInfo modelQuation)
       {
           return _db.Create(modelQuation);
       }
       public QuationInfo Find(long Id)
       {
           return _db.Find(Id);
       }
       public bool Update(QuationInfo modelQuation)
       {
           return _db.Update(modelQuation);
       }
       public List<ViewModel.Transactions.QuationInfo_Tra> QuationDetails(long Challan_No)
       {
           return _db.QuationDetails(Challan_No);

       }
       public String GEN_ChallanNo()
       {
           return _db.GEN_ChallanNo();
       }
       public bool ApproveQuation(long QuationId)
       {
          return _db.ApproveQuation(QuationId);
       }
       public bool Delete(int Id)
       {
           return _db.Delete(Id);
       }
    }

}
