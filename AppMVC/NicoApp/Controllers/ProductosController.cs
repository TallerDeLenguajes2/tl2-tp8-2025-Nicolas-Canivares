using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NicoApp.Models;
using NicoApp.ViewModels;
using MVC.Interfaces;
using MVC.Services;
using TiendaDB;
namespace NicoApp.Controllers;

public class ProductosController : Controller
{

    //private ProductoRepository productoRepository;
    private IProductoRepository _repo;
    private IAuthenticationService _authService;
    public ProductosController(IProductoRepository prodRepo, IAuthenticationService authService)
    {
        //productoRepository = new ProductoRepository();
        _repo = prodRepo;
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        
        var listadoProductos = _repo.getProductos();
        return View(listadoProductos);
    }

    private IActionResult CheckAdminPermissions()
    {
    // 1. No logueado? -> vuelve al login
        if (!_authService.IsAuthenticated())
        {
        return RedirectToAction("Index", "Login");
        }

        // 2. No es Administrador? -> Da Error
        if (!_authService.HasAccessLevel("Administrador"))
        {
        // Llamamos a AccesoDenegado (llama a la vista correspondiente de Productos)
        return RedirectToAction(nameof(AccesoDenegado));
        }
        return null; // Permiso concedido
    }
    public IActionResult AccesoDenegado()
    {
        // El usuario está logueado, pero no tiene el rol suficiente.
        return View();
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

        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }

        var nuevoProducto = new Productos
        {
            Descripcion = productoVM.Descripcion,
            Precio = productoVM.Precio
        };

        _repo.addNewProducto(nuevoProducto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int idProducto)
    {

        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var producto = _repo.getProductosById(idProducto);

        var editProductoVM = new ProductoViewModel
        {
            IdProducto = producto.IdProducto,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio
        };

        return View(editProductoVM);
        //return View(producto);
    }

//
    [HttpPost]
    public IActionResult Edit(ProductoViewModel productoVM)
    {

        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

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

        _repo.updateProducto(productoAEditar.IdProducto,productoAEditar);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int idProducto)
    {
        // Aplicamos el chequeo de seguridad
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        _repo.deleteProductoById(idProducto);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
