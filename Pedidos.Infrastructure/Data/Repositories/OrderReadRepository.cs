using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Pedidos.Application.Queries.DTOs;
using Pedidos.Application.Repositories;

namespace Pedidos.Infrastructure.Data.Repositories;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly IMongoCollection<PedidoReadModel> _collection;

    public OrderReadRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
        var database = client.GetDatabase("PedidosReadDb");
        _collection = database.GetCollection<PedidoReadModel>("Pedidos");
    }

    public async Task<List<PedidoReadModel>> ListarPedidosAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<PedidoReadModel?> ObterPorIdAsync(Guid id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task SalvarAsync(PedidoReadModel pedido)
    {
        await _collection.InsertOneAsync(pedido);
    }
}
