using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InventoryManagement.WebAPI
{
    public class PurchaseController : ApiController
    {
        //ViewModel.Ledger
        [HttpGet]
        [ActionName("PurchaseInformation")]
        public Dictionary<string, object> PurchaseList()
        {
            int TagValue = 1;
            var request = HttpContext.Current.Request;
            try
            { TagValue = Convert.ToInt32(request["Tag"]); }
            catch { TagValue = 1; }
            if (TagValue == (int)ViewModel.Ledger.PurchaseTag.ItemWise)
                return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.Transactions.PurchaseServiceLayer().List());
            else
                return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.Transactions.PurchaseServiceLayer().List());

        }
    }
}
