using ECommerceApp.Core.Entities;
using ECommerceApp.Core.Interfaces;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class PaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> CreatePaymentAsync(Order order, string method)
        {
            var payment = new Payment
            {
                OrderId = order.OrderId,
                Amount = order.TotalAmount,
                Method = method,
                Status = "completed",
                TransactionDate = System.DateTime.Now
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return payment;
        }

        public async Task<bool> IsPaymentValidAsync(Payment payment, Order order)
        {
            return payment.Amount == order.TotalAmount;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId)
        {
            return await _paymentRepository.GetPaymentsByOrderAsync(orderId);
        }
    }
}
