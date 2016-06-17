using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Transactions;
using ViewModel.Transactions;
namespace ServiceLayer.Transactions
{
   public class SalesOrderServiceLayer
    {
       SalesOrderDbLayer _db;
       public SalesOrderServiceLayer()
       {
           _db = new SalesOrderDbLayer();
       }
       public SalesOrderServiceLayer(string ConnectionString)
       {
  _db = new SalesOrderDbLayer(ConnectionString);
       }
       public List<SalesOrdeList> List()
       {
           return _db.List();
       }
       public bool Create(SalesOrderInfo modelSalesOrder)
       {
           return _db.Create(modelSalesOrder);
       }
       public bool Update(SalesOrderInfo modelSalesOrder)
       {
           return _db.Update(modelSalesOrder);
       }
       public SalesOrderInfo Find(long Id)
       {
           return _db.Find(Id);
       }
       public SalesOrderInfo FindByQuationId(long QuationId)
       {
           SalesOrderInfo modelSalesOrder = new SalesOrderInfo();
           if (QuationId != 0)
           {
               var tblquation = new QuationServiceLayer().Find(QuationId);
               if (tblquation != null)
               {
                   var tblCustomer = new AccountMaster.CustomerServicesLayer().FindCustomer(tblquation.Customer_Id, "CUSTOMERS");
                   if (tblCustomer != null)
                   {
                       modelSalesOrder.Quation_Id = QuationId;
                       modelSalesOrder.Customer_Name = tblCustomer.Name;
                       modelSalesOrder.Customer_Id = tblquation.Customer_Id;
                       modelSalesOrder.Email = tblCustomer.Address.ContactInfo.Email_Id;
                       modelSalesOrder.Contact_No = tblCustomer.Address.ContactInfo.Mobile;
                       modelSalesOrder.Posting_Date = DateTime.Now;
                       modelSalesOrder.Request_Delivery_Date = DateTime.Now;
                       modelSalesOrder.Order_Date = DateTime.Now;
                       modelSalesOrder.Document_Date = DateTime.Now;
                       modelSalesOrder.Order_No = _db.GEN_OrderNo();
                   }
               }
           }
                return modelSalesOrder;
       }
       public List<ViewModel.Transactions.SalesOrderInfo_Tra> OrderDetailsByQuationId(int QuationId)
       {
           return _db.OrderDetailsByQuationId(QuationId);
       }
       public List<ViewModel.Transactions.SalesOrderInfo_Tra> OrderDetails(long OrderNo)
       {
           return _db.OrderDetails(OrderNo);
       }
       public bool Delete(int Id)
       {
           return _db.Delete(Id);
       }
       public String GEN_OrderNo()
       {
           return _db.GEN_OrderNo();
       }
       public bool CreateByQuation(SalesOrderInfo modelSalesOrder)
       {
           bool result = false;
           result = _db.Create(modelSalesOrder); 
           if(result==true)
           {
               result = new QuationServiceLayer().ApproveQuation(modelSalesOrder.Quation_Id);
           }
           return result;
       }
      
       //public SalesOrderInfo FindByQuationId(int Quation_Id)
       //{
       //    return _db.FindByQuationId(Quation_Id);
       //}
    }
}
