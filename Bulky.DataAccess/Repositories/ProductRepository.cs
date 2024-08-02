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
            var oldProduct = _dbContext.Products.FirstOrDefault(p => p.ID == product.ID);

            oldProduct.ISBN = product.ISBN;
            oldProduct.Author = product.Author;
            oldProduct.Description = product.Description;   
            oldProduct.CategoryID = product.CategoryID;
            oldProduct.ImageUrl = product.ImageUrl;
            oldProduct.Title = product.Title;
            oldProduct.ListPrice = product.ListPrice;
            oldProduct.Price = product.Price;   
            oldProduct.Price50  = product.Price50;
            oldProduct.Price100 = product.Price100;

            _dbContext.Products.Update(oldProduct);    
        }
    }
}
