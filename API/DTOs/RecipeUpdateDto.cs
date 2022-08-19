using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RecipeUpdateDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string? RecipeDescription { get; set; }
    }
}