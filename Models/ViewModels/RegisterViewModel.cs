using System.ComponentModel.DataAnnotations;

namespace QuantumStore.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Enter a nickname")]
    public string Name { get; set; }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Enter your email")]
    [Display(Name = "Specify the email")]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Enter your password")]
    [Display(Name = "Specify the password")]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare(nameof(Password),ErrorMessage = "Passwords don't match")]
    [Display(Name = "Repeat the password")]
    public string PasswordConfirm { get; set; }
}