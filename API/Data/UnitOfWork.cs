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
        public DataContext Context { get; }
        public IMapper Mapper { get; }
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Context = context;
        }

        public IProductRepository ProductRepository => new ProductRepository(Context, Mapper);

        public IUnitRepository UnitRepository => new UnitRepository(Context, Mapper);
        public IRecipeRepository RecipeRepository => new RecipeRepository(Context, Mapper);

         public async Task<bool> Complete()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }
    }
}