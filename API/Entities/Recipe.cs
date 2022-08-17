using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Recipe
    {
        public Recipe(string recipieName)
        {
            RecipeName = recipieName;
        }

        public Recipe()
        {
            
        }


        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string? RecipeDescription { get; set; }
        
        public ICollection<Photo> Photos { get; set; }

       

        public  ICollection<RecipeProduct> RecipeProducts { get; set; }
    }
}