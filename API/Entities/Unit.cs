using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Unit
    {
      public Unit(){}

      public Unit(string unitName)
      {
        UnitName = unitName;
      }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }

      public ICollection<Product> Products { get; set; }
      public AppUser User { get; set; }
      public int UserId { get; set; }
    }
}