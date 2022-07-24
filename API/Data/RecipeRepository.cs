using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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

        public void AddRecipe(Recipe product)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecipe(Recipe product)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipeDto?> GetRecipeByIdAsync(int productId)
        {
           throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecipeDto>> GetRecipesAsync()
        {
            var recipes = await _mapper.ProjectTo<RecipeDto>(_dataContext.Recipes).ToListAsync();
            
            return recipes;
        }

        public void UpdateRecipe(Recipe product)
        {
            throw new NotImplementedException();
        }
    }
}