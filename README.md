# Orders API (.NET starter)

Starter repo for the Claude Code bootcamp: a small ASP.NET Core REST API for orders (in-memory, seeded with 2 orders).

- Prerequisites: .NET 8 SDK or newer, git, jq (for the hooks). Windows: run the hooks via Git Bash.
- Run: `dotnet run --project Orders.Api`, then `curl localhost:8080/orders`
- Test: `dotnet test`. Run the tests once before 15:15 so the NuGet cache is warm (the Stop hook runs them).
- Checkpoints: `cp/1300-plan-mode`, `cp/1335-context`, `cp/1445-subagents`, `cp/1515-hooks`, `cp/1540-mini-run` hold the state at the start of each block. Jump with `git stash -u && git switch cp/<block>`.

Based on [spring-guides/tut-rest](https://github.com/spring-guides/tut-rest) (`links/`), Apache-2.0, ported to .NET and simplified. See `LICENSE`.
