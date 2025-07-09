using MediatR;
using Microsoft.EntityFrameworkCore;
using Pedidos.Application.Commands.CriarPedido;
using Pedidos.Application.Repositories;
using Pedidos.Domain.Repositories;
using Pedidos.Infrastructure.Data;
using Pedidos.Infrastructure.Repositories;
using Pedidos.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ?? EF Core
builder.Services.AddDbContext<PedidosDbContext>(options =>
    options.UseSqlServer(connectionString));

// ?? MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CriarPedidoCommand).Assembly));

// ?? Repositórios
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CriarPedidoCommand).Assembly));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PedidosDbContext>();
    DatabaseSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
