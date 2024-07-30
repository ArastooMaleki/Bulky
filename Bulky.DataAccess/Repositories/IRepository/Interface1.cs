using Bulky.Models.Models;
namespace Bulky.DataAccess.Repository.IRepository
{
    /// <summary>
    /// this is the product repository 
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Update the database with new product
        /// </summary>
        /// <param name="product"></param>
        void Update(Product product);
    }
}
