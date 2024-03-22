using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Homies.Controllers
{
	[Authorize]
	public class EventController : Controller
	{
		private readonly HomiesDbContext data;
		public EventController(HomiesDbContext data)
		{
			this.data = data;
		}

		public async Task<IActionResult> AllAsync()
		{
			var events = await data.Events
				.Select(e=>new EventInfoViewModel(e.Id,e.Name,e.Start,e.Type.Name,e.Organiser.UserName)).ToListAsync();

			return View(events);
		}

		[HttpPost]
		public async Task<IActionResult> Join(int id)
		{
			var e = await data.Events
				.Where(e => e.Id == id)
				.Include(e => e.EventsParticipants)
				.FirstOrDefaultAsync();

			if (e == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();

			if (!e.EventsParticipants.Any(p => p.HelperId == userId))
			{
				e.EventsParticipants.Add(new EventParticipant()
				{
					EventId = e.Id,
					HelperId = userId
				});

				await data.SaveChangesAsync();
			}

			return RedirectToAction("Joined");
		}

		[HttpGet]
		public async Task<IActionResult> Joined()
		{
			string userId = GetUserId();

			var model = data.EventParticipants.Where(ep => ep.HelperId == userId)
				.AsNoTracking()
				.Select(ep => new EventInfoViewModel(ep.EventId, ep.Event.Name, ep.Event.Start, ep.Event.Type.Name, ep.Event.Organiser.UserName)).ToList();

			return View(model);
		}

		public async Task<IActionResult> Leave(int id)
		{
			var e = await data.Events
				.Where(e => e.Id == id)
				.Include(e => e.EventsParticipants)
				.FirstOrDefaultAsync();

			if (e == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();

			var ep = e.EventsParticipants
				.FirstOrDefault(ep => ep.HelperId == userId);

			if (ep == null)
			{
				return BadRequest();
			}

			e.EventsParticipants.Remove(ep);

			await data.SaveChangesAsync();

			return RedirectToAction("All");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value??string.Empty;
		}
	}
}
