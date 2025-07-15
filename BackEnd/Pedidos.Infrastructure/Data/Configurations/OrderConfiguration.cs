using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Entities.Order;

namespace Pedidos.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey("OrderId");

        builder.Property(x => x.Status)
               .HasConversion<int>();
    }
}
