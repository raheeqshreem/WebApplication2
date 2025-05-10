using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public class PasswordRestCodeService : Service<PasswordRestCodeService>, IPasswordRestCodeService
    {


        ApplicationDbContext _context;
        public PasswordRestCodeService(ApplicationDbContext context) : base(context)
        {
           this. _context = context;
        }



      
    }
}
