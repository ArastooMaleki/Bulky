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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;

        public CompanyController(IUnitOfWork unitOfWork , IWebHostEnvironment webHost)
        {
            this._unitOfWork = unitOfWork;
            this._webHost = webHost;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.CompanyRepository.GetAll().OrderBy(c=>c.Name));
        }
        [HttpGet]
        public IActionResult Upsert(Guid? ID)//UpdateInsert
        {
            if (ID == null) // Insert
            {
                var company = new Company();
                return View(company);
            }
            else//Update 
            {
                Company? company = _unitOfWork.CompanyRepository.Get(p => p.ID.Equals(ID));
                if (company == null)
                {
                    TempData["error"] = "the id doesnt exist";
                    return RedirectToAction("Index");
                }
                return View(company);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Company viewModel)
        {
            
            if (ModelState.IsValid)
            {
                
                if (viewModel.ID.Equals(Guid.Empty)) // Insert
                {
                    
                    _unitOfWork.CompanyRepository.Add(viewModel);
                    _unitOfWork.Save();
                    TempData["success"] = "The Company created successfully";

                    return RedirectToAction("Index","Company");

                }
                else//Update
                {
                    _unitOfWork.CompanyRepository.Update(viewModel);
                    _unitOfWork.Save();
                    TempData["success"] = "The Company edited successfully";
                    return RedirectToAction("Index","Company");
                }
            }
            else
            {
                
                return View(viewModel);
            }

        }
        

        public IActionResult Delete(Guid ID)
        {
            Company? company = _unitOfWork.CompanyRepository.Get(p=>p.ID.Equals(ID));
            if(company == null)
            {
                ModelState.AddModelError("CompanyNotFind","the company with this ID doesnt exist");
                return RedirectToAction("Index");
            }
            _unitOfWork.CompanyRepository.Remove(company);
            _unitOfWork.Save();
            TempData["success"] = "Company deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
