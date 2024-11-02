using AlphaShop1.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Repository.Components
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly DataContext _dataContext;
		public CategoriesViewComponent(DataContext context)
		{
			_dataContext = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var Data = await _dataContext.Categories.Where(p => p.Status == 1).Select(c => new SiderbarVM
			{
				Id = c.Id,
				Name = c.Name,
				ProductCount = _dataContext.Products.Count(p=> p.CategoryId == c.Id),
				AllProductCount = _dataContext.Products.Count()
			}).ToListAsync();

			return View(Data);
		}
	}
}
