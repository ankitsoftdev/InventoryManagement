using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDMEntity;
using ViewModel.Common;
namespace DataLayer.Masters
{
    public  class RegionDbLayer
    {
        INVENTORY_DBEntities _db;
        public RegionDbLayer()
        {
             _db = new INVENTORY_DBEntities();
             
          
        }
        public RegionDbLayer(string ConnectionString)
        {
            _db = new INVENTORY_DBEntities();
            _db.Database.Connection.ConnectionString = ConnectionString;
        }
        public List<ViewModel.Common.RegionInfo> List()
        {
            List<ViewModel.Common.RegionInfo> lst = new List<RegionInfo>();
            var list = _db.Pr_RegionList(1).ToList();
            list.ForEach((x) => lst.Add(new RegionInfo { 
            Id=x.Id,Name=x.Name,Remarks=x.Remarks,Under_Name=x.Under_Name,GoDown_Name=x.GoDwon_Name,Alias_Name=x.Alias_Name
            }));
            return lst.ToList();
        }
        public bool Create(ViewModel.Common.RegionInfo modelRegionInfo)
        {
            bool result = false;
            try
            {

                GoDown_Region tblGoDwonRegion = new GoDown_Region();
                tblGoDwonRegion.Id = modelRegionInfo.Id;
                tblGoDwonRegion.Name = modelRegionInfo.Name;
                tblGoDwonRegion.Remarks = modelRegionInfo.Remarks;
                tblGoDwonRegion.Under_Id = modelRegionInfo.Under_Id??0;
                tblGoDwonRegion.GoDown_Id = modelRegionInfo.GoDown_Id;
                tblGoDwonRegion.Alias_Name = modelRegionInfo.Alias_Name;
                tblGoDwonRegion.Company_Id = 1;
                tblGoDwonRegion.Is_Active = true;
                tblGoDwonRegion.Is_Deleted = false;
                tblGoDwonRegion.Alias_Name = modelRegionInfo.Alias_Name ?? string.Empty;
                tblGoDwonRegion.Modified_Date = DateTime.Now;
                tblGoDwonRegion.Modified_By = 1;
                tblGoDwonRegion.Created_By = 1;
                tblGoDwonRegion.Created_Date = DateTime.Now;

                result = Save(tblGoDwonRegion);

            }
            catch
            {

                result = false;
            }
            return result;

        }

        public bool Update(ViewModel.Common.RegionInfo modelRegionInfo)
        {
            bool result = false;
            try
            {
                if (modelRegionInfo != null && modelRegionInfo.Id != 0)
                {
                    GoDown_Region tblGoDwonRegion = _db.GoDown_Region.Find(modelRegionInfo.Id);
                    tblGoDwonRegion.Id = modelRegionInfo.Id;
                    tblGoDwonRegion.Alias_Name = modelRegionInfo.Alias_Name;
                    tblGoDwonRegion.Name = modelRegionInfo.Name;
                    tblGoDwonRegion.Remarks = modelRegionInfo.Remarks;
                    tblGoDwonRegion.Under_Id = modelRegionInfo.Under_Id;
                    tblGoDwonRegion.GoDown_Id = modelRegionInfo.GoDown_Id;
                    tblGoDwonRegion.Modified_Date = DateTime.Now;
                    tblGoDwonRegion.Modified_By = 1;
                   
                    result = Save(tblGoDwonRegion);
                }
            }
            catch
            {

                result = false;
            }
            return result;

        }
        private bool Save(GoDown_Region tblGoDwonRegion)
        {
            bool result = false;
            try
            {
                bool res = _db.GoDown_Region.Any(m => m.Company_Id == 1 && (0 == tblGoDwonRegion.Id || m.Id != tblGoDwonRegion.Id) && (m.Name.Trim().Equals(tblGoDwonRegion.Name.ToUpper().Trim())));
                if (res == false)
                {
                    if (tblGoDwonRegion.Id == 0)
                    {
                        tblGoDwonRegion.Id = _db.GoDown_Region.AsNoTracking().Count() != 0 ? _db.GoDown_Region.Max(x => x.Id) + 1 : 1;
                        _db.GoDown_Region.Add(tblGoDwonRegion);
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
        public bool Delete(int Id)
        {
            if (Id != 0)
            {
                GoDown_Region tblGoDwonRegion = _db.GoDown_Region.Find(Id);
                tblGoDwonRegion.Modified_Date = DateTime.Now;
                tblGoDwonRegion.Modified_By = 1;
                tblGoDwonRegion.Is_Deleted = true;
                return true;
            }
            else
                return false;
        }
        public ViewModel.Common.RegionInfo  Find(long Id)
        {
           RegionInfo modelRegionInfo  = new RegionInfo();
            if (Id != 0)
            {
                GoDown_Region tblGoDwonRegion = _db.GoDown_Region.Find(Id);
                modelRegionInfo.Id = tblGoDwonRegion.Id;
                modelRegionInfo.Name = tblGoDwonRegion.Name;
                modelRegionInfo.Alias_Name = tblGoDwonRegion.Alias_Name;
                modelRegionInfo.GoDown_Id = tblGoDwonRegion.GoDown_Id;
                modelRegionInfo.Remarks = tblGoDwonRegion.Remarks ?? string.Empty;
                modelRegionInfo.Under_Id = tblGoDwonRegion.Under_Id ?? 0;
               
            }

            return modelRegionInfo;
        }
        //public String GEN_TaxCode()
        //{
        //    string Challan_No = "";

        //    int countRows = _db.GoDowns.Where(x => x.Company_Id == 1).Count();
        //    if (countRows != 0)
        //        Challan_No = _db.GoDowns.OrderBy(x => x.Id).Skip(countRows - 1).FirstOrDefault().Code;
        //    if (!string.IsNullOrWhiteSpace(Challan_No))
        //    {
        //        Challan_No = Regex.Replace(Challan_No, @"\d+(?=\D*$)",
        //           m => (Convert.ToInt64(m.Value) + 1).ToString());


        //    }
        //    else
        //    {
        //        Challan_No = "1GD-" + 1;
        //    }


        //    return Challan_No;
        //}
        //public bool IsNameExists(int Id, string Name)
        //{
        //    return _db.GoDowns.Any(m => m.Company_Id == 1 && (0 == Id || m.Id != Id) && (m.Name.Trim().ToUpper().Equals(Name.ToUpper().Trim())));
        //}
    }
}
