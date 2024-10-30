using AlphaShop1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AlphaShop1.Controllers
{
	public class CheckOutController : Controller
	{
		private DbContext _dB;
		public CheckOutController(DbContext dB)
		{
			_dB = dB;
		}

		public async Task<IActionResult> Index()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if(userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var ordercode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.Order_Code = ordercode;
				orderItem.UserName = userEmail;
				orderItem.Status = 1;
				orderItem.CreateDate = DateTime.Now;
				_dB.Add(orderItem);
				_dB.SaveChanges();
				RedirectToAction("Index");
			}
			return View();
		}
	}
}
