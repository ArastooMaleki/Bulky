using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// update the table with category
        /// </summary>
        /// <param name="category">an entity type of category</param>
        void Update(Category category);
        /// <summary>
        /// save the changes to database 
        /// </summary>
        void Save();
    }
}
