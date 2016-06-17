using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using HDMEntity;
using ViewModel.Common;
using System.Text.RegularExpressions;
namespace DataLayer.Masters
{
     public class TaxDbLayer
    {
         INVENTORY_DBEntities _db;
         public TaxDbLayer()
         {
             _db = new INVENTORY_DBEntities();
             
         }

         public TaxDbLayer(String ConnectionString)
         {
             _db = new INVENTORY_DBEntities();
             _db.Database.Connection.ConnectionString = ConnectionString;
         }
         public List<ViewModel.Common.Tax> List()
         {
             var lst = _db.Tax_Deduction_Master.Where(x => x.Company_Id == 1 && x.Is_Deleted==false).Select(x => new ViewModel.Common.Tax { Id = x.Id, Name = x.Name,Rate_Type_Name=x.Rate_Type ,Alias=x.Alias_Name,Type_Name=x.Type,Rate = x.Rate, Code = x.Code,Remarks=x.Remarks }).ToList();
             return lst;
         }
         public bool Create(ViewModel.Common.Tax modelTax)
         {
             bool result = false;
             try
             {

                 Tax_Deduction_Master tblTax = new Tax_Deduction_Master();
                 tblTax.Company_Id = 1;
                 tblTax.Id = modelTax.Id;
                 tblTax.Name = modelTax.Name;
                 tblTax.Rate = modelTax.Rate;
                 tblTax.Alias_Name = modelTax.Alias;
                 tblTax.Type = modelTax.Type_Id == 1 ? "DEDUCTION" : "ADDTION";
                 tblTax.Rate_Type = modelTax.Rate_Type == 1 ? "PERCENT" : "AMOUNT";
                 tblTax.Remarks = modelTax.Remarks;
                 tblTax.Is_Active = true;
                 tblTax.Is_Deleted = false;
                 tblTax.Code = modelTax.Code;
                 tblTax.Modified_Date = DateTime.Now;
                 tblTax.Modified_By = 1;
                 tblTax.Created_By = 1;
                 tblTax.Created_Date = DateTime.Now;

                 result = Save(tblTax);

             }
             catch
             {

                 result = false;
             }
             return result;

         }
         public List<DDLBind> DDLBind()
         {
             var lst = _db.Tax_Deduction_Master.Select(x => new DDLBind { Id = x.Id, Name = x.Name + "(" + x.Rate + "%)" }).ToList();
             return lst;
         }
         public bool Update(ViewModel.Common.Tax modelTax)
         {
             bool result = false;
             try
             {
                 if (modelTax != null)
                 {
                     Tax_Deduction_Master tblTax = _db.Tax_Deduction_Master.Find(modelTax.Id);
                     tblTax.Name = modelTax.Name;
                     tblTax.Rate = modelTax.Rate;
                     tblTax.Code = modelTax.Code;
                     tblTax.Alias_Name = modelTax.Alias;
                     tblTax.Type = modelTax.Type_Id == 1 ? "DEDUCTION" : "ADDTION";
                     tblTax.Rate_Type = modelTax.Rate_Type == 1 ? "PERCENT" : "AMOUNT";
                     tblTax.Remarks = modelTax.Remarks;
                     tblTax.Modified_Date = DateTime.Now;
                     tblTax.Modified_By = 1;
                     result = Save(tblTax);
                 }
             }
             catch
             {

                 result = false;
             }
             return result;

         }
         private bool Save(Tax_Deduction_Master tblTax)
         {
             bool result = false;
             try
             {
                 bool res = _db.Tax_Deduction_Master.Any(m => m.Company_Id == 1 && (0 == tblTax.Id || m.Id != tblTax.Id) && (m.Name.Trim().Equals(tblTax.Name.ToUpper().Trim())));
                 if (res == false)
                 {
                     if (tblTax.Id == 0)
                     {
                         tblTax.Id = _db.Tax_Deduction_Master.Count()!=0? _db.Tax_Deduction_Master.Max(x => x.Id) + 1:1;
                         _db.Tax_Deduction_Master.Add(tblTax);


                     }
                     _db.SaveChanges();
                     result = true;

                 }
             }
             catch
             {
                 return result;
             }
             return result;
         }
         public bool Delete(int Id)
         {
             if (Id != 0)
             {
                 Tax_Deduction_Master tblTax = _db.Tax_Deduction_Master.Find(Id);
                 tblTax.Modified_Date = DateTime.Now;
                 tblTax.Modified_By = 1;
                 tblTax.Is_Deleted = true;
                 return true;
             }
             else
                 return false;
         }
         public ViewModel.Common.Tax  Find(int Id)
         {
             ViewModel.Common.Tax modelTax = new Tax();
             if (Id != 0)
             {
                 Tax_Deduction_Master tblTax = _db.Tax_Deduction_Master.Find(Id);
                 modelTax.Id = tblTax.Id;
                 modelTax.Name = tblTax.Name;
                 modelTax.Alias = tblTax.Alias_Name;
                 modelTax.Code = tblTax.Code;
                 modelTax.Rate = tblTax.Rate;
                 modelTax.Alias = tblTax.Alias_Name;
                 modelTax.Type_Id = tblTax.Type== "DEDUCTION"? 1 : 2;
                 modelTax.Rate_Type = tblTax.Rate_Type == "PERCENT" ? 1 : 2;
                 modelTax.Remarks = tblTax.Remarks;
                // return _db.Tax_Master.Find(Id);
             }
           
                 return modelTax;
         }
         public String GEN_TaxCode()
         {
             string Challan_No = "";

             int countRows = _db.Tax_Deduction_Master.Where(x => x.Company_Id == 1).Count();
             if (countRows != 0)
                 Challan_No = _db.Tax_Deduction_Master.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Code;
             if (!string.IsNullOrWhiteSpace(Challan_No))
             {
                 Challan_No = Regex.Replace(Challan_No, @"\d+(?=\D*$)",
                    m => (Convert.ToInt64(m.Value) + 1).ToString().PadLeft(5,'0'));


             }
             else
             {
                 Challan_No =  "1".PadLeft(5,'0');
             }


             return Challan_No;
         }
         public bool IsNameExists(int Id, string Name)
         {
             return _db.Tax_Deduction_Master.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
         }
    }
}
