using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Areas.Admin.Controllers
{
	[Area("Admin")]
	
	public class UserController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<AppUserModel> _userManager;
		public UserController(RoleManager<IdentityRole> roleManager, UserManager<AppUserModel> userManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task< IActionResult> Index()
		{
			var user = await _userManager.Users.OrderByDescending(p => p.Id).ToListAsync();
			return View(user);
		}

		[HttpGet]
		public async Task< IActionResult> Create()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");
			return View(new AppUserModel());
		}
	}
}
