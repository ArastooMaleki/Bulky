using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositories;
using Bulky.Models.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public  IActionResult Index()
        {
            List<Category> categoris = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View("Index", categoris);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category cat)
        {

            if (cat.Name == "error")
            {
                ModelState.AddModelError("Time", "name cannot ber error ");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(cat);
                _unitOfWork.Save();
                TempData["success"] = "category create successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        //[Route("Category/Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cat = _unitOfWork.CategoryRepository.Get(c=>c.Id==id);
            if (cat != null)
            {
                _unitOfWork.CategoryRepository.Remove(cat);
                _unitOfWork.Save();
                TempData["success"] = "category delete successfully";
                return RedirectToAction("Index", "Category");
            }

            return BadRequest();
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var cat = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            if (cat == null)
            {
                return BadRequest();
            }
            return View(cat);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _unitOfWork.CategoryRepository.Update(cat);
            _unitOfWork.Save();
            TempData["success"] = "category Update successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
