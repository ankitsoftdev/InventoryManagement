using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDMEntity;
using ViewModel.Transactions;
using System.Text.RegularExpressions;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data;
namespace DataLayer.Transactions
{
    public class SalesOrderDbLayer
    {
        INVENTORY_DBEntities _db;
        public SalesOrderDbLayer()
        {
            _db = new INVENTORY_DBEntities();

        }
        public SalesOrderDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public List<SalesOrdeList> List()
        {
            List<SalesOrdeList> list = new List<SalesOrdeList>();
            var lst = _db.Pr_Sales_Order_List(1).ToList();
            lst.ForEach(x => list.Add(new SalesOrdeList { Id = x.Id, Address = x.Address, Amount = x.Amount, Customer_Id = x.Customer_Id, Customer_Name = x.Customer_Name, Email = x.Email_Id, Order_No = x.Order_No, Request_Delivery_Date = x.Request_Delivery_Date.ToShortDateString(), Order_Date = x.Order_Date.ToShortDateString(), Contact_No = x.Contact_No, Remarks = x.Remarks, Sales_Person = x.Sales_Person, Sales_Person_Name = x.Sales_Person == 1 ? "Ajay" : x.Sales_Person == 2 ? "Vijay" : "Sanjay", Status = x.Status }));

            return list;

        }
        public bool Update(SalesOrderInfo modelSalesOrder)
        {
            bool result = false;
            if (modelSalesOrder != null && modelSalesOrder.Id != 0)
            {
                Sales_Order tblSalesOrder = _db.Sales_Order.Find(modelSalesOrder.Id);
                tblSalesOrder.Company_Id = 1;
                tblSalesOrder.Sales_Person = modelSalesOrder.Sales_Person;
                tblSalesOrder.Refered_Id = modelSalesOrder.Quation_Id;
                // tblSalesOrder.Status = modelSalesOrder.Order_Status;
                tblSalesOrder.Remarks = modelSalesOrder.Remarks;
                tblSalesOrder.Sales_Order_VC_No = modelSalesOrder.Order_No;
                tblSalesOrder.Posting_Date = modelSalesOrder.Posting_Date;
                tblSalesOrder.Request_Delivery_Date = modelSalesOrder.Request_Delivery_Date;
                // tblSalesOrder.Is_CreditNote = false;
                tblSalesOrder.Order_Date = modelSalesOrder.Order_Date;

                tblSalesOrder.Amount = modelSalesOrder.Amount;

                tblSalesOrder.Modified_By = 1;
                tblSalesOrder.Modified_Date = DateTime.Now;
                result = Save(tblSalesOrder);
                if (result == true)
                {
                    Update_SalesOrder_Tra(modelSalesOrder.OrderDetails.Where(x => x.Status == true).ToList(), tblSalesOrder.Id);
                }
            }
            return result;
        }
        public bool Create(SalesOrderInfo modelSalesOrder)
        {
            bool result = false;
            if (modelSalesOrder != null)
            {
                Sales_Order tblSalesOrder = new Sales_Order();

                tblSalesOrder.Company_Id = 1;
                tblSalesOrder.Sales_Person = modelSalesOrder.Sales_Person;
                tblSalesOrder.Refered_Id = modelSalesOrder.Quation_Id;

                tblSalesOrder.Remarks = modelSalesOrder.Remarks;
                tblSalesOrder.Sales_Order_VC_No = modelSalesOrder.Order_No;
                tblSalesOrder.Customer_Id = modelSalesOrder.Customer_Id;
                tblSalesOrder.Posting_Date = modelSalesOrder.Posting_Date;
                tblSalesOrder.Request_Delivery_Date = modelSalesOrder.Request_Delivery_Date;
                tblSalesOrder.Is_DeliveryNote = false;
                tblSalesOrder.Order_Date = modelSalesOrder.Order_Date;

                tblSalesOrder.Amount = modelSalesOrder.Amount ?? 0.00m;

                tblSalesOrder.Company_Id = 1;
                tblSalesOrder.Modified_By = 1;
                tblSalesOrder.Modified_Date = DateTime.Now;
                tblSalesOrder.Is_Deleted = false;
                tblSalesOrder.Created_By = 1;
                tblSalesOrder.Is_Active = true;

                tblSalesOrder.Created_Date = DateTime.Now;

                result = AddSalesOrder(modelSalesOrder, result, tblSalesOrder);// Save(tblSalesOrder);

                if (result == true)
                {
                    Update_SalesOrder_Tra(modelSalesOrder.OrderDetails.Where(x => x.Status == true).ToList(), tblSalesOrder.Id);
                }

            }
            return result;
        }
        public SalesOrderInfo FindByQuationId(int Quation_Id)
        {
            Sales_Order tblSalesOrder = _db.Sales_Order.FirstOrDefault(x => x.Refered_Id == Quation_Id);
            SalesOrderInfo modelSalesOrder = new SalesOrderInfo();
            if (tblSalesOrder != null)
            {
                modelSalesOrder.Id = tblSalesOrder.Id;
                //  modelSalesOrder.Order_No = tblSalesOrder.Order_No;
                modelSalesOrder.Remarks = tblSalesOrder.Remarks;
                modelSalesOrder.Quation_Id = Quation_Id;
                // modelSalesOrder.Request_Delivery_Date = tblSalesOrder.Request_Delivery_Date;
                //  modelSalesOrder.Sales_Person = tblSalesOrder.Sales_Person;
                modelSalesOrder.Posting_Date = tblSalesOrder.Posting_Date;
                modelSalesOrder.Order_Date = tblSalesOrder.Order_Date;

                modelSalesOrder.Is_CreditNote = tblSalesOrder.Is_DeliveryNote;

                modelSalesOrder.Amount = tblSalesOrder.Amount;
                modelSalesOrder.Customer_Id = tblSalesOrder.Customer_Id ?? 0;

                if (tblSalesOrder.Customer_Id != 0)
                {
                    var tblcustomer = _db.Customers.Find(tblSalesOrder.Customer_Id);
                    modelSalesOrder.Email = tblcustomer.Email_Id;
                    modelSalesOrder.Contact_No = tblcustomer.Contact_No;

                }
            }
            return modelSalesOrder;
        }
        public SalesOrderInfo Find(long Id)
        {
            Sales_Order tblSalesOrder = new Sales_Order();

            tblSalesOrder = Id > 0 ? _db.Sales_Order.Find(Id) : Id <= 0 ? _db.Sales_Order.FirstOrDefault() : null;
            SalesOrderInfo modelSalesOrder = new SalesOrderInfo();
            if (tblSalesOrder != null)
            {
                modelSalesOrder.Id = tblSalesOrder.Id;
                modelSalesOrder.Order_No = tblSalesOrder.Sales_Order_VC_No;
                modelSalesOrder.Remarks = tblSalesOrder.Remarks;
                modelSalesOrder.Quation_Id = tblSalesOrder.Refered_Id ?? 0;
                modelSalesOrder.Request_Delivery_Date = tblSalesOrder.Request_Delivery_Date;
                modelSalesOrder.Sales_Person = tblSalesOrder.Sales_Person;
                modelSalesOrder.Posting_Date = tblSalesOrder.Posting_Date;
                modelSalesOrder.Order_Date = tblSalesOrder.Order_Date;

                modelSalesOrder.Is_CreditNote = tblSalesOrder.Is_DeliveryNote;

                modelSalesOrder.Amount = tblSalesOrder.Amount;
                modelSalesOrder.Customer_Id = tblSalesOrder.Customer_Id ?? 0;

                if (tblSalesOrder.Customer_Id != 0)
                {
                    var tblcustomer = _db.Ledger_Master.Find(tblSalesOrder.Customer_Id);
                    modelSalesOrder.Email = tblcustomer.Email_Id;
                    modelSalesOrder.Contact_No = tblcustomer.Contact_No;
                    modelSalesOrder.Customer_Name = tblcustomer.Name;

                }
            }
            return modelSalesOrder;


        }
        public bool Delete(long Id)
        {
            bool result = false;
            try
            {
                if (Id != 0)
                {


                    Sales_Order tblSalesOrder = _db.Sales_Order.Find(Id);
                    tblSalesOrder.Is_Deleted = true;
                    tblSalesOrder.Modified_By = 1;
                    tblSalesOrder.Modified_Date = DateTime.Now;
                    result = true;
                }
            }
            catch
            {

                result = false;
            }

            return result;

        }

