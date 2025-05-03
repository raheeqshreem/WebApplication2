using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public interface IUserService:IService<ApplicationUsr>
    {
        Task<bool> ChangeRole(string userId, string roleName);
        Task<bool?> LockUnLocK(String userId);

    }
}
