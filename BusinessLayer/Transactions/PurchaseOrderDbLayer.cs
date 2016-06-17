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
    public class PurchaseOrderDbLayer
    {
        INVENTORY_DBEntities _db;
        public PurchaseOrderDbLayer()
        {
            _db = new INVENTORY_DBEntities();
        }
        public PurchaseOrderDbLayer(string ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public List<ViewModel.Transactions.PurchaseOrdeList> List()
        {
            List<PurchaseOrdeList> list = new List<PurchaseOrdeList>();
            var lst = _db.Pr_Purchase_Order_List(1).ToList();
            lst.ForEach(x => list.Add(new PurchaseOrdeList { Id = x.Id, Address = x.Address, Amount = x.Amount, Supplier_Id = x.Supplier_Id, Supplier_Name = x.Customer_Name, Email = x.Email_Id, Order_No = x.Order_No, Request_Delivery_Date = x.Request_Delivery_Date, Order_Date = x.Order_Date, Contact_No = x.Contact_No, Remarks = x.Remarks, Sales_Person = x.Sales_Person, Sales_Person_Name = x.Sales_Person == 1 ? "Ajay" : x.Sales_Person == 2 ? "Vijay" : "Sanjay", Status = x.Status }));

            return list;
        }
        public bool Create(PurchaseOrderInfo modelPurchaseOrder)
        {
            bool result = false;
            if (modelPurchaseOrder != null)
            {
                Purchase_Order tblPurOrder = new Purchase_Order();


                tblPurOrder.Sales_Person = modelPurchaseOrder.Sales_Person;

                tblPurOrder.Status = modelPurchaseOrder.Order_Status;
                tblPurOrder.Remarks = modelPurchaseOrder.Remarks;
                tblPurOrder.Pur_Order_VC_No = modelPurchaseOrder.Order_No;
                tblPurOrder.Supplier_Id = modelPurchaseOrder.Supplier_Id;
                tblPurOrder.Posting_Date = modelPurchaseOrder.Posting_Date;
                tblPurOrder.Request_Delivery_Date = modelPurchaseOrder.Request_Delivery_Date;
                tblPurOrder.Is_Receipt_Note = false;
                tblPurOrder.Order_Date = modelPurchaseOrder.Order_Date;

                tblPurOrder.Amount = modelPurchaseOrder.Amount ?? 0.00m;

                tblPurOrder.Company_Id = 1;
                tblPurOrder.Modified_By = 1;
                tblPurOrder.Modified_Date = DateTime.Now;
                tblPurOrder.Is_Deleted = false;
                tblPurOrder.Created_By = 1;
                tblPurOrder.Is_Active = true;

                tblPurOrder.Created_Date = DateTime.Now;

                result = Save(tblPurOrder);

                if (result == true)
                {
                    Update_Purchase_Tra(modelPurchaseOrder.OrderDetails.Where(x => x.Status == true).ToList(), tblPurOrder.Id);
                }

            }
            return result;
        }
        public bool Update(PurchaseOrderInfo modelPurchaseOrder)
        {
            bool result = false;
            if (modelPurchaseOrder != null && modelPurchaseOrder.Id != 0)
            {

                Purchase_Order tblPurOrder = _db.Purchase_Order.Find(modelPurchaseOrder.Id);
                tblPurOrder.Sales_Person = modelPurchaseOrder.Sales_Person;

                tblPurOrder.Status = modelPurchaseOrder.Order_Status;
                tblPurOrder.Remarks = modelPurchaseOrder.Remarks;
                tblPurOrder.Pur_Order_VC_No = modelPurchaseOrder.Order_No;
                tblPurOrder.Supplier_Id = modelPurchaseOrder.Supplier_Id;
                tblPurOrder.Posting_Date = modelPurchaseOrder.Posting_Date;
                tblPurOrder.Request_Delivery_Date = modelPurchaseOrder.Request_Delivery_Date;
                tblPurOrder.Is_Receipt_Note = false;
                tblPurOrder.Order_Date = modelPurchaseOrder.Order_Date;

                tblPurOrder.Amount = modelPurchaseOrder.Amount ?? 0.00m;

                tblPurOrder.Modified_By = 1;
                tblPurOrder.Modified_Date = DateTime.Now;
                result = Save(tblPurOrder);
                if (result == true)
                {
                    Update_Purchase_Tra(modelPurchaseOrder.OrderDetails.Where(x => x.Status == true).ToList(), tblPurOrder.Id);
                }
            }
            return result;
        }
        public PurchaseOrderInfo Find(long Id)
        {
            Purchase_Order tblPurOrder = new Purchase_Order();
            tblPurOrder = Id > 0 ? _db.Purchase_Order.Find(Id) : Id <= 0 ? _db.Purchase_Order.FirstOrDefault() : null;
            PurchaseOrderInfo modelPurchaseOrder = new PurchaseOrderInfo();
            if (tblPurOrder != null)
            {
                modelPurchaseOrder.Id = tblPurOrder.Id;
                modelPurchaseOrder.Order_No = tblPurOrder.Pur_Order_VC_No;
                modelPurchaseOrder.Remarks = tblPurOrder.Remarks;
                // modelPurchaseOrder.Quation_Id = tblPurOrder.r ?? 0;
                modelPurchaseOrder.Request_Delivery_Date = tblPurOrder.Request_Delivery_Date;
                modelPurchaseOrder.Sales_Person = tblPurOrder.Sales_Person;
                modelPurchaseOrder.Posting_Date = tblPurOrder.Posting_Date;
                modelPurchaseOrder.Order_Date = tblPurOrder.Order_Date;
                modelPurchaseOrder.Order_Status = tblPurOrder.Status;
                modelPurchaseOrder.Is_ReceiptNote = tblPurOrder.Is_Receipt_Note;

                modelPurchaseOrder.Amount = tblPurOrder.Amount;
                modelPurchaseOrder.Supplier_Id = tblPurOrder.Supplier_Id ?? 0;

                if (tblPurOrder.Supplier_Id != 0)
                {
                    var tblcustomer = _db.Customers.Find(tblPurOrder.Supplier_Id);
                    if (tblcustomer != null)
                    {
                        modelPurchaseOrder.Email = tblcustomer.Email_Id;
                        modelPurchaseOrder.Contact_No = tblcustomer.Contact_No;
                    }
                }
            }
            return modelPurchaseOrder;


        }
        public List<ViewModel.Transactions.PurchaseOrderInfo_Tra> OrderDetails(long Pur_Order_Id)
        {
            List<ViewModel.Transactions.PurchaseOrderInfo_Tra> modellist = new List<PurchaseOrderInfo_Tra>();

            var lst = _db.Purchase_Order_Tra.Where(x => x.Pur_Order_Id == Pur_Order_Id).ToList();

            var finlst = (from l in lst
                          join I in _db.Stock_Item on l.Item_Id equals I.Id
                          join U in _db.UnitMasters on I.Unit_Id equals U.Id
                          select new { Item_Id = l.Item_Id, Item_Name = I.Name, Oreder_Quantity = l.Oreder_Quantity, Rate = l.Rate, Unit_Name = U.Name, Unit_Id = U.Id, UnitPlace = U.No_of_Decimal ?? 0 }).ToList();

            finlst.ForEach(x => modellist.Add(new PurchaseOrderInfo_Tra
            {
                Item_Id = x.Item_Id,
                Oreder_Quantity = decimal.Round(x.Oreder_Quantity.Value, x.UnitPlace),
                Rate = x.Rate,
                Ship_Quantity = decimal.Round(x.Oreder_Quantity.Value, x.UnitPlace),
                Amount = (x.Oreder_Quantity.Value * x.Rate.Value),
                Invoice_Quantity = x.Oreder_Quantity,
                Unit_Name = x.Unit_Name,
                Item_Name = x.Item_Name

            }));




            return modellist;
        }
        private bool Update_Purchase_Tra(List<ViewModel.Transactions.PurchaseOrderInfo_Tra> ModelpurchaseOrderTra, long Pur_Order_Id )
        {
            System.Data.DataTable tbl = new System.Data.DataTable("Product");
            System.Data.DataColumn[] columns = new System.Data.DataColumn[6];
            columns[0] = new System.Data.DataColumn("Item_Id", typeof(long));
            columns[1] = new System.Data.DataColumn("Pur_Order_Id", typeof(long));
            columns[2] = new System.Data.DataColumn("Oreder_Quantity", typeof(decimal));
            columns[3] = new System.Data.DataColumn("Ship_Quantity", typeof(decimal));
            
            columns[4] = new System.Data.DataColumn("Rate", typeof(decimal));
            columns[5] = new System.Data.DataColumn("Remarks", typeof(string));
            tbl.Columns.AddRange(columns);
            foreach (var item in ModelpurchaseOrderTra)
            {
                var row = tbl.NewRow();

                row["Item_Id"] = item.Item_Id;
                row["Pur_Order_Id"] = Pur_Order_Id;
                row["Oreder_Quantity"] = item.Oreder_Quantity;
                row["Ship_Quantity"] = item.Oreder_Quantity;
              
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
                command.CommandText = "Pr_UpdatePurchaseOrder";
                command.Parameters.Clear();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                command.Parameters.AddWithValue("@Pur_Order_Id", Pur_Order_Id).SqlDbType = SqlDbType.BigInt;
                command.ExecuteNonQuery();
                connection.Close();
                bool result = true;

                return result;
            };
        }
        public String GEN_OrderNo()
        {
            string Challan_No = "";

            int countRows = _db.Purchase_Order.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Purchase_Order.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Pur_Order_VC_No;
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
        public bool Save(Purchase_Order tblPurOrder)
        {
            bool result = false;
            try
            {
                bool res = true;
                if (res == true)
                {
                    if (tblPurOrder.Id == 0)
                    {
                        tblPurOrder.Id = _db.Purchase_Order.Count() != 0 ? _db.Purchase_Order.Max(x => x.Id) + 1 : 1;
                        _db.Purchase_Order.Add(tblPurOrder);
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
        public bool Delete(long Id)
        {
            bool result = false;
            try
            {
                if (Id != 0)
                {


                    Purchase_Order tblPurOrder = _db.Purchase_Order.Find(Id);
                    tblPurOrder.Is_Deleted = true;
                    tblPurOrder.Modified_By = 1;
                    tblPurOrder.Modified_Date = DateTime.Now;
                    result = true;
                }
            }
            catch
            {

                result = false;
            }

            return result;

        }

    }
   
}
