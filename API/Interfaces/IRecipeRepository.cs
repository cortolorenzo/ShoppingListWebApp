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
         Task<RecipeDto> GetRecipeDtoByIdAsync(int productId);
         void UpdateRecipe(Recipe recipe);

         void DeleteRecipe(Recipe recipe);

         void AddRecipe(Recipe recipe);


         
    }
}