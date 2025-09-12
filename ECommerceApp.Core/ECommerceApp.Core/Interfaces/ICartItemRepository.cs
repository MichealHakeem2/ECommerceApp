using ECommerceApp.Core.Entities;
using System.Threading.Tasks;

namespace ECommerceApp.Core.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<CartItem?> GetByIdAsync(int cartItemId);
    }
}
