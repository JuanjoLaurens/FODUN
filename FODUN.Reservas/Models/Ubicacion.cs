// Models/Ubicacion.cs
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FODUN.Reservas.Models
{
    public class Ubicacion
    {
        [Key]
        public int UbicacionId { get; set; }

        [Required(ErrorMessage = "El nombre de la ubicación es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la ubicación no puede exceder 100 caracteres.")]
        [Display(Name = "Nombre de la Ubicación")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo de ubicación es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de ubicación no puede exceder 50 caracteres.")]
        [Display(Name = "Tipo de Ubicación")]
        public string TipoUbicacion { get; set; } = string.Empty; 
        [Display(Name = "¿Está Activa?")]
        public bool Activa { get; set; } = true;

        public ICollection<Alojamiento>? Alojamientos { get; set; }

        public ICollection<Tarifa>? Tarifas { get; set; }
        public DateTime FechaCreacion { get; internal set; }
    }
}