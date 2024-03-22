using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCIntroDemo.Models;
using System.Text;
using System.Text.Json;

namespace MVCIntroDemo.Controllers
{
	public class ProductController : Controller
	{
		private IEnumerable<ProductViewModel> products
			= new List<ProductViewModel>()
			{
				new ProductViewModel()
				{
					Id= 1,
					Name="Cheese",
					Price=6.50
				},
				new ProductViewModel()
				{
					Id= 2,
					Name="Ham",
					Price=8.50,
				}, new ProductViewModel()
				{
					Id= 3,
					Name="Bread",
					Price=1.50,
				}
			};
		[ActionName("My-Products")]
		public IActionResult Index(string keyword)
		{
			if (keyword != null)
			{
				var foundProducts = products.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));

				return View(foundProducts);
			}
			return View(products);
		}
		public IActionResult ReturnToJson()
		{
			var options = new JsonSerializerOptions()
			{
				WriteIndented = true,
			};
			return Json(products, new JsonSerializerOptions() { WriteIndented = true, });
		}

		public IActionResult AllAsPlainText()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var item in products)
			{
				sb.AppendLine($"Product {item.Id}: {item.Name} - {item.Price}");
			}

			return Content(sb.ToString());
		}
		public IActionResult AllAsTextFile()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in products)
			{
				sb.AppendLine($"Product {item.Id}: {item.Name} - {item.Price}");

			}
			Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");
			return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
		}
	}
}
