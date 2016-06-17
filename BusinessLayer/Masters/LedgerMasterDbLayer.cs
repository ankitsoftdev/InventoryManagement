using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDMEntity;
using ViewModel.Common;
using System.Text.RegularExpressions;
namespace DataLayer.Masters
{
  public  class LedgerMasterDbLayer
    {
      private  INVENTORY_DBEntities _db;
        public LedgerMasterDbLayer()
        {
            _db = new INVENTORY_DBEntities();
        }

        public LedgerMasterDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
            
        }
        public List<ViewModel.Common.List_Common> List()
        {


            // var lst = _db.Pr_CommonList(1, "LEDGRROUP").ToList();

            var lst = _db.Pr_CommonList(1, "LEDGERMASTER").ToList().Select(x => new ViewModel.Common.List_Common { Id = x.Id, Name = x.Name, Alias_Name = x.Alias_Name, Under_Name = x.Under_Name, Remarks = x.Remarks }).ToList();
            return lst;
        }
       
        public bool Create(LedgerMasterInfo modelledmaster)
        {
            bool result = false;
            if (modelledmaster != null && modelledmaster.Id==0)
            {
                Ledger_Master tblledgermaster = new Ledger_Master();
              //  tblledgermaster.Id = _db.Ledger_Master.Max(x=>x.Id)+1 ;
                tblledgermaster.Code = GEN_LedgerCode();
                tblledgermaster.Name = modelledmaster.Name;
                tblledgermaster.Group_Id = modelledmaster.Group_Id;
                tblledgermaster.Alias_Name = modelledmaster.Alias_Name;
                tblledgermaster.Contact_No = modelledmaster.Contact_No;
                tblledgermaster.Credit_Period_Time = modelledmaster.Credit_Period_Time;
                tblledgermaster.Address = modelledmaster.Address;
                tblledgermaster.Email_Id = modelledmaster.Email_Id;
                tblledgermaster.Country_Id = modelledmaster.Country_Id;
                tblledgermaster.State_Id = modelledmaster.State_Id;
                tblledgermaster.City_Id = modelledmaster.City_Id;
                tblledgermaster.Image_Url = modelledmaster.Image_Url;
                tblledgermaster.Mnt_Bill_By_Bill = modelledmaster.Mnt_Bill_By_Bill;
                tblledgermaster.Opeaning_Bal = modelledmaster.Opeaning_Bal;
                tblledgermaster.Pan_No = modelledmaster.Pan_No;
                tblledgermaster.Pin_Code = modelledmaster.Pin_Code;
                tblledgermaster.Remarks = modelledmaster.Remarks;
                tblledgermaster.Is_Active = true;
                tblledgermaster.Is_Deleted = false;
                tblledgermaster.Modified_By = 1;
             
                tblledgermaster.Company_Id = 1;
                tblledgermaster.Created_By = 1;
                tblledgermaster.Modified_Date = DateTime.Now;
                tblledgermaster.Created_Date = DateTime.Now;
                Save(tblledgermaster);
                result = true;
            }
            return result;
        }
        public LedgerMasterInfo Find(int Id)
        {
            LedgerMasterInfo modelledmaster = new LedgerMasterInfo();
            if (Id > 0)
            {
                Ledger_Master tblledgermaster = _db.Ledger_Master.Find(Id);
                modelledmaster.Id = tblledgermaster.Id;
                modelledmaster.Name = tblledgermaster.Name;
                modelledmaster.Alias_Name = tblledgermaster.Alias_Name;
                modelledmaster.Code = tblledgermaster.Code;
                modelledmaster.Contact_No = tblledgermaster.Contact_No;
                modelledmaster.Email_Id = tblledgermaster.Email_Id;
                modelledmaster.Group_Id = tblledgermaster.Group_Id;
                modelledmaster.Image_Url = tblledgermaster.Image_Url;
                modelledmaster.Mnt_Bill_By_Bill = tblledgermaster.Mnt_Bill_By_Bill;
                modelledmaster.Opeaning_Bal = tblledgermaster.Opeaning_Bal;
                modelledmaster.Pan_No = tblledgermaster.Pan_No;
                modelledmaster.Pin_Code = tblledgermaster.Pin_Code;
                modelledmaster.Address = tblledgermaster.Address;
                modelledmaster.Country_Id = tblledgermaster.Country_Id;
                modelledmaster.State_Id = tblledgermaster.State_Id;
                modelledmaster.City_Id = tblledgermaster.City_Id;
                modelledmaster.Remarks = tblledgermaster.Remarks;
             
            }
           
            return modelledmaster;


        }
        public bool Update(LedgerMasterInfo modelledmaster)
        {
            bool result = false;
            if (modelledmaster != null)
            {
                Ledger_Master tblledgermaster = _db.Ledger_Master.Find(modelledmaster.Id);
               // tblledgermaster.Code = modelledmaster.Code;
                tblledgermaster.Name = modelledmaster.Name;
                tblledgermaster.Group_Id = modelledmaster.Group_Id;
                tblledgermaster.Alias_Name = modelledmaster.Alias_Name;
                tblledgermaster.Contact_No = modelledmaster.Contact_No;
                tblledgermaster.Credit_Period_Time = modelledmaster.Credit_Period_Time;
                tblledgermaster.Address = modelledmaster.Address;
                tblledgermaster.Email_Id = modelledmaster.Email_Id;
                tblledgermaster.Country_Id = modelledmaster.Country_Id;
                tblledgermaster.State_Id = modelledmaster.State_Id;
                tblledgermaster.City_Id = modelledmaster.City_Id;
                tblledgermaster.Image_Url = modelledmaster.Image_Url;
                tblledgermaster.Mnt_Bill_By_Bill = modelledmaster.Mnt_Bill_By_Bill;
                tblledgermaster.Opeaning_Bal = modelledmaster.Opeaning_Bal;
                tblledgermaster.Pan_No = modelledmaster.Pan_No;
                tblledgermaster.Pin_Code = modelledmaster.Pin_Code;
                tblledgermaster.Remarks = modelledmaster.Remarks;
                tblledgermaster.Modified_By = 1;
                tblledgermaster.Modified_Date = DateTime.Now;
                Save(tblledgermaster);
                result = true;
            }
            return result;
        }
        private bool Save(Ledger_Master tblledgermaster)
        {
            bool result = false;
            try
            {
                bool res = _db.Ledger_Master.Any(m => m.Company_Id == 1 && (0 == tblledgermaster.Id || m.Id != tblledgermaster.Id) && (m.Name.Trim().ToUpper().Equals(tblledgermaster.Name.ToUpper().Trim())));
                if (res == false)
                {
                    if (tblledgermaster.Id == 0)
                    {
                        tblledgermaster.Id = _db.Ledger_Master.Count()!=0? _db.Ledger_Master.Max(x => x.Id) + 1:1;
                        _db.Ledger_Master.Add(tblledgermaster);
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
        public String GEN_LedgerCode()
        {
            string Challan_No = "";

            int countRows = _db.Ledger_Master.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Ledger_Master.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Code;
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
        public bool Delete(int Id)
        {
            bool result = false;
            try
            {
                if (Id > 0)
                {
                    Ledger_Master tblledgermaster = _db.Ledger_Master.Find(Id);
                    tblledgermaster.Is_Deleted = true;
                    tblledgermaster.Modified_By = 1;
                    tblledgermaster.Modified_Date = DateTime.Now;
                    _db.SaveChanges();
                }
            }
            catch 
            {

                result = false;
            }
           
            return result;
        }
        public bool IsNameExists(int Id, string Name)
        {
            return _db.Ledger_Master.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
        }
    }
}
