using AlphaShop1.Models;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is Large Product", Status = 1 };
				CategoryModel pc = new CategoryModel { Name = "PC", Slug = "pc", Description = "PC is Large Product", Status = 1 };

				BrandModel apple= new BrandModel { Name = "Apple", Slug = "apple", Description = "Apple is Large Brand", Status = 1 };
				BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "samsung", Description = "Apple is Large Brand", Status = 1 };

				_context.Products.AddRange(
					new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is best",Image="1.jpg", Category=macbook, Brand=apple,Price=123 }, 
					new ProductModel { Name = "Macbook", Slug = "pc", Description = "Macbook is best", Image = "1.jpg", Category = pc, Brand = apple, Price = 123 }
					);
			}
			_context.SaveChanges();
		}
	}
}
