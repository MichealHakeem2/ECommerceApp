using ECommerceApp.Core.Entities;
using ECommerceApp.Core.Interfaces;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApp.Infrastructure.Repositories
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<CartItem?> GetByIdAsync(int cartItemId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
        }
    }
}
