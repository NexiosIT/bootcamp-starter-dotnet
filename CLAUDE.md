# Order App
## Commands
- Build: `dotnet build` · Test: `dotnet test`
- Run: `dotnet run --project Orders.Api` (http://localhost:8080)
## Conventies
- Controllers in `Controllers/`, services in `Services/`, repositories in `Repositories/`, modellen in `Models/`
- Nooit rechtstreeks repository-calls vanuit controllers
- Tests in `Orders.Tests/`, gespiegeld op de structuur van `Orders.Api/`
## Gotchas
- Order-status is een enum `OrderStatus`, niet hardcoden
- JSON toont de status als `ORDERED`, `SHIPPED`, ... via de converter in `Program.cs`
- Opslag is in-memory (`InMemoryOrderRepository`): data verdwijnt bij een herstart
