using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using ViewModel.Ledger;

namespace DataLayer.Transactions
{
    public class SalesDbLayer
    {
        INVENTORY_DBEntities _db;
        int CompanyId;
        public SalesDbLayer()
        {
            _db = new INVENTORY_DBEntities();
            CompanyId = 1;
        }
        public SalesDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }

        public List<PurchaseList> List()
        {

            var lst = _db.Pr_SalesList(1).ToList().Select(x => new PurchaseList
            {
                Id = x.Id,
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
        public List<Challan_Details> ViewDetails(long Id)
        {
            List<Challan_Details> lstchldetal = new List<Challan_Details>();
            if (Id != 0)
            {
                //var tblPurmaster = _db.Sales_Master.Find(Id);
                //var lst = _db.Pr_Challan_Details(Id, "SALES").ToList();


                //lst.ForEach(x => lstchldetal.Add(new Challan_Details
                //{
                //    Id = x.Id,
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
                //}));
            }
            return lstchldetal;
        }
        public List<ViewModel.Ledger.Purchase_Tra> ItemDetails(long salesId)
        {
            List<ViewModel.Ledger.Purchase_Tra> modellist = new List<Purchase_Tra>();
            // var lst = _db.Sales_Master_Tra.Where(x => x.Sales_VC_No == Challan_No).ToList();
            //              stock in _db.Stock_Master on m.Item_Id equals stock.Item_Id select new{ Id=m.Id,Challan_Number=m.Chalan_No,GoDownId=0,Item_Id=m.Item_Id,Qty=m.Qty,Rate=m.Rate,

            //}

            (from tra in _db.Sales_Master_Tra
             join St in _db.Stock_Item on tra.Item_Id equals St.Id into i
             from item in i.DefaultIfEmpty()
             where tra.Sales_Id == salesId
             select new
             {
                 tra.Id,
                 //  tra.Sales_VC_No,
                 tra.Item_Id,
                 tra.Qty,
                 tra.Rate,
                 tra.Tax_Id,
                 item.Name,
                 tra.Sale_Serial_No
             }).AsParallel().AsEnumerable().ToList().ForEach((x) => modellist.Add(new Purchase_Tra
             {
                 Id = x.Id,
                 // Challan_Number = x.Sales_VC_No,
                 GoDownId = 0,
                 Item_Id = x.Item_Id,
                 Qty = x.Qty ?? 0.0m,
                 Rate = x.Rate ?? 0.0m,
                 // Tax_Id = x.Tax_Id ?? 0,
                 Total_Amount = x.Qty * x.Rate ?? 0.0m,
                 Unit_Id = 1,
                 Unit_Name = UnitNameByItemId(x.Item_Id),
                 ItemName = x.Name,
                 SerialNo = x.Sale_Serial_No
             }));

            //lst.ForEach(x =>
            //    modellist.Add(new Purchase_Tra
            //    {
            //        Id = x.Id,
            //        Challan_Number = x.Sales_VC_No,
            //        GoDownId = 0,
            //        Item_Id = x.Item_Id,
            //        Qty = x.Qty ?? 0.0m,
            //        Rate = x.Rate ?? 0.0m,
            //        Tax_Id = x.Tax_Id ?? 0,
            //        Total_Amount = x.Qty * x.Rate ?? 0.0m,
            //        Unit_Id = 1,
            //        Unit_Name = UnitNameByItemId(x.Item_Id)

            //    }));

            //foreach (var x in lst)
            //{

            //}

            //    Id=x.Id,Challan_Number=x.Chalan_No,GoDownId=0,Item_Id=x.Item_Id,Qty=x.Qty??0.0m,Rate=x.Rate??0.0m,Tax_Id=x.Tax_Id??0,Total_Amount=x.Qty*x.Rate??0.0m,Unit_Id=1,
            //    Unit_Name = UnitNameByItemId(x.Item_Id)
            //}).ToList();

            return modellist;
        }
        private string UnitNameByItemId(long ItemId)
        {
            string name = "";
            if (ItemId != 0)
            {
                var item = _db.Stock_Item.SingleOrDefault(x => x.Id == ItemId);
                name = _db.UnitMasters.SingleOrDefault(x => x.Id == item.Unit_Id).Name;
            }
            return name;
        }
        public bool Create(ViewModel.Ledger.SalesMaster ModelPurMaster)
        {
            bool result = false;

            //using (TransactionScope ts = new TransactionScope()) 
            //{  
            try
            {
                //Purchase_Master TblPurMaster = new Purchase_Master();
                Sales_Master TblSalesMaster = new Sales_Master();
                //  TblSalesMaster.Id = ModelPurMaster.Id;
                TblSalesMaster.Fncl_Year_Id = ModelPurMaster.Finance_Id != 0 ? ModelPurMaster.Finance_Id : 1;
                TblSalesMaster.Company_Id = 1;
                //  TblSalesMaster.Branch_Id = 1;
                TblSalesMaster.Sales_VC_No = ModelPurMaster.Challan_Number;
                TblSalesMaster.Is_Active = true;
                TblSalesMaster.Is_Deleted = false;
                TblSalesMaster.Modified_By = 1;
                TblSalesMaster.Refered_Id = ModelPurMaster.Quotation_Id != 0 ? ModelPurMaster.Quotation_Id : 0;
                TblSalesMaster.Created_By = 1;
                TblSalesMaster.Created_Date = DateTime.Now;
                TblSalesMaster.Modified_By = 1;
                TblSalesMaster.Modified_Date = DateTime.Now;
                TblSalesMaster.DATE = ModelPurMaster.Purchase_Date;
                TblSalesMaster.Remarks = ModelPurMaster.Remarks;
                TblSalesMaster.Customer_Id = ModelPurMaster.Customer_Id;
                TblSalesMaster.Total_Amount = ModelPurMaster.Grand_Total;
                TblSalesMaster.Discount_Amount = ModelPurMaster.Discount != 0 ? ModelPurMaster.Discount : 0.0m;
                TblSalesMaster.Is_Refered = false;
                var rs = Save(TblSalesMaster);
                if (rs != 0)
                {
                    //result = CreatePurchaseTra(ModelPurMaster.sales_tra.listpurchase_tra.ToList(), TblSalesMaster.Sales_VC_No, 0);
                    // result = Stock_MasterUpdate(ModelPurMaster.sales_tra.listpurchase_tra.Where(x => x.Status == true).ToList());
                    result = UpdateSales_TraAndStock_master(ModelPurMaster.sales_tra.listpurchase_tra, rs);
                    result = CreateSalesTaxDetail(ModelPurMaster.sales_tra.taxdetails.listTaxDetails, rs);
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

        private long Save(Sales_Master TblPurMaster)
        {
            bool result = false;
            try
            {
                bool res = _db.Sales_Master.Any(m => m.Company_Id == 1 && (0 == TblPurMaster.Id || m.Id != TblPurMaster.Id) && (m.Sales_VC_No.ToUpper().Trim().Equals(TblPurMaster.Sales_VC_No.ToUpper().Trim())));
                if (res == false)
                {
                    if (TblPurMaster.Id == 0)
                    {
                        TblPurMaster.Id = _db.Sales_Master.Count() > 0 ? _db.Sales_Master.Max(x => x.Id) + 1 : 1;
                        _db.Sales_Master.Add(TblPurMaster);
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
        //public bool Update(ViewModel.Ledger.SalesMaster ModelPurMaster)
        //{
        //    bool result = false;
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        try
        //        {
        //            Sales_Master TblSalesMaster = _db.Sales_Master.Find(ModelPurMaster.Id);
        //            TblSalesMaster.Fncl_Year_Id = ModelPurMaster.Finance_Id != 0 ? ModelPurMaster.Finance_Id : 1;

        //            TblSalesMaster.Sales_VC_No = ModelPurMaster.Challan_Number;

        //            TblSalesMaster.Modified_By = 1;
        //            TblSalesMaster.Refered_Id = ModelPurMaster.Quotation_Id != 0 ? ModelPurMaster.Quotation_Id : 0;

        //            TblSalesMaster.Modified_By = 1;
        //            TblSalesMaster.Modified_Date = DateTime.Now;
        //            TblSalesMaster.DATE = ModelPurMaster.Purchase_Date;
        //            TblSalesMaster.Remarks = ModelPurMaster.Remarks;
        //            TblSalesMaster.Customer_Id = ModelPurMaster.Customer_Id;
        //            TblSalesMaster.Total_Amount = ModelPurMaster.Grand_Total;
        //            TblSalesMaster.Discount_Amount = ModelPurMaster.Discount != 0 ? ModelPurMaster.Discount : 0.0m;
        //            TblSalesMaster.Is_Refered = false;
        //            var rs = Save(TblSalesMaster);
        //            if (rs != 0)
        //            {
        //                result = CreatePurchaseTra(ModelPurMaster.sales_tra.listpurchase_tra.ToList(), TblSalesMaster.Sales_VC_No, ModelPurMaster.Id);
        //                result = Stock_MasterUpdate(ModelPurMaster.sales_tra.listpurchase_tra);
        //                result = CreateSalesTaxDetail(ModelPurMaster.sales_tra.taxdetails.listTaxDetails, TblSalesMaster.Sales_VC_No);
        //            }
        //        }
        //        catch
        //        {

        //            result = false;
        //        }
        //        ts.Complete();
        //        ts.Dispose();
        //    }

        //    return result;
        //}
        public bool Update(ViewModel.Ledger.SalesMaster ModelPurMaster)
        {
            bool result = false;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    Sales_Master TblSalesMaster = _db.Sales_Master.Find(ModelPurMaster.Id);
                    TblSalesMaster.Fncl_Year_Id = ModelPurMaster.Finance_Id != 0 ? ModelPurMaster.Finance_Id : 1;

                    TblSalesMaster.Sales_VC_No = ModelPurMaster.Challan_Number;

                    TblSalesMaster.Modified_By = 1;
                    TblSalesMaster.Refered_Id = ModelPurMaster.Quotation_Id != 0 ? ModelPurMaster.Quotation_Id : 0;

                    TblSalesMaster.Modified_By = 1;
                    TblSalesMaster.Modified_Date = DateTime.Now;
                    TblSalesMaster.DATE = ModelPurMaster.Purchase_Date;
                    TblSalesMaster.Remarks = ModelPurMaster.Remarks;
                    TblSalesMaster.Customer_Id = ModelPurMaster.Customer_Id;
                    TblSalesMaster.Total_Amount = ModelPurMaster.Grand_Total;
                    TblSalesMaster.Discount_Amount = ModelPurMaster.Discount != 0 ? ModelPurMaster.Discount : 0.0m;
                    TblSalesMaster.Is_Refered = false;
                    var rs = Save(TblSalesMaster);
                    if (rs != 0)
                    {
                        // result = CreatePurchaseTra(ModelPurMaster.sales_tra.listpurchase_tra.ToList(), TblSalesMaster.Sales_VC_No, ModelPurMaster.Id);
                        //  result = Stock_MasterUpdate(ModelPurMaster.sales_tra.listpurchase_tra);
                        result = UpdateSales_TraAndStock_master(ModelPurMaster.sales_tra.listpurchase_tra, rs);
                        result = CreateSalesTaxDetail(ModelPurMaster.sales_tra.taxdetails.listTaxDetails, rs);
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
        public bool CreatePurchaseTra(List<ViewModel.Ledger.Purchase_Tra> ModelPurMasterTra, long Sales_Id, int Id)
        {
            bool result = false;
            try
            {
                // List<Purchase_Master_Tra> TblPurMasterTra = new List<Purchase_Master_Tra>();
                List<Sales_Master_Tra> TblPurMasterTra = new List<Sales_Master_Tra>();
                Sales_Master_Tra tbltra;
                if (ModelPurMasterTra != null && Sales_Id != 0)
                {
                    string IDs = "";
                    foreach (var item in ModelPurMasterTra)
                    {
                        if (!IDs.Contains(item.Item_Id.ToString()))
                        {
                            IDs += item.Item_Id + ",";
                        }
                    }
                    IDs.Remove(IDs.LastIndexOf(','));

                    if (Id > 0)
                    {
                        var salestralist = _db.Sales_Master_Tra.Where(x => x.Sales_Id == Sales_Id).ToList();
                        List<Purchase_Master_Tra> listtra = new List<Purchase_Master_Tra>();
                        Purchase_Master_Tra p_tra = new Purchase_Master_Tra();
                        foreach (var item in salestralist)
                        {
                            var PurchaseTra = _db.Purchase_Master_Tra.Where(x => x.Sale_Serial_No == item.Sale_Serial_No).FirstOrDefault();

                            _db.UpdateQtyAndIsActive_PurchaseTra(PurchaseTra.Id, PurchaseTra.Qty + 1, PurchaseTra.Is_Active, PurchaseTra.Is_Deleted);
                        }
                        //_db.Purchase_Master_Tra.AddRange(listtra);// I am at this line ...
                        //_db.SaveChanges();
                    }
                    // var lst = _db.Purchase_Master_Tra.Where(x => IDs.Contains(x.Item_Id.ToString()) && x.Is_Active == true).ToList();
                    List<ViewModel.Ledger.Purchase_Tra> list = new List<Purchase_Tra>();
                    //(from tra in _db.Purchase_Master_Tra
                    //              join s in _db.Sales_Master_Tra on tra.Sale_Serial_No equals s.Sale_Serial_No into temp
                    //              from t in temp.DefaultIfEmpty()
                    //              where tra.Sale_Serial_No != t.Sale_Serial_No
                    //              select new
                    //              {
                    //                  tra.Id,
                    //                  tra.Item_Id,
                    //                  tra.Sale_Serial_No,
                    //                  tra.Is_Active,
                    //                  tra.Is_Deleted,
                    //                  tra.Qty
                    //              }).ToList().ForEach((x) => list.Add(new Purchase_Tra {Id=x.Id,Item_Id=x.Item_Id,SerialNo=x.Sale_Serial_No,IsActive=x.Is_Active,IsDeleted=x.Is_Deleted }));
                    foreach (var x in _db.GetdatafromPurchaseTra())
                    {
                        list.Add(new Purchase_Tra { Id = x.Id, Item_Id = x.Item_Id, SerialNo = x.Sale_Serial_No, IsActive = x.Is_Active, IsDeleted = x.Is_Deleted });
                    }


                    _db.Pr_DeleteStock_Item(1, "SALES");
                    //List<Purchase_Master_Tra> list =new List<Purchase_Master_Tra>() ;
                    int i = 0;
                    int itm_Id = 0;
                    foreach (var item in ModelPurMasterTra)
                    {
                        tbltra = new Sales_Master_Tra();

                        tbltra.Id = item.Id;
                        tbltra.Item_Id = item.Item_Id;
                        tbltra.Qty = item.Qty;
                        tbltra.Rate = item.Rate;
                        tbltra.Tax_Id = 0;
                        // tbltra.Sales_VC_No = Challan_No;
                        tbltra.GoDown_Id = 1;

                        tbltra.Sale_Serial_No = item.SerialNo != null ? item.SerialNo : list.Where(x => x.IsActive == true).FirstOrDefault().SerialNo; //lst.Where(x => x.Item_Id == item.Item_Id && x.Is_Active == true).FirstOrDefault().Sale_Serial_No;

                        tbltra.Created_By = 1;
                        tbltra.Created_Date = DateTime.Now;
                        tbltra.Is_Active = true;
                        tbltra.Is_Deleted = false;
                        tbltra.Modified_By = 1;
                        tbltra.Modified_Date = DateTime.Now;
                        tbltra.Modified_By = 1;
                        TblPurMasterTra.Add(tbltra);
                        i++;

                        if (item.SerialNo == "")
                        {
                            var lst = list.FirstOrDefault(x => x.SerialNo == tbltra.Sale_Serial_No);
                            if (lst.Qty > 1)
                            {
                                lst.Qty -= 1;
                            }
                            else
                            {
                                lst.Qty -= 1;
                                lst.IsActive = false;
                                lst.IsDeleted = true;
                            }
                        }
                    }
                    _db.Sales_Master_Tra.AddRange(TblPurMasterTra);
                    _db.SaveChanges();


                    foreach (var item in list)
                    {
                        _db.UpdateQtyAndIsActive_PurchaseTra(item.Id, item.Qty, item.IsActive, item.IsDeleted);
                    }
                }
            }
            catch
            {

                result = false;
            }
            return result;
        }
        public bool CreatePurchaseTra2(List<ViewModel.Ledger.Purchase_Tra> ModelPurMasterTra, long Sales_Id, int Id)
        {
            bool result = false;
            try
            {
                // List<Purchase_Master_Tra> TblPurMasterTra = new List<Purchase_Master_Tra>();
                List<Sales_Master_Tra> TblPurMasterTra = new List<Sales_Master_Tra>();
                Sales_Master_Tra tbltra;
                if (ModelPurMasterTra != null && Sales_Id != 0)
                {
                    string IDs = "";
                    foreach (var item in ModelPurMasterTra)
                    {
                        if (!IDs.Contains(item.Item_Id.ToString()))
                        {
                            IDs += item.Item_Id + ",";
                        }
                    }
                    IDs.Remove(IDs.LastIndexOf(','));

                    if (Id > 0)
                    {
                        var salestralist = _db.Sales_Master_Tra.Where(x => x.Sales_Id == Sales_Id).ToList();
                        // List<Purchase_Master_Tra> listtra = new List<Purchase_Master_Tra>();
                        Purchase_Master_Tra p_tra = new Purchase_Master_Tra();
                        foreach (var item in salestralist)
                        {
                            var PurchaseTra = _db.Purchase_Master_Tra.Where(x => x.Sale_Serial_No == item.Sale_Serial_No).FirstOrDefault();
                            p_tra.Id = PurchaseTra.Id;
                            p_tra.Is_Active = true;
                            p_tra.Is_Deleted = false;
                            p_tra.Qty += 1;

                            _db.SaveChanges();
                        }
                        //_db.Purchase_Master_Tra.AddRange(listtra);// I am at this line ...
                        //_db.SaveChanges();
                    }
                    var lst = _db.Purchase_Master_Tra.Where(x => IDs.Contains(x.Item_Id.ToString()) && x.Is_Active == true).ToList();
                    _db.Pr_DeleteStock_Item(1, "SALES");
                    //List<Purchase_Master_Tra> list =new List<Purchase_Master_Tra>() ;
                    int i = 0;
                    //  int itm_Id = 0;
                    foreach (var item in ModelPurMasterTra)
                    {
                        tbltra = new Sales_Master_Tra();

                        tbltra.Id = item.Id;
                        tbltra.Item_Id = item.Item_Id;
                        tbltra.Qty = item.Qty;
                        tbltra.Rate = item.Rate;
                        tbltra.Tax_Id = 0;
                        //  tbltra.Sales_VC_No = Challan_No;
                        tbltra.GoDown_Id = 1;

                        tbltra.Sale_Serial_No = lst.Where(x => x.Item_Id == item.Item_Id && x.Is_Active == true).FirstOrDefault().Sale_Serial_No;
                        foreach (var item1 in lst.Where(x => x.Sale_Serial_No == tbltra.Sale_Serial_No && x.Is_Active == true))
                        {
                            if (item1.Qty == 1 || item1.Qty == 0)
                            {
                                //lst.Remove(lst.Single(s => s.Id == item1.Id));
                                //lst.Where(w => w.Sale_Serial_No == tbltra.Sale_Serial_No).FirstOrDefault(k =>  k.Is_Active = false  );
                                //lst.Where(w => w.Sale_Serial_No == tbltra.Sale_Serial_No).FirstOrDefault(s => s.Qty=Convert.ToDecimal(item1.Qty));
                                var dd = lst.Where(w => w.Sale_Serial_No == tbltra.Sale_Serial_No).FirstOrDefault();

                                if (dd != null)
                                {
                                    dd.Qty -= 1;
                                    dd.Is_Active = false;

                                }
                            }
                            else
                            {
                                item1.Qty -= 1;

                                if (item1.Qty == 0)
                                {
                                    lst.Where(w => w.Sale_Serial_No == tbltra.Sale_Serial_No).FirstOrDefault(k => k.Is_Active = false);
                                    lst.Where(w => w.Sale_Serial_No == tbltra.Sale_Serial_No).Select(s => { s.Qty = s.Qty - 1; return s; });
                                }
                                //lst.Where(w=> w.Id == item1.Id).ToList().ForEach(k => k.Qty = (k.Qty- 1));
                                lst.Where(w => w.Sale_Serial_No == tbltra.Sale_Serial_No).ToList().ForEach(k => k.Qty = (k.Qty - 1));
                            }
                            break;
                        }


                        tbltra.Created_By = 1;
                        tbltra.Created_Date = DateTime.Now;
                        tbltra.Is_Active = true;
                        tbltra.Is_Deleted = false;
                        tbltra.Modified_By = 1;
                        tbltra.Modified_Date = DateTime.Now;
                        tbltra.Modified_By = 1;
                        TblPurMasterTra.Add(tbltra);
                        i++;
                    }
                    _db.Sales_Master_Tra.AddRange(TblPurMasterTra);
                    _db.SaveChanges();

                    foreach (var item in lst)
                    {
                        if (item.Is_Active == false)
                        {
                            var Purchase_tra = _db.Purchase_Master_Tra.Where(x => x.Id == item.Id).FirstOrDefault();
                            Purchase_tra.Is_Active = false;
                            Purchase_tra.Is_Deleted = true;
                        }
                        else
                        {
                            var Purchase_tra = _db.Purchase_Master_Tra.Where(x => x.Id == item.Id).FirstOrDefault();
                            Purchase_tra.Qty = item.Qty;

                        }

                        _db.SaveChanges();
                    }
                }
            }
            catch
            {

                result = false;
            }
            return result;
        }
        public ViewModel.Ledger.SalesMaster Find(long Id = 0)
        {
            ViewModel.Ledger.SalesMaster ModelPurMaster = new SalesMaster();
            if (Id != 0)
            {

                Sales_Master tblPurMaster = _db.Sales_Master.Find(Id);
                ModelPurMaster.Id = tblPurMaster.Id;
                ModelPurMaster.Challan_Number = tblPurMaster.Sales_VC_No;
                ModelPurMaster.Company_Id = tblPurMaster.Company_Id;
                ModelPurMaster.Discount = tblPurMaster.Discount_Amount ?? 0.00m;
                ModelPurMaster.Finance_Id = tblPurMaster.Fncl_Year_Id;
                ModelPurMaster.Grand_Total = tblPurMaster.Total_Amount ?? 0.00m;
                ModelPurMaster.Is_Quotation = tblPurMaster.Is_Refered;
                ModelPurMaster.Quotation_Id = tblPurMaster.Refered_Id ?? 0;
                ModelPurMaster.Remarks = tblPurMaster.Remarks;
                ModelPurMaster.Customer_Id = tblPurMaster.Customer_Id;
                ModelPurMaster.Purchase_Date = tblPurMaster.DATE;
                ModelPurMaster.Supplier_Name = tblPurMaster.Company_Id != 0 ? _db.Ledger_Master.SingleOrDefault(x => x.Id == tblPurMaster.Company_Id).Name : "";

            }
            return ModelPurMaster;
        }
        public bool CreateStockMaster(List<ViewModel.Ledger.Sales_Tra> ModelSalTra)
        {
            List<Stock_Master> lstStockMaster = new List<Stock_Master>();
            // tblStockMaster.Id = ModelStock.Id;
            Stock_Master tblStockMaster;
            bool result = false;
            bool existsItem = false;
            foreach (var item in ModelSalTra)
            {
                existsItem = _db.Stock_Master.Any(m => m.Item_Id == item.Item_Id);
                if (existsItem == true)
                {
                    tblStockMaster = _db.Stock_Master.SingleOrDefault(x => x.Item_Id == item.Item_Id);
                    tblStockMaster.Qty = tblStockMaster.Qty - item.Qty;
                    // tblStockMaster.Rate = item.Rate;
                }


            }
            //  _db.Stock_Master.AddRange(lstStockMaster);
            _db.SaveChanges();
            result = true;
            return result;
        }
        //private bool Stock_MasterUpdate(List<ViewModel.Ledger.Purchase_Tra> ModelSalTra)
        //{
        //    System.Data.DataTable tbl = new System.Data.DataTable("Product");
        //    System.Data.DataColumn[] columns = new System.Data.DataColumn[4];
        //    columns[0] = new System.Data.DataColumn("Item_Id", typeof(int));
        //    columns[1] = new System.Data.DataColumn("Qty", typeof(decimal));
        //    columns[2] = new System.Data.DataColumn("Rate", typeof(decimal));
        //    columns[3] = new System.Data.DataColumn("Status", typeof(bool));
        //    tbl.Columns.AddRange(columns);

        //    foreach (var item in ModelSalTra)
        //    {
        //        var row = tbl.NewRow();

        //        row["Item_Id"] = item.Item_Id;
        //        row["Qty"] = item.Qty;
        //        row["Status"] = false;
        //        row["Rate"] = item.Rate;
        //        tbl.Rows.Add(row);
        //    }




        //    var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
        //    var regularConnectionString = builder.ProviderConnectionString;
        //    using (SqlConnection connection = new SqlConnection(regularConnectionString))
        //    {
        //        connection.Open();

        //        SqlCommand command = new SqlCommand("", connection);

        //        command.CommandText = "Pr_UpdateStockMaster";
        //        command.Parameters.Clear();
        //        command.CommandType = CommandType.StoredProcedure;
        //        // command.Parameters.AddWithValue("@someIdForParameter", someId);
        //        command.Parameters.AddWithValue("@datatable", tbl).SqlDbType = SqlDbType.Structured;
        //        command.ExecuteNonQuery();

        //        connection.Close();

        //        bool result = false;

        //        return result;
        //    };
        //}
        private bool UpdateSales_TraAndStock_master(List<ViewModel.Ledger.Purchase_Tra> ModelSalTra, long Sale_Id)
        {
            System.Data.DataTable tbl = new System.Data.DataTable("Product");
            System.Data.DataColumn[] columns = new System.Data.DataColumn[14];
            columns[0] = new System.Data.DataColumn("Item_Id", typeof(int));
            columns[1] = new System.Data.DataColumn("Qty", typeof(decimal));
            columns[2] = new System.Data.DataColumn("Rate", typeof(decimal));
            columns[3] = new System.Data.DataColumn("Sales_Id", typeof(long));
            //columns[3] = new System.Data.DataColumn("Challan_No", typeof(string));
            columns[4] = new System.Data.DataColumn("Godown_Id", typeof(int));
            columns[5] = new System.Data.DataColumn("SerialNo", typeof(string));
            columns[6] = new System.Data.DataColumn("Created_By", typeof(int));
            columns[7] = new System.Data.DataColumn("Created_Date", typeof(DateTime));
            columns[8] = new System.Data.DataColumn("Modified_By", typeof(int));
            columns[9] = new System.Data.DataColumn("Modified_Date", typeof(DateTime));
            columns[10] = new System.Data.DataColumn("Is_Active", typeof(bool));
            columns[11] = new System.Data.DataColumn("Is_Deleted", typeof(bool));

            tbl.Columns.AddRange(columns);

            foreach (var item in ModelSalTra)
            {
                var row = tbl.NewRow();

                row["Item_Id"] = item.Item_Id;
                row["Qty"] = item.Qty;
                row["Rate"] = item.Rate;
                row["Sales_Id"] = Sale_Id;
                //row["Challan_No"] = "";
                row["Godown_Id"] = item.GoDownId;
                row["SerialNo"] = item.SerialNo;
                row["Created_By"] = 1;
                row["Created_Date"] = DateTime.Now;
                row["Modified_By"] = 1;
                row["Modified_Date"] = DateTime.Now;
                row["Is_Active"] = true;
                row["Is_Deleted"] = false;

                tbl.Rows.Add(row);
            }

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["INVENTORY_DBEntities"].ConnectionString);
            var regularConnectionString = builder.ProviderConnectionString;
            using (SqlConnection connection = new SqlConnection(regularConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("", connection);

                // command.CommandText = "Pr_UpdateStockMaster"; 
                //command.CommandText = "Pr_UpdateStockMaster";
                command.CommandText = "Update_stock_and_Tra";
                command.Parameters.Clear();
                command.CommandType = CommandType.StoredProcedure;
                // command.Parameters.AddWithValue("@someIdForParameter", someId);

                command.Parameters.AddWithValue("@tblsales_tra", tbl).SqlDbType = SqlDbType.Structured;
                command.Parameters.AddWithValue("@tblStockMaster", null).SqlDbType = SqlDbType.Structured;
                command.Parameters.AddWithValue("@pur_Id", Sale_Id).SqlDbType = SqlDbType.BigInt;
                command.Parameters.AddWithValue("@Tag", "Sales").SqlDbType.ToString();
                command.ExecuteNonQuery();

                connection.Close();

                bool result = false;

                return result;
            };
        }
        public bool CreateSalesTaxDetail(List<ViewModel.Common.TaxDetails> taxdetails, long SalesId)
        {
            List<Purchase_Master_TaxDetails> listtax = new List<Purchase_Master_TaxDetails>();

            if (taxdetails != null)
            {
                _db.Pr_DeleteStock_Item(SalesId, "SALESTAXDETAILS");
                _db.Sales_Master_TaxDetails.AddRange(taxdetails.Select(x => new Sales_Master_TaxDetails
                {
                    Id = x.TId,
                    // Company_Id = CompanyId,
                    Sales_Id = SalesId,
                    Sales_VC_No = "",
                    Tax_Id = Convert.ToInt32(x.Id.Split('_')[0]),
                    Tax_Rate = Convert.ToDecimal(x.Rate_Type),
                    Tax_Amount = Convert.ToDecimal(x.Value),
                    Created_By = 1,
                    Created_Date = DateTime.Now,
                    Modified_By = 1,
                    Modified_Date = DateTime.Now,
                    Is_Active = true,
                    Is_Deleted = false
                }));

                //foreach (var x in taxdetails)
                //{
                //   // var lst = x.Id.Split('_');
                //    //var tid = _db.Purchase_Master.Max(p => p.Id);
                //    //var chlanno = _db.Purchase_Master.LastOrDefault(p=>p.Is_Active==true).Chalan_No;

                //    listtax.Add(new Purchase_Master_TaxDetails
                //    {
                //        Id= x.TId,
                //        Company_Id = _companyId,
                //        Purchase_Id =_db.Purchase_Master.Max(m=>m.Id),
                //        Purchase_Challan_no =  challan_No,
                //        Tax_Id = Convert.ToInt32(x.Id.Split('_')[0]),
                //        Tax_Rate = Convert.ToDecimal(x.Id.Split('_')[3]),
                //        Tax_Amount = Convert.ToDecimal(x.Value),
                //        Created_By = 1,
                //        Created_Date = DateTime.Now,
                //        Modified_By = 1,
                //        Modified_Date = DateTime.Now,
                //        Is_Deleted = false,
                //        Is_Active = true,
                //    });
                //}
            }

            // _db.Purchase_Master_TaxDetails.AddRange(listtax);
            _db.SaveChanges();
            return true;
        }
        public String GEN_ChallanNo()
        {
            string Challan_No = "";

            int countRows = _db.Sales_Master.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Sales_Master.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Sales_VC_No;
            if (!string.IsNullOrWhiteSpace(Challan_No))
            {
                Challan_No = Regex.Replace(Challan_No, @"\d+(?=\D*$)",
                   m => (Convert.ToInt64(m.Value) + 1).ToString());
            }
            else
            {
                Challan_No = "1".PadLeft(5, '0');
            }


            return Challan_No;
        }




        public bool Gen_ChallanOfSales(int Id)
        {
            bool result = false;
            try
            {
                var tblSalMaster = _db.Sales_Master.Find(Id);
                tblSalMaster.Is_Bill_Gen = true;
                tblSalMaster.Modified_Date = DateTime.Now;
                tblSalMaster.Modified_By = 1;
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
                    tblBilling.Discount_Amount = tblSalMaster.Discount_Amount;
                    tblBilling.Company_Id = tblSalMaster.Company_Id;
                    tblBilling.Billing_Amount = tblSalMaster.Total_Amount;
                    //  tblBilling.Chalan_No = tblSalMaster.Sales_VC_No;
                    tblBilling.Remarks = "After gen challan";
                    tblBilling.Tax_Amount = 0;
                    tblBilling.Transport = 0;
                    tblBilling.Amount = (tblBilling.Billing_Amount + tblBilling.Tax_Amount + tblBilling.Transport) - tblBilling.Discount_Amount;
                    tblBilling.Billing_Date = tblSalMaster.DATE;
                    tblBilling.User_Id = tblSalMaster.Customer_Id;
                    tblBilling.Tag_Type = "CUSTOMER";
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
                Sales_Return TblPurReturn = new Sales_Return();
                //  TblPurMaster.Id = ModelPurMaster.Id;
                TblPurReturn.Fncl_Year_Id = 1;
                //TblPurReturn = 1;

                //TblPurReturn.p = ModelPurMaster.FirstOrDefault().Challan_Number;
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
                //TblPurReturn.pur = ModelPurMaster.FirstOrDefault().SupplierId;
                TblPurReturn.Total_Amount = ModelPurMaster.FirstOrDefault().Grand_Total;
                TblPurReturn.Discount_Amount = ModelPurMaster.FirstOrDefault().Discount != 0 ? ModelPurMaster.FirstOrDefault().Discount : 0.0m;
                TblPurReturn.Is_Deleted = false;
                TblPurReturn.Is_Active = true;
                TblPurReturn.Created_By = 1;
                TblPurReturn.Modified_By = 1;

                TblPurReturn.Created_Date = System.DateTime.Now;
                TblPurReturn.Modified_Date = System.DateTime.Now;
                var status = false;
                _db.Sales_Return.Add(TblPurReturn);
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


        public bool ReturnPurchaseTra(List<ViewModel.Ledger.Challan_Details> ModelPurMasterTra, long Pur_Id)
        {
            bool result = false;
            try
            {
                List<Sales_Return_Tra> TblPurMasterTra = new List<Sales_Return_Tra>();
                Sales_Return_Tra tbltra;
                if (ModelPurMasterTra != null && Pur_Id != 0)
                {
                    _db.Pr_DeleteStock_Item(Pur_Id, "");
                    foreach (var item in ModelPurMasterTra)
                    {
                        if (item.ReturnQuantity != 0)
                        {
                            tbltra = new Sales_Return_Tra();

                            tbltra.Id = item.Id;
                            tbltra.Item_Id = item.ItemId;
                            tbltra.Qty = item.ReturnQuantity;
                            tbltra.Rate = item.Rate;
                            // tbltra.Tax_Id = item.Tax;
                            // tbltra.Chalan_No = Challan_No;

                            tbltra.Created_By = 1;
                            tbltra.Created_Date = DateTime.Now;
                            tbltra.Is_Active = true;
                            tbltra.Is_Deleted = false;
                            tbltra.Modified_By = 1;
                            tbltra.Modified_Date = DateTime.Now;


                            tbltra.Modified_By = 1;
                            TblPurMasterTra.Add(tbltra);
                        }
                    }
                    _db.Sales_Return_Tra.AddRange(TblPurMasterTra);
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
                    findData.Qty = findData.Qty + ModelPurMaster[i].ReturnQuantity;

                    _db.SaveChanges();
                }
            }

            return true;
        }
        public List<ViewModel.Common.TaxDetails> GetTaxList(long Sales_Id)
        {
            var list = _db.Sales_Master_TaxDetails.Where(x => x.Sales_Id == Sales_Id).ToList();
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

        public int DeleteSalesMaster(int ID)
        {
            var data = _db.Sales_Master.Find(ID);
            data.Is_Active = false;
            data.Is_Deleted = true;
            return _db.SaveChanges();
        }
        //public List<string> GetSerial_No(int Id, int Qty, string SrNo)
        //{
        //    return _db.Purchase_Master_Tra.Where(x => x.Item_Id == Id && (SrNo == "" || !SrNo.Contains(x.Sale_Serial_No))).OrderBy(x=>x.Sale_Serial_No).Select(x => x.Sale_Serial_No).Take(Qty).ToList();
        //}
        public List<string> GetSerial_No(int Id, int Qty, string SrNo)
        {
            var lst = _db.Purchase_Master_Tra.Where(x => x.Item_Id == Id && (SrNo == "" || !SrNo.Contains(x.Sale_Serial_No)) && x.Is_Active == true).OrderBy(x => x.Sale_Serial_No).Select(x => x.Sale_Serial_No).ToList();
            string temp;
            dynamic p = null;
            for (int i = 0; i < lst.Count(); i++)
            {
                for (int j = i + 1; j < lst.Count(); j++)
                {
                    p = Convert.ToInt32(lst[i].Split('-')[1]);
                    p = Convert.ToInt32(lst[j].Split('-')[1]);
                    if (Convert.ToInt32(lst[i].Split('-')[1]) > Convert.ToInt32(lst[j].Split('-')[1]))
                    {
                        temp = lst[i];
                        lst[i] = lst[j];
                        lst[j] = temp;
                    }
                }

            }

            return lst.Take(Qty).ToList();
        }
    }
}
