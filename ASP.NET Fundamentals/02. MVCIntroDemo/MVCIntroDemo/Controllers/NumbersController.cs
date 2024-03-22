using Microsoft.AspNetCore.Mvc;

namespace MVCIntroDemo.Controllers
{
	
	public class NumbersController : Controller
	{
		private readonly ILogger logger;
		public NumbersController(ILogger<NumbersController> _logger)
		{
			this.logger = _logger;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult NumbersToN(int number=123)
		{
			ViewBag.Count=number;
			return View();
		}
	}
}
