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
            var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productDto.ProductId);
            _mapper.Map(productDto, product);

            unitOfWork.ProductRepository.UpdateProduct(product);
            if (await unitOfWork.Complete()) return NoContent();
            return BadRequest("Failes to update product");

        }
    }
}