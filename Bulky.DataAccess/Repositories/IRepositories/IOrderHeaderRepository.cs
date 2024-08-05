using Bulky.Models.Models;
using Bulky.DataAccess.Repositories.IRepositories;


namespace BulkyBook.DataAccess.Repository.IRepositories

{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
		
	}
}
