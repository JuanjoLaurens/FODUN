using System.ComponentModel.DataAnnotations;

namespace FODUN.Reservas.Models.DTOs
{
 
    public class AlojamientoDisponibleDto
    {
        [Key] 
        public int AlojamientoID { get; set; }
        public string? NombreAlojamiento { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoAlojamiento { get; set; }
        public string? NombreUbicacion { get; set; }
        public int CapacidadMaxima { get; set; }
        public int? NumeroHabitacionesDisponibles { get; set; }
        public decimal TarifaDiaOrdinario { get; set; }
        public decimal TarifaDiaEspecial { get; set; }

    }
}