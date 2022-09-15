using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class RecipeProduct
    {

      public RecipeProduct(){}

      public RecipeProduct(Recipe recipe, Product product){
        Recipe = recipe;
        Product = product;
      }

        public RecipeProduct(int recipeId, int productId, double quantity) 
        {
            this.RecipeId = recipeId;
            this.ProductId = productId;
            this.Quantity = quantity;
   
        }
        
        public int RecipeProductId { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string? UnitName { get; set; }
        public double Quantity { get; set; }
        
        
    }
}