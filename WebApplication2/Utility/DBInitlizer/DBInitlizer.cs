using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Reflection;
using WebApplication2.Data;
using WebApplication2.Models;
namespace WebApplication2.Utility.DBInitlizer
{
    public class DBInitlizer : IDBInitlizer
    {

        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUsr> userManager;

        public DBInitlizer(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUsr> userManager)
        {

            this.context = context;

            this.roleManager = roleManager;

            this.userManager = userManager;


        }
        public async Task initilize()

        {

            try

            {

                if (context.Database.GetPendingMigrations().Any())

                    context.Database.Migrate();
            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.Message);
            }

            if (!roleManager.Roles.Any())

            {

                await roleManager.CreateAsync(new("SuperAdmin"));
                await roleManager.CreateAsync(new("Admin"));
                await roleManager.CreateAsync(new("Customer"));
                await roleManager.CreateAsync(new("Company"));

                await userManager.CreateAsync(new()

                {

                    FirstName = "super",
                    LastName = "admin",
                    UserName = "super admin",
                    Gender = ApplicationUserGender.Male,
                  
                    Email = "admingtshop.com",

                }, "Admin@1");

                var user = await userManager.FindByEmailAsync("admin@tshop.com");

                await userManager.AddToRoleAsync(user, "SuperAdmin");

            }
        }




                    }
                }
