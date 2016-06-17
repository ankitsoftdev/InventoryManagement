using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Masters;
namespace ServiceLayer.Masters
{
   public class RegionServiceLayer
    {
       RegionDbLayer _db;
       public RegionServiceLayer()
       {
           _db = new RegionDbLayer();
       }
       public RegionServiceLayer(string ConnectionString)
       {
  _db = new RegionDbLayer(ConnectionString  );
       }
       public List<ViewModel.Common.RegionInfo> List()
       {
           return _db.List();
       }
       public bool Create(ViewModel.Common.RegionInfo modelRegionInfo)
       {
           return _db.Create(modelRegionInfo);
       }
       public bool Update(ViewModel.Common.RegionInfo modelRegionInfo)
       {
           return _db.Update(modelRegionInfo);
       }
       public ViewModel.Common.RegionInfo Find(int Id)
       {
           return _db.Find(Id);
       }
       public bool Delete(int Id)
       {
           return _db.Delete(Id);
       }
    }
}
