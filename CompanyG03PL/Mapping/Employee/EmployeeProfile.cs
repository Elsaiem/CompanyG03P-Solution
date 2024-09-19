using AutoMapper;
using CompanyG03DAL.Models;
using CompanyG03PL.ViewModels;

namespace CompanyG03PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            //CreateMap<Employee, EmployeeViewModel>();



        }



    }
}
