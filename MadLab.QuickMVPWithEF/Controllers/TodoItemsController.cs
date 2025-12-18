using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadLab.QuickMVPWithEF.Data;
using MadLab.QuickMVPWithEF.Entities;

namespace MadLab.QuickMVPWithEF.Controllers
{
    public class TodoItemsController : BaseCrudController<TodoItem, AppDbContext>
    {
        public TodoItemsController(AppDbContext context) : base(context) { }

        protected override IQueryable<TodoItem> IncludeNavigation(IQueryable<TodoItem> query)
            => query.Include(i => i.TodoList);

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


    }
}
