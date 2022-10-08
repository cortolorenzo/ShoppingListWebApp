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
    public class SchedulesController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public SchedulesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{scheduleDate}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(DateTime scheduleDate)
        {
            var scheduleDto = await unitOfWork.ScheduleRepository.GetScheduleDtoByDate(scheduleDate);
            return Ok(scheduleDto);
        }

    }
}