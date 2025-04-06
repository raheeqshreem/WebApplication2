using System.Linq.Expressions;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class BrandService : IBrandService
    {


        ApplicationDbContext _context;
        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }



        public Brand Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return brand;

        }


        public bool Edit(int id, Brand brand)
        {
            Brand? BrandInDb = _context.Brands.Find(id);
            if (BrandInDb == null)
                return false;
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return true;

        }

        public Brand? Get(Expression<Func<Brand, bool>> expression)
        {
            return _context.Brands.FirstOrDefault(expression);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        public bool remove(int id)
        {
            Brand? BrandInDP = _context.Brands.Find(id);
            if (BrandInDP == null)
                return false;
            _context.Brands.Remove(BrandInDP);
            _context.SaveChanges();
            return true;
        }
    }
}



    }
}
