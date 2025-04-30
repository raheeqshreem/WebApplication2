using System.Linq.Expressions;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public interface IBrandService : IService<Brand>
    {
        Task<bool> EditAsync(int id, Brand brand, CancellationToken cancellationToken = default);
        Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken);

    }
}
