using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using HDMEntity;
using DataLayer;
using ViewModel.Ledger;

using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Transactions;

namespace DataLayer.Transactions
{
    public class PurchaseDbLayer
    {
        INVENTORY_DBEntities _db;

        public int _companyId { get; set; }
        public PurchaseDbLayer()
        {
            _db = new INVENTORY_DBEntities();
            _companyId = 1;
        }
        public PurchaseDbLayer(string ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }

        public List<PurchaseList> List()
        {

            var lst = _db.Pr_PurchaseList(_companyId).ToList().Select(x => new PurchaseList
            {
                Id = x.Id,
                //Branch_Id = x.Branch_Id,

                Name = x.Name,
                Alias_Name = x.Alias_Name,
                Address = x.Address,
                Challan_No = x.Chalan_No,
                Email_Id = x.Email_Id,
                Purchase_Date = x.DATE.ToShortDateString(),
                Amount = x.Total_Amount,
                Code = x.Code,
                Contact_No = x.Contact_No,
                Discount = x.Discount_Amount,
                Is_Challan_Gen = x.Is_Bill_Gen

            }).ToList();
            return lst;
        }

        public int DeletePurchase(int Id)
        {
            Purchase_Master master = _db.Purchase_Master.Find(Id);
            master.Is_Active = false;
            master.Is_Deleted = true;
            return _db.SaveChanges();
        }
        public List<Challan_Details> ViewDetails(int Id, string Challan_No)
        {
            List<Challan_Details> lst = new List<Challan_Details>();
            if (Id != 0)
            {
                var tblPurmaster = _db.Purchase_Master.Find(Id);
                // Challan_No = tblPurmaster.Pur_VC_No;
            }

            //return _db.Pr_Challan_Details(Challan_No, "PURCHASE").Select(x => new Challan_Details
            //{
            //    Id = x.Id,
            //    ItemId = x.Item_Id,
            //    Supplier_Name = x.Supplier_Name,
            //    Item_Name = x.Item_Name,
            //    Challan_Number = x.Challan_No,
            //    Purchase_Date = x.Purchase_Date.ToShortDateString(),
            //    Qty = x.Qty,
            //    Rate = x.Rate,
            //    Remarks = "",

            //    Total_Amount = x.Qty * x.Rate,
            //    Grand_Total = x.Total_Amount,
            //    Unit_Name = x.Per
            //}).ToList();


            return lst;

        }
        public List<ViewModel.Ledger.Purchase_Tra> ItemDetails(long Pur_Id)
        {


            return (from t in _db.Purchase_Master_Tra
                    join i in _db.Stock_Item on t.Item_Id equals i.Id
                    join g in _db.GoDown_Master on t.GoDown_Id equals g.Id
                    join u in _db.UnitMasters on i.Unit_Id equals u.Id
                    where t.Pur_Id == Pur_Id
                    orderby t.Id ascending
                    select new
                    {
                        t.Id,
                        t.Item_Id,
                        t.GoDown_Id,
                        //  t.Pur_VC_No,
                        t.Qty,
                        t.Rate,
                        t.Tax_Id,
                        t.Purchase_Serial_No,
                        t.Sale_Serial_No,
                        TotalAmount = t.Qty * t.Rate ?? 0.0m,
                        GoDownName = g.Name,
                        ItemName = i.Name,
                        UnitId = u.Id,
                        UnitName = u.Alias_Name
                    }).AsParallel().Select(item => new Purchase_Tra
                    {
                        Id = item.Id,
                        // Challan_Number = item.Pur_VC_No,
                        GoDownId = item.GoDown_Id ?? 0,
                        GoDownName = item.GoDownName,
                        Item_Id = item.Item_Id,
                        ItemName = item.ItemName,
                        SerialNo = item.Sale_Serial_No,
                        ProductCode = item.Purchase_Serial_No,
                        Rate = item.Rate ?? 0,
                        Qty = item.Qty ?? 0,
                        Tax_Id = item.Tax_Id ?? 0,
                        Total_Amount = item.TotalAmount,
                        Unit_Id = item.UnitId,
                        Unit_Name = item.UnitName
                    }).ToList();





        }

