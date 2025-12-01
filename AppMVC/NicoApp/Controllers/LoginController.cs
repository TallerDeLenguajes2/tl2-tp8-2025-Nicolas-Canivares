using Microsoft.AspNetCore.Mvc;
using MVC.Interfaces;
using MVC.Services;
using NicoApp.ViewModels;
namespace NicoApp.Controllers;

public class LoginController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    //[HttpGet] Muestra la vista de login
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }
    //Procesa el Login
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            model.ErrorMessage = "Debe ingresar usuario y contraseña.";
            return View("Index",model);
        }
        if (_authenticationService.Login(model.Username,model.Password))
        {
            return RedirectToAction("Index","Home");
        }
        model.ErrorMessage = "Credenciales Invalidas.";
        return View("Index",model);
    }

    //[HttpGet] Cierra la sesion
    public IActionResult Logout()
    {
        _authenticationService.Logout();
        return RedirectToAction("Index");
    }

    
}