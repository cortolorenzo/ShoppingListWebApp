using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UnitsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public UnitsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetUnits()
        {
            var units = await unitOfWork.ProductRepository.GetUnitsAsync();
            return Ok(units);
        }


          [HttpDelete("{unitName}")]
        public async Task<ActionResult> DeleteUnit(string unitName)
        {
            var unit = await unitOfWork.ProductRepository.GetUnitByUnitName(unitName);
            unitOfWork.ProductRepository.DeleteUnit(unit);

            if (await unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting unit");
        }

        [HttpPost("{unitName}")]
        public async Task<ActionResult> AddUnit(string unitName)
        {
            var unit = await unitOfWork.ProductRepository.GetUnitByUnitName(unitName);

            if (unit == null)
            {
                unit = new Unit(unitName);
                unitOfWork.ProductRepository.AddUnit(unit);
                 
                if (await unitOfWork.Complete()) 
                    return Ok(unit);
            }
              
            return BadRequest("Unit already exists");

        }

    }
}