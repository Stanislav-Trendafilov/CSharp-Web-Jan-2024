using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Services;
using HouseRentingSystem.Models.Houses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;

        private readonly IAgentService agentService;
        public HouseController(IHouseService _houseService, IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;

        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View(new AllHousesQueryModel());
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            return View(new AllHousesQueryModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpGet]
        [MustBeAgent]
        public async Task<IActionResult> Add()
        {
            var model = new HouseFormModel()
            {
                Categories = await houseService.AllCategoriesAsync()
            };

            return View(model);
        }
        [HttpPost]
        [MustBeAgent]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if(await houseService.CategoryExistsAsync(model.CategoryId)==false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "");
            
            }
            if(ModelState.IsValid==false)
            {
                model.Categories=await houseService.AllCategoriesAsync();

                return View(model);
            }

            int? agentId = await agentService.GetAgentIdAsync(User.Id());

            int newHouseId = await houseService.CreateAsync(model, agentId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newHouseId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(new HouseFormModel());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel house)
        {
            return RedirectToAction(nameof(Details), new { id = "1" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(new HouseFormModel());
        }
        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel house)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
