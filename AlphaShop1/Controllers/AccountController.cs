using AlphaShop1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop1.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManager;
		private SignInManager<AppUserModel> _signInManager;

		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Create()
		{
			return View();
		}

		public async Task<IActionResult> Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid)
			{
				AppUserModel newUser = new AppUserModel
				{
					UserName = user.UserName,
					Email = user.Email
				};

				IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
				if (result.Succeeded)
				{
					return Redirect("/admin/products");
				}

				foreach(IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(user);
		}
	}
}
