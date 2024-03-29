using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<ScheduleDto>> GetSchedulesDtoByDate (ScheduleParams scheduleParams, int UserId);
        void DeleteScheduleRecipe(ScheduleRecipe scheduleRecipe);
        Task<ScheduleRecipe?> GetScheduleRecipeByIdAsync(int scheduleRecipeId);
        void AddScheduleRecipe(ScheduleRecipe scheduleRecipe);
        void UpdateScheduleRecipe (ScheduleRecipe scheduleRecipe);

        Task<bool> IsRecipeUsed(int recipeId);
    }
}