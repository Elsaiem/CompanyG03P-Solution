using CompanyG03DAL.Data.Configrations;
using Microsoft.Extensions.Configuration;
using CompanyG03DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CompanyG03DAL.Data.Contexts
{
    public class AppDbContext:IdentityDbContext<ApllicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //    modelBuilder.ApplyConfiguration(new DepartmentConfigration()); 
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            



        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.; Database=CompanyMvc;Trusted_Connection=True;TrustedServerCertificate=True ");
        //}


        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }

        //public DbSet<IdentityRole> Roles { get; set; }


    }
}
