using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public DataContext Context { get; }
        public UnitOfWork(DataContext context)
        {
            this.Context = context;
        }

        public IProductRepository ProductRepository => new ProductRepository(Context);
    }
}