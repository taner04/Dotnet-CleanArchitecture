# Dotnet Clean Architecture

> **Note:** This is my first project implementing Clean Architecture in .NET. Feedback and suggestions are very welcome—feel free to open an issue or start a discussion!

This repository is a reference implementation of **Clean Architecture** principles in a .NET solution.  
It demonstrates how to structure a modern, maintainable, and testable application by separating concerns into clearly defined layers.

It is also intended to serve as inspiration for others looking to adopt Clean Architecture in their own .NET projects.

---

## 🏛 Architecture Overview

- **Domain** – Contains the core business model and rules (Entities, Value Objects, Domain Events, Domain Interfaces).
- **Application** – Coordinates business processes, orchestrates domain logic, and handles cross-cutting concerns.
- **Persistence** – Responsible for data persistence and access. Implements interfaces from the Domain and Application layers.
- **Infrastructure** – Handles external integrations and technical concerns (e.g., email, logging, authentication) that are not directly related to data persistence.
- **Api** – Exposes HTTP endpoints for clients and maps requests to Application services.
- **SharedKernel** – Common abstractions and utilities shared across multiple projects.
- **eShop.AppHost / eShop.ServiceDefaults** – Hosting and default configuration for running the application.

---

## 🧪 Used Dependencies

- [**BCrypt**](https://github.com/BcryptNet/bcrypt.net) – Used for hashing and validation of passwords.
- [**FluentValidation**](https://github.com/FluentValidation/FluentValidation) – Used to validate DTOs.
- [**Vogen**](https://github.com/SteveDunn/Vogen) – Value object generator for strongly-typed IDs and primitives.
- [**Scalar**](https://github.com/scalar/scalar) – Used for the API documentation.
- [**MailKit**](https://github.com/jstedfast/MailKit) – Used to send emails via IMAP, POP3, and SMTP protocols.
- [**MimeKit**](https://github.com/jstedfast/MimeKit) – MIME parsing and generation library for handling email messages.
- [**DotNetEnv**](https://github.com/tonerdo/dotnet-env) – Used for loading environment variables from `.env` files into .NET applications.
- [**Mediator**](https://github.com/martinothamar/Mediator) – Used for handling domain events.

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
- **MacOS/Unix (bash)**
  ```bash
  ./launch.sh
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
- **Unit-Tests:** Implement tests for each layer
- **Documentation:** Document the codebase with comments and explanations.

---

## 📝 License

This project is licensed under the [MIT License](/LICENSE)
