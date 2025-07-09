using MediatR;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Commands.AtualizarStatusPedido;

public class AtualizarStatusPedidoCommandHandler : IRequestHandler<AtualizarStatusPedidoCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public AtualizarStatusPedidoCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(AtualizarStatusPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _orderRepository.GetByIdAsync(request.PedidoId);

        if (pedido is null)
            throw new Exception("Pedido não encontrado.");


        pedido.AtualizarStatus(request.NovoStatus);

        await _orderRepository.UpdateAsync(pedido);

        return Unit.Value;
    }
}
