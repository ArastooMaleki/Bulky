using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
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
