using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.IO;
namespace ViewModel.Stock
{
    public class Stock
    {
        public int Id { get; set; }
        public int Company_Id { get; set; }
        public int Branch_Id { get; set; }
        public int Item_Id { get; set; }
        public int Fin_Id { get; set; }
        public decimal Opening_Qty { get; set; }
        public decimal Qty { get; set; }
        public decimal Opening_Rate { get; set; }
        public decimal Unit_Id { get; set; }
        public decimal Selling_Price { get; set; }

    }
    public class StockItem
    {
        public StockItem()
        {
            UnitList = new List<Common.DDLBind>();
            CategoryList = new List<Common.DDLBind>();
            GroupList = new List<Common.DDLBind>();
           
        }
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long Branch_Id { get; set; }
        [Required(ErrorMessage = "Enter The Item Code")]
        public String Item_Code { get; set; }
        [Required(ErrorMessage = "Enter The Name")]
        [Remote("IsNameExists", "StockItem", "InventoryMaster", AdditionalFields = "Id,Name", ErrorMessage = "Name Already Exists.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter The Alias Name")]
        public string Alias_Name { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Please Select Unit")]
        public long Unit_Id { get; set; }
        [Required(ErrorMessage = "Please Select Group")]
        public long Group_Id { get; set; }
        [Required(ErrorMessage = "Please Select Category")]
        public long Category_Id { get; set; }
        public string GroupName { set; get; }
        public string CategoryName { set; get; }
        public string UnitName { set; get; }
        public Decimal Min_Qty { get; set; }
        public string Prefix_SerialNo { get; set; }
        public string Sufix_SerialNo { get; set; }
        public int Start_From { get; set; }
        public bool Is_Auto_SerialNo { get; set; }
        public string Image_Path { get; set; }
      
        public List<ViewModel.Common.DDLBind> UnitList { set; get; }
        public List<ViewModel.Common.DDLBind> CategoryList { set; get; }
        public List<ViewModel.Common.DDLBind> GroupList { set; get; }
    }

   
}
