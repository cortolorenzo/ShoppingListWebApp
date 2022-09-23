using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RecipeProductDto
    {
        public int RecipeProductId { get; set; }
        public int ProductId { get; set; }
        public int RecipeId { get; set; }
        public string ProductName { get; set; }
        public string? UnitName { get; set; }
        public double Quantity { get; set; }
    }
}