using MediatR;
using Pedidos.Application.Queries.DTOs;
using Pedidos.Application.Repositories;

namespace Pedidos.Application.Queries.ObterPedidoPorId;

public class ObterPedidoPorIdQueryHandler : IRequestHandler<ObterPedidoPorIdQuery, PedidoReadModel?>
{
    private readonly IOrderReadRepository _readRepo;

    public ObterPedidoPorIdQueryHandler(IOrderReadRepository readRepo)
    {
        _readRepo = readRepo;
    }

    public async Task<PedidoReadModel?> Handle(ObterPedidoPorIdQuery request, CancellationToken cancellationToken)
    {
        return await _readRepo.ObterPorIdAsync(request.PedidoId);
    }
}
