using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using HDMEntity;
using DataLayer;
using DataLayer.Transactions;
using ViewModel.Ledger;
namespace ServiceLayer.Transactions
{
    public class SalesServiceLayer
    {
        SalesDbLayer _db;

        public SalesServiceLayer()
        {
            _db = new SalesDbLayer();
        }
        public SalesServiceLayer(string ConnectionString)
        {
            _db = new SalesDbLayer(ConnectionString);
        }
        public List<PurchaseList> List()
        {
            return _db.List();
        }
        public ViewModel.Ledger.SalesMaster Find(long Id = 0)
        {
            return _db.Find(Id);
        }
        public List<ViewModel.Ledger.Purchase_Tra> ItemDetails(long SalesId)
        {
            return _db.ItemDetails(SalesId);
        }
        public bool Create(ViewModel.Ledger.SalesMaster ModelSalMaster)
        {
            ModelSalMaster.Grand_Total = GetGrandTotal(ModelSalMaster.sales_tra.listpurchase_tra);
            return _db.Create(ModelSalMaster);
        }
        public bool Update(ViewModel.Ledger.SalesMaster ModelSalMaster)
        {
            ModelSalMaster.Grand_Total = GetGrandTotal(ModelSalMaster.sales_tra.listpurchase_tra);
            return _db.Update(ModelSalMaster);
        }
        public String GEN_ChallanNo()
        {
            return _db.GEN_ChallanNo();
        }
        public decimal GetGrandTotal(List<ViewModel.Ledger.Purchase_Tra> salTra)
        {
            decimal GrTotal = 00.0m;
            try
            {
                foreach (var item in salTra.Where(x => x.Status == true))
                {
                    GrTotal = GrTotal + (item.Qty * item.Rate);
                }
            }
            catch
            {

                GrTotal = 00.0m;
            }
            return GrTotal;
        }
        public List<Challan_Details> ViewDetails(int Id)
        {
            return _db.ViewDetails(Id);
        }
        public String GetUnitName(int Id)
        {
            ServiceLayer.Masters.UnitsServiceLayer _unitSL = new Masters.UnitsServiceLayer();
            string UnitName = "";
            if (Id != 0)
                UnitName = _unitSL.Find(Id).Name;
            return UnitName;
        }


        public bool Gen_ChallanOfSales(int Id)
        {
            return _db.Gen_ChallanOfSales(Id);
        }
        public bool PurchageReturn(List<ViewModel.Ledger.Challan_Details> ModelPurMaster)
        {
            bool res = false;
            res = _db.PurchageReturn(ModelPurMaster);

            //if(res==false)
            //{
            //    res = CreateXMl(ModelPurMaster.ItemDetails, ModelPurMaster.Challan_Number);
            //}
            return res;


        }
        public List<ViewModel.Common.TaxDetails> GetTaxList(long Sales_Id)
        {
            return _db.GetTaxList(Sales_Id);
        }
        public int DeleteSalesMaster(int ID)
        {
            return _db.DeleteSalesMaster(ID);
        }
        public List<string> GetSerial_No(int Id, int Qty, string SrNo)
        {
            return _db.GetSerial_No(Id, Qty, SrNo);
        }
    }
}
