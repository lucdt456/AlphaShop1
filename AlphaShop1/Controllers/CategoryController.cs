using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Controllers
{
	
	public class CategoryController : Controller
	{
		public readonly DataContext _dataContext;
		public CategoryController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<IActionResult> Index(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			var products = await _dataContext.Products.OrderByDescending(p => p.Id).Where(p => p.CategoryId == Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync();
			return PartialView("_DanhSachSanPham", products);
		}
	}
	
}
