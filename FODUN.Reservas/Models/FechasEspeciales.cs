using System;
using System.ComponentModel.DataAnnotations;

namespace FODUN.Reservas.Models
{
    public class FechasEspeciales
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Especial")]
        public DateTime Fecha { get; set; }

        [StringLength(255)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; } 
    }
}