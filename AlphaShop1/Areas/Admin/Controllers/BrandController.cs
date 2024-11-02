using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class BrandController : Controller
	{
		private readonly DataContext _dB;
		public BrandController(DataContext db)
		{
			_dB = db;
		}

		public async Task<ActionResult> Index(int pg = 1)
		{
			List<BrandModel> brand = await _dB.Brands.OrderByDescending(p => p.Id).ToListAsync();

			const int pagSize = 5;

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = brand.Count();
			var pager = new Paginate(recsCount, pg, pagSize);
			int recSkip = (pg - 1) * pagSize;

			var data = brand.Skip(recSkip).Take(pager.PageSize).ToList();
			ViewBag.Pager = pager;

			return View(data);
		}

		//Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BrandModel brand)
		{
			ModelState.Remove("Slug");
			if (ModelState.IsValid)
			{
				brand.Slug = brand.Name.Replace(" ", "-");
				_dB.Brands.Add(brand);
				_dB.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(brand);
		}


		//Edit

		[HttpGet]
		public async Task<IActionResult> Edit(int Id)
		{
			BrandModel brand = await _dB.Brands.FindAsync(Id);
			return View(brand);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, BrandModel brand)
		{
			ModelState.Remove("Slug");
			if (ModelState.IsValid)
			{
				brand.Slug = brand.Name.Replace(" ", "-");
				_dB.Brands.Update(brand);
				await _dB.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(brand);
		}


		//Delete
		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			var brand = await _dB.Brands.FindAsync(Id);
			return View(brand);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(int Id)
		{
			BrandModel brand = await _dB.Brands.FirstOrDefaultAsync(p => p.Id == Id);

			_dB.Brands.Remove(brand);
			await _dB.SaveChangesAsync();
			return RedirectToAction("Index");
		}

	}
}
