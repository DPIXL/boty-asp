using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boty_asp.ViewModels;

public class RegisterViewModel {
    
    [Required(ErrorMessage = "Uživatelské jméno je povinné")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Heslo je povinné")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Potvrďte heslo")]
    [Compare("Password", ErrorMessage = "Hesla se neshodují")]
    public string ConfirmPassword { get; set; }
}