using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Schedule
    {
        public Schedule()
        {
        }

        public Schedule(int scheduleId, DateTime scheduleDate)
        {
            this.ScheduleId = scheduleId;
            ScheduleDate = scheduleDate;
        }

     
        public int ScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public ICollection<ScheduleRecipe> ScheduleRecipes { get; set; }


        

       
    
    }
}