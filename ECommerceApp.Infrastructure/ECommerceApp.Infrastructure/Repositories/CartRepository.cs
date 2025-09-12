using ECommerceApp.Core.Entities;
using ECommerceApp.Core.Interfaces;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApp.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart?> GetUserCartAsync(int userId)
        {
            return await _context.Carts
                                 .Include(c => c.CartItems)
                                 .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
