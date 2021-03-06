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
            

            CreateMap<Recipe, RecipeDto>();
            
            CreateMap<RecipeProduct, RecipeProductDto>()
                // .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.Recipe.RecipieName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            
            CreateMap<RecipeProductDto, RecipeProduct>();
        }
        
        
    }
}