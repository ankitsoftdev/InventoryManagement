using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Helper
{
    public class ResponseHelper
    {
        public  static Dictionary<String, Object> GenerateResponse(bool Status, Object Message)
        {
            Dictionary<String, Object> _data = new Dictionary<string, object>();
            _data.Add("Status", Status ? "true" : "false");
            _data.Add("Data", Message);
            return _data;

        }
    }
}