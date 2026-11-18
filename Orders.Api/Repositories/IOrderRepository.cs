using Orders.Api.Models;

namespace Orders.Api.Repositories;

public interface IOrderRepository
{
    IReadOnlyList<Order> FindAll();
    Order? FindById(long id);
    Order Save(Order order);
}
