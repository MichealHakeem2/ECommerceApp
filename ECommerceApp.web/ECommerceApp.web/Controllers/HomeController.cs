using ECommerceApp.Application.Services;
using ECommerceApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ECommerceApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public HomeController(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Index(int? categoryId)
        {
            var products = categoryId.HasValue
                ? await _productService.GetProductsByCategoryAsync(categoryId.Value)
                : await _productService.GetAllProductsAsync();

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
