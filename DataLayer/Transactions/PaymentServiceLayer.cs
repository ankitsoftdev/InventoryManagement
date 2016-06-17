using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Transactions;
using DataLayer.Transactions;
using ServiceLayer.Common;
namespace ServiceLayer.Transactions
{
    public class PaymentServiceLayer:CommonServiceLayer
    {

        PaymentDbLayer _db;
        public PaymentServiceLayer()
        {
            _db = new PaymentDbLayer();
            
        }
        public PaymentServiceLayer(string ConnectionString)
            : base(ConnectionString)
        {
   _db = new PaymentDbLayer(ConnectionString);
        }
        public List<ViewModel.Transactions.PaymentInfo> List(String Tag)
        {
            return _db.List(Tag);
        }
        public bool Create(PaymentInfo modelPayment)
        {
            return _db.Create(modelPayment);
        }
        public bool Update(PaymentInfo modelPayment)
        {
            return _db.Update(modelPayment);
        }
        public ViewModel.Transactions.PaymentInfo Find(int Id)
        {
            return _db.Find(Id);
        }
        
        public List<ViewModel.Transactions.PaymentDetails> PaymentDetails(int UserId, string Tag)
        {
            return _db.PaymentDetails(UserId, Tag);

        }
        public List<ViewModel.Transactions.BillingDetails> BillDetails(int UserId, string Tag)
        {
            return _db.BillDetails(UserId, Tag);
        }
        public List<BillList> PendingBillList(int Supplier_Id,int Id)
        {
            return _db.PendingBillList(Supplier_Id,Id);
        }
        public decimal GetBalance(int UserId)
        {
            return _db.GetBalance(UserId);
        }

        public bool GetChequeDetails(int Id=0,string Cheque_No="")
        {
          return  _db.GetChequeDetails(Id,Cheque_No);
        }
    }
}
