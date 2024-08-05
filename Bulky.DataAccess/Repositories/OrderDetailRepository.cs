using BulkyBook.DataAccess.Repository.IRepositories;
using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Bulky.DataAccess.Repositories;

namespace Bulky.DataAccess.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