        //public List<ViewModel.Common.TaxDetails> GetTaxList(string challanNo)
        //{
        //    return  _db.Purchase_Master_TaxDetails.AsNoTracking().Where(x => x.Purchase_Challan_no == challanNo).Select(x => new ViewModel.Common.TaxDetails
        //    {
        //        TId = x.Id,
        //        Id = _db.Tax_Deduction_Master.Where(p => p.Id == x.Tax_Id && p.Is_Active == true).Select(t => t.Id + "_" + t.Type + "_" + t.Rate_Type + "_" + t.Rate).First().ToString(),
        //        Rate_Type = Convert.ToString(x.Tax_Rate),
        //        Value = Convert.ToString(x.Tax_Amount),

        //    }).ToList();

        //}

        public List<ViewModel.Common.TaxDetails> GetTaxList(long Pur_Id)
        {
            var list = _db.Purchase_Master_TaxDetails.Where(x => x.Pur_Id == Pur_Id).ToList();
            List<ViewModel.Common.TaxDetails> listobj = new List<ViewModel.Common.TaxDetails>();
            list.ForEach((x) => listobj.Add(new ViewModel.Common.TaxDetails
            {
                TId = x.Id,
                Id = _db.Tax_Deduction_Master.Where(p => p.Id == x.Tax_Id && p.Is_Active == true).Select(t => t.Id + "_" + t.Type + "_" + t.Rate_Type + "_" + t.Rate).First().ToString(),
                Rate_Type = Convert.ToString(x.Tax_Rate),
                Value = Convert.ToString(x.Tax_Amount),

            }));
            return listobj;
        }
        private string UnitNameByItemId(int ItemId)
        {
            string name = "";
            if (ItemId != 0)
            {
                var item = _db.Stock_Item.SingleOrDefault(x => x.Id == ItemId);
                name = _db.UnitMasters.SingleOrDefault(x => x.Id == item.Unit_Id).Name ?? "";
            }
            return name;
        }
        public bool Create(ViewModel.Ledger.PurchaseMaster ModelPurMaster)
        {

            bool result = false;
            using (TransactionScope ts = new TransactionScope())
            {


                try
                {
                    Purchase_Master TblPurMaster = new Purchase_Master();

                    TblPurMaster.Fncl_Year_Id = ModelPurMaster.Finance_Id != 0 ? ModelPurMaster.Finance_Id : 1;
                    TblPurMaster.Company_Id = 1;
                    //TblPurMaster.Branch_Id = 1;
                    TblPurMaster.Pur_VC_No = ModelPurMaster.Challan_Number;
                    TblPurMaster.Is_Active = true;
                    TblPurMaster.Is_Deleted = false;
                    TblPurMaster.Modified_By = 1;
                    TblPurMaster.Refered_Id = ModelPurMaster.Quotation_Id != 0 ? ModelPurMaster.Quotation_Id : 0;
                    TblPurMaster.Created_By = 1;
                    TblPurMaster.Created_Date = DateTime.Now;
                    TblPurMaster.Modified_By = 1;
                    TblPurMaster.Modified_Date = DateTime.Now;
                    TblPurMaster.DATE = ModelPurMaster.Purchase_Date;
                    TblPurMaster.Discount_Amount = ModelPurMaster.Discount;
                    TblPurMaster.Tax_Amount = ModelPurMaster.Tax_Amount;
                    TblPurMaster.Remarks = ModelPurMaster.Remarks;
                    TblPurMaster.Supplier_Id = ModelPurMaster.Supplier_Id;
                    TblPurMaster.Total_Amount = ModelPurMaster.purchase_tra.Grand_Total;
                    TblPurMaster.Discount_Amount = ModelPurMaster.Discount != 0 ? ModelPurMaster.Discount : 0.0m;
                    TblPurMaster.Is_Refered = false;
                    TblPurMaster.Is_Bill_Gen = false;
                    var rs = Save(TblPurMaster);
                    if (rs != 0)
                    {
                        result = CreatePurchaseTra(ModelPurMaster.purchase_tra.listpurchase_tra.Where(x => x.Status == true).ToList(), rs);
                    }
                    if (result == true)
                    {
                        result = CreatePurchaseTaxDetail(ModelPurMaster.purchase_tra.taxdetails.listTaxDetails, rs);
                    }
                    if (result == true)
                    {
                        result = CreateStockMaster(ModelPurMaster.purchase_tra.listpurchase_tra.Where(x => x.Status == true).ToList());
                    }
                    if (result == true)
                    {
                        if (ModelPurMaster.orderId > 0)
                        {
                            result = UpdateStatusInPurchaseOrder(ModelPurMaster.orderId);
                        }

                    }
                    result = rs != 0 ? true : false;
                }
                catch
                {

                    result = false;
                }
                ts.Complete();
                ts.Dispose();
            }
            return result;
        }


