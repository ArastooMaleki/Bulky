using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Net.Security;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;

        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHost)
        {
            this._unitOfWork = unitOfWork;
            this._webHost = webHost;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.ProductRepository.GetAll("Category"));
        }
        [HttpGet]
        public IActionResult Upsert(int? ID)//UpdateInsert
        {
            var CategoryListItems = _unitOfWork.CategoryRepository.GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            
            if (ID == null) // Insert
            {
                var viewModel = new ProductUpsertVM() { Product = new Product(), CategoryListItems = CategoryListItems };
                return View(viewModel);
            }
            else//Update 
            {
                Product? product = _unitOfWork.ProductRepository.Get(p => p.ID == ID);
                if (product == null)
                {
                    TempData["error"] = "the id doesnt exist";
                    return RedirectToAction("Index");
                }
                var viewModel = new ProductUpsertVM() { Product = product, CategoryListItems = CategoryListItems };
                return View(viewModel);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Upsert(ProductUpsertVM? viewModel, IFormFile? fileInput)//, IFormFile file )//UpdateInsert UpsertVM? viewModel
        {
            
            if (ModelState.IsValid)
            {
                Product? product = viewModel.Product;
                if (product == null || product.ID == 0) // Insert
                {
                    string fileName = $"{Guid.NewGuid()}.{Path.GetExtension(fileInput.FileName)}";
                    string fileRootPath = _webHost.WebRootPath;
                    string filePath = Path.Combine(fileRootPath, "images\\product", fileName);
                    using (var writer = new FileStream(filePath, FileMode.Create))
                    {
                        fileInput.CopyTo(writer);
                    }
                    var URl = "/images/product/" + fileName;
                    product.ImageUrl = URl;
                    _unitOfWork.ProductRepository.Add(product);
                    _unitOfWork.Save();
                    TempData["success"] = "The product created successfully";

                    return RedirectToAction("Index","Product");

                }
                else//Update
                {
                    string fileName = $"{Guid.NewGuid()}.{Path.GetExtension(fileInput.FileName)}";
                    string fileRootPath = _webHost.WebRootPath;
                    string filePath = Path.Combine(fileRootPath, "images\\product", fileName);
                    using (var writer = new FileStream(filePath, FileMode.Create))
                    {
                        fileInput.CopyTo(writer);
                    }
                    if (!String.IsNullOrEmpty(product.ImageUrl))
                    {
                        
                        var oldImagePath = Path.Combine(fileRootPath, "images\\product", Path.GetFileName(product.ImageUrl));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        } 
                    }
                    var URl = "/images/product/" + fileName;
                    product.ImageUrl = URl;
                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.Save();
                    TempData["success"] = "The product edited successfully";
                    return RedirectToAction("Index","Product");
                }
            }
            else
            {
                viewModel.CategoryListItems = _unitOfWork.CategoryRepository.GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                return View(viewModel);
            }

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
