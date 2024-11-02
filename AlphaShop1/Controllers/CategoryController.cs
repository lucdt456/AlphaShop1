using AlphaShop1.Models;
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

		public async Task<IActionResult> Index(int? Id, int pg = 1)
		{
			if (Id == null)
			{
				return NotFound();
			}

			var products = await _dataContext.Products.OrderByDescending(p => p.Id).Where(p => p.CategoryId == Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync();


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
