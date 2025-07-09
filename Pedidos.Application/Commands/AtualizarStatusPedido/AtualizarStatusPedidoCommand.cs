using MediatR;
using Pedidos.Domain.Enuns;

namespace Pedidos.Application.Commands.AtualizarStatusPedido;

public class AtualizarStatusPedidoCommand : IRequest<Unit>
{
    public Guid PedidoId { get; set; }
    public OrderStatus NovoStatus { get; set; }

    public AtualizarStatusPedidoCommand(Guid pedidoId, OrderStatus novoStatus)
    {
        PedidoId = pedidoId;
        NovoStatus = novoStatus;
    }
}
