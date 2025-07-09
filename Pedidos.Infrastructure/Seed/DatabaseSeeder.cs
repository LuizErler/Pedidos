using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities.Customer;
using Pedidos.Domain.Entities.Product;
using Pedidos.Infrastructure.Data;

namespace Pedidos.Infrastructure.Seed;

public static class DatabaseSeeder
{
    public static void Seed(PedidosDbContext context)
    {
        context.Database.Migrate(); // aplica migrações se tiver

        if (!context.Customers.Any())
        {
            var cliente = new Customer("Cliente Padrão", "cliente@email.com", "11999999999");
            context.Customers.Add(cliente);
        }

        if (!context.Products.Any())
        {
            var produtos = new List<Product>
            {
                new Product("Notebook Dell", 4500),
                new Product("Mouse Logitech", 150),
                new Product("Teclado Mecânico", 300)
            };

            context.Products.AddRange(produtos);
        }

        context.SaveChanges();
    }
}
