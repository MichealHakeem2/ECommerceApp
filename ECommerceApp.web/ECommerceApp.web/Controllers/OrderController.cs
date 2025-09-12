using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public OrderController(OrderService orderService, ProductService productService, CartService cartService)
        {
            _orderService = orderService;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "User");

            var orders = await _orderService.GetOrdersByUserAsync(userId.Value);
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "User");

            var order = await _orderService.CreateOrderFromCartAsync(userId.Value);
            return RedirectToAction("Details", new { id = order.OrderId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderWithItemsAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }
    }
}
