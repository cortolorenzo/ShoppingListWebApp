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
        Task<IEnumerable<RecipeDto>> GetRecipesAsync(int UserId);
    
         Task<Recipe> GetRecipeByIdAsync(int productId);
    
         Task<RecipeProduct> GetRecipeProductByIdAsync(int recipeProductId);
         Task<RecipeDto> GetRecipeDtoByIdAsync(int productId);
         void UpdateRecipe(Recipe recipe);
         void UpdateRecipeProduct(RecipeProduct recipe);

         void DeleteRecipe(Recipe recipe);
         void DeleteRecipeProduct(RecipeProduct recipeProduct);

         void AddRecipe(Recipe recipe);

        void AddRecipeProduct(RecipeProduct recipeProduct);


         
    }
}