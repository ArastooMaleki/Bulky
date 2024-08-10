using Bulky.Models.Models;

namespace BulkyWeb.Areas.Admin.ViewModels
{
    public class OrderIndex
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool HasNextPage => PageCount > PageIndex;
        public bool HasPreviousPage => PageIndex > 1; 
        public List<OrderHeader> OrderHeaders { get; set; }

        public OrderIndex(int pageSize , int pageIndex ,int AllElementCount, IQueryable<OrderHeader> query)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount =(int) Math.Ceiling((decimal)AllElementCount/pageSize) ;
            OrderHeaders = query.ToList();
        }
    }
}
