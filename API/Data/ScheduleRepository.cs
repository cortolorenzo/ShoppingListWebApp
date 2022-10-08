using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesDtoByDate(DateTime date)
        {
            
            var dateMin = date.AddDays(-7);
            var dateMax = date.AddDays(7);

            var query = _dataContext.Schedules.AsQueryable();
            query = query.Where(d => d.ScheduleDate >= dateMin);
            query = query.Where(d => d.ScheduleDate <= dateMax);
            query.OrderBy(o => o.ScheduleId);
            
            return await query.ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
            
            // var scheduleDto = await _mapper.ProjectTo<ScheduleDto>(from log in _dataContext.Schedules
            //             where log.ScheduleDate.Date == date
            //            select log)
            //            .FirstOrDefaultAsync();

            // return scheduleDto;


              
        }
    }
}