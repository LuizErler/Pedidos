using MediatR;
using Pedidos.Application.DTOs;

namespace Pedidos.Application.Commands.CriarPedido;

public class CriarPedidoCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public List<PedidoItemDto> Itens { get; set; } = new();
}
