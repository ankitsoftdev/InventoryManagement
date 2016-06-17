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
   public class DeliveryNoteDbLayer
    {
       INVENTORY_DBEntities _db;
       public DeliveryNoteDbLayer()
       {
           _db = new INVENTORY_DBEntities();
         
       }
       public DeliveryNoteDbLayer(String ConnectionString)
       {
           _db = new INVENTORY_DBEntities();
           _db.Database.Connection.ConnectionString = ConnectionString;
       }
       public List<ViewModel.Transactions.DeliveryNoteList> List()
       {
           List<ViewModel.Transactions.DeliveryNoteList> objlstmodelDelivery = new List<DeliveryNoteList>();
           var lstDelivery = _db.Pr_DeliveryNoteList(1).ToList();
           lstDelivery.ForEach((x) => objlstmodelDelivery.Add(new DeliveryNoteList { Id = x.Id,Delivery_Date=x.Delivery_Date.ToShortDateString(),Customer_Name=x.Customer_Name, Order_Date = x.Order_Date.ToShortDateString(), Remarks = x.Remarks, Sales_Order_Id = x.Sales_Order_Id, Sales_Order_No = x.Sales_Order_No, Email_Id = x.Email_Id, Delivery_Note_No = x.Delivery_Note_No, Contact_No = x.Contact_No }));
           return objlstmodelDelivery;
       }
       public bool Create(DeliveryNoteInfo modelDeliveryNote)
       {
           bool result = false;
          
           if (modelDeliveryNote != null)
           {
               Delivery_Note objtblDeliveryNote = new Delivery_Note();

               objtblDeliveryNote.Amount = modelDeliveryNote.Amount;
               objtblDeliveryNote.Company_Id = 1;
               
               objtblDeliveryNote.Delivery_Note_No = modelDeliveryNote.Delivery_Note_No;
               objtblDeliveryNote.Customer_Id = modelDeliveryNote.Customer_Id;
               objtblDeliveryNote.Sales_Order_Id = modelDeliveryNote.Sales_Order_Id;
            //   objtblDeliveryNote.Sales_Order_No = modelDeliveryNote.Sales_Order_No;
               objtblDeliveryNote.Delivery_Date = Convert.ToDateTime(modelDeliveryNote.Delivery_Date);
               objtblDeliveryNote.Remarks = modelDeliveryNote.Remarks;
               objtblDeliveryNote.Is_Active = true;
               objtblDeliveryNote.Is_Deleted = false;
               objtblDeliveryNote.Created_By = 1;
               objtblDeliveryNote.Created_Date = DateTime.Now;
               objtblDeliveryNote.Modified_By = 1;
               objtblDeliveryNote.Modified_Date = DateTime.Now;
               

               result = Save(objtblDeliveryNote);
               

               if (result == true)
               {

                   Update_DeliveryNote_Tra(modelDeliveryNote.DeliveryDetails.Where(x => x.Status == true).ToList(), objtblDeliveryNote.Id);
                  
               }

           }
           return result;
       }
       public bool Update(DeliveryNoteInfo modelDeliveryNote)
       {
           bool result = false;

           if (modelDeliveryNote != null)
           {
               Delivery_Note objtblDeliveryNote = _db.Delivery_Note.Find(modelDeliveryNote.Id);

               objtblDeliveryNote.Amount = modelDeliveryNote.Amount;
              
              
               objtblDeliveryNote.Created_Date = DateTime.Now;
               objtblDeliveryNote.Delivery_Note_No = modelDeliveryNote.Delivery_Note_No;
            
               objtblDeliveryNote.Sales_Order_Id = modelDeliveryNote.Sales_Order_Id;
             //  objtblDeliveryNote.Sales_Order_No = modelDeliveryNote.Sales_Order_No;
               objtblDeliveryNote.Remarks = modelDeliveryNote.Remarks;
               result = Save(objtblDeliveryNote);


               if (result == true)
               {

                   Update_DeliveryNote_Tra(modelDeliveryNote.DeliveryDetails.Where(x => x.Status == true).ToList(), objtblDeliveryNote.Id);

               }

           }
           return result;
       }
       public DeliveryNoteInfo Find(long Id)
       {
           Delivery_Note objtblDeliveryNote = new Delivery_Note();
           objtblDeliveryNote = Id > 0 ? _db.Delivery_Note.Find(Id) : Id <= 0 ? _db.Delivery_Note.FirstOrDefault() : null;
           DeliveryNoteInfo modelDeliveryNote = new DeliveryNoteInfo();
           if (Id >= 0 && objtblDeliveryNote!=null)
           {
              // Delivery_Note objtblDeliveryNote = _db.Delivery_Note.Find(Id);
               modelDeliveryNote.Id = objtblDeliveryNote.Id;
               modelDeliveryNote.Customer_Id = objtblDeliveryNote.Customer_Id;
               modelDeliveryNote.Remarks = objtblDeliveryNote.Remarks;
               modelDeliveryNote.Delivery_Date = objtblDeliveryNote.Delivery_Date.ToShortDateString();
               modelDeliveryNote.Sales_Order_Id = objtblDeliveryNote.Sales_Order_Id;
               //modelDeliveryNote.Sales_Order_No = objtblDeliveryNote.Sales_Order_Id;
               modelDeliveryNote.Delivery_Note_No = objtblDeliveryNote.Delivery_Note_No;
               modelDeliveryNote.Amount = objtblDeliveryNote.Amount;

               if (objtblDeliveryNote.Customer_Id != 0)
               {
                   var tblCustomer = _db.Customers.Find(objtblDeliveryNote.Customer_Id);
                   if (tblCustomer != null)
                   {
                       modelDeliveryNote.Email = tblCustomer.Email_Id;
                       modelDeliveryNote.Name = tblCustomer.Name;
                       modelDeliveryNote.Contact_No = tblCustomer.Contact_No;
                   }
               }

           }
           return modelDeliveryNote;
       }
       public List<ViewModel.Common.DDLBind> DDLBind(long CustomerId=0,long Id=0)
       {
           var objtbllist = _db.Sales_Order.Where(x => x.Company_Id == 1 && x.Is_Deleted == false).ToList();
           bool Status = false;
           Status = Id != 0 ? true : false;
           if(CustomerId!=0)
           {

               objtbllist = objtbllist.Where(x => x.Customer_Id == CustomerId ).ToList();
           }
           var salesOrderNoList = objtbllist.Select(x => new ViewModel.Common.DDLBind
           { Id=x.Id, Name=x.Sales_Order_VC_No
           }).ToList();
           return salesOrderNoList;
       }
       public List<ViewModel.Transactions.DeliveryNoteInfo_Tra> DeliveryDetails(long  Debit_No)
       {
           List<ViewModel.Transactions.DeliveryNoteInfo_Tra> modellist = new List<DeliveryNoteInfo_Tra>();
           Common.CommonDbLayer objcommon = new Common.CommonDbLayer();
           var lst = _db.Delivery_Note_Tra.Where(x => x.Delivery_Note_Id == Debit_No).ToList();
           var finlst = (from l in lst
                         join I in _db.Stock_Item on l.Item_Id equals I.Id
                         join U in _db.UnitMasters on I.Unit_Id equals U.Id
                         select new { Id = l.Id, Debit_No = l.Delivery_Note_Id,  Amount = l.Amount, Item_Id = l.Item_Id, Item_Name = I.Name, Order_Quantity = l.Order_Quantity, Delivered_Quantity = l.Delivered_Quantity, Rate = l.Rate, Unit_Name = U.Name, Unit_Id = U.Id }).ToList();
           finlst.ForEach(x =>
               modellist.Add(new DeliveryNoteInfo_Tra
               {
                 Id = x.Id,
                 Debit_No=x.Debit_No.ToString(),
                 Delivered_Quantity=x.Delivered_Quantity,
                 Amount=x.Amount,
               
                 Item_Id=x.Item_Id,

               }));



           return modellist;
       }
       public bool Save(Delivery_Note tblDeliveryNote)
       {
           bool result = false;
           try
           {
               bool res = true;
               if (res == true)
               {
                   if (tblDeliveryNote.Id == 0)
                   {
                       tblDeliveryNote.Id = _db.Delivery_Note.Count() != 0 ? _db.Delivery_Note.Max(x => x.Id) + 1 : 1;
                       _db.Delivery_Note.Add(tblDeliveryNote);
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
       private bool Update_DeliveryNote_Tra(List<ViewModel.Transactions.DeliveryNoteInfo_Tra> ModelDeliveryNoteTra, long Delivery_Note_Id= 0)
       {
           System.Data.DataTable tbl = new System.Data.DataTable("Product");
           System.Data.DataColumn[] columns = new System.Data.DataColumn[6];
           columns[0] = new System.Data.DataColumn("Item_Id", typeof(long));
           columns[1] = new System.Data.DataColumn("Pur_Tra_Id", typeof(long));
           columns[2] = new System.Data.DataColumn("Delivery_Note_Id", typeof(long));
           columns[3] = new System.Data.DataColumn("Order_Quantity", typeof(decimal));
           columns[4] = new System.Data.DataColumn("Delivered_Quantity", typeof(decimal));
           columns[5] = new System.Data.DataColumn("Rate", typeof(decimal));
           
           tbl.Columns.AddRange(columns);
           foreach (var item in ModelDeliveryNoteTra)
           {
               var row = tbl.NewRow();

               row["Item_Id"] = item.Item_Id;
               row["Pur_Tra_Id"] = item.Pur_Tra_Id;
               row["Delivery_Note_Id"] = Delivery_Note_Id;
              
               row["Order_Quantity"] = 1;
               row["Delivered_Quantity"] = 1;
               row["Rate"] = item.Rate;
              
               tbl.Rows.Add(row);
           }
           var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
           var regularConnectionString = builder.ProviderConnectionString;
           using (SqlConnection connection = new SqlConnection(regularConnectionString))
           {
               connection.Open();
               SqlCommand command = new SqlCommand("", connection);
               command.CommandText = "Pr_UpdateDeliveryNote";
               command.Parameters.Clear();
               command.CommandType = CommandType.StoredProcedure;
               command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
               command.Parameters.AddWithValue("@Delivery_Note_Id", Delivery_Note_Id).SqlDbType = SqlDbType.NChar;
               command.ExecuteNonQuery();
               connection.Close();
               bool result = true;

               return result;
           };
       }
       public String GEN_DebitNo()
       {
           string Challan_No = "";

           int countRows = _db.Delivery_Note.Where(x => x.Company_Id == 1).Count();
           if (countRows != 0)
               Challan_No = _db.Delivery_Note.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Delivery_Note_No;
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
                   Delivery_Note objtblDeliveryNote = _db.Delivery_Note.Find(Id);
                   objtblDeliveryNote.Modified_By = 1;
                   objtblDeliveryNote.Modified_Date = DateTime.Now;
                   objtblDeliveryNote.Is_Deleted = true;
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

       public List<DeliveryNoteInfo_Tra> DeliveryNoteDetails(int Id, int OrderId)
       {
           List<DeliveryNoteInfo_Tra> lstmodel = new List<DeliveryNoteInfo_Tra>();
           Common.CommonDbLayer objcommon = new Common.CommonDbLayer();
          
           string orderNo = "";
           if(Id!=0)
           {
               var objdeliveryNote = _db.Delivery_Note.Find(Id);
             //  orderNo = objdeliveryNote.Delivery_Note_No;
               var finlst = _db.Pr_Delivery_Note_Tra("EDIT", objdeliveryNote.Sales_Order_Id, 1).ToList();
               finlst.ForEach(x =>
                 lstmodel.Add(new DeliveryNoteInfo_Tra
                 {
                     Id = x.Sales_Tra_Id,
                     Pur_Tra_Id=x.PurTra_Id,
                     Purchase_SerialNo=x.Purchase_Serial_No,
                     Sales_Serial_No=x.Sales_Serial_No,
                     Debit_No = "",
                     Item_Id = x.Item_Id,
                     Item_Name = x.Item_Name,
                     Order_Quantity = x.Oreder_Quantity,
                     Rate = x.Rate,
                     Remarks = x.Remarks,
                     Delivered_Quantity = x.Oreder_Quantity,
                     Amount = (x.Oreder_Quantity * x.Rate),
                     Unit_Name = x.Unit_Name

                 }));

              
           }
          if( OrderId!=0)
           {
               //var objsalesorder = _db.Sales_Order.Find(OrderId);
             //  orderNo = objsalesorder.Sales_Order_VC_No;
               var finlst = _db.Pr_Delivery_Note_Tra("CREATE", OrderId, 1).ToList();
               finlst.ForEach(x =>
                 lstmodel.Add(new DeliveryNoteInfo_Tra
                 {
                     Id = x.Sales_Tra_Id,
                     Pur_Tra_Id = x.PurTra_Id,
                     Purchase_SerialNo = x.Purchase_Serial_No,
                     Sales_Serial_No = x.Sales_Serial_No,
                     Debit_No = "",
                     Item_Id = x.Item_Id,
                     Item_Name = x.Item_Name,
                     Order_Quantity = x.Oreder_Quantity,
                     Rate = x.Rate,
                     Remarks = x.Remarks,
                     Delivered_Quantity = x.Oreder_Quantity,
                     Amount = (x.Oreder_Quantity * x.Rate),
                     Unit_Name = x.Unit_Name

                 }));
           }

           
           return lstmodel;
          
       }
    }
}
