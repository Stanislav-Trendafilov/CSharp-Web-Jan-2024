using HouseRentingSystem.Models.Agents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Humanizer.In;

namespace HouseRentingSystem.Controllers
{
	[Authorize]
	public class AgentController : Controller
	{
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel model)
		{
			return RedirectToAction(nameof(HouseController.All),"Houses");
		}
	}
}
