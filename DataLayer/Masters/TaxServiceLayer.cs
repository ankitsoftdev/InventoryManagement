using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
using DataLayer.Masters;
namespace ServiceLayer.Masters
{
   public class TaxServiceLayer
    {
       TaxDbLayer _db;
       public TaxServiceLayer()
       {
           _db = new TaxDbLayer();
       }
       public TaxServiceLayer(string ConnectionString)
       {
 _db = new TaxDbLayer(ConnectionString);
       }
       public List<ViewModel.Common.Tax> List()
       {
         return  _db.List();
       }
       public bool Create(ViewModel.Common.Tax modelTax)
       {
           return _db.Create(modelTax);
       }
       public List<DDLBind> DDLBind()
       {
           return _db.DDLBind();
       }
       public bool Update(ViewModel.Common.Tax modelTax)
       {
           return _db.Update(modelTax);
       }
       public ViewModel.Common.Tax Find(int Id)
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
       public String GEN_TaxCode()
       {
           return _db.GEN_TaxCode();
       }
    }
}
