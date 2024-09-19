using CompanyG03BLL.Interface;
using CompanyG03DAL.Data.Contexts;
using CompanyG03DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyG03BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
      

        public EmployeeRepository(AppDbContext context) : base(context)
        {//Ask CLR To create new object of appDbContext
            


        }

        public IEnumerable<Employee> GetByName(string name)
        {
          return   _Context.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E=>E.WorkFor).ToList();   
        }

        //public int Add(Employee entity)
        //{
        //    _Context.Employees.Add(entity);
        //    return _Context.SaveChanges();
        //}
        //public int Update(Employee entity)
        //{
        //    _Context.Employees.Update(entity);
        //    return _Context.SaveChanges();

        //}

        //public int Delete(Employee entity)
        //{
        //    _Context.Employees.Remove(entity);
        //    return _Context.SaveChanges();
        //}

        //public Employee Get(int id)
        //{
        //    // return _Context.Department.FirstOrDefault(D => D.Id == id);
        //    return _Context.Employees.Find(id);
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return _Context.Employees.ToList();
        //}

    }
}
