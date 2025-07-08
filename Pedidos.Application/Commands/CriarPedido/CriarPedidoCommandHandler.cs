using MediatR;
using Pedidos.Domain.Entities.Order;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Commands.CriarPedido;

public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, Guid>
{
    private readonly IOrderRepository _orderRepo;
    private readonly ICustomerRepository _customerRepo;
    private readonly IProductRepository _productRepo;

    public CriarPedidoCommandHandler(
        IOrderRepository orderRepo,
        ICustomerRepository customerRepo,
        IProductRepository productRepo)
    {
        _orderRepo = orderRepo;
        _customerRepo = customerRepo;
        _productRepo = productRepo;
    }

    public async Task<Guid> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepo.GetByIdAsync(request.CustomerId)
                      ?? throw new Exception("Cliente não encontrado");

        var order = new Order(customer.Id);

        foreach (var item in request.Itens)
        {
            var product = await _productRepo.GetByIdAsync(item.ProductId)
                         ?? throw new Exception($"Produto {item.ProductId} não encontrado");

            order.AddItem(product.Id, product.Name, item.Quantity, product.Price);
        }

        await _orderRepo.AddAsync(order);

        return order.Id;
    }
}
