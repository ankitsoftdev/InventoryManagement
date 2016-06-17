using InventoryManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InventoryManagement.WebAPI
{
    public class LoginController : BaseAPIController
    {
        [HttpGet]
        public Dictionary<string, object> Login()
        {
            return InventoryManagement.Helper.ResponseHelper.GenerateResponse(true, new UserDetails { UserName = "Ankit Kumar Singh", UserToken = Guid.NewGuid().ToString() });
        }
        
       
    }
    public class UserDetails
    {
        public String UserToken { get; set; }
        public String UserName { get; set; }
    
    }
    }

