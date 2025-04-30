using System.Linq.Expressions;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public class BrandService : Service<Brand>,IBrandService
    {


        ApplicationDbContext _context;
        public BrandService(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }






        public async Task<bool> EditAsync(int id, Brand brand, CancellationToken cancellationToken = default)
        {
            Brand? brandInDP = _context.Brands.Find(id);
            if (brandInDP == null)
                return false;
            brandInDP.Name = brand.Name;
            brandInDP.Description = brand.Description;
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }




        public async Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken)
        {
            Brand? brandInDP = _context.Brands.Find(id);
            if (brandInDP == null)
                return false;
            brandInDP.status = !brandInDP.status;
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }



    }
}
