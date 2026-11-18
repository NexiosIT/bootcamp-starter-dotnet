using Microsoft.AspNetCore.Mvc;
using Orders.Api.Models;
using Orders.Api.Services;

namespace Orders.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<Order>> GetAll() => Ok(orderService.GetAll());

    [HttpGet("{id:long}")]
    public ActionResult<Order> GetById(long id) => Handle(() => orderService.GetById(id));

    [HttpPost]
    public ActionResult<Order> Create(CreateOrderRequest request)
    {
        var order = orderService.Create(request.Description);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id:long}/ship")]
    public ActionResult<Order> Ship(long id) => Handle(() => orderService.Ship(id));

    [HttpPut("{id:long}/deliver")]
    public ActionResult<Order> Deliver(long id) => Handle(() => orderService.Deliver(id));

    [HttpDelete("{id:long}/cancel")]
    public ActionResult<Order> Cancel(long id) => Handle(() => orderService.Cancel(id));

    private ActionResult<Order> Handle(Func<Order> action)
    {
        try
        {
            return Ok(action());
        }
        catch (OrderNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOrderStatusException e)
        {
            return Conflict(e.Message);
        }
    }
}
