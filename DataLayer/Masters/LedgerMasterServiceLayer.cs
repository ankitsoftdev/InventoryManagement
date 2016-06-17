using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Masters;
using ViewModel.Common;
using ServiceLayer.Common;
namespace ServiceLayer.Masters
{
  public  class LedgerMasterServiceLayer:CommonServiceLayer
    {

      LedgerMasterDbLayer _db;
      public LedgerMasterServiceLayer()
      {
          _db = new LedgerMasterDbLayer();
      }
      public LedgerMasterServiceLayer(string ConnectionString)
          : base(ConnectionString)
      {
          _db = new LedgerMasterDbLayer(ConnectionString);
      }
      public List<ViewModel.Common.List_Common> List()
      {
          return _db.List();
          
      }
      public bool Create(LedgerMasterInfo modelledmaster)
      {
          var modelledgr = _db.Create(modelledmaster);

          return _db.Create(modelledmaster);
      }
      public bool Update(LedgerMasterInfo modelledmaster)
      {
          return _db.Update(modelledmaster);
      }
      public LedgerMasterInfo Find(int Id)
      {
          return _db.Find(Id);
      }
      public bool Delete(int Id)
      {
          return _db.Delete(Id);
      }
      public bool IsNameExists(int Id, string Name)
      {
          return _db.IsNameExists(Id, Name);
      }
    }
}
