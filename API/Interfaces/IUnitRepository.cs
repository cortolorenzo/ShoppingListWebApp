using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUnitRepository
    {
        void DeleteUnit(Unit unit);
        Task<IEnumerable<UnitDto>> GetUnitsAsync(int UserId);
        void AddUnit(Unit unit);

        Task<bool> IsUnitUsed(string unitName, int UserId);

        Task <Unit> GetUnitByUnitName(string unitName, int UserId);
    }
}