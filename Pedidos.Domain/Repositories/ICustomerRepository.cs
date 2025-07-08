using Pedidos.Domain.Entities.Customer;

namespace Pedidos.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
    }

}
