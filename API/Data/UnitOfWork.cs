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
    }
}