using MediatR;

namespace Pedidos.Application.Commands.RemoverPedido;

public class RemoverPedidoCommand : IRequest<Unit>
{
    public Guid PedidoId { get; }

    public RemoverPedidoCommand(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }
}
