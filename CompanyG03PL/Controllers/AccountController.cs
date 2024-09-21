using CompanyG03PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using CompanyG03PL.Models;
using Microsoft.AspNetCore.Mvc;
using CompanyG03DAL.Models;

namespace CompanyG03PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApllicationUser> _userManager; // Fixed semicolon

		// Constructor
		public AccountController(UserManager<ApllicationUser> userManager) // Fixed parameter name and assignment
		{
			_userManager = userManager;
		}

		// GET: SignUp
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		// POST: SignUp
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
			  var result=await _userManager.FindByNameAsync(model.UserName);  
			
				if(result is null)
				{
				 	result= await _userManager.FindByEmailAsync(model.Email);
					//mapping
					if (result is null)
					{
						result = new ApllicationUser()
						{
							UserName = model.UserName,
							FirstName = model.FirstName,
							LastName = model.LastName,
							Email = model.Email,
							IsAgree = model.IsAgree,


						};
						var user = await _userManager.CreateAsync(result, model.Password);
						if (user.Succeeded)
						{
							return RedirectToAction("SignIn");
						}
						foreach (var error in user.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}
					ModelState.AddModelError(string.Empty, "Email is Aleady Exist");

				}


				ModelState.AddModelError(string.Empty, "username is Aleady Exist");
			
			
			}
			

			return View(model); // Return the model to show validation errors if needed
		}
	}
}
