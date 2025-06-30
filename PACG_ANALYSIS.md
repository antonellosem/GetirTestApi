# Pacg Project Analysis

The original **Pacg** solution contains multiple .NET Framework projects. It exposes
SOAP and web services for managing gaming accounts and transactions.

Key projects include:

- `PgadCommunication` – gateway library with hundreds of request/response DTOs,
  WCF service references and custom message encoders.
- `PGADService` – ASP.NET service application hosting endpoints and using
  Autofac for dependency injection.
- `PgadData`, `PgadManager`, `PgadCommon` – data access layer, business logic and
  shared enumerations.
- Several helper libraries (`ClrPgad`, `PGADServiceLibrary`) and test utilities.

The code base relies heavily on legacy technologies such as WCF, packages.config
and .NET Framework 4.7.2. Refactoring is needed to move towards a modern,
maintainable architecture.

This repository keeps the original Pacg sources untouched for reference during
the refactoring effort. Detailed module analysis can be found in
[`NewPacg/PACG_ANALYSIS.md`](NewPacg/PACG_ANALYSIS.md).
