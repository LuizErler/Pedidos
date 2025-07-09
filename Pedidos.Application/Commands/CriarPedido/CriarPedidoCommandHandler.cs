using MediatR;
using Pedidos.Application.Repositories;
using Pedidos.Domain.Entities.Order;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Commands.CriarPedido;

public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderReadRepository _orderReadRepository;

    public CriarPedidoCommandHandler(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository,
    IOrderReadRepository orderReadRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderReadRepository = orderReadRepository;
    }

    public async Task<Guid> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId)
                      ?? throw new Exception("Cliente não encontrado");

        var order = new Order(customer.Id);

        foreach (var item in request.Itens)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId)
                         ?? throw new Exception($"Produto {item.ProductId} não encontrado");

            order.AddItem(product.Id, product.Name, item.Quantity, product.Price);
        }

        await _orderRepository.AddAsync(order);

        return order.Id;
    }
    
}
