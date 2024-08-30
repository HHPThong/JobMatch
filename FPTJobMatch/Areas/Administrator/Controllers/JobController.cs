using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository;
using FPTJobMatch.Repository.IRepository;
using AspNetCore;


namespace FPTJobMatch.Controllers
{
	[Area("Aministrator")]
	[Authorize(Roles = "Administrator")]

	public class JobController : Controller
	{
		private readonly IJobRepository _jobRepository;
		private readonly ITimeWorkRepository _workRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;

		private JobController(IJobRepository jobRepository, ITimeWorkRepository workRepository, IWebHostEnvironment webHostEnvironment)
		{
			_jobRepository = jobRepository;
			_workRepository = workRepository;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			List<Job> myList = _jobRepository.GetAll().ToList();
			return View(myList);
		}
		[Authorize(Roles = "Administrator")]
		public IActionResult Delete() 
		{
			return View();
		}
		public IActionResult Delete(int? JobID)
		{
			if (JobID == null|| JobID == 0)
			{ 
				return NotFound();
			}
			Job? job = _jobRepository.Get(j => j.ID == JobID);
			if (job == null)
			{
				return NotFound();
			}
			return View(job);
		}
		[HttpPost]
		public IActionResult Delete(Job job)
		{
			_jobRepository.Delete(job);
			_jobRepository.Save();
			TempData["success"] = "Job Deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
