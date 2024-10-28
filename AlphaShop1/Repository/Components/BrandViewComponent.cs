using AlphaShop1.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Repository.Components
{
	public class BrandViewComponent : ViewComponent
	{
		private readonly DataContext _dataContext;
		public BrandViewComponent(DataContext context)
		{
			_dataContext = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var Data = await _dataContext.Brands.Select(c => new SiderbarVM
            {
				Id = c.Id,
				Name = c.Name,
				ProductCount = _dataContext.Products.Count(p => p.BrandId == c.Id)
			}).ToListAsync();

			return View(Data);
		}
	}
}
