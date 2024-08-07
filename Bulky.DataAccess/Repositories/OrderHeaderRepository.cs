using BulkyBook.DataAccess.Repository.IRepositories;
using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Bulky.DataAccess.Repositories;

namespace Bulky.DataAccess.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int ID, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.ID == ID);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentID(int ID, string sessionID, string paymentIntentID)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.ID == ID);
            if (orderFromDb != null)
            {
                if (!string.IsNullOrEmpty(sessionID))
                {
                    orderFromDb.SessionId = sessionID;
                }
                if (!string.IsNullOrEmpty(paymentIntentID))
                {
                    orderFromDb.PaymentIntentId = paymentIntentID;
                }
            }
        }
    }
}
