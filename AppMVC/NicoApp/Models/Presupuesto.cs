namespace TiendaDB;

public class Presupuestos
{
    private int idPresupuesto;
    private string nombreDestinatario;

    private DateTime FechaCreacion;

    private List<PresupuestoDetalle> detalle;

    public Presupuestos()
    {
    }
    public Presupuestos(int idPresupuesto, string nombreDestinatario, DateTime fechaCreacion, List<PresupuestoDetalle> detalle)
    {
        this.IdPresupuesto = idPresupuesto;
        this.NombreDestinatario = nombreDestinatario;
        FechaCreacion1 = fechaCreacion;
        this.Detalle = detalle;
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion1 { get => FechaCreacion; set => FechaCreacion = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }
}
