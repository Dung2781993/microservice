using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            //Create sample data for microservices
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Hellomama", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Hellomama2", Price = 30, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Hellomama3", Price = 40, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "Hellomama4", Price = 50, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 5, Name = "Hellomama5", Price = 60, Inventory = 100 });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, Models.Product Products, string ErrorMessage)> GetProductsAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
