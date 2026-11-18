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
    public ActionResult<Order> Create(CreateOrderRequest request) =>
        Handle(() => orderService.Create(request.Description),
            order => CreatedAtAction(nameof(GetById), new { id = order.Id }, order));

    [HttpPut("{id:long}/ship")]
    public ActionResult<Order> Ship(long id) => Handle(() => orderService.Ship(id));

    [HttpPut("{id:long}/deliver")]
    public ActionResult<Order> Deliver(long id) => Handle(() => orderService.Deliver(id));

    [HttpDelete("{id:long}/cancel")]
    public ActionResult<Order> Cancel(long id) => Handle(() => orderService.Cancel(id));

    private ActionResult<T> Handle<T>(Func<T> action, Func<T, ActionResult>? onSuccess = null)
    {
        try
        {
            var result = action();
            return onSuccess is null ? Ok(result) : onSuccess(result);
        }
        catch (OrderNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOrderException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOrderStatusException e)
        {
            return Conflict(e.Message);
        }
    }
}
