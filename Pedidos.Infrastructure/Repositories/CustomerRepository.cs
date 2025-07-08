using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities.Customer;
using Pedidos.Domain.Repositories;
using Pedidos.Infrastructure.Data;

namespace Pedidos.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly PedidosDbContext _context;

    public CustomerRepository(PedidosDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
    }
}
