using Orders.Api.Models;

namespace Orders.Api.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();
    private readonly object _lock = new();
    private long _nextId = 1;

    public InMemoryOrderRepository()
    {
        Save(new Order { Description = "MacBook Pro", Status = OrderStatus.Ordered });
        Save(new Order { Description = "iPhone", Status = OrderStatus.Shipped });
    }

    public IReadOnlyList<Order> FindAll()
    {
        lock (_lock)
        {
            return _orders.ToList();
        }
    }

    public Order? FindById(long id)
    {
        lock (_lock)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }
    }

    public Order Save(Order order)
    {
        lock (_lock)
        {
            if (order.Id == 0)
            {
                order.Id = _nextId++;
                _orders.Add(order);
            }
            return order;
        }
    }
}
