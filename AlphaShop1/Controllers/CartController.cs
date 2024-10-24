using AlphaShop1.Models.ViewModel;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop1.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _dB;
		public CartController(DataContext dB)
		{
			_dB = dB;
		}
		const string CART_KEY = "MYCART";

		public List<CartItem2> Cart => HttpContext.Session.Get<List<CartItem2>>(CART_KEY) ?? new List<CartItem2>();



		public IActionResult Index()
		{
			return View(Cart);
		}

		public IActionResult CheckOut()
		{
			return View();
		}

		public IActionResult AddToCart(int Id, int quantity = 1)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.Id == Id);

			if (item == null)
			{
				var product = _dB.Products.SingleOrDefault(p => p.Id == Id);
				if (product == null)
				{
					return NotFound();
				}
				item = new CartItem2
				{
					Id = product.Id,
					Name = product.Name,
					Price = (double)product.Price,
					Image = product.Image ?? string.Empty,
					SoLuong = quantity
				};
				gioHang.Add(item);
			}
			else
			{
				item.SoLuong += quantity;
			}

			HttpContext.Session.Set(CART_KEY, gioHang);
			return RedirectToAction("Index");
		}

		public IActionResult RemoveCart(int Id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.Id == Id);
			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}

		public IActionResult IncreaseQuantity(int Id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.Id == Id);
			if (item != null)
			{
				item.SoLuong += 1;
				HttpContext.Session.Set(CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}

		public IActionResult DecreaseQuantity(int Id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.Id == Id);
			if (item.SoLuong > 0)
			{
				if (item != null)
				{
					item.SoLuong -= 1;
					HttpContext.Session.Set(CART_KEY, gioHang);
				}
				return RedirectToAction("Index");
			}else return RedirectToAction("RemoveCart", new { Id = Id });

		}
	}
}
