using System.ComponentModel.DataAnnotations;

namespace boty_asp.ViewModels;

public class LoginViewModel {
    
    [Required(ErrorMessage = "Uživatelské jméno je povinné")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Heslo je povinné")]
    public string Password { get; set; }
}