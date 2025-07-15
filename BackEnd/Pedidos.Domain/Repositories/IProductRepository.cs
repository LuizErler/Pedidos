using Pedidos.Domain.Entities.Product;

namespace Pedidos.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
    }

}
