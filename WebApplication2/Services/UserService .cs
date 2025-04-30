using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services
{
    public class UserService : Service<ApplicationUsr>,IUserService
    {
        private readonly UserManager<ApplicationUsr> userManager;

      
        ApplicationDbContext _context;
        public UserService(ApplicationDbContext context, UserManager<ApplicationUsr> userManager) : base(context)
        {
           this. _context = context;
            this.userManager = userManager;
        }
        [HttpGet]

        public async Task<bool> ChangeRole(string userId, string roleName)
        {

            var user = await userManager.FindByIdAsync(userId);
            if (user is not null)

            {

                //remove old roles

                var oldRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, oldRoles);

                //add new role

                var result = await userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)

                {
                    return true;
                }



                else
                {

                    return false;
                }
            }

            return false;
        }



            }
}
