using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bulky.DataAccess.Repositories

{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext ):base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public void Update(Product product)
        {
            _dbContext.Products.Update(product);    
        }
    }
}
