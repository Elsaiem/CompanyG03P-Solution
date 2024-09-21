using System.ComponentModel.DataAnnotations;

namespace CompanyG03PL.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Password is requierd")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password Min Length is 5")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is requierd")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password does not Match the Passwod")]
        public string ConfirmPassword { get; set; }


    }
}
