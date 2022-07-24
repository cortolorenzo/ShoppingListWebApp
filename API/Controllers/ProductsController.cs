using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await unitOfWork.ProductRepository.GetProductsAsync();
            return Ok(products);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductDto productDto)
        {
            //check if new unit was added
            var unit = await unitOfWork.UnitRepository.GetUnitByUnitName(productDto.UnitName);
            if (unit == null)
            {
                unit = new Unit(productDto.UnitName);
                unitOfWork.UnitRepository.AddUnit(unit);
            }
            
            var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productDto.ProductId);
            _mapper.Map(productDto, product);

            product.Unit = unit;

            unitOfWork.ProductRepository.UpdateProduct(product);

            if (await unitOfWork.Complete()) return NoContent();
            return BadRequest("Failes to update product");

        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productId);
            unitOfWork.ProductRepository.DeleteProduct(product);

            if (await unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting product");
        }


        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductDto productDto)
        {
            var unit = await unitOfWork.UnitRepository.GetUnitByUnitName(productDto.UnitName);

            if (unit == null)
            {
                unit = new Unit(productDto.UnitName);
                unitOfWork.UnitRepository.AddUnit(unit);
            }
                
            
            var product = new Product(productDto.ProductName, unit);
           
            unitOfWork.ProductRepository.AddProduct(product);
            
            if (await unitOfWork.Complete()) 
                return Ok();
            return BadRequest("Failed to send message");

        }

    }
}