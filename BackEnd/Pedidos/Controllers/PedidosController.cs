using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Commands.AtualizarStatusPedido;
using Pedidos.Application.Commands.CriarPedido;
using Pedidos.Application.Commands.RemoverPedido;
using Pedidos.Application.Queries.ListarPedidos;
using Pedidos.Application.Queries.ObterPedidoPorId;
using Pedidos.Domain.Enuns;
using Pedidos.Domain.Repositories;


namespace Pedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPedidoRepository _pedidoRepository;

    public PedidosController(IMediator mediator, IPedidoRepository pedidoRepository)
    {
        _mediator = mediator;
        _pedidoRepository = pedidoRepository;
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
            return Ok(await _mediator.Send(new ListarPedidosQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPedidoPorId(Guid id)
    {
            var mongoPedido = await _mediator.Send(new ObterPedidoPorIdQuery(id));
            return mongoPedido is null ? NotFound() : Ok(mongoPedido);
    }

}
