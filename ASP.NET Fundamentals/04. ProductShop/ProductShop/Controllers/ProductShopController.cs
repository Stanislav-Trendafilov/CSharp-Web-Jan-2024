using Microsoft.AspNetCore.Mvc;
using ProductShop.Data.Configuration;
using ProductShop.Data.Models;
using ProductShop.Models;

namespace ProductShop.Controllers
{
    public class ProductShopController : Controller
    {
        private readonly ProductShopDbContext context;
        public ProductShopController(ProductShopDbContext context)
                => this.context = context;
        public IActionResult All()
        {
            var products = context
                .Products
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name

                })
                .ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new ProductViewModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(ProductViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var entity = new Product()
            {
                Name = model.Name
            };

            context.Products.Add(entity);
            context.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        { 
            var entity = context.Products.Find(id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid product");
            }

            var model= new ProductViewModel()
            {
                Id = id,
                Name = entity.Name
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            var entity = context.Products.Find(model.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid product");
            }

            entity.Name = model.Name;
            context.SaveChanges();

            return RedirectToAction(nameof(All));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {

            var entity = context.Products.Find(id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid product");
            }

            context.Products.Remove(entity);
            context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

    }
}
