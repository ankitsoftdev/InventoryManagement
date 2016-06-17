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

namespace DataLayer.Transactions
{
  public  class SalesReturnDbLayer
    {
      INVENTORY_DBEntities _db;
      public SalesReturnDbLayer()
        {
            _db = new INVENTORY_DBEntities();
        }
      public List<SalesReturnList> List(string search="")
      {
          List<SalesReturnList> list = new List<SalesReturnList>();
          var ls = _db.Sales_Return.OrderByDescending(x=>x.DATE).ToList();
          ls.ForEach(x => list.Add(new SalesReturnList
          {
              Id = x.Id,
              Return_Date = x.DATE.ToShortDateString(),
              VC_No = x.SalesRet_VC_No,
              Remarks = x.Remarks,
              Customer_Id = x.Customer_Id,

          }));
          return list;
      }
      public SalesReturnDbLayer(string ConnectionString)
      {
          _db = new INVENTORY_DBEntities();
          _db.Database.Connection.ConnectionString = ConnectionString;
        

      }

      public bool Create(SalesReturnInfo ModelSales)
      {

          bool result = false;
          //using (TransactionScope ts = new TransactionScope())
          //{


              try
              {
                  Sales_Return TblSalesReturn = new Sales_Return();
                  TblSalesReturn.Sales_Id = ModelSales.Sales_Id;
                  TblSalesReturn.Customer_Id = ModelSales.Customer_Id;
                  TblSalesReturn.DATE = ModelSales.Return_Date;
                  TblSalesReturn.Discount_Amount = ModelSales.Discount;
                  TblSalesReturn.Fncl_Year_Id = 1;
                  TblSalesReturn.Is_Active = true;
                  TblSalesReturn.Is_Deleted = false;
                  TblSalesReturn.Modified_By = 1;
                  TblSalesReturn.Modified_Date = DateTime.Now;
                  TblSalesReturn.Created_By = 1;
                  TblSalesReturn.Created_Date = DateTime.Now;
                  TblSalesReturn.SalesRet_VC_No = ModelSales.VC_No;
                  TblSalesReturn.Total_Amount=ModelSales.Amount;
                  TblSalesReturn.Remarks = ModelSales.Remarks;
                  
               var res=   Save(TblSalesReturn);
               if (res > 0)
                  {
                      result = CreateSalesRetTra(ModelSales.ReturnDetails , TblSalesReturn.Id,"CREATE");
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
      public bool Update(SalesReturnInfo ModelSales)
      {

          bool result = false;
          //using (TransactionScope ts = new TransactionScope())
          //{


              try
              {
                  Sales_Return TblSalesReturn = _db.Sales_Return.Find(ModelSales.Id);
                  TblSalesReturn.Sales_Id = ModelSales.Sales_Id;
                  TblSalesReturn.Customer_Id = ModelSales.Customer_Id;
                  TblSalesReturn.DATE = ModelSales.Return_Date;
                  TblSalesReturn.Discount_Amount = ModelSales.Discount;
                  TblSalesReturn.Modified_By = 1;
                  TblSalesReturn.Modified_Date = DateTime.Now;
                  TblSalesReturn.SalesRet_VC_No = ModelSales.VC_No;
                  TblSalesReturn.Total_Amount = ModelSales.Amount;
                  TblSalesReturn.Remarks = ModelSales.Remarks;

                  var res = Save(TblSalesReturn);
                  if (res > 0)
                  {
                      result = CreateSalesRetTra(ModelSales.ReturnDetails, TblSalesReturn.Id,"UPDATE");
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
      public List<SalesReturnInfo_Tra> ReturnDetais(long Id)
      {
         List<SalesReturnInfo_Tra> lst = new List<SalesReturnInfo_Tra>();
         if (Id > 0)
         {
            
          var list = _db.Sales_Return_Tra.Where(x => x.SalesRet_Id == Id).ToList();
          var fnl = (from l in list
                     join i in _db.Stock_Item on l.Item_Id equals i.Id
                     join u in _db.UnitMasters on i.Unit_Id equals u.Id
                     select new
                     {
                         Id = l.Id,
                         Item_Id = l.Item_Id,

                         SalesRet_Id = l.SalesRet_Id,
                         Quantity = l.Qty,
                         Rate = l.Rate,
                         Sale_Serial_No = l.Sale_Serial_No,
                         Unit_Name = u.Name,
                         No_of_Decimal = u.No_of_Decimal ?? 0
                     }).ToList();
      
              fnl.ForEach(x => lst.Add(new SalesReturnInfo_Tra
              {
                  Id = x.Id,
                  Item_Id = x.Item_Id,
                  Quantity = x.Quantity,
                  Rate = x.Rate,
                  Sale_Serial_No = x.Sale_Serial_No,
                  SalesRet_Id = x.SalesRet_Id,
                  Total_Amount=x.Rate*x.Quantity,
                  No_of_Decimal=x.No_of_Decimal,
                  Unit_Name=x.Unit_Name,
                  
              }));
          }
          return lst;
      }
      public SalesReturnInfo Find(long Id)
      {
          
          try
          {
              SalesReturnInfo modelsalesRet = new SalesReturnInfo();
              Sales_Return tblSalesRet = _db.Sales_Return.Find(Id);
              modelsalesRet.Id = tblSalesRet.Id;
              modelsalesRet.Is_RejectionNote = false;
              modelsalesRet.Discount = tblSalesRet.Discount_Amount??0.0m;
              modelsalesRet.Amount = tblSalesRet.Total_Amount;
              modelsalesRet.Customer_Id = tblSalesRet.Customer_Id;
              modelsalesRet.Remarks = tblSalesRet.Remarks;
              modelsalesRet.Return_Date = tblSalesRet.DATE;
              modelsalesRet.Sales_Id = tblSalesRet.Sales_Id;
              modelsalesRet.VC_No = tblSalesRet.SalesRet_VC_No;
              
              return modelsalesRet;
          }
          catch 
          {

              return null;
          }
      }
      public String GEN_VC_No()
      {
          string VC_No = "";

          int countRows = _db.Sales_Return.Count();
          if (countRows != 0)
              VC_No = _db.Sales_Return.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().SalesRet_VC_No;
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
      public bool Delete(long Id)
      {
          bool result = false;
          try
          {
              if (Id > 0)
              {
                  Sales_Return tblSaleRet = _db.Sales_Return.Find(Id);
                  tblSaleRet.Is_Deleted = true;
                  tblSaleRet.Modified_By = 1;
                  tblSaleRet.Modified_Date = DateTime.Now;
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
      private long Save(Sales_Return TblSalesReturn)
      {
          bool result = false;
          try
          {

              if (result == false)
              {
                  if (TblSalesReturn.Id == 0)
                  {
                      TblSalesReturn.Id = _db.Sales_Return.Count() > 0 ? _db.Sales_Return.Max(x => x.Id) + 1 : 1;
                      _db.Sales_Return.Add(TblSalesReturn);
                  }
                  _db.SaveChanges();
                  result = true;

              }
          }
          catch
          {

              return 0;
          }

          return result == true ? TblSalesReturn.Id : 0;
      }


      public bool CreateSalesRetTra(List<SalesReturnInfo_Tra> ModelSalesRetTra, long SalesRet_Id,string Tag)
      {
          bool result = false;
          try
          {
             // List<Sales_Return_Tra> TblPurMasterTra = new List<Sales_Return_Tra>();
              Sales_Return_Tra tbltra;

              if (ModelSalesRetTra != null)
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

                  foreach (var item in ModelSalesRetTra)
                  {
                      var row = tbl.NewRow();
                      row["Id"] = item.Id;
                      row["SalesRet_Id"] = SalesRet_Id;
                      row["Item_Id"] = item.Item_Id;
                      row["Qty"] = item.Quantity;
                      row["Rate"]=item.Rate;
                      row["Sale_Serial_No"] = "abc";
                      tbl.Rows.Add(row);
                  }




                  var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
                  var regularConnectionString = builder.ProviderConnectionString;
                  using (SqlConnection connection = new SqlConnection(regularConnectionString))
                  {
                      connection.Open();

                      SqlCommand command = new SqlCommand("", connection);

                      command.CommandText = "Pr_SalesReturnTra";
                      command.Parameters.Clear();
                      command.CommandType = CommandType.StoredProcedure;
                      // command.Parameters.AddWithValue("@someIdForParameter", someId);
                      command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                      command.Parameters.AddWithValue("@SalesRet_Id", SalesRet_Id).SqlDbType = SqlDbType.BigInt;
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
    }

    
}
