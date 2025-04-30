using System.Linq.Expressions;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public interface IProductServise :IService<Product>
    {

        Task<bool> AddAsync(ProductRequest request, CancellationToken cancellationToken = default);
        Task<bool> remove(int id, CancellationToken cancellationToken = default);
        Task<bool> Edit(int id, ProductRequest category, CancellationToken cancellationToken = default);

    }
}
