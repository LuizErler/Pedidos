using MediatR;
using Pedidos.Application.ReadModels;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Queries.ListarPedidos;

public class ListarPedidosQueryHandler : IRequestHandler<ListarPedidosQuery, List<PedidoReadModel>>
{
    private readonly IPedidoRepository _readRepo;

    public ListarPedidosQueryHandler(IPedidoRepository readRepo)
    {
        _readRepo = readRepo;
    }

    public async Task<List<PedidoReadModel>> Handle(ListarPedidosQuery request, CancellationToken cancellationToken)
    {
        return await _readRepo.ObterTodosAsync();
    }
}
