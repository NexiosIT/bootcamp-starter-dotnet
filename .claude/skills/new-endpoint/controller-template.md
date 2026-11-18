# Controller-template

Controller-actie in `Orders.Api/Controllers/OrderController.cs`. De controller praat alleen met `IOrderService`.

```csharp
[HttpPut("{id:long}/<actie>")]
public ActionResult<Order> <Actie>(long id) => Handle(() => orderService.<Actie>(id));
```

`Handle<T>` is generiek, dus een actie die een DTO teruggeeft gebruikt hem net zo:

```csharp
[HttpGet("{id:long}/<iets>")]
public ActionResult<<Dto>> <Actie>(long id) => Handle(() => orderService.<Actie>(id));
```

Service-methode in `Orders.Api/Services/OrderService.cs` (ook toevoegen aan `IOrderService`):

```csharp
public Order <Actie>(long id) => Transition(id, "<actie>", OrderStatus.<Van>, OrderStatus.<Naar>);
```

- `Handle<T>` geeft 200 met het resultaat; `OrderNotFoundException` wordt 404, `InvalidOrderException` 400 en `InvalidOrderStatusException` 409, telkens met de boodschap als `text/plain`
- Test in `Orders.Tests/Controllers/OrderControllerTests.cs` met een gemockte `IOrderService`
