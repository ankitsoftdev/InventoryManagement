using DataLayer.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Dashboard;

using DataLayer.Transactions;

namespace ServiceLayer.Dashboard
{
    public class DashboardSLayer : QuationDbLayer
    {

        DashboardDbLayer _db;

        //call constructor
        public DashboardSLayer():base()
        {
            _db = new DashboardDbLayer();
        }
        public DashboardSLayer(String ConnectionString)
            : base(ConnectionString)
        {
            _db = new DashboardDbLayer(ConnectionString);
        }
        public List<PieGraphInfo> PieGraph()
        {
           return  _db.PieGraph();
        }


        public List<DashboardInfo> SelesPurchaseGraph()
        {
            return _db.SelesPurchaseGraph().ToList();
        }


        //Get top 10 Quations
        public IEnumerable<ViewModel.Transactions.QuationList> QuationsList()
        {
          
             return  base.List().ToList();
            
        }

        public IEnumerable<ViewModel.Dashboard.SalerOrderinfo> SalesOrderList()
        {
            return _db.SalesOrderList().ToList();
        }

         //Get top 10 Purchase Order
        public IEnumerable<ViewModel.Dashboard.Purchaseinfo> PurchaseOrderList()
        {
            return _db.PurchaseOrderList().ToList();
        }

        public IEnumerable<ManageStockInfo> ManageStock()
        {
            return _db.ManageStock().ToList();
        }
    }
}
