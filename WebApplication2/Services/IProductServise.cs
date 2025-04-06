using System.Linq.Expressions;
using WebApplication2.DTO.Request;
using WebApplication2.DTO.Response;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IProductServise
    {

        IEnumerable<ProductResponse> GetAll();

        ProductResponse? Get(int id);
        ProductResponse Add(ProductRequest productRequest);
      
        bool remove(int id);



    }
}
