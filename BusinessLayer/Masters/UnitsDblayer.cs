using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using HDMEntity;
using ViewModel.Common;

namespace DataLayer.Masters
{
  public  class UnitsDblayer
    {
      INVENTORY_DBEntities _db;
      public UnitsDblayer()
      {
          _db = new INVENTORY_DBEntities();
         
      }
      public UnitsDblayer(String ConnectionString)
      {
          _db = new INVENTORY_DBEntities();
          _db.Database.Connection.ConnectionString = ConnectionString;
      }
      public List<ViewModel.Common.Unit> List(string Search="")
      {
          List<Unit> list = new List<Unit>();
          var data=_db.UnitMasters.Where(x=>x.Group_Id==0).ToList();
          data.ForEach(x => list.Add(new Unit { Id = x.Id, Name = x.Name, Remarks = x.Remarks, AliasName = x.Alias_Name, Unit_Volume = x.Unit_Volume, No_of_Decimal = x.No_of_Decimal ?? 0 }));
          //dat
          //    .Select(x => new Unit { Id = x.Id, Name = x.Name, Remarks = x.Remarks, AliasName = x.Alias_Name, Unit_Volume = x.Unit_Volume,No_of_Decimal=x.No_of_Decimal??0 }).ToList();
       
          if(!String.IsNullOrWhiteSpace(Search))
          {
              list = list.Where(m => m.Name.ToUpper().Trim().StartsWith(Search.ToUpper().Trim())).ToList();
          }
          return list;
      }
      public bool Create(ViewModel.Common.Unit modelunit)
      {
          bool result = false;
          UnitMaster tblunit=new UnitMaster();
          

        //  tblunit.Id = modelunit.Id;
          tblunit.Name = modelunit.Name.Trim();
          tblunit.Remarks = modelunit.Remarks;
          tblunit.Alias_Name = modelunit.AliasName;
          tblunit.Unit_Volume = "0";
          tblunit.No_of_Decimal = modelunit.No_of_Decimal ;
          tblunit.Is_Active = true;
          tblunit.Is_Deleted = false;
          tblunit.Company_Id = 1;
          tblunit.Created_By = 1;
          tblunit.Ratio1 = 1;
          tblunit.Ratio2 = 1;
          tblunit.Created_Date = DateTime.Now;
          tblunit.Modified_Date = DateTime.Now;
          tblunit.Modified_By = 1;
          result = Save(tblunit);
          if (result == true && modelunit.SubUnitList.Count > 0 && modelunit.AddSubUnit == true)
          {
             
              result = CreateSubUnit(modelunit.SubUnitList, tblunit.Id);
          }
          return result;

      }

