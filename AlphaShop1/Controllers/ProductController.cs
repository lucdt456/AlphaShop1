using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public readonly DataContext _dB;

		public ProductController(DataContext dB)
		{
			_dB = dB;
		}

		public async Task<IActionResult> Detail(int? Id)
		{
			if (_dB == null)
			{
				return NotFound();
			}

			var product = await _dB.Products.Include(p=>p.Category).Include(p=>p.Brand).FirstOrDefaultAsync(p=>p.Id == Id);
			return View(product);
		}
	}
}
