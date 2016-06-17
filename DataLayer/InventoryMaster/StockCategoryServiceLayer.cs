using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.InventoryMaster;
using ViewModel.Category;
using DataLayer;
namespace ServiceLayer.InventoryMaster
{
    public class StockCategoryServiceLayer
    {
        DataLayer.InventoryMaster.StockCategoryDbLayer _db;
        public StockCategoryServiceLayer()
        {
            _db = new StockCategoryDbLayer();
        }
        public StockCategoryServiceLayer(string ConnectionString)
        {
            _db = new StockCategoryDbLayer(ConnectionString);
        }
        public List<ViewModel.Common.List_Common> List()
        {
            
            return _db.List();
        }
        public bool Create(StockCategory modlCategory)
        {


            return _db.Create(modlCategory);
        }
        public bool Update(StockCategory modlCategory)
        {


            return _db.Update(modlCategory);
        }
        public StockCategory Find(long Id)
        {
            return _db.Find(Id);
        }
        public List<ViewModel.Common.DDLBind> DDLBind()
        {
            return _db.DDLBind();
        }
        public bool IsNameExists(int Id, string Name)
        {
            return _db.IsNameExists(Id, Name);
        }
        //public dynamic List()
        //{
        //    var d=_db.List();
        //    return d;
        }
    
}
