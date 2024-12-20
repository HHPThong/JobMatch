﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;
using FPTJobMatch.Repository;

namespace FPTJobMatch.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	[Authorize(Roles = "Administrator")]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public CategoryController(IUnitOfWork UnitOfWork)
		{
			_unitOfWork = UnitOfWork;
		}
		public IActionResult Index()
		{
			List<Category> myList = _unitOfWork.CategoryRepository.GetAll("ApplicationUser").ToList();
			return View(myList);
		}
		public async Task<IActionResult> ToggleAvailability(int id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Category? category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
			if (category == null)
			{
				return NotFound();
			}

			category.Availability = !category.Availability;

			_unitOfWork.Save();

			TempData["Success"] = category.Availability ? "Category approve successfully!" : "Category refuse successfully!";
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int? Id)
		{
			if (Id == null || Id == 0)
			{
				return NotFound();
			}
			Category? category = _unitOfWork.CategoryRepository.Get(c => c.Id == Id, "ApplicationUser");
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}
		[HttpPost]
		public IActionResult Delete(Category category)
		{
			_unitOfWork.CategoryRepository.Delete(category);
			_unitOfWork.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}
	}
}

