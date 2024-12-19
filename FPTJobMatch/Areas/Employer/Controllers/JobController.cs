using FPTJobMatch.Models;
using FPTJobMatch.Repository;
using FPTJobMatch.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace FPTJobMatch.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = "Employer")]
    public class JobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public JobController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var myList = _unitOfWork.JobRepository.GetAll("Category")
                    .Where(j => j.UserId == userId)  // Chỉ lấy các job thuộc về user hiện tại
                    .ToList();
                return View(myList);
            }
            return View(new List<Job>());
        }

		public IActionResult Create()
		{
			JobVM jobVM = new JobVM()
			{
				Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = c.Name,
					Value = c.Id.ToString(),
				}),
				Job = new Job()
			};
			return View(jobVM);
		}
		[HttpPost]
		public IActionResult Create(JobVM jobVM)
		{
			if (ModelState.IsValid)
			{
				var claimIdentity = (ClaimsIdentity)User.Identity;
				var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

				if (userId != null)
				{
					jobVM.Job.UserId = userId;
					_unitOfWork.JobRepository.Add(jobVM.Job);
					_unitOfWork.JobRepository.Save();
					TempData["success"] = "Job created successfully";
					return RedirectToAction("Index");
				}
			}
			return View(jobVM);
		}

		public IActionResult Edit(int? id)
		{
			JobVM jobVM = new JobVM()
			{
				Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = c.Name,
					Value = c.Id.ToString(),
				}),
				Job = _unitOfWork.JobRepository.Get(c => c.ID == id)
			};
			jobVM.Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = c.Name,
				Value = c.Id.ToString(),
			});
			return View(jobVM);
		}

		[HttpPost]
		public IActionResult Edit(JobVM jobVM)
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			if (userId != null)
			{
				if (ModelState.IsValid)
				{
					jobVM.Job.UserId = userId;
					_unitOfWork.JobRepository.Update(jobVM.Job);
					_unitOfWork.JobRepository.Save();
					TempData["success"] = "Job edited successfully";
					return RedirectToAction("Index");
				}
			}
			return View(jobVM);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Job? job = _unitOfWork.JobRepository.Get(c => c.ID == id, "Category");
			if (job == null)
			{
				return NotFound();
			}
			return View(job);
		}
		[HttpPost]
		public IActionResult Delete(Job job)
		{
			_unitOfWork.JobRepository.Delete(job);
			_unitOfWork.JobRepository.Save();
			TempData["success"] = "Job deleted successfully";
			return RedirectToAction("Index");
		}
		public IActionResult ViewJob(int? ID, string sortBy, string filterBy)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}

			// Lọc dữ liệu
			Expression<Func<ApplicationJob, bool>> filter = j => j.JobID == ID;
			var jobApps = _unitOfWork.ApplicationJobRepository.GetAllAppJob(filter);

			// Sắp xếp dữ liệu
			if (!string.IsNullOrEmpty(sortBy))
			{
				switch (sortBy)
				{
					case "email":
						jobApps = jobApps.OrderBy(j => j.Email);
						break;
					case "emailDesc":
						jobApps = jobApps.OrderByDescending(j => j.Email);
						break;
					case "dayApply":
						jobApps = jobApps.OrderBy(j => j.DayApply); // Sắp xếp theo ngày áp dụng giảm dần
						break;
					case "dayApplyDesc":
						jobApps = jobApps.OrderByDescending(j => j.DayApply); // Sắp xếp theo ngày áp dụng giảm dần
						break;
				}
			}

			// Search Email
			if (!string.IsNullOrEmpty(filterBy))
			{
				// Lọc theo Email
				jobApps = jobApps.Where(j => j.Email.Contains(filterBy));
			}

			return View(jobApps);
		}
		public async Task<IActionResult> ViewProfile(int? ID)
		{
			if (ID == null)
			{
				return NotFound();
			}

			var applicationJob = _unitOfWork.ApplicationJobRepository.Get(c => c.Id == ID);
			if (applicationJob == null)
			{
				return NotFound();
			}

			var jobSeeker = await _userManager.FindByEmailAsync(applicationJob.Email);
			if (jobSeeker == null)
			{
				return NotFound();
			}

			return View(jobSeeker);
		}

		public IActionResult Accept(int? ID)
		{
			if (ID == null)
			{
				return NotFound();
			}

			var jobApp = _unitOfWork.ApplicationJobRepository.Get(c => c.Id == ID);
			if (jobApp == null)
			{
				return NotFound();
			}

			jobApp.Status = true;
			_unitOfWork.ApplicationJobRepository.Update(jobApp);
			_unitOfWork.Save(); // Lưu thay đổi vào cơ sở dữ liệu

			return RedirectToAction("Index");
		}
		public IActionResult Decline(int? ID)
		{
			if (ID == null)
			{
				return NotFound();
			}

			var jobApp = _unitOfWork.ApplicationJobRepository.Get(c => c.Id == ID);
			if (jobApp == null)
			{
				return NotFound();
			}
			_unitOfWork.ApplicationJobRepository.Delete(jobApp);
			_unitOfWork.Save(); // Lưu thay đổi vào cơ sở dữ liệu

			return RedirectToAction("Index");
		}
	}
}