        public bool CreateByQuation(SalesOrderInfo modelSalesOrder)
        {
            bool result = false;
            long customerId = 0;
            if (modelSalesOrder != null)
            {
                Sales_Order tblSalesOrder = new Sales_Order();
                if (modelSalesOrder.Quation_Id != 0 && modelSalesOrder.Customer_Id == 0)
                {
                    Customer tblcust = new Customer();
                    var res = _db.Suppliers.SingleOrDefault(m => m.Company_Id == 1 && m.Is_Deleted == false && (m.Email_Id.Trim().ToUpper().Equals(modelSalesOrder.Email.ToUpper().Trim())) && (m.Name.Trim().ToUpper().Equals(modelSalesOrder.Customer_Name.ToUpper().Trim())) && (m.Contact_No.Trim().ToUpper().Equals(modelSalesOrder.Contact_No.ToUpper().Trim())));
                    if (res != null)
                    {
                        customerId = res.Id;
                        modelSalesOrder.Customer_Id = res.Id;
                    }
                    else
                    {
                        tblcust.Company_Id = 1;

                        tblcust.Name = modelSalesOrder.Customer_Name;
                        tblcust.Contact_No = modelSalesOrder.Contact_No;
                        tblcust.Email_Id = modelSalesOrder.Email;
                        tblcust.Group_Id = 1;
                        tblcust.Is_Active = true;
                        tblcust.Is_Deleted = false;
                        tblcust.Mnt_Bill_By_Bill = false;
                        tblcust.Opeaning_Bal = 0;
                        tblcust.Credit_Period_Time = 0;
                        tblcust.Pan_No = "N/A";
                        tblcust.Address = "N/A";

                        tblcust.Code = new DataLayer.AccountMaster.CustomerDbLayer().GEN_AccountsCode("CUSTOMERS");
                        tblcust.Created_Date = DateTime.Now;
                        tblcust.Modified_Date = DateTime.Now;
                        tblcust.Modified_By = 1;
                        tblcust.Created_By = 1;
                        tblcust.Country_Id = 104;
                        tblcust.City_Id = 1;
                        tblcust.State_Id = 1973;
                        _db.Customers.Add(tblcust);
                        _db.SaveChanges();
                        modelSalesOrder.Customer_Id = tblcust.Id;
                    }
                    result = AddSalesOrder(modelSalesOrder, result, tblSalesOrder);

                }


                if (result == true)
                {
                    Update_SalesOrder_Tra(modelSalesOrder.OrderDetails.Where(x => x.Status == true).ToList(), tblSalesOrder.Id);
                }

            }
            return result;
        }

