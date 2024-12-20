﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;
using System.Security.Claims;

namespace FPTJobMatch.Area.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = "Employer")]
    public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;       
		public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId != null)
			{
				var myList = _unitOfWork.CategoryRepository.GetAll()
					.Where(c => c.UserId == userId)  
					.ToList();
				return View(myList);
			}
			return View(new List<Category>());
		}
		public async Task<IActionResult> ToggleNotification(int id)
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

			category.NotificationStatus = !category.NotificationStatus;

			_unitOfWork.Save();

			TempData["Success"] = "Notification status delete successfully!";
			return RedirectToAction("Index");
		}
		public IActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
				var claimIdentity = (ClaimsIdentity)User.Identity;
				var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

				if (userId != null)
				{
					category.UserId = userId;
					category.DateCreate = DateTime.Now;
					_unitOfWork.CategoryRepository.Add(category);
					_unitOfWork.CategoryRepository.Save();
					TempData["success"] = "Category created successfully";
				}
				return RedirectToAction("Index");
			}
			return View();
		}           

        public IActionResult Edit(int? ID)
        {
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			Category? category = _unitOfWork.CategoryRepository.Get(c => c.Id == ID);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
				var claimIdentity = (ClaimsIdentity)User.Identity;
				var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
				if (userId != null)
                {
					category.UserId = userId;
					category.DateCreate = DateTime.Now;
					_unitOfWork.CategoryRepository.Update(category);
					_unitOfWork.CategoryRepository.Save();
					TempData["success"] = "Category edited successfully";
				}               
				return RedirectToAction("Index");
            };
            return View();  
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.CategoryRepository.Get(j => j.Id == id);
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
			_unitOfWork.CategoryRepository.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");           
        }
    }
}
