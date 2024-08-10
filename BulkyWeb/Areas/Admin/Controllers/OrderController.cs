using Bulky.DataAccess.Repository.IRepositories;
using Bulky.Models.Models;
using Bulky.Utility;
using BulkyWeb.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string?sortOrder , string? SearchString
            ,string? currentFilter,int? pageIndex)
        {
            ViewData["CurrnetSort"] = sortOrder; 
            ViewData["OrderDateSortParam"] = sortOrder.IsNullOrEmpty() ? "OrderDateAssending" : (sortOrder == "OrderDateAssending" ? "OrderDatedessending" : "OrderDateAssending"); 
            ViewData["OrderTotalSortParam"] = sortOrder.IsNullOrEmpty() ? "OrderTotalAssending" : (sortOrder == "OrderTotalAssending" ? "OrderTotaldessending" : "OrderTotalAssending");
            
            if (SearchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewData["currentFilter"] = SearchString;
            IQueryable<OrderHeader> orderHeaderQuery; 
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                orderHeaderQuery = _unitOfWork.OrderHeaderRepository.GetAll(includeProperty: "ApplicationUser").AsQueryable();
            }
            else
            {
                var ci = (ClaimsIdentity)User.Identity;
                var userID = ci.FindFirst(ClaimTypes.NameIdentifier).Value;
                orderHeaderQuery = _unitOfWork.OrderHeaderRepository.GetAll(includeProperty: "ApplicationUser" , oh=>oh.ApplicationUserId == userID).AsQueryable();
            }
            if (!SearchString.IsNullOrEmpty())
            {
                orderHeaderQuery = orderHeaderQuery.Where(oh => oh.ApplicationUser.UserName.Contains(SearchString) ||
                                    oh.Name.Contains(SearchString));
            }
            switch (sortOrder)
            {
                case "OrderDateAssending":
                    orderHeaderQuery = orderHeaderQuery.OrderBy(oh=>oh.OrderDate);
                    break;
                case "OrderDatedessending":
                    orderHeaderQuery = orderHeaderQuery.OrderByDescending(oh => oh.OrderDate);
                    break;
                case "OrderTotalAssending":
                    orderHeaderQuery = orderHeaderQuery.OrderBy(oh=>oh.OrderTotal);
                    break;
                case "OrderTotaldessending":
                    orderHeaderQuery = orderHeaderQuery.OrderByDescending(oh => oh.OrderTotal);
                    break;
                default:
                    break;
            }
            int pageSize = 3;
            var vm = new OrderIndex(pageSize, pageIndex ?? 1, await orderHeaderQuery.CountAsync(),orderHeaderQuery.Skip(((pageIndex ?? 1) - 1) * pageSize).Take(pageSize));
            return View(vm);
        }


        public IActionResult Remove(int id)
        {
            var sc = _unitOfWork.OrderHeaderRepository.Get(sc => sc.ID == id);
            _unitOfWork.OrderHeaderRepository.Remove(sc);
            _unitOfWork.Save();
            TempData["success"] = "the Order Header deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            return View(_unitOfWork.OrderHeaderRepository.Get(od=>od.ID==id, "ApplicationUser"));
        }
        public IActionResult Edit(int id)
        {
            return View(_unitOfWork.OrderHeaderRepository.Get(od => od.ID == id, "ApplicationUser"));
        }
        [HttpPost]
        public IActionResult Edit(OrderHeader newOrderHeader)
        {
            var oldOrder = _unitOfWork.OrderHeaderRepository.Get(od => od.ID == newOrderHeader.ID, "ApplicationUser"); 

            oldOrder.PhoneNumber = newOrderHeader.PhoneNumber;
            oldOrder.StreetAddress = newOrderHeader.StreetAddress;
            oldOrder.City = newOrderHeader.City;
            oldOrder.PostalCode = newOrderHeader.PostalCode;
            oldOrder.State = newOrderHeader.State;
            oldOrder.Name = newOrderHeader.Name;
            oldOrder.OrderTotal = newOrderHeader.OrderTotal;
            oldOrder.OrderStatus = newOrderHeader.OrderStatus;
            oldOrder.PaymentStatus = newOrderHeader.PaymentStatus;

            _unitOfWork.OrderHeaderRepository.Update(oldOrder);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Details), new {id=newOrderHeader.ID});
        }
    }
}
