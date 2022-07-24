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
    public class UnitRepository : IUnitRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public UnitRepository(DataContext dataContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._dataContext = dataContext;
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

        public async Task<bool> IsUnitUsed(string unitName)
        {
            var products = await _dataContext.Products
                            .Where(x => x.Unit.UnitName == unitName)
                            .ToListAsync();

            if (products.Any())
                return true;
            return false;

            
        }
    }
}
