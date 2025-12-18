using MadLab.QuickMVPWithEF.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/**********************************************/
// Welcome to QuickMVP with EF!
//
// This project is intended to be a quick starter template for those situations
// where you want to get going quickly with ASP.NET Core Web API and Entity Framework Core
// using a code-first approach.
//
//  Basically, this project is a pared-down version of the default ASP.NET Core Web API template
//  with a focus on simplicity and minimalism, no fancy architectural patterns, no DTOs, no repositories,
//  and just enough EF Core setup, just create your entities and get going quickly with CRUD operations,
//  and if you need to, you can easily expand from there.
//
//  The project uses SQLite as the database provider for simplicity, but you can easily switch to another provider
//  by changing the configuration in Program.cs and updating the NuGet packages accordingly.

// Just remember, this is intended for quick prototyping and small projects, once your project has 
// outgrown this simple setup, consider refactoring to a more robust architecture.
//
// In order for you to understand how this works, you will need to have a good understanding of:
//  - ASP.NET Core Web API
//  - Entity Framework Core
//  - Inheritance in C#
//  - Generics in C#
//
//  **********************************************/


// We will start by configuring the DbContext for EF Core. As I said, we are using SQLite for simplicity,
// but you can change this to any other provider as needed.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//Now let's go to Controllers/TodoListsController.cs to see how we can implement a simple CRUD controller


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
