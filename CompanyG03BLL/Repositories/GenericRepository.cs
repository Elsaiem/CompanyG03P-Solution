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
        public async Task<int> AddAsync(T entity)
        {
            await _Context.AddAsync(entity);
            return await _Context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T entity)
        {
            _Context.Update(entity);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _Context.Remove(entity);
            return await _Context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee)){

                return  (IEnumerable<T>) await _Context.Employees.Include(E=>E.WorkFor).AsNoTracking().ToListAsync();

            }
            else
            return await _Context.Set<T>().ToListAsync();
        }

       
    }
}
