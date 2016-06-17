using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using HDMEntity;
using ViewModel.Common;
namespace DataLayer.Masters
{
 public   class FinancialYearDbLayer
    {
     INVENTORY_DBEntities _db;
     public FinancialYearDbLayer()
     {
         _db = new INVENTORY_DBEntities();
     
     }
     public FinancialYearDbLayer(String ConnectionString)
     {   
         _db = new INVENTORY_DBEntities();
         _db.Database.Connection.ConnectionString = ConnectionString;
     }

     public List<ViewModel.Common.FinancialYear> List()
     {
         //FinancialYear modelFincial = new FinancialYear();
         List<ViewModel.Common.FinancialYear> lst = new List<ViewModel.Common.FinancialYear>();
         var list = _db.FinancialYears.Where(x => x.Company_Id == 1 && x.Is_Deleted == false).ToList();
             list.ForEach((x)=>lst.Add(new 
             ViewModel.Common.FinancialYear { Id = x.Id, Name = x.Name,Remarks=x.Remarks, FromPeriod = x.From_Date.ToString("yyyy-MM-dd"), ToPeriod = x.To_Date.ToString("yyyy-MM-dd") }));
         return lst;
     }
     public bool Create(ViewModel.Common.FinancialYear modelFincial)
     {
         bool result = false;
         FinancialYear tblFincial = new FinancialYear();


         //  tblunit.Id = modelunit.Id;
         tblFincial.Name = modelFincial.Name;
         tblFincial.Remarks = modelFincial.Remarks;
         tblFincial.To_Date = Convert.ToDateTime(modelFincial.ToPeriod);
         tblFincial.From_Date = Convert.ToDateTime(modelFincial.FromPeriod);
         tblFincial.Is_Active = true;
         tblFincial.Is_Deleted = false;
         tblFincial.Company_Id = 1;
         tblFincial.Created_By = 1;
         tblFincial.Created_Date = DateTime.Now;
         tblFincial.Modified_Date = DateTime.Now;
         tblFincial.Modified_By = 1;
         result = Save(tblFincial);
         return result;

     }
     public bool Update(ViewModel.Common.FinancialYear modelFincial)
     {
         bool result = false;

         if (modelFincial.Id != 0)
         {
             FinancialYear tblFincial = _db.FinancialYears.Find(modelFincial.Id);
             tblFincial.Id = modelFincial.Id;
             tblFincial.Name = modelFincial.Name;
             tblFincial.Remarks = modelFincial.Remarks;
             tblFincial.To_Date = Convert.ToDateTime(modelFincial.ToPeriod);
             tblFincial.From_Date = Convert.ToDateTime(modelFincial.FromPeriod);
             //tblunit.Is_Active = true;
             //tblunit.Is_Deleted = false;
             //tblunit.Company_Id = 1;
             //  tblunit.Created_By = 1;
             // tblunit.Created_Date = DateTime.Now;
             tblFincial.Modified_Date = DateTime.Now;
             tblFincial.Modified_By = 1;
             result = Save(tblFincial);
         }
         return result;

     }
     public List<DDLBind> DDLBind()
     {
         var lst = _db.FinancialYears.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
         return lst;
     }
     private bool Save(FinancialYear tblFincial)
     {
         bool result = false;
         try
         {
             bool res = _db.UnitMasters.Any(m => m.Company_Id == 1 && (0 == tblFincial.Id || m.Id != tblFincial.Id) && (m.Name.Trim().Equals(tblFincial.Name.ToUpper().Trim())));
             if (res == false)
             {
                 if (tblFincial.Id == 0)
                 {
                     tblFincial.Id = _db.FinancialYears.AsNoTracking().Count()!=0 ? _db.FinancialYears.Max(x => x.Id) + 1 : 1;
                     _db.FinancialYears.Add(tblFincial);
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
     public ViewModel.Common.FinancialYear Find(int Id)
     {
         ViewModel.Common.FinancialYear modelFincial = new ViewModel.Common.FinancialYear();
         if(Id!=0)
         {
             FinancialYear tblFincial = _db.FinancialYears.Find(Id);
             modelFincial.Id = tblFincial.Id;
              modelFincial.Name=tblFincial.Name;
              modelFincial.Remarks = tblFincial.Remarks;
              modelFincial.ToPeriod = tblFincial.To_Date.ToString("yyyy-MM-dd");
              modelFincial.FromPeriod = tblFincial.From_Date.ToString("yyyy-MM-dd");
            // return tblFincial
         }
         return modelFincial;
     }
     public bool Delete(int Id)
     {
         if (Id != 0)
         {
             FinancialYear tblFincial = _db.FinancialYears.Find(Id);
             tblFincial.Is_Deleted = true;
             tblFincial.Modified_By = 1;
             tblFincial.Modified_Date = DateTime.Now;
             _db.SaveChanges();
             return true;
         }
         return false;
     }
     public bool IsNameExists(int Id, string Name)
     {
         return _db.FinancialYears.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
     }
    }
}
