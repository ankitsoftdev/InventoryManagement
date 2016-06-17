using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using HDMEntity;
using ViewModel.Category;
namespace DataLayer.InventoryMaster
{
    public  class StockGroupDbLayer
    {
      INVENTORY_DBEntities _db;
      public StockGroupDbLayer()
      {
          _db = new INVENTORY_DBEntities();
        
      }
      public StockGroupDbLayer(String ConnectionString)
      {  _db = new INVENTORY_DBEntities();
          _db.Database.Connection.ConnectionString = ConnectionString;
      }
      public IEnumerable<Pr_CommonList_Result> List()
      {
          var lst = _db.Pr_CommonList(1, "STOCKGROUP").ToList();
          return lst;
         
      }
      public bool Create(StockGroup modlGroup)
      {
          bool result = false;
          if (modlGroup != null)
          {

              Stock_Group tblGroup = new Stock_Group();
              // tblledgrp.Id = modelledgrp.Ledger.Id;
              tblGroup.Name = modlGroup.Group.Name;
              tblGroup.Remarks = modlGroup.Group.Remarks;
              tblGroup.Under_Group = modlGroup.Group.UnderId;
              tblGroup.Alias_Name = modlGroup.Group.Alias_Name;
              tblGroup.Is_Active = true;
              tblGroup.Is_Deleted = false;
              tblGroup.Modified_By = 1;
              // tblGroup.Branch_Id = 1;
              tblGroup.Company_Id = 1;
              tblGroup.Created_By = 1;
              tblGroup.Modified_Date = DateTime.Now;
              tblGroup.Created_Date = DateTime.Now;
              result = Save(tblGroup);
              result = true;
          }
          return result;
      }
      public bool Update(StockGroup modlGroup)
      {
          bool result = false;
          if (modlGroup != null)
          {

              Stock_Group tblGroup = _db.Stock_Group.Find(modlGroup.Group.Id);
              tblGroup.Id = modlGroup.Group.Id;
              tblGroup.Name = modlGroup.Group.Name;
              tblGroup.Remarks = modlGroup.Group.Remarks;
              tblGroup.Under_Group = modlGroup.Group.UnderId;
              tblGroup.Alias_Name = modlGroup.Group.Alias_Name;
              //tblGroup.Is_Active = true;
              //tblGroup.Is_Deleted = false;
              tblGroup.Modified_By = 1;
              // tblGroup.Branch_Id = 1;
              //tblGroup.Company_Id = 1;
              //tblGroup.Created_By = 1;
              tblGroup.Modified_Date = DateTime.Now;

              Save(tblGroup);
              result = true;
          }
          return result;
      }
      public bool Save(Stock_Group tblStockGroup)
      {
          bool result = false;
          try
          {
              bool res = _db.Tax_Master.Any(m => m.Company_Id == 1 && (0 == tblStockGroup.Id || m.Id != tblStockGroup.Id) && (m.Name.Trim().Equals(tblStockGroup.Name.ToUpper().Trim())));
              if (res == false)
              {
                  if (tblStockGroup.Id == 0)
                  {
                      tblStockGroup.Id = _db.Stock_Group.Count() != 0 ? _db.Stock_Group.Max(x => x.Id) + 1 : 1;
                      _db.Stock_Group.Add(tblStockGroup);
                  }
                  _db.SaveChanges();

                  result = true;
              }
              return result;
          }
          catch
          {
              return result;
          }
      }
      public StockGroup Find(long Id)
      {
          StockGroup modelGroup = new StockGroup();
          Stock_Group tblGroup = _db.Stock_Group.Find(Id);
          if (tblGroup != null)
          {
              modelGroup.Group.Id = tblGroup.Id;
              modelGroup.Group.Name = tblGroup.Name;
              modelGroup.Group.Alias_Name = tblGroup.Alias_Name;
              modelGroup.Group.Remarks = tblGroup.Remarks;
              modelGroup.Group.UnderId = tblGroup.Under_Group;
          }
          return modelGroup;
      }
      public List<ViewModel.Common.DDLBind> DDLlBind()
      {
        return  _db.Stock_Group.Select(x=> new ViewModel.Common .DDLBind {
              Id=x.Id,
              Name=x.Name
          }).ToList();
      }
      public bool IsNameExists(long Id, string Name)
      {
          return _db.Stock_Group.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
      }
    }
}
