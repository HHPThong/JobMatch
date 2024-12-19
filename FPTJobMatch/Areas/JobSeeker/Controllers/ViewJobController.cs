using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FPTJobMatch.Areas.JobSeeker.Controllers
{
    [Area("JobSeeker")]
    [Authorize(Roles = "JobSeeker")]
	public class ViewJobController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;

		public ViewJobController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}


		public IActionResult Index()
		{
			List<Job> myList = _unitOfWork.JobRepository.GetAll("Category").ToList();
			return View(myList);
		}

		public IActionResult Apply(int? ID)
		{
			if (ID == null)
			{
				return NotFound();
			}
			Job? job = _unitOfWork.JobRepository.Get(c => c.ID == ID);
			if (job == null)
			{
				return NotFound();
			}
			JobVM JobVm = new JobVM();
			JobVm.apply = new ApplicationJob();
			JobVm.apply.JobID = job.ID;
			JobVm.Job = job;

			return View(JobVm);
		}
		[HttpPost]
		public async Task<IActionResult> Apply(JobVM job)
		{
			if (ModelState.IsValid)
			{
				var currentUser = await _userManager.GetUserAsync(User);

				if (currentUser == null)
				{
					return RedirectToAction("Login", "Account");
				}

				job.apply.Email = currentUser.Email;

				DateTime currentDate = DateTime.Now;

				job.apply.DayApply = currentDate;

				_unitOfWork.ApplicationJobRepository.Add(job.apply);
				_unitOfWork.Save();

				TempData["success"] = "Job applied successfully";
				return RedirectToAction("Index");
			}
			return View(job);
		}
	}
}
