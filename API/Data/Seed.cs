using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class Seed
    {

        public static async Task SeedData(DataContext dataContext)
        {
            if (dataContext.Products.Any())
                Clear(dataContext.Products);
            if (dataContext.Units.Any())
                Clear(dataContext.Units);
            
            
            await SeedUnits(dataContext);
            await SeedProducts(dataContext);
        }

        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }


        public static async Task SeedUnits(DataContext dataContext)
        {

            try
            {
                var units = new List<Unit>
                {
                    new Unit{UnitId = 1, UnitName =  "kg"},
                    new Unit{UnitId = 2, UnitName = "g"},
                    new Unit{UnitId = 3, UnitName = "l"},
                    new Unit{UnitId = 4, UnitName = "ml"},
                    new Unit{UnitId = 5, UnitName = "pcs."}

                };

                foreach (var unit in units)
                {
                    dataContext.Units.Add(unit);
                    await dataContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {

            }
            

        }

        public static async Task SeedProducts(DataContext dataContext)
        {
            
            try
            {
                var products = new List<Product>
                {
                    new Product {ProductId = 1,ProductName = "Carrot", UnitId = 1},
                    new Product {ProductId = 2,ProductName = "Potato", UnitId = 1},
                    new Product {ProductId = 3,ProductName = "Milk", UnitId = 4}
                };

                foreach ( var product in products)
                {
                    await dataContext.AddAsync(product);
                    await dataContext.SaveChangesAsync();
                }
            }
            catch (System.Exception)
            {
                
                
            }
            
        }

    }
}