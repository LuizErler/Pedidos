using MongoDB.Driver;
using Pedidos.Application.ReadModels;
using Pedidos.Domain.Repositories;

namespace Pedidos.Infra.Mongo.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<PedidoReadModel> _collection;

        public PedidoRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<PedidoReadModel>("Pedidos");
        }

        public async Task<List<PedidoReadModel>> ObterTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<PedidoReadModel?> ObterPorIdAsync(Guid id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AdicionarAsync(PedidoReadModel pedido)
        {
            await _collection.InsertOneAsync(pedido);
        }
    }
}
