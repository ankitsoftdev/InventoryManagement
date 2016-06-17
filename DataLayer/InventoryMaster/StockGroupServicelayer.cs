using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.InventoryMaster;
using ViewModel.Category;
using DataLayer;
namespace ServiceLayer.InventoryMaster
{
    public class StockGroupServicelayer
    {

        StockGroupDbLayer _db;
        public StockGroupServicelayer()
        {
            _db = new StockGroupDbLayer();
        }
        public StockGroupServicelayer(string ConnectionString)
        {
            _db = new StockGroupDbLayer(ConnectionString);
        }
        public List<ViewModel.Common.List_Common> List()
        {
            var lst = _db.List().Select(x => new ViewModel.Common.List_Common { Id = x.Id, Name = x.Name, Alias_Name = x.Alias_Name, Under_Name = x.Under_Name, Remarks = x.Remarks }).ToList();
            return lst;
        }

        public bool Create(StockGroup modlGroup)
        {
            return _db.Create(modlGroup);
        }
        public bool Update(StockGroup modlGroup)
        {

            return _db.Update(modlGroup);
        }
        public StockGroup Find(long Id)
        {
            return _db.Find(Id);
        }
        public List<ViewModel.Common.DDLBind> DDLlBind()
        {
            return _db.DDLlBind().ToList();
        }
        public bool IsNameExists(long Id, string Name)
        {
            return _db.IsNameExists(Id, Name);
        }
    }
}
