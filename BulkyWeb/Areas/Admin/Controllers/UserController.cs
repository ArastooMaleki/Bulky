using Bulky.DataAccess.Repository.IRepositories;
using Bulky.Models.Models;
using Bulky.Utility;
using BulkyWeb.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUnitOfWork unitOfWork
            ,UserManager<IdentityUser> userManager
            ,RoleManager<IdentityRole> roleManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string sortOrder , string SearchString
            ,string currentFilter,int? pageIndex)
        {
            ViewBag.CurrnetSort = sortOrder;//!=null?sortOrder: ViewData["CurrnetSort"]; 
            ViewBag.UserNameSortParam = sortOrder.IsNullOrEmpty() ? "UserNameAssending" : (sortOrder == "UserNameAssending" ? "UserNamedessending" : "UserNameAssending"); 
            
            if (SearchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            var tempFilter = SearchString;
            IQueryable<ApplicationUser> ApplicationUserQuery; 
            
            ApplicationUserQuery = _unitOfWork.ApplicationUserRepository.GetAll(includeProperty:"Company").AsQueryable();
              
            if (!SearchString.IsNullOrEmpty())
            {
                ApplicationUserQuery = ApplicationUserQuery.Where(oh => oh.UserName.Contains(SearchString) ||
                                    oh.Name.Contains(SearchString) || oh.Email.Contains(SearchString));
            }
            switch (sortOrder)
            {
                case "UserNameAssending":
                    ApplicationUserQuery = ApplicationUserQuery.OrderBy(oh=>oh.UserName);
                    break;
                case "UserNamedessending":
                    ApplicationUserQuery = ApplicationUserQuery.OrderByDescending(oh => oh.UserName);
                    break;              
                default:
                    break;
            }
            int pageSize = 3;
            var vm = new ApplicationUserIndex(pageSize, pageIndex ?? 1, await ApplicationUserQuery.CountAsync(),ApplicationUserQuery.Skip(((pageIndex ?? 1) - 1) * pageSize).Take(pageSize),sortOrder, tempFilter);
            return View(vm);
        }

        public IActionResult LockedOrUnlocked(Guid id)
        {
            var user = _unitOfWork.ApplicationUserRepository.Get(u=>u.Id==id.ToString());
            if (user.LockoutEnd!=null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(150); 
            }
            _unitOfWork.ApplicationUserRepository.Update(user);
            _unitOfWork.Save();
            TempData["success"] = $"the user {user.UserName} locked successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid id)
        {
            var sc = _unitOfWork.ApplicationUserRepository.Get(sc => sc.Id == id.ToString());
            _unitOfWork.ApplicationUserRepository.Remove(sc);
            _unitOfWork.Save();
            TempData["success"] = "the user deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(Guid id)
        {
            return View(_unitOfWork.ApplicationUserRepository.Get(od=>od.Id==id.ToString(), "Company"));
        }
        public IActionResult Edit(Guid id)
        {
            UserManagerEditVM vm = new BulkyWeb.Areas.Admin.ViewModels.UserManagerEditVM()
            {
                Id = id.ToString(),
                Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUserRepository.Get(u => u.Id == id.ToString())).GetAwaiter().GetResult()[0],
                RoleselectListItems =_roleManager.Roles.Select(r => r.Name).Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = r,
                    Value = r
                }).ToList()
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BulkyWeb.Areas.Admin.ViewModels.UserManagerEditVM? userManagerEditVM)
        {
            var user = _unitOfWork.ApplicationUserRepository.Get(od => od.Id == userManagerEditVM.Id.ToString(),tracked:false);
            var currentRole = await _userManager.GetRolesAsync(user); 
            await _userManager.RemoveFromRoleAsync(user, currentRole[0] );
            await _userManager.AddToRoleAsync(user, userManagerEditVM.Role);
            return RedirectToAction(nameof(Index));
        }
    }
}
