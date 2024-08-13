using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.IRepositories
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        /// <summary>
        /// update the table with productImage
        /// </summary>
        /// <param name="productImage">an entity type of productImage</param>
        void Update(ProductImage productImage);
    }
}
