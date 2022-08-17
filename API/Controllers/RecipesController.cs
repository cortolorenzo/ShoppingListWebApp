using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecipesController: BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public RecipesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            var recipes = await unitOfWork.RecipeRepository.GetRecipesAsync();
            return Ok(recipes);
        }


        [HttpGet("{recipeId}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int recipeId)
        {
            var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(recipeId);
            return Ok(recipe);
        }


    }
}