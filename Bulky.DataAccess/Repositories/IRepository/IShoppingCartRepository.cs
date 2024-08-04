using Bulky.DataAccess.Repositories.IRepository;
using Bulky.Models.Models;
namespace Bulky.DataAccess.Repository.IRepository
{
    /// <summary>
    /// this is the product repository 
    /// </summary>
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart sc);
    }
}
