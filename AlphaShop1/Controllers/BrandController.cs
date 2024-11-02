using AlphaShop1.Models;
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

		public async Task<IActionResult> Index(int? Id, int pg = 1)
		{
			if (Id == null) { return NotFound(); }
			List<ProductModel> products = await _dataContext.Products.OrderByDescending(p => p.Id).Where(p => p.BrandId == Id).Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			if (pg < 1) { pg = 1; }

			const int pageSize = 3;
			int countProduct = products.Count();

			var pager = new Paginate(countProduct, pg, pageSize);

			int recSkip = (pg - 1) * pageSize;

			var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
			ViewBag.Pager = pager;

			return View(data);
		}
	}
}
