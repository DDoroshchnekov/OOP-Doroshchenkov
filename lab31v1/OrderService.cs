using System;

namespace lab31v1
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;

        public OrderService(IOrderRepository orderRepository, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
        }

        public void ProcessOrder(int orderId)
        {
            var order = _orderRepository.GetById(orderId);

            if (order == null)
                throw new ArgumentException("Order not found.");

            if (order.IsProcessed)
                throw new InvalidOperationException("Order is already processed.");

            if (order.TotalAmount <= 0)
                throw new InvalidOperationException("Order total amount must be greater than zero.");

            order.IsProcessed = true;
            _orderRepository.Save(order);

            if (!string.IsNullOrWhiteSpace(order.CustomerEmail))
            {
                _emailService.SendEmail(
                    order.CustomerEmail,
                    "Order processed",
                    $"Your order #{order.Id} has been processed successfully."
                );
            }
        }

        public void DeleteOrder(int orderId)
        {
            var order = _orderRepository.GetById(orderId);

            if (order == null)
                throw new ArgumentException("Order not found.");

            _orderRepository.Delete(orderId);
        }

        public Order? GetOrder(int orderId)
        {
            return _orderRepository.GetById(orderId);
        }
    }
}