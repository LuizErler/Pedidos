using MediatR;
using Pedidos.Application.Exceptions;
using Pedidos.Application.ReadModels;
using Pedidos.Domain.Entities.Order;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Commands.AtualizarStatusPedido;

public class AtualizarStatusPedidoCommandHandler : IRequestHandler<AtualizarStatusPedidoCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public AtualizarStatusPedidoCommandHandler(IOrderRepository orderRepository, IPedidoRepository pedidoRepository)
    {
        _orderRepository = orderRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Unit> Handle(AtualizarStatusPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _orderRepository.GetByIdAsync(request.PedidoId);

        if (pedido is null)
            throw new NotFoundException($"Pedido com ID {request.PedidoId} não encontrado.");


        pedido.AtualizarStatus(request.NovoStatus);

        await _orderRepository.UpdateAsync(pedido);

        var readModel = PedidoReadModel.FromDomain(pedido);
        await _pedidoRepository.AtualizarAsync(readModel);

        return Unit.Value;
    }

}
