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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected AppDbContext _Context;

        public GenericRepository(AppDbContext context)
        {

            _Context = context;

        }
        public int Add(T entity)
        {
            _Context.Add(entity);
            return _Context.SaveChanges();
        }
        public int Update(T entity)
        {
            _Context.Update(entity);
            return _Context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _Context.Remove(entity);
            return _Context.SaveChanges();
        }

        public T Get(int id)
        {
            return _Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee)){

                return (IEnumerable<T>)_Context.Employees.Include(E=>E.WorkFor).AsNoTracking().ToList();

            }
            else
            return _Context.Set<T>().ToList();
        }


    }
}
