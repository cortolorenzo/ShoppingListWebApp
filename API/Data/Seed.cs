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
            // if (dataContext.Products.Any())
            //     Clear(dataContext.Products);
                
            // if (dataContext.Units.Any())
            //     Clear(dataContext.Units);

            // if (dataContext.RecipeProducts.Any())
            //     Clear(dataContext.RecipeProducts);

            // if (dataContext.Recipes.Any())
            //     Clear(dataContext.Recipes);


            
            
            // await SeedUnits(dataContext);
            // await SeedProducts(dataContext);
            // await SeedRecipes(dataContext);
            // await SeedRecipesProducts(dataContext);
        }

        private static async Task SeedRecipesProducts(DataContext dataContext)
        {
            try
            {
                var recipeProducts = new List<RecipeProduct>
                {

                    // new Unit{UnitId = 1, UnitName =  "kg"},
                    // new Unit{UnitId = 2, UnitName = "g"},
                    // new Unit{UnitId = 3, UnitName = "l"},
                    // new Unit{UnitId = 4, UnitName = "ml"},
                    // new Unit{UnitId = 5, UnitName = "pcs."}

                    new RecipeProduct(1,1,4),
                    new RecipeProduct(1,2,2),
                    new RecipeProduct(1,3,0.5),
                    new RecipeProduct(1,4,1),
                    new RecipeProduct(1,5,10),
                    new RecipeProduct(1,6,500),

                    new RecipeProduct(2,1,1),
                    new RecipeProduct(2,6,200),
                    new RecipeProduct(2,7,30),
                    new RecipeProduct(2,8,10),
                    new RecipeProduct(2,9, 15),
                    new RecipeProduct(2,10,15),
                    
                    new RecipeProduct(3,13,1),
                    new RecipeProduct(3,11,60),
                    new RecipeProduct(3,12,7),
                    new RecipeProduct(3,3,1),
                    new RecipeProduct(3,4,1),


                    

                    

                };

                foreach (var recipeProduct in recipeProducts)
                {
                    dataContext.RecipeProducts.Add(recipeProduct);
                    await dataContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private static  async Task SeedRecipes(DataContext dataContext)
        {
            try
            {
                var recipes = new List<Recipe>
                {
                    new Recipe("Scrambled eggs"),
                    new Recipe("Pancake"),
                    new Recipe("Greek salad")
                };

                foreach (var recipe in recipes)
                {
                    dataContext.Recipes.Add(recipe);
                    await dataContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {

            }
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
                    new Product {ProductId = 1,ProductName = "egg", UnitId = 5},
                    new Product {ProductId = 2,ProductName = "bread", UnitId = 5},
                    new Product {ProductId = 3,ProductName = "onion", UnitId = 5},
                    new Product {ProductId = 4,ProductName = "tomato", UnitId = 5},
                    new Product {ProductId = 5,ProductName = "butter", UnitId = 2},
                    new Product {ProductId = 6,ProductName = "milk", UnitId = 4},

                    new Product {ProductId = 7,ProductName = "flour", UnitId = 2},
                    new Product {ProductId = 8,ProductName = "ham", UnitId = 2},
                    new Product {ProductId = 9,ProductName = "ketchup", UnitId = 4},
                    new Product {ProductId = 10,ProductName = "cheese", UnitId = 2},

                    new Product {ProductId = 11,ProductName = "feta cheese", UnitId = 2},
                    new Product {ProductId = 12,ProductName = "olives", UnitId = 5},
                    new Product {ProductId = 13,ProductName = "salad", UnitId = 5},
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