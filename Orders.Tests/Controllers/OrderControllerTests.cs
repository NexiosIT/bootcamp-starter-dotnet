using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Orders.Api.Controllers;
using Orders.Api.Models;
using Orders.Api.Services;

namespace Orders.Tests.Controllers;

public class OrderControllerTests
{
    private readonly IOrderService _orderService = Substitute.For<IOrderService>();
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _controller = new OrderController(_orderService);
    }

    [Fact]
    public void GetAll_WhenOrdersExist_ReturnsAllOrders()
    {
        var orders = new List<Order>
        {
            new() { Id = 1, Description = "MacBook Pro", Status = OrderStatus.Ordered },
            new() { Id = 2, Description = "iPhone", Status = OrderStatus.Shipped }
        };
        _orderService.GetAll().Returns(orders);

        var result = _controller.GetAll();

        var body = result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IReadOnlyList<Order>>().Subject;
        body.Should().BeEquivalentTo(orders);
        body[0].CreatedAt.Should().NotBe(default);
    }

    [Fact]
    public void GetById_WhenOrderDoesNotExist_ReturnsNotFound()
    {
        _orderService.GetById(99).Returns(_ => throw new OrderNotFoundException(99));

        var result = _controller.GetById(99);

        result.Result.Should().BeOfType<NotFoundObjectResult>()
            .Which.Value.Should().Be("Could not find order 99");
    }

    [Fact]
    public void Create_WhenDescriptionIsBlank_ReturnsBadRequest()
    {
        _orderService.Create(" ").Returns(_ => throw new InvalidOrderException("Description is required"));

        var result = _controller.Create(new CreateOrderRequest(" "));

        result.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().Be("Description is required");
    }

    [Fact]
    public void Ship_WhenOrderAlreadyShipped_ReturnsConflict()
    {
        _orderService.Ship(2).Returns(_ => throw new InvalidOrderStatusException(
            "Cannot ship an order with status SHIPPED"));

        var result = _controller.Ship(2);

        result.Result.Should().BeOfType<ConflictObjectResult>()
            .Which.StatusCode.Should().Be(409);
    }
}
