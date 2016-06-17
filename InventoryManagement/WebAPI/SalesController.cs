using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InventoryManagement.WebAPI
{
    public class SalesController : ApiController
    {

        [HttpGet]
        [ActionName("SalesInformation")]
        public Dictionary<string, object> SalesList()
        {
            int TagValue = 1;
            var request = HttpContext.Current.Request;
            try
            { TagValue = Convert.ToInt32(request["Tag"]); }
            catch { TagValue = 1; }
            if (TagValue == (int)ViewModel.Ledger.SaleTag.ItemWise)
                return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.Transactions.SalesServiceLayer().List());
            else
                return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.Transactions.SalesServiceLayer().List());

        }
    }
}
