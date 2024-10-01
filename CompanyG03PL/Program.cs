using CompanyG03BLL;
using CompanyG03BLL.Interface;
using CompanyG03BLL.Repositories;
using CompanyG03DAL.Data.Contexts;
using CompanyG03DAL.Models;
using CompanyG03PL.Mapping;
using CompanyG03PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CompanyG03PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // builder.Services.AddScoped<AppDbContext>();//Allow DI for AddDbContext
            // builder.Services.AddDbContext<AppDbContext>();


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();//Allow For DI fro deparmtent depository to create 
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//Allow For DI fro deparmtent depository to create 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            //the differnt is the life time
            //builder.Services.AddScoped();//life time per request,after request its become objct unreachable
            //builder.Services.AddTransient();//life time per operations 
            //builder.Services.AddSingleton();//life time per application 

            //builder.Services.AddScoped<IScoopedService, ScoobedServise>();//pre request
            //builder.Services.AddTransient<ITransientService,TransionServise>();//per operation
            //builder.Services.AddSingleton<ISingeletonService,SingeletonServise>();//per application



            builder.Services.AddIdentity<ApllicationUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();



           // Add services to the container.
           //builder.Services.AddControllersWithViews(options =>
           //{
           //    var policy = new AuthorizationPolicyBuilder()
           //        .RequireAuthenticatedUser()
           //        .Build();
           //    options.Filters.Add(new AuthorizeFilter(policy));
           //});


           // builder.Services.ConfigureApplicationCookie(config =>
           // {
           //     config.LoginPath = "/Account/SignIn";
           //     //config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
           //     //config.SlidingExpiration = true;
           //    // config.AccessDeniedPath = "/Account/AccessDenied";
           // });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

          
            app.Run();
        }
    }
}

