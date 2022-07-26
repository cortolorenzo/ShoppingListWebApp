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

        public void AddProduct(Product product)
        {
            _dataContext.Add(product);
            
            
        }

        public async Task<Unit> GetUnitByUnitName(string unitName)
        {
            return await _dataContext.Units.SingleOrDefaultAsync(x => x.UnitName == unitName);
        }

        public void AddUnit(Unit unit)
        {
            _dataContext.Add(unit);
        }

        public async Task<IEnumerable<UnitDto>> GetUnitsAsync()
        {
            var units = await _mapper.ProjectTo<UnitDto>(_dataContext.Units).ToListAsync();
            
            return  units;
        }

        public void DeleteUnit(Unit unit)
        {
            _dataContext.Remove(unit);
        }

        public async Task<bool> IsProductUsed(int productId)
        {
            var recipeProducts = await _dataContext.RecipeProducts
                                    .Where(x => x.ProductId == productId)
                                    .ToListAsync();
            if (recipeProducts.Any())
                return true;
            return false;
        }
    }
}