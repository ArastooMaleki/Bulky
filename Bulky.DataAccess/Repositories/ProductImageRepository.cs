using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Bulky.DataAccess.Repository.IRepositories;

namespace Bulky.DataAccess.Repositories

{
    internal class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductImageRepository(ApplicationDbContext dbContext ):base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Update(ProductImage sc)
        {
            _dbContext.ProdcutImages.Update(sc);
        }
    }
}
