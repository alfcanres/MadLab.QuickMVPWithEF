using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadLab.QuickMVPWithEF.Data;
using MadLab.QuickMVPWithEF.Entities;

namespace MadLab.QuickMVPWithEF.Controllers
{
    /// This controller has a little more code isn't it?
    /// Let's see what is going on here.
    public class TodoItemsController : BaseCrudController<TodoItem, AppDbContext>
    {
        public TodoItemsController(AppDbContext context) : base(context) { }

        // Here we are overriding the IncludeNavigation method to include the related TodoList entity
        // whenever we query for TodoItem entities, we will be able to get the associated TodoList data as well.
        protected override IQueryable<TodoItem> IncludeNavigation(IQueryable<TodoItem> query)
            => query.Include(i => i.TodoList);


        // This is a custom endpoint to search TodoItems by their title.
        // It uses the EF.Functions.Like method to perform a case-insensitive search.
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name query parameter is required.");

            var items = await IncludeNavigation(_dbSet)
                .Where(i => EF.Functions.Like(i.Title, $"%{name}%"))
                .ToListAsync();

            return Ok(items);
        }

        // Another custom endpoint to get TodoItems by their associated TodoListId.
        // This allows clients to retrieve all items that belong to a specific todo list.
        [HttpGet("by-todolist")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetByTodoListId([FromQuery] int todoListId)
        {
            var items = await IncludeNavigation(_dbSet)
                .Where(i => i.TodoListId == todoListId)
                .ToListAsync();

            return Ok(items);
        }


        // And that's a wrap! this controller now has all the CRUD operations inherited from BaseCrudController
        // and has extended functionality with two custom endpoints for searching and filtering TodoItems.
        // What's next?, well you can create another entity and controller following the same pattern, just make sure to 
        // update the DbSet and include any necessary navigation properties.
        // Happy coding!

    }
}
