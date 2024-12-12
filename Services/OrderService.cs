using DataAccess;
using ComputerECommerce.Models;

namespace Services
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository;

        public OrderService(OrderRepository OrderRepository)
        {
            this.orderRepository = OrderRepository;
        }

        public void AddOrder(int userId, DateTime orderDate, decimal price, string status, string addressLine, string city, string postalCode, string country)
        {
            orderRepository.InsertOrder(userId, orderDate, price, status, addressLine, city, postalCode, country);
        }

        public void AddOrderItem(int orderId, int productId, int quantity, decimal price)
        {
            orderRepository.InsertOrderItem(orderId, productId, quantity, price);
        }

        public void UpdateOrderDate(int orderId, DateTime orderDate)
        {
            orderRepository.EditOrderDate(orderId, orderDate);
        }

        public void UpdateOrderPrice(int orderId, decimal price)
        {
            orderRepository.EditOrderPrice(orderId, price);
        }

        public void UpdateOrderStatus(int orderId, OrderStatus status)
        {
            orderRepository.EditOrderStatus(orderId, status);
        }

        public void UpdateOrderAddressLine(int orderId, string addressLine)
        {
            orderRepository.EditOrderAddressLine(orderId, addressLine);
        }

        public void UpdateOrderCity(int orderId, string city)
        {
            orderRepository.EditOrderCity(orderId, city);
        }

        public void UpdateOrderPostalCode(int orderId, string postalCode)
        {
            orderRepository.EditOrderPostalCode(orderId, postalCode);
        }

        public void UpdateOrderCountry(int orderId, string country)
        {
            orderRepository.EditOrderCountry(orderId, country);
        }

        public void RemoveOrder(int id)
        {
            orderRepository.DeleteOrder(id);
        }

        public List<Order> GetUserOrders(int userId)
        {
            return orderRepository.GetUserOrders(userId);
        }

        public List<OrderItem> GetUserOrderItems(int orderId)
        {
            return orderRepository.GetUserOrderItems(orderId);
        }
    }
}