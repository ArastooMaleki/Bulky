using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using BulkyBook.DataAccess.Repository.IRepositories;


namespace Bulky.DataAccess.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Update(ApplicationUser obj)
        {
            _dbContext.applicationUsers.Update(obj);
        }
    }
}
