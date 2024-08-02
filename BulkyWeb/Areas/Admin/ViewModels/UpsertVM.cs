using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.ViewModels
{
    public class UpsertVM
    {
        public Product? Product { get; set; }
        
        public IEnumerable<SelectListItem>? CategoryListItems { get; set; }

    }
}
