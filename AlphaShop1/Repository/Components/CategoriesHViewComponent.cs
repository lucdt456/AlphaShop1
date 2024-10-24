using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Repository.Components
{
	public class CategoriesHViewComponent : ViewComponent
	{
		private readonly DataContext _dataContext;
		public CategoriesHViewComponent(DataContext context)
		{
			_dataContext = context;
		}

		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Categories.ToListAsync());
	}
}
