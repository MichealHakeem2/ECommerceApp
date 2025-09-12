using ECommerceApp.Core.Entities;

namespace ECommerceApp.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
        Task<Order?> GetOrderWithItemsAsync(int orderId);
    }
}
