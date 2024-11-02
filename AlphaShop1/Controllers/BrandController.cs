using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Controllers
{
	public class BrandController : Controller
	{
		public readonly DataContext _dataContext;
		public BrandController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<IActionResult> Index(int? Id)
		{
			if (Id == null) { return NotFound(); }

			var products = await _dataContext.Products.OrderByDescending(p => p.Id).Where(p => p.BrandId == Id).Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			return PartialView("_DanhSachSanPham", products);
		}
	}
}
