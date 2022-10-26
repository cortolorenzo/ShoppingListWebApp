using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedule([FromQuery] ScheduleParams scheduleParams)
        {
            var schedulesDto = await unitOfWork.ScheduleRepository.GetSchedulesDtoByDate(scheduleParams);
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

    }
}