using Bulky.Models.Models;
namespace Bulky.DataAccess.Repositories.IRepositories
{
    /// <summary>
    /// Repository for company crud operation 
    /// </summary>
    public interface ICompanyRepository : IRepository<Company>
    {
        /// <summary>
        /// use this to update a company
        /// </summary>
        /// <param name="company">updated company user</param>
        void Update(Company company);
    }
}
