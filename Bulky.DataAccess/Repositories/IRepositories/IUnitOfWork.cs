using Bulky.DataAccess.Repositories.IRepositories;
using BulkyBook.DataAccess.Repository.IRepositories;
namespace Bulky.DataAccess.Repository.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        void Save();
    }
}
