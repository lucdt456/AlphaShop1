using AlphaShop1.Models;
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
		public async Task<IActionResult> Index(int pg = 1)
		{
			List<ProductModel> products = await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync();

			const int pagSize = 6;

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = products.Count();
			var pager = new Paginate(recsCount, pg, pagSize);
			int recSkip = (pg - 1) * pagSize;

			var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
			ViewBag.Pager = pager;
			return PartialView("_DanhSachSanPham", data);
		}


		//Tìm kiếm
		public async Task<IActionResult> Search(string? query)
		{
			if (query != null)
			{
				var products = await _dataContext.Products.OrderByDescending(p => p.Id).Where(p => p.Name.Contains(query)).Include(p => p.Category).Include(p => p.Brand).ToListAsync();
				return PartialView("_DanhSachSanPham", products);
			}
			return RedirectToAction("Index");
		}
	}
}
