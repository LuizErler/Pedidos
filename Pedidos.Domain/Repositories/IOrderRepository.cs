using Pedidos.Domain.Entities.Order;

namespace Pedidos.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);

    }

}
