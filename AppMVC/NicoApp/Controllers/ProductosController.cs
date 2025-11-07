using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NicoApp.Models;
using NicoApp.ViewModels;
using TiendaDB;
namespace NicoApp.Controllers;

public class ProductosController : Controller
{

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
        //var producto = new Productos();
        return View(new ProductoViewModel());
    }

    [HttpPost]
    public IActionResult Create(ProductoViewModel productoVM)
    {

        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }

        var nuevoProducto = new Productos
        {
            Descripcion = productoVM.Descripcion,
            Precio = productoVM.Precio
        };

        productoRepository.addNewProducto(nuevoProducto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int idProducto)
    {

        var producto = productoRepository.getProductosById(idProducto);

        var editProductoVM = new ProductoViewModel
        {
            IdProducto = producto.IdProducto,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio
        };

        return View(editProductoVM);
        //return View(producto);
    }
    [HttpPost]
    public IActionResult Edit(ProductoViewModel productoVM)
    {
        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }

        var productoAEditar = new Productos
        {
            IdProducto = productoVM.IdProducto,
            Descripcion = productoVM.Descripcion,
            Precio = productoVM.Precio
        };

        productoRepository.updateProducto(productoAEditar.IdProducto,productoAEditar);
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
