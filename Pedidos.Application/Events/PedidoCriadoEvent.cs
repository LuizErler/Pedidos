using MediatR;
using Pedidos.Application.ReadModels;

namespace Pedidos.Application.Events
{
    public class PedidoCriadoEvent : INotification
    {
        public PedidoReadModel Pedido { get; }

        public PedidoCriadoEvent(PedidoReadModel pedido)
        {
            Pedido = pedido;
        }
    }
}