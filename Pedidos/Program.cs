using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Pedidos.Application.Commands.CriarPedido;
using Pedidos.Domain.Repositories;
using Pedidos.Infra.Mongo.Repositories;
using Pedidos.Infrastructure.Data;
using Pedidos.Infrastructure.Data.Repositories;
using Pedidos.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Configurar MongoSettings do appsettings.json
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

// Criar e configurar MongoClient com GuidRepresentation padrão
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Registrar PedidoRepository usando IPedidoRepository
builder.Services.AddScoped<IPedidoRepository>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return new PedidoRepository(client, settings.DatabaseName);
});

// Configurar EF Core com SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PedidosDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar MediatR para handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CriarPedidoCommand).Assembly));

// Outros repositórios da camada de persistência
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Popular banco de dados SQL na inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PedidosDbContext>();
    DatabaseSeeder.Seed(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
