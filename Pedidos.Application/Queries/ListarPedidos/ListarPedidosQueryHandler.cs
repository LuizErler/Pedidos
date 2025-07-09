using MediatR;
using Pedidos.Application.Queries.DTOs;
using Pedidos.Application.Repositories;

namespace Pedidos.Application.Queries.ListarPedidos;

public class ListarPedidosQueryHandler : IRequestHandler<ListarPedidosQuery, List<PedidoReadModel>>
{
    private readonly IOrderReadRepository _readRepo;

    public ListarPedidosQueryHandler(IOrderReadRepository readRepo)
    {
        _readRepo = readRepo;
    }

    public async Task<List<PedidoReadModel>> Handle(ListarPedidosQuery request, CancellationToken cancellationToken)
    {
        return await _readRepo.ListarPedidosAsync();
    }
}
