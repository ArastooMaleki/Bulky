using Bulky.Models.Models;

namespace BulkyWeb.Areas.Admin.ViewModels
{
    public class ApplicationUserIndex
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool HasNextPage => PageCount > PageIndex;
        public bool HasPreviousPage => PageIndex > 1; 
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public string? CurrentSort { set; get; }
        public string? CurrentFilter { set; get; }
        public ApplicationUserIndex(int pageSize , int pageIndex ,int AllElementCount, IQueryable<ApplicationUser> query , string? currentSort=null, string? currentFilter=null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount =(int) Math.Ceiling((decimal)AllElementCount/pageSize) ;
            ApplicationUsers = query.ToList();
            CurrentSort = currentSort;
            CurrentFilter = currentFilter;
        }
    }
}
