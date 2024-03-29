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
            this.Photos = new List<Photo>();
        }

         public Recipe(string recipieName, string? recipeDesc, AppUser user)
        {
            RecipeName = recipieName;
            RecipeDescription = recipeDesc; 
            this.Photos = new List<Photo>();
            User = user;
            UserId = user.Id;
        }

        public Recipe()
        {
            this.Photos = new List<Photo>();
        }


        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string? RecipeDescription { get; set; }
        
        public ICollection<Photo> Photos { get; set; }

       

        public  ICollection<RecipeProduct> RecipeProducts { get; set; }
        public ICollection<ScheduleRecipe> ScheduleRecipes { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }
    }
}