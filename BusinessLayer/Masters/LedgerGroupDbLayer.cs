using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
using ViewModel.LadgerGroup;
namespace DataLayer.Masters
{
    public class LedgerGroupDbLayer
    {
        INVENTORY_DBEntities _db;
        public LedgerGroupDbLayer()
        {
            _db = new INVENTORY_DBEntities();
            
        }
        public LedgerGroupDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public List<ViewModel.Common.List_Common> List()
        {
           
            
           // var lst = _db.Pr_CommonList(1, "LEDGRROUP").ToList();

            var lst = _db.Pr_CommonList(1, "LEDGERGROUP").ToList().Select(x => new ViewModel.Common.List_Common { Id = x.Id, Name = x.Name, Alias_Name = x.Alias_Name, Under_Name = x.Under_Name, Remarks = x.Remarks }).ToList();
            return lst;
        }
        public bool create(LedgerGroup modelledgrp)
        {
            bool result = false;
            if (modelledgrp != null)
            {
                Ledger_Group tblledgrp = new Ledger_Group();
                // tblledgrp.Id = modelledgrp.Ledger.Id;
                tblledgrp.Name = modelledgrp.Ledger.Name;
                tblledgrp.Remarks = modelledgrp.Ledger.Remarks;
                tblledgrp.Under_Group = modelledgrp.Ledger.UnderId;
                tblledgrp.Short_Name = modelledgrp.Ledger.Alias_Name;
                tblledgrp.Is_Active = true;
                tblledgrp.Is_Deleted = false;
                tblledgrp.Modified_By = 1;
              
                tblledgrp.Company_Id = 1;
                tblledgrp.Created_By = 1;
                tblledgrp.Modified_Date = DateTime.Now;
                tblledgrp.Created_Date = DateTime.Now;
                Save(tblledgrp);
                result = true;
            }
            return result;
        }
        public LedgerGroup Find(int Id)
        {
            Ledger_Group tblledgrp = _db.Ledger_Group.Find(Id);
            LedgerGroup modelledgrp = new LedgerGroup();
            modelledgrp.Ledger.Id = tblledgrp.Id;
            modelledgrp.Ledger.Name = tblledgrp.Name;
            modelledgrp.Ledger.Alias_Name = tblledgrp.Short_Name;
           
            modelledgrp.Ledger.CompanyId = tblledgrp.Company_Id;
            modelledgrp.Ledger.Remarks = tblledgrp.Remarks;
            modelledgrp.Ledger.UnderId = tblledgrp.Under_Group;
          //  modelledgrp.Ledger.UnderName = modelledgrp.Ledger.UnderId != 0 ? _db.Ledger_Group.FirstOrDefault(x => x.Under_Group == modelledgrp.Ledger.Id).Name : "Primary";
            return modelledgrp;


        }
        public bool Update(LedgerGroup modelledgrp)
        {
            bool result = false;
            if (modelledgrp != null)
            {
                Ledger_Group tblledgrp = _db.Ledger_Group.Find(modelledgrp.Ledger.Id);
                tblledgrp.Name = modelledgrp.Ledger.Name;
                tblledgrp.Remarks = modelledgrp.Ledger.Remarks;
                tblledgrp.Under_Group = modelledgrp.Ledger.UnderId;
                tblledgrp.Short_Name = modelledgrp.Ledger.Alias_Name;
                tblledgrp.Modified_By = 1;

                tblledgrp.Created_By = 1;
                tblledgrp.Modified_Date = DateTime.Now;
                Save(tblledgrp);
                result = true;
            }
            return result;
        }
        public bool Save(Ledger_Group ledgrp)
        {
            bool result = false;
            try
            {
                bool res = _db.Ledger_Group.Any(m => m.Company_Id == 1 && (0 == ledgrp.Id || m.Id != ledgrp.Id) && (m.Name.Trim().ToUpper().Equals(ledgrp.Name.ToUpper().Trim())));
                if (res == false)
                {
                    if (ledgrp.Id == 0)

                        _db.Ledger_Group.Add(ledgrp);
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

       
        public List<ViewModel.Common.DDLBind> DDLBind(string Tag, string searchText)
        {
            List<DDLBind> list = new List<DDLBind>();
            if(Tag.Trim().ToUpper()=="LEDGERGROUP")
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Ledger_Group.Where(x => x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Ledger_Group.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
            }
            else if (Tag.Trim().ToUpper() == "STOCKGROUP")
            {
                return _db.Stock_Group.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
            }
            else if (Tag.Trim().ToUpper() == "STOCKCATEGORY")
            {
                return _db.Stock_Category.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
            }
            return list;
        }

        public bool IsNameExists(long Id, string Name)
        {
            return _db.Ledger_Group.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
        }
    }
}
