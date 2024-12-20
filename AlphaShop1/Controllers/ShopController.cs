﻿using AlphaShop1.Migrations;
using AlphaShop1.Models;
using AlphaShop1.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Controllers
{
	public class ShopController : Controller
	{

		public readonly DataContext _dataContext;

		public ShopController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		//Hiển thị danh sách hàng hóa + Tìm kiếm
		public async Task<IActionResult> Index(string query = "",int pg = 1, int size = 8)
		{
			List<ProductModel> products = await _dataContext.Products.OrderByDescending(p => p.Id).Where(p => p.Name.Contains(query)).Include(p => p.Category).Include(p => p.Brand).ToListAsync();

			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = products.Count();
			var pager = new Paginate(recsCount, pg, size);
			int recSkip = (pg - 1) * size;

			var data = products.Skip(recSkip).Take(pager.PageSize).ToList();
			ViewBag.Pager = pager;
			ViewBag.Query = query;
			ViewBag.Size = size;
			return View(data);
		}
	}
}