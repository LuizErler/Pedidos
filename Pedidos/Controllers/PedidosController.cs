using Microsoft.AspNetCore.Mvc;
using MediatR;
using Pedidos.Application.Commands.CriarPedido;
using Pedidos.Application.Commands.RemoverPedido;
using Pedidos.Application.Commands.AtualizarStatusPedido;
using Pedidos.Application.Queries.ListarPedidos;
using Pedidos.Application.Queries.ObterPedidoPorId;
using Pedidos.Domain.Enuns;
using Pedidos.Domain.Repositories;
using Pedidos.Application.ReadModels;


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
    public async Task<IActionResult> ListarPedidos([FromQuery] string? fonte = null)
    {
        if (fonte?.ToLower() == "mongo")
            return Ok(await _mediator.Send(new ListarPedidosQuery()));

        return Ok(await _mediator.Send(new ListarPedidosQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPedidoPorId(Guid id, [FromQuery] string? fonte = null)
    {
        if (fonte?.ToLower() == "mongo")
        {
            var mongoPedido = await _mediator.Send(new ObterPedidoPorIdQuery(id));
            return mongoPedido is null ? NotFound() : Ok(mongoPedido);
        }

        var pedido = await _mediator.Send(new ObterPedidoPorIdQuery(id));
        return pedido is null ? NotFound() : Ok(pedido);
    }

    // 🚀 Endpoint temporário para popular o MongoDB
    [HttpPost("seed-mongo")]
    public async Task<IActionResult> SeedMongo()
    {
        var pedido = new PedidoReadModel
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.Parse("8B75340F-99D4-428E-B0D9-CA96C6AB9220"),
            CustomerName = "Cliente Teste Mongo",
            OrderDate = DateTime.UtcNow,
            TotalAmount = 450,
            Status = "Criado",
            Itens = new List<ItemPedidoReadModel>
            {
                new()
                {
                    ProductId = Guid.Parse("0D4E725B-5E22-4F52-91E1-6078F0E9FD3A"),
                    ProductName = "Produto A",
                    UnitPrice = 150,
                    Quantity = 2,
                    TotalPrice = 300
                },
                new()
                {
                    ProductId = Guid.Parse("28E4CEFA-C0F6-4D99-96C8-7209F1632694"),
                    ProductName = "Produto B",
                    UnitPrice = 75,
                    Quantity = 2,
                    TotalPrice = 150
                }
            }
        };

        await _pedidoRepository.AdicionarAsync(pedido);
        return Ok("Pedido inserido no MongoDB com sucesso!");
    }

}
