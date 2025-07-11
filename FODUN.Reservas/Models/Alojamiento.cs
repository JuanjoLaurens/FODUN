using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FODUN.Reservas.Models
{
    public class Alojamiento
    {
        [Key]
        public int AlojamientoId { get; set; }

        [Required(ErrorMessage = "El nombre del alojamiento es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        [Display(Name = "Nombre del Alojamiento")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo de alojamiento es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo no puede exceder 50 caracteres.")]
        [Display(Name = "Tipo de Alojamiento")]
        public string TipoAlojamiento { get; set; }

        [Required(ErrorMessage = "La capacidad máxima de personas es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad máxima debe ser al menos 1.")]
        [Display(Name = "Capacidad Máxima de Personas")]
        public int CapacidadMaximaPersonas { get; set; }

        [Display(Name = "Camas Dobles")]
        public int CamasDobles { get; set; }

        [Display(Name = "Camas Sencillas")]
        public int CamasSencillas { get; set; }

        [Display(Name = "Camarotes")]
        public int Camarotes { get; set; }

        [Display(Name = "¿Tiene Baño Privado?")]
        public bool TieneBanoPrivado { get; set; } = false;

        [Display(Name = "¿Tiene Nevera?")]
        public bool TieneNevera { get; set; } = false;

        [Display(Name = "¿Tiene Televisor?")]
        public bool TieneTelevisor { get; set; } = false;

        [Display(Name = "¿Tiene Cocineta Equipada?")]
        public bool TieneCocinetaEquipada { get; set; } = false;

        [Display(Name = "¿Tiene Sala de Estar?")]
        public bool TieneSalaEstar { get; set; } = false;

        [Display(Name = "¿Tiene Terraza/Balcón?")]
        public bool TieneTerraza { get; set; } = false;

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Tarifa Día Ordinario")]
        public decimal? TarifaDiaOrdinario { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Tarifa Día Especial")]
        public decimal? TarifaDiaEspecial { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }


        [Display(Name = "Ubicación")]
        [Required(ErrorMessage = "Debe seleccionar una ubicación para el alojamiento.")]
        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        public Ubicacion? Ubicacion { get; set; }


        public ICollection<Tarifa> Tarifas { get; set; }
        public ICollection<Reserva> Reservas { get; set; }

        public Alojamiento()
        {
            Tarifas = new HashSet<Tarifa>();
            Reservas = new HashSet<Reserva>();
        }
    }
}