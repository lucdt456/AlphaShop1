using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Controllers
{
	public class ShopController : Controller
	{

		public readonly DataContext _dataContext;

		public ShopController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public IActionResult Index()
		{
			var products = _dataContext.Products.Include(p => p.Category).Include(p=>p.Brand).ToList();
			return View(products);
		}

		public IActionResult Search(string? query)
		{
			if (query != null) {
				var product = _dataContext.Products.Where(p => p.Name.Contains(query)).Include(p => p.Category).Include(p => p.Brand).ToList();
				return View(product);
			}
			return RedirectToAction("Index");
		}
	}
}
