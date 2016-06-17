using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InventoryManagement.Helper
{
    public class BaseAPIController:ApiController
    {
        public override System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Web.Http.Controllers.HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {

            var s = base.ExecuteAsync(controllerContext, cancellationToken);
          
            return s;
        }
       
      
    }
}