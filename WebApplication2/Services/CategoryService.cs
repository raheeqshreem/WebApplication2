using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {


        ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context) : base(context)
        {
           this. _context = context;
        }



      


        public async Task<bool> EditAsync(int id, Category category,CancellationToken cancellationToken=default)
        {
            Category? categoryInDP = _context.Categories.Find(id);
            if (categoryInDP == null)
                return false;
            categoryInDP .Name = category.Name;
            categoryInDP .Description = category.Description;
           await _context.SaveChangesAsync(cancellationToken);
            return true;
            
        }




        public async Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken)
        {
            Category? categoryInDP = _context.Categories.Find(id);
            if (categoryInDP == null)
                return false;
            categoryInDP.status = !categoryInDP.status;
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

    }
}
