using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<RecipeDto>> GetRecipesAsync();
    
         Task<Recipe> GetRecipeByIdAsync(int productId);
         void UpdateRecipe(Recipe product);

         void DeleteRecipe(Recipe product);

         void AddRecipe(Recipe product);


         
    }
}