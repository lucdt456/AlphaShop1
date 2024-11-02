using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

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

		public async Task< IActionResult> Index(int pg = 1)
		{
			var user = await _userManager.Users.OrderByDescending(p => p.Id).ToListAsync();


			const int pagSize = 5;

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = user.Count();
			var pager = new Paginate(recsCount, pg, pagSize);
			int recSkip = (pg - 1) * pagSize;

			var data = user.Skip(recSkip).Take(pager.PageSize).ToList();
			ViewBag.Pager = pager;

			return View(data);
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
