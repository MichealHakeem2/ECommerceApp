using ECommerceApp.Core.Entities;
using ECommerceApp.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Cart?> GetUserCartAsync(int userId)
        {
            return await _cartRepository.GetUserCartAsync(userId);
        }

        public async Task AddItemToCart(int userId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetUserCartAsync(userId) ?? new Cart { UserId = userId };
            if (cart.CartItems == null)
                cart.CartItems = new System.Collections.Generic.List<CartItem>();

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item != null)
                item.Quantity += quantity;
            else
                cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });

            if (cart.CartId == 0)
                await _cartRepository.AddAsync(cart);
            else
                _cartRepository.Update(cart);
        }

        public async Task RemoveItemFromCart(int cartItemId)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            if (cartItem != null)
                _cartItemRepository.Delete(cartItem);
        }

        public async Task SaveChangesAsync()
        {
            await _cartRepository.SaveChangesAsync();
        }
    }
}
