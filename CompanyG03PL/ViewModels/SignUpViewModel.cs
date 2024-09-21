using System.ComponentModel.DataAnnotations;

namespace CompanyG03PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="UserName is requierd")]
		public string UserName {  get; set; }
		
		[Required(ErrorMessage = "First Name is requierd")]
		public string FirstName {  get; set; }
		
		[Required(ErrorMessage = "Last Name is requierd")]

		public string LastName { get; set; }
		
		[Required(ErrorMessage = "Email is requierd")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "Password is requierd")]
		[DataType(DataType.Password)]
		[MinLength(5,ErrorMessage ="Password Min Length is 5" )]
		public string Password { get; set; }
		
		[Required(ErrorMessage = "Confirm Password is requierd")]
		[Compare(nameof(Password),ErrorMessage ="The password does not Match the Passwod")]
		public string ConfirmPassword { get; set; }
		
		[Required(ErrorMessage = "Is Agree is requierd")]

		public bool IsAgree { get; set; }






	}
}
