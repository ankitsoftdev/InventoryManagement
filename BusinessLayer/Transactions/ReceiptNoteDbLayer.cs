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
    public class ReceiptNoteDbLayer
    {
        INVENTORY_DBEntities _db;
        public ReceiptNoteDbLayer()
        {
            _db = new INVENTORY_DBEntities();

        }
        public ReceiptNoteDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
         public List<ReceiptNoteList> List()
        {
            List<ViewModel.Transactions.ReceiptNoteList> objlstmodelReceipt = new List<ReceiptNoteList>();
            var lstDelivery = _db.Pr_ReceiptNoteList(1).ToList();
            lstDelivery.ForEach((x) => objlstmodelReceipt.Add(new ReceiptNoteList { Id = x.Id, Receipt_Date = x.Receipt_Date, Supplier_Name = x.Supplier_Name, Order_Date = x.Order_Date, Remarks = x.Remarks, Purchase_Order_Id = x.Purchae_Order_Id, Purchase_Order_No = x.Purchase_Order_No, Email_Id = x.Email_Id, Receipt_Note_No = x.Receipt_Note_No, Contact_No = x.Contact_No }));
            return objlstmodelReceipt;
             
        }
        public bool Create(ReceiptNoteInfo modelReceiptNote)
        {
            bool result = false;

            if (modelReceiptNote != null)
            {
                Receipt_Note objtblReceiptNote = new Receipt_Note();

                objtblReceiptNote.Amount = modelReceiptNote.Amount;
                objtblReceiptNote.Company_Id = 1;
                objtblReceiptNote.Receipt_Note_No = modelReceiptNote.Receipt_Note_No;
                objtblReceiptNote.Supplier_Id = modelReceiptNote.Supplier_Id;
                objtblReceiptNote.Purchase_Order_Id = modelReceiptNote.Purchase_Order_Id;
                objtblReceiptNote.Purchase_Order_No = modelReceiptNote.Purchase_Order_No;
                objtblReceiptNote.Receipt_Date = Convert.ToDateTime(modelReceiptNote.Receipt_Date);
                objtblReceiptNote.Remarks = modelReceiptNote.Remarks;
                objtblReceiptNote.Is_Active = true;
                objtblReceiptNote.Is_Deleted = false;
                objtblReceiptNote.Created_By = 1;
                objtblReceiptNote.Created_Date = DateTime.Now;
                objtblReceiptNote.Modified_By = 1;
                objtblReceiptNote.Modified_Date = DateTime.Now;
                result = Save(objtblReceiptNote);


                if (result == true)
                {

                  Update_ReceiptNote_Tra(modelReceiptNote.ReceiptDetails.Where(x => x.Status == true).ToList(), objtblReceiptNote.Id);

                }

            }
            return result;
        }
        public bool Update(ReceiptNoteInfo modelReceiptNote)
        {
            bool result = false;

            if (modelReceiptNote != null)
            {
                Receipt_Note objtblReceiptNote = _db.Receipt_Note.Find(modelReceiptNote.Id);

                objtblReceiptNote.Amount = modelReceiptNote.Amount;
             
                objtblReceiptNote.Receipt_Note_No = modelReceiptNote.Receipt_Note_No;
                objtblReceiptNote.Supplier_Id = modelReceiptNote.Supplier_Id;
                objtblReceiptNote.Purchase_Order_Id = modelReceiptNote.Purchase_Order_Id;
                objtblReceiptNote.Purchase_Order_No = modelReceiptNote.Purchase_Order_No;
                objtblReceiptNote.Receipt_Date = Convert.ToDateTime(modelReceiptNote.Receipt_Date);
                objtblReceiptNote.Remarks = modelReceiptNote.Remarks;
                objtblReceiptNote.Modified_By = 1;
                objtblReceiptNote.Modified_Date = DateTime.Now;
                result = Save(objtblReceiptNote);


                if (result == true)
                {

                   Update_ReceiptNote_Tra(modelReceiptNote.ReceiptDetails.Where(x => x.Status == true).ToList(), objtblReceiptNote.Id);

                }

            }
            return result;
        }
        private bool Update_ReceiptNote_Tra(List<ViewModel.Transactions.ReceiptNoteTraInfo> ModelReceiptNoteTra, long Receipt_Note_Id = 0)
        {
            System.Data.DataTable tbl = new System.Data.DataTable("Product");
            System.Data.DataColumn[] columns = new System.Data.DataColumn[7];
            columns[0] = new System.Data.DataColumn("Item_Id", typeof(long));
            columns[1] = new System.Data.DataColumn("Pur_Tra_Id", typeof(long));
            columns[2] = new System.Data.DataColumn("Receipt_Note_Id", typeof(long));
            columns[3] = new System.Data.DataColumn("Order_Quantity", typeof(decimal));
            columns[4] = new System.Data.DataColumn("Received_Quantity", typeof(decimal));
            columns[5] = new System.Data.DataColumn("Rate", typeof(decimal));
            columns[6] = new System.Data.DataColumn("Remarks", typeof(string));
            tbl.Columns.AddRange(columns);
            foreach (var item in ModelReceiptNoteTra)
            {
                var row = tbl.NewRow();

                row["Item_Id"] = item.Item_Id;
                row["Pur_Tra_Id"] = item.Pur_Tra_Id;
                row["Receipt_Note_Id"] = Receipt_Note_Id;

                row["Order_Quantity"] = 1;
                row["Received_Quantity"] = 1;
                row["Rate"] = item.Rate;
                row["Remarks"] = item.Remarks;
                tbl.Rows.Add(row);
            }
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
            var regularConnectionString = builder.ProviderConnectionString;
            using (SqlConnection connection = new SqlConnection(regularConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("", connection);
                command.CommandText = "Pr_UpdateReceiptNote";
                command.Parameters.Clear();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                command.Parameters.AddWithValue("@Receipt_Note_Id", Receipt_Note_Id).SqlDbType = SqlDbType.NChar;
                command.ExecuteNonQuery();
                connection.Close();
                bool result = true;

                return result;
            };
        }
        public List<ViewModel.Common.DDLBind> DDLBind(long SuuplierId = 0,long Id=0)
        {
            var objtbllist = _db.Purchase_Order.Where(x => x.Company_Id == 1 && x.Is_Deleted == false).ToList();
            if (SuuplierId != 0)
            {
                objtbllist = objtbllist.Where(x => x.Supplier_Id == SuuplierId && x.Status == false).ToList();
            }

            return objtbllist.Select(x => new ViewModel.Common.DDLBind
            {
                Id = x.Id,
                Name = x.Pur_Order_VC_No
            }).ToList();
         
        }
        public ReceiptNoteInfo Find(int Id)
        {
            Receipt_Note objtblReceiptNote = new Receipt_Note();
            objtblReceiptNote = Id > 0 ? _db.Receipt_Note.Find(Id) : Id <= 0 ? _db.Receipt_Note.FirstOrDefault() : null;
            ReceiptNoteInfo modelReceiptNote = new ReceiptNoteInfo();

            if (objtblReceiptNote != null)
            {
               // Receipt_Note objtblReceiptNote = _db.Receipt_Note.Find(Id);
                modelReceiptNote.Id = objtblReceiptNote.Id;
                modelReceiptNote.Supplier_Id = objtblReceiptNote.Supplier_Id;
                modelReceiptNote.Remarks = objtblReceiptNote.Remarks;
                modelReceiptNote.Receipt_Date = objtblReceiptNote.Receipt_Date.ToShortDateString();
                modelReceiptNote.Purchase_Order_Id = objtblReceiptNote.Purchase_Order_Id;
                modelReceiptNote.Purchase_Order_No = objtblReceiptNote.Purchase_Order_No;
                modelReceiptNote.Receipt_Note_No = objtblReceiptNote.Receipt_Note_No;
                modelReceiptNote.Amount = objtblReceiptNote.Amount;

                if (objtblReceiptNote.Supplier_Id != 0)
                {
                    var tblsupplier = _db.Suppliers.Find(objtblReceiptNote.Supplier_Id);
                    if (tblsupplier != null)
                    {
                        modelReceiptNote.Email = tblsupplier.Email_Id;
                        modelReceiptNote.Supplier_Name = tblsupplier.Name;
                        modelReceiptNote.Contact_No = tblsupplier.Contact_No;
                    }
                }

            }
            return modelReceiptNote;
        }
        public String GEN_ReceiptNo()
        {
            string Challan_No = "";

            int countRows = _db.Receipt_Note.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Receipt_Note.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Receipt_Note_No;
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
                if (Id != 0)
                {
                    Receipt_Note objtblReceiptNote = _db.Receipt_Note.Find(Id);
                    objtblReceiptNote.Modified_By = 1;
                    objtblReceiptNote.Modified_Date = DateTime.Now;
                    objtblReceiptNote.Is_Deleted = true;
                    _db.SaveChanges();
                    result = true;
                }
            }
            catch
            {

                result = false;
            }


            return result;
        }

        public List<ReceiptNoteTraInfo> ReceiptNoteDetails(int Id, int OrderId)
        {
            List<ReceiptNoteTraInfo> lstmodel = new List<ReceiptNoteTraInfo>();
            Common.CommonDbLayer objcommon = new Common.CommonDbLayer();

            long orderNo = 0;
            if (Id != 0)
            {
                var objRedceiptNote = _db.Receipt_Note.Find(Id);
                orderNo = objRedceiptNote.Id;
                var finlst = _db.Pr_Receipt_Note_Tra("EDIT", orderNo, 1).ToList();
                finlst.ForEach(x =>
                  lstmodel.Add(new ReceiptNoteTraInfo
                  {
                      Id = x.Purchase_Tra_Id,
                      Pur_Tra_Id = x.PurTra_Id,
                      Purchase_SerialNo = x.Purchase_Serial_No,
                      Sales_Serial_No = x.Sales_Serial_No,
                      Receipt_No = "",
                      Item_Id = x.Item_Id,
                      Item_Name = x.Item_Name,
                      Order_Quantity = x.Oreder_Quantity,
                      Rate = x.Rate,
                      Remarks = x.Remarks,
                      Received_Quantity = x.Oreder_Quantity,
                      Amount = (x.Oreder_Quantity * x.Rate),
                      Unit_Name = x.Unit_Name

                  }));


            }
            if (OrderId != 0)
            {
                var objpurorder = _db.Purchase_Order.Find(OrderId);
               // orderNo = objsalesorder.Id;
                var finlst = _db.Pr_Receipt_Note_Tra("CREATE", objpurorder.Id, 1).ToList();
                finlst.ForEach(x =>
                  lstmodel.Add(new ReceiptNoteTraInfo
                  {
                      Id = x.Purchase_Tra_Id,
                      Pur_Tra_Id = x.PurTra_Id,
                      Purchase_SerialNo = x.Purchase_Serial_No,
                      Sales_Serial_No = x.Sales_Serial_No,
                      Receipt_No = "",
                      Item_Id = x.Item_Id,
                      Item_Name = x.Item_Name,
                      Order_Quantity = x.Oreder_Quantity,
                      Rate = x.Rate,
                      Remarks = x.Remarks,
                      Received_Quantity = x.Oreder_Quantity,
                      Amount = (x.Oreder_Quantity * x.Rate),
                      Unit_Name = x.Unit_Name

                  }));
            }


            return lstmodel;

        }
        public bool Save(Receipt_Note tblReceiptNote)
        {
            bool result = false;
            try
            {
                bool res = true;
                if (res == true)
                {
                    if (tblReceiptNote.Id == 0)
                    {
                        tblReceiptNote.Id = _db.Receipt_Note.Count() != 0 ? _db.Receipt_Note.Max(x => x.Id) + 1 : 1;
                        
                        _db.Receipt_Note.Add(tblReceiptNote);
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
