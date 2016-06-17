using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Common;
using DataLayer.Masters;
namespace ServiceLayer.Masters
{
    public class UnitsServiceLayer
    {
        UnitsDblayer _db;
        public UnitsServiceLayer()
        {
            _db = new UnitsDblayer();
        }
        public UnitsServiceLayer(string ConnectionString)
        {
      _db = new UnitsDblayer(ConnectionString);
        }
        public List<ViewModel.Common.Unit> List(string Search="")
        {
            return _db.List(Search);
        }
        public List<DDLBind> DDLBind()
        {
            return _db.DDLBind();
        }
        public bool Update(ViewModel.Common.Unit modelunit)
        {
             return _db.Update(modelunit);
        }

        public bool Create(ViewModel.Common.Unit modelunit)
        {
            return _db.Create(modelunit);
        }
        public ViewModel.Common.Unit Find(int Id)
        {
            return _db.Find(Id);
        }
        public List<SubUnit> FindSubUnit(long Id)
        {
            return _db.FindSubUnit(Id);
        }
        public bool IsNameExists(int Id, string Name)
        {
            return _db.IsNameExists(Id, Name);
        }

        public bool Delete(int Id)
        {
            return _db.Delete(Id);
        }
    }
     
}
