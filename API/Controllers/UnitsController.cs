using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
            var units = await unitOfWork.UnitRepository.GetUnitsAsync(User.GetUserId());
            return Ok(units);
        }


        [HttpDelete("{unitName}")]
        public async Task<ActionResult> DeleteUnit(string unitName)
        {
            var unit = await unitOfWork.UnitRepository.GetUnitByUnitName(unitName, User.GetUserId());
            unitOfWork.UnitRepository.DeleteUnit(unit);

            if( await unitOfWork.UnitRepository.IsUnitUsed(unitName, User.GetUserId()))
                return BadRequest("Unit: " + unitName +", is already used in some product definition." +
                
                " If you want to delete it please firstly delete the corresponding product.");

            
            if (await unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting unit");
        }

        [HttpPost("{unitName}")]
        public async Task<ActionResult> AddUnit(string unitName)
        {
            var unit = await unitOfWork.UnitRepository.GetUnitByUnitName(unitName, User.GetUserId());
            var user = await unitOfWork.UserRepository.GetUserByNameAsync(User.GetUsername());

            if (unit == null)
            {
                unit = new Unit(unitName);
                unit.User = user;
                unit.UserId = User.GetUserId();
                unitOfWork.UnitRepository.AddUnit(unit);
                 
                if (await unitOfWork.Complete()) 
                    return Ok(unit);
            }
              
            return BadRequest("Unit already exists");

        }

    }
}