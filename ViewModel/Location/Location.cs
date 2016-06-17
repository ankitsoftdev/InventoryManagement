using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Location
{
 public  class Location
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public String Name { get; set; }
        public String Alias { get; set; }

    }

 public class Country
 {
     public virtual Location country { get; set; }
 }
 public class State
 {
     public virtual Location state { get; set; }
 }
 public class City {
     public int CountyId { get; set; }
     public int StateId { get; set; }
     public String Name { get; set; }
     public String Alias { get; set; }
     public String Pin_Code { get; set; }

 }
}
