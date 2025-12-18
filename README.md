# MadLab.QuickMVPWithEF

## Overview
Welcome to **QuickMVP with EF**! This educational project demonstrates how to quickly set up a basic CRUD application using ASP.NET Core, Entity Framework Core, generics, and inheritance. 

This is a pared-down version of the default ASP.NET Core Web API template with a focus on **simplicity and minimalism**—no fancy architectural patterns, no DTOs, no repositories. Just create your entities and get going quickly with CRUD operations, and if you need to, you can easily expand from there.

> ⚠️ **Note:** This is intended for quick prototyping and small projects. Once your project has outgrown this simple setup, consider refactoring to a more robust architecture.

## Prerequisites
To understand how this project works, you should have a good understanding of:
- ASP.NET Core Web API
- Entity Framework Core
- Inheritance in C#
- Generics in C#

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
- SQLite (default, easily switchable)

## Project Structure
```
MadLab.QuickMVPWithEF/
├── Controllers/
│   ├── BaseCrudController.cs    # Generic base controller with CRUD operations
│   ├── TodoListsController.cs   # Simple controller inheriting CRUD functionality
│   └── TodoItemsController.cs   # Extended controller with custom endpoints
├── Data/
│   └── AppDbContext.cs          # EF Core DbContext
├── Entities/
│   ├── IHasKey.cs               # Interface for entities with an Id property
│   ├── TodoList.cs              # TodoList entity
│   └── TodoItem.cs              # TodoItem entity
└── Program.cs                   # Application entry point and configuration
```

## Learning Path
Follow this order to understand how the project works:

1. **`Program.cs`** – Start here to see how the application is configured, including DbContext setup with SQLite.

2. **`Controllers/TodoListsController.cs`** – Notice how this controller is almost empty, yet it has full CRUD functionality! It inherits everything from `BaseCrudController`.

3. **`Controllers/BaseCrudController.cs`** – The magic happens here. This generic base controller provides all CRUD operations for any entity that implements `IHasKey`.

4. **`Controllers/TodoItemsController.cs`** – See how to extend the base controller with custom endpoints like search and filtering.

## How It Works

### The `IHasKey` Interface
All entities must implement this interface to work with the generic base controller:
```csharp
public interface IHasKey
{
    int Id { get; set; }
}
```

### The Generic Base Controller
`BaseCrudController<TEntity, TContext>` uses generics and constraints to provide CRUD operations for any entity:
- `TEntity` must implement `IHasKey`
- `TContext` must be a `DbContext`

This means you get `GetAll`, `GetById`, `Create`, `Update`, and `Delete` endpoints **without writing any code**!

### Empty Controller = Full CRUD
```csharp
public class TodoListsController : BaseCrudController<TodoList, AppDbContext>
{
    public TodoListsController(AppDbContext context) : base(context) { }
    // That's it! Full CRUD functionality inherited.
}
```

### Extending Functionality
Need custom endpoints? Override methods or add new ones:
```csharp
public class TodoItemsController : BaseCrudController<TodoItem, AppDbContext>
{
    // Override IncludeNavigation to include related entities
    protected override IQueryable<TodoItem> IncludeNavigation(IQueryable<TodoItem> query)
        => query.Include(i => i.TodoList);

    // Add custom search endpoint
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> SearchByName([FromQuery] string name)
    {
        // Custom logic here
    }
}
```

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/alfcanres/MadLab.QuickMVPWithEF.git
cd MadLab.QuickMVPWithEF
```

### 2. Update Connection String (Optional)
The default connection string in `appsettings.json` uses SQLite. Update if needed.

### 3. Run Migrations
```bash
dotnet ef database update
```

### 4. Start the Application
```bash
cd MadLab.QuickMVPWithEF
dotnet run
```

### 5. Explore the API
Access Swagger UI at `https://localhost:<port>/swagger` to test the endpoints.

## API Endpoints

### TodoLists
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/TodoLists` | Get all todo lists |
| GET | `/api/TodoLists/{id}` | Get a specific todo list |
| POST | `/api/TodoLists` | Create a new todo list |
| PUT | `/api/TodoLists/{id}` | Update a todo list |
| DELETE | `/api/TodoLists/{id}` | Delete a todo list |

### TodoItems
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/TodoItems` | Get all todo items |
| GET | `/api/TodoItems/{id}` | Get a specific todo item |
| POST | `/api/TodoItems` | Create a new todo item |
| PUT | `/api/TodoItems/{id}` | Update a todo item |
| DELETE | `/api/TodoItems/{id}` | Delete a todo item |
| GET | `/api/TodoItems/search?name=...` | Search items by title |
| GET | `/api/TodoItems/by-todolist?todoListId=...` | Get items by list ID |

## Adding a New Entity

1. **Create the entity** implementing `IHasKey`:
   ```csharp
   public class MyEntity : IHasKey
   {
       public int Id { get; set; }
       // Add your properties
   }
   ```

2. **Add DbSet to AppDbContext**:
   ```csharp
   public DbSet<MyEntity> MyEntities { get; set; }
   ```

3. **Create the controller**:
   ```csharp
   public class MyEntitiesController : BaseCrudController<MyEntity, AppDbContext>
   {
       public MyEntitiesController(AppDbContext context) : base(context) { }
   }
   ```

4. **Run migrations** and you're done!

## Switching Database Providers
The project uses SQLite by default. To switch to another provider:

1. Install the appropriate NuGet package (e.g., `Microsoft.EntityFrameworkCore.SqlServer`)
2. Update the configuration in `Program.cs`:
   ```csharp
   builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   ```
3. Update the connection string in `appsettings.json`

## Educational Value
This project is ideal for learning:
- How to use generics and inheritance in ASP.NET Core
- Setting up EF Core with a code-first approach
- Building maintainable, extensible APIs
- The power of abstraction in reducing boilerplate code


Happy coding! 🚀
