using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repositories

{
    internal class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartRepository(ApplicationDbContext dbContext ):base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Update(ShoppingCart sc)
        {
            _dbContext.shoppingCarts.Update(sc);
        }
    }
}
