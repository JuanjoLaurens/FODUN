using Microsoft.AspNetCore.Identity; 


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FODUN.Reservas.Models
{
    public class ApplicationUser : IdentityUser<int> 
    {


        [Required(ErrorMessage = "El número de documento es requerido.")]
        [StringLength(50, ErrorMessage = "El documento no puede exceder 50 caracteres.")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El nombre completo es requerido.")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres.")]
        public string NombreCompleto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }

        [StringLength(20, ErrorMessage = "El celular no puede exceder 20 caracteres.")]
        public string? Celular { get; set; }


        [StringLength(100, ErrorMessage = "El departamento no puede exceder 100 caracteres.")]
        public string Departamento { get; set; }

        [StringLength(100, ErrorMessage = "El municipio no puede exceder 100 caracteres.")]
        public string Municipio { get; set; }

        [StringLength(100, ErrorMessage = "El barrio no puede exceder 100 caracteres.")]
        public string? Barrio { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede exceder 255 caracteres.")]
        public string DireccionResidencia { get; set; }

        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres.")]
        public string TelefonoResidencia { get; set; }

        [StringLength(255, ErrorMessage = "La pregunta secreta no puede exceder 255 caracteres.")]
        public string PreguntaSecreta { get; set; }

        [StringLength(255, ErrorMessage = "La respuesta secreta no puede exceder 255 caracteres.")]
        public string RespuestaSecreta { get; set; } 

        public bool AutorizaEnvioInfoCorreo { get; set; } = false;
        public bool AutorizaEnvioInfoCelular { get; set; } = false;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        // Propiedad de navegación para las reservas relacionadas
        public ICollection<Reserva> Reservas { get; set; }

    }
}