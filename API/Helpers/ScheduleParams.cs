using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class ScheduleParams
    {
        public bool IsInitial { get; set; }
        public int LoadDirection { get; set; }
        public int PageSize { get; set; }
        public DateTime Date { get; set; }
    }
}