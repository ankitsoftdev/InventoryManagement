using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Masters;
using ViewModel.Common;
namespace ServiceLayer.Masters
{
  public  class FinancialYearServiceLayer
    {
        FinancialYearDbLayer _db;
        public FinancialYearServiceLayer()
        {
            _db = new FinancialYearDbLayer();
        }
        public FinancialYearServiceLayer(string ConnectionString)
        {
    _db = new FinancialYearDbLayer(ConnectionString);
        }
        public List<ViewModel.Common.FinancialYear> List()
        {
            return _db.List();
        }
        public bool Create(ViewModel.Common.FinancialYear modelFincial)
        {
            return _db.Create(modelFincial);

        }
        public bool Update(ViewModel.Common.FinancialYear modelFincial)
        {
            return _db.Update(modelFincial);

        }
        public ViewModel.Common.FinancialYear Find(int Id )
        {
            return _db.Find(Id);

        }
        public List<DDLBind> DDLBind()
        {
            return _db.DDLBind();
        }
        public bool IsNameExists(int Id, string Name)
        {
            return _db.IsNameExists(Id, Name);
        }
    }
}
