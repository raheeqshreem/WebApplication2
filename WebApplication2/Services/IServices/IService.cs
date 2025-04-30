using System;
using System.Linq.Expressions;
using WebApplication2.Models;

namespace WebApplication2.Services.IServices
{
    public interface IService<T>where T : class
    {
       Task< IEnumerable<T>> GetAsync(Expression<Func<T, bool>> ?expression = null,Expression<Func<T, object>>?[] includes = null, bool isTracked = true);
        Task<T?> GetOne(Expression<Func<T, bool>> expression,Expression<Func<T,object>>? []includes = null, bool isTracked = true);
        Task<T> AddAsync(T category, CancellationToken cancellationToken = default);
        Task<bool> removeAsync(int id, CancellationToken cancellationToken = default);



    }
}
