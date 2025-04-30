using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services.IServices
{
    public class Service<T> : IService<T> where T : class
    {


       private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;
        public Service(ApplicationDbContext context)
        {
           _context = context;
            _dbset = _context.Set<T>();
        }


        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity,cancellationToken);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> GetOne(Expression<Func<T, bool>> expression = null, Expression<Func<T, object>>?[] includes = null
            , bool isTracked = true)

        {
            var all = await GetAsync(expression, includes, isTracked);

            return all.FirstOrDefault();
        }


        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>?[] includes = null
            , bool isTracked = true)

        {
            IQueryable<T> entites = _dbset;
            if(expression is not null)
            {
                entites = entites.Where(expression);
            }
            if(includes is not null)
            {
                foreach (var item in includes)
                {
                    entites = entites.Include(item);
                }
            }
            if (!isTracked)
            {
                entites = entites.AsNoTracking();
            }

            return await entites.ToListAsync();
        }


        public async Task<bool> removeAsync(int id, CancellationToken cancellationToken = default)
        {
            T? categoryInDP = _dbset.Find(id);
            if (categoryInDP == null)
                return false;
            _dbset.Remove(categoryInDP);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        
    }
}
