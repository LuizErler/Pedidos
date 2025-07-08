using Pedidos.Domain.Enuns;

namespace Pedidos.Domain.Entities.Order
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items;

        private Order() { }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            var item = new OrderItem(productId, productName, quantity, unitPrice);
            _items.Add(item);
            TotalAmount += item.TotalPrice;
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }


}
