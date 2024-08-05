using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;
namespace Bulky.DataAccess.Repository.IRepositories
{
    /// <summary>
    /// this is the product repository 
    /// </summary>
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart sc);
    }
}
