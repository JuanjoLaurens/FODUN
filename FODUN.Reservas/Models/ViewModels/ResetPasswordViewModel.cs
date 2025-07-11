using System.ComponentModel.DataAnnotations;

namespace FODUN.Reservas.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Clave")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Clave")]
        [Compare("Password", ErrorMessage = "La nueva clave y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Code { get; set; } 
    }
}