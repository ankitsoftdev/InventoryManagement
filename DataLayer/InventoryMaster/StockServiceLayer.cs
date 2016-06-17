using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.InventoryMaster;
using ViewModel.Stock;
using DataLayer;
using ViewModel.Common;

namespace ServiceLayer.InventoryMaster
{
    public   class StockServiceLayer
    {
        StockDbLayer _db;
        public StockServiceLayer()
        {
                _db = new StockDbLayer();
        }
        public StockServiceLayer(string ConnectionString)
        {
        _db = new StockDbLayer(ConnectionString);
        }
        public List<ViewModel.Stock.StockItem> GetStock()
        {

            return _db.GetStock().ToList();
        }
        public bool Create(ViewModel.Stock.StockItem modelStockitem)
        {
            bool result = false;
            if (modelStockitem != null)
            {

                _db.Create(modelStockitem);
                result = true;
            }
            return result;
        }
        public bool Update(ViewModel.Stock.StockItem modelStockitem)
        {
            bool result = false;
            if (modelStockitem != null)
            {
                _db.Update(modelStockitem);
                result = true;
            }
            return result;
        }
        public ViewModel.Stock.StockItem  Find(long Id)
        {
            ViewModel.Stock.StockItem modelStockitem = new StockItem();
           
            if (Id !=0)
            {

            return    _db.Find(Id);
               
            }
            return modelStockitem;
        }
        public bool Delete(int Id=0)
        {
            _db.Delete(Id);
            return true;
        }
        public List<DDLBind> DDLBind()
        {
            return _db.DDLBind();
        }
        public String GEN_ItemCode()
        {
            return _db.GEN_ItemCode();
        }
        public bool IsNameExists(int Id, string Name)
        {
            return _db.IsNameExists(Id, Name);
        }
        public string GEN_MAXId(long itemid)
        {
            return _db.GEN_MAXId(itemid);
        }

        public dynamic GetUnitAndAmount(int itemId)
        {
            return _db.GetUnitAndAmount(itemId);
        }

        public List<ViewModel.Ledger.SalesMaster> GetItemDetails(long ItemId)
        {
            return _db.GetItemDetails(ItemId);
        }
        public List<ItemDDl> DDLItemBind()
        {
            return _db.DDLItemBind();
        }
        public bool GetItemType(long itemId)
        {
            return _db.GetItemType(itemId);
        }
        public List<ItemDDl> ddlBindSaleItem()
        {
            return _db.ddlBindSaleItem();
        }
    }
}
