using BooksStore.DataAccess.Repository.IRepository;
using BooksStore.Models;
using BooksStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BooksStoreApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = SD.CustomerRole)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(String searchString)
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(include: "Category");
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Title.Contains(searchString));
                return View(products);
            }
            return View(products);
        }

        //public async Task<IActionResult> Index(String searchString)
        //{
        //    IEnumerable<Product> products = _unitOfWork.Product.GetAll(include: "Category");

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        products = products.Where(p => p.Title.Contains(searchString));
        //        return View(await products.ToListAsync());
        //    }
        //    return View(await products.ToListAsync());
        //}

        public IActionResult Details(int ProductId)
        {
            ShoppingCart cart = new ShoppingCart()
            {                
                Product = _unitOfWork.Product.Get(det => det.Id == ProductId, include: "Category"),
                Count = 1,
                ProductId = ProductId
            };
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

            if (cartDb == null)
            {
                _unitOfWork.ShoppingCart.Added(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionKey,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).ToList().Count());
            }
            else
            {
                cartDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartDb);
                _unitOfWork.Save();
            }
            _unitOfWork.Save();
            TempData["success"] = "Cart updated successfully";
            return RedirectToAction("Index");
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
