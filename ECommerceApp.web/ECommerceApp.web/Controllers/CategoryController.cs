using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Subcategories(int parentId)
        {
            var subcategories = await _categoryService.GetSubcategoriesAsync(parentId);
            return View(subcategories);
        }
    }
}
