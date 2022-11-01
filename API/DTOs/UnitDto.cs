using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UnitDto
    {
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int UserId { get; set; }
    }
}