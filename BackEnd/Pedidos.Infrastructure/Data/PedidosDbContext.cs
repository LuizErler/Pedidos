using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities.Customer;
using Pedidos.Domain.Entities.Order;
using Pedidos.Domain.Entities.Product;

namespace Pedidos.Infrastructure.Data;

public class PedidosDbContext : DbContext
{
    public PedidosDbContext(DbContextOptions<PedidosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosDbContext).Assembly);
    }
}
