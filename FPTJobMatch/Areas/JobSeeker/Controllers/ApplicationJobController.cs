using Microsoft.AspNetCore.Mvc;

namespace FPTJobMatch.Areas.Administrator.Controllers
{
	public class ApplicationJobController : Controller
	{
		public ApplicationJobController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
