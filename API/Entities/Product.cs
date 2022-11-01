using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Product
    {
        public Product()
        {
        }

        public Product( string productName, Unit unit)
        {
        
            ProductName = productName;
           
            Unit = unit;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        
        
        public int? UnitId { get; set; }

//        [JsonIgnore]
        public Unit Unit { get; set; }

        public  ICollection<RecipeProduct> RecipeProducts { get; set; }

        public AppUser User { get; set; }
        public int UserId { get; set; }

    }
}