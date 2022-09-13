using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public RecipeRepository(DataContext dataContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._dataContext = dataContext;
        }

        public void AddRecipe(Recipe recipe)
        {
            _dataContext.Add(recipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _dataContext.Remove(recipe);
        }

        
        public async Task<Recipe> GetRecipeByIdAsync(int recipeId)
        {
            return await _dataContext.Recipes
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.RecipeId == recipeId);
 
        }

        public async Task<RecipeDto> GetRecipeDtoByIdAsync(int recipeId)
        {
            return await _mapper.ProjectTo<RecipeDto>(_dataContext.Recipes
            .Include(p => p.Photos)
            .Include(p => p.RecipeProducts))
            .FirstOrDefaultAsync(x => x.RecipeId == recipeId);
 
        }


        public async Task<IEnumerable<RecipeDto>> GetRecipesAsync()
        {
            var recipes = await _mapper.ProjectTo<RecipeDto>
                (_dataContext.Recipes
                    .Include(p => p.Photos)
                    .Include(p => p.RecipeProducts))
                    .ToListAsync();
            
            return recipes;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _dataContext.Entry(recipe).State = EntityState.Modified;
        }
    }
}