using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }
        public string? UnitName { get; set; }
        public int UserId { get; set; }
        
    }
}