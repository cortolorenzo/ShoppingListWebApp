using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {

            var products = await unitOfWork.ProductRepository.GetProductsAsync(User.GetUserId());
            return Ok(products);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductDto productDto)
        {
            //check if new unit was added
            var unit = await unitOfWork.UnitRepository.GetUnitByUnitName(productDto.UnitName, User.GetUserId());
            var user = await unitOfWork.UserRepository.GetUserByNameAsync(User.GetUsername());

            if (unit == null)
            {
                unit = new Unit(productDto.UnitName);
                unit.User = user;
                unit.UserId = User.GetUserId();
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

            if ( await unitOfWork.ProductRepository.IsProductUsed(productId))
                return BadRequest("Product: " + product.ProductName +", is already used in some recipe definition." +
                
                " If you want to delete it please firstly delete it from the corresponding recipe.");

            if (await unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting product");
        }


        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductDto productDto)
        {
            var unit = await unitOfWork.UnitRepository.GetUnitByUnitName(productDto.UnitName, User.GetUserId());
            var user = await unitOfWork.UserRepository.GetUserByNameAsync(User.GetUsername());

            if (unit == null)
            {
                unit = new Unit(productDto.UnitName);
                unit.User = user;
                unit.UserId = User.GetUserId();
                unitOfWork.UnitRepository.AddUnit(unit);
            }
                
            
            var product = new Product(productDto.ProductName, unit);
            product.User = user;
            product.UserId = User.GetUserId();
           
            unitOfWork.ProductRepository.AddProduct(product);
            
            if (await unitOfWork.Complete()) 
                return Ok();
            return BadRequest("Failed to add product");

        }

    }
}