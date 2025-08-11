# Dotnet Clean Architecture

This repository is a reference implementation of **Clean Architecture** principles in a .NET solution.
It demonstrates how to structure a modern, maintainable, and testable application by separating concerns into clearly defined layers.

---

## 🏛 Architecture Overview

The solution follows the **Clean Architecture** pattern:

- **Domain** – Contains the core business model and rules (entities, value objects, domain events, domain interfaces).
- **Application** – Contains application services that orchestrate business operations, coordinate domain logic, and handle cross-cutting concerns.
- **Infrastructure** – Handles persistence, external integrations, and technical concerns. Implements interfaces defined in the domain/application layers.
- **Api** – Provides HTTP endpoints for clients, mapping requests to application services.
- **SharedKernel** – Cross-cutting abstractions and utilities shared across projects.
- **eShop.AppHost / eShop.ServiceDefaults** – Hosting and default configuration for running the application.

> **Note:** This solution does **not** use CQRS. All reads and writes are handled through the same application services.
> A custom **Domain Event Dispatcher** propagates domain events to their handlers (see section below).

---

## 📂 Project Structure

```
src/
 ├── Api                   # Web API entry point
 ├── Application           # Application services, DTOs, validators
 ├── Domain                # Entities, value objects, domain events, interfaces
 ├── Infrastructure        # Data access, EF Core (if used), external service implementations
 ├── SharedKernel          # Common abstractions and utilities
 ├── eShop.AppHost         # Hosting project
 ├── eShop.ServiceDefaults # Shared service defaults and extensions
 └── eShop.sln             # Visual Studio solution file
```
---

## ✅ Principles

- **Dependency Rule:** Source code dependencies always point inward toward the **Domain**.
- **Separation of Concerns:** Web, application, domain, and infrastructure concerns are **isolated**.
- **Testability:** Business rules can be tested **without** infrastructure.
- **Explicit Boundaries:** Contracts (interfaces) live in the inner layers; implementations live outside.
- **Event-Driven Domain:** Use **Domain Events** to react to state changes cleanly.

---

## 🚀 Getting Started

To get started, ensure you have the following prerequisites installed:

- [Docker](https://www.docker.com/get-started) (for containerized development and infrastructure)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (for building and running the solution)

Once prerequisites are installed, you can clone the repository and follow the setup instructions below.

### 🏁 Running the Solution

To launch the application, start the `eShop.Aspire` project. This project orchestrates the startup of all required services and dependencies.

You can run it using the following command:

```bash
dotnet run --project src/eShop.AppHost
```

This will start the application and all supporting services defined in the Aspire project.

---
## 📚 References
- Robert C. Martin: [*Clean Architecture*](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)  
- Jimmy Bogard: [*Domain Driven Design*](https://www.jimmybogard.com/tag/domain-driven-design/)

---

## 📝 License

This project is licensed under the [MIT License](/docs/License.md).