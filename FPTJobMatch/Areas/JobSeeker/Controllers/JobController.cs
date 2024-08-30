using Microsoft.AspNetCore.Mvc;

namespace FPTJobMatch.Areas.JobSeeker.Controllers
{
	public class JobController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
