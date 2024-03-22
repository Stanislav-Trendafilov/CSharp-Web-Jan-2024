using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;

namespace SeminarHub.Controllers
{
	[Authorize]
	public class SeminarController : Controller
	{
		private readonly SeminarHubDbContext data;

		public SeminarController(SeminarHubDbContext data)
		{
			this.data = data;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> All()
		{
			var seminars = await data.Seminars
				.AsNoTracking()
				.Select(s => new SeminarViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					Lecturer = s.Lecturer,
					Organizer = s.Organizer.UserName,
					Category=s.Category.Name,
					DateAndTime=s.DateAndTime.ToString(DataConstants.DateFormat),
				}).ToListAsync();

			return View(seminars);
		}

		[HttpPost]
		public async Task<IActionResult> Join(int id)
		{
			var seminar = await data.Seminars
				.Where(s => s.Id == id)
				.Include(s => s.SeminarsParticipants)
				.FirstOrDefaultAsync();

			if (seminar == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();



			if (!seminar.SeminarsParticipants.Any(sp => sp.ParticipantId == userId))
			{
				seminar.SeminarsParticipants.Add(new SeminarParticipant()
				{
					SeminarId = seminar.Id,
					ParticipantId = userId
				});

				await data.SaveChangesAsync();
			}
			else
			{
                return RedirectToAction(nameof(All));
            }


			return RedirectToAction(nameof(Joined));
		}

		[HttpGet]
		public async Task<IActionResult> Joined()
		{
			string userId = GetUserId();

			var model = await data.SeminarsParticipants
				.Where(sp => sp.ParticipantId == userId)
				.AsNoTracking()
				.Select(sp => new SeminarViewModel
				{
					Id = sp.SeminarId,
					Topic = sp.Seminar.Topic,
					Lecturer = sp.Seminar.Lecturer,
					Organizer = sp.Seminar.Organizer.UserName,
					Category = sp.Seminar.Category.Name,
					DateAndTime = sp.Seminar.DateAndTime.ToString(DataConstants.DateFormat),
				}).ToListAsync(); ;

			return View(model);
		}

		public async Task<IActionResult> Leave(int id)
		{
			var seminar = await data.Seminars
				.Where(s => s.Id == id)
				.Include(s => s.SeminarsParticipants)
				.FirstOrDefaultAsync();

			if (seminar == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();

			var sp = seminar.SeminarsParticipants
				.FirstOrDefault(sp => sp.ParticipantId == userId);

			if (sp == null)
			{
				return BadRequest();
			}

			seminar.SeminarsParticipants.Remove(sp);

			await data.SaveChangesAsync();

			return RedirectToAction(nameof(Joined));
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new SeminarFormViewModel();
			model.Categories = await GetCategories();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(SeminarFormViewModel model)
		{
			DateTime start = DateTime.Now;

			if (!DateTime.TryParseExact(
				model.DateAndTime,
				DataConstants.DateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out start))
			{
				ModelState
					.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DataConstants.DateFormat}");
			}


			if (!ModelState.IsValid)
			{
				model.Categories = await GetCategories();

				return View(model);
			}
			string user = GetUserId();

			var entity = new Seminar()
			{
				DateAndTime = start,
				Duration= model.Duration,
				Topic= model.Topic,
				Lecturer= model.Lecturer,
				Details= model.Details,
				CategoryId=model.CategoryId	,
				OrganizerId = user
			};

			await data.Seminars.AddAsync(entity);
			await data.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		public async Task<IActionResult> Details(int id)
		{
			var model = await data.Seminars
				.Where(s => s.Id == id)
				.AsNoTracking()
				.Select(s => new SeminarDetailsViewModel()
				{
					Id=s.Id,
					Topic=s.Topic,
					DateAndTime=s.DateAndTime.ToString(DataConstants.DateFormat) ,
					Duration=s.Duration,
					Lecturer=s.Lecturer,
					Category=s.Category.Name,
					Organizer=s.Organizer.UserName,
					Details=s.Details

				})
				.FirstOrDefaultAsync();

			if (model == null)
			{
				return BadRequest();
			}

			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminar = await data.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new SeminarFormViewModel()
            {
                Topic = seminar.Topic,
                Lecturer = seminar.Lecturer,
                Details = seminar.Details,
                Duration = seminar.Duration,
                DateAndTime = seminar.DateAndTime.ToString(DataConstants.DateFormat),
                CategoryId = seminar.CategoryId
            };

            model.Categories = await GetCategories();

            return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
		{
			var seminar = await data.Seminars
				.FindAsync(id);

			if (seminar == null)
			{
				return BadRequest();
			}

			if (seminar.OrganizerId != GetUserId())
			{
				return Unauthorized();
			}

			DateTime start = DateTime.Now;

			if (!DateTime.TryParseExact(
				model.DateAndTime,
				DataConstants.DateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out start))
			{
				ModelState
					.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DataConstants.DateFormat}");
			}


			if (!ModelState.IsValid)
			{
				model.Categories = await GetCategories();

				return View(model);
			}

			seminar.Topic = model.Topic;
			seminar.Lecturer = model.Lecturer;
			seminar.Details = model.Details;
			seminar.DateAndTime = start;
            seminar.Duration = model.Duration;
			seminar.CategoryId = model.CategoryId;

			await data.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var seminar = data.Seminars
                .Find(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new SeminarDeleteViewModel()
            {
                Topic = seminar.Topic,
                Id = seminar.Id,
                DateAndTime = seminar.DateAndTime
            };

			return View(model);
        }

		[HttpPost]
		public IActionResult DeleteConfirmed(int id)
		{
            var seminar = data.Seminars
				.Include(s=>s.SeminarsParticipants)
				.FirstOrDefault(x=>x.Id==id);

            if (seminar == null)
            {
                return BadRequest();
            }

			string userId = GetUserId();
            if (seminar.OrganizerId != userId)
            {
                return Unauthorized();
            }

			if(seminar.SeminarsParticipants.Any())
			{
				 List<SeminarParticipant>seminarParticipants=data.SeminarsParticipants.Where(sp=>sp.SeminarId==seminar.Id).ToList();
				data.SeminarsParticipants.RemoveRange(seminarParticipants);
			}
			data.Seminars.Remove(seminar);
			data.SaveChanges();

			return RedirectToAction(nameof(All));
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}

		private async Task<IEnumerable<CategoryViewModel>> GetCategories()
		{
			return await data.Categories
				.AsNoTracking()
				.Select(c => new CategoryViewModel
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();
		}
	}
}
