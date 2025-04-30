using System.Linq.Expressions;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public interface ICategoryService:IService<Category>
    {
      
       Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default);
        Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken);
    }
}
