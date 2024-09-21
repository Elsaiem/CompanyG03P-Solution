using System.ComponentModel.DataAnnotations;

namespace CompanyG03PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is requierd")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }


    }
}
