using System;
using System.ComponentModel.DataAnnotations;

namespace FODUN.Reservas.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de documento no puede exceder 50 caracteres.")]
        [Display(Name = "Número de Documento")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder 200 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
        [Display(Name = "Dirección Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Clave")]
        [Compare("Password", ErrorMessage = "La clave y la confirmación de la clave no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "La pregunta secreta es obligatoria.")]
        [StringLength(255, ErrorMessage = "La pregunta secreta no puede exceder 255 caracteres.")]
        [Display(Name = "Pregunta Secreta")]
        public string PreguntaSecreta { get; set; }

        [Required(ErrorMessage = "La respuesta secreta es obligatoria.")]
        [StringLength(255, ErrorMessage = "La respuesta secreta no puede exceder 255 caracteres.")]
        [Display(Name = "Respuesta Secreta")]
        public string RespuestaSecreta { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El celular es obligatorio.")]
        [StringLength(20, ErrorMessage = "El celular no puede exceder 20 caracteres.")]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "La dirección de residencia es obligatoria.")]
        [StringLength(255, ErrorMessage = "La dirección de residencia no puede exceder 255 caracteres.")]
        [Display(Name = "Dirección Residencia")]
        public string DireccionResidencia { get; set; }

        [Required(ErrorMessage = "El barrio es obligatorio.")]
        [StringLength(100, ErrorMessage = "El barrio no puede exceder 100 caracteres.")]
        [Display(Name = "Barrio")]
        public string Barrio { get; set; }

        [Required(ErrorMessage = "El municipio es obligatorio.")]
        [StringLength(100, ErrorMessage = "El municipio no puede exceder 100 caracteres.")]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "El departamento es obligatorio.")]
        [StringLength(100, ErrorMessage = "El departamento no puede exceder 100 caracteres.")]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        [StringLength(20, ErrorMessage = "El teléfono de residencia no puede exceder 20 caracteres.")]
        [Display(Name = "Teléfono Residencia")]
        public string TelefonoResidencia { get; set; }

        [Required(ErrorMessage = "¿Autoriza envío de información al correo? es obligatorio.")]
        [Display(Name = "¿Autoriza el Envío de información al Correo?")]
        public bool AutorizaEnvioInfoCorreo { get; set; }

        [Required(ErrorMessage = "¿Autoriza envío de información al celular? es obligatorio.")]
        [Display(Name = "¿Autoriza el Envío de información al Celular?")]
        public bool AutorizaEnvioInfoCelular { get; set; }
    }
}