# Pacg Refactoring Analysis

This page collects detailed notes about the legacy **Pacg** solution while we refactor it into the new `NewPacg` code base. The document acts as a hub to the rest of the analysis. See the [root overview](../PACG_ANALYSIS.md) for a high-level summary.

## Project Structure in `Pacg`

* **PgadCommunication** - gateway library with request/response DTOs and custom message encoders.
* **PGADService** - ASP.NET application hosting SOAP endpoints and using Autofac for dependency injection.
* **PgadCommon** - shared enumerations and constants.
* **PgadData** - data access objects and connection helpers.
* **PgadManager** - initial business layer with stub methods.
* **PGADServiceLibrary** - utility classes used by the service host.
* **ClrPgad / ClrPGAD_sql2012** - SQL CLR projects.
* **Lib** - third‑party libraries and old packages.config references.
* **Docs** - architecture documents from the original development team.

All projects target **.NET Framework 4.7.2** and rely heavily on WCF and packages.config packages. The build process uses *.csproj* files with legacy references.

## Key Modules

### PgadCommunication
- `Business` folder exposes domain concepts such as `Bonus`.
- `Configuration` manages service endpoints and X509 certificates.
- `Contracts` contains hundreds of DTO classes under `Requests` and `Responses`.
- `CustomMessageEncoder` implements SOAP message encoders for AAMS protocols.
- `Factory.cs` builds gateway instances based on configuration.

### PGADService
- WCF service host (`PGADService.svc` and `.json.svc`).
- Global.asax configures dependency injection and service routes.
- Autofac modules register `PgadManager` and data layer services.

### PgadData
- Data access layer with DAO classes such as `PacgProtocolloDao` and `TransazioniDao`.
- `Entity` folder provides simple POCOs (`HeaderInfo` etc.) used by DAOs.

### PgadManager
- Minimal wrapper over `PgadData` providing business logic entry points.
- In legacy code many methods are still TODO or thin pass‑throughs.

## Refactoring Considerations

The target `NewPacg` follows the structure of **grattaevinciapi**:

```
NewPacg.sln
├── NewPacg           (ASP.NET Core Web API)
├── NewPacg.Common    (contracts, entities, enums)
├── NewPacg.Data      (data access abstractions)
├── NewPacg.Manager   (business services)
└── NewPacg.Tests     (xUnit tests)
```

We will progressively port functionality from `PgadCommunication`, `PGADService` and related projects into this new layout. Initial steps focus on modernising the DTOs and service contracts, followed by the data layer and business logic.

* Use **.NET 6** and SDK‑style project files.
* Replace WCF endpoints with ASP.NET Core Web API controllers.
* Introduce dependency injection via `Microsoft.Extensions.DependencyInjection`.
* Move away from packages.config to SDK project references.
* Add thorough unit tests in `NewPacg.Tests` as components are migrated.

Further details about each legacy component will be documented in dedicated pages linked from this file as the refactoring progresses.
