using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository;
using FPTJobMatch.Repository.IRepository;
using Microsoft.AspNetCore.Identity;


namespace FPTJobMatch.Area.Administrator.Controllers
{
    [Area("Aministrator")]
	[Authorize(Roles = "Administrator")]

	public class UserController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
		public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			List<ApplicationUser> myList = _unitOfWork.AppUserRepository.GetAll().ToList();
			return View(myList);
		}
		public async Task<IActionResult> ToggleStatus(string id)
		{
			if (id == null)
			{
				return NotFound();
			}
			ApplicationUser? applicationUser = _unitOfWork.AppUserRepository.Get(c => c.Id == id);
			if (applicationUser == null)
			{
				return NotFound();
			}

			// Toggle the status
			applicationUser.Status = !applicationUser.Status;

			// Save changes
			_unitOfWork.Save();

			TempData["Success"] = applicationUser.Status ? "Account enabled successfully!" : "Account disabled successfully!";
			return RedirectToAction("Index");
		}


		public IActionResult Delete(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			ApplicationUser? applicationUser = _unitOfWork.AppUserRepository.Get(c => c.Id == id);
			if (applicationUser == null)
			{
				return NotFound();
			}

			return View(applicationUser);
		}

		[HttpPost, ActionName("Delete")]  //[HttpPost] or [HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string id)
		{
			ApplicationUser applicationUser = _unitOfWork.AppUserRepository.Get(c => c.Id == id);

			if (applicationUser == null)
			{
				return NotFound();
			}

			_unitOfWork.AppUserRepository.Delete(applicationUser);
			_unitOfWork.Save();
			TempData["Success"] = "Account deleted successfully!!";
			return RedirectToAction("index");
		}
	}
}