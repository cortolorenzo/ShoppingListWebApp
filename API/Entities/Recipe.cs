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
            RecipieName = recipieName;
        }


        public int RecipieId { get; set; }
        public string RecipieName { get; set; }
        public string RecipeDescription { get; set; }
        

       

        public  ICollection<RecipeProduct> RecipeProducts { get; set; }
    }
}