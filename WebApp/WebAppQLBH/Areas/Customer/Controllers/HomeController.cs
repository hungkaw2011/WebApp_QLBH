using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.DataAccess.Repository.IRepository;
using WebApp.Models;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            ShoppingCard shoppingCard = new()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category"),
                Count = 1,
                ProductId = id
            };
            return View(shoppingCard);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCard obj)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            obj.ApplicationUserId= userId;
            ShoppingCard objFromDb=_unitOfWork.ShoppingCard.GetFirstOrDefault(u=>u.ProductId==obj.ProductId &&
                u.ApplicationUserId==obj.ApplicationUserId);
            if (objFromDb == null)
            {
                obj.Id = 0; //Set Id=0 Thuộc tính Id mới có thể tự động tăng ShoppingCard (.net 7 )
                _unitOfWork.ShoppingCard.Add(obj);
            }
            else
            {
                objFromDb.Count += obj.Count;
                _unitOfWork.ShoppingCard.Update(objFromDb);
            }
            _unitOfWork.Save();
			TempData["success"] = "Đã thêm sản phẩm vào giỏ hàng";
			return RedirectToAction(nameof(Index));
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