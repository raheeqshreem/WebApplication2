using System.Linq.Expressions;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class CategoryService : ICategoryService
    {


        ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }



        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;

        }


        public bool Edit(int id, Category category)
        {
            Category? categoryInDP = _context.Categories.Find(id);
            if (categoryInDP == null)
                return false;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return true;
            
        }

        public Category? Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.FirstOrDefault(expression);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public bool remove(int id)
        {
            Category? categoryInDP = _context.Categories.Find(id);
            if (categoryInDP == null)
                return false;
            _context.Categories.Remove(categoryInDP);
            _context.SaveChanges();
            return true;
        }
    }
}
