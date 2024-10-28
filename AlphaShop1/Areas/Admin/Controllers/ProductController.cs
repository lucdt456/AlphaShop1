using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Areas.Admin.Controllers
{
	[Area("Admin")]
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
					string uploadsDir = Path.Combine(_webhostEnvironment.WebRootPath, "imgs");
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
	}
}
