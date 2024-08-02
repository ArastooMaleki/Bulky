using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Net.Security;
namespace BulkyWeb.Areas.Admin.Controllers
{
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

            return View(_unitOfWork.ProductRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Upsert(int? ID)//UpdateInsert
        {
            var CategoryListItems = _unitOfWork.CategoryRepository.GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            
            if (ID == null) // Insert
            {
                var viewModel = new UpsertVM() { Product = new Product(), CategoryListItems = CategoryListItems };
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
                var viewModel = new UpsertVM() { Product = product, CategoryListItems = CategoryListItems };
                return View(viewModel);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertVM? viewModel, IFormFile? fileInput)//, IFormFile file )//UpdateInsert UpsertVM? viewModel
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
                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.Save();
                    return RedirectToAction("Index","Product");
                }
            }
            else
            {

                return View(viewModel);
            }

        }
        /*    public IActionResult Upsert(UpsertVM? viewModel  )//, IFormFile file )//UpdateInsert
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Product?.ID == 0) // Insert
                {
                    Product product = viewModel.Product;
                    //if (file == null)
                    //{
                    //    ModelState.AddModelError("file","image file is empty");
                    //    return View(viewModel);
                    //}
                    //var rootPath = _webHost.WebRootFileProvider.ToString();
                    //var fileName = $"{Guid.NewGuid}.{Path.GetExtension(file.FileName)}";
                    //var filePath = Path.Combine(rootPath,"images\\product", fileName.Trim() );
                    return View();
                }
                else//Update 
                {
                    return View(viewModel);
                }

            }
            else
            {
                return View(viewModel); 
            }
        }*/
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
