using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.DatabaseConfiguration
{
   public static class DbConfiguration
    {
       private static string _connectionString { get; set; }
       public static String ConnectionString { get {
           return _connectionString;
       }
           set {
               _connectionString = value;
           }
       }

    }
}
