using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;
using FPTJobMatch.Repository;
using AspNetCore;


namespace FPTJobMatch.Areas.Administrator.Controllers
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
			List<ApplicationJob> myList = _applicationJobRepository.GetAll("ApplicationJob").ToList();
			return View(myList);
		}
		public IActionResult Create()
		{
			ApplicationJobVM applicationJobVM = new ApplicationJobVM();
			{
				jobs = _jobRepostitory.GetAll().Select(j => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = j.Name,
					Value = j.ID.ToString()
				}),
				Job = new Job()
			};
			return View(applicationJobVM);
			}
		}
	}
}
