using MediatR;
using Pedidos.Application.ReadModels;

namespace Pedidos.Application.Queries.ObterPedidoPorId;

public class ObterPedidoPorIdQuery : IRequest<PedidoReadModel?>
{
    public Guid PedidoId { get; }

    public ObterPedidoPorIdQuery(Guid pedidoId)
    {
        PedidoId = pedidoId;
    }
}
