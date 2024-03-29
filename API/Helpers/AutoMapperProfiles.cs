using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.UnitName,opt => opt.MapFrom(src => src.Unit.UnitName));
            CreateMap<ProductDto, Product>();
            CreateMap<Unit, UnitDto>();

            CreateMap<RecipeUpdateDto, Recipe>();
            CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                src.Photos.FirstOrDefault(x => x.IsMain).Url));
              

            CreateMap<Photo,PhotoDto>();
            CreateMap<RecipeDto, Recipe>();
            
            
            CreateMap<RecipeProduct, RecipeProductDto>()
                // .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.Recipe.RecipieName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
                
                
            
            CreateMap<RecipeProductDto, RecipeProduct>();
            CreateMap<Schedule,ScheduleDto>();
            CreateMap<ScheduleRecipe,ScheduleRecipeDto>();
            CreateMap<RegisterDto, AppUser>();
      
            
            
        }
        
        
    }
}