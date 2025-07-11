using System.ComponentModel.DataAnnotations;

namespace FODUN.Reservas.ViewModels
{
    public class AlojamientoResultadoBusquedaViewModel
    {
        [Display(Name = "ID Alojamiento")]
        public int AlojamientoID { get; set; }

        [Display(Name = "Nombre Alojamiento")]
        public string NombreAlojamiento { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Tipo de Alojamiento")]
        public string TipoAlojamiento { get; set; }

        [Display(Name = "Ubicación")]
        public string NombreUbicacion { get; set; }

        [Display(Name = "Capacidad Máxima")]
        public int CapacidadMaxima { get; set; }

        [Display(Name = "Habitaciones Disponibles")]
        public int NumeroHabitacionesDisponibles { get; set; }

        [Display(Name = "Tarifa Día Ordinario")]
        public decimal? TarifaDiaOrdinario { get; set; } 

        [Display(Name = "Tarifa Día Especial")]
        public decimal? TarifaDiaEspecial { get; set; } 
    }
}