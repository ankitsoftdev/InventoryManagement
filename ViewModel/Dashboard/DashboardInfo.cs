using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Dashboard
{
    public class DashboardInfo
    {
        public string Months { get; set; }
        public decimal TotalPurchase { get; set; }
        public decimal TotalSale { get; set; }
    }


    public class PieGraphInfo
    {
        public string Name { get; set; }
        public int Total { get; set; }
    }

    public class SalerOrderinfo
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
        public string Contact_No { get; set; }
        public string Email_Id { get; set; }
        public string Order_Date { get; set; }
        public string Order_No { get; set; }
        public string Request_Delivery_Date { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int Sales_Person { get; set; }
    }

    public class ManageStockInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Item_Code { get; set; }
        public decimal Min_Qty { get; set; }
        public decimal Total_Qty { get; set; }
        public decimal Selling_Rate { get; set; }
        public decimal Purchase_Rate { get; set; }
        public string ManageStock { get; set; }
        public string Status { get; set; }
    }
    public class Purchaseinfo
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Supplier_Id { get; set; }
        public string Supplier_Name { get; set; }
        public string Address { get; set; }
        public string Contact_No { get; set; }
        public string Email_Id { get; set; }
        public System.DateTime Order_Date { get; set; }
        public string Order_No { get; set; }
        public System.DateTime Request_Delivery_Date { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int Sales_Person { get; set; }
    }
}
