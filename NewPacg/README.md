# NewPacg

This directory contains the new solution used to refactor the legacy **Pacg** project.
It follows the layered approach of the `grattaevinciapi` reference project and targets
.NET 6.

## Projects

- `NewPacg` – ASP.NET Core Web API. No endpoints are implemented yet.
- `NewPacg.Manager` – placeholder for business services.
- `NewPacg.Data` – data access abstractions only.
- `NewPacg.Common` – shared contracts, entities and enumerations.
- `NewPacg.Tests` – xUnit test suite.

At the moment only base abstractions are implemented. Actual business logic will
be developed during the refactoring of the old Pacg solution.
