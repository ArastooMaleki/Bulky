using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;

namespace BulkyBook.DataAccess.Repository.IRepositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
    }
}
