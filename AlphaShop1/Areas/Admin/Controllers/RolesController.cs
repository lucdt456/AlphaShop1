using AlphaShop1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RolesController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int pg = 1)
		{
			var roles = await _roleManager.Roles.OrderByDescending(p => p.Id).ToListAsync();
			const int pagSize = 5;

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = roles.Count();
			var pager = new Paginate(recsCount, pg, pagSize);
			int recSkip = (pg - 1) * pagSize;

			var data = roles.Skip(recSkip).Take(pager.PageSize).ToList();
			ViewBag.Pager = pager;

			return View(roles);
		}

		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(string roleName)
		{
			var roleExitsts = await _roleManager.RoleExistsAsync(roleName);
			if (roleExitsts)
			{
				ModelState.AddModelError("", "Role đã tồn tại");
			}

			var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}

			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}

			return View(role);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, string roleName)
		{
			if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(roleName))
			{
				ModelState.AddModelError("", "Thông tin không hợp lệ.");
				return View();
			}

			var roleExitsts = await _roleManager.RoleExistsAsync(roleName);
			if (roleExitsts)
			{
				ModelState.AddModelError("", "Role đã tồn tại");
			}

			var role = await _roleManager.FindByIdAsync(id);

			if (role == null)
			{
				return NotFound();
			}

			role.Name = roleName;
			var result = await _roleManager.UpdateAsync(role);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(role);

		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}

			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}

			return View(role);
		}

		[HttpPost]
		public async Task<IActionResult> DeletePOST(string id)
		{

			var role = await _roleManager.FindByIdAsync(id);

			if (role == null)
			{
				return NotFound();
			}
			var result = await _roleManager.DeleteAsync(role);

			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(role);

		}

	}
}
