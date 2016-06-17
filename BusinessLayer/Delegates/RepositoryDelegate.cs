using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Delegates
{
  public static  class RepositoryDelegate<T>
    {
   
      public   delegate bool Bool_UpdateData(T lst) ;
    
    }
}
