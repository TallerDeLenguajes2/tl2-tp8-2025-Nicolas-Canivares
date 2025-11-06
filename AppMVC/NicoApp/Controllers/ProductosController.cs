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
    [HttpGet]
    public IActionResult Create()
    {
        var producto = new Productos();
        return View(producto);
    }
    [HttpPost]
    public IActionResult Create(Productos producto)
    {
        productoRepository.addNewProducto(producto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int idProducto)
    {
        Productos producto = productoRepository.getProductosById(idProducto);
        return View(producto);
    }
    [HttpPost]
    public IActionResult Edit(Productos producto)
    {
        productoRepository.updateProducto(producto.IdProducto,producto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int idProducto)
    {
        productoRepository.deleteProductoById(idProducto);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
