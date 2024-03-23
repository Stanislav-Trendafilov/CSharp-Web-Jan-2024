using HouseRentingSystem.Core.Services;
using HouseRentingSystem.Models;
using HouseRentingSystem.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService _houseService)
        {
            this.houseService = _houseService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var models = await houseService.LastThreeHousesAsync();

            return View(models);
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}