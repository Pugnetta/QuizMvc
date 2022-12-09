using System.ComponentModel.DataAnnotations;

namespace AspNetMvcClass.Models.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Indirizzo Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
