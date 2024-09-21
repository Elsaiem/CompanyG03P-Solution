using System.ComponentModel.DataAnnotations;

namespace CompanyG03PL.ViewModels
{
	public class SignInViewModel
	{

		[Required(ErrorMessage = "Email is requierd")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is requierd")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Password Min Length is 5")]
		public string Password { get; set; }

		public bool RememberMe { get; set; }

	}
}
