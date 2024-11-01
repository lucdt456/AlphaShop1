using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly DataContext _dB;
		public OrderController(DataContext db)
		{
			_dB = db;
		}
		public async Task<IActionResult> Index()
		{
			var order = await _dB.Orders.OrderByDescending(p => p.Id).ToListAsync();	
			return View(order);
		}

		public async Task<IActionResult> ViewOrder(string orderCode)
		{
			var orderDetails = await _dB.OrderDetails.Include(od => od.Product).Include(od => od.Order).Where(p => p.OrderCode == orderCode).ToListAsync();

			return View(orderDetails);
		}

		public async Task<IActionResult> Delete()
		{
			var order = await _dB.Orders.OrderBy(p => p.Id).ToListAsync();	
			return View(order);
		}
	}
}
