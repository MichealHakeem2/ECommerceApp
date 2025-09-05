using System;
using System.Collections.Generic;

namespace ECommerceApp.Core.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "pending";
        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}
