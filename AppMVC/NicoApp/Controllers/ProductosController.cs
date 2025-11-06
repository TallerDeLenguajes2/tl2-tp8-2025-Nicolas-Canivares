using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NicoApp.Models;
using TiendaDB;

namespace NicoApp.Controllers;

public class ProductosController : Controller
{
/*  private readonly ILogger<ProductosController> _logger;

    public ProductosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
    } */

    private ProductoRepository productoRepository;

    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }
    [HttpGet]
    public IActionResult Index()
    {
        var listadoProductos = productoRepository.getProductos();
        return View(listadoProductos);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return View();
    }
    public IActionResult Delete()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
