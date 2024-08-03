using Bulky.Models.Models;

namespace BulkyWeb.Areas.Admin.ViewModels
{
    public class ProductIndexVM
    {
        public IEnumerable<Product> products { set; get; }
        public IEnumerable<Category> categories { set; get; }

    }
}
