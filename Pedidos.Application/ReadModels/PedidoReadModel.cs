using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pedidos.Application.ReadModels
{
    public class PedidoReadModel
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("customerId")]
        public Guid CustomerId { get; set; }

        [BsonElement("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [BsonElement("orderDate")]
        public DateTime OrderDate { get; set; }

        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = string.Empty;

        [BsonElement("itens")]
        public List<ItemPedidoReadModel> Itens { get; set; } = new List<ItemPedidoReadModel>();
    }

    public class ItemPedidoReadModel
    {
        [BsonElement("productId")]
        public Guid ProductId { get; set; }

        [BsonElement("productName")]
        public string ProductName { get; set; } = string.Empty;

        [BsonElement("unitPrice")]
        public decimal UnitPrice { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
