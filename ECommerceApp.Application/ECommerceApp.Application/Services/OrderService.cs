using ECommerceApp.Core.Entities;
using ECommerceApp.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CartService _cartService;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, CartService cartService, IRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Order> CreateOrderFromCartAsync(int userId)
        {
            var cart = await _cartService.GetUserCartAsync(userId);
            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                throw new System.Exception("Cart is empty");

            var order = new Order
            {
                UserId = userId,
                OrderDate = System.DateTime.Now,
                Status = "pending",
                TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<decimal> CalculateTotalAsync(Order order)
        {
            return order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
        }

        public async Task<bool> CanPlaceOrderAsync(Order order, ProductService productService)
        {
            foreach (var item in order.OrderItems)
            {
                if (!await productService.IsProductInStock(item.ProductId, item.Quantity))
                    return false;
            }
            return true;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _orderRepository.GetOrdersByUserAsync(userId);
        }

        public async Task<Order?> GetOrderWithItemsAsync(int orderId)
        {
            return await _orderRepository.GetOrderWithItemsAsync(orderId);
        }
    }
}
