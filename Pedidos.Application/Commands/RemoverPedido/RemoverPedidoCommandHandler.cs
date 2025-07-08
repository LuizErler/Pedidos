using MediatR;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Commands.RemoverPedido;

public class RemoverPedidoCommandHandler : IRequestHandler<RemoverPedidoCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoverPedidoCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(RemoverPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _orderRepository.GetByIdAsync(request.PedidoId);

        if (pedido is null)
            throw new Exception("Pedido não encontrado.");

        // Caso tenha lógica de negócio (ex: não pode remover finalizado), valida aqui

        await _orderRepository.DeleteAsync(pedido.Id);
        return Unit.Value;
    }
}
