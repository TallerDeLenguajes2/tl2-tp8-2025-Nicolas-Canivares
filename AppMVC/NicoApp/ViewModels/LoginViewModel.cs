using System.ComponentModel.DataAnnotations;
namespace NicoApp.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Nombre de Usuario")]
    [Required(ErrorMessage = "El Nombre de Usuario es obligatorio.")]
    public string Username {get;set;}

    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "La Contraseña es obligatoria."),DataType(DataType.Password)]
    public string Password {get;set;}
    
    public string ErrorMessage {get;set;}

    public bool IsAuthenticated {get;set;}
}