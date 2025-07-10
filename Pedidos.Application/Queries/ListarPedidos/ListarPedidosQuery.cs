using MediatR;
using Pedidos.Application.Queries.DTOs;

namespace Pedidos.Application.Queries.ListarPedidos;

public record ListarPedidosQuery() : IRequest<List<PedidoReadModel>>;
