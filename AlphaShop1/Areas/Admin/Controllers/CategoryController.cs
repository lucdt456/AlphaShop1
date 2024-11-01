using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class CategoryController : Controller
	{
		private readonly DataContext _dB;
		public CategoryController(DataContext dB)

		{
			_dB = dB;
		}

		public async Task<IActionResult> Index()
		{
			var product = await _dB.Categories.OrderByDescending(p => p.Id).ToListAsync();
			return View(product);
		}

		//Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CategoryModel category)
		{
			ModelState.Remove("Slug");
			if (ModelState.IsValid)
			{
				category.Slug = category.Name.Replace(" ", "-");
				//var slug = _dB.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);
				//if (slug != null)
				//{
				//	ModelState.AddModelError("", "Category đã tồn tại!");
				//	return View(category);
				//}

				_dB.Categories.Add(category);
				_dB.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(category);
		}


		//Edit

		[HttpGet]
		public async Task<IActionResult> Edit(int Id)
		{
			CategoryModel category = await _dB.Categories.FindAsync(Id);
			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, CategoryModel category)
		{
			ModelState.Remove("Slug");
			if (ModelState.IsValid)
			{
				category.Slug = category.Name.Replace(" ", "-");
				_dB.Categories.Update(category);
				await _dB.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(category);
		}


		//Delete
		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			var category = await _dB.Categories.FindAsync(Id);
			return View(category);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(int Id)
		{
			CategoryModel category = await _dB.Categories.FirstOrDefaultAsync(p => p.Id == Id);

			_dB.Categories.Remove(category);
			await _dB.SaveChangesAsync();
			return RedirectToAction("Index");
		}

	}
}
