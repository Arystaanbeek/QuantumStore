using System.ComponentModel.DataAnnotations;

namespace QuantumStore.Models.ViewModels;

public class LoginViewModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Enter your email")]
    [Display(Name = "Specify the email")]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Enter your password")]
    [Display(Name = "Specify the password")]
    public string Password { get; set; }
}