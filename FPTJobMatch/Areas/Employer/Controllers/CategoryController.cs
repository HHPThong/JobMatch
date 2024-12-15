﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net; 
using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository;
using FPTJobMatch.Repository.IRepository;

namespace FPTJobMatch.Area.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = "Employer")]
    public class CategoryController : Controller
	{
		private readonly ITimeWorkRepository _workRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IJobRepository _jobRepostitory;
        private readonly IApplicationJobRepository _applicationJobRepository;
        private readonly IStatusRepository _statusRepository;
        
        public CategoryController(IWebHostEnvironment webHostEnvironment , IJobRepository jobRepostitory, IApplicationJobRepository applicationJobRepository, IStatusRepository statusRepository, ITimeWorkRepository workRepository)
        {
            _jobRepostitory = jobRepostitory;
            _applicationJobRepository = applicationJobRepository;
            _statusRepository = statusRepository;
            _workRepository = workRepository;
            _webHostEnvironment = webHostEnvironment;

        }


        public IActionResult Index()
        {

            List<ApplicationJob> myList = _applicationJobRepository.GetAll("Job").ToList();
            return View(myList);
        }

        public IActionResult Create()
        {
            ApplicationJobVM applicationJobVM = new ApplicationJobVM()
            {
                Job = _jobRepostitory.GetAll().Select(j => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = j.Name,
                    Value = j.ID.ToString()
                }),
				Status = _jobRepostitory.GetAll().Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = s.Name,
					Value = s.ID.ToString()
				}),
				applicationJob = new ApplicationJob()
            };
            return View(applicationJobVM);
        }

        [HttpPost]
        public IActionResult Create(ApplicationJob? applicationJob,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string  applicationPath = Path.Combine(wwwRootPath, @"img\CV");
                    using (var fileStream = new FileStream(Path.Combine(applicationPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    applicationJob.CV = @"\img\CV\" + fileName;
                }
                _applicationJobRepository.Add(applicationJob);
                _applicationJobRepository.Save();
                TempData["success"] = "CV Created successfully";
                return RedirectToAction("Index");
            }
            ApplicationJobVM applicationJobVM = new ApplicationJobVM()
            {
                Job = _jobRepostitory.GetAll().Select(j => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = j.Name,
                    Value = j.ID.ToString()
                }),

				 Status = _jobRepostitory.GetAll().Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				 {
					 Text = s.Name,
					 Value = s.ID.ToString()
				 }),
				applicationJob = new ApplicationJob()
			};

            return View(applicationJobVM);
        }

        public IActionResult Edit(int? ApplicationJobID)
        {
            ApplicationJobVM applicationJobVM = new ApplicationJobVM()
            {
                Job = _jobRepostitory.GetAll().Select(j => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = j.Name,
                    Value = j.ID.ToString()
                }),
				Status = _jobRepostitory.GetAll().Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = s.Name,
					Value = s.ID.ToString()
				}),
				applicationJob = new ApplicationJob()
            };
            if (ApplicationJobID == null || ApplicationJobID == 0)
            {
                return NotFound();
            }
            applicationJobVM.applicationJob = _applicationJobRepository.Get(j => j.Id == ApplicationJobID);
            if (applicationJobVM.applicationJob == null)
            {
                return NotFound();
            }
            return View(applicationJobVM);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationJob? applicationJob, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPart = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string applicationPath = Path.Combine(wwwRootPart, @"img\CV");
                    if (!string.IsNullOrEmpty(applicationJob.CV))
                    {
                        var oldCVPath = Path.Combine(wwwRootPart,applicationJob.CV.TrimStart('\\'));
                        if (System.IO.File.Exists(oldCVPath))
                        {
                            System.IO.File.Delete(oldCVPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(applicationPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    applicationJob.CV = @"\img\CV\" + fileName;
                }
                _applicationJobRepository.Update(applicationJob);
                _applicationJobRepository.Save();
                TempData["success"] = "CV Updated successfully";
                return RedirectToAction("Index");
            }
            ApplicationJobVM applicationJobVM = new ApplicationJobVM()
            {
                Job = _jobRepostitory.GetAll().Select(j => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = j.Name,
                    Value = j.ID.ToString()
                }),
				Status = _jobRepostitory.GetAll().Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = s.Name,
					Value = s.ID.ToString()
				}),
				applicationJob = _applicationJobRepository.Get(j => j.Id == applicationJob.Id)
            };
            return View(applicationJobVM);  
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