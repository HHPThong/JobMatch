using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;
using FPTJobMatch.Repository;

namespace FPTJobMatch.Area.Administrator.Controllers
{
	[Area("Administrator")]
	[Authorize(Roles = "Administrator")]
	public class ApplicationJobController : Controller
	{
		private readonly IJobRepository _jobRepostitory;
		private readonly IApplicationJobRepository _applicationJobRepository;
		private readonly IStatusRepository _statusRepository;
		private readonly ITimeWorkRepository _workRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private ApplicationJobController(IJobRepository jobRepostitory, IApplicationJobRepository applicationJobRepository, IStatusRepository statusRepository, ITimeWorkRepository workRepository)
		{
			_jobRepostitory = jobRepostitory;
            _applicationJobRepository = applicationJobRepository;
			_statusRepository = statusRepository;
			_workRepository = workRepository;
		}

		public IActionResult Index()
		{
			List<ApplicationJob> myList = _applicationJobRepository.GetAll("applicationJob").ToList();
			return View(myList);
		}
		public IActionResult Delete(int? ApplicationJobId)
		{
			if (ApplicationJobId == null || ApplicationJobId == 0)
			{
				return NotFound();
			}
			ApplicationJob? applicationJob = _applicationJobRepository.Get(j => j.Id == ApplicationJobId);
			if (applicationJob == null)
			{
				return NotFound();
			}
			return View(applicationJob);
		}
		[HttpPost]
		public IActionResult Delete(ApplicationJob? applicationJob)
		{
			_applicationJobRepository.Delete(applicationJob);
			_applicationJobRepository.Save();
			TempData["Success"] = "CV Deleted Successfully";
			return RedirectToAction("Index");
		}
	}
}

