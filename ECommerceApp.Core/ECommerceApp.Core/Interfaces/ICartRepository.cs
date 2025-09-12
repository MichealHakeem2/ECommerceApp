using ECommerceApp.Core.Entities;

namespace ECommerceApp.Core.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetUserCartAsync(int userId);
    }
}
