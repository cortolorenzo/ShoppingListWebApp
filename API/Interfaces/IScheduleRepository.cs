using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<ScheduleDto>> GetSchedulesDtoByDate (ScheduleParams scheduleParams);
    }
}