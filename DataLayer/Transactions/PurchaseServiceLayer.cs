using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Transactions;
using ViewModel.Ledger;
using System.Xml.Linq;
using ViewModel.Transactions;

namespace ServiceLayer.Transactions
{
    public   class PurchaseServiceLayer
    {

             PurchaseDbLayer _db;

            public PurchaseServiceLayer()
            {
                _db = new PurchaseDbLayer();
            }
            public PurchaseServiceLayer(string ConnectionString)
            {
 _db = new PurchaseDbLayer(ConnectionString);
            }
            public List<PurchaseList> List()
            {
                return _db.List();
            }
            public int DeletePurchase(int Id)
            {
                return _db.DeletePurchase(Id);
            }
            public List<Challan_Details> ViewDetails(int Id, string Challan_No)
            {
                return _db.ViewDetails(Id, Challan_No);
            }
            public ViewModel.Ledger.PurchaseMaster Find(long Id = 0)
            {
                return _db.Find(Id);
            }
            public List<ViewModel.Ledger.Purchase_Tra> ItemDetails(long Challan_No)
            {
                return _db.ItemDetails(Challan_No);
            }
            public bool Create(ViewModel.Ledger.PurchaseMaster ModelPurMaster)
            {
                bool res = false;
                ModelPurMaster.Grand_Total = GetGrandTotal(ModelPurMaster.ItemDetails);
                 res=  _db.Create(ModelPurMaster);
                
                //if(res==false)
                //{
                //    res = CreateXMl(ModelPurMaster.ItemDetails, ModelPurMaster.Challan_Number);
                //}
                return res;


            }
            public decimal GetGrandTotal(List<ViewModel.Ledger.Purchase_Tra> purTra)
            {
                decimal GrTotal = 00.0m;
                try
                {
                    foreach (var item in purTra.Where(x=>x.Status==true))
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
            private bool CreateXMl(List<ViewModel.Ledger.Purchase_Tra> purTra,string Challan_No)
            {
                bool result = false;
                var xmlfromLINQ = new XElement("Items",

               from c in purTra.Where(x => x.Status == true)
               select new XElement("Item",
                   new XElement("Item_Id", c.Id),
                    new XElement("Challan_No", Challan_No),
                     new XElement("Qty", c.Qty),
                    new XElement("Rate", c.Rate),
                    new XElement("Selling_Rate", c.Rate),
                   new XElement("Is_Active", true),
                    new XElement("Is_Deleted",true),
                    new XElement("Created_Date", DateTime.Today),
                   new XElement("Modified_Date", DateTime.Today),
                      new XElement("Created_By",1),
                   new XElement("Modified_By", 1)

                   ));
              //  result = _db.ManageStockMaster(xmlfromLINQ.ToString(), Challan_No);
                return result;
            }
            public bool Update(ViewModel.Ledger.PurchaseMaster ModelPurMaster)
            {

                ModelPurMaster.Grand_Total = GetGrandTotal(ModelPurMaster.purchase_tra.listpurchase_tra);
                return _db.Update(ModelPurMaster);
            }
            
            public String GEN_ChallanNo()
            {
                return _db.GEN_ChallanNo();
            }
            public String GetUnitName(int Id)
            {
                ServiceLayer.Masters.UnitsServiceLayer _unitSL = new Masters.UnitsServiceLayer();
                string UnitName = "";
                if(Id!=0)
                UnitName= _unitSL.Find(Id).Name;
                return UnitName;
            }
            public bool Gen_ChallanOfPurchse(int Id)
            {
                return _db.Gen_ChallanOfPurchse(Id);
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
            public List<ViewModel.Common.TaxDetails> GetTaxList(long Pur_Id)
            {
                return _db.GetTaxList(Pur_Id);
            }
            public PurchaseOrderItem GetPurchaseOrderItemList(PurchaseOrderInfo info)
            {
                PurchaseOrderItem orderItem = new PurchaseOrderItem();
                long itemId = 0;
                var maxId = "";
                bool itemType;
                int total;
                foreach (var item in info.OrderDetails)
                {
                    itemId = item.Item_Id;
                    maxId = new ServiceLayer.InventoryMaster.StockServiceLayer().GEN_MAXId(itemId);
                    itemType = new ServiceLayer.InventoryMaster.StockServiceLayer().GetItemType(itemId);

                    if (itemType == false)
                    {
                        for (int i = 0; i < item.Oreder_Quantity; i++)
                        {
                            orderItem.listPurchaseOrderItem.Add(new PurchaseOrderItem
                            {
                                ItemId = item.Item_Id,
                                ItemName = item.Item_Name,
                                SerialNo = maxId.Contains('-') ? maxId.Split('-')[0] + "-" + (Convert.ToInt32(maxId.Split('-')[1]) + (i + 1)) : maxId + "-" + (i + 1),
                                ProductCode = maxId.Contains('-') ? maxId.Split('-')[0] + "-" + (Convert.ToInt32(maxId.Split('-')[1]) + (i + 1)) : maxId + "-" + (i + 1),
                                Quantity = 1,
                                Rate = item.Rate ?? 0,
                                UnitName = item.Unit_Name
                            });

                        }
                        orderItem.orderId = info.Id;
                        orderItem.SupplierId = info.Supplier_Id;
                        orderItem.TotalAMount = info.OrderDetails.Select(x => x.Amount).Sum() ?? 0;
                    }
                    else
                    {
                        orderItem.listPurchaseOrderItem.Add(new PurchaseOrderItem
                        {
                            ItemId = item.Item_Id,
                            ItemName = item.Item_Name,
                            SerialNo = maxId.Contains('-') ? maxId.Split('-')[0] + "-" + (Convert.ToInt32(maxId.Split('-')[1]) + 1) : maxId + "-" + 1,
                            ProductCode = maxId.Contains('-') ? maxId.Split('-')[0] + "-" + (Convert.ToInt32(maxId.Split('-')[1]) + 1) : maxId + "-" + 1,
                            Quantity = item.Oreder_Quantity ?? 0,
                            Rate = item.Rate ?? 0,
                            TotalAMount = item.Rate * item.Oreder_Quantity ?? 0,
                        });
                        orderItem.orderId = info.Id;
                    }

                }
                return orderItem;
            }
            public PurchaseMaster BindOrderItemWithPurchaseMaster(PurchaseOrderItem OrderItem)
            {
                PurchaseMaster purchase = new PurchaseMaster();
                foreach (var item in OrderItem.listPurchaseOrderItem)
                {
                    purchase.ItemDetails.Add(new Purchase_Tra
                    {
                        Item_Id = item.ItemId,
                        ItemName = item.ItemName,
                        ProductCode = item.ProductCode,
                        SerialNo = item.SerialNo,
                        Qty = item.Quantity,
                        Rate = item.Rate
                    });
                }
                purchase.Grand_Total = OrderItem.TotalAMount;

                return purchase;
            }
    }
}
