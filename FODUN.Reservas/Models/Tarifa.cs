using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FODUN.Reservas.Models
{
    public class Tarifa
    {
        [Key]
        public int TarifaId { get; set; }

        [Required(ErrorMessage = "El tipo de regla es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de regla no puede exceder 50 caracteres.")]
        public string TipoRegla { get; set; } = string.Empty; 

        [Display(Name = "Alojamiento")]
        public int? AlojamientoId { get; set; }

        [ForeignKey(nameof(AlojamientoId))]
        public Alojamiento? Alojamiento { get; set; } 

        [Display(Name = "Ubicación")]
        public int? UbicacionId { get; set; }

        [ForeignKey(nameof(UbicacionId))]
        public Ubicacion? Ubicacion { get; set; } 
        [StringLength(200, ErrorMessage = "El nombre específico del alojamiento no puede exceder 200 caracteres.")]
        [Display(Name = "Nombre Alojamiento Específico")]
        public string? NombreAlojamientoEspecifico { get; set; }

        [StringLength(100, ErrorMessage = "El tipo de alojamiento general no puede exceder 100 caracteres.")]
        [Display(Name = "Tipo de Alojamiento General")]
        public string? TipoAlojamientoGeneral { get; set; }

        [Required(ErrorMessage = "El tipo de temporada es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de temporada no puede exceder 50 caracteres.")]
        [Display(Name = "Tipo de Temporada")]
        public string TipoTemporada { get; set; } = string.Empty; 

        [Required(ErrorMessage = "El valor base es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor Base")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor base debe ser mayor que cero.")]
        public decimal ValorBase { get; set; }

        [Required(ErrorMessage = "El mínimo de personas es obligatorio.")]
        [Display(Name = "Mínimo de Personas")]
        [Range(1, int.MaxValue, ErrorMessage = "El mínimo de personas debe ser al menos 1.")]
        public int MinPersonas { get; set; }

        [Required(ErrorMessage = "El máximo de personas es obligatorio.")]
        [Display(Name = "Máximo de Personas")]
        [Range(1, int.MaxValue, ErrorMessage = "El máximo de personas debe ser al menos 1.")]
        public int MaxPersonas { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor por Persona Adicional")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor por persona adicional no puede ser negativo.")]
        public decimal? ValorPorPersonaAdicional { get; set; } 

        [Display(Name = "¿Aplica Lunes a Jueves?")]
        public bool AplicaLunesJueves { get; set; } = false;

        [StringLength(255, ErrorMessage = "El concepto de servicio adicional no puede exceder 255 caracteres.")]
        [Display(Name = "Concepto de Servicio Adicional")]
        public string? ConceptoServicioAdicional { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor de Servicio Adicional")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor de servicio adicional no puede ser negativo.")]
        public decimal? ValorServicioAdicional { get; set; }

        [Display(Name = "Mínimo Acompañantes Servicio")]
        [Range(0, int.MaxValue, ErrorMessage = "El mínimo de acompañantes para servicio debe ser al menos 0.")]
        public int? MinAcompanantesServicio { get; set; } 

        [Display(Name = "Máximo Acompañantes Servicio")]
        [Range(0, int.MaxValue, ErrorMessage = "El máximo de acompañantes para servicio debe ser al menos 0.")]
        public int? MaxAcompanantesServicio { get; set; } 

        [Required(ErrorMessage = "El estado activo es obligatorio.")]
        [Display(Name = "Activa")]
        public bool Activo { get; set; } = true;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [Display(Name = "Última Actualización")]
        public DateTime? FechaActualizacion { get; set; } 

        
    }
}