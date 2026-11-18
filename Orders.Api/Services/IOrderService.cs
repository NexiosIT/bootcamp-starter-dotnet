using Orders.Api.Models;

namespace Orders.Api.Services;

public interface IOrderService
{
    IReadOnlyList<Order> GetAll();
    Order GetById(long id);
    Order Create(string description);
    Order Ship(long id);
    Order Deliver(long id);
    Order Cancel(long id);
}
