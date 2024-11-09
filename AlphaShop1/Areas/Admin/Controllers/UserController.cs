using AlphaShop1.Models;
using AlphaShop1.Models.ViewModel;
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

		public async Task<IActionResult> Index(int pg = 1)
		{
			var users = await _userManager.Users.OrderByDescending(p => p.Id).ToListAsync();

			var usersWithRole = new List<UserManagerVM>();
			foreach(var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				usersWithRole.Add(new UserManagerVM
				{
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = roles.ToList()
				});
			}

			const int pagSize = 5;

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = usersWithRole.Count();
			var pager = new Paginate(recsCount, pg, pagSize);
			int recSkip = (pg - 1) * pagSize;

			var data = usersWithRole.Skip(recSkip).Take(pager.PageSize).ToList();
			ViewBag.Pager = pager;

			return View(data);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Name", "Name");
			return View(new AppUserModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(AppUserModel user, string roleName)
		{
            if (ModelState.IsValid)
            {
                AppUserModel newUser = new AppUserModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
					PhoneNumber = user.PhoneNumber,
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, user.PasswordHash);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        await _userManager.AddToRoleAsync(newUser, roleName);
                    }
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    Console.WriteLine(error.Description);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(user);
        }


		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Name", "Name");
			return View(new AppUserModel());
		}

		[HttpPost]
		public async Task<IActionResult> Edit(AppUserModel user, string roleName, string id)
		{
			if (ModelState.IsValid)
			{
				AppUserModel newUser = new AppUserModel
				{
					UserName = user.UserName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
				};

				IdentityResult result = await _userManager.CreateAsync(newUser, user.PasswordHash);
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(roleName))
					{
						await _userManager.AddToRoleAsync(newUser, roleName);
					}
					return RedirectToAction("Index");
				}

				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
					Console.WriteLine(error.Description);
				}
			}
			else
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{
					Console.WriteLine(error.ErrorMessage);
				}
			}
			return View(user);
		}
	}
}
