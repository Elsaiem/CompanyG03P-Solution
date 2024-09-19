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

namespace CompanyG03DAL.Data.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

        //    modelBuilder.ApplyConfiguration(new DepartmentConfigration()); 

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.; Database=CompanyMvc;Trusted_Connection=True;TrustedServerCertificate=True ");
        //}


        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }





    }
}
