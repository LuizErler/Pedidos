using MediatR;
using Pedidos.Application.Events;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.EventHandlers;

public class PedidoCriadoEventHandler : INotificationHandler<PedidoCriadoEvent>
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoCriadoEventHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task Handle(PedidoCriadoEvent notification, CancellationToken cancellationToken)
    {
        await _pedidoRepository.AdicionarAsync(notification.Pedido);
    }
}
