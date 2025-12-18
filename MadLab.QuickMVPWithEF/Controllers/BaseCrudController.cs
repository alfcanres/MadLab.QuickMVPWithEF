using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadLab.QuickMVPWithEF.Entities;

namespace MadLab.QuickMVPWithEF.Controllers
{
    // Now this looks fancy, but it's actually quite simple.
    // This is a generic base controller that provides CRUD operations for any entity type
    // that implements the IHasKey interface and uses a DbContext of type TContext.
    // Let's break it down:
    //
    // 1. The class is marked as abstract, meaning it cannot be instantiated directly.
    // 2. It uses generics (TEntity and TContext) to allow for flexibility in the types of entities and DbContexts it can work with.
    // 3. It has a constructor that takes a DbContext of type TContext and initializes the DbSet for the entity type TEntity.
    // 4. It provides virtual methods for standard CRUD operations (GetAll, GetById, Create, Update, Delete).
    // 5. Each method uses Entity Framework Core to interact with the database.
    // 6. The IncludeNavigation method is a placeholder for including related entities, which can be overridden in derived classes if needed.
    //
    // This base controller can be inherited by specific controllers for different entities, so you don't have to rewrite the same CRUD logic for each entity type.

    /// <summary>
    /// Base controller for CRUD operations. Inherit from this controller to create CRUD endpoints for your entities.
    /// Constraints in generics ensure that TEntity has an Id property and TContext is a DbContext.
    /// </summary>
    /// <typeparam name="TEntity">Your entity type</typeparam>
    /// <typeparam name="TContext">Your DbContext class</typeparam>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseCrudController<TEntity, TContext> : ControllerBase
        where TEntity : class, IHasKey
        where TContext : DbContext
    {
        // Protected members to access the DbContext and DbSet from
        // derived classes in case you need to extend functionality.
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseCrudController(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        // This method can be overridden in derived classes to include related entities.
        // By default, it just returns the query as is. Use it carefully to avoid performance issues, 
        // especially with large datasets or complex relationships. But remember, this is intended for flexibility
        // not performance optimization.
        protected virtual IQueryable<TEntity> IncludeNavigation(IQueryable<TEntity> query) => query;


        // Take a look at these CRUD methods.
        // They're marked as virtual, so you can override them in derived classes if you need custom behavior.
        // However, for most standard use cases, you can simply inherit from this base controller and get CRUD functionality out of the box.
        // No additional code needed!, feels like cheating, right?, that is the beauty of abstraction.
        // Now let's analyze Controllers/TodoItemsController.cs to see how to use this base controller and expand its functionality.

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            return await IncludeNavigation(_dbSet)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(int id)
        {
            var entity = await IncludeNavigation(_dbSet)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (entity == null)
                return NotFound();
            return entity;
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, TEntity entity)
        {
            if (id != entity.Id)
                return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dbSet.AnyAsync(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return NotFound();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
