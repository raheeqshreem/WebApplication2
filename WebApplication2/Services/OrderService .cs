using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public class OrderService : Service<Order>,IOrderService
    {


        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context) : base(context)
        {
           this. _context = context;
        }

       

        

    }
}
