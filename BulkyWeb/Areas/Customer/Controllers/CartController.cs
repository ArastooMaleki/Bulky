using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Areas.Customer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: CartController1
        public IActionResult Index()
        {
            var scs = _unitOfWork.ShoppingCartRepository.GetAll("Product").ToList();
            return View(
                new ShoppingCartVM()
                {
                    ShoppingCartList = scs,
                    OrderTotal = OrderTotalCalculator(scs)
                });
        }
        public IActionResult Summary()
        {
            return View();
        }
        private double OrderTotalCalculator(IEnumerable<ShoppingCart> ShoppingCartList)
        {
            double OrderTotal = 0;
            foreach (var sc in ShoppingCartList)
            {
                var n = sc.Count;
                if (n > 0 && n < 51)
                {
                    OrderTotal += sc.Product.Price * n;
                }
                else if (n > 50 && n < 101)
                {
                    OrderTotal += sc.Product.Price50 * n;
                }
                else if (n > 100)
                {
                    OrderTotal += sc.Product.Price100 * n;
                }

            }
            return OrderTotal;
        }

        public IActionResult Plus(Guid cartId)
        {
            var sc = _unitOfWork.ShoppingCartRepository.Get(sc => sc.ID.Equals(cartId));
            sc.Count += 1;
            _unitOfWork.ShoppingCartRepository.Update(sc);
            _unitOfWork.Save();
            TempData["success"] = "the count increased by one";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(Guid cartId)
        {
            var sc = _unitOfWork.ShoppingCartRepository.Get(sc => sc.ID.Equals(cartId));
            if(sc.Count > 1)
            {
                sc.Count -= 1;
                _unitOfWork.ShoppingCartRepository.Update(sc);
                _unitOfWork.Save();
                TempData["success"] = "the count decreased by one";
                return RedirectToAction(nameof(Index));
            }else
            {
                return RedirectToAction(nameof(Remove),"Cart", new { cartId = cartId });
            }
            
        }

        public IActionResult Remove(Guid cartId)
        {
            var sc = _unitOfWork.ShoppingCartRepository.Get(sc => sc.ID.Equals(cartId));
            _unitOfWork.ShoppingCartRepository.Remove(sc);
            _unitOfWork.Save();
            TempData["success"] = "the shopping cart deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}