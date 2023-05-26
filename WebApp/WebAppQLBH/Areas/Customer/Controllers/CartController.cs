using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.DataAccess.Repository.IRepository;
using WebApp.Models;
using WebApp.Models.ViewModels;

#pragma warning disable CS8618

namespace WebAppQLBH.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCardVM ShoppingCardVM { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity!;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

			ShoppingCardVM = new()
			{
				ShoppingCardList = _unitOfWork.ShoppingCard.GetAll(u => u.ApplicationUserId == userId,
				includeProperties: "Product"),
			};
			foreach (var cart in ShoppingCardVM.ShoppingCardList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				ShoppingCardVM.OrderTotal += (cart.Price * cart.Count);
			}

			return View(ShoppingCardVM);
		}

		public IActionResult Sumary()
		{
			return View();
		}
		public IActionResult Plus(int CartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCard.GetFirstOrDefault(u => u.Id == CartId);
			cartFromDb.Count += 1;
			_unitOfWork.ShoppingCard.Update(cartFromDb);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Minus(int CartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCard.GetFirstOrDefault(u => u.Id == CartId);
			cartFromDb.Count -= 1;
			if (cartFromDb.Count == 0)
			{
				_unitOfWork.ShoppingCard.Remove(cartFromDb);
				_unitOfWork.Save();
			}
			else
			{
				_unitOfWork.ShoppingCard.Update(cartFromDb);
				_unitOfWork.Save();
			}
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Remove(int CartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCard.GetFirstOrDefault(u => u.Id == CartId);
			_unitOfWork.ShoppingCard.Remove(cartFromDb);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		private static double GetPriceBasedOnQuantity(ShoppingCard shoppingCart)
		{
			if (shoppingCart.Count <= 50)
			{
				return shoppingCart.Product.Price;
			}
			else
			{
				if (shoppingCart.Count <= 100)
				{
					return shoppingCart.Product.Price50;
				}
				else
				{
					return shoppingCart.Product.Price100;
				}
			}
		}
	}
}
