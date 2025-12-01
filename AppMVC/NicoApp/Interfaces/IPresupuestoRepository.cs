namespace MVC.Interfaces;
using TiendaDB;

public interface IPresupuestoRepository
{
    public bool addNewPresupuesto(Presupuestos presupuesto);
    public List<Presupuestos> getPresupuestos();
    public Presupuestos getPresupuestosById(int idPresupuesto);
    public bool updatePresupuesto(Presupuestos presupuesto);
    public bool deletePresupuesto(int id);
    public Presupuestos getDetallesPresupuesto(int id);
    public bool agregarDetalle(int idPresupuesto, int idProducto, int cantidad);

}