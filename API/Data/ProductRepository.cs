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
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProductRepository(DataContext dataContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._dataContext = dataContext;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _mapper.ProjectTo<ProductDto>(_dataContext.Products).ToListAsync();
            
            return products;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _dataContext.Products.FindAsync(productId);
        }

        public void UpdateProduct(Product product)
        {
            _dataContext.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(Product product)
        {
            _dataContext.Remove(product);
        }
    }
}