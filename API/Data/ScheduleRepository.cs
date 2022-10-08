using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ScheduleRepository : IScheduleRepository
    {

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ScheduleRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ScheduleDto> GetScheduleDtoByDate(DateTime date)
        {
            
            var scheduleDto = await _mapper.ProjectTo<ScheduleDto>(from log in _dataContext.Schedules
                        where log.ScheduleDate.Date == date
                       select log)
                       .FirstOrDefaultAsync();

            return scheduleDto;
              
        }
    }
}