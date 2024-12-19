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
	public class ApplicationJobController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;

		public ApplicationJobController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}


		public IActionResult Index()
		{
			List<Job> myList = _unitOfWork.JobRepository.GetAll("Category").ToList();
			return View(myList);
		}

		public IActionResult Create(int? ID)
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
		public async Task<IActionResult> Create(JobVM job)
		{
			if (ModelState.IsValid)
			{
				// Lấy thông tin người dùng hiện tại từ UserManager
				var currentUser = await _userManager.GetUserAsync(User);

				if (currentUser == null)
				{
					// Xử lý trường hợp người dùng không tồn tại
					return RedirectToAction("Login", "Account");
				}

				// Gán email của người dùng hiện tại vào đối tượng JobApplication
				job.apply.Email = currentUser.Email;

				// Lấy ngày giờ hiện tại
				DateTime currentDate = DateTime.Now;

				// Gán ngày giờ hiện tại vào trường DayApply của đối tượng JobApplication
				job.apply.DayApply = currentDate;

				// Lưu đối tượng JobApplication vào cơ sở dữ liệu
				_unitOfWork.ApplicationJobRepository.Add(job.apply);
				_unitOfWork.Save();

				TempData["success"] = "Job applied successfully";
				return RedirectToAction("Index");
			}

			// Nếu ModelState không hợp lệ, trả về lại view với dữ liệu và thông báo lỗi
			return View(job);
		}
	}
}
