using AutoMapper;
using CompanyG03BLL.Interface;
using CompanyG03DAL.Models;
using CompanyG03PL.Helper;
using CompanyG03PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

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

       
        public async Task<IActionResult> Index(string searchInput)
        {
            //   var employees=Enumerable.Empty<Employee>();
            //   var employeesViewModel = new Collection<EmployeeViewModel>();
            //   employees = mapper.Map<Employee>(employeesViewModel);

            //   if (string.IsNullOrEmpty(searchInput))
            //   {
            //        employees=unitOfWork.EmployeeRepository.GetAll();
            //   }
            //   else
            //   {
            //       employees= unitOfWork.EmployeeRepository.GetByName(searchInput);
            //   }
            //   //Auto Mapping
            //  var result= mapper.Map<IEnumerable<EmployeeViewModel>>(employees); 





            //   // employees = repository.GetAll();
            //   //string Message = "hello World";
            //   // view dictionary :Transfer Data from Action To View[One Way]
            //   //you can  call View Dictionary by Three Ways
            //   //1-View Data : Property Inherted From C  ViewData["Message"] = Message;
            ////   ViewData["Message"] = Message + "From View Data";

            ////   //2-ViewBag   : Property Inherted From Controller - Dynamic
            ////ViewBag.Message=Message + "From View Bag";


            ////   //3-TempData  : Property Inherted From Controller - Dictionary
            ////   TempData["Message"]=Message + "From TempData";

            //   return View(employees);
            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(searchInput))
            {
                employees = await unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees =await unitOfWork.EmployeeRepository.GetByNameAsync(searchInput);
            }

            // Auto Mapping from Employee to EmployeeViewModel
            var employeesViewModel = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeesViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var department= await unitOfWork.DepartmentRepository.GetAllAsync();//Extra
            //View Dictionary : 
            //1-ViewData
            ViewData["Department"]= department;


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSetting.UploadFile(model.Image, "Images");
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



                var count = await unitOfWork.EmployeeRepository.AddAsync(employee);
                if (count > 0)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            else
            {
                var result = await unitOfWork.EmployeeRepository.GetAsync(id.Value);
                return View(result);

            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var result = await unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            var department = await unitOfWork.DepartmentRepository.GetAllAsync();//Extra
            ViewData["Department"]=department;
            var employeeViewModel = mapper.Map<EmployeeViewModel>(result);
            return View(employeeViewModel);
           
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model, [FromRoute] int? id)
        {
            // Check if ID is null, return BadRequest if so
            if (id is null)
            {
                return BadRequest();
            }

            // If ModelState is invalid, return the current view with the existing model
            if (!ModelState.IsValid)
            {
                // Fetch the employee and convert it to EmployeeViewModel to return to the view
                var employeeEntity = await unitOfWork.EmployeeRepository.GetAsync(id.Value);
                var employeeViewModel = mapper.Map<EmployeeViewModel>(employeeEntity);
                return View(employeeViewModel);
            }
            if(model.ImageName is not null)
            {
                DocumentSetting.DeleteFile(model.ImageName, "Images");
            }

            model.ImageName = DocumentSetting.UploadFile(model.Image, "Images");


            // Map the EmployeeViewModel to Employee entity
            var employee = mapper.Map<Employee>(model);

            // Perform the update
            var count = await unitOfWork.EmployeeRepository.UpdateAsync(employee);

            // If update is successful, redirect to the Index page
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            // If update fails, return the view with the current model to show the form again
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }

            var result = await unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);


        }


        [HttpPost]
        public async Task<IActionResult> Delete(Employee model, int? id)
        {

            if (id is null || !ModelState.IsValid)
            {
                if (id is null)
                {
                    return BadRequest();
                }

                var result = await unitOfWork.EmployeeRepository.GetAsync(id.Value);
                return View(result);
            }


            var count =await unitOfWork.EmployeeRepository.DeleteAsync(model);
            if (count > 0)
            {
                DocumentSetting.DeleteFile(model.ImageName, "Images");
                return RedirectToAction(nameof(Index));

            }


            return View(model);
        }




    }
}

