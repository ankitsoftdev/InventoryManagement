﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class INVENTORY_DBEntities : DbContext
    {
        public INVENTORY_DBEntities()
            : base("name=INVENTORY_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Billing_Info> Billing_Info { get; set; }
        public virtual DbSet<Branch_Master> Branch_Master { get; set; }
        public virtual DbSet<City_Master> City_Master { get; set; }
        public virtual DbSet<Company_Master> Company_Master { get; set; }
        public virtual DbSet<Contact_Info> Contact_Info { get; set; }
        public virtual DbSet<Country_State> Country_State { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Delivery_Note> Delivery_Note { get; set; }
        public virtual DbSet<Delivery_Note_Tra> Delivery_Note_Tra { get; set; }
        public virtual DbSet<Emp_Common_Master> Emp_Common_Master { get; set; }
        public virtual DbSet<FinancialYear> FinancialYears { get; set; }
        public virtual DbSet<GoDown_Master> GoDown_Master { get; set; }
        public virtual DbSet<GoDown_Region> GoDown_Region { get; set; }
        public virtual DbSet<Ledger_Group> Ledger_Group { get; set; }
        public virtual DbSet<Ledger_Master> Ledger_Master { get; set; }
        public virtual DbSet<LedgerGroupTemp1> LedgerGroupTemp1 { get; set; }
        public virtual DbSet<Login_Master> Login_Master { get; set; }
        public virtual DbSet<Payment_Info> Payment_Info { get; set; }
        public virtual DbSet<Purchase_Order> Purchase_Order { get; set; }
        public virtual DbSet<Purchase_Order_Tra> Purchase_Order_Tra { get; set; }
        public virtual DbSet<Purchase_Return> Purchase_Return { get; set; }
        public virtual DbSet<Purchase_Return_Tra> Purchase_Return_Tra { get; set; }
        public virtual DbSet<Quation_Master> Quation_Master { get; set; }
        public virtual DbSet<Quation_Master_Tra> Quation_Master_Tra { get; set; }
        public virtual DbSet<Receipt_Note> Receipt_Note { get; set; }
        public virtual DbSet<Role_Master> Role_Master { get; set; }
        public virtual DbSet<Sales_Order> Sales_Order { get; set; }
        public virtual DbSet<Sales_Order_Tra> Sales_Order_Tra { get; set; }
        public virtual DbSet<Sales_Return> Sales_Return { get; set; }
        public virtual DbSet<Sales_Return_Tra> Sales_Return_Tra { get; set; }
        public virtual DbSet<Stock_Category> Stock_Category { get; set; }
        public virtual DbSet<Stock_Group> Stock_Group { get; set; }
        public virtual DbSet<Stock_Item> Stock_Item { get; set; }
        public virtual DbSet<Stock_Master> Stock_Master { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tagging_Master> Tagging_Master { get; set; }
        public virtual DbSet<Tagging_Master_Assign> Tagging_Master_Assign { get; set; }
        public virtual DbSet<Tagging_Master_Assign_Tra> Tagging_Master_Assign_Tra { get; set; }
        public virtual DbSet<Tax_Deduction_Master> Tax_Deduction_Master { get; set; }
        public virtual DbSet<Tax_Master> Tax_Master { get; set; }
        public virtual DbSet<UnitMaster> UnitMasters { get; set; }
        public virtual DbSet<Purchase_Master> Purchase_Master { get; set; }
        public virtual DbSet<Purchase_Master_Tra> Purchase_Master_Tra { get; set; }
        public virtual DbSet<Purchase_Master_TaxDetails> Purchase_Master_TaxDetails { get; set; }
        public virtual DbSet<Sales_Master_TaxDetails> Sales_Master_TaxDetails { get; set; }
        public virtual DbSet<Sales_Master_Tra> Sales_Master_Tra { get; set; }
        public virtual DbSet<Receipt_Note_Tra> Receipt_Note_Tra { get; set; }
        public virtual DbSet<Payment_Info_Tra> Payment_Info_Tra { get; set; }
        public virtual DbSet<Sales_Master> Sales_Master { get; set; }
        public virtual DbSet<Receipt_Info> Receipt_Info { get; set; }
        public virtual DbSet<Receipt_Info_Tra> Receipt_Info_Tra { get; set; }
        public virtual DbSet<Advance_Info> Advance_Info { get; set; }
    
        public virtual ObjectResult<GetdatafromPurchaseTra_Result> GetdatafromPurchaseTra()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetdatafromPurchaseTra_Result>("GetdatafromPurchaseTra");
        }
    
        public virtual ObjectResult<GetIItemlist_Result> GetIItemlist()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetIItemlist_Result>("GetIItemlist");
        }
    
        [DbFunction("INVENTORY_DBEntities", "GetPurchaseTra")]
        public virtual IQueryable<GetPurchaseTra_Result> GetPurchaseTra(Nullable<int> ordQty, Nullable<int> itemId)
        {
            var ordQtyParameter = ordQty.HasValue ?
                new ObjectParameter("OrdQty", ordQty) :
                new ObjectParameter("OrdQty", typeof(int));
    
            var itemIdParameter = itemId.HasValue ?
                new ObjectParameter("ItemId", itemId) :
                new ObjectParameter("ItemId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPurchaseTra_Result>("[INVENTORY_DBEntities].[GetPurchaseTra](@OrdQty, @ItemId)", ordQtyParameter, itemIdParameter);
        }
    
        public virtual ObjectResult<GetUnitNameAndAmount_Result> GetUnitNameAndAmount(Nullable<int> itemId)
        {
            var itemIdParameter = itemId.HasValue ?
                new ObjectParameter("itemId", itemId) :
                new ObjectParameter("itemId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUnitNameAndAmount_Result>("GetUnitNameAndAmount", itemIdParameter);
        }
    
        public virtual int Pr_Challan_Details(Nullable<long> pur_Id, string tag)
        {
            var pur_IdParameter = pur_Id.HasValue ?
                new ObjectParameter("Pur_Id", pur_Id) :
                new ObjectParameter("Pur_Id", typeof(long));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_Challan_Details", pur_IdParameter, tagParameter);
        }
    
        public virtual ObjectResult<Pr_CommonList_Result> Pr_CommonList(Nullable<long> companyId, string tag)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(long));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_CommonList_Result>("Pr_CommonList", companyIdParameter, tagParameter);
        }
    
        public virtual ObjectResult<Pr_CreateDashBoardData_Result> Pr_CreateDashBoardData(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_CreateDashBoardData_Result>("Pr_CreateDashBoardData", companyIdParameter);
        }
    
        public virtual int Pr_DeleteCommonTblRcrd(Nullable<long> id, string tag)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(long));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_DeleteCommonTblRcrd", idParameter, tagParameter);
        }
    
        public virtual int Pr_DeleteStock_Item(Nullable<long> pur_Id, string tag)
        {
            var pur_IdParameter = pur_Id.HasValue ?
                new ObjectParameter("Pur_Id", pur_Id) :
                new ObjectParameter("Pur_Id", typeof(long));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_DeleteStock_Item", pur_IdParameter, tagParameter);
        }
    
        public virtual ObjectResult<Pr_Delivery_Note_Tra_Result> Pr_Delivery_Note_Tra(string tag, Nullable<long> orderNo, Nullable<int> companyId)
        {
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            var orderNoParameter = orderNo.HasValue ?
                new ObjectParameter("OrderNo", orderNo) :
                new ObjectParameter("OrderNo", typeof(long));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_Delivery_Note_Tra_Result>("Pr_Delivery_Note_Tra", tagParameter, orderNoParameter, companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_DeliveryNoteList_Result> Pr_DeliveryNoteList(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_DeliveryNoteList_Result>("Pr_DeliveryNoteList", companyIdParameter);
        }
    
        public virtual int Pr_PaymentList(Nullable<int> companyId, string tag)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_PaymentList", companyIdParameter, tagParameter);
        }
    
        public virtual ObjectResult<Pr_Purchase_Order_List_Result> Pr_Purchase_Order_List(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_Purchase_Order_List_Result>("Pr_Purchase_Order_List", companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_PurchaseList_Result> Pr_PurchaseList(Nullable<long> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_PurchaseList_Result>("Pr_PurchaseList", companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_Receipt_Note_Tra_Result> Pr_Receipt_Note_Tra(string tag, Nullable<long> pur_Id, Nullable<long> companyId)
        {
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            var pur_IdParameter = pur_Id.HasValue ?
                new ObjectParameter("Pur_Id", pur_Id) :
                new ObjectParameter("Pur_Id", typeof(long));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_Receipt_Note_Tra_Result>("Pr_Receipt_Note_Tra", tagParameter, pur_IdParameter, companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_ReceiptNoteList_Result> Pr_ReceiptNoteList(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_ReceiptNoteList_Result>("Pr_ReceiptNoteList", companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_RegionList_Result> Pr_RegionList(Nullable<long> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_RegionList_Result>("Pr_RegionList", companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_Sales_Order_List_Result> Pr_Sales_Order_List(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_Sales_Order_List_Result>("Pr_Sales_Order_List", companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_SalesList_Result> Pr_SalesList(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_SalesList_Result>("Pr_SalesList", companyIdParameter);
        }
    
        public virtual ObjectResult<Pr_StockMasterList_Result> Pr_StockMasterList(Nullable<long> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_StockMasterList_Result>("Pr_StockMasterList", companyIdParameter);
        }
    
        public virtual int Pr_UpdateStockMaster()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdateStockMaster");
        }
    
        public virtual ObjectResult<sp_Customerinfo_Result> sp_Customerinfo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Customerinfo_Result>("sp_Customerinfo");
        }
    
        public virtual ObjectResult<SP_DashSalePurc_Result> SP_DashSalePurc(Nullable<int> companyId, Nullable<System.DateTime> startDt, Nullable<System.DateTime> endDt)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            var startDtParameter = startDt.HasValue ?
                new ObjectParameter("StartDt", startDt) :
                new ObjectParameter("StartDt", typeof(System.DateTime));
    
            var endDtParameter = endDt.HasValue ?
                new ObjectParameter("EndDt", endDt) :
                new ObjectParameter("EndDt", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_DashSalePurc_Result>("SP_DashSalePurc", companyIdParameter, startDtParameter, endDtParameter);
        }
    
        public virtual ObjectResult<SP_Purchase_Order_List_Result> SP_Purchase_Order_List(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Purchase_Order_List_Result>("SP_Purchase_Order_List", companyIdParameter);
        }
    
        public virtual ObjectResult<Sp_PurchaseData_Result> Sp_PurchaseData(string pur_Id)
        {
            var pur_IdParameter = pur_Id != null ?
                new ObjectParameter("Pur_Id", pur_Id) :
                new ObjectParameter("Pur_Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_PurchaseData_Result>("Sp_PurchaseData", pur_IdParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> SP_SalesPurYearly(Nullable<int> company_Id, Nullable<System.DateTime> financialYrfrm, Nullable<System.DateTime> financialYrTo)
        {
            var company_IdParameter = company_Id.HasValue ?
                new ObjectParameter("Company_Id", company_Id) :
                new ObjectParameter("Company_Id", typeof(int));
    
            var financialYrfrmParameter = financialYrfrm.HasValue ?
                new ObjectParameter("FinancialYrfrm", financialYrfrm) :
                new ObjectParameter("FinancialYrfrm", typeof(System.DateTime));
    
            var financialYrToParameter = financialYrTo.HasValue ?
                new ObjectParameter("FinancialYrTo", financialYrTo) :
                new ObjectParameter("FinancialYrTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("SP_SalesPurYearly", company_IdParameter, financialYrfrmParameter, financialYrToParameter);
        }
    
        public virtual ObjectResult<Sp_StockItemInfo_Result> Sp_StockItemInfo(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_StockItemInfo_Result>("Sp_StockItemInfo", companyIdParameter);
        }
    
        public virtual ObjectResult<Sp_Supplierinfo_Result> Sp_Supplierinfo(string tagname, Nullable<int> companyId)
        {
            var tagnameParameter = tagname != null ?
                new ObjectParameter("Tagname", tagname) :
                new ObjectParameter("Tagname", typeof(string));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_Supplierinfo_Result>("Sp_Supplierinfo", tagnameParameter, companyIdParameter);
        }
    
        public virtual int UpdateQtyAndIsActive_PurchaseTra(Nullable<long> id, Nullable<decimal> qty, Nullable<bool> isActive, Nullable<bool> isDeleted)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(long));
    
            var qtyParameter = qty.HasValue ?
                new ObjectParameter("Qty", qty) :
                new ObjectParameter("Qty", typeof(decimal));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("isActive", isActive) :
                new ObjectParameter("isActive", typeof(bool));
    
            var isDeletedParameter = isDeleted.HasValue ?
                new ObjectParameter("isDeleted", isDeleted) :
                new ObjectParameter("isDeleted", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateQtyAndIsActive_PurchaseTra", idParameter, qtyParameter, isActiveParameter, isDeletedParameter);
        }
    
        public virtual int GetMaxId(Nullable<long> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetMaxId", idParameter);
        }
    
        public virtual ObjectResult<PR_GetPendingBillList_Result> PR_GetPendingBillList(Nullable<long> userId, Nullable<long> ptm_Id, string tagType, string modeType)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(long));
    
            var ptm_IdParameter = ptm_Id.HasValue ?
                new ObjectParameter("Ptm_Id", ptm_Id) :
                new ObjectParameter("Ptm_Id", typeof(long));
    
            var tagTypeParameter = tagType != null ?
                new ObjectParameter("TagType", tagType) :
                new ObjectParameter("TagType", typeof(string));
    
            var modeTypeParameter = modeType != null ?
                new ObjectParameter("ModeType", modeType) :
                new ObjectParameter("ModeType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PR_GetPendingBillList_Result>("PR_GetPendingBillList", userIdParameter, ptm_IdParameter, tagTypeParameter, modeTypeParameter);
        }
    
        public virtual int Pr_UpdateDeliveryNote(Nullable<long> delivery_Note_Id)
        {
            var delivery_Note_IdParameter = delivery_Note_Id.HasValue ?
                new ObjectParameter("Delivery_Note_Id", delivery_Note_Id) :
                new ObjectParameter("Delivery_Note_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdateDeliveryNote", delivery_Note_IdParameter);
        }
    
        public virtual int Pr_UpdatePayment(Nullable<long> pmt_Id)
        {
            var pmt_IdParameter = pmt_Id.HasValue ?
                new ObjectParameter("Pmt_Id", pmt_Id) :
                new ObjectParameter("Pmt_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdatePayment", pmt_IdParameter);
        }
    
        public virtual int Pr_UpdatePurchaseOrder(Nullable<long> pur_Order_Id)
        {
            var pur_Order_IdParameter = pur_Order_Id.HasValue ?
                new ObjectParameter("Pur_Order_Id", pur_Order_Id) :
                new ObjectParameter("Pur_Order_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdatePurchaseOrder", pur_Order_IdParameter);
        }
    
        public virtual int Pr_UpdateQuationkMaster(Nullable<long> quationId)
        {
            var quationIdParameter = quationId.HasValue ?
                new ObjectParameter("QuationId", quationId) :
                new ObjectParameter("QuationId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdateQuationkMaster", quationIdParameter);
        }
    
        public virtual int Pr_UpdateReceiptNote(Nullable<long> receipt_Note_Id)
        {
            var receipt_Note_IdParameter = receipt_Note_Id.HasValue ?
                new ObjectParameter("Receipt_Note_Id", receipt_Note_Id) :
                new ObjectParameter("Receipt_Note_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdateReceiptNote", receipt_Note_IdParameter);
        }
    
        public virtual int Pr_UpdateSalesOrder(Nullable<long> sales_Order_Id)
        {
            var sales_Order_IdParameter = sales_Order_Id.HasValue ?
                new ObjectParameter("Sales_Order_Id", sales_Order_Id) :
                new ObjectParameter("Sales_Order_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdateSalesOrder", sales_Order_IdParameter);
        }
    
        public virtual int sp_OpeningBalData(string tagType, Nullable<long> item_Id)
        {
            var tagTypeParameter = tagType != null ?
                new ObjectParameter("TagType", tagType) :
                new ObjectParameter("TagType", typeof(string));
    
            var item_IdParameter = item_Id.HasValue ?
                new ObjectParameter("Item_Id", item_Id) :
                new ObjectParameter("Item_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_OpeningBalData", tagTypeParameter, item_IdParameter);
        }
    
        public virtual int Update_stock_and_Tra(string pur_Id, string tag)
        {
            var pur_IdParameter = pur_Id != null ?
                new ObjectParameter("pur_Id", pur_Id) :
                new ObjectParameter("pur_Id", typeof(string));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_stock_and_Tra", pur_IdParameter, tagParameter);
        }
    
        public virtual int Pr_UpdateReceipt(Nullable<long> recpt_Id)
        {
            var recpt_IdParameter = recpt_Id.HasValue ?
                new ObjectParameter("Recpt_Id", recpt_Id) :
                new ObjectParameter("Recpt_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_UpdateReceipt", recpt_IdParameter);
        }
    
        public virtual int Pr_PurchaseReturnTra(Nullable<long> purRet_Id, string tag)
        {
            var purRet_IdParameter = purRet_Id.HasValue ?
                new ObjectParameter("PurRet_Id", purRet_Id) :
                new ObjectParameter("PurRet_Id", typeof(long));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_PurchaseReturnTra", purRet_IdParameter, tagParameter);
        }
    
        public virtual int Pr_SalesReturnTra(Nullable<long> salesRet_Id, string tag)
        {
            var salesRet_IdParameter = salesRet_Id.HasValue ?
                new ObjectParameter("SalesRet_Id", salesRet_Id) :
                new ObjectParameter("SalesRet_Id", typeof(long));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pr_SalesReturnTra", salesRet_IdParameter, tagParameter);
        }
    
        public virtual ObjectResult<Pr_GetPendingBal_Result> Pr_GetPendingBal(Nullable<long> userId, string tag)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(long));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_GetPendingBal_Result>("Pr_GetPendingBal", userIdParameter, tagParameter);
        }
    
        public virtual ObjectResult<Pr_DashOrderQuats_Result> Pr_DashOrderQuats(Nullable<long> company_Id)
        {
            var company_IdParameter = company_Id.HasValue ?
                new ObjectParameter("Company_Id", company_Id) :
                new ObjectParameter("Company_Id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_DashOrderQuats_Result>("Pr_DashOrderQuats", company_IdParameter);
        }
    
        public virtual ObjectResult<SP_ManageStock_Result> SP_ManageStock(Nullable<int> company_Id)
        {
            var company_IdParameter = company_Id.HasValue ?
                new ObjectParameter("Company_Id", company_Id) :
                new ObjectParameter("Company_Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ManageStock_Result>("SP_ManageStock", company_IdParameter);
        }
    
        public virtual ObjectResult<Pr_QuationList_Result> Pr_QuationList(Nullable<long> companyId, string tag_Type)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(long));
    
            var tag_TypeParameter = tag_Type != null ?
                new ObjectParameter("Tag_Type", tag_Type) :
                new ObjectParameter("Tag_Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pr_QuationList_Result>("Pr_QuationList", companyIdParameter, tag_TypeParameter);
        }
    }
}
