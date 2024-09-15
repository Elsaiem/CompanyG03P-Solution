using CompanyG03BLL.Interface;
using CompanyG03BLL.Repositories;
using CompanyG03DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyG03PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository repository;

        public DepartmentController(IDepartmentRepository repository)//Ask CLR To Create Object From Department Repository
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = repository.GetAll();

            return View(departments);
        }
        [HttpGet]
        public IActionResult Create() {
        return View();
        
        }
        [HttpPost]
        public IActionResult Create(Department model) {
            if (ModelState.IsValid)
            {
                var count = repository.Add(model);
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
            else{ 
                var result= repository.Get(id.Value);
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

            var result = repository.Get(id.Value);
            if (result == null)
            {
                return NotFound(); 
            }

            return View(result); 
        }

       
        [HttpPost]
        public IActionResult Edit(Department model, int? id)
        {
           
            if (id is null || !ModelState.IsValid)
            {
                if (id is null)
                {
                    return BadRequest(); 
                }

                var result = repository.Get(id.Value);
                return View(result);
            }

           
            var count = repository.Update(model);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index)); 
            }

          
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id) {

            if (id is null)
            {
                return BadRequest();
            }

            var result = repository.Get(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);


        }


        [HttpPost]
        public IActionResult Delete(Department model,int? id)
        {

            if (id is null || !ModelState.IsValid)
            {
                if (id is null)
                {
                    return BadRequest();
                }

                var result = repository.Get(id.Value);
                return View(result);
            }


            var count = repository.Delete(model);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }


    }







    }

