using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Entities.Product;
using Pedidos.Domain.Repositories;
using Pedidos.Infrastructure.Data;

namespace Pedidos.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PedidosDbContext _context;

    public ProductRepository(PedidosDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}
