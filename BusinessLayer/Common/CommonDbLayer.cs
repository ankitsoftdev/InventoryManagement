using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDMEntity;
using ViewModel.Common;
namespace DataLayer.Common
{
    public  class CommonDbLayer
    {
        INVENTORY_DBEntities _db;
        public CommonDbLayer()
        {
            _db = new INVENTORY_DBEntities();
        
        }

        public CommonDbLayer(String ConnectionString)
        {  
            _db = new INVENTORY_DBEntities();
              _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public string UnitNameByItemId(long ItemId)
        {
            string name = "";
            if (ItemId != 0)
            {
                var item = _db.Stock_Item.SingleOrDefault(x => x.Id == ItemId);
                name = _db.UnitMasters.SingleOrDefault(x => x.Id == item.Unit_Id).Name ?? "";
            }
            return name;
        }
        public IEnumerable<ViewModel.Common.DDLBind> ListCityDDL(long ParentId = 0)
        {

            return _db.City_Master
                 .Where(x => x.Company_Id == 1 && (0 == ParentId || x.State_Id == ParentId) && x.Is_Active == true)
                 .AsParallel()
                 .Select(x => new ViewModel.Common.DDLBind { Id = x.Id, Name = x.Name })
                 ;


        }

        public IEnumerable<ViewModel.Common.DDLBind> DDLCountryState(long Tag_Id, long ParentId = 0)
        {


            //  List<ViewModel.Master.CountryStateCityInfo> lst = new List<CountryStateCityInfo>();
            return _db.Country_State
                .Where(m => m.Tag_Id == Tag_Id && (0 == ParentId || m.Parent_Id == ParentId) && m.Is_Visible == true)
                .AsParallel()
                .Select(x => new ViewModel.Common.DDLBind { Id = x.Id, Name = x.Name }).AsParallel()
               ;

        }
        public List<ViewModel.Common.DDLBind> DDLBind(string TableName, string searchText="")
        {
            List<DDLBind> list = new List<DDLBind>();
            if (string.Compare(TableName.Trim(), "Ledger_Group",true)==0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Ledger_Group.Where(x => x.Is_Deleted==false && x.Is_Active==true && x.Company_Id==1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Ledger_Group.Where(x => x.Is_Deleted==false && x.Is_Active==true && x.Company_Id==1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
            }
            else if (string.Compare(TableName.Trim(), "Stock_Group",true) == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Stock_Group.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Stock_Group.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
               
            }
            else if (string.Compare(TableName.Trim(), "Stock_Category") == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Stock_Category.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Stock_Category.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
               
            }
            else if (string.Compare(TableName.Trim(), "UnitMaster", true) == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.UnitMasters.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.UnitMasters.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
               
            }
            else if (string.Compare(TableName.Trim(), "GoDown_Master", true) == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.GoDown_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.GoDown_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();

            }
            else if (string.Compare(TableName.Trim(), "GoDown_Region", true) == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.GoDown_Region.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.GoDown_Region.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
                
            }
            else if (string.Compare(TableName.Trim(), "Stock_Item", true) == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Stock_Item.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Stock_Item.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
                
            }
            else if (string.Compare(TableName.Trim(), "Customer", true) == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Ledger_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Group_Id == 28 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Ledger_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Group_Id == 28).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();

            }
            else if (string.Compare(TableName.Trim(), "Supplier", true) == 0)
            {
                list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Ledger_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Group_Id == 27 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Ledger_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Group_Id == 27).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();

            }
            else if (string.Compare(TableName.Trim(), "Employee", true) == 0)
            {
              //  list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Stock_Item.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList() : _db.Stock_Item.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
                list.Add(new DDLBind { Id = 1, Name = "Ajay" });
                list.Add(new DDLBind { Id = 2, Name = "Vijay" });
                list.Add(new DDLBind { Id = 3, Name = "Sanjay" });

            }
            else if (string.Compare(TableName.Trim(), "Tax_Deduction_Master", true) == 0)
            {
                // list = String.IsNullOrWhiteSpace(searchText) != true ? _db.Tax_Deduction_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1 && x.Name.ToUpper().StartsWith(searchText.ToUpper())).Select(x => new DDLBind { Id = x.Id, Name = x.Name + "(" + x.Rate + "%)" }).ToList() : _db.Tax_Deduction_Master.Where(x => x.Is_Deleted == false && x.Is_Active == true && x.Company_Id == 1).Select(x => new DDLBind { Id = x.Id, Name = x.Name + "(" + x.Rate + "%)" }).ToList();
                list = _db.Tax_Deduction_Master.Where(x => x.Is_Active == true && x.Is_Deleted == false && x.Company_Id == 1 && ("" == searchText || x.Name.ToUpper().StartsWith(searchText.ToUpper()))).Select(t => new DDLBind { Value = t.Id + "_" + t.Type + "_" + t.Rate_Type + "_" + t.Rate, Name = t.Name }).ToList();
            }
            return list;
        }
    }
}
