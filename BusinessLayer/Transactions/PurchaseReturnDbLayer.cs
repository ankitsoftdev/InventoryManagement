using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using ViewModel.Transactions;

using System.Data.Entity.Core.Objects;
namespace DataLayer.Transactions
{
   public class PurchaseReturnDbLayer
    {
       INVENTORY_DBEntities _db;
       public PurchaseReturnDbLayer()
       {
           _db = new INVENTORY_DBEntities();
       }
       public PurchaseReturnDbLayer(string ConnectionString)
       {
            _db = new INVENTORY_DBEntities();
             _db.Database.Connection.ConnectionString = ConnectionString;
       }
       public List<PurchaseReturnList> List(string search="")
       {
           List<PurchaseReturnList> list = new List<PurchaseReturnList>();
      
           var lst = _db.Purchase_Return.OrderByDescending(x => x.DATE).ToList();
           lst.ForEach(x => list.Add(new PurchaseReturnList
           {
               Id=x.Id,Return_Date=x.DATE.ToShortDateString(),Remarks=x.Remarks,VC_No=x.PurRet_VC_No
           }));
           return list;
       }
       public bool Create(PurchaseReturnInfo ModelPurRet)
       {
           bool result = false;
           //using (TransactionScope ts = new TransactionScope())
           //{

               try
               {
                   if(ModelPurRet!=null && ModelPurRet.Id==0)
                   {
                       Purchase_Return tblPurRet = new Purchase_Return();
                       tblPurRet.DATE = ModelPurRet.Return_Date;
                       tblPurRet.Discount_Amount = ModelPurRet.Discount;
                       tblPurRet.Supplier_Id = ModelPurRet.Supplier_Id;
                       tblPurRet.Created_Date = DateTime.Now;
                       tblPurRet.Created_By = 1;
                       tblPurRet.Modified_By = 1;
                       tblPurRet.Modified_Date = DateTime.Now;
                       tblPurRet.Purchase_Id = ModelPurRet.Purchase_Id;
                       tblPurRet.Remarks = ModelPurRet.Remarks;
                       tblPurRet.PurRet_VC_No = ModelPurRet.VC_No;
                       tblPurRet.Total_Amount = ModelPurRet.Amount;

                       var r= Save(tblPurRet);
                       if(r>0)
                       {
                           CreatePurchaseRetTra(ModelPurRet.ReturnDetails, tblPurRet.Id,"CREATE");
                       }

                   }
               }
               catch
               {

                   result = false;
               }
           //    ts.Complete();
           //    ts.Dispose();
           //}
           return result;
       }
       public PurchaseReturnInfo Find(long Id)
       {
           PurchaseReturnInfo modelPurRet = new PurchaseReturnInfo();
           if(Id!=0)
           {
               Purchase_Return tblPurRet = _db.Purchase_Return.Find(Id);
               modelPurRet.Id = tblPurRet.Id;
               modelPurRet.Purchase_Id = tblPurRet.Purchase_Id;
               modelPurRet.Remarks = tblPurRet.Remarks;
               modelPurRet.Return_Date = tblPurRet.DATE;
               modelPurRet.Supplier_Id = tblPurRet.Supplier_Id;
               modelPurRet.VC_No = tblPurRet.PurRet_VC_No;
               modelPurRet.Amount = tblPurRet.Total_Amount;
               modelPurRet.Discount = tblPurRet.Discount_Amount??0.00m;

           }
           return modelPurRet;
       }
       public bool Update(PurchaseReturnInfo ModelPurRet)
       {
           bool result = false;
           //using (TransactionScope ts = new TransactionScope())
           //{

               try
               {
                   if (ModelPurRet != null && ModelPurRet.Id != 0)
                   {
                       Purchase_Return tblPurRet = _db.Purchase_Return.Find(ModelPurRet.Id);
                       tblPurRet.DATE = ModelPurRet.Return_Date;
                       tblPurRet.Discount_Amount = ModelPurRet.Discount;
                       tblPurRet.Supplier_Id = ModelPurRet.Supplier_Id;
                       tblPurRet.Modified_By = 1;
                       tblPurRet.Modified_Date = DateTime.Now;
                       tblPurRet.Purchase_Id = ModelPurRet.Purchase_Id;
                       tblPurRet.Remarks = ModelPurRet.Remarks;
                       tblPurRet.Total_Amount = ModelPurRet.Amount;

                       Save(tblPurRet);
                       var r = Save(tblPurRet);
                       if (r > 0)
                       {
                           CreatePurchaseRetTra(ModelPurRet.ReturnDetails, tblPurRet.Id, "UPDATE");
                       }
                   }
               }
               catch
               {

                   result = false;
               }
           //    ts.Complete();
           //    ts.Dispose();
           //}
           return result;
       }
       public List<PurchaseReturnInfo_Tra> ReturnDetails(long Id)
       {
           List<PurchaseReturnInfo_Tra> list = new List<PurchaseReturnInfo_Tra>();
           var lst = _db.Purchase_Return_Tra.Where(x => x.PurRet_Id == Id).ToList();
           var fnl = (from l in lst
                     join i in _db.Stock_Item on l.Item_Id equals i.Id
                     join u in _db.UnitMasters on i.Unit_Id equals u.Id
                     select new
                     {
                         Id = l.Id,
                         Item_Id = l.Item_Id,

                         PurRet_Id = l.PurRet_Id,
                         Quantity = l.Qty,
                         Rate = l.Rate,
                         Sale_Serial_No = l.Sale_Serial_No,
                         Unit_Name = u.Name,
                         No_of_Decimal = u.No_of_Decimal ?? 0
                     }).ToList();
           fnl.ForEach(x => list.Add(new PurchaseReturnInfo_Tra
           {
               Id=x.Id,
               Item_Id=x.Item_Id,
               PurRet_Id=x.PurRet_Id,
               Quantity=x.Quantity,
               Rate=x.Rate,
               Amount=x.Quantity*x.Rate,
               No_of_Decimal=x.No_of_Decimal,
               Unit_Name=x.Unit_Name,
              Sale_Serial_No=x.Sale_Serial_No
           }));
           return list;
       }
       public bool Delete(long Id)
       {
           bool result = false;
           try
           {
               if(Id>0)
               {
                   Purchase_Return tblpurRet = _db.Purchase_Return.Find(Id);
                   tblpurRet.Is_Deleted = true;
                   tblpurRet.Modified_By = 1;
                   tblpurRet.Modified_Date = DateTime.Now;
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
       public bool CheckSerialNo(string SerialNo)
       {
           bool result = false;
           try
           {
               result = _db.Purchase_Master_Tra.Any(x => x.Purchase_Serial_No == SerialNo && x.Is_Deleted==false);
           }
           catch 
           {

               result = false;
           }
           return result;
       }
       public String GEN_VC_No()
       {
           string VC_No = "";

           int countRows = _db.Purchase_Return.Count();
           if (countRows != 0)
               VC_No = _db.Purchase_Return.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().PurRet_VC_No;
           if (!string.IsNullOrWhiteSpace(VC_No))
           {
               VC_No = Regex.Replace(VC_No, @"\d+(?=\D*$)",
                  m => (Convert.ToInt64(m.Value) + 1).ToString().PadLeft(5, '0'));


           }
           else
           {
               VC_No = "1".PadLeft(5, '0');
           }


           return VC_No;
       }
       public bool CreatePurchaseRetTra(List<PurchaseReturnInfo_Tra> ModelPurRetTra, long PurRet_Id,string Tag)
       {
           bool result = false;
           try
           {
               // List<Sales_Return_Tra> TblPurMasterTra = new List<Sales_Return_Tra>();
               Sales_Return_Tra tbltra;

               if (ModelPurRetTra != null)
               {

                   System.Data.DataTable tbl = new System.Data.DataTable("Product");
                   System.Data.DataColumn[] columns = new System.Data.DataColumn[6];
                   columns[0] = new System.Data.DataColumn("Id", typeof(long));
                   columns[1] = new System.Data.DataColumn("SalesRet_Id", typeof(long));
                   columns[2] = new System.Data.DataColumn("Item_Id", typeof(long));
                   columns[3] = new System.Data.DataColumn("Qty", typeof(decimal));
                   columns[4] = new System.Data.DataColumn("Rate", typeof(decimal));
                   columns[5] = new System.Data.DataColumn("Sale_Serial_No", typeof(string));

                   tbl.Columns.AddRange(columns);

                   foreach (var item in ModelPurRetTra)
                   {
                       var row = tbl.NewRow();
                       row["Id"] = item.Id;
                       row["SalesRet_Id"] = PurRet_Id;
                       row["Item_Id"] = item.Item_Id;
                       row["Qty"] = item.Quantity;
                       row["Rate"] = item.Rate;
                       row["Sale_Serial_No"] = "abc";
                       tbl.Rows.Add(row);
                   }




                   var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
                   var regularConnectionString = builder.ProviderConnectionString;
                   using (SqlConnection connection = new SqlConnection(regularConnectionString))
                   {
                       connection.Open();

                       SqlCommand command = new SqlCommand("", connection);

                       command.CommandText = "Pr_PurchaseReturnTra";
                       command.Parameters.Clear();
                       command.CommandType = CommandType.StoredProcedure;
                       // command.Parameters.AddWithValue("@someIdForParameter", someId);
                       command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                       command.Parameters.AddWithValue("@PurRet_Id", PurRet_Id).SqlDbType = SqlDbType.BigInt;
                       command.Parameters.AddWithValue("@Tag", Tag).SqlDbType = SqlDbType.NVarChar;
                       command.ExecuteNonQuery();

                       connection.Close();

                       //  bool result = false;

                       return result;
                   };
                   result = true;

               }
           }
           catch
           {

               result = false;
           }
           return result;
       }
       private long Save(Purchase_Return TblPurRet)
       {
           bool result = false;
           try
           {
               //bool res = _db.Purchase_Return.Any(m => m.Company_Id == 1 && (0 == TblPurMaster.Id || m.Id != TblPurMaster.Id) && (m.Pur_VC_No.ToUpper().Trim().Equals(TblPurMaster.Pur_VC_No.ToUpper().Trim())));
               if (result == false)
               {
                   if (TblPurRet.Id == 0)
                   {
                       TblPurRet.Id = _db.Purchase_Return.Count() > 0 ? _db.Purchase_Return.Max(x => x.Id) + 1 : 1;
                       _db.Purchase_Return.Add(TblPurRet);
                   }
                   _db.SaveChanges();
                   result = true;

               }
           }
           catch
           {

               return 0;
           }

           return result == true ? TblPurRet.Id : 0;
       }

       public List<ViewModel.Common.DDLBind> GetBillList(long Supplier_Id)
       {
           return _db.Purchase_Master.Where(x => x.Supplier_Id == Supplier_Id && x.Is_Deleted==false).Select(x => new ViewModel.Common.DDLBind
           {
              Id=x.Id, Name=x.Pur_VC_No
           }).ToList();
       }
    }
}
