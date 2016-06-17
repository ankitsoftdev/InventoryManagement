using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using HDMEntity;
using ViewModel.Stock;
using ViewModel.Common;
using System.Text.RegularExpressions;
namespace DataLayer.InventoryMaster
{
    public class StockDbLayer
    {
        INVENTORY_DBEntities _db;
        public StockDbLayer()
        {
            _db = new INVENTORY_DBEntities();

        }
        public StockDbLayer(String ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public List<ViewModel.Stock.StockItem> GetStock()
        {
            return _db.Sp_StockItemInfo(1).Select(x => new ViewModel.Stock.StockItem
            {
                Id = x.Id,
                Item_Code = x.Code,
                Name = x.Name,
                Group_Id = x.Group_Id,
                Category_Id = x.Category_Id,
                Unit_Id = x.Unit_Id,
                GroupName = x.GroupName,
                CategoryName = x.CategoryName,
                UnitName = x.UnitName,
                Remarks = x.Remarks,
                Alias_Name = x.Alias_Name,
                Min_Qty = x.Min_Qty,
                Image_Path = x.Image_Url,
                Prefix_SerialNo = x.Prefix_SerialNo,
                Start_From = x.Start_From

            }).ToList();
        }
        public bool Create(ViewModel.Stock.StockItem modelstockItem)
        {
            bool result = false;
            try
            {

                Stock_Item tblstockItem = new Stock_Item();
                tblstockItem.Id = modelstockItem.Id;
                tblstockItem.Name = modelstockItem.Name;
                tblstockItem.Remarks = modelstockItem.Remarks;
                tblstockItem.Alias_Name = modelstockItem.Alias_Name;
                tblstockItem.Unit_Id = modelstockItem.Unit_Id;
                tblstockItem.Category_Id = modelstockItem.Category_Id;
                tblstockItem.Group_Id = modelstockItem.Group_Id;
                tblstockItem.Is_Auto = modelstockItem.Is_Auto_SerialNo;
                tblstockItem.Prefix_SerialNo = modelstockItem.Prefix_SerialNo;
                tblstockItem.Starting_SerialNo = modelstockItem.Start_From;
                tblstockItem.Min_Qty = modelstockItem.Min_Qty;
                tblstockItem.Sufix_SerialNo = modelstockItem.Sufix_SerialNo;
                tblstockItem.Company_Id = 1;
                tblstockItem.Is_Active = true;
                tblstockItem.Is_Deleted = false;
                tblstockItem.Item_Code = modelstockItem.Item_Code;
                tblstockItem.Modified_Date = DateTime.Now;
                tblstockItem.Modified_By = 1;
                tblstockItem.Created_By = 1;
                tblstockItem.Created_Date = DateTime.Now;
                tblstockItem.Image_Url = modelstockItem.Image_Path;

                result = Save(tblstockItem);

            }
            catch
            {

                result = false;
            }
            return result;

        }
        public bool Update(ViewModel.Stock.StockItem modelstockItem)
        {
            bool result = false;
            try
            {

                Stock_Item tblstockItem = _db.Stock_Item.Find(modelstockItem.Id);
                tblstockItem.Item_Code = modelstockItem.Item_Code;
                tblstockItem.Name = modelstockItem.Name;
                tblstockItem.Remarks = modelstockItem.Remarks;
                tblstockItem.Alias_Name = modelstockItem.Alias_Name;
                tblstockItem.Unit_Id = modelstockItem.Unit_Id;
                tblstockItem.Category_Id = modelstockItem.Category_Id;
                tblstockItem.Group_Id = modelstockItem.Group_Id;
                tblstockItem.Is_Auto = modelstockItem.Is_Auto_SerialNo;
                tblstockItem.Prefix_SerialNo = modelstockItem.Prefix_SerialNo;
                tblstockItem.Sufix_SerialNo = modelstockItem.Sufix_SerialNo;
                tblstockItem.Min_Qty = modelstockItem.Min_Qty;
                tblstockItem.Starting_SerialNo = modelstockItem.Start_From;
                tblstockItem.Modified_Date = DateTime.Now;
                tblstockItem.Modified_By = 1;
                tblstockItem.Image_Url = modelstockItem.Image_Path ?? tblstockItem.Image_Url;
                //tblstockItem.Branch_Id = 1;
                //tblstockItem.Company_Id = 1;
                //tblstockItem.Is_Active = true;
                //tblstockItem.Is_Deleted = false;
                //tblstockItem.Created_By = 1;
                //tblstockItem.Created_Date = DateTime.Now;

                result = Save(tblstockItem);

            }
            catch
            {

                result = false;
            }
            return result;

        }
        public List<DDLBind> DDLBind()
        {
            var lst = _db.Stock_Item.Where(x => x.Company_Id == 1 && x.Is_Deleted == false).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
            return lst;
        }


        private bool Save(Stock_Item tblStockItem)
        {
            bool result = false;
            try
            {
                bool res = _db.Stock_Item.Any(m => m.Company_Id == 1 && (0 == tblStockItem.Id || m.Id != tblStockItem.Id) && (m.Name.Trim().Equals(tblStockItem.Name.ToUpper().Trim())));
                if (res == false)
                {
                    if (tblStockItem.Id == 0)
                    {
                        tblStockItem.Id = _db.Stock_Item.AsNoTracking().Count() != 0 ? _db.Stock_Item.AsNoTracking().Max(x => x.Id) + 1 : 1;
                        _db.Stock_Item.Add(tblStockItem);
                    }
                    _db.SaveChanges();
                    result = true;

                }
            }
            catch
            {
                return result;
            }
            return result;
        }
        public ViewModel.Stock.StockItem Find(long Id)
        {
            ViewModel.Stock.StockItem modelstockItem = new StockItem();
            if (Id != 0)
            {
                Stock_Item tblstockItem = _db.Stock_Item.Find(Id);
                modelstockItem.Name = tblstockItem.Name;
                modelstockItem.Id = tblstockItem.Id;
                modelstockItem.Item_Code = tblstockItem.Item_Code;
                modelstockItem.Remarks = tblstockItem.Remarks;
                modelstockItem.Unit_Id = tblstockItem.Unit_Id;
                modelstockItem.Alias_Name = tblstockItem.Alias_Name;
                modelstockItem.Group_Id = tblstockItem.Group_Id;
                modelstockItem.Category_Id = tblstockItem.Category_Id;
                modelstockItem.Start_From = tblstockItem.Starting_SerialNo;
                modelstockItem.Prefix_SerialNo = tblstockItem.Prefix_SerialNo;
                modelstockItem.Is_Auto_SerialNo = tblstockItem.Is_Auto;
                modelstockItem.Min_Qty = tblstockItem.Min_Qty;
                modelstockItem.Image_Path = tblstockItem.Image_Url;
                // return null;

            }

            return modelstockItem;
        }
        public bool Delete(int Id)
        {
            if (Id != 0)
            {
                Stock_Item tblstockItem = _db.Stock_Item.Find(Id);


                tblstockItem.Is_Deleted = true;
                tblstockItem.Modified_By = 1;
                tblstockItem.Modified_Date = DateTime.Now;
                _db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public String GEN_ItemCode()
        {
            string Challan_No = "";

            int countRows = _db.Stock_Item.Where(x => x.Company_Id == 1).Count();
            if (countRows != 0)
                Challan_No = _db.Stock_Item.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Item_Code;
            if (!string.IsNullOrWhiteSpace(Challan_No))
            {
                Challan_No = Regex.Replace(Challan_No, @"\d+(?=\D*$)",
                   m => (Convert.ToInt64(m.Value) + 1).ToString());


            }
            else
            {
                Challan_No = "1ITM-" + 1;
            }


            return Challan_No;
        }
        public bool IsNameExists(int Id, string Name)
        {
            return _db.Stock_Item.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
        }
        public String GEN_MAXId(long itemId)
        {
            string Max_Id = "";

            int countRows = _db.Purchase_Master_Tra.Where(x => x.Item_Id == itemId).Count();
            if (countRows != 0)
                Max_Id = _db.Purchase_Master_Tra.Where(x=>x.Item_Id==itemId).OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Purchase_Serial_No;
            if (!string.IsNullOrWhiteSpace(Max_Id))
            {
                Max_Id = Regex.Replace(Max_Id, @"\d+(?=\D*$)",
                   m => (Convert.ToInt64(m.Value) + 1).ToString());


            }
            else
            {
                // Max_Id = "1QUT" + 1;
                Max_Id = _db.Stock_Item.FirstOrDefault(x => x.Id == itemId).Prefix_SerialNo + "-" + 1;
            }


            return Max_Id;
        }

        public dynamic GetUnitAndAmount(int itemId)
        {
            ViewModel.Stock.StockItem item = new StockItem();
            if (itemId != 0)
            {
                //return (from u in _db.UnitMasters
                //            join s in _db.Stock_Item on u.Id equals s.Unit_Id into temp
                //            from t in temp.DefaultIfEmpty()
                //            join st in _db.Stock_Master on t.Id equals st.Item_Id  
                //            where t.Id==itemId
                //            select new {u.Name, st.Selling_Rate}
                //            ).AsParallel().AsEnumerable().Select(x=>new{UnitName=x.Name,Amount=x.Selling_Rate}).FirstOrDefault();

                return _db.GetUnitNameAndAmount(itemId).AsParallel().AsEnumerable().Select(x => new { UnitName = x.Name, Amount = x.Selling_Rate });
            }
            return null;
        }

        public List<ViewModel.Ledger.SalesMaster> GetItemDetails(long itemId)
        {
            List<ViewModel.Ledger.SalesMaster> listitem = new List<ViewModel.Ledger.SalesMaster>();
            _db.Purchase_Master_Tra.Where(x => x.Item_Id == itemId).Select(i => new ViewModel.Ledger.SalesMaster { Id = i.Item_Id, Challan_Number = i.Purchase_Serial_No, Discount = i.Rate ?? 0 }).AsParallel().AsEnumerable().ToList().ForEach(p => listitem.Add(
                 new ViewModel.Ledger.SalesMaster
                 {
                     Id = p.Id,
                     Challan_Number = p.Challan_Number,
                     Discount = p.Discount,
                 }));
            return listitem;
        }
        public List<ItemDDl> DDLItemBind()
        {
            var lst = _db.Stock_Item.Where(x => x.Company_Id == 1 && x.Is_Deleted == false).Select(x => new ItemDDl { Id = x.Id + "_" + x.Is_Auto, Name = x.Name }).ToList();
            return lst;
        }

        public List<ItemDDl> ddlBindSaleItem()
        {
            List<ItemDDl> list = new List<ItemDDl>();
            //(from st in _db.Stock_Item
            //         join sm in _db.Stock_Master on st.Id equals sm.Item_Id

            //         where st.Is_Active == true
            //         select new
            //         {
            //             itemId = st.Id,
            //             ItemName = st.Name,
            //             st.Is_Auto,
            //             Quantity = sm.Qty
            //         }).AsParallel().AsEnumerable().ToList().ForEach((x) => list.Add(new ItemDDl {Id=x.itemId+"_"+x.Is_Auto+"_"+x.Quantity,Name=x.ItemName }));
            //return list;
            _db.GetIItemlist().AsParallel().AsEnumerable().ToList().ForEach((x) => list.Add(new ItemDDl { Id = x.Id + "_" + x.Is_Auto + "_" + x.Quantity, Name = x.Name }));
            return list;
        }
        public bool GetItemType(long itemId)
        {
            return _db.Stock_Item.SingleOrDefault(x => x.Id == itemId).Is_Auto;
        }
    }
}
