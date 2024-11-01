using AlphaShop1.Models;
using AlphaShop1.Models.ViewModel;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace AlphaShop1.Controllers
{
	[Authorize]
	public class CheckOutController : Controller
	{
		const string CART_KEY = "MYCART";

		public List<CartItem2> Cart => HttpContext.Session.Get<List<CartItem2>>(CART_KEY) ?? new List<CartItem2>();

		private DataContext _dB;
		public CheckOutController(DataContext dB)
		{
			_dB = dB;
		}

		public async Task<IActionResult> Index()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var ordercode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.OrderCode = ordercode;
				orderItem.UserEmail = userEmail;
				orderItem.Status = 1;
				orderItem.CreateDate = DateTime.Now;
				_dB.Add(orderItem);
				_dB.SaveChanges();
				RedirectToAction("Index");

				foreach (var cartitem in Cart)
				{
					var oderdetails = new OrderDetail()
					{
						OrderCode = ordercode,
						ProductId = cartitem.Id,
						Price = cartitem.Price,
						Quantity = cartitem.SoLuong
					};

					_dB.Add(oderdetails);
					_dB.SaveChanges();

					HttpContext.Session.Remove(CART_KEY);
				}

				return RedirectToAction("Index","Shop");

			}
		}
	}
}
