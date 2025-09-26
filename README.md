# Dotnet Clean Architecture

> **Note:** This is my first project implementing Clean Architecture in .NET. Feedback and suggestions are very
> welcome—feel free to open an issue or start a discussion!

This repository is a reference implementation of **Clean Architecture** principles in a .NET solution.  
It demonstrates how to structure a modern, maintainable, and testable application by separating concerns into clearly
defined layers.

It is also intended to serve as inspiration for others looking to adopt Clean Architecture in their own .NET projects.

---

## 🚀 Getting Started

To get started, ensure you have the following prerequisites installed:

- [Docker](https://www.docker.com/get-started)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

Once prerequisites are installed, you can clone the repository and follow the setup instructions below.

### 🏁 Running the Solution

To launch the application:

1. **Start Docker** to ensure all required containers and services can run.
2. Run the `AppHost` project. This will orchestrate the startup of all necessary services and dependencies.

Alternatively, run the provided script from the root directory:

- **Windows (PowerShell):**
  ```bash
  ./launch.ps1
  ```
- **MacOS/Unix (bash):**
  ```bash
  ./launch.sh
  ```
  This will start the application and all supporting services defined in the Aspire project.

---

## 📁 Project Structure

A quick overview of the main folders and projects:

```
tools/
  AppHost                  # Orchestrates service startup
  MigrationService         # Database migration runner
  ServiceDefaults          # Shared service configuration (telemetry, health, etc.)

src/
  Application              # Application layer (CQRS, business logic)
  Domain                   # Domain models and rules
  Infrastructure           # Persistence, integrations, security
  SharedKernel             # Cross-cutting shared types/utilities
  WebApi                   # HTTP API (presentation layer)

tests/
  Domain.UnitTests         # Unit tests for domain logic
  WebApi.IntegrationTests  # End-to-end tests for API
```

---

## ✨ Features

- **Clean Architecture**: Strict separation of domain, application, infrastructure, and presentation layers for
  maintainability and testability.
- **Aspire Integration**: Modern service orchestration using .NET Aspire, including service discovery and resilience.
- **OpenAPI (Scalar)**: Automatic API documentation and testing available in development mode.
- **JWT Authentication**: Secure authentication via bearer tokens and configurable JWT settings.
- **Global Error Handling**: Unified error handling for the API using custom error responses and problem details.
- **Integration Tests**: Comprehensive integration tests with test database and WebApiFactory.
- **Migration Services**: Automated database migrations on startup to ensure the database schema is up-to-date.
- **CQRS Pattern & Mediator Behaviors**: Implements Command Query Responsibility Segregation using Mediator, with
  pipeline behaviors for logging, validation, and performance monitoring.

---

## 📦 Used Dependencies

### 🔧 Aspire & Hosting

- [Aspire.Hosting.AppHost](https://www.nuget.org/packages/Aspire.Hosting.AppHost)
- [Aspire.Hosting.PostgreSQL](https://www.nuget.org/packages/Aspire.Hosting.PostgreSQL)
- [Aspire.Hosting.Testing](https://www.nuget.org/packages/Aspire.Hosting.Testing)
- [Aspire.Npgsql.EntityFrameworkCore.PostgreSQL](https://www.nuget.org/packages/Aspire.Npgsql.EntityFrameworkCore.PostgreSQL)
- [Microsoft.Extensions.ServiceDiscovery](https://www.nuget.org/packages/Microsoft.Extensions.ServiceDiscovery)

### 🗄️ Database & ORM

- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
- [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design)
- [Microsoft.EntityFrameworkCore.Relational](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Relational)
- [Npgsql.EntityFrameworkCore.PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL)
- [Testcontainers.PostgreSql](https://www.nuget.org/packages/Testcontainers.PostgreSql)

### 🔐 Authentication & Security

- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)
- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt)
- [BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next)

### ✅ Validation & Mapping

- [FluentValidation](https://www.nuget.org/packages/FluentValidation)
- [FluentValidation.DependencyInjectionExtensions](https://www.nuget.org/packages/FluentValidation.DependencyInjectionExtensions)
- [Vogen](https://www.nuget.org/packages/Vogen)
- [Mapster](https://www.nuget.org/packages/Mapster)

### ⚙️ Mediator & CQRS

- [Mediator.Abstractions](https://www.nuget.org/packages/Mediator.Abstractions)
- [Mediator.SourceGenerator](https://www.nuget.org/packages/Mediator.SourceGenerator)

### 📊 OpenTelemetry & Observability

- [OpenTelemetry.Exporter.OpenTelemetryProtocol](https://www.nuget.org/packages/OpenTelemetry.Exporter.OpenTelemetryProtocol)
- [OpenTelemetry.Extensions.Hosting](https://www.nuget.org/packages/OpenTelemetry.Extensions.Hosting)
- [OpenTelemetry.Instrumentation.AspNetCore](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AspNetCore)
- [OpenTelemetry.Instrumentation.Http](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Http)
- [OpenTelemetry.Instrumentation.Runtime](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Runtime)

### 🌐 ASP.NET & Web

- [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing)
- [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi)
- [Scalar.AspNetCore](https://www.nuget.org/packages/Scalar.AspNetCore)

### 🧪 Testing

- [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk)
- [coverlet.collector](https://www.nuget.org/packages/coverlet.collector)
- [xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio)
- [xunit.v3](https://www.nuget.org/packages/xunit.v3)
- [xunit.v3.assert](https://www.nuget.org/packages/xunit.v3.assert)
- [Respawn](https://www.nuget.org/packages/Respawn)

### 🛠️ Utilities

- [ErrorOr](https://www.nuget.org/packages/ErrorOr)
- [Microsoft.Extensions.Http.Resilience](https://www.nuget.org/packages/Microsoft.Extensions.Http.Resilience)
- [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting)

---

## 🤝 Contributing

Contributions, feedback, and suggestions are very welcome!  
Feel free to open an issue, start a discussion, or submit a pull request if you want to help improve this project.

---

## 📝 License

This project is licensed under the [MIT License](/LICENSE)