using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ViewModel.Transactions;

namespace DataLayer.Transactions
{
    public class PaymentDbLayer
    {
        INVENTORY_DBEntities _db;
 
        public PaymentDbLayer()
        {
            _db = new INVENTORY_DBEntities();

        }
        public PaymentDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public List<ViewModel.Transactions.PaymentInfo> List(String Tag)
        {
            List<ViewModel.Transactions.PaymentInfo> list = new List<PaymentInfo>();
            
            
            
            //  var lst = _db.Pr_PaymentList(1, Tag).ToList();
            var lst = _db.Payment_Info.ToList();
            lst.ForEach((x) => list.Add(new PaymentInfo
            {
                
                Id = x.Id,

                VC_No = x.VC_No ?? "N/A",
                Payment_Date = x.Payment_Date,
                User_Id = x.Supplier_Id,
                Amount = x.Amount,
               // Tag_Type = x.Tag_Type,
                Balance = x.Balance,
                Bank_Name = x.Bank_Name ?? "N/A",
                Remarks = x.Remarks ?? "N/A",
                Cheque_No = x.Cheque_No ?? "N/A",

            }));
            return list;

        }
        public bool Create(ViewModel.Transactions.PaymentInfo modelPayment)
        {
            bool result = false;
         
            try
            {
               

             

                if (modelPayment != null)
                {
                    Payment_Info tblPayment = new Payment_Info();
                    tblPayment.Account_No = modelPayment.Account_No;
                    tblPayment.Amount = modelPayment.Amount;
                    tblPayment.Balance = modelPayment.Balance ?? 0.00m;
                    tblPayment.Tax_Amount = modelPayment.Tax_Amount ?? 0.00m;
                    tblPayment.Bill_By_Bill = false;
                    tblPayment.VC_No = GEN_BillNo();

                    tblPayment.Company_Id = 1;
                    tblPayment.Created_By = 1;
                    tblPayment.Created_Date = DateTime.Now;
                    tblPayment.Modified_By = 1;
                    tblPayment.Modified_Date = DateTime.Now;
                    tblPayment.Remarks = modelPayment.Remarks ?? string.Empty;
                    //tblPayment.Tag_Type = modelPayment.Tag_Type ?? "SUPPLIER";
                    tblPayment.Supplier_Id = modelPayment.User_Id;
                    tblPayment.IFSC_Code = modelPayment.IFSC_Code;
                    tblPayment.Cheque_No = modelPayment.Cheque_No;
                    tblPayment.Bank_Name = modelPayment.Bank_Name;
                    tblPayment.Is_Active = true;
                    tblPayment.Is_Deleted = false;
                    tblPayment.Payment_Date = modelPayment.Payment_Date;
                    tblPayment.Payment_Type = modelPayment.Payment_Type == 1 ? "CASH" : modelPayment.Payment_Type == 2 ? "CHEQUE" : "NEFT";
                    Save(tblPayment);

                    result = true;
                    if (result == true)
                    {
                     result=   Payment_Tra(modelPayment.BillList.Where(x => x.Status == true).ToList(), tblPayment.Id);

                    }
                }
            }
            catch
            {

                result = false;
            }
            return result;
        }
        private bool Payment_Tra(List<ViewModel.Transactions.BillList> ModelBillList, long Pmt_Id)
        {
            System.Data.DataTable tbl = new System.Data.DataTable("Product");
            System.Data.DataColumn[] columns = new System.Data.DataColumn[6];
            columns[0] = new System.Data.DataColumn("Pur_Id", typeof(int));
            columns[1] = new System.Data.DataColumn("Pur_VC_No", typeof(string));
            columns[2] = new System.Data.DataColumn("Pmt_Id", typeof(long));
            columns[3] = new System.Data.DataColumn("Balance", typeof(decimal));
            columns[4] = new System.Data.DataColumn("PaidBalance", typeof(decimal));
            columns[5] = new System.Data.DataColumn("Status", typeof(bool));
            tbl.Columns.AddRange(columns);

            foreach (var item in ModelBillList)
            {
                var row = tbl.NewRow();

                row["Pur_Id"] = item.Pur_Id;
                row["Pur_VC_No"] = item.VC_No;
                row["Pmt_Id"] = Pmt_Id;
                row["Balance"] = item.Balance;
                row["PaidBalance"] = item.PaidBalance;
                row["Status"] = item.Status;
                tbl.Rows.Add(row);
            }




            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
            var regularConnectionString = builder.ProviderConnectionString;
            using (SqlConnection connection = new SqlConnection(regularConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);

                command.CommandText = "Pr_UpdatePayment";
                command.Parameters.Clear();
                command.CommandType = CommandType.StoredProcedure;
                // command.Parameters.AddWithValue("@someIdForParameter", someId);
                command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                command.Parameters.AddWithValue("@Pmt_Id", Pmt_Id).SqlDbType = SqlDbType.BigInt;
                command.ExecuteNonQuery();

                connection.Close();

                bool result = false;

                return result;
            };
        }
        public bool Update(ViewModel.Transactions.PaymentInfo modelPayment)
        {
            bool result = false;
            try
            {
                if (modelPayment != null && modelPayment.Id != 0)
                {
                    Payment_Info tblPayment = _db.Payment_Info.Find(modelPayment.Id);
                    tblPayment.Account_No = modelPayment.Account_No;
                    tblPayment.Amount = modelPayment.Amount;
                    tblPayment.Balance = modelPayment.Balance;
                    tblPayment.Tax_Amount = modelPayment.Tax_Amount;
                    tblPayment.Bill_By_Bill = false;
                    tblPayment.Modified_By = 1;
                    tblPayment.Modified_Date = DateTime.Now;
                    tblPayment.Remarks = modelPayment.Remarks ?? string.Empty;
                    //tblPayment. = modelPayment.Tag_Type;
                    tblPayment.Supplier_Id = modelPayment.User_Id;
                    tblPayment.IFSC_Code = modelPayment.IFSC_Code;
                    tblPayment.Cheque_No = modelPayment.Cheque_No;
                    tblPayment.Bank_Name = modelPayment.Bank_Name;
                    tblPayment.Payment_Type = modelPayment.Payment_Type == 1 ? "CASH" : modelPayment.Payment_Type == 2 ? "CHEQUE" : "NEFT";
                    tblPayment.Payment_Date = modelPayment.Payment_Date;
                    Save(tblPayment);
                    result = true;
                    if (result == true)
                    {
                        result = Payment_Tra(modelPayment.BillList.Where(x => x.Status == true).ToList(), tblPayment.Id);

                    }
                }

            }
            catch
            {

                result = false;
            }
            return result;
        }
        public ViewModel.Transactions.PaymentInfo Find(int Id)
        {

            Payment_Info tblPayment = new Payment_Info();
            tblPayment = Id > 0 ? _db.Payment_Info.Find(Id) : Id <= 0 ? _db.Payment_Info.FirstOrDefault() : null;
            ViewModel.Transactions.PaymentInfo modelPayment = new ViewModel.Transactions.PaymentInfo();

            try
            {
                if (Id != 0)
                {
                    // Payment_Info tblPayment = _db.Payment_Info.Find(Id);
                    modelPayment.Id = tblPayment.Id;
                    modelPayment.Account_No = tblPayment.Account_No;
                    modelPayment.Amount = tblPayment.Amount;
                    modelPayment.Balance = tblPayment.Balance;
                    modelPayment.Tax_Amount = tblPayment.Tax_Amount;
                    modelPayment.Cheque_No = tblPayment.Cheque_No;
                    modelPayment.Bank_Name = tblPayment.Bank_Name;
                    modelPayment.Payment_Type = tblPayment.Payment_Type == "CHEQUE" ? 2 : tblPayment.Payment_Type == "NEFT" ? 3 : 1;
                    modelPayment.Remarks = tblPayment.Remarks ?? string.Empty;
                  //  modelPayment.Tag_Type = tblPayment.Tag_Type;
                    modelPayment.User_Id = tblPayment.Supplier_Id;
                    modelPayment.IFSC_Code = tblPayment.IFSC_Code;
                    modelPayment.Payment_Date = tblPayment.Payment_Date;

                    if (tblPayment.Supplier_Id != 0 )
                    {

                        var tblsupplier = _db.Suppliers.Find(tblPayment.Supplier_Id);
                        if (tblsupplier != null)
                        {
                            modelPayment.Code = tblsupplier.Code;
                            modelPayment.Name = tblsupplier.Name;
                            modelPayment.Email = tblsupplier.Email_Id;
                            modelPayment.Contact_No = tblsupplier.Contact_No;
                        }

                    }

                }
            }
            catch
            {

                modelPayment = null;
            }
            return modelPayment;
        }
        public String GEN_BillNo()
        {
            string Challan_No = "";

            int countRows = _db.Payment_Info.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Payment_Info.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().VC_No;
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
        public List<ViewModel.Transactions.PaymentDetails> PaymentDetails(int UserId, string Tag)
        {
            List<ViewModel.Transactions.PaymentDetails> lst = new List<PaymentDetails>();
            try
            {
                if (!String.IsNullOrWhiteSpace(Tag))
                {
                    var list = _db.Payment_Info.Where(x => x.Supplier_Id == UserId).OrderByDescending(x => x.Id).ToList();
                    list.ForEach((x) => lst.Add(new PaymentDetails { Id = x.Id, Payment_Type = x.Payment_Type, Amount = x.Amount ?? 0.00m, Balance_Amount = x.Balance ?? 0.00m, Payment_Date = x.Payment_Date.ToShortDateString(), Remarks = x.Remarks, Tag_Type ="", Tax_Amount = x.Tax_Amount ?? 0.00m, UserId = x.Supplier_Id }));

                }
            }
            catch
            {

                lst = new List<PaymentDetails>();
            }
            return lst;
        }
        private bool Save(Payment_Info tblPayment)
        {
            bool result = false;
            try
            {
                var res = false;
                if (res == false)
                {
                    if (tblPayment.Id == 0)
                    {
                        tblPayment.Id = _db.Payment_Info.AsNoTracking().Count() != 0 ? _db.Payment_Info.AsNoTracking().Max(x => x.Id) + 1 : 1;

                        _db.Payment_Info.Add(tblPayment);
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
        public List<ViewModel.Transactions.BillingDetails> BillDetails(int UserId, string Tag)
        {
            List<ViewModel.Transactions.BillingDetails> lst = new List<BillingDetails>();
            try
            {
                if (!String.IsNullOrWhiteSpace(Tag))
                {
                    var list = _db.Billing_Info.Where(x => x.User_Id == UserId && x.Tag_Type.Trim().ToUpper() == Tag.Trim().ToUpper()).ToList();
                    list.ForEach((x) => lst.Add(new BillingDetails { Id = x.Id, Chalan_No = x.Bill_VC_No, Amount = x.Amount ?? 0.00m, Billing_Amount = x.Billing_Amount ?? 0.00m, Billing_Date = x.Billing_Date.ToShortDateString(), Remarks = x.Remarks, Tag_Type = x.Tag_Type, Tax_Amount = x.Tax_Amount ?? 0.00m, User_Id = x.User_Id }));

                }
            }
            catch
            {

                lst = new List<BillingDetails>();
            }
            return lst;
        }
        public decimal GetBalance(int UserId)
        {
            decimal bal = 0.00m;
            try
            {
                if (UserId != 0)
                {
                    bal = _db.Pr_GetPendingBal(UserId, "SUPPLIER").FirstOrDefault().Balance;
                }
            }
            catch
            {

                bal = 0.00m;
            }
            return bal;
        }

        public bool GetChequeDetails(int Id = 0, string Cheque_No = "")
        {
            if (!string.IsNullOrWhiteSpace(Cheque_No))
                return _db.Payment_Info.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Cheque_No.Trim().ToUpper().Equals(Cheque_No.Trim().Trim())));
            return false;
        }

        public List<BillList> PendingBillList(int Supplier_Id = 1,int Id=0)
        {
            //List<ViewModel.Common.DDLBind> lstmodel = new List<ViewModel.Common.DDLBind>();
            List<BillList> lstmodel = new List<BillList>();
            if (Supplier_Id > 0)
            {
               
                var list = Id > 0 ? _db.PR_GetPendingBillList(Supplier_Id, Id, "SUPPLIER", "EDIT").ToList() : _db.PR_GetPendingBillList(Supplier_Id, 0, "SUPPLIER", "NEW").ToList();
                list.ForEach(x => lstmodel.Add(new BillList
                    {
                        Pur_Id = x.Id ?? 0,
                        Balance = x.Balance ?? 0.0m,
                        PaidBalance = x.Paid_Amount ?? 0.0m,
                        Bill_Amount = x.Bill_Amount ?? 0.0m,
                        VC_No = x.VC_No,
                        Status = x.Status
                    }));

            }
                    
               

              return lstmodel;
        }
    }
}
