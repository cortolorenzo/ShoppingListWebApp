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

         Task<Product?> GetProductByIdAsync(int productId);
         void UpdateProduct(Product product);

         void DeleteProduct(Product product);
    }
}