        private long Save(Purchase_Master TblPurMaster)
        {
            bool result = false;
            try
            {
                bool res = _db.Purchase_Master.Any(m => m.Company_Id == 1 && (0 == TblPurMaster.Id || m.Id != TblPurMaster.Id) && (m.Pur_VC_No.ToUpper().Trim().Equals(TblPurMaster.Pur_VC_No.ToUpper().Trim())));
                if (res == false)
                {
                    if (TblPurMaster.Id == 0)
                    {
                        TblPurMaster.Id = _db.Purchase_Master.Count() > 0 ? _db.Purchase_Master.Max(x => x.Id) + 1 : 1;
                        _db.Purchase_Master.Add(TblPurMaster);
                    }
                    _db.SaveChanges();
                    result = true;

                }
            }
            catch
            {

                return 0;
            }

            return result == true ? TblPurMaster.Id : 0;
        }
        public bool Update(ViewModel.Ledger.PurchaseMaster ModelPurMaster)
        {
            bool result = false;
            using (TransactionScope ts = new TransactionScope())
            {

                try
                {
                    List<Purchase_Master_Tra> prevlst = new List<Purchase_Master_Tra>();
                    Purchase_Master TblPurMaster = _db.Purchase_Master.Find(ModelPurMaster.Id);
                    TblPurMaster.Fncl_Year_Id = ModelPurMaster.Finance_Id != 0 ? ModelPurMaster.Finance_Id : 1;

                    TblPurMaster.Pur_VC_No = ModelPurMaster.Challan_Number;

                    TblPurMaster.Modified_By = 1;
                    TblPurMaster.Refered_Id = ModelPurMaster.Quotation_Id != 0 ? ModelPurMaster.Quotation_Id : 0;

                    TblPurMaster.Modified_By = 1;
                    TblPurMaster.Modified_Date = DateTime.Now;
                    TblPurMaster.DATE = ModelPurMaster.Purchase_Date;
                    TblPurMaster.Remarks = ModelPurMaster.Remarks;
                    TblPurMaster.Supplier_Id = ModelPurMaster.Supplier_Id;
                    TblPurMaster.Total_Amount = ModelPurMaster.Grand_Total;
                    TblPurMaster.Discount_Amount = ModelPurMaster.Discount != 0 ? ModelPurMaster.Discount : 0.0m;
                    TblPurMaster.Is_Refered = false;
                    var rs = Save(TblPurMaster);
                    if (rs != 0)
                    {
                        //result = CreatePurchaseTra(ModelPurMaster.purchase_tra.listpurchase_tra.Where(x => x.Status == true).ToList(), TblPurMaster.Pur_VC_No);
                        result = CreatePurchaseTaxDetail(ModelPurMaster.purchase_tra.taxdetails.listTaxDetails, rs);

                        result = Stock_MasterUpdate(ModelPurMaster.purchase_tra.listpurchase_tra, rs);
                        // result = Stock_MasterUpdate(ModelPurMaster.purchase_tra.listpurchase_tra);
                        // var list = ModelPurMaster.ItemDetails.Where(x => x.Status == false).ToList();
                    }
                }
                catch
                {

                    result = false;
                }
                ts.Complete();
                ts.Dispose();
            }
            return result;
        }
        public bool CreatePurchase(string Purch, string Chllan_no)
        {
            bool result = false;
            return result;
        }
        public bool CreatePurchaseTra(List<ViewModel.Ledger.Purchase_Tra> ModelPurMasterTra, long id)
        {
            bool result = false;
            try
            {
                List<Purchase_Master_Tra> TblPurMasterTra = new List<Purchase_Master_Tra>();
                Purchase_Master_Tra tbltra;

                if (ModelPurMasterTra != null)
                {
                    _db.Pr_DeleteStock_Item(1, "PURCHASE");

                    foreach (var item in ModelPurMasterTra)
                    {
                        tbltra = new Purchase_Master_Tra();
                        tbltra.Item_Id = item.Item_Id;
                        tbltra.Pur_Id = id;
                        tbltra.Qty = item.Qty;
                        tbltra.Rate = item.Rate;
                        tbltra.Tax_Id = item.Tax_Id;
                        //tbltra.Pur_VC_No = Challan_No;
                        tbltra.GoDown_Id = item.GoDownId;
                        tbltra.Purchase_Serial_No = item.ProductCode;
                        tbltra.Sale_Serial_No = item.SerialNo;
                        tbltra.Created_By = 1;
                        tbltra.Created_Date = DateTime.Now;
                        tbltra.Is_Active = true;
                        tbltra.Is_Deleted = false;
                        tbltra.Modified_By = 1;
                        tbltra.Modified_Date = DateTime.Now;
                        tbltra.Modified_By = 1;
                        TblPurMasterTra.Add(tbltra);

                    }
                    _db.Purchase_Master_Tra.AddRange(TblPurMasterTra);
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

        public bool CreatePurchaseTaxDetail(List<ViewModel.Common.TaxDetails> taxdetails, long Pur_Id)
        {
            List<Purchase_Master_TaxDetails> listtax = new List<Purchase_Master_TaxDetails>();

            if (taxdetails != null)
            {
                _db.Pr_DeleteStock_Item(Pur_Id, "TAXDETAILS");
                _db.Purchase_Master_TaxDetails.AddRange(taxdetails.Select(x => new Purchase_Master_TaxDetails
                {
                    Pur_Id = Pur_Id,
                    Pur_VC_No = "",
                    Tax_Id = Convert.ToInt32(x.Id.Split('_')[0]),
                    //Tax_Rate = Convert.ToDecimal(x.Id.Split('_')[3]),
                    Tax_Rate = Convert.ToDecimal(x.Rate_Type),
                    Tax_Amount = Convert.ToDecimal(x.Value),
                    Created_By = 1,
                    Created_Date = DateTime.Now,
                    Modified_By = 1,
                    Modified_Date = DateTime.Now,
                    Is_Deleted = false,
                    Is_Active = true,
                }));

            }

            // _db.Purchase_Master_TaxDetails.AddRange(listtax);
            _db.SaveChanges();
            return true;
        }
        public ViewModel.Ledger.PurchaseMaster Find(long Id = 0)
        {
            ViewModel.Ledger.PurchaseMaster ModelPurMaster = new PurchaseMaster();
            if (Id != 0)
            {
                DataLayer.Purchase_Master tblPurMaster = new Purchase_Master();
                tblPurMaster = Id > 0 ? _db.Purchase_Master.Find(Id) : Id <= 0 ? _db.Purchase_Master.FirstOrDefault() : null;

                if (tblPurMaster != null)
                {
                    // tblPurMaster = _db.Purchase_Master.Find(Id);
                    ModelPurMaster.Id = tblPurMaster.Id;
                    ModelPurMaster.Challan_Number = tblPurMaster.Pur_VC_No;
                    ModelPurMaster.Company_Id = tblPurMaster.Company_Id;
                    ModelPurMaster.Discount = tblPurMaster.Discount_Amount ?? 0.00m;
                    ModelPurMaster.Finance_Id = tblPurMaster.Fncl_Year_Id;
                    ModelPurMaster.Grand_Total = tblPurMaster.Total_Amount ?? 0.00m;
                    ModelPurMaster.Is_Quotation = tblPurMaster.Is_Refered;
                    ModelPurMaster.Quotation_Id = tblPurMaster.Refered_Id ?? 0;
                    ModelPurMaster.Remarks = tblPurMaster.Remarks;
                    ModelPurMaster.Supplier_Id = tblPurMaster.Supplier_Id;
                    ModelPurMaster.Purchase_Date = tblPurMaster.DATE;
                    ModelPurMaster.Is_Challan_Gen = tblPurMaster.Is_Bill_Gen;
                    ModelPurMaster.Supplier_Name = tblPurMaster.Supplier_Id != 0 ? _db.Ledger_Master.SingleOrDefault(x => x.Id == tblPurMaster.Supplier_Id).Name : "";
                    ModelPurMaster.EmailId = tblPurMaster.Supplier_Id != 0 ? _db.Ledger_Master.SingleOrDefault(x => x.Id == tblPurMaster.Supplier_Id).Email_Id : "";
                    ModelPurMaster.ContactNo = tblPurMaster.Supplier_Id != 0 ? _db.Ledger_Master.SingleOrDefault(x => x.Id == tblPurMaster.Supplier_Id).Contact_No : "";

                }
            }
            return ModelPurMaster;
        }

        public bool CreateStockMaster(List<ViewModel.Ledger.Purchase_Tra> ModelPurMasterTra)
        {
            List<Stock_Master> lstStockMaster = new List<Stock_Master>();
            List<Stock_Master> ls = new List<Stock_Master>();
            // tblStockMaster.Id = ModelStock.Id;
            Stock_Master tblStockMaster;
            bool result = false;
            bool existsItem = false;

            foreach (var item in ModelPurMasterTra)
            {
                existsItem = _db.Stock_Master.Any(m => m.Item_Id == item.Item_Id);
                if (existsItem == true)
                {
                    tblStockMaster = _db.Stock_Master.FirstOrDefault(x => x.Item_Id == item.Item_Id);
                    tblStockMaster.Qty = tblStockMaster.Qty + item.Qty;
                    tblStockMaster.Rate = item.Rate;
                    // lstStockMaster.Add(tblStockMaster);
                }
                else
                {
                    tblStockMaster = new Stock_Master();


                    tblStockMaster.Company_Id = 1;
                    tblStockMaster.Fncl_Year_Id = 1;
                    tblStockMaster.Item_Id = item.Item_Id;


                    tblStockMaster.Qty = item.Qty;
                    tblStockMaster.Rate = item.Rate;
                    tblStockMaster.Selling_Rate = item.Rate;

                    tblStockMaster.Created_By = 1;
                    tblStockMaster.Created_Date = DateTime.Now;
                    tblStockMaster.Is_Active = true;
                    tblStockMaster.Is_Deleted = false;
                    tblStockMaster.Modified_By = 1;
                    tblStockMaster.Modified_Date = DateTime.Now;
                    lstStockMaster.Add(tblStockMaster);
                }

            }




            var summary = (from p in lstStockMaster
                           let k = new
                           {
                               //try this if you need a date field 
                               //   p.SaleDate.Date.AddDays(-1 *p.SaleDate.Day - 1)
                               Company = p.Company_Id,

                               Item_Id = p.Item_Id,
                               Fncl_Year_Id = p.Fncl_Year_Id,
                               Qty = p.Qty,
                               Rate = p.Rate,
                               Opening_Qty = 0
                           }
                           group p by k into t
                           select new
                           {

                               Item_Id = t.Key.Item_Id,

                               Opening_Qty = t.Key.Opening_Qty,
                               Rate = t.Key.Rate,
                               Qty = t.Sum(p => p.Qty)
                           }).ToList();

            //var ls=from m in lstStockMaster
            //       group by new {ls}
            var id = _db.Stock_Master.Count() > 0 ? _db.Stock_Master.Max(x => x.Id) + 1 : 1;

            foreach (var item in summary)
            {
                ls.Add(new Stock_Master
                {
                    Id = id,
                    Company_Id = 1,
                    Fncl_Year_Id = 1,
                    Item_Id = item.Item_Id,
                    Qty = item.Qty,
                    Rate = item.Rate,
                    Selling_Rate = item.Rate,
                    Created_By = 1,
                    Created_Date = DateTime.Now,
                    Is_Active = true,
                    Is_Deleted = false,
                    Modified_By = 1,
                    Modified_Date = DateTime.Now

                });
                id++;
            }
            //summary.ForEach(item => ls.Add(new Stock_Master
            //{
            //    Id = id,
            //        Company_Id = 1,
            //        Fncl_Year_Id = 1,
            //       Item_Id = item.Item_Id,


            //       Qty = item.Qty,
            //       Rate = item.Rate,
            //       Selling_Rate = item.Rate,

            //       Created_By = 1,
            //       Created_Date = DateTime.Now,
            //       Is_Active = true,
            //       Is_Deleted = false,
            //       Modified_By = 1,
            //       Modified_Date = DateTime.Now

            //}));
            _db.Stock_Master.AddRange(ls);
            _db.SaveChanges();
            result = true;
            return result;
        }
        //public bool ManageStockMaster(string Xmldata, string Challan_No)
        //{
        //    bool result = false;

        //    int r = _db.Pr_Purchase_master_tra(Xmldata, Challan_No);
        //    return result = r == 0 ? false : true;

        //}
        private bool Stock_MasterUpdate(List<ViewModel.Ledger.Purchase_Tra> ModelPurMasterTra, long Pur_Id)
        {
            System.Data.DataTable tbl = new System.Data.DataTable("Product");
            System.Data.DataColumn[] columns = new System.Data.DataColumn[14];
            columns[0] = new System.Data.DataColumn("Item_Id", typeof(int));
            columns[1] = new System.Data.DataColumn("Qty", typeof(decimal));
            columns[2] = new System.Data.DataColumn("Rate", typeof(decimal));

            columns[3] = new System.Data.DataColumn("Pur_Id", typeof(long));

            columns[4] = new System.Data.DataColumn("Godown_Id", typeof(int));
            columns[5] = new System.Data.DataColumn("ProductCode", typeof(string));
            columns[6] = new System.Data.DataColumn("SerialNo", typeof(string));
            columns[7] = new System.Data.DataColumn("Created_By", typeof(int));
            columns[8] = new System.Data.DataColumn("Created_Date", typeof(DateTime));

            columns[9] = new System.Data.DataColumn("Is_Active", typeof(bool));
            columns[10] = new System.Data.DataColumn("Is_Deleted", typeof(bool));
            columns[11] = new System.Data.DataColumn("Modified_By", typeof(int));
            columns[12] = new System.Data.DataColumn("Modified_Date", typeof(DateTime));
            columns[13] = new System.Data.DataColumn("Status", typeof(bool));
            tbl.Columns.AddRange(columns);

            foreach (var item in ModelPurMasterTra)
            {
                var row = tbl.NewRow();

                row["Item_Id"] = item.Item_Id;
                row["Qty"] = item.Qty;
                row["Status"] = item.Status;
                row["Rate"] = item.Rate;
                row["Pur_Id"] = Pur_Id;
                row["Godown_Id"] = item.GoDownId;
                row["ProductCode"] = item.ProductCode;
                row["SerialNo"] = item.SerialNo;
                row["Created_By"] = 1;
                row["Created_Date"] = DateTime.Now;
                row["Is_Active"] = true;
                row["Is_Deleted"] = false;
                row["Modified_By"] = 1;
                row["Modified_Date"] = DateTime.Now;

                tbl.Rows.Add(row);
            }

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
            var regularConnectionString = builder.ProviderConnectionString;

            using (SqlConnection connection = new SqlConnection(regularConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);

                // command.CommandText = "Pr_UpdateStockMaster";
                command.CommandText = "Update_stock_and_Tra";
                command.Parameters.Clear();
                command.CommandType = CommandType.StoredProcedure;
                // command.Parameters.AddWithValue("@someIdForParameter", someId);
                command.Parameters.AddWithValue("@tblStockMaster", tbl).SqlDbType = SqlDbType.Structured;
                command.Parameters.AddWithValue("@pur_Id", Pur_Id).SqlDbType = SqlDbType.BigInt;
                command.Parameters.AddWithValue("@Tag", "Purcahse").SqlDbType.ToString();
                command.ExecuteNonQuery();

                connection.Close();

                bool result = true;

                return result;
            };
        }
        private bool Stock_MasterUpdate2(List<ViewModel.Ledger.Purchase_Tra> ModelPurMasterTra)
        {
            System.Data.DataTable tbl = new System.Data.DataTable("Product");
            System.Data.DataColumn[] columns = new System.Data.DataColumn[4];
            columns[0] = new System.Data.DataColumn("Item_Id", typeof(int));
            columns[1] = new System.Data.DataColumn("Qty", typeof(decimal));
            columns[2] = new System.Data.DataColumn("Rate", typeof(decimal));
            columns[3] = new System.Data.DataColumn("Status", typeof(bool));
            tbl.Columns.AddRange(columns);

            foreach (var item in ModelPurMasterTra)
            {
                var row = tbl.NewRow();

                row["Item_Id"] = item.Item_Id;
                row["Qty"] = item.Qty;
                row["Status"] = item.Status;
                row["Rate"] = item.Rate;
                tbl.Rows.Add(row);
            }




            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
            var regularConnectionString = builder.ProviderConnectionString;
            using (SqlConnection connection = new SqlConnection(regularConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);

                command.CommandText = "Pr_UpdateStockMaster";
                command.Parameters.Clear();
                command.CommandType = CommandType.StoredProcedure;
                // command.Parameters.AddWithValue("@someIdForParameter", someId);
                command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
                command.ExecuteNonQuery();

                connection.Close();

                bool result = true;

                return result;
            };
        }
        public String GEN_ChallanNo()
        {
            string Challan_No = "";

            int countRows = _db.Purchase_Master.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Purchase_Master.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Pur_VC_No;
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

        public bool Gen_ChallanOfPurchse(int Id)
        {
            bool result = false;
            try
            {
                var tblpurMaster = _db.Purchase_Master.Find(Id);
                tblpurMaster.Is_Bill_Gen = true;
                tblpurMaster.Modified_Date = DateTime.Now;
                tblpurMaster.Modified_By = 1;
                _db.SaveChanges();
                result = true;
                if (result == true)
                {
                    Billing_Info tblBilling = new Billing_Info();
                    tblBilling.Is_Active = true;
                    tblBilling.Is_Deleted = false;
                    tblBilling.Modified_By = 1;

                    tblBilling.Modified_Date = DateTime.Now;
                    tblBilling.Created_By = 1;
                    tblBilling.Created_Date = DateTime.Now;
                    tblBilling.Discount_Amount = tblpurMaster.Discount_Amount;
                    tblBilling.Company_Id = tblpurMaster.Company_Id;
                    tblBilling.Billing_Amount = tblpurMaster.Total_Amount;
                    //tblBilling.Chalan_No = tblpurMaster.Pur_VC_No;
                    tblBilling.Remarks = "After gen challan";
                    tblBilling.Tax_Amount = 0;
                    tblBilling.Transport = 0;
                    tblBilling.Amount = (tblBilling.Billing_Amount + tblBilling.Tax_Amount + tblBilling.Transport) - tblBilling.Discount_Amount;
                    tblBilling.Billing_Date = tblpurMaster.DATE;
                    tblBilling.User_Id = tblpurMaster.Supplier_Id;
                    //tblBilling.s = false;
                    tblBilling.Tag_Type = "SUPPLIER";
                    _db.Billing_Info.Add(tblBilling);
                    _db.SaveChanges();

                }
            }
            catch
            {
                return result;

            }
            return result;
        }
        public bool PurchageReturn(List<ViewModel.Ledger.Challan_Details> ModelPurMaster)
        {
            bool result = false;
            try
            {
                Purchase_Return TblPurReturn = new Purchase_Return();
                //  TblPurMaster.Id = ModelPurMaster.Id;
                TblPurReturn.Purchase_Id = 1;


                TblPurReturn.PurRet_VC_No = ModelPurMaster.FirstOrDefault().Challan_Number;
                TblPurReturn.Is_Active = true;
                TblPurReturn.Is_Deleted = false;
                TblPurReturn.Modified_By = 1;
                // TblPurReturn.Quation_Id = ModelPurMaster.Quotation_Id != 0 ? ModelPurMaster.Quotation_Id : 0;
                TblPurReturn.Created_By = 1;
                TblPurReturn.Created_Date = DateTime.Now;
                TblPurReturn.Modified_By = 1;
                TblPurReturn.Modified_Date = DateTime.Now;
                TblPurReturn.DATE = Convert.ToDateTime(ModelPurMaster.FirstOrDefault().Purchase_Date);
                TblPurReturn.Remarks = ModelPurMaster.FirstOrDefault().Remarks;
                TblPurReturn.Purchase_Id = ModelPurMaster.FirstOrDefault().SupplierId;
                TblPurReturn.Total_Amount = ModelPurMaster.FirstOrDefault().Grand_Total;
                TblPurReturn.Discount_Amount = ModelPurMaster.FirstOrDefault().Discount != 0 ? ModelPurMaster.FirstOrDefault().Discount : 0.0m;
                TblPurReturn.Is_Deleted = false;
                TblPurReturn.Is_Active = true;
                TblPurReturn.Created_By = 1;
                TblPurReturn.Modified_By = 1;
                TblPurReturn.Created_Date = System.DateTime.Now;
                TblPurReturn.Modified_Date = System.DateTime.Now;
                var status = false;
                _db.Purchase_Return.Add(TblPurReturn);
                _db.SaveChanges();
                status = true;

                var rs = false;
                // var rs = Save(TblPurReturn);
                if (status == true)
                {
                    rs = true;
                    result = ReturnPurchaseTra(ModelPurMaster, TblPurReturn.Id);
                }
                if (result == true)
                {
                    result = UpdatetockMaster(ModelPurMaster);
                }
                result = rs;
            }
            catch
            {

                result = false;
            }
            return result;
        }


        public bool ReturnPurchaseTra(List<ViewModel.Ledger.Challan_Details> ModelPurMasterTra, long PurRet_Id)
        {
            bool result = false;
            try
            {
                List<Purchase_Return_Tra> TblPurMasterTra = new List<Purchase_Return_Tra>();
                Purchase_Return_Tra tbltra;
                if (ModelPurMasterTra != null && PurRet_Id != 0)
                {
                    _db.Pr_DeleteStock_Item(PurRet_Id, "");
                    _db.Purchase_Return_Tra.AddRange(ModelPurMasterTra.Where(item => item.ReturnQuantity != 0).Select(item => new Purchase_Return_Tra
                    {
                        Id = item.Id,
                        Item_Id = item.ItemId,
                        Qty = item.ReturnQuantity,
                        Rate = item.Rate,
                        // Tax_Id = item.Tax,
                        //  Chalan_No = Challan_No,
                        Created_By = 1,
                        Created_Date = DateTime.Now,
                        Is_Active = true,
                        Is_Deleted = false,
                        Modified_By = 1,
                        Modified_Date = DateTime.Now
                    }));
                    //foreach (var item in ModelPurMasterTra)
                    //{
                    //    if (item.ReturnQuantity != 0)
                    //    {
                    //        tbltra = new Purchase_Return_Tra();

                    //        tbltra.Id = item.Id;
                    //        tbltra.Item_Id = item.ItemId;
                    //        tbltra.Qty = item.ReturnQuantity;
                    //        tbltra.Rate = item.Rate;
                    //        // tbltra.Tax_Id = item.Tax;
                    //        tbltra.Chalan_No = Challan_No;

                    //        tbltra.Created_By = 1;
                    //        tbltra.Created_Date = DateTime.Now;
                    //        tbltra.Is_Active = true;
                    //        tbltra.Is_Deleted = false;
                    //        tbltra.Modified_By = 1;
                    //        tbltra.Modified_Date = DateTime.Now;


                    //        tbltra.Modified_By = 1;
                    //        TblPurMasterTra.Add(tbltra);
                    //    }
                    //}
                    //_db.Purchase_Return_Tra.AddRange(TblPurMasterTra);
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
        public bool UpdatetockMaster(List<ViewModel.Ledger.Challan_Details> ModelPurMaster)
        {
            for (int i = 0; i < ModelPurMaster.Count(); i++)
            {
                if (ModelPurMaster[i].ReturnQuantity != 0)
                {
                    long id = ModelPurMaster[i].ItemId;
                    var findData = _db.Stock_Master.SingleOrDefault(m => m.Item_Id == id);
                    findData.Qty = findData.Qty - ModelPurMaster[i].ReturnQuantity;
                    _db.SaveChanges();
                }
            }

            return true;
        }
        public bool UpdateStatusInPurchaseOrder(long orderId)
        {
            var data = _db.Purchase_Order.Where(x => x.Id == orderId).FirstOrDefault();
            data.Status = true;
            data.Is_Receipt_Note = true;
            _db.SaveChanges();
            return true;
        }
    }
}
