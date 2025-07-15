using MediatR;
using Pedidos.Application.Exceptions;
using Pedidos.Application.ReadModels;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Commands.RemoverPedido;

public class RemoverPedidoCommandHandler : IRequestHandler<RemoverPedidoCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public RemoverPedidoCommandHandler(IOrderRepository orderRepository, IPedidoRepository pedidoRepository)
    {
        _orderRepository = orderRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Unit> Handle(RemoverPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _orderRepository.GetByIdAsync(request.PedidoId);

        if (pedido is null)
            throw new NotFoundException($"Pedido com ID {request.PedidoId} não encontrado.");

        await _orderRepository.DeleteAsync(pedido.Id);

        await _pedidoRepository.RemoverAsync(pedido.Id);


        return Unit.Value;
    }
}
