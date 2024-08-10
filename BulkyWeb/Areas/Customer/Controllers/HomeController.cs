using Bulky.DataAccess.Repository.IRepositories;
using Bulky.Models.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger
            , IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var viweModel = _unitOfWork.ProductRepository.GetAll();
            return View(viweModel);
        }
        public IActionResult Details(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(u=>u.ID==id , "Category");
            var shoppingCart = new ShoppingCart() { Product = product, Count = 1, ProductID = id }; 
            return View(shoppingCart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var userID = identityClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var previousCart = _unitOfWork.ShoppingCartRepository
                .Get(sc=>sc.ApplicationUserID==userID 
                && sc.ProductID==shoppingCart.ProductID );
            if(HttpContext.Session.GetInt32(SD.SessionCart) == null)
            {
                HttpContext.Session.SetInt32(SD.SessionCart,0);

            }
            if (previousCart != null)
            {
                previousCart.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCartRepository.Update(previousCart);
                TempData["success"] = "the shopping cart updated successfully";
                HttpContext.Session.SetInt32(SD.SessionCart,  shoppingCart.Count + HttpContext.Session.GetInt32(SD.SessionCart) ?? 0);
                
            }
            else
            {
                shoppingCart.ApplicationUserID = userID;
                _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
                TempData["success"] = "the product added to shopping cart successfully";
                HttpContext.Session.SetInt32(SD.SessionCart,shoppingCart.Count + HttpContext.Session.GetInt32(SD.SessionCart) ?? 0);
            }
            
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index)); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
