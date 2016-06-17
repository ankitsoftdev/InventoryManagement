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
   public class ReceiptDbLayer
    {
       INVENTORY_DBEntities _db;
       public ReceiptDbLayer()
       {
           _db = new INVENTORY_DBEntities();
       }
       public ReceiptDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
       public List<ReceiptInfo> List(String Tag)
       {
           List<ReceiptInfo> list = new List<ReceiptInfo>();



           //  var lst = _db.Pr_PaymentList(1, Tag).ToList();
           var lst = _db.Receipt_Info.ToList();
           lst.ForEach((x) => list.Add(new ReceiptInfo
           {

               Id = x.Id,

               VC_No = x.VC_No ?? "N/A",
               Receipt_Date = x.Receipt_Date,
               User_Id = x.Customer_Id,
               Amount = x.Amount,
               // Tag_Type = x.Tag_Type,
               Balance = x.Balance,
               Bank_Name = x.Bank_Name ?? "N/A",
               Remarks = x.Remarks ?? "N/A",
               Cheque_No = x.Cheque_No ?? "N/A",

           }));
           return list;

       }
       public bool Create(ViewModel.Transactions.ReceiptInfo modelReceipt)
       {
           bool result = false;

           try
           {




               if (modelReceipt != null)
               {
                   Receipt_Info tblReceipt = new Receipt_Info();
                   tblReceipt.Account_No = modelReceipt.Account_No;
                   tblReceipt.Amount = modelReceipt.Amount;
                   tblReceipt.Balance = modelReceipt.Balance ?? 0.00m;
                   tblReceipt.Tax_Amount = modelReceipt.Tax_Amount ?? 0.00m;
                   tblReceipt.Bill_By_Bill = false;
                   tblReceipt.VC_No = GEN_BillNo();

                   tblReceipt.Company_Id = 1;
                   tblReceipt.Created_By = 1;
                   tblReceipt.Created_Date = DateTime.Now;
                   tblReceipt.Modified_By = 1;
                   tblReceipt.Modified_Date = DateTime.Now;
                   tblReceipt.Remarks = modelReceipt.Remarks ?? string.Empty;
                   //tblReceipt.Tag_Type = modelReceipt.Tag_Type ?? "SUPPLIER";
                   tblReceipt.Customer_Id = modelReceipt.User_Id;
                   tblReceipt.IFSC_Code = modelReceipt.IFSC_Code;
                   tblReceipt.Cheque_No = modelReceipt.Cheque_No;
                   tblReceipt.Bank_Name = modelReceipt.Bank_Name;
                   tblReceipt.Is_Active = true;
                   tblReceipt.Is_Deleted = false;
                   tblReceipt.Receipt_Date = modelReceipt.Receipt_Date;
                   tblReceipt.Receipt_Type = modelReceipt.Receipt_Type == 1 ? "CASH" : modelReceipt.Receipt_Type == 2 ? "CHEQUE" : "NEFT";
                   Save(tblReceipt);

                   result = true;
                   if (result == true)
                   {
                       
                       result = Receipt_Tra(modelReceipt.BillList.Where(x => x.Status == true).ToList(), tblReceipt.Id);

                   }
               }
           }
           catch
           {

               result = false;
           }
           return result;
       }
       public List<ViewModel.Transactions.ReceiptDetails> ReceiptDetails(int UserId, string Tag)
       {
           List<ViewModel.Transactions.ReceiptDetails> lst = new List<ReceiptDetails>();
           try
           {
               if (!String.IsNullOrWhiteSpace(Tag))
               {
                   var list = _db.Receipt_Info.Where(x => x.Customer_Id == UserId).OrderByDescending(x => x.Id).ToList();
                   list.ForEach((x) => lst.Add(new ReceiptDetails { Id = x.Id, Receipt_Type = x.Receipt_Type, Amount = x.Amount ?? 0.00m, Balance_Amount = x.Balance ?? 0.00m, Receipt_Date = x.Receipt_Date.ToShortDateString(), Remarks = x.Remarks, Tag_Type = "", Tax_Amount = x.Tax_Amount ?? 0.00m, UserId = x.Customer_Id }));

               }
           }
           catch
           {

               lst = new List<ReceiptDetails>();
           }
           return lst;
       }
       private bool Receipt_Tra(List<ViewModel.Transactions.BillList> ModelBillList, long Recpt_Id)
       {
           System.Data.DataTable tbl = new System.Data.DataTable("Product");
           System.Data.DataColumn[] columns = new System.Data.DataColumn[6];
           columns[0] = new System.Data.DataColumn("Sale_Id", typeof(int));
           columns[1] = new System.Data.DataColumn("Sale_VC_No", typeof(string));
           columns[2] = new System.Data.DataColumn("Recpt_Id", typeof(long));
           columns[3] = new System.Data.DataColumn("Balance", typeof(decimal));
           columns[4] = new System.Data.DataColumn("RecptBalance", typeof(decimal));
           columns[5] = new System.Data.DataColumn("Status", typeof(bool));
           tbl.Columns.AddRange(columns);

           foreach (var item in ModelBillList)
           {
               var row = tbl.NewRow();

               row["Sale_Id"] = item.Pur_Id;
               row["Sale_VC_No"] = item.VC_No;
               row["Recpt_Id"] = Recpt_Id;
               row["Balance"] = item.Balance;
               row["RecptBalance"] = item.PaidBalance;
               row["Status"] = item.Status;
               tbl.Rows.Add(row);
           }




           var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
           var regularConnectionString = builder.ProviderConnectionString;
           using (SqlConnection connection = new SqlConnection(regularConnectionString))
           {
               connection.Open();

               SqlCommand command = new SqlCommand("", connection);

               command.CommandText = "Pr_UpdateReceipt";
               command.Parameters.Clear();
               command.CommandType = CommandType.StoredProcedure;
               // command.Parameters.AddWithValue("@someIdForParameter", someId);
               command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
               command.Parameters.AddWithValue("@Recpt_Id", Recpt_Id).SqlDbType = SqlDbType.BigInt;
               command.ExecuteNonQuery();

               connection.Close();

               bool result = false;

               return result;
           };
       }
       public bool Update(ViewModel.Transactions.ReceiptInfo modelReceipt)
       {
           bool result = false;
           try
           {
               if (modelReceipt != null && modelReceipt.Id != 0)
               {
                   Receipt_Info tblReceipt = _db.Receipt_Info.Find(modelReceipt.Id);
                   tblReceipt.Account_No = modelReceipt.Account_No;
                   tblReceipt.Amount = modelReceipt.Amount;
                   tblReceipt.Balance = modelReceipt.Balance;
                   tblReceipt.Tax_Amount = modelReceipt.Tax_Amount;
                   tblReceipt.Bill_By_Bill = false;
                   tblReceipt.Modified_By = 1;
                   tblReceipt.Modified_Date = DateTime.Now;
                   tblReceipt.Remarks = modelReceipt.Remarks ?? string.Empty;
                   //tblReceipt. = modelReceipt.Tag_Type;
                   tblReceipt.Customer_Id = modelReceipt.User_Id;
                   tblReceipt.IFSC_Code = modelReceipt.IFSC_Code;
                   tblReceipt.Cheque_No = modelReceipt.Cheque_No;
                   tblReceipt.Bank_Name = modelReceipt.Bank_Name;
                   tblReceipt.Receipt_Type = modelReceipt.Receipt_Type == 1 ? "CASH" : modelReceipt.Receipt_Type == 2 ? "CHEQUE" : "NEFT";
                   tblReceipt.Receipt_Date = modelReceipt.Receipt_Date;
                   Save(tblReceipt);
                   result = true;
                   if (result == true)
                   {
                       result = Receipt_Tra (modelReceipt.BillList.Where(x => x.Status == true).ToList(), tblReceipt.Id);

                   }
               }

           }
           catch
           {

               result = false;
           }
           return result;
       }
       public ViewModel.Transactions.ReceiptInfo Find(int Id)
       {

           Receipt_Info tblReceipt = new Receipt_Info();
           tblReceipt = Id > 0 ? _db.Receipt_Info.Find(Id) : Id <= 0 ? _db.Receipt_Info.FirstOrDefault() : null;
           ViewModel.Transactions.ReceiptInfo modelReceipt = new ViewModel.Transactions.ReceiptInfo();

           try
           {
               if (Id != 0)
               {
                   // Payment_Info tblReceipt = _db.Payment_Info.Find(Id);
                   modelReceipt.Id = tblReceipt.Id;
                   modelReceipt.Account_No = tblReceipt.Account_No;
                   modelReceipt.Amount = tblReceipt.Amount;
                   modelReceipt.Balance = tblReceipt.Balance;
                   modelReceipt.Tax_Amount = tblReceipt.Tax_Amount;
                   modelReceipt.Cheque_No = tblReceipt.Cheque_No;
                   modelReceipt.Bank_Name = tblReceipt.Bank_Name;
                   modelReceipt.Receipt_Type = tblReceipt.Receipt_Type == "CHEQUE" ? 2 : tblReceipt.Receipt_Type == "NEFT" ? 3 : 1;
                   modelReceipt.Remarks = tblReceipt.Remarks ?? string.Empty;
                   //  modelReceipt.Tag_Type = tblReceipt.Tag_Type;
                   modelReceipt.User_Id = tblReceipt.Customer_Id;
                   modelReceipt.IFSC_Code = tblReceipt.IFSC_Code;
                   modelReceipt.Receipt_Date = tblReceipt.Receipt_Date;

                   if (tblReceipt.Customer_Id != 0)
                   {

                       var tblsupplier = _db.Suppliers.Find(tblReceipt.Customer_Id);
                       if (tblsupplier != null)
                       {
                           modelReceipt.Code = tblsupplier.Code;
                           modelReceipt.Name = tblsupplier.Name;
                           modelReceipt.Email = tblsupplier.Email_Id;
                           modelReceipt.Contact_No = tblsupplier.Contact_No;
                       }

                   }

               }
           }
           catch
           {

               modelReceipt = null;
           }
           return modelReceipt;
       }
       public String GEN_BillNo()
       {
           string Challan_No = "";

           int countRows = _db.Receipt_Info.Where(x => x.Company_Id == 1).Count();
           if (countRows != 0)
               Challan_No = _db.Receipt_Info.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().VC_No;
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
     
       private bool Save(Receipt_Info tblReceipt)
       {
           bool result = false;
           try
           {
               var res = false;
               if (res == false)
               {
                   if (tblReceipt.Id == 0)
                   {
                       tblReceipt.Id = _db.Receipt_Info.AsNoTracking().Count() != 0 ? _db.Receipt_Info.AsNoTracking().Max(x => x.Id) + 1 : 1;

                       _db.Receipt_Info.Add(tblReceipt);
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
       //public List<ViewModel.Transactions.BillingDetails> BillDetails(int UserId, string Tag)
       //{
       //    List<ViewModel.Transactions.BillingDetails> lst = new List<BillingDetails>();
       //    try
       //    {
       //        if (!String.IsNullOrWhiteSpace(Tag))
       //        {
       //            var list = _db.Billing_Info.Where(x => x.User_Id == UserId && x.Tag_Type.Trim().ToUpper() == Tag.Trim().ToUpper()).ToList();
       //            list.ForEach((x) => lst.Add(new BillingDetails { Id = x.Id, Chalan_No = x.Bill_VC_No, Amount = x.Amount ?? 0.00m, Billing_Amount = x.Billing_Amount ?? 0.00m, Billing_Date = x.Billing_Date.ToShortDateString(), Remarks = x.Remarks, Tag_Type = x.Tag_Type, Tax_Amount = x.Tax_Amount ?? 0.00m, User_Id = x.User_Id }));

       //        }
       //    }
       //    catch
       //    {

       //        lst = new List<BillingDetails>();
       //    }
       //    return lst;
       //}
       public decimal GetBalance(int UserId)
       {
           decimal bal = 0.00m;
           try
           {
               if (UserId != 0)
               {
                   bal = _db.Pr_GetPendingBal(UserId, "CUSTOMER").FirstOrDefault().Balance;
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
               return _db.Receipt_Info.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Cheque_No.Trim().ToUpper().Equals(Cheque_No.Trim().Trim())));
           return false;
       }

       public List<BillList> PendingBillList(long Customer_Id = 1, int Id = 0)
       {
           //List<ViewModel.Common.DDLBind> lstmodel = new List<ViewModel.Common.DDLBind>();
           List<BillList> lstmodel = new List<BillList>();
           if (Customer_Id > 0)
           {

               var list = Id > 0 ? _db.PR_GetPendingBillList(Customer_Id, Id, "CUSTOMER", "EDIT").ToList() : _db.PR_GetPendingBillList(Customer_Id, 0, "CUSTOMER", "NEW").ToList();
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
