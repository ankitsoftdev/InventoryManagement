using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ViewModel.Common;
namespace ViewModel.Transactions
{
    public class PaymentInfo
    {
        public PaymentInfo()
        {
            User_List = new List<Common.DDLBind>();
            Bill_List = new List<DDLBind>();
            BillList = new List<BillList>();
        }
        public long Id { get; set; }

        public bool Bill_By_Bill { get; set; }
        public Nullable<decimal> Amount { get; set; }
        [Required(ErrorMessage = "Required")]
        public long User_Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public long Payment_Type { get; set; }
        public Nullable<decimal> Tax_Amount { get; set; }
        public string Cheque_No { get; set; }
        public string Account_No { get; set; }
        public string Bank_Name { get; set; }
        public string IFSC_Code { get; set; }
        [Required(ErrorMessage = "Required")]
        public System.DateTime Payment_Date { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string VC_No { get; set; }
        public string Remarks { get; set; }
        public string Tag_Type { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Contact_No { get; set; }
        public string Image_Path { get; set; }
        public List<ViewModel.Common.DDLBind> User_List { get; set; }
        public List<DDLBind> Bill_List { get; set; }
        public List<BillList> BillList {get;set;}
    }
    public class BillList
    {
        public BillList()
        {
            Status = false;
        }
        public long Pur_Id { get; set; }
        public decimal Bill_Amount { get; set; }
        public string VC_No { get; set; }
        public decimal Balance { get; set; }
        public decimal PaidBalance { get; set; }
        public bool Status { get; set; }
    }
    public class PaymentList
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email_Id { get; set; }
        public string Contact_No { get; set; }
        public decimal Billing_Amount { get; set; }
        public decimal Payment_Amount { get; set; }
        public decimal Balance_Amount { get; set; }


    }
    public class PaymentDetails
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Image_Path { get; set; }
        public string Tag_Type { get; set; }
        public string Payment_Date { get; set; }
        public string Payment_Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax_Amount { get; set; }
        public decimal Balance_Amount { get; set; }
        public string Remarks { get; set; }
    }

    public class BillingDetails
    {
        public long Id { get; set; }

        public string Chalan_No { get; set; }
        public long User_Id { get; set; }
        public Nullable<decimal> Billing_Amount { get; set; }
        public Nullable<decimal> Tax_Amount { get; set; }
        public Nullable<decimal> Discount_Amount { get; set; }
        public Nullable<decimal> Transport { get; set; }
        public string Tag_Type { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
        public string Billing_Date { get; set; }

    }
}
