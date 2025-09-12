using ECommerceApp.Core.Entities;

namespace ECommerceApp.Core.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId);
    }
}
