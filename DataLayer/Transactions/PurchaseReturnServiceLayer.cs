using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
using ViewModel.Transactions;
using DataLayer.Transactions;
namespace ServiceLayer.Transactions
{
    public class PurchaseReturnServiceLayer
    {
        PurchaseReturnDbLayer _db;
        public PurchaseReturnServiceLayer()
        {
            _db = new PurchaseReturnDbLayer();
        }
        public PurchaseReturnServiceLayer(string ConnectionString)
        {
            _db = new PurchaseReturnDbLayer(ConnectionString);
        }
        public List<PurchaseReturnList> List(string search="")
        {
            return _db.List(search);
        }
        public bool Create(PurchaseReturnInfo ModelPurRet)
        {
            return _db.Create(ModelPurRet);
        }
        public bool Update(PurchaseReturnInfo ModelPurRet)
        {
            return _db.Update(ModelPurRet);
        }
        public List<PurchaseReturnInfo_Tra> ReturnDetails(long Id)
        {
            return _db.ReturnDetails(Id);
        }
        public PurchaseReturnInfo Find(long Id)
        {
            return _db.Find(Id);
        }
        public List<DDLBind> GetBillList(long Supplier_Id)
        {
            return _db.GetBillList(Supplier_Id);
        }
        public String GEN_VC_No()
        {
            return _db.GEN_VC_No();
        }
        public bool CheckSerialNo(string SerialNo)
        {
            return _db.CheckSerialNo(SerialNo);
        }
        public bool Delete(long Id)
        {
            return _db.Delete(Id);
        }
    }


}
