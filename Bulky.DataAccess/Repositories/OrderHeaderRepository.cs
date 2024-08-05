using BulkyBook.DataAccess.Repository.IRepositories;
using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Bulky.DataAccess.Repositories;

namespace Bulky.DataAccess.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

		
	}
}
