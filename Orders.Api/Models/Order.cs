namespace Orders.Api.Models;

public class Order
{
    public long Id { get; set; }
    public string Description { get; set; } = "";
    public OrderStatus Status { get; set; }
}
