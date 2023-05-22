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
			IEnumerable<Product> productList=_unitOfWork.Product.GetAll(includeProperties:"Category");
			return View(productList);	
		}
		public IActionResult Details(int id)
		{
			Product product = _unitOfWork.Product.GetFirstOrDefault(u=>u.Id==id,includeProperties:"Category");
			return View(product);
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