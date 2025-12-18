# MadLab.QuickMVPWithEF

## Overview
MadLab.QuickMVPWithEF is an educational project designed to demonstrate how to quickly set up a basic CRUD application using ASP.NET Core, Entity Framework Core, generics, inheritance, and common design patterns. The project shows how to build a maintainable and extensible solution with minimal effort, and how to expand its functionality easily.

## Features
- Generic CRUD controllers using inheritance
- Entity Framework Core for data access
- Separation of concerns with entities, data context, and controllers
- Easily extensible endpoints (e.g., search, filter)
- Demonstrates best practices for rapid MVP development

## Technologies Used
- ASP.NET Core (.NET 8)
- Entity Framework Core
- C# 12
- SQL Server (default, can be changed)

## Project Structure
- `Entities/` – Domain models (`TodoItem`, `TodoList`)
- `Data/` – EF Core `DbContext`
- `Controllers/` – Generic and custom controllers for CRUD and search operations

## How to Run
1. Clone the repository
2. Update connection string in `appsettings.json` if needed
3. Run database migrations (if applicable)
4. Start the project (`dotnet run`)
5. Access API endpoints via Swagger or Postman

## Example Endpoints
- `GET /api/TodoItems` – List all todo items
- `GET /api/TodoItems/search?name=...` – Search items by name
- `GET /api/TodoItems/by-todolist?todoListId=...` – Get items by list
- Standard CRUD for `TodoList` and `TodoItem`

## Extending Functionality
To add new features:
- Create new endpoints in controllers
- Add new entities and update `DbContext`
- Leverage generics and inheritance for rapid development

## Educational Value
This project is ideal for learning:
- How to use generics and inheritance in ASP.NET Core
- Setting up EF Core with best practices
- Building maintainable, extensible APIs
