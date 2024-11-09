using Microsoft.AspNetCore.Mvc;

namespace AlphaShop1.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
