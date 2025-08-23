# Dotnet Clean Architecture

> **Note:** This is my first project implementing Clean Architecture in .NET. Feedback and suggestions are very welcome—feel free to open an issue or start a discussion!

This repository is a reference implementation of **Clean Architecture** principles in a .NET solution.  
It demonstrates how to structure a modern, maintainable, and testable application by separating concerns into clearly defined layers.

It is also intended to serve as inspiration for others looking to adopt Clean Architecture in their own .NET projects.

---

## 🏛 Architecture Overview

The solution follows the **Clean Architecture** pattern:

![Architecture Diagram](/png/architecture.png)

- **Domain** – Contains the core business model and rules (entities, value objects, domain events, domain interfaces).
- **Application** – Contains application services that orchestrate business operations, coordinate domain logic, and handle cross-cutting concerns.
- **Infrastructure** – Handles persistence, external integrations, and technical concerns. Implements interfaces defined in the domain/application layers.
- **Api** – Provides HTTP endpoints for clients, mapping requests to application services.
- **SharedKernel** – Cross-cutting abstractions and utilities shared across projects.
- **eShop.AppHost / eShop.ServiceDefaults** – Hosting and default configuration for running the application.

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

## 🚀 Getting Started

To get started, ensure you have the following prerequisites installed:

- [Docker](https://www.docker.com/get-started)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

Once prerequisites are installed, you can clone the repository and follow the setup instructions below.

### 🏁 Running the Solution

To launch the application:

1. **Start Docker** to ensure all required containers and services can run.
2. Run the `eShop.AppHost` project. This will orchestrate the startup of all necessary services and dependencies.

Alternatively, run the provided script from the root directory:

- **Windows (PowerShell):**
  ```powershell
  ./launch.ps1
  ```

This will start the application and all supporting services defined in the Aspire project.

---

## 📚 References

- Robert C. Martin: [_Clean Architecture_](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- Jimmy Bogard: [_Domain Driven Design_](https://www.jimmybogard.com/tag/domain-driven-design/)

---

## 💡 Future Ideas

Here are some features planned for future implementation:

- **Caching:** Improve performance by adding caching strategies for frequently accessed data.
- **Paging:** Implement paging support for API endpoints that return collections.
- **Client:** Implement a client that interacts with the API
- **Launch (macOS/Unix):** Provide a shell script (e.g., `launch.sh`) similar to the Windows PowerShell script (`launch.ps1`) to automate environment checks and application startup for macOS and Linux users.

---

## 📝 License

This project is licensed under the [MIT License](/LICENSE)
