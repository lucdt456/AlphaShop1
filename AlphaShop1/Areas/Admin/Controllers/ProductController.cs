using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class ProductController : Controller
	{
		private readonly DataContext _dB;
		private readonly IWebHostEnvironment _webhostEnvironment;

		public ProductController(DataContext context, IWebHostEnvironment webhostEnvironment)
		{
			_dB = context;
			_webhostEnvironment = webhostEnvironment;
		}

		public async Task<IActionResult> Index()
		{
			var product = await _dB.Products.OrderBy(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync();
			return View(product);
		}

		//Create

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_dB.Categories, "Id", "Name");
			ViewBag.Brands = new SelectList(_dB.Brands, "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductModel product)
		{
			ViewBag.Categories = new SelectList(_dB.Categories, "Id", "Name", product.CategoryId);
			ViewBag.Brands = new SelectList(_dB.Brands, "Id", "Name", product.BrandId);

			ModelState.Remove("Slug");
			ModelState.Remove("Category");
			ModelState.Remove("Brand");
			ModelState.Remove("Image");

			if (ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ", "-");
				var slug = await _dB.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
				if (slug != null)
				{
					ModelState.AddModelError("", "SP đã có trong DTB");
					return View(product);
				}
				else
				{
					if (product.ImageUpload != null)
					{
						string uploadsDir = Path.Combine(_webhostEnvironment.WebRootPath, "media/products");
						string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
						string filePath = Path.Combine(uploadsDir, imageName);
						FileStream fs = new FileStream(filePath, FileMode.Create);
						await product.ImageUpload.CopyToAsync(fs);
						fs.Close();
						product.Image = imageName;
					}
				}
				_dB.Add(product);
				await _dB.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(product);
		}


		//Edit

		[HttpGet]
		public async Task<IActionResult> Edit(int Id)
		{
			ProductModel product = await _dB.Products.FindAsync(Id);
			ViewBag.Categories = new SelectList(_dB.Categories, "Id", "Name", product.CategoryId);
			ViewBag.Brands = new SelectList(_dB.Brands, "Id", "Name", product.BrandId);

			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, ProductModel product)
		{
			ViewBag.Categories = new SelectList(_dB.Categories, "Id", "Name", product.CategoryId);
			ViewBag.Brands = new SelectList(_dB.Brands, "Id", "Name", product.BrandId);

			ModelState.Remove("Slug");
			ModelState.Remove("Category");
			ModelState.Remove("Brand");
			ModelState.Remove("Image");

			if (ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ", "-");

				if (product.ImageUpload != null)
				{
					// Xóa ảnh cũ
					string uploadsDir = Path.Combine(_webhostEnvironment.WebRootPath, "media/products");
					ProductModel existingProduct = await _dB.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Id);

					if (existingProduct != null && !string.IsNullOrEmpty(existingProduct.Image))
					{
						string oldFileImage = Path.Combine(uploadsDir, existingProduct.Image);
						if (System.IO.File.Exists(oldFileImage))
						{
							System.IO.File.Delete(oldFileImage);
						}
					}

					// Lưu ảnh mới
					string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
					string filePath = Path.Combine(uploadsDir, imageName);
					using (FileStream fs = new FileStream(filePath, FileMode.Create))
					{
						await product.ImageUpload.CopyToAsync(fs);
					}
					product.Image = imageName;
				}

				_dB.Update(product);
				await _dB.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(product);
		}


		//Delete

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			ProductModel product = await _dB.Products.FindAsync(Id);
			ViewBag.Categories = new SelectList(_dB.Categories, "Id", "Name", product.CategoryId);
			ViewBag.Brands = new SelectList(_dB.Brands, "Id", "Name", product.BrandId);

			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(int Id)
		{
			ProductModel product = await _dB.Products.FirstOrDefaultAsync(x => x.Id == Id);

			//Xoá ảnh
			string uploadsDir = Path.Combine(_webhostEnvironment.WebRootPath, "media/products");
			string oldfileImange = Path.Combine(uploadsDir, product.Image);
			if (System.IO.File.Exists(oldfileImange))
			{
				System.IO.File.Delete(oldfileImange);
			}

			_dB.Products.Remove(product);
			await _dB.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}
