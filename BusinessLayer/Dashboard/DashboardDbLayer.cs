using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Dashboard;
using DataLayer.Transactions;
namespace DataLayer.Dashboard
{
    public class DashboardDbLayer
    {

        INVENTORY_DBEntities _db;

        /// <summary>
        /// This layer is used to display data on dashboard panel
        /// </summary>
        /// 

        private int _companyId { get; set; }
        
        public DashboardDbLayer()
        {
              _db = new INVENTORY_DBEntities();
            
        }
        public DashboardDbLayer(String ConnectionString)
        {  
            this._companyId = 1;
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }



        public List<PieGraphInfo> PieGraph()
        {
            List<PieGraphInfo> list = new List<PieGraphInfo>();
            return _db.Pr_DashOrderQuats(_companyId).Select(_data => new PieGraphInfo { Name = _data.Name, Total = _data.Total }).ToList();
           // return list;
        }

        public List<DashboardInfo> SelesPurchaseGraph()
        {
       


            return _db.SP_DashSalePurc(1, DateTime.Today, DateTime.Today).Select(_data => new DashboardInfo { Months = _data.Months, TotalPurchase = _data.TotalPurchase, TotalSale = _data.TotalSale }).ToList();
        }


       

        //Get Top 10 Sales order list
        public IEnumerable<ViewModel.Dashboard.SalerOrderinfo> SalesOrderList(int No_of_Records=10)
        {
            List<SalerOrderinfo> list = new List<SalerOrderinfo>();

            var lst = _db.Pr_Sales_Order_List(_companyId).ToList();
            lst.ForEach(_data=>list.Add(new ViewModel.Dashboard.SalerOrderinfo { Customer_Name = _data.Customer_Name, Contact_No = _data.Contact_No, Order_No = _data.Order_No, Order_Date = _data.Order_Date.ToShortDateString(), Request_Delivery_Date = _data.Request_Delivery_Date.ToShortDateString(), Status = _data.Status, Amount = _data.Amount }));
           
            return list;
        }


        //Get top 10 Purchase Order
        public IEnumerable<Purchaseinfo> PurchaseOrderList(int No_of_Records=10)
        {
            List<Purchaseinfo> lst = new List<ViewModel.Dashboard.Purchaseinfo>();

            var list = _db.Pr_Purchase_Order_List(_companyId).Take(No_of_Records).ToList();
            list.ForEach(_data=>lst.Add(new ViewModel.Dashboard.Purchaseinfo { Supplier_Name = _data.Customer_Name, Contact_No = _data.Contact_No, Order_No = _data.Order_No, Order_Date = _data.Order_Date, Request_Delivery_Date = _data.Request_Delivery_Date, Status = _data.Status, Amount = _data.Amount }));
            return lst;
        }


        //Manage Stock List
        public IEnumerable<ManageStockInfo> ManageStock()
        {
            List<ManageStockInfo> list = new List<ManageStockInfo>();
            
            return _db.SP_ManageStock(_companyId).Take(10).Select(m => new ManageStockInfo { Id = m.Id, Name = m.Name, Item_Code = m.Item_Code, Min_Qty = m.Min_Qty, Total_Qty = m.Total_Qty, Selling_Rate = m.Selling_Rate, Purchase_Rate = m.Purchase_Rate, ManageStock = m.ManageStock, Status = m.Status }).ToList();
           // return list;
        }

    }
}
