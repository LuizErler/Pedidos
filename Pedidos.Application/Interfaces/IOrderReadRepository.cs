using Pedidos.Application.Queries.DTOs;

namespace Pedidos.Application.Repositories;

public interface IOrderReadRepository
{
    Task<List<PedidoReadModel>> ListarPedidosAsync();
    Task<PedidoReadModel?> ObterPorIdAsync(Guid id);
    Task SalvarAsync(PedidoReadModel pedido);
}
