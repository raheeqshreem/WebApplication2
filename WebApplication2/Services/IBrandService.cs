using System.Linq.Expressions;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAll();

        Brand? Get(Expression<Func<Brand, bool>> expression);
        Brand Add(Brand brand);
        bool Edit(int id, Brand brand);
        bool remove(int id);

    }
}
