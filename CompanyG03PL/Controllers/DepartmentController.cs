using CompanyG03BLL.Interface;
using CompanyG03BLL.Repositories;
using CompanyG03DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyG03PL.Controllers
{
    [Authorize]
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
            var departments = await repository.GetAllAsync();

            return  View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var count = await repository.AddAsync(model);
                if (count > 0)
                {

                    return  RedirectToAction(nameof(Index));
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
                var result = await repository.GetAsync(id.Value);
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

            var result = await repository.GetAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Department model, int? id)
        {

            if (id is null || !ModelState.IsValid)
            {
                if (id is null)
                {
                    return BadRequest();
                }

                var result = await repository.GetAsync(id.Value);
                return View(result);
            }


            var count = await repository.UpdateAsync(model);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }

            var result = await repository.GetAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);


        }


        [HttpPost]
        public async Task<IActionResult> Delete(Department model, int? id)
        {

            if (id is null || !ModelState.IsValid)
            {
                if (id is null)
                {
                    return BadRequest();
                }

                var result = await repository.GetAsync(id.Value);
                return View(result);
            }


            var count = await repository.DeleteAsync(model);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }


    }







}








