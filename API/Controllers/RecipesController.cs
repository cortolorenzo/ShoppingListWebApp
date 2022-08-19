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
            var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(recipeId);
            var recipeDto = _mapper.Map<RecipeDto>(recipe);
            
            return Ok(recipeDto);
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
            _mapper.Map(recipeUpdateDto,recipe);

            unitOfWork.RecipeRepository.UpdateRecipe(recipe);

            if (await unitOfWork.Complete()) return NoContent();
            return BadRequest("Failes to update recipe");
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