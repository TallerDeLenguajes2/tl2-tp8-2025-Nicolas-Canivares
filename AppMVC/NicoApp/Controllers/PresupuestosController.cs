using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NicoApp.Models;

namespace TiendaDB;

public class PresupuestosController : Controller
{

    private PresupuestosRepository presupuestosRepository;

    public PresupuestosController()
    {
        presupuestosRepository = new PresupuestosRepository();
    }

    public IActionResult Index()
    {
        return View(presupuestosRepository.getPresupuestos());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
