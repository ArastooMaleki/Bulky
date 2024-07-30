using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            return View(_unitOfWork.ProductRepository.GetAll());
        }
        public IActionResult Create()
        {
            var CategoryListItems = _unitOfWork.CategoryRepository.GetAll()
                .Select(c=> new SelectListItem { Text = c.Name , Value = c.Id.ToString()});
            ViewBag.CategoryListItems = CategoryListItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Save();
            TempData["success"] = "product created successfully";
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit([FromRoute]int ID)
        {
            var CategoryListItems = _unitOfWork.CategoryRepository.GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            ViewBag.CategoryListItems = CategoryListItems;
            Product product = _unitOfWork.ProductRepository.Get(p=>p.ID==ID);
            return View(product);
            
        }
        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            _unitOfWork.ProductRepository.Update(updatedProduct);
            _unitOfWork.Save();
            TempData["success"] = "product edited successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete([FromRoute]int ID)
        {
            Product product = _unitOfWork.ProductRepository.Get(p=>p.ID==ID);
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
