using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Controllers
{
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		public BrandController(DataContext dataContext) {  _dataContext = dataContext; }

		public IActionResult Index(int? Id)
		{
			if (Id == null) { return NotFound(); }
			var product = _dataContext.Products.Where(p => p.BrandId == Id).Include(p=> p.Brand).Include(p => p.Category).ToList();
			return View(product);
		}
	}
}
