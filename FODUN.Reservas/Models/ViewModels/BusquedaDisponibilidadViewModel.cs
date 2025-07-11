
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FODUN.Reservas.Models; 
using System.Linq; 

namespace FODUN.Reservas.ViewModels
{
    public class BusquedaDisponibilidadViewModel
    {
        [Required(ErrorMessage = "La fecha de llegada es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Llegada")]
        public DateTime FechaLlegada { get; set; } = DateTime.Today; // Valor por defecto

        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Salida")]
        public DateTime FechaSalida { get; set; } = DateTime.Today.AddDays(1); // Valor por defecto

        [Required(ErrorMessage = "El número de personas es obligatorio.")]
        [Range(1, 20, ErrorMessage = "El número de personas debe ser entre 1 y 20.")]
        [Display(Name = "Número de Personas")]
        public int NumeroPersonas { get; set; } = 1; 


        // Propiedad para los resultados de la búsqueda
        public IEnumerable<Alojamiento> ResultadosBusqueda { get; set; } = new List<Alojamiento>();

        
        public bool SearchAttempted { get; set; } = false; 
        public bool IsSearchValid { get; set; } = false; 
    }
}