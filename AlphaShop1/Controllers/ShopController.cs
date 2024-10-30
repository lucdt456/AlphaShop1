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

		//Hiển thị danh sách hàng hóa
		public async Task<IActionResult> Index()
		{
			var products = await _dataContext.Products.Include(p => p.Category).Include(p => p.Brand).ToListAsync();
			return PartialView("_DanhSachSanPham", products);
		}


		//Tìm kiếm
		public async Task<IActionResult> Search(string? query)
		{
			if (query != null)
			{
				var products = await _dataContext.Products.Where(p => p.Name.Contains(query)).Include(p => p.Category).Include(p => p.Brand).ToListAsync();
				return PartialView("_DanhSachSanPham", products);
			}
			return RedirectToAction("Index");
		}


		//Lọc theo Category
		public async Task<IActionResult> CategoryFill(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			var products = await _dataContext.Products.Where(p => p.CategoryId == Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync();
			return PartialView("_DanhSachSanPham", products);
		}

		//Lọc theo Brand
		public async Task<IActionResult> BrandFill(int? Id)
		{
			if (Id == null) { return NotFound(); }

			var products = await _dataContext.Products.Where(p => p.BrandId == Id).Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			return PartialView("_DanhSachSanPham", products);
		}
	}
}
