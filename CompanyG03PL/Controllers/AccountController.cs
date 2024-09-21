using CompanyG03PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using CompanyG03PL.Models;
using Microsoft.AspNetCore.Mvc;
using CompanyG03DAL.Models;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using CompanyG03PL.Helper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CompanyG03PL.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
	{
		private readonly UserManager<ApllicationUser> _userManager; // Fixed semicolon
        private readonly SignInManager<ApllicationUser> _signInManager;

        // Constructor
        public AccountController(UserManager<ApllicationUser> userManager,SignInManager<ApllicationUser> signInManager) // Fixed parameter name and assignment
		{
			_userManager = userManager;
            _signInManager = signInManager;
        }

		// GET: SignUp
		#region Sign Up
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
				var result = await _userManager.FindByNameAsync(model.UserName);

				if (result is null)
				{
					result = await _userManager.FindByEmailAsync(model.Email);
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
		#endregion
		#region Sign In
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    if (await _userManager.CheckPasswordAsync(user, input.Password))
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");

                    }
                }
                ModelState.AddModelError("", "Incorrect Email Or Password");
                return View(input);
            }
            return View(input);
        }



		#endregion
		#region Sign Out
		public new async Task<IActionResult> SignOut()
		{
		     await	_signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));

		}













		#endregion
		#region Forget Password
		[HttpGet]
		public IActionResult ForgetPaswword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid) {
		        
				var result=await _userManager.FindByEmailAsync(model.Email);
				if (result is not null) {
					//Create Token 
				    var token=await	_userManager.GeneratePasswordResetTokenAsync(result);
					
					
					
					
					//Create Password URL
					var url= Url.Action("ResetPassword", "Account", new {email=model.Email,token=token },Request.Scheme);
					
					//Create Email
					CompanyG03DAL.Models.Email email = new CompanyG03DAL.Models.Email()
					{
						To=model.Email,
						Subject="Reset Password",
						Body=url,
					};
					//send Email
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox)); 



				
				
				}
				ModelState.AddModelError(string.Empty, "Invalid Operation Please try again !!");
			
			}
            return View(model);
        }

		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}




		#endregion
		#region Reset Password
		[HttpGet]
		public	IActionResult ResetPassword(string email,string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
        [HttpPost]
        public	async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

				
				var user=await _userManager.FindByEmailAsync(email);
				if (user != null)
				{
				var result=await	_userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded) {

						return RedirectToAction(nameof(SignIn));
					
					}
				}





            }
           



            return View(model);
		}






		#endregion

		public IActionResult AccessDenied()
		{
			return View();
		}









	}



















}
