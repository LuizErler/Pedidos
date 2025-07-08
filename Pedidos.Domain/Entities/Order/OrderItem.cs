namespace Pedidos.Domain.Entities.Order
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }

        private OrderItem() { }

        public OrderItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = unitPrice * quantity;
        }
    }


}
