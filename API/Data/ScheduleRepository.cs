using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
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

        public void AddScheduleRecipe(ScheduleRecipe scheduleRecipe)
        {
            _dataContext.Add(scheduleRecipe);
        }

        public void DeleteScheduleRecipe(ScheduleRecipe scheduleRecipe)
        {
            _dataContext.ScheduleRecipes.Remove(scheduleRecipe);
        }

        public async Task<ScheduleRecipe?> GetScheduleRecipeByIdAsync(int scheduleRecipeId)
        {
            return await _dataContext.ScheduleRecipes.FindAsync(scheduleRecipeId);
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesDtoByDate(ScheduleParams scheduleParams)
        {
            var query = _dataContext.Schedules.AsQueryable();
            DateTime dateMin;
            DateTime dateMax;

            if (scheduleParams.IsInitial)
            {
                 dateMin = scheduleParams.Date.AddDays(-scheduleParams.PageSize/2);
                 dateMax = scheduleParams.Date.AddDays(scheduleParams.PageSize/2);
            }
            else
            {
                if(scheduleParams.LoadDirection == 1)
                {
                    dateMin = scheduleParams.Date;
                    dateMax = scheduleParams.Date.AddDays(scheduleParams.PageSize/2);
                }
                else
                {
                    dateMin = scheduleParams.Date.AddDays(-scheduleParams.PageSize/2);
                    dateMax = scheduleParams.Date;
                }
            }

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