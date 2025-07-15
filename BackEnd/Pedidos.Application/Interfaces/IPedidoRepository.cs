using Pedidos.Application.ReadModels;

namespace Pedidos.Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task<List<PedidoReadModel>> ObterTodosAsync();
        Task<PedidoReadModel?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(PedidoReadModel pedido);
        Task AtualizarAsync(PedidoReadModel pedido);
        Task RemoverAsync(Guid id);

    }
}
