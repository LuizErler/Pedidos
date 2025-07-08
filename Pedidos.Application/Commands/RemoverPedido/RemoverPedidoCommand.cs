using MediatR;

namespace Pedidos.Application.Commands.RemoverPedido;

public class RemoverPedidoCommand : IRequest<Guid>
{
    public Guid PedidoId { get; set; }

    public RemoverPedidoCommand(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }
}
