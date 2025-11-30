using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using NicoApp.Models;
using NicoApp.ViewModels;

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

    public IActionResult Create()
    {
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

        if (!ModelState.IsValid)
        {
            return View(presupuestoVM);
        }
        var nuevoPresupuesto = new Presupuestos
        {
            NombreDestinatario = presupuestoVM.NombreDestinatario,
            FechaCreacion1 = presupuestoVM.FechaCreacion
        };

        presupuestosRepository.addNewPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int idPresupuesto)
    {

        var presupuesto = presupuestosRepository.getPresupuestosById(idPresupuesto);

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

        presupuestosRepository.updatePresupuesto(presupuestoAEditar);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int idPresupuesto)
    {
        presupuestosRepository.deletePresupuesto(idPresupuesto);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
