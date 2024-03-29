using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SchedulesController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public SchedulesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedule([FromQuery] ScheduleParams scheduleParams)
        {
            var schedulesDto = await unitOfWork.ScheduleRepository.GetSchedulesDtoByDate(scheduleParams, User.GetUserId());
            return Ok(schedulesDto);
        }

         [HttpDelete("{scheduleRecipeId}")]
        public async Task<ActionResult> DeleteScheduleRecipe(int scheduleRecipeId)
        {
            var scheduleRec = await unitOfWork.ScheduleRepository.GetScheduleRecipeByIdAsync(scheduleRecipeId);
            unitOfWork.ScheduleRepository.DeleteScheduleRecipe(scheduleRec);

            if (await unitOfWork.Complete()) return Ok();
            return BadRequest("Problem deleting schedule recipe");
        }

        [HttpPost("add-schedule-recipes")]
        public async Task<ActionResult> AddScheduleRecipes(List<ScheduleRecipeDto> scheduleRecipes)
        {
            var user = await unitOfWork.UserRepository.GetUserByNameAsync(User.GetUsername());
            foreach (var sr in scheduleRecipes)
            {
                ScheduleRecipe newScheduleRecipe = 
                new ScheduleRecipe(sr.ScheduleId, sr.RecipeId, sr.Quantity, sr.RecipeName);
                var recipe = await unitOfWork.RecipeRepository.GetRecipeByIdAsync(sr.RecipeId);
                newScheduleRecipe.Recipe = recipe;
                newScheduleRecipe.User = user;
                newScheduleRecipe.UserId = User.GetUserId();

                unitOfWork.ScheduleRepository.AddScheduleRecipe(newScheduleRecipe);
            }

            if (await unitOfWork.Complete()) 
                return Ok();
            return BadRequest("Failed to add recipe products");
          
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSchedule(ScheduleDto scheduleDto)
        {

            foreach(var sr in scheduleDto.ScheduleRecipes)
            {
               var scheduleRec = await unitOfWork.ScheduleRepository.GetScheduleRecipeByIdAsync(sr.ScheduleRecipeId);
               scheduleRec.Quantity = sr.Quantity;
               unitOfWork.ScheduleRepository.UpdateScheduleRecipe(scheduleRec);
            }

            if (await unitOfWork.Complete()) return NoContent();
            return BadRequest("Failes to update schdedule");
        }

    }
}