      public bool CreateSubUnit(List<SubUnit> _sub,long UnitId=0)
      {
          bool result = false;
          List<UnitMaster> lstunit=new List<UnitMaster>();
          UnitMaster tblunit ;
        
          long maxid = _db.UnitMasters.Count() != 0 ? _db.UnitMasters.Max(x => x.Id) + 1 : 1;
          if (_sub!=null &&  UnitId!=0 )
          { 
          foreach (var item in _sub)
          {
              tblunit = new UnitMaster();


              tblunit.Id = maxid;
              tblunit.Group_Id = UnitId;
              tblunit.Name = item.Name??string.Empty;
              tblunit.Remarks = "Subunit";
              tblunit.Alias_Name = item.Alias_Name;
              tblunit.Unit_Volume = "0";
              tblunit.No_of_Decimal = 0;
              tblunit.Ratio1 = Convert.ToDecimal(item.Ratio1) > 0 ? Convert.ToDecimal(item.Ratio1) : 1;
              tblunit.Ratio2 = Convert.ToDecimal(item.Ratio2) > 0 ? Convert.ToDecimal(item.Ratio2) : 1;
          tblunit.Is_Active = true;
          tblunit.Is_Deleted = false;
          tblunit.Company_Id = 1;
          tblunit.Created_By = 1;
          tblunit.Created_Date = DateTime.Now;
          tblunit.Modified_Date = DateTime.Now;
          tblunit.Modified_By = 1;
              lstunit.Add(tblunit);
              maxid++;
          }
          _db.UnitMasters.AddRange(lstunit);
          _db.SaveChanges();
          result = true;
          }
          return result;

      }
            public bool Update(ViewModel.Common.Unit modelunit)
         {
          bool result = false;
           
          if (modelunit.Id != 0)
          {
              UnitMaster tblunit = _db.UnitMasters.Find(modelunit.Id);


              tblunit.Name = modelunit.Name;
              tblunit.Remarks = modelunit.Remarks;
              tblunit.Alias_Name = modelunit.AliasName;
              tblunit.Unit_Volume = modelunit.Unit_Volume;
              tblunit.No_of_Decimal = modelunit.No_of_Decimal;
             
              tblunit.Modified_Date = DateTime.Now;
              tblunit.Modified_By = 1;
              result = Save(tblunit);
              _db.Pr_DeleteCommonTblRcrd(tblunit.Id, "UNIT");
              if (result == true && modelunit.SubUnitList.Count > 0)
              {
                 
                  result = CreateSubUnit(modelunit.SubUnitList, tblunit.Id);
              }
          }
          return result;

      }
      public List<DDLBind> DDLBind()
      {
          var lst = _db.UnitMasters.Select(x => new DDLBind { Id = x.Id, Name = x.Name }).ToList();
          return lst;
      }
      public List<SubUnit> FindSubUnit(long Id)
      {
          List<SubUnit> modelsubunit = new List<SubUnit>();
         
          if (Id != 0)
          {
              var lst = _db.UnitMasters.Where(x => x.Group_Id == Id && x.Is_Deleted==false).ToList();
              lst.ForEach(x => modelsubunit.Add(new SubUnit
              {
                 Id=x.Id,
              Alias_Name=x.Alias_Name,
              Name=x.Name,
              Ratio1= String.Format("{0:#}",x.Ratio1)??"1",
                 Ratio2 = String.Format("{0:#}", x.Ratio2) ?? "1",
              }));
          }
          return modelsubunit;
      }
      public ViewModel.Common.Unit Find(int Id)
      {

          ViewModel.Common.Unit modelunit = new Unit();
          if(Id!=0)
          {
             
              UnitMaster tblunit = _db.UnitMasters.Find(Id);
              if (tblunit != null)
              {
                  modelunit.Id = tblunit.Id;
                  modelunit.Name = tblunit.Name;
                  modelunit.Remarks = tblunit.Remarks;
                  modelunit.Unit_Volume = tblunit.Remarks;
                  modelunit.AliasName = tblunit.Alias_Name;
                  modelunit.No_of_Decimal = tblunit.No_of_Decimal ?? 0;
                  int cnt = _db.UnitMasters.Count(x => x.Company_Id==1 && x.Is_Deleted==false && x.Group_Id == Id);
                  modelunit.AddSubUnit = cnt > 0 ? true : false;
              }
              
          }
         return modelunit;
      }
      public bool Delete(int Id)
      {
          if (Id != 0)
          {
              UnitMaster tblunit = _db.UnitMasters.Find(Id);
              tblunit.Is_Deleted = true;
              tblunit.Modified_By = 1;
              tblunit.Modified_Date = DateTime.Now;
              _db.SaveChanges();
              return true;
          }
          return false;
      }
      private bool Save(UnitMaster units)
      {
          bool result = false;
          try
          {
              bool res = _db.UnitMasters.AsNoTracking().Any(m => m.Company_Id == 1 && (0 == units.Id || m.Id != units.Id) && (m.Name.Trim().Equals(units.Name.ToUpper().Trim())));
              if (res == false )

              {
                  if (units.Id == 0)
                  {
                      units.Id=_db.UnitMasters.AsNoTracking().Count()!=0?_db.UnitMasters.AsNoTracking().Max(x=>x.Id)+1:1;
                      _db.UnitMasters.Add(units);
                  }
                  _db.SaveChanges();
                  result = true;
              }
              return result;
          }
          catch
          {
              return result;
          }
      }
      public bool IsNameExists(int Id, string Name)
      {
          return _db.UnitMasters.AsNoTracking().Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
      }

      
    }
}
