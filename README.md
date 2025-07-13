# .NET 9 Web API Project

## Project Overview
Modern .NET 9 Web API implementing Clean Architecture with robust infrastructure components.

## Architectural Patterns

### 1. Specification & Repository Patterns
- **Package**: `Ardalis.Specification`
- Base `Repository<T>` with CRUD operations
- Specifications encapsulate query logic
- Supports eager loading via `Include()`/`ThenInclude()`

### 2. Mediator Pattern
- **Package**: `MediatR`
- Commands/Queries separated from handlers
- No direct controller-business logic coupling

### 3. MediatR Pipeline `ValidationBehavior<TRequest, TResponse>` `ValidationProcessor<TRequest>`
- Pipeline behaviors for:
  - Validation
  - Logging
  - Transactions
  - Exception handling
- `IPipelineBehavior<TRequest, TResponse>`, `IRequestPreProcessor<TRequest>`

### 4. Fluent Validation
- Request validation layer
- Automatic DI registration
- Custom validation exceptions

### 5. Entity Framework Core
- Code-first migrations
- Supports **SQL Server**
- Repository + Unit of Work
- Database-agnostic design

### 6. Clean Architecture
- **Layers**:
  - Domain (Core business)
  - Application (Use cases)
  - Infrastructure (Persistence)
  - Presentation (API)

### 7. Global Exception Handling
- `ExceptionHandlerMiddleware`
- RFC 7807 ProblemDetails
- Handles:
  - Validation
  - Database
  - Domain
  - Unhandled exceptions

### 8. Strongly-Typed IDs
- **Package**: `StronglyTypedId`
- Type-safe entity IDs
- EF Core value converters

### 9. Pagination & Dynamic Querying
- Features:
  - Multi-column sorting
  - Expression tree filtering
  - Server-side pagination
- `PagedResponse<T>` and `RequestOptions` models

### 10. EF Core interceptor
- Features:
  - Auditing added and modified entities
- `SaveChangesInterceptor` abstract class

### 11. Auto Apply Migration
- Features:
  - extension method on IApplicationBuilder to create a scope to apply migration automatically
  - No need for `dotnet ef database update` or `Update-Database` commands

### 12. ICurrentUserService
- Features:
  - Implement `IHttpContextAccessor` to get the current user's login data from identity claims and user claims

## Project Instructions
- `env` folder contains `sqlserver.yaml` you can use this file with `command docker compose -f file.yml up`
