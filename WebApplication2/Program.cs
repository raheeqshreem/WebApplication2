
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services;
using WebApplication2.Servises;
using WebApplication2.Utility;
using WebApplication2.Utility.DBInitlizer;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi builder.Services.AddOpenApi();


            builder.Services.AddOpenApi();
          

            var MyAllowSpecificorigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>

            {

                options.AddPolicy(name: MyAllowSpecificorigins,

                    policy =>
                    {

                    });
            });

            builder. Services. AddScoped <IDBInitlizer, DBInitlizer>();

            builder.Services.AddTransient <IEmailSender, EmailSender>();

            builder.Services.AddDbContext <ApplicationDbContext> (options =>

            options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped <ICartService, CartService>();
            builder.Services.AddScoped < IUserService, UserService>();

            builder.Services.AddIdentity <ApplicationUsr, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })

            .AddEntityFrameworkStores <ApplicationDbContext > ()

              .AddDefaultTokenProviders();


            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(
              Options =>
              {
                  Options.TokenValidationParameters = new()
                  {
                      ValidateIssuer=false,
                      ValidateAudience=false,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b2PoI959Z00AcgyDmBU7K8lxI1LWIDV6")),
                  };

              });



            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificorigins);
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IDBInitlizer>();
            service.initilize();



            app.Run();
        }
    }
}
