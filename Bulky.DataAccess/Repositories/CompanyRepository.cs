using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories.IRepository;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories
{
    internal class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            this._dbContext = dbContext;
        }
        public void Update(Company company)
        {
            _dbContext.Companies.Update(company);
            _dbContext.SaveChanges();
        }
    }
}
