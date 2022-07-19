using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<IEnumerable<UnitDto>> GetUnitsAsync();

         Task<Product?> GetProductByIdAsync(int productId);
         void UpdateProduct(Product product);

         void DeleteProduct(Product product);

         void DeleteUnit(Unit unit);

         void AddProduct(Product product);

         Task <Unit> GetUnitByUnitName(string unitName);

         void AddUnit(Unit unit);
    }
}