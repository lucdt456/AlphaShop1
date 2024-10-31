using AlphaShop1.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop1.Repository.Components
{
	public class CartViewViewComponent : ViewComponent
	{
		private readonly DataContext _dB;
		public CartViewViewComponent(DataContext dB)
		{
			_dB = dB;
		}
		const string CART_KEY = "MYCART";
		public List<CartItem2> Cart => HttpContext.Session.Get<List<CartItem2>>(CART_KEY) ?? new List<CartItem2>();
		public async Task<IViewComponentResult> InvokeAsync()
		{
			int sl = Cart.Count;
			return (IViewComponentResult)View(sl);
		}
	}
}
