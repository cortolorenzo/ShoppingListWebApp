using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context { get; }
        private IMapper mapper { get; }
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public IUserRepository UserRepository => new UserRepository(context, mapper);
        public IProductRepository ProductRepository => new ProductRepository(context, mapper);

        public IUnitRepository UnitRepository => new UnitRepository(context, mapper);
        public IRecipeRepository RecipeRepository => new RecipeRepository(context, mapper);
        public IScheduleRepository ScheduleRepository => new ScheduleRepository(context, mapper);

         public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}