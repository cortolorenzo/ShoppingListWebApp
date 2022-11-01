using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Entities
{
    public class ScheduleRecipe
    {
        public ScheduleRecipe()
        {
        }

        public ScheduleRecipe(int scheduleId, int recipeId, int quantity, string? recipeName)
        {
            ScheduleId = scheduleId;
            RecipeId = recipeId;
            Quantity = quantity;
            RecipeName = recipeName;
        }

        public ScheduleRecipe(int scheduleId, int recipeId, int quantity)
        {
            ScheduleId = scheduleId;
            RecipeId = recipeId;
            Quantity = quantity;
            
        }

        public int ScheduleRecipeId { get; set; }
        public int ScheduleId { get; set; }
        public int RecipeId { get; set; }
        public int Quantity { get; set; }

        public string? RecipeName { get; set; } 


        [JsonIgnore]
        public Schedule Schedule { get; set; }
         [JsonIgnore]
        public Recipe Recipe { get; set; }
        
     
    }

}