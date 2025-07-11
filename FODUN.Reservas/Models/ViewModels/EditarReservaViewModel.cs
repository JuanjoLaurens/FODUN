using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace FODUN.Reservas.ViewModels
{
    public class EditarReservaViewModel
    {
        public int ReservaId { get; set; }

        [Required]
        public int AlojamientoId { get; set; }

        [Display(Name = "Nombre del Alojamiento")]
        public string? AlojamientoNombre { get; set; } 

        [Display(Name = "Ubicación")]
        public string? UbicacionNombre { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        [DateGreaterThan("FechaInicio", ErrorMessage = "La fecha de fin debe ser posterior a la fecha de inicio.")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El número de personas es obligatorio.")]
        [Range(1, 20, ErrorMessage = "El número de personas debe estar entre 1 y 20.")]
        [Display(Name = "Número de Personas")]
        public int NumeroPersonas { get; set; }

        [Display(Name = "Número de Unidades Reservadas")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de unidades debe ser al menos 1.")]
        public int NumeroUnidadesReservadas { get; set; } = 1; // Necesaria para CalcularTarifaTotalReserva

        [Display(Name = "¿Incluye Lavandería?")]
        public bool IncluyeLavanderia { get; set; } = false;

        [Range(0, 10, ErrorMessage = "El número de acompañantes en visita de día debe estar entre 0 y 10.")]
        [Display(Name = "Número de Acompañantes (Visita de Día)")]
        public int NumeroAcompanantesVisitaDia { get; set; } = 0;

        [StringLength(500, ErrorMessage = "Las notas no pueden exceder 500 caracteres.")]
        [Display(Name = "Notas Adicionales")]
        public string? Notas { get; set; } 

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Valor Total Estimado")]
        [DataType(DataType.Currency)]
        public decimal ValorTotalEstimado { get; set; } 

        [Required(ErrorMessage = "El estado de la reserva es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder 50 caracteres.")]
        [Display(Name = "Estado de la Reserva")]
        public string EstadoReserva { get; set; } = "Pendiente";

        public int ApplicationUserId { get; internal set; }

        
        public class DateGreaterThanAttribute : ValidationAttribute
        {
            private readonly string _comparisonProperty;

            public DateGreaterThanAttribute(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var currentValue = (DateTime)value;

                var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
                if (property == null)
                    throw new ArgumentException("Property with this name not found.");

                var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

                if (currentValue <= comparisonValue)
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }
    }
}