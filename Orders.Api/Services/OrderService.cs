using Orders.Api.Models;
using Orders.Api.Repositories;

namespace Orders.Api.Services;

public class OrderService(IOrderRepository repository) : IOrderService
{
    public IReadOnlyList<Order> GetAll() => repository.FindAll();

    public Order GetById(long id) => repository.FindById(id) ?? throw new OrderNotFoundException(id);

    public Order Create(string description) =>
        repository.Save(new Order { Description = description, Status = OrderStatus.Ordered });

    public Order Ship(long id) => Transition(id, "ship", OrderStatus.Ordered, OrderStatus.Shipped);

    public Order Deliver(long id) => Transition(id, "deliver", OrderStatus.Shipped, OrderStatus.Delivered);

    public Order Cancel(long id) => Transition(id, "cancel", OrderStatus.Ordered, OrderStatus.Cancelled);

    private Order Transition(long id, string action, OrderStatus from, OrderStatus to)
    {
        var order = GetById(id);
        if (order.Status != from)
        {
            throw new InvalidOrderStatusException(
                $"Cannot {action} an order with status {order.Status.ToString().ToUpperInvariant()}");
        }
        order.Status = to;
        return repository.Save(order);
    }
}
