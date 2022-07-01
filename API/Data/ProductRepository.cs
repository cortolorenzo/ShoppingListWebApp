using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        public ProductRepository(DataContext dataContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await mapper.ProjectTo<ProductDto>(dataContext.Products).ToListAsync();
            
            return products;
        }
    }
}