using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InventoryManagement.WebAPI
{
    public class CustomerController : ApiController
    {
        //ServiceLayer.AccountMaster
        [HttpGet]
        [ActionName("CustomerList")]
        public Dictionary<string, object> CustomerList()
        {
           
                return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.AccountMaster.CustomerServicesLayer().GetCustomer(Tagname:"CUSTOMER"));
          
        }
        [HttpGet]
        [ActionName("SupplierList")]
        public Dictionary<string, object> SupplierList()
        {

            return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.AccountMaster.CustomerServicesLayer().GetCustomer(Tagname: "SUPLLIER"));

        }
    }
}
