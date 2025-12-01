using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using NicoApp.Models;
using NicoApp.ViewModels;
using MVC.Interfaces;
using TiendaDB;
namespace NicoApp.Controllers;

public class PresupuestosController : Controller
{

    //private PresupuestosRepository presupuestosRepository;
    private IPresupuestoRepository _repo;
    private IProductoRepository _productoRepo;
    private IAuthenticationService _authService;

    public PresupuestosController(IPresupuestoRepository repo,
IProductoRepository prodRepo, IAuthenticationService authService)
    {
        _repo = repo;
        _productoRepo = prodRepo;
        _authService = authService;
    }

    public IActionResult Index()
    {

        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso que necesite validar
        if (_authService.HasAccessLevel("Administrador") ||
            _authService.HasAccessLevel("Cliente") )
        {
        //si es es valido entra sino vuelve a login

            return View(_repo.getPresupuestos());
        }else
        {
            return RedirectToAction("Index", "Login");
        }
    }

    public IActionResult AccesoDenegado()
    {
        // El usuario está logueado, pero no tiene el rol suficiente.
        return View();
    }

    public IActionResult Create()
    {

        // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction(nameof(AccesoDenegado));
        }
        //var producto = new Productos();
        var presupuestoNuevo = new PresupuestoViewModel
        {
            FechaCreacion = DateTime.Today
        };

        return View(presupuestoNuevo);
    }

    [HttpPost]
    public IActionResult Create(PresupuestoViewModel presupuestoVM)
    {
         // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction(nameof(AccesoDenegado));
        }

        if (!ModelState.IsValid)
        {
            return View(presupuestoVM);
        }
        var nuevoPresupuesto = new Presupuestos
        {
            NombreDestinatario = presupuestoVM.NombreDestinatario,
            FechaCreacion1 = presupuestoVM.FechaCreacion
        };

        _repo.addNewPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int idPresupuesto)
    {
         // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction(nameof(AccesoDenegado));
        }

        var presupuesto = _repo.getPresupuestosById(idPresupuesto);

        var editPresupuestoVM = new PresupuestoViewModel
        {
            IdPresupuesto = presupuesto.IdPresupuesto,
            NombreDestinatario = presupuesto.NombreDestinatario,
            FechaCreacion = presupuesto.FechaCreacion1
        };

        return View(editPresupuestoVM);
    }

    [HttpPost]
    public IActionResult Edit(PresupuestoViewModel presupuestoVM)
    {
         // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction(nameof(AccesoDenegado));
        }
        
        if (!ModelState.IsValid)
        {
            return View(presupuestoVM);
        }

        var presupuestoAEditar =  new Presupuestos
        {
            IdPresupuesto = presupuestoVM.IdPresupuesto,
            NombreDestinatario = presupuestoVM.NombreDestinatario,
            FechaCreacion1 = presupuestoVM.FechaCreacion
        };

        _repo.updatePresupuesto(presupuestoAEditar);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int idPresupuesto)
    {
         // Comprobación de si está logueado
        if (!_authService.IsAuthenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Verifica Nivel de acceso
        if (!_authService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction(nameof(AccesoDenegado));
        }
        _repo.deletePresupuesto(idPresupuesto);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
