using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadLab.QuickMVPWithEF.Entities;

namespace MadLab.QuickMVPWithEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseCrudController<TEntity, TContext> : ControllerBase
        where TEntity : class, IHasKey
        where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseCrudController(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> IncludeNavigation(IQueryable<TEntity> query) => query;

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            return await IncludeNavigation(_dbSet).ToListAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(int id)
        {
            var entity = await IncludeNavigation(_dbSet).FirstOrDefaultAsync(e => e.Id == id);
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
