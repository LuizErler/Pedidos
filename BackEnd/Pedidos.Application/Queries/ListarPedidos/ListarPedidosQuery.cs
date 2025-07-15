using MediatR;
using Pedidos.Application.ReadModels;

namespace Pedidos.Application.Queries.ListarPedidos;

public record ListarPedidosQuery() : IRequest<List<PedidoReadModel>>;
