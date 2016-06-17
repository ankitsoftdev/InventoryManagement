using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ViewModel.Common;
namespace DataLayer.AccountMaster
{
   public  class CustomerDbLayer
    {
       INVENTORY_DBEntities _db;
       public CustomerDbLayer()
       {
           _db = new INVENTORY_DBEntities();
          
       }
       public CustomerDbLayer(String ConnectionString)
       {

           _db = new INVENTORY_DBEntities();
           _db.Database.Connection.ConnectionString = ConnectionString;
           //_db.Database.Initialize(true);
       }

       public bool Insert(ViewModel.Ledger.Supplier supplier, string Tagname = "")
       {
           bool status = false;
           if (supplier!=null && supplier.Id==0)
           {
               //Customer objCustomer = new Customer();
               Ledger_Master objCustomer = new Ledger_Master();
               objCustomer.Company_Id = 1;
              
               objCustomer.Name = supplier.Name;
               objCustomer.Code = supplier.Code;
               objCustomer.Alias_Name = supplier.Alias_Name;
               objCustomer.Group_Id = supplier.Group_Id;
               objCustomer.Opeaning_Bal = supplier.Opening_Balance;
               objCustomer.Mnt_Bill_By_Bill = supplier.MaintainRecord_BillbyBill;
               objCustomer.Credit_Period_Time = supplier.CreditPeriodTime;
               objCustomer.Address = supplier.Address.address;
               objCustomer.Country_Id = supplier.Address.Country_Id;
               objCustomer.State_Id = supplier.Address.State_Id;
               objCustomer.City_Id = supplier.Address.City_Id;
               objCustomer.Pin_Code = supplier.Address.Pin_Code;
               objCustomer.Contact_No = supplier.Address.ContactInfo.Mobile;
               objCustomer.Email_Id = supplier.Address.ContactInfo.Email_Id;
               objCustomer.Pan_No = supplier.Account_Detail.PANNumber;
               objCustomer.Created_By = 1;
               objCustomer.Created_Date = System.DateTime.Now;
               objCustomer.Modified_By = 1;
               objCustomer.Modified_Date = System.DateTime.Now;
               objCustomer.Is_Active = true;
               objCustomer.Is_Deleted = false;
               objCustomer.Image_Url = supplier.Image_Path;
               status= Save(objCustomer);

             //  _db.Customers.Add(objCustomer);

           }
           

           _db.SaveChanges();
           return status;
       }
       //public bool SupplierSave(DataLayer.Supplier _Supplier)
       //{
       //    _db.Suppliers.Add(_Supplier);
       //    _db.SaveChanges();
       //    return true;
       //}
       public bool Update(ViewModel.Ledger.Supplier supplier, string Tagname = "")
       {
           bool Status = false;
           if (supplier!=null && supplier.Id>0)
           {
               Ledger_Master objCustomer = _db.Ledger_Master.Find(supplier.Id);
               objCustomer.Name = supplier.Name;
               objCustomer.Code = supplier.Code;
               objCustomer.Alias_Name = supplier.Alias_Name;
               objCustomer.Group_Id = supplier.Group_Id;
               objCustomer.Opeaning_Bal = supplier.Opening_Balance;
               objCustomer.Mnt_Bill_By_Bill = supplier.MaintainRecord_BillbyBill ;
               objCustomer.Credit_Period_Time = supplier.CreditPeriodTime;
               objCustomer.Address = supplier.Address.address;
               objCustomer.Country_Id = supplier.Address.Country_Id;
               objCustomer.State_Id = supplier.Address.State_Id;
               objCustomer.City_Id = supplier.Address.City_Id;
               objCustomer.Pin_Code = supplier.Address.Pin_Code;
               objCustomer.Contact_No = supplier.Address.ContactInfo.Mobile;
               objCustomer.Email_Id = supplier.Address.ContactInfo.Email_Id;
               objCustomer.Pan_No = supplier.Account_Detail.PANNumber;
               objCustomer.Image_Url = supplier.Image_Path ?? objCustomer.Image_Url;
              Status= Save(objCustomer);
           }
          

          // _db.SaveChanges();
           return Status;
       }
       private bool Save(Ledger_Master tblCustomer)
       {
           bool result = false;
           try
           {
               bool res = _db.Ledger_Master.Any(m => m.Company_Id == 1 && m.Is_Deleted == false && (0 == tblCustomer.Id || m.Id != tblCustomer.Id) && (m.Email_Id.Trim().ToUpper().Equals(tblCustomer.Email_Id.ToUpper().Trim())));
               if (res == false)
               {
                   if (tblCustomer.Id == 0)
                   {
                       tblCustomer.Id = _db.Ledger_Master.AsNoTracking().Count() != 0 ? _db.Ledger_Master.AsNoTracking().Max(x => x.Id) + 1 : 1;
                       _db.Ledger_Master.Add(tblCustomer);
                    //   _db.Customers.Add(tblCustomer);
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
     
       public List<ViewModel.Ledger.Supplier> GetCustomer(string Tagname = "")
       {
           List<ViewModel.Ledger.Supplier> lst = new List<ViewModel.Ledger.Supplier>();

           Tagname = !String.IsNullOrWhiteSpace(Tagname) && Tagname == "Supplier" ? "Sundry Creditors" : "Sundry Debtors";
              var l=_db.Sp_Supplierinfo(Tagname,1).ToList();
                      
          
               
               l.ForEach(x=>lst.Add(new ViewModel.Ledger.Supplier
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image_Path = x.Image_Url,
                        Code = x.Code,
                        Alias_Name = x.Alias_Name,
                        Group_Id = x.Group_Id,
                        GroupName = x.GroupName,
                        Opening_Balance = Convert.ToDecimal(x.Opeaning_Bal),
                        MaintainRecord_BillbyBill = x.Mnt_Bill_By_Bill,
                        CreditPeriodTime = Convert.ToInt32(x.Credit_Period_Time),
                        Address = new ViewModel.Common.Address
                        {
                            address = x.Address,
                            CountryName = x.CountryName,
                            StateName = x.StateName,
                            CityName = x.CityName,
                            Pin_Code = x.Pin_Code,
                            ContactInfo = new ViewModel.Common.Contact
                            {
                                Mobile = x.Contact_No,
                                Email_Id = x.Email_Id,
                            }

                        },
                        Account_Detail = new ViewModel.Common.PersonAccountDetails
                        {
                            PANNumber = x.Pan_No,
                        }

                    
                }));
          
           return lst;
       }


       public IEnumerable<ViewModel.Common.DDLBind> DDlGroupLList(string Tag="")
       {
           List<ViewModel.Common.DDLBind> lst = new List<DDLBind>();

           var list = String.Compare(Tag, "Supplier", true) == 0 ? _db.Ledger_Group.Where(x => x.Name.ToUpper() == "Sundry Creditors").ToList() : _db.Ledger_Group.Where(x => x.Name.ToUpper() == "Sundry Debtors").ToList();
           list.ForEach( x => lst.Add(new ViewModel.Common.DDLBind
           {
               Id = x.Id,
               Name = x.Name
           }));
           return lst;
       }

       //public IEnumerable<ViewModel.Common.DDLBind> DDlGroupLList()
       //{
       //    List<ViewModel.Common.DDLBind> ddllist = new List<ViewModel.Common.DDLBind>();
       //    objCustomerDbLayer.GetLedgerGroup().ToList().ForEach(x =>
       //    {
       //        ddllist.Add(new ViewModel.Common.DDLBind
       //        {
       //            Id = x.Id,
       //            Name = x.Name
       //        });
       //    });

       //    return ddllist;
       //}
       public Customer Find(long CusId=0)
       {
           return _db.Customers.Find(CusId);
       }
       public Supplier FindSupplier(long Id=0)
       {
           return _db.Suppliers.Find(Id);
       }
       public bool Delete(int id = 0, string Tagname="")
       {
           bool Status = false;
           if (id != 0)
           {
               Type data = Tagname == "Customers" ? typeof(Customer) : typeof(Supplier);
                   dynamic finddatra = _db.Set(data).Find(id);
                   finddatra.Is_Deleted = true;
                   finddatra.Modified_By = 1;
                   finddatra.Modified_Date = System.DateTime.Now;
                   _db.Entry(typeof(Customer)).State = System.Data.Entity.EntityState.Modified;
                   _db.SaveChanges();
                   Status = true;
           }
           return Status;
       }

       public List<ViewModel.Common.DDLBind> DDLBind(string Tag, string searchText)
       {
           List<ViewModel.Common.DDLBind> list = new List<ViewModel.Common.DDLBind>();
           if (Tag.Trim().ToUpper() == "SUPPLIER")
           {
               list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Suppliers.Where(x => x.Is_Deleted == false && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Suppliers.Where(x => x.Is_Deleted == false && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
           }
           else if (Tag.Trim().ToUpper() == "CUSTOMER")
           {
               list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Customers.Where(x => x.Is_Deleted == false && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Customers.Where(x => x.Is_Deleted == false && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
           }
           else if (Tag.Trim().ToUpper() == "STOCKCATEGORY")
           {
               list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Stock_Category.Where(x => x.Is_Deleted == false && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Stock_Category.Where(x => x.Is_Deleted == false && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
              // return _db.Stock_Category.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
           }
           else if (Tag.Trim().ToUpper() == "STOCKGROUP")
           {
               list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Stock_Group.Where(x => x.Is_Deleted == false && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Stock_Group.Where(x => x.Is_Deleted == false && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
               // return _db.Stock_Group.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
           }
           return list;
       }

       public ViewModel.Ledger.Supplier FindCUSTOMER_SUPPLIER(long Id = 0, string Tagname = "")
       {
           ViewModel.Ledger.Supplier objSupplier = new ViewModel.Ledger.Supplier();
           if (Id > 0)
           {
               var FindData = _db.Ledger_Master.Find(Id);
               objSupplier.Id = FindData.Id;
               objSupplier.Name = FindData.Name;
               objSupplier.Code = FindData.Code;
               objSupplier.Image_Path = FindData.Image_Url;
               objSupplier.Alias_Name = FindData.Alias_Name;
               objSupplier.Group_Id = FindData.Group_Id;
               objSupplier.Opening_Balance = Convert.ToDecimal(FindData.Opeaning_Bal);
               objSupplier.MaintainRecord_BillbyBill = FindData.Mnt_Bill_By_Bill;
               objSupplier.CreditPeriodTime = Convert.ToInt32(FindData.Credit_Period_Time);
               objSupplier.Address = new Address
               {
                   address = FindData.Address,
                   Country_Id = FindData.Country_Id??0,
                   State_Id = FindData.State_Id??0,
                   City_Id = FindData.City_Id??0,
                   Pin_Code = FindData.Pin_Code,
                   ContactInfo = new Contact
                   {
                       Mobile = FindData.Contact_No,
                       Email_Id = FindData.Email_Id
                   }

               };
               objSupplier.Account_Detail = new PersonAccountDetails
               {
                   PANNumber = FindData.Pan_No
               };
           }
           return objSupplier;
       }

       //private static void BindCustomer_SupplierData(ref ViewModel.Ledger.Supplier objSupplier, dynamic FindData)
       //{

       //    objSupplier.Id = FindData.Id;
       //    objSupplier.Name = FindData.Name;
       //    objSupplier.Code = FindData.Code;
       //    objSupplier.Image_Path = FindData.Image_Url;
       //    objSupplier.Alias_Name = FindData.Alias_Name;
       //    objSupplier.Group_Id = FindData.Group_Id;
       //    objSupplier.Opening_Balance = Convert.ToDecimal(FindData.Opeaning_Bal);
       //    objSupplier.MaintainRecord_BillbyBill = FindData.Mnt_Bill_By_Bill;
       //    objSupplier.CreditPeriodTime = Convert.ToInt32(FindData.Credit_Period_Time);
       //    objSupplier.Address = new Address
       //    {
       //        address = FindData.Address,
       //        Country_Id = FindData.Country_Id,
       //        State_Id = FindData.State_Id,
       //        City_Id = FindData.City_Id,
       //        Pin_Code = FindData.Pin_Code,
       //        ContactInfo = new Contact
       //        {
       //            Mobile = FindData.Contact_No,
       //            Email_Id = FindData.Email_Id
       //        }

       //    };
       //    objSupplier.Account_Detail = new PersonAccountDetails
       //    {
       //        PANNumber = FindData.Pan_No
       //    };
       //}
       public String GEN_AccountsCode(string Tag)
       {
           string Challan_No = "";
           // Here Find That Actually What type of Transactional User
           //ViewModel.Transactions.AccountUserType _aUserType = !string.IsNullOrWhiteSpace(Tag) && Tag.Trim().ToUpper() == "SUPPLIER" ? 
           //    ViewModel.Transactions.AccountUserType.Supplier : 
           //    ViewModel.Transactions.AccountUserType.Customer;

           //// Get the Use Counts
           //int cnt = ViewModel.Transactions.AccountUserType.Supplier==_aUserType ? 
           //    _db.Suppliers.AsNoTracking().Where(x => x.Company_Id == 1).Count() : 
           //    _db.Customers.AsNoTracking().Where(x => x.Company_Id == 1).Count();
          
           //if (cnt != 0)
           //    Challan_No = ViewModel.Transactions.AccountUserType.Supplier==_aUserType   ? 
           //        _db.Suppliers.Where(x => x.Company_Id == 1).OrderByDescending(x=>x.Id).FirstOrDefault().Code :
           //        _db.Customers.Where(x => x.Company_Id == 1).OrderByDescending(x => x.Id).FirstOrDefault().Code;

           int cnt = _db.Ledger_Master.AsNoTracking().Where(x => x.Company_Id == 1).Count();
           if (cnt != 0)
               Challan_No = _db.Ledger_Master.Where(x => x.Company_Id == 1).OrderByDescending(x => x.Id).FirstOrDefault().Code;
           Challan_No = !string.IsNullOrWhiteSpace(Challan_No) ?
               Regex.Replace(Challan_No, @"\d+(?=\D*$)",
               m => (Convert.ToInt64(m.Value) + 1).ToString().PadLeft(5, '0')) : 
               "1".PadLeft(5, '0');
          
           return Challan_No;
       }
       public bool IsEmailIdExists(string Tag, int Id = 0,string Email="")
       {

           ViewModel.Transactions.AccountUserType _aUserType = !string.IsNullOrWhiteSpace(Tag) && Tag.Trim().ToUpper() == "SUPPLIER" ?
           ViewModel.Transactions.AccountUserType.Supplier :
           ViewModel.Transactions.AccountUserType.Customer;

           if (ViewModel.Transactions.AccountUserType.Supplier==_aUserType)
           {
               return _db.Ledger_Master.Any(m => m.Company_Id == 1 && m.Is_Deleted == false && (0 == Id || m.Id != Id) && (m.Email_Id.Trim().ToUpper().Equals(Email.ToUpper().Trim())));
           }
           if (ViewModel.Transactions.AccountUserType.Customer == _aUserType)
           {
               return _db.Ledger_Master.Any(m => m.Company_Id == 1 && m.Is_Deleted == false && (0 == Id || m.Id != Id) && (m.Email_Id.Trim().ToUpper().Equals(Email.ToUpper().Trim())));
           }
           else return false;
          
       }
    }
}
