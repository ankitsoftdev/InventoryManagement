using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Masters
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class CityDbLayer
    {
        INVENTORY_DBEntities _db = new INVENTORY_DBEntities();
        public CityDbLayer()
        {
            _db = new INVENTORY_DBEntities();

        }
        public CityDbLayer(String ConnectionString)
        {        
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public IEnumerable<ViewModel.Common.DDLBind> ListCityDDL(long ParentId = 0)
        {

            return _db.City_Master
                 .Where(x => x.Company_Id == 1 && (0 == ParentId || x.State_Id == ParentId) && x.Is_Active == true)
                 .AsParallel()
                 .Select(x => new ViewModel.Common.DDLBind { Id = x.Id, Name = x.Name })
                 ;


        }

        public IEnumerable<ViewModel.Common.DDLBind> DDLList(long Tag_Id, long ParentId = 0)
        {


            //  List<ViewModel.Master.CountryStateCityInfo> lst = new List<CountryStateCityInfo>();
            return _db.Country_State
                .Where(m => m.Tag_Id == Tag_Id && (0 == ParentId || m.Parent_Id == ParentId) && m.Is_Visible == true)
                .AsParallel()
                .Select(x => new ViewModel.Common.DDLBind { Id = x.Id, Name = x.Name }).AsParallel()
               ;

        }

        public bool IsNameExists(long Id, long StateId, string Name)
        {
            return _db.City_Master.Any(m => m.Company_Id == 1 && m.State_Id==StateId && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
        }
       
    }
}
