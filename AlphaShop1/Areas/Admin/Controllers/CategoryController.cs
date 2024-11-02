using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

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

		public async Task<IActionResult> Index(int pg = 1)
		{
			var categories = await _dB.Categories.OrderByDescending(p => p.Id).ToListAsync();
			const int pagSize = 5;

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = categories.Count();
			var pager = new Paginate(recsCount, pg, pagSize);
			int recSkip = (pg - 1) * pagSize;

			var data = categories.Skip(recSkip).Take(pager.PageSize).ToList();
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
