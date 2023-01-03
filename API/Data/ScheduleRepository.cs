using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ScheduleRepository : IScheduleRepository
    {

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ScheduleRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public void AddScheduleRecipe(ScheduleRecipe scheduleRecipe)
        {
            _dataContext.Add(scheduleRecipe);
        }

        public void DeleteScheduleRecipe(ScheduleRecipe scheduleRecipe)
        {
            _dataContext.ScheduleRecipes.Remove(scheduleRecipe);
        }

        public async Task<ScheduleRecipe?> GetScheduleRecipeByIdAsync(int scheduleRecipeId)
        {
            return await _dataContext.ScheduleRecipes.FindAsync(scheduleRecipeId);
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesDtoByDate(ScheduleParams scheduleParams, int UserId)
        {
            
            DateTime dateMin ;
            DateTime dateMax;

            

            if (scheduleParams.IsInitial)
            {
                 dateMin = DateTime.SpecifyKind(scheduleParams.Date.AddDays(-scheduleParams.PageSize/2), DateTimeKind.Utc);
                 dateMax = DateTime.SpecifyKind(scheduleParams.Date.AddDays(scheduleParams.PageSize/2), DateTimeKind.Utc);
            }
            else
            {
                if(scheduleParams.LoadDirection == 1)
                {
                    dateMin = DateTime.SpecifyKind(scheduleParams.Date, DateTimeKind.Utc);
                    dateMax = DateTime.SpecifyKind(scheduleParams.Date.AddDays(scheduleParams.PageSize/2), DateTimeKind.Utc);
                }
                else
                {
                    dateMin = DateTime.SpecifyKind(scheduleParams.Date.AddDays(-scheduleParams.PageSize/2), DateTimeKind.Utc);
                    dateMax = DateTime.SpecifyKind(scheduleParams.Date, DateTimeKind.Utc);
                }
            }

            var query = _dataContext.Schedules
                    .Where(s => s.ScheduleDate.Date >= dateMin.Date
                                                && s.ScheduleDate.Date <= dateMax.Date) 
                    .Include(sr => sr.ScheduleRecipes.Where(u => u.UserId == UserId)).AsQueryable();
            var result = await query.ToListAsync();
            return  _mapper.ProjectTo<ScheduleDto>(result.AsQueryable()).ToList();
            
#region test queries
            // var query = 
            // from s in _dataContext.Schedules.Where(x => x.ScheduleRecipes.Where(y => y.Recipe.UserId == UserId).Count() > 0)
            // join d in _dataContext.ScheduleRecipes
            //         on s.ScheduleId equals d.ScheduleId into grouping
            //         from d in grouping.DefaultIfEmpty()
            // where 
            //  s.ScheduleDate.Date >= dateMin.Date
            // && s.ScheduleDate.Date <= dateMax.Date
            // orderby s.ScheduleId
            // select s;

// var query = (from x in _dataContext.Schedules
//              join y in _dataContext.ScheduleRecipes on 
//              new  {
//                   Key1 = (int?)x.ScheduleId, 
//                   Key2 = x.ScheduleDate >= dateMin.Date,
//                   Key3 = x.ScheduleDate <= dateMin.Date,
//                   Key4 = UserId
//                  }
//              equals
//              new {
//                   Key1 = (int?)y.ScheduleId, 
//                   Key2 =  true,
//                   Key3 = true,
//                   Key4 = y.Recipe.UserId
//                  }  
               
//              into result
// from r in result.DefaultIfEmpty()
// select new {r.Schedule.ScheduleId
//                 ,r.Schedule.ScheduleDate
//                     ,r.ScheduleRecipeId
//                         ,r.RecipeName
//                             ,r.RecipeId
//                                 ,r.Quantity});


// var query = (from s in _dataContext.Schedules 
//                 join sr in _dataContext.ScheduleRecipes 
//                 on s.ScheduleId equals sr.ScheduleId
//                 into Tempp
//             from temp in Tempp.DefaultIfEmpty() 
//             where temp.Recipe.UserId == UserId
//             select s).AsQueryable();

// var query = (from s in _dataContext.Schedules 
//                 join sr in _dataContext.ScheduleRecipes on s.ScheduleId equals sr.ScheduleId
//                 into Tempp
//             from temp in Tempp.Where(x => x.Quantity == 1).DefaultIfEmpty() 
//             select new {s.ScheduleDate}).AsQueryable();

// var query = (from s in _dataContext.Schedules.Where(s => s.ScheduleDate.Date >= dateMin.Date
//                                                      && s.ScheduleDate.Date <= dateMax.Date) 
//                 from sr in _dataContext.ScheduleRecipes.Where(sr => sr.ScheduleId == s.ScheduleId 
//                                                                  && sr.Recipe.UserId == UserId
   
//             ).DefaultIfEmpty()
//             select  s).AsQueryable();


// var query = _dataContext.Schedules.FromSqlInterpolated
//             ($"SELECT t0.ScheduleId, t0.ScheduleDate, t3.ScheduleRecipeId, t3.RecipeId, t3.RecipeName, t3.Quantity FROM Schedules t0 LEFT JOIN (SELECT t1.ScheduleId, t1.ScheduleRecipeId, t2.RecipeId, t2.RecipeName, t1.Quantity from ScheduleRecipes t1 INNER JOIN Recipes t2 on t1.RecipeId = t2.RecipeId WHERE  t2.UserId = {UserId}) t3 on t3.ScheduleId = t0.ScheduleId WHERE t0.ScheduleDate >= \"{dateMin.Date}\" AND t0.ScheduleDate <= \"{dateMax.Date}\"").AsQueryable();
             

        

// var query =  _dataContext.Schedules
// .Include(sr => sr.ScheduleRecipes)
// .ThenInclude(r => r.Recipe)
// .Where(sr.)

// .Where(r => r.Recipe.UserId == UserId).DefaultIfEmpty()).AsQueryable();
                                                
          

    // var result =
    // from customer in db.Customers
    // join order in db.Orders
    // on customer.Custid equals order.Custid
    // into Abc
    // from abc in Abc.Where(abc => abc.Orderdate == new DateTime(2016, 2, 12)).DefaultIfEmpty()
    // select new
    // {
    //     customer.Cust
    //     customer.Companyname,
    //     Orderid = abc == null ? -1 : abc.Orderid,
    //     Orderdate = abc == null ? new DateTime() : abc.Orderdate
    // };
         
             
             
            //  s.ScheduleId equals sr.ScheduleId


            //  and sr.Recipe.UserId == UserId  into UserColor
            //  from q in UserColor.DefaultIfEmpty() join u in dbo.Users 
            //  on q.UserID equals u.UserID into Users
            //  from l in Users.DefaultIfEmpty()
            //  select new
            //    {
            //      ColorID = c.ColorID,
            //      ColorName = c.ColorName,
            //      IsSelected = uc.ColorID == null ? 0 : 1
            //    };




            // var query = 
            // from s in _dataContext.Schedules.Join(_dataContext.ScheduleRecipes,
            // s => new {ScheduleId = s.ScheduleId, UserId = UserId, z = s.ScheduleDate.Date >= dateMin.Date, y = s.ScheduleDate.Date  <= dateMax.Date },
            // sr => new {sr.ScheduleId, sr.Recipe.UserId, true, true },
            // (s, sr) => new {u = s.ScheduleId, m = sr.ScheduleId}).Where()
            
            // where 
            //  s.ScheduleDate.Date >= dateMin.Date
            // && s.ScheduleDate.Date <= dateMax.Date
            // orderby s.ScheduleId
            // select s;

            //query.AsQueryable();
           
           
            // return await query
            //                   .ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider)
            //                   .ToListAsync();
   
       #endregion 

        }

        public async Task<bool> IsRecipeUsed(int recipeId)
        {
            var schedulesWithRecipe = await _dataContext.ScheduleRecipes
                                    .Where(x => x.RecipeId == recipeId)
                                    .ToListAsync();
            if (schedulesWithRecipe.Any())
                return true;
            return false;
        }

        public void UpdateScheduleRecipe(ScheduleRecipe recipe)
        {
            _dataContext.Entry(recipe).State = EntityState.Modified;
        }

    }
}