using Microsoft.AspNetCore.Mvc;

namespace FPTJobMatch.Areas.JobSeeker.Controllers
{
	public class ApplicationStatusController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
