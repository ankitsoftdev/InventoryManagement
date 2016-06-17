using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InventoryManagement.WebAPI
{
    public class StockController : ApiController
    {
        [HttpGet]
        [ActionName("StockInformation")]
        public  Dictionary<string, object> StockList()
        {
            int TagValue=1;
            var request=HttpContext.Current.Request;
            try
            {TagValue = Convert.ToInt32(request["Tag"]);}
            catch{TagValue =1;}
           if(TagValue==(int) ViewModel.Transactions.StockTag.Repurchase)
            return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.Transactions.StockMasterServiceLayer().List());
            else
                if(TagValue==(int) ViewModel.Transactions.StockTag.OutOfStock)
                    return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.Transactions.StockMasterServiceLayer().List().Where(m=>Convert.ToInt32(m.Qty)<=0));
                else
                    return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new ServiceLayer.Transactions.StockMasterServiceLayer().List());

        }
    }
}
