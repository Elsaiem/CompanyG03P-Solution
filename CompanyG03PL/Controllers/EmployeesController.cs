using AutoMapper;
using CompanyG03BLL.Interface;
using CompanyG03DAL.Models;
using CompanyG03PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace CompanyG03PL.Controllers
{
    public class EmployeesController : Controller
    {
        //private readonly IEmployeeRepository repository;
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public EmployeesController(/*IEmployeeRepository repository,IDepartmentRepository departmentRepository,*/IMapper mapper,IUnitOfWork unitOfWork)//Ask CLR To Create Object From Department Repository
        {
            //this.repository = repository;
            //this.departmentRepository = departmentRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

       
        public IActionResult Index(string searchInput)
        {
            var employees=Enumerable.Empty<Employee>();
            var employeesViewModel = new Collection<EmployeeViewModel>();

            if (string.IsNullOrEmpty(searchInput))
            {
                 employees=unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees= unitOfWork.EmployeeRepository.GetByName(searchInput);
            }
            //Auto Mapping
           var result= mapper.Map<IEnumerable<EmployeeViewModel>>(employees); 





            // employees = repository.GetAll();
            //string Message = "hello World";
            // view dictionary :Transfer Data from Action To View[One Way]
            //you can  call View Dictionary by Three Ways
            //1-View Data : Property Inherted From C  ViewData["Message"] = Message;
         //   ViewData["Message"] = Message + "From View Data";
            
         //   //2-ViewBag   : Property Inherted From Controller - Dynamic
         //ViewBag.Message=Message + "From View Bag";


         //   //3-TempData  : Property Inherted From Controller - Dictionary
         //   TempData["Message"]=Message + "From TempData";
             
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var department= unitOfWork.DepartmentRepository.GetAll();//Extra
            //View Dictionary : 
            //1-ViewData
            ViewData["Department"]= department;


            return View();

        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                //casting employeeviewmodel --> Employee
                // manual mapping
                Employee employee = new Employee()
                //{
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    HireDate = model.HireDate,
                //    IsActive = model.IsActive,
                //    Salary = model.Salary,
                //    WorkFor = model.WorkFor,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,

                //}
            ;
                //AutoMapper you go install the package
                 employee=mapper.Map<Employee>(model);





                var count = unitOfWork.EmployeeRepository.Add(employee);
                if (count > 0)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            else
            {
                var result = unitOfWork.EmployeeRepository.Get(id.Value);
                return View(result);

            }

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var result = unitOfWork.EmployeeRepository.Get(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            var department = unitOfWork.DepartmentRepository.GetAll();//Extra
            ViewData["Department"]=department;
            return View(result);
        }


        [HttpPost]
        public IActionResult Edit(Employee model, int? id)
        {

            if (id is null || !ModelState.IsValid)
            {
                if (id is null)
                {
                    return BadRequest();
                }

                var result = unitOfWork.EmployeeRepository.Get(id.Value);
                return View(result);
            }


            var count = unitOfWork.EmployeeRepository.Update(model);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }

            var result = unitOfWork.EmployeeRepository.Get(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);


        }


        [HttpPost]
        public IActionResult Delete(Employee model, int? id)
        {

            if (id is null || !ModelState.IsValid)
            {
                if (id is null)
                {
                    return BadRequest();
                }

                var result = unitOfWork.EmployeeRepository.Get(id.Value);
                return View(result);
            }


            var count = unitOfWork.EmployeeRepository.Delete(model);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }




    }
}

