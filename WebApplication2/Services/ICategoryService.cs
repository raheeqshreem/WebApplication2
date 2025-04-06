using System.Linq.Expressions;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();

        Category? Get(Expression<Func<Category, bool>> expression);
        Category Add(Category category);
        bool Edit(int id,Category category);
        bool remove(int id);

    }
}
