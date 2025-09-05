using System.Collections.Generic;

namespace ECommerceApp.Core.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal GrandTotal => Items.Sum(x => x.Total);
    }
}
