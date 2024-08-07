using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;

namespace BulkyBook.DataAccess.Repository.IRepositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);
    }
}
