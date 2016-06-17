using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDMEntity;
using ViewModel.Transactions;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
namespace DataLayer.Transactions
{
    public  class QuationDbLayer
    {
        INVENTORY_DBEntities _db;
        public QuationDbLayer()
        {
            _db = new INVENTORY_DBEntities();
        }
        public QuationDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public List<ViewModel.Transactions.QuationList> List()
        {
            List<ViewModel.Transactions.QuationList> list = new List<QuationList>();
            long compId = 1;
            var lst = _db.Pr_QuationList(compId, "CUSTOMERS").ToList();

            lst.ForEach((x) => list.Add(new QuationList
            {
                Id = x.Id,
                Name = x.Name,
                Alias = x.Alias,
                Address = x.Address,
                Remarks = x.Remarks,
                Contact_No = x.Contact_No,
                Email = x.Email,
                Quation_Chalan_No = x.Quation_Chalan_No,
                Tag_Type = x.Tag_Type,
                Fncl_Year_Id = x.Fncl_Year_Id,
                Status = x.Is_Approved,
                Amount=x.Amount,
                Quation_Date=x.Quation_Date.ToShortDateString()
            }));
         
         
            return list;
        }
        
        public bool Create(QuationInfo modelQuation)
        {
            bool result = false;
            if (modelQuation != null ){
                Quation_Master tblQuation = new Quation_Master();
             
                tblQuation.Company_Id = 1;
                tblQuation.Customer_Id = modelQuation.Customer_Id;
                tblQuation.Quation_VC_No = modelQuation.Quation_Chalan_No;
                tblQuation.Remarks = modelQuation.Remarks;
                tblQuation.Tag_Type ="TAKE";
                tblQuation.Quation_Type = "SALES";
                tblQuation.Fncl_Year_Id = modelQuation.Fncl_Year_Id>0?modelQuation.Fncl_Year_Id:1;
                tblQuation.Amount = modelQuation.Grand_Total;
                tblQuation.Discount =modelQuation.Discount;
                tblQuation.Company_Id = 1;
                tblQuation.Modified_By = 1;
                tblQuation.Modified_Date = DateTime.Now;
                tblQuation.Is_Deleted = false;
                tblQuation.Created_By = 1;
                tblQuation.Is_Active = true;
                tblQuation.Is_Approved = false;
                tblQuation.Approved_By = 1;
                tblQuation.Created_Date = DateTime.Now;

              result=  Save(tblQuation);

                if(result==true)
                {
                    Update_Qutaion_Tra(modelQuation.QuationItemList.Where(x => x.Status == true).ToList(), tblQuation.Id);
                }
                
            }
            return result;
        }
            public QuationInfo Find(long Id)
            {
                Quation_Master tblQuation = new Quation_Master();
                QuationInfo modelQuation = new QuationInfo();
              tblQuation = Id > 0 ? _db.Quation_Master.Find(Id) : Id <= 0 ? _db.Quation_Master.FirstOrDefault() : null;
           // Quation_Master tblQuation = _db.Quation_Master.Find(Id);
                if (tblQuation != null)
                {
                    modelQuation.Id = tblQuation.Id;
                    modelQuation.Customer_Id = tblQuation.Customer_Id;
                    modelQuation.Fncl_Year_Id = tblQuation.Fncl_Year_Id;
                    modelQuation.Tag_Type = tblQuation.Tag_Type;
                    modelQuation.Quation_Chalan_No = tblQuation.Quation_VC_No;
                    modelQuation.Remarks = tblQuation.Remarks;
                    modelQuation.Grand_Total = tblQuation.Amount ?? 0.00m;
                    modelQuation.Discount = tblQuation.Discount ?? 0.00m;
                    if (tblQuation.Customer_Id != 0)
                    {
                        var tblcustomer = _db.Ledger_Master.Find(tblQuation.Customer_Id);
                        if (tblcustomer != null)
                        {
                            modelQuation.Name = tblcustomer.Name;
                            modelQuation.Email = tblcustomer.Email_Id;
                            modelQuation.Contact_No = tblcustomer.Contact_No;
                        }
                    }
                   
                }
                return modelQuation;
        }
            public bool Delete(long Id)
            {
                bool result = false;
                try
                {
                    if (Id != 0)
                    {
                        Quation_Master tblQuation = _db.Quation_Master.Find(Id);
                        tblQuation.Is_Deleted = true;
                        tblQuation.Modified_By = 1;
                        tblQuation.Modified_Date = DateTime.Now;
                        _db.SaveChanges();
                    }
                }
                catch
                {

                    result = false;
                }

                return result;

            }
            public List<ViewModel.Transactions.QuationInfo_Tra> QuationDetails(long Quation_Id)
            {
                List<ViewModel.Transactions.QuationInfo_Tra> modellist = new List<QuationInfo_Tra>();
                var lst = _db.Quation_Master_Tra.Where(x => x.Quation_Id == Quation_Id).ToList();
                var finlst = (from l in lst
                              join I in _db.Stock_Item on l.Item_Id equals I.Id
                              join U in _db.UnitMasters on I.Unit_Id equals U.Id
                              select new {Id=l.Id, Item_Id = l.Item_Id, Item_Name = I.Name,Final_Rate=l.Final_Rate, Qty = l.Qty, Rate = l.Quat_Rate, Unit_Name = U.Name, Unit_Id = U.Id, UnitPlace = U.No_of_Decimal ?? 0 }).ToList();
                finlst.ForEach(x =>
                    modellist.Add(new QuationInfo_Tra
                    {
                        Id = x.Id,
                        Quation_Chalan_No = "",
                        Item_Id = x.Item_Id,
                        Item_Name=x.Item_Name,
                        UnitPlace=x.UnitPlace,
                        Qty = x.Qty ?? 0.0m,
                        Quat_Rate = x.Rate ?? 0.0m,
                        Final_Rate = x.Final_Rate ?? 0,
                        Total_Amount = x.Rate * x.Qty ?? 00.00m,
                        Unit_Name=new DataLayer.Common.CommonDbLayer().UnitNameByItemId(x.Item_Id)
                    }));



                return modellist;
            }
            private bool Update_Qutaion_Tra(List<ViewModel.Transactions.QuationInfo_Tra> ModelQuationTra,long QuationId=0)
            {
                System.Data.DataTable tbl = new System.Data.DataTable("Product");
                System.Data.DataColumn[] columns = new System.Data.DataColumn[5];
                columns[0] = new System.Data.DataColumn("Item_Id", typeof(long));
                columns[1] = new System.Data.DataColumn("QuationId", typeof(long));
                columns[2] = new System.Data.DataColumn("Qty", typeof(decimal));
                columns[3] = new System.Data.DataColumn("Quat_Rate", typeof(decimal));
                columns[4] = new System.Data.DataColumn("Final_Rate", typeof(decimal));
                tbl.Columns.AddRange(columns);
                foreach (var item in ModelQuationTra)
                {
                    var row = tbl.NewRow();

                    row["Item_Id"] = item.Item_Id;
                    row["QuationId"] = QuationId;
                    row["Qty"] = item.Qty;
                    row["Quat_Rate"] = item.Quat_Rate;
                    row["Final_Rate"] = item.Final_Rate ?? item.Quat_Rate;
                    tbl.Rows.Add(row);
                }
                var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
                var regularConnectionString = builder.ProviderConnectionString;
                using (SqlConnection connection = new SqlConnection(regularConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "Pr_UpdateQuationkMaster";
                    command.Parameters.Clear();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                    command.Parameters.AddWithValue("@QuationId", QuationId).SqlDbType = SqlDbType.NChar;
                    command.ExecuteNonQuery();
                    connection.Close();
                    bool result = true;

                    return result;
                };
            }
            public bool ApproveQuation(long QuationId)
            {
                bool result = false;

                try
                {
                    if (QuationId != 0)
                    {
                        var objtblQuation = _db.Quation_Master.Find(QuationId);
                        objtblQuation.Is_Approved = true;
                        objtblQuation.Approved_By = 1;
                        objtblQuation.Modified_By = 1;
                        objtblQuation.Modified_Date = DateTime.Now;
                        _db.SaveChanges();
                    }
                }
                catch
                {

                    throw;
                }
                return result;

            }
            public String GEN_ChallanNo()
            {
                string Challan_No = "";

                int countRows = _db.Quation_Master.Where(x => x.Company_Id == 1).Count();
                if (countRows != 0)
                    Challan_No = _db.Quation_Master.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Quation_VC_No;
                if (!string.IsNullOrWhiteSpace(Challan_No))
                {
                    Challan_No = Regex.Replace(Challan_No, @"\d+(?=\D*$)",
                       m => (Convert.ToInt64(m.Value) + 1).ToString().PadLeft(5, '0'));


                }
                else
                {
                    Challan_No =  "1".PadLeft(5,'0');
                }


                return Challan_No;
            }
        public bool Update(QuationInfo modelQuation)
        {
            bool result = false;
            if (modelQuation != null && modelQuation.Id!=0)
            {
                Quation_Master tblQuation = _db.Quation_Master.Find(modelQuation.Id);
              
            
                tblQuation.Quation_VC_No = modelQuation.Quation_Chalan_No;
                tblQuation.Remarks = modelQuation.Remarks;
                tblQuation.Customer_Id = modelQuation.Customer_Id;
                tblQuation.Fncl_Year_Id = modelQuation.Fncl_Year_Id;
                tblQuation.Amount = modelQuation.Grand_Total != 0 ? modelQuation.Grand_Total : 0.0m;
                tblQuation.Discount = modelQuation.Discount != 0 ? modelQuation.Discount : 0.0m;
                tblQuation.Modified_By = 1;
                tblQuation.Modified_Date = DateTime.Now;
                result=   Save(tblQuation);
                if (result == true)
                {
                    Update_Qutaion_Tra(modelQuation.QuationItemList.Where(x => x.Status == true).ToList(), tblQuation.Id);
                }
            }
            return result;
        }
        public bool Save(Quation_Master tblQuation)
        {
            bool result = false;
            try
            {
               // bool res = _db..Any(m => m.Company_Id == 1 && (0 == tblQuation.Id || m.Id != tblQuation.Id) && (m.Name.Trim().ToUpper().Equals(tblQuation.Name.ToUpper().Trim())));
                bool res = true;
                if (res == true)
                {
                    if (tblQuation.Id == 0)
                    {
                        tblQuation.Id = _db.Quation_Master.AsNoTracking().Count()!=0 ? _db.Quation_Master.Max(x => x.Id) + 1 : 1;
                        _db.Quation_Master.Add(tblQuation);
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
}
}
