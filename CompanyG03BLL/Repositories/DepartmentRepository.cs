using CompanyG03BLL.Interface;
using CompanyG03DAL.Data.Contexts;
using CompanyG03DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyG03BLL.Repositories
{
    //CLR

    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _Context;

        public DepartmentRepository(AppDbContext context) : base(context)
        { /*:base(context) {//Ask CLR To create new object of appDbContext*/
            _Context = context;



        }

        //public int Add(Department entity)
        //{
        //    _Context.Department.Add(entity);
        //    return _Context.SaveChanges();
        //}
        //public int Update(Department entity)
        //{
        //    _Context.Department.Update(entity);
        //    return _Context.SaveChanges();

        //}

        //public int Delete(Department entity)
        //{
        //    _Context.Department.Remove(entity);
        //    return _Context.SaveChanges();
        //}

        //public Department Get(int id)
        //{
        //    // return _Context.Department.FirstOrDefault(D => D.Id == id);
        //    return _Context.Department.Find(id);
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    return _Context.Department.ToList();
        //}


    }





}


















