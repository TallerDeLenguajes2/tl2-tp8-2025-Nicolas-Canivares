using System.ComponentModel.DataAnnotations;
namespace NicoApp.ViewModels;

public class PresupuestoViewModel
{
    public int IdPresupuesto { get; set; }

    [Display(Name = "Nombre o Email del Destinatario")]
    [Required(ErrorMessage ="El nombre del destinatario es obligatorio")]
    public string NombreDestinatario { get; set; }

    [Display(Name = "Fecha de Creacion")]
    [Required(ErrorMessage = "La fecha de creacion es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaCreacion { get; set; }
}