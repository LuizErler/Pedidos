using MediatR;
using Pedidos.Application.Queries.DTOs;

namespace Pedidos.Application.Queries.ObterPedidoPorId;

public class ObterPedidoPorIdQuery : IRequest<PedidoReadModel?>
{
    public Guid PedidoId { get; }

    public ObterPedidoPorIdQuery(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }
}
