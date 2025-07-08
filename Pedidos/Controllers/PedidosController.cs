using Microsoft.AspNetCore.Mvc;
using MediatR;
using Pedidos.Application.Commands.CriarPedido;
using Pedidos.Application.Commands.RemoverPedido;

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
        return CreatedAtAction(nameof(ObterPorId), new { id = pedidoId }, new { id = pedidoId });
    }

    // Placeholder para o GetById – vamos montar depois com MongoDB (Query Side)
    [HttpGet("{id}")]
    public IActionResult ObterPorId(Guid id)
    {
        return Ok($"Consulta de pedido {id} virá do MongoDB futuramente.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverPedido(Guid id)
    {
        var command = new RemoverPedidoCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

}
