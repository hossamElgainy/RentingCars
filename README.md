# Car Rental Management System

This project is a modular, layered Car Rental Management System built with C# 12 and .NET 8. It follows clean architecture principles, separating concerns across domain models, data access, services, and presentation layers. The system is designed for scalability, maintainability, and testability.

## Features

- **Car Management:** Add, update, delete, and list cars with details such as model, brand, year, power, and availability.
- **Pagination & Filtering:** Efficiently retrieve paged and filtered lists of cars.
- **Repository & Unit of Work Patterns:** Abstract data access for flexibility and testability.
- **DTOs & Specifications:** Use Data Transfer Objects and Specification pattern for clean querying and data shaping.
- **Async Operations:** All data access and service methods are asynchronous for scalability.

## Technologies

- **.NET 8**
- **C# 12**
- **Entity Framework Core** (assumed for data access)
- **Clean Architecture Principles**

## Project Structure


## Key Classes

- **CarService:** Handles business logic for car-related operations.
- **IUnitOfWork:** Manages repositories and transaction scope.
- **IRepository<T>:** Generic repository interface for CRUD operations.
- **PagedResult<T>:** Encapsulates paginated query results.
- **AllCarsSpecParam:** Parameters for filtering and paginating car queries.

## Example Usage


## Getting Started

1. **Clone the repository**
2. **Restore NuGet packages**
3. **Configure your database connection**
4. **Run database migrations (if using EF Core)**
5. **Build and run the solution**

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

