using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ViewModel.Common;

namespace DataLayer.Masters
{
    public   class GoDownDbLayer
    {
        INVENTORY_DBEntities _db;
        public GoDownDbLayer()
         {
             _db = new INVENTORY_DBEntities();
             
         }
        public GoDownDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }

         public List<ViewModel.Common.GoDown_Info> List()
          {
             List<ViewModel.Common.GoDown_Info> lst = new List<GoDown_Info>();
             var tblGoDown = _db.GoDown_Master.Where(x => x.Company_Id == 1 && x.Is_Deleted == false).ToList();
             var List = (from bk in tblGoDown
                                 join ordr in tblGoDown
                                 on bk.Under_Id equals ordr.Id
                                 into a
                                 from b in a.DefaultIfEmpty(new GoDown_Master())
                                 select new
                                 {
                                     Id= bk.Id,
                                     Code="",
                                     Name = bk.Name,
                                    Remarks= bk.Remarks,
                                    Address=bk.Address??string.Empty,
                                     Under_Name = b.Name ?? "Primary"
                                 }).ToList();


             List.ForEach((x) => lst.Add(new GoDown_Info
             {
                 Id = x.Id,
                 Code=x.Code,
                 Name = x.Name,
                 Remarks = x.Remarks,
                 Address = x.Address ,
                 Under_Name = x.Under_Name
             }));
            
             return lst.ToList();
         }
         public bool Create(ViewModel.Common.GoDown_Info modelGoDown)
         {
             bool result = false;
             try
             {

                 GoDown_Master tblGoDwon = new GoDown_Master();
                 tblGoDwon.Id = modelGoDown.Id;
                 tblGoDwon.Name = modelGoDown.Name;
                 tblGoDwon.Remarks = modelGoDown.Remarks;
                 tblGoDwon.Under_Id = modelGoDown.Under_Id;
                 tblGoDwon.Address = modelGoDown.Address;
                 tblGoDwon.Country_Id = modelGoDown.Country_Id;
                 tblGoDwon.State_Id = modelGoDown.State_Id;
                 tblGoDwon.City_Id = modelGoDown.City_Id;
                // tblGoDwon.Code = modelGoDown.Code;
                 tblGoDwon.Company_Id = 1;
                 tblGoDwon.Is_Active = true;
                 tblGoDwon.Is_Deleted = false;
                 
                 tblGoDwon.Modified_Date = DateTime.Now;
                 tblGoDwon.Modified_By = 1;
                 tblGoDwon.Created_By = 1;
                 tblGoDwon.Created_Date = DateTime.Now;

                 result = Save(tblGoDwon);

             }
             catch
             {

                 result = false;
             }
             return result;

         }
         public List<DDLBind> DDLBind()
         {
             var lst = _db.GoDown_Master.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
             return lst;
         }
         public bool Update(ViewModel.Common.GoDown_Info modelGoDown)
         {
             bool result = false;
             try
             {
                 if (modelGoDown != null && modelGoDown.Id!=0)
                 {
                     GoDown_Master tblGoDwon = _db.GoDown_Master.Find(modelGoDown.Id);
                     tblGoDwon.Id = modelGoDown.Id;
                     tblGoDwon.Name = modelGoDown.Name;
                     tblGoDwon.Remarks = modelGoDown.Remarks;
                     tblGoDwon.Under_Id = modelGoDown.Under_Id;
                     tblGoDwon.Address = modelGoDown.Address;
                    // tblGoDwon.Code = modelGoDown.Code;
                     tblGoDwon.Modified_Date = DateTime.Now;
                     tblGoDwon.Modified_By = 1;
                     tblGoDwon.Country_Id = modelGoDown.Country_Id;
                     tblGoDwon.State_Id = modelGoDown.State_Id;
                     tblGoDwon.City_Id = modelGoDown.City_Id;
                     result = Save(tblGoDwon);
                 }
             }
             catch
             {

                 result = false;
             }
             return result;

         }
         private bool Save(GoDown_Master tblGoDown)
         {
             bool result = false;
             try
             {
                 bool res = _db.GoDown_Master.Any(m => m.Company_Id == 1 && (0 == tblGoDown.Id || m.Id != tblGoDown.Id) && (m.Name.Trim().Equals(tblGoDown.Name.ToUpper().Trim())));
                 if (res == false)
                 {
                     if (tblGoDown.Id == 0)
                     {

                         tblGoDown.Id = _db.GoDown_Master.Count()!=0?_db.GoDown_Master.Max(x => x.Id)+1:1;

                         _db.GoDown_Master.Add(tblGoDown);
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
                 GoDown_Master tblGoDown = _db.GoDown_Master.Find(Id);
                 tblGoDown.Modified_Date = DateTime.Now;
                 tblGoDown.Modified_By = 1;
                 tblGoDown.Is_Deleted = true;
                 return true;
             }
             else
                 return false;
         }
         public ViewModel.Common.GoDown_Info  Find(int Id)
         {
             ViewModel.Common.GoDown_Info modelGoDown = new GoDown_Info();
             if (Id != 0)
             {
                 GoDown_Master tblGoDown = _db.GoDown_Master.Find(Id);
                 modelGoDown.Id = tblGoDown.Id;
                 modelGoDown.Name = tblGoDown.Name;
                 modelGoDown.Address = tblGoDown.Address;
                // modelGoDown.Code = tblGoDown.Code;
                 modelGoDown.Remarks = tblGoDown.Remarks ?? string.Empty;
                 modelGoDown.Under_Id=tblGoDown.Under_Id??0;
                 modelGoDown.Country_Id = tblGoDown.Country_Id??104;
                 modelGoDown.State_Id = tblGoDown.State_Id??0;
                 modelGoDown.City_Id = tblGoDown.City_Id??0;
                // return _db.Tax_Master.Find(Id);
             }

             return modelGoDown;
         }
         public String GEN_GodownCode()
         {
             string Challan_No = "";

             int countRows = _db.GoDown_Master.Where(x => x.Company_Id == 1).Count();
             if (countRows != 0)
                 Challan_No = _db.GoDown_Master.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Id.ToString();
             if (!string.IsNullOrWhiteSpace(Challan_No))
             {
                 Challan_No = Regex.Replace(Challan_No, @"\d+(?=\D*$)",
                    m => (Convert.ToInt64(m.Value) + 1).ToString().PadLeft(5, '0'));


             }
             else
             {
                 Challan_No = "1".PadLeft(5, '0');
             }


             return Challan_No;
         }
         public bool IsNameExists(int Id, string Name)
         {
             return _db.GoDown_Master.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
         }
    }
}
