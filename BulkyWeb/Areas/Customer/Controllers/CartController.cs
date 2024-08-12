using Bulky.DataAccess.Repository.IRepositories;
using Bulky.Models.Models;
using Bulky.Utility;
using BulkyWeb.Areas.Customer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mono.TextTemplating;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ShoppingCartVM shoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork , IEmailSender emailSender)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: CartController1
        public IActionResult Index()
        {
            
            var identityClaims = (ClaimsIdentity)User.Identity;
            var userID = identityClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var scs = _unitOfWork.ShoppingCartRepository.GetAll("Product" , sc=>sc.ApplicationUserID.Equals(userID)).ToList();

            shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = scs,
                OrderHeader = new OrderHeader() { OrderTotal = OrderTotalCalculator(scs) }
            };
            
            return View(shoppingCartVM);

        }
        public IActionResult Summary()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var userID = identityClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var scs = _unitOfWork.ShoppingCartRepository.GetAll("Product", sc => sc.ApplicationUserID.Equals(userID)).ToList();
            var curentUser = _unitOfWork.ApplicationUserRepository.Get(u=>u.Id.Equals(userID),tracked:true);
            shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = scs,
                OrderHeader = new OrderHeader()
                {
                    Name = curentUser.Name,
                    ApplicationUserId = userID,
                    ApplicationUser = curentUser,
                    PhoneNumber = curentUser.PhoneNumber,
                    City = curentUser.City,
                    State = curentUser.State,
                    StreetAddress = curentUser.StreetAddress,
                    PostalCode = curentUser.PostalCode,
                    OrderTotal = OrderTotalCalculator(scs)
                }
            };
            /*shoppingCartVM.ShoppingCartList = scs;
            shoppingCartVM.OrderHeader.ApplicationUserId = userID;
            shoppingCartVM.OrderHeader.ApplicationUser = curentUser;
            shoppingCartVM.OrderHeader.PhoneNumber = curentUser.PhoneNumber;
            shoppingCartVM.OrderHeader.City = curentUser.City;
            shoppingCartVM.OrderHeader.State = curentUser.State;
            shoppingCartVM.OrderHeader.StreetAddress = curentUser.StreetAddress;
            shoppingCartVM.OrderHeader.PostalCode = curentUser.PostalCode;
            shoppingCartVM.OrderHeader.OrderTotal = OrderTotalCalculator(scs);*/
            return View(shoppingCartVM);
        }
        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var userID = identityClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var scs = _unitOfWork.ShoppingCartRepository.GetAll("Product", sc => sc.ApplicationUserID.Equals(userID)).ToList();
            var curentUser = _unitOfWork.ApplicationUserRepository.Get(u => u.Id.Equals(userID),tracked:true);
            shoppingCartVM.ShoppingCartList = scs;
            shoppingCartVM.OrderHeader.ApplicationUserId = userID;
            shoppingCartVM.OrderHeader.ApplicationUser = curentUser;
            shoppingCartVM.OrderHeader.OrderTotal = OrderTotalCalculator(scs);
            shoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            if (curentUser.CompanyID==null)//simple user
            {
                shoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
                shoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            }
            else//company user 
            {
                shoppingCartVM.OrderHeader.OrderStatus =SD.StatusApproved;
                shoppingCartVM.OrderHeader.PaymentStatus =SD.PaymentStatusDelayedPayment;
            }
            if (curentUser.Name.IsNullOrEmpty())
            {
                curentUser.PostalCode = shoppingCartVM.OrderHeader.PostalCode;
                curentUser.StreetAddress = shoppingCartVM.OrderHeader.StreetAddress;
                curentUser.City = shoppingCartVM.OrderHeader.City;
                curentUser.State = shoppingCartVM.OrderHeader.State;
                curentUser.PhoneNumber = shoppingCartVM.OrderHeader.PhoneNumber;
                curentUser.Name = shoppingCartVM.OrderHeader.Name;
            }
            _unitOfWork.OrderHeaderRepository.Add(shoppingCartVM.OrderHeader);
            _unitOfWork.Save();
            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                OrderDetail detail = new OrderDetail()
                {
                    ProductId = cart.ProductID,
                    OrderHeaderId = shoppingCartVM.OrderHeader.ID,
                    Count = cart.Count,
                    Price = cart.Count < 50 ? cart.Product.Price : cart.Count < 100 ?cart.Product.Price50: cart.Product.Price100
                };
                _unitOfWork.OrderDetailRepository.Add(detail);
                _unitOfWork.Save();
            }
            _unitOfWork.ApplicationUserRepository.Update(curentUser);
            _unitOfWork.Save();
            
            
            if (curentUser.CompanyID == null)
            {
                //it is a regular customer account and we need to capture payment
                //stripe logic
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={shoppingCartVM.OrderHeader.ID}",
                    CancelUrl = domain + "customer/cart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach (var item in shoppingCartVM.ShoppingCartList)
                {
                    var P = item.Count < 50 ? item.Product.Price : item.Count < 100 ?item.Product.Price50: item.Product.Price100 ;
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(P* 100), // $20.50 => 2050
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);

                }

                
                _unitOfWork.ShoppingCartRepository.RemoveRange(shoppingCartVM.ShoppingCartList);
                HttpContext.Session.SetInt32(SD.SessionCart , (HttpContext.Session.GetInt32(SD.SessionCart)??0) - shoppingCartVM.ShoppingCartList.Select(sc=>sc.Count).Sum());
                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeaderRepository.UpdateStripePaymentID(shoppingCartVM.OrderHeader.ID, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);

            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = shoppingCartVM.OrderHeader.ID });
            

        }
        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeaderRepository.Get(u => u.ID == id,  "ApplicationUser");
            if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
            {
                //this is an order by customer

                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeaderRepository.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeaderRepository.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
                HttpContext.Session.Clear();
            }

            

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCartRepository
                .GetAll(filter:u => u.ApplicationUserID == orderHeader.ApplicationUserId).ToList();

            _unitOfWork.ShoppingCartRepository.RemoveRange(shoppingCarts);
            _unitOfWork.Save();

            return View(id);
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
            HttpContext.Session.SetInt32(SD.SessionCart, (HttpContext.Session.GetInt32(SD.SessionCart) ?? 0) + 1);

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
                HttpContext.Session.SetInt32(SD.SessionCart, (HttpContext.Session.GetInt32(SD.SessionCart) ?? 0) - 1);
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
            
            HttpContext.Session.SetInt32(SD.SessionCart,(HttpContext.Session.GetInt32(SD.SessionCart)??0)-sc.Count);
            
            TempData["success"] = "the shopping cart deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        
    }
}