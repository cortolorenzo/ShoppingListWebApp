using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public ICollection<ScheduleRecipe> ScheduleRecipes { get; set; }

        

       
    }
}