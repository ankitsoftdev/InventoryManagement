using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Transactions;
namespace DataLayer.Transactions
{
  public  class StockMasterDbLayer
    {
      INVENTORY_DBEntities _db;
      public StockMasterDbLayer()
      {
          _db = new INVENTORY_DBEntities();
      }
      public StockMasterDbLayer(String ConnectionString)
      {
          _db = new INVENTORY_DBEntities();
          _db.Database.Connection.ConnectionString = ConnectionString;
      }
      public List<ViewModel.Transactions.StockMaster> List()
      {
          List<ViewModel.Transactions.StockMaster> list = new List<StockMaster>();
          var lst = _db.Pr_StockMasterList(1).ToList();
          lst.ForEach(x=>list.Add( new ViewModel.Transactions.StockMaster
          {
              Id=x.Stock_Id,
              Alias_Name=x.Alias_Name,
              CategoryName=x.Category_Name,
              GroupName=x.Group_Name,
              Name=x.Item_Name,
              Qty = x.Qty.ToString("#." + new string('0', x.No_of_Decimal)),
              Rate=x.Rate.ToString(),
              Selling_Rate=x.Selling_Rate.ToString(),
              UnitName=x.Unit_Name,
              Item_Code=x.Item_Code,
              
          }));
          return list;
      }
      public ViewModel.Transactions.StockMaster Find(int ItemId)
      {
          ViewModel.Transactions.StockMaster modelstock = new ViewModel.Transactions.StockMaster();
          var tblstock = _db.Stock_Master.FirstOrDefault(x => x.Item_Id == ItemId);
          modelstock.Id = tblstock.Id;
          modelstock.Item_Id = tblstock.Item_Id;
          modelstock.Qty = tblstock.Qty.ToString();
          modelstock.Rate = tblstock.Rate.ToString();
          modelstock.Selling_Rate = tblstock.Selling_Rate.ToString();
          
          return modelstock;

      }
      public List<ViewModel.Ledger.StockMasterDDL> StockMasterDDL(long Id = 0)
      {
          List<ViewModel.Ledger.StockMasterDDL> list = new List<ViewModel.Ledger.StockMasterDDL>();
          var lst = _db.Stock_Master.Where(x => x.Company_Id == 1 && (0 == Id || x.Item_Id == Id)).ToList();
          lst.ForEach((x) => list.Add(new ViewModel.Ledger.StockMasterDDL
          {
              Id = x.Item_Id,
              Qty = x.Qty ?? 0,
              SellingRate = x.Selling_Rate >= x.Rate ? x.Selling_Rate ?? 0 : x.Rate ?? 0
          }));

          
          return list.ToList();

          
      }
    }
}
