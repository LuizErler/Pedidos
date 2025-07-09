using Pedidos.Domain.Entities;
using Pedidos.Domain.Repositories;
using Pedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities.Order;

namespace Pedidos.Infrastructure.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly PedidosDbContext _context;

    public OrderRepository(PedidosDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var pedido = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (pedido is not null)
        {
            _context.Orders.Remove(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
