using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ViewModel.Common
{
    public class Ledger_Common
    {
        // private String _Name { get; set; }

        public Ledger_Common()
        {
            ListUnder = new List<DDLBind>();
        }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long UnderId { get; set; }
        public string UnderName { get; set; }
        //   public int Under_Id { get; set; }
        public long Id { get; set; }
        [Required(ErrorMessage = "Required")]
        //[Remote("IsNameExists", "Ledger", "Masters", AdditionalFields = "Id,Name", ErrorMessage = "Name Already Exists.")]
        public String Name { get; set; }
        //get { return _Name; }
        //set {
        //    if (String.IsNullOrWhiteSpace(value) || value.Trim().Length < 3)
        //        throw new Exception("Ledger Name Could not be less than 3 characters");
        //    else
        //        _Name = value;
        //} 

        public String Alias_Name { get; set; }
        public String Remarks { get; set; }
        public virtual List<DDLBind> ListUnder { get; set; }
        public List<ItemDDl> itemddlbind { get; set; }
        public List<DDLBind> bindDDl { get; set; }
        public List<DDLBind> GoDownList { get; set; }
        public List<DDLBind> bindTax { get; set; }
    }
    public class List_Common
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias_Name { get; set; }
        public string Under_Name { get; set; }
        public string Remarks { get; set; }
    }
    public class DDLBind
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class BindDdlTax
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
    public class ItemDDl
    {
        public string Id { get; set; }
        public String Name { get; set; }
    }
}
