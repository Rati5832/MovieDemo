A lightweight demo Web API built with **ASP.NET Core (.NET 8)** showcasing clean architecture, dependency injection, and automated testing with **xUnit**, **FluentAssertions**, and **InMemory EF Core**.

---

## Features

- RESTful API for managing movies (CRUD operations)
- Entity Framework Core with **InMemory** provider for simplicity
- **Repository + Service** architecture pattern
- **Dependency Injection** for modular, testable design
- **Swagger UI** for interactive API testing
- Automated **Unit Tests** and **Integration Tests**

# Clone the repo
git clone https://github.com/Rati5832/MovieDemo.git


cd MovieDemo

# Restore dependencies
dotnet restore

## Run the API
dotnet run --project MovieDemo.Api

## Running Tests
Run all tests (unit + integration):

dotnet test

Unit tests validate business logic in isolation.
Integration tests spin up the full API using WebApplicationFactory and test real HTTP endpoints.
