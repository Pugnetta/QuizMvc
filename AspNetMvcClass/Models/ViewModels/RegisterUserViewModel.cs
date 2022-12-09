using System.ComponentModel.DataAnnotations;

namespace AspNetMvcClass.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name = "Indirizzo Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Conferma Password")]
        [Compare(nameof(Password), ErrorMessage ="Password e Conferma Password non corrispondono")]
        public string ConfirmPassword { get; set; }
    }
}
