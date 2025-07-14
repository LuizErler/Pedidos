using MediatR;
using Pedidos.Application.Events;
using Pedidos.Application.Exceptions;
using Pedidos.Application.ReadModels;
using Pedidos.Domain.Entities.Order;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Commands.CriarPedido;

public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;


    public CriarPedidoCommandHandler(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository,
    IMediator mediator)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId)
                      ?? throw new NotFoundException("Cliente não encontrado");

        var order = new Order(customer.Id);

        foreach (var item in request.Itens)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId)
                         ?? throw new NotFoundException($"Produto {item.ProductId} não encontrado");

            order.AddItem(product.Id, product.Name, item.Quantity, product.Price);
        }

        await _orderRepository.AddAsync(order);

        await DisparaEvent(customer, order, cancellationToken);

        return order.Id;
    }

    private async Task DisparaEvent(Domain.Entities.Customer.Customer customer, Order order, CancellationToken cancellationToken)
    {
        var readModel = new PedidoReadModel
        {
            Id = order.Id,
            CustomerId = customer.Id,
            CustomerName = customer.Name,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status.ToString(),
            Itens = order.Items.Select(item => new ItemPedidoReadModel
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice
            }).ToList()
        };

        await _mediator.Publish(new PedidoCriadoEvent(readModel), cancellationToken);
    }
}
