namespace MVC.Interfaces;
using TiendaDB;

public interface IProductoRepository
{
    public bool addNewProducto(Productos producto);
    public bool updateProducto(int idProducto, Productos productos);
    public List<Productos> getProductos();
    public Productos getProductosById(int idProducto);
    public bool deleteProductoById(int idProducto);


}

