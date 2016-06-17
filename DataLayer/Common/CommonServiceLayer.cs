using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Common;
namespace ServiceLayer.Common
{
    public   class CommonServiceLayer
    {
        CommonDbLayer _db;
        public CommonServiceLayer()
        {
            _db = new CommonDbLayer();
        }
        public CommonServiceLayer(String ConnectionString)
        {
            _db = new CommonDbLayer(ConnectionString);
        }
        public IEnumerable<ViewModel.Common.DDLBind> ListCityDDL(long ParentId = 0)
        {
            return _db.ListCityDDL(ParentId);
        }
        public List<ViewModel.Common.DDLBind> DDLCountryState(long   Tag_Id, long ParentId = 0)
        {
            //  objCityDbLayer = new CityDbLayer();
            return _db.DDLCountryState(Tag_Id, ParentId).ToList();
        }
        public List<ViewModel.Common.DDLBind> DDLBind(string TableName, string searchText="")
        {
            return _db.DDLBind(TableName, searchText);
        }
        
        public string UnitNameByItemId(long ItemId)
        {
            return _db.UnitNameByItemId(ItemId);
        }
    }
}
