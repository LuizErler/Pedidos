using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pedidos.Application.Commands.CriarPedido;
using Pedidos.Domain.Repositories;
using Pedidos.Infra.Mongo;
using Pedidos.Infra.Mongo.Repositories;
using Pedidos.Infrastructure.Data;
using Pedidos.Infrastructure.Data.Repositories;
using Pedidos.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});



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

builder.Services.AddScoped<IPedidoRepository>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return new PedidoRepository(client, settings.DatabaseName);
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
