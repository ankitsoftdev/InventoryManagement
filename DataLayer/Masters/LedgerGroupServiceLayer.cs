using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Masters;
using ViewModel.LadgerGroup;
using DataLayer;
namespace ServiceLayer.Masters
{
   public class LedgerGroupServiceLayer
    {
       LedgerGroupDbLayer _db;
       public LedgerGroupServiceLayer()
       {
           _db = new LedgerGroupDbLayer();
       }
       public LedgerGroupServiceLayer(string ConnectionString)
       {
 _db = new LedgerGroupDbLayer(ConnectionString);
       }
       public List<ViewModel.Common.List_Common> List()
       {
           
           return _db.List();
       }
       public List<ViewModel.Common.DDLBind> DDLBind(string Tag, string searchText)
       {
           return _db.DDLBind(Tag, searchText);
       }
     
       public bool create(LedgerGroup modelledgrp)
       {
         
         
           return _db.create(modelledgrp);
       }
       
       public LedgerGroup Find(int Id)
       {
           
           return _db.Find(Id);
           

       }
       public bool Update(LedgerGroup modelledgrp)
       {
           return _db.Update(modelledgrp);
           
       }
       public bool IsNameExists(long Id,string Name)
       {
           return _db.IsNameExists(Id, Name);
       }
    }
}
