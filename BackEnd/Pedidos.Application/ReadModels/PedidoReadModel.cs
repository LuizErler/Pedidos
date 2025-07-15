using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Pedidos.Domain.Entities.Order;

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
        public List<ItemPedidoReadModel> Itens { get; set; } = new();

        public static PedidoReadModel FromDomain(Order pedido)
        {
            return new PedidoReadModel
            {
                Id = pedido.Id,
                CustomerId = pedido.CustomerId,
                CustomerName = "Cliente Padrão",
                OrderDate = pedido.OrderDate,
                TotalAmount = pedido.TotalAmount,
                Status = pedido.Status.ToString(),
                Itens = pedido.Items.Select(item => new ItemPedidoReadModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                }).ToList()
            };
        }
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
