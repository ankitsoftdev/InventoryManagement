using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Common;
using DataLayer.Transactions;
using ViewModel.Transactions;
namespace ServiceLayer.Transactions
{
  public  class ReceiptServiceLayer:CommonServiceLayer
    {

      ReceiptDbLayer _db;
      public ReceiptServiceLayer()
      {
          _db = new ReceiptDbLayer();
      }
      public ReceiptServiceLayer(string ConnectionString)
            : base(ConnectionString)
        {
            _db = new ReceiptDbLayer(ConnectionString);
        }
      public List<ReceiptInfo> List(String Tag)
      {
          return _db.List(Tag);
      }
      public bool Create(ReceiptInfo modelReceipt)
      {
          return _db.Create(modelReceipt);
      }
      public bool Update(ReceiptInfo modelReceipt)
      {
          return _db.Update(modelReceipt);
      }
      public ViewModel.Transactions.ReceiptInfo Find(int Id)
      {
          return _db.Find(Id);
      }

      public List<ViewModel.Transactions.ReceiptDetails> ReceiptDetails(int UserId, string Tag)
      {
          return _db.ReceiptDetails(UserId, Tag);

      }
      //public List<ViewModel.Transactions.BillingDetails> BillDetails(int UserId, string Tag)
      //{
      //    return _db.BillDetails(UserId, Tag);
      //}
      public List<BillList> PendingBillList(int Customer_Id, int Id)
      {
          return _db.PendingBillList(Customer_Id, Id);
      }
      public decimal GetBalance(int UserId)
      {
          return _db.GetBalance(UserId);
      }

      public bool GetChequeDetails(int Id = 0, string Cheque_No = "")
      {
          return _db.GetChequeDetails(Id, Cheque_No);
      }
    }
}
