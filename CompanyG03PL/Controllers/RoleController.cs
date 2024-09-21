using CompanyG03DAL.Models;
using CompanyG03PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyG03PL.Controllers
{
   [Authorize(Roles ="admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly UserManager<ApllicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApllicationUser> userManager)
        {
            _RoleManager = roleManager;
            _userManager = userManager;
        }
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                //var role = new IdentityRole()
                //{
                //    Name = model.RoleName,
                //};
                var result = await _RoleManager.CreateAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }


            }




            return View(model);
        }
        #endregion


        #region Index
      
        public async Task<IActionResult> Index(string searchInput)
        {
            var roles = Enumerable.Empty<IdentityRole>();

            if (string.IsNullOrEmpty(searchInput))
            {
                roles = await _RoleManager.Roles
                    .Select(R => new IdentityRole()
                    {
                        Id = R.Id,
                        Name = R.Name,

                    }).ToListAsync();
            }
            else
            {
                roles = await _RoleManager.Roles
                    .Where(R => R.Name.ToLower().Contains(searchInput.ToLower()))
                    .Select(R => new IdentityRole
                    {
                        Id = R.Id,
                        Name = R.Name

                    })
                    .ToListAsync();
            }



            return View(roles);
        }

        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            else
            {
                var rolefromDb = await _RoleManager.FindByIdAsync(id);
                if (rolefromDb == null)
                    return NotFound();

                //var roles = new RoleViewModel()
                //{
                //    Id = rolefromDb.Id,
                //    RoleName = rolefromDb.Name
                //};
                return View(ViewName, rolefromDb);



            }

        }

        #endregion
        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");

        }


        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model, [FromRoute] string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var roleFromdb = await _RoleManager.FindByIdAsync(id);
                if (roleFromdb == null)
                    return NotFound();
                roleFromdb.Name = model.Name;



                var result = await _RoleManager.UpdateAsync(roleFromdb);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));


            }


            // If update fails, return the view with the current model to show the form again
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {


            return await Details(id, "Delete");


        }


        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model, string id)
        {

            if (id is null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var roleFromdb = await _RoleManager.FindByIdAsync(id);
                if (roleFromdb == null)
                    return NotFound();
                roleFromdb.Name = model.Name;


                var result = await _RoleManager.DeleteAsync(roleFromdb);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));


            }


            // If update fails, return the view with the current model to show the form again
            return View(model);


        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string RoleId)
        {
            var result = await _RoleManager.FindByIdAsync(RoleId);
            if (result is null)
            {
                return NotFound();
            }
            ViewData["RoleId"]=RoleId;
            var usersInRole = new List<UserInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName

                };
                if (await _userManager.IsInRoleAsync(user, result.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);

            }


            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string RoleId, List<UserInRoleViewModel> users)
        {
            var result = await _RoleManager.FindByIdAsync(RoleId);
            if (result is null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                foreach (var user in users) {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null) {
                        if (user.IsSelected &&!await _userManager.IsInRoleAsync(appUser,result.Name))
                        {
                         await  _userManager.AddToRoleAsync(appUser,result.Name);

                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, result.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, result.Name);

                        }


                    }
                    

                }
                return RedirectToAction("Edit", new {id=RoleId});

            }
            return View(users);



        }

    }
}
