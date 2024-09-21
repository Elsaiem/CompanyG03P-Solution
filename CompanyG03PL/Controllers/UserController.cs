using AutoMapper;
using CompanyG03BLL;
using CompanyG03BLL.Interface;
using CompanyG03DAL.Models;
using CompanyG03PL.Helper;
using CompanyG03PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyG03PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApllicationUser> _UserManager; 



        public UserController(UserManager<ApllicationUser> userManager)
        {
            _UserManager = userManager;
        }


        #region Index
        public async Task<IActionResult> Index(string searchInput)
        {
            var users = Enumerable.Empty<UserViewModel>();

            if (string.IsNullOrEmpty(searchInput))
            {
                users = await _UserManager.Users
                    .Select(U => new UserViewModel
                    {
                        Id = U.Id,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        Email = U.Email
                    })
                    .ToListAsync();
            }
            else
            {
                users = await _UserManager.Users
                    .Where(U => U.Email.ToLower().Contains(searchInput.ToLower()))
                    .Select(U => new UserViewModel
                    {
                        Id = U.Id,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        Email = U.Email
                    })
                    .ToListAsync();
            }

            foreach (var user in users)
            {
                user.Roles = await _UserManager.GetRolesAsync(await _UserManager.FindByIdAsync(user.Id));
            }

            return View(users);
        }

        #endregion
        #region Details
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            else
            {
                var userfromDb = await _UserManager.FindByIdAsync(id);
                if (userfromDb == null)
                    return NotFound();

                var user = new UserViewModel()
                {
                    Id = userfromDb.Id,
                    FirstName = userfromDb.FirstName,
                    LastName = userfromDb.LastName,
                    Email = userfromDb.Email,
                    Roles = _UserManager.GetRolesAsync(userfromDb).Result
                };
                return View(ViewName, user);



            }

        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");

        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
              var userFromdb=await _UserManager.FindByIdAsync(id);
             if(userFromdb == null)
                    return NotFound();
             userFromdb.FirstName=model.FirstName;
             userFromdb.LastName=model.LastName;
             userFromdb.Email=model.Email;
          
             var result= await  _UserManager.UpdateAsync(userFromdb);
              if(result.Succeeded)
                return RedirectToAction(nameof(Index));
                

            }


            // If update fails, return the view with the current model to show the form again
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {

          
            return await Details(id,"Delete");


        }


        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel model, string id)
        {

            if (id is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var userFromdb = await _UserManager.FindByIdAsync(id);
                if (userFromdb == null)
                    return NotFound();
                userFromdb.FirstName = model.FirstName;
                userFromdb.LastName = model.LastName;
                userFromdb.Email = model.Email;

                var result = await _UserManager.DeleteAsync(userFromdb);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));


            }


            // If update fails, return the view with the current model to show the form again
            return View(model);

        }
        }




    }

