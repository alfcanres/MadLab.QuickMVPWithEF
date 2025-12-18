using MadLab.QuickMVPWithEF.Data;
using MadLab.QuickMVPWithEF.Entities;

namespace MadLab.QuickMVPWithEF.Controllers
{

    public class TodoListsController : BaseCrudController<TodoList, AppDbContext>
    {
        // Wow! this controller is empty!, right?
        // Not quite, it inherits all the CRUD operations from BaseCrudController
        // it is all you need for a simple CRUD controller as this controller is
        // fully functional out of the box for CRUD operations on TodoList entities.
        // without writing a single additional line of code!
        // 
        // But how? you may ask, well we usually forget a controller is just a class
        // that handles HTTP requests and like all classes in C#,
        // it can inherit from other classes. Let's take a look at the BaseCrudController
        public TodoListsController(AppDbContext context) : base(context) { }
    }
}