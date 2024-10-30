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

		public async Task<ActionResult> Index()
		{
			var brand = await _dB.Brands.OrderBy(p => p.Id).ToListAsync();
			return View(brand);
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