        private bool AddSalesOrder(SalesOrderInfo modelSalesOrder, bool result, Sales_Order tblSalesOrder)
        {
            tblSalesOrder.Company_Id = 1;
            tblSalesOrder.Sales_Person = modelSalesOrder.Sales_Person;
            tblSalesOrder.Refered_Id = modelSalesOrder.Quation_Id;

            tblSalesOrder.Remarks = modelSalesOrder.Remarks;
            tblSalesOrder.Sales_Order_VC_No = modelSalesOrder.Order_No;
            tblSalesOrder.Customer_Id = modelSalesOrder.Customer_Id;
            tblSalesOrder.Posting_Date = modelSalesOrder.Posting_Date;
            tblSalesOrder.Request_Delivery_Date = modelSalesOrder.Request_Delivery_Date;
            tblSalesOrder.Is_DeliveryNote = false;
            tblSalesOrder.Order_Date = modelSalesOrder.Order_Date;

            tblSalesOrder.Amount = modelSalesOrder.Amount ?? 0.00m;

            tblSalesOrder.Company_Id = 1;
            tblSalesOrder.Modified_By = 1;
            tblSalesOrder.Modified_Date = DateTime.Now;
            tblSalesOrder.Is_Deleted = false;
            tblSalesOrder.Created_By = 1;
            tblSalesOrder.Is_Active = true;

            tblSalesOrder.Created_Date = DateTime.Now;
            result = Save(tblSalesOrder);
            return result;
        }
        public List<ViewModel.Transactions.SalesOrderInfo_Tra> OrderDetailsByQuationId(long QuationId)
        {
            List<ViewModel.Transactions.SalesOrderInfo_Tra> modellist = new List<SalesOrderInfo_Tra>();
            Common.CommonDbLayer objcommon = new Common.CommonDbLayer();
            QuationDbLayer objquation = new QuationDbLayer();
            var quationChallan = _db.Quation_Master.Find(QuationId);
            var lst = _db.Quation_Master_Tra.Where(x => x.Quation_Id == quationChallan.Id).ToList();
            var finlst = (from l in lst
                          join I in _db.Stock_Item on l.Item_Id equals I.Id
                          join U in _db.UnitMasters on I.Unit_Id equals U.Id
                          select new { Item_Id = l.Item_Id, Item_Name = I.Name, Qty = l.Qty, Quat_Rate = l.Final_Rate, Unit_Name = U.Name, Unit_Id = U.Id }).ToList();

            finlst.ForEach(x => modellist.Add(new SalesOrderInfo_Tra
            {
                Item_Id = x.Item_Id,
                Oreder_Quantity = x.Qty,
                Rate = x.Quat_Rate,
                Ship_Quantity = x.Qty,
                Amount = (x.Qty * x.Quat_Rate),
                Invoice_Quantity = x.Qty,
                Unit_Name = x.Unit_Name,
                Item_Name = x.Item_Name

            }));
            return modellist;
        }
        public List<ViewModel.Transactions.SalesOrderInfo_Tra> OrderDetails(long OrderNo)
        {
            List<ViewModel.Transactions.SalesOrderInfo_Tra> modellist = new List<SalesOrderInfo_Tra>();
            Common.CommonDbLayer objcommon = new Common.CommonDbLayer();
            var lst = _db.Sales_Order_Tra.Where(x => x.Sales_Order_Id == OrderNo).ToList();
            var finlst = (from l in lst
                          join I in _db.Stock_Item on l.Item_Id equals I.Id
                          join U in _db.UnitMasters on I.Unit_Id equals U.Id
                          select new { Id = l.Id, Order_No = l.Id, Item_Id = l.Item_Id, Item_Name = I.Name, Oreder_Quantity = l.Oreder_Quantity, Rate = l.Rate, Remarks = l.Remarks, Unit_Name = U.Name, Unit_Id = U.Id, Invoice_Quantity = l.Invoice_Quantity }).ToList();
            finlst.ForEach(x =>
                modellist.Add(new SalesOrderInfo_Tra
                {
                    Id = x.Id,
                    // Order_No = x.Order_No,
                    Item_Id = x.Item_Id,
                    Oreder_Quantity = x.Oreder_Quantity,
                    Rate = x.Rate,
                    Item_Name = x.Item_Name,
                    Remarks = x.Remarks,

                    Invoice_Quantity = x.Invoice_Quantity,
                    Amount = (x.Oreder_Quantity * x.Rate),
                    Unit_Name = x.Unit_Name


                }));



            return modellist;
        }
        public bool Save(Sales_Order tblSalesOrder)
        {
            bool result = false;
            try
            {
                bool res = true;
                if (res == true)
                {
                    if (tblSalesOrder.Id == 0)
                    {
                        tblSalesOrder.Id = _db.Sales_Order.Count() != 0 ? _db.Sales_Order.Max(x => x.Id)+1 : 1;
                        _db.Sales_Order.Add(tblSalesOrder);
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
        private bool Update_SalesOrder_Tra(List<ViewModel.Transactions.SalesOrderInfo_Tra> ModelSalesOrderTra, long Sales_Order_Id)
        {
            System.Data.DataTable tbl = new System.Data.DataTable("Product");
            System.Data.DataColumn[] columns = new System.Data.DataColumn[7];
            columns[0] = new System.Data.DataColumn("Item_Id", typeof(long));
            columns[1] = new System.Data.DataColumn("Sales_Order_Id", typeof(long));
            columns[2] = new System.Data.DataColumn("Oreder_Quantity", typeof(decimal));

            columns[4] = new System.Data.DataColumn("Invoice_Quantity", typeof(decimal));
            columns[5] = new System.Data.DataColumn("Rate", typeof(decimal));
            columns[6] = new System.Data.DataColumn("Remarks", typeof(string));
            tbl.Columns.AddRange(columns);
            foreach (var item in ModelSalesOrderTra)
            {
                var row = tbl.NewRow();

                row["Item_Id"] = item.Item_Id;
                row["Sales_Order_Id"] = Sales_Order_Id;
                row["Oreder_Quantity"] = item.Oreder_Quantity;
                row["Invoice_Quantity"] = item.Oreder_Quantity;
                row["Rate"] = item.Rate;
                row["Remarks"] = "";
                tbl.Rows.Add(row);
            }
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
            var regularConnectionString = builder.ProviderConnectionString;
            using (SqlConnection connection = new SqlConnection(regularConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = "Pr_UpdateSalesOrder";
                command.Parameters.Clear();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                command.Parameters.AddWithValue("@Sales_Order_Id", Sales_Order_Id).SqlDbType = SqlDbType.NChar;
                command.ExecuteNonQuery();
                connection.Close();
                bool result = true;

                return result;
            };
        }
        public String GEN_OrderNo()
        {
            string Challan_No = "";

            int countRows = _db.Sales_Order.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Sales_Order.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Sales_Order_VC_No;
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
    }
}
