using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Masters;
using ViewModel.Common;
namespace ServiceLayer.Masters
{
   public class GoDownServiceLayer
    {
        GoDownDbLayer _db;
        public GoDownServiceLayer()
       {
           _db = new GoDownDbLayer();
       }
        public GoDownServiceLayer(string ConnectionString)
        {
            _db = new GoDownDbLayer(ConnectionString);
        }
       public List<ViewModel.Common.GoDown_Info> List()
       {
         return  _db.List();
       }
       public bool Create(ViewModel.Common.GoDown_Info modelGoDown)
       {
           return _db.Create(modelGoDown);
       }
       public List<DDLBind> DDLBind()
       {
           return _db.DDLBind();
       }
       public bool Update(ViewModel.Common.GoDown_Info modelGoDown)
       {
           return _db.Update(modelGoDown);
       }
       public ViewModel.Common.GoDown_Info Find(int Id)
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
       public String GEN_GodownCode()
       {
           return _db.GEN_GodownCode();
       }
    
    }
}
