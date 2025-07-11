using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace FODUN.Reservas.ViewModels
{
    public class CrearReservaViewModel
    {
        [Required(ErrorMessage = "El alojamiento es obligatorio.")]
        [Display(Name = "Alojamiento")]
        public int AlojamientoId { get; set; }


        [Display(Name = "Nombre Alojamiento")]
        public string? AlojamientoNombre { get; set; }

        
        [Display(Name = "Nombre Ubicación")]
        public string? UbicacionNombre { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "El número de personas es obligatorio.")]
        [Range(1, 20, ErrorMessage = "El número de personas debe ser entre 1 y 20.")]
        [Display(Name = "Número de Personas")]
        public int NumeroPersonas { get; set; } = 1;

        [Display(Name = "Número de Unidades a Reservar")]
        [Range(1, 10, ErrorMessage = "El número de unidades debe ser entre 1 y 10.")]
        public int NumeroUnidadesReservadas { get; set; } = 1;

        [Display(Name = "¿Incluye Lavandería?")]
        public bool IncluyeLavanderia { get; set; } = false;

        [Display(Name = "Número de Acompañantes Visita Día")]
        [Range(0, 10, ErrorMessage = "El número de acompañantes debe ser entre 0 y 10.")]
        public int NumeroAcompanantesVisitaDia { get; set; } = 0;

        [Required(ErrorMessage = "El valor total es obligatorio.")]
        [Display(Name = "Valor Total de la Reserva")]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal ValorTotal { get; set; } = 0;

        [Required(ErrorMessage = "El valor total estimado es obligatorio.")]
        [Display(Name = "Valor Total Estimado")]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal ValorTotalEstimado { get; set; } = 0;

        [StringLength(500, ErrorMessage = "Las notas no pueden exceder 500 caracteres.")]
        [Display(Name = "Notas/Observaciones")]
        public string? Notas { get; set; }

        public int ApplicationUserId { get; internal set; }

    }
}