using Microsoft.AspNetCore.Mvc;
using MediatR;
using Pedidos.Application.Commands.CriarPedido;
using Pedidos.Application.Commands.RemoverPedido;
using Pedidos.Application.Commands.AtualizarStatusPedido;
using Pedidos.Domain.Enuns;
using Pedidos.Application.Queries.ListarPedidos;
using Pedidos.Application.Queries.ObterPedidoPorId;

namespace Pedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PedidosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoCommand command)
    {
        var pedidoId = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObterPedidoPorId), new { id = pedidoId }, new { id = pedidoId });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverPedido(Guid id)
    {
        var command = new RemoverPedidoCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] OrderStatus novoStatus)
    {
        var command = new AtualizarStatusPedidoCommand(id, novoStatus);
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpGet]
    public async Task<IActionResult> ListarPedidos()
    {
        var pedidos = await _mediator.Send(new ListarPedidosQuery());
        return Ok(pedidos);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPedidoPorId(Guid id)
    {
        var pedido = await _mediator.Send(new ObterPedidoPorIdQuery(id));
        if (pedido is null)
            return NotFound();

        return Ok(pedido);
    }

}
