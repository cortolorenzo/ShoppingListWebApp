using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<RecipeProductDto> RecipeProducts { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        
    }
}