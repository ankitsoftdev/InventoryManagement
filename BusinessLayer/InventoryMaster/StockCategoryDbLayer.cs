using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using HDMEntity;
using ViewModel.Category;
namespace DataLayer.InventoryMaster
{
  public  class StockCategoryDbLayer
    {

      INVENTORY_DBEntities _db;
      public StockCategoryDbLayer()
      {
          _db = new INVENTORY_DBEntities();
        
      }
      public StockCategoryDbLayer(String ConnectionString)
      {
    _db = new INVENTORY_DBEntities();
    _db.Database.Connection.ConnectionString = ConnectionString;
      }

      //public IEnumerable<Stock_Category> List()
      //{
      //    var lst = _db.Stock_Category.ToList(); 
      //    return lst;
      //}
      public List<ViewModel.Common.List_Common> List()
      {
          List<ViewModel.Common.List_Common> list = new List<ViewModel.Common.List_Common>();
          var lst = _db.Pr_CommonList(1, "STOCKCATEGORY").ToList();

          lst.ForEach(x => list.Add(new ViewModel.Common.List_Common { Id = x.Id, Name = x.Name, Alias_Name = x.Alias_Name, Under_Name = x.Under_Name, Remarks = x.Remarks }));
          return list;
      }
      public bool Save(Stock_Category tblcatgory)
      {
          bool result = false;
          try
          {
              bool res = _db.Stock_Category.Any(m => m.Company_Id == 1 && (0 == tblcatgory.Id || m.Id != tblcatgory.Id) && (m.Name.Trim().Equals(tblcatgory.Name.ToUpper().Trim())));
              if (res == false)
              {
                  if (tblcatgory.Id == 0)
                  {
                      tblcatgory.Id =_db.Stock_Category.Count()!=0? _db.Stock_Category.Max(x => x.Id)+1:1;
                      _db.Stock_Category.Add(tblcatgory);
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
      public StockCategory Find(long Id)
      {
          StockCategory modelCategory = new StockCategory();
          if (Id != 0)
          {
            
              Stock_Category tblCategory =_db.Stock_Category.Find(Id);
              modelCategory.Category.Id = tblCategory.Id;
              modelCategory.Category.Name = tblCategory.Name;
              modelCategory.Category.Alias_Name = tblCategory.Alias_Name;
              modelCategory.Category.Remarks = tblCategory.Remarks;
              modelCategory.Category.UnderId = tblCategory.Under_Category ?? 0;
              //  modelCategory.Category.BranchId = tblCategory.Branch_Id;
              
          }
         
              return modelCategory;
      }
      public List<ViewModel.Common.DDLBind> DDLBind()
      {
          return _db.Stock_Category.Select(x => new ViewModel.Common.DDLBind
          {
              Id = x.Id,
              Name = x.Name
          }).ToList();
      }
      public bool Create(StockCategory modlCategory)
      {
          bool result = false;
          try
          {
              Stock_Category tblCategory = new Stock_Category();

              tblCategory.Name = modlCategory.Category.Name;
              tblCategory.Remarks = modlCategory.Category.Remarks;
              tblCategory.Under_Category = modlCategory.Category.UnderId;
              tblCategory.Alias_Name = modlCategory.Category.Alias_Name;
              tblCategory.Is_Active = true;
              tblCategory.Is_Deleted = false;
              tblCategory.Modified_By = 1;

              tblCategory.Company_Id = 1;
              tblCategory.Created_By = 1;
              tblCategory.Modified_Date = DateTime.Now;
              tblCategory.Created_Date = DateTime.Now;
           result=   Save(tblCategory);
             
              return result;
          }
          catch
          {
              return result;
          }


      }
      public bool Update(StockCategory modlCategory)
      {
          bool result = false;
          if (modlCategory != null)
          {

              Stock_Category tblCategory = _db.Stock_Category.Find(modlCategory.Category.Id);
              tblCategory.Id = modlCategory.Category.Id;
              tblCategory.Name = modlCategory.Category.Name;
              tblCategory.Remarks = modlCategory.Category.Remarks;
              tblCategory.Under_Category = modlCategory.Category.UnderId;
              tblCategory.Alias_Name = modlCategory.Category.Alias_Name;
              //tblCategory.Is_Active = true;
              //tblCategory.Is_Deleted = false;
              tblCategory.Modified_By = 1;
              // tblCategory.Branch_Id = 1;
              //tblCategory.Company_Id = 1;
              //tblCategory.Created_By = 1;
              tblCategory.Modified_Date = DateTime.Now;

            result=  Save(tblCategory);
              
          }
          return result;
      }
      public bool IsNameExists(int Id, string Name)
      {
          return _db.Stock_Category.Any(m => m.Company_Id == 1  && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
      }
    }
}
