using MediatR;
using Pedidos.Application.ReadModels;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Queries.ObterPedidoPorId;

public class ObterPedidoPorIdQueryHandler : IRequestHandler<ObterPedidoPorIdQuery, PedidoReadModel?>
{
    private readonly IPedidoRepository _readRepo;

    public ObterPedidoPorIdQueryHandler(IPedidoRepository readRepo)
    {
        _readRepo = readRepo;
    }

    public async Task<PedidoReadModel?> Handle(ObterPedidoPorIdQuery request, CancellationToken cancellationToken)
    {
        return await _readRepo.ObterPorIdAsync(request.PedidoId);
    }
}
