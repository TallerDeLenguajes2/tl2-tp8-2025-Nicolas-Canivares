namespace TiendaDB;

public class Productos
{
    private int idProducto;
    private string descripcion;
    private decimal precio;

    public Productos()
    {

    }
    public Productos(int idProducto, string descripcion, decimal precio)
    {
        this.IdProducto = idProducto;
        this.Descripcion = descripcion;
        this.Precio = precio;
    }

    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public decimal Precio { get => precio; set => precio = value; }
}
