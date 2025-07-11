using System.ComponentModel.DataAnnotations;

namespace FODUN.Reservas.ViewModels
{
    public class ReservaCreateViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar un alojamiento.")]
        [Display(Name = "Alojamiento")]
        public int AlojamientoId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es requerida.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El número de personas es requerido.")]
        [Range(1, 20, ErrorMessage = "El número de personas debe estar entre 1 y 20.")]
        [Display(Name = "Número de Personas")]
        public int NumeroPersonas { get; set; }

        [Display(Name = "Notas Adicionales")]
        [StringLength(500, ErrorMessage = "Las notas no pueden exceder 500 caracteres.")]
        public string? Notas { get; set; }

        // Propiedades para mostrar información al usuario (no directamente para entrada)
        [Display(Name = "Valor Total Estimado")]
        public decimal ValorTotalEstimado { get; set; }
        public string? AlojamientoNombre { get; set; }
        public string? UbicacionNombre { get; set; }

        // Propiedades adicionales si el cálculo de tarifa requiere más info desde el formulario
        // Opcional: bool IncluyeLavanderia, int NumeroAcompanantesVisitaDia (si se capturan en UI)
        [Display(Name = "¿Incluye Servicio de Lavandería?")]
        public bool IncluyeLavanderia { get; set; } = false;

        [Display(Name = "Número de Acompañantes de Visita (Día)")]
        [Range(0, 10, ErrorMessage = "Debe ser entre 0 y 10 acompañantes.")]
        public int NumeroAcompanantesVisitaDia { get; set; } = 0;
    }
}