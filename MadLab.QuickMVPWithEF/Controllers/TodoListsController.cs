using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadLab.QuickMVPWithEF.Data;
using MadLab.QuickMVPWithEF.Entities;

namespace MadLab.QuickMVPWithEF.Controllers
{
    public class TodoListsController : BaseCrudController<TodoList, AppDbContext>
    {
        public TodoListsController(AppDbContext context) : base(context) { }

        protected override IQueryable<TodoList> IncludeNavigation(IQueryable<TodoList> query)
            => query.Include(l => l.Items);
    }
}
