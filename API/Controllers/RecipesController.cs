using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecipesController: BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        private readonly IPhotoService _photoService;

        public RecipesController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            this.unitOfWork = unitOfWork;
            this._photoService = photoService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            var recipes = await unitOfWork.RecipeRepository.GetRecipesAsync();
            return Ok(recipes);
        }


        [HttpGet("{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int recipeId)
        {
            var recipeDto = await unitOfWork.RecipeRepository.GetRecipeDtoByIdAsync(recipeId);
            //var recipeDto = _mapper.Map<RecipeDto>(recipe);
            
            return Ok(recipeDto);
        }

        [HttpDelete("{recipeId}")]
        public async Task<ActionResult> DeleteRecipe(int recipeId)
        {
            var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(recipeId);
            unitOfWork.RecipeRepository.DeleteRecipe(recipe);

            if (await unitOfWork.Complete()) return Ok();
            return BadRequest("Problem deleting recipe");
        }

        
        [HttpDelete("del-recipe-product/{recipeProductId}")]
        public async Task<ActionResult> DeleteRecipeProduct(int recipeProductId)
        {
            var recipeProduct = await unitOfWork.RecipeRepository.GetRecipeProductByIdAsync(recipeProductId);
            unitOfWork.RecipeRepository.DeleteRecipeProduct(recipeProduct);

            if (await unitOfWork.Complete()) return Ok();
            return BadRequest("Problem deleting recipe product");
        }

        

        [HttpPost("add-photo/{recipeId}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(int recipeId, IFormFile file)
        {
            var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(recipeId);
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (recipe.Photos.Count == 0)
            {
                
                photo.IsMain = true;
            }

            recipe.Photos.Add(photo);

            if (await unitOfWork.Complete())
            {
                return CreatedAtRoute("GetRecipe", new { recipeId = recipe.RecipeId }, _mapper.Map<PhotoDto>(photo));

            }


            return BadRequest("Problem adding photo");

        }

        [HttpPut]
        public async Task<ActionResult> UpdateRecipe(RecipeUpdateDto recipeUpdateDto)
        {
            var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(recipeUpdateDto.RecipeId);
            recipe.RecipeName = recipeUpdateDto.RecipeName;
            recipe.RecipeDescription = recipeUpdateDto.RecipeDescription;
            
            //_mapper.Map(recipeUpdateDto,recipe);
            foreach(var rp in recipeUpdateDto.RecipeProducts)
            {
                var recipeProduct2Update = await unitOfWork.RecipeRepository.GetRecipeProductByIdAsync(rp.RecipeProductId);
                _mapper.Map(rp,recipeProduct2Update);
                unitOfWork.RecipeRepository.UpdateRecipeProduct(recipeProduct2Update);

            }

            unitOfWork.RecipeRepository.UpdateRecipe(recipe);

            if (await unitOfWork.Complete()) return NoContent();
            return BadRequest("Failes to update recipe");
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddRecipe(RecipeUpdateDto recipeUpdateDto)
        {
            var recipe = new Recipe(recipeUpdateDto.RecipeName, recipeUpdateDto.RecipeDescription);
            unitOfWork.RecipeRepository.AddRecipe(recipe);

            if (await unitOfWork.Complete()) 
                return Ok(recipe.RecipeId);
            return BadRequest("Failed to add recipe");
          
        }

        [HttpPost("add-recipe-products")]
        public async Task<ActionResult> AddRecipeProducts(List<RecipeProductDto> recipeProductDto)
        {
            foreach (var rp in recipeProductDto)
            {
                RecipeProduct newRp = new RecipeProduct(rp.RecipeId, rp.ProductId, 0.0);
                newRp.UnitName = rp.UnitName;
                unitOfWork.RecipeRepository.AddRecipeProduct(newRp);
            }

            if (await unitOfWork.Complete()) 
                return Ok();
            return BadRequest("Failed to add recipe products");
          
        }

        [HttpPut("{recipeId}/set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int recipeId, int photoId)
        {
            var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(recipeId);
            var photo = recipe.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain) return BadRequest("This is already your main photo");
            var currentMain = recipe.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to set main photo");
        }


        [HttpDelete("{recipeId}/delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int recipeId, int photoId)
        {
            var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(recipeId);
            var photo = recipe.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();
            if (photo.IsMain) return BadRequest("You can't delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);


            }

            recipe.Photos.Remove(photo);
            if (await unitOfWork.Complete()) return Ok();
            return BadRequest("Failed to delete a photo");


        }


    }
}