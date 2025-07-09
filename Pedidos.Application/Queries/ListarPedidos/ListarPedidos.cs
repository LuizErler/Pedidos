using MediatR;
using Pedidos.Application.Queries.DTOs;

namespace Pedidos.Application.Queries.ListarPedidos;

public class ListarPedidosQuery : IRequest<List<PedidoReadModel>> { }
