using FODUN.Reservas.Models;
using FODUN.Reservas.Models.ViewModels;
using FODUN.Reservas.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FODUN.Reservas.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }


        // =========== REGISTRO ================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NumeroDocumento = model.NumeroDocumento,
                    NombreCompleto = model.NombreCompleto,
                    PreguntaSecreta = model.PreguntaSecreta,
                    RespuestaSecreta = model.RespuestaSecreta,
                    FechaNacimiento = model.FechaNacimiento,
                    Celular = model.Celular,
                    DireccionResidencia = model.DireccionResidencia,
                    Barrio = model.Barrio,
                    Municipio = model.Municipio,
                    Departamento = model.Departamento,
                    TelefonoResidencia = model.TelefonoResidencia,
                    AutorizaEnvioInfoCorreo = model.AutorizaEnvioInfoCorreo,
                    AutorizaEnvioInfoCelular = model.AutorizaEnvioInfoCelular,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                    EmailConfirmed = true 
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "¡Registro exitoso! Por favor, inicia sesión.";
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // ============== LOGIN =================


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                    return RedirectToLocal(returnUrl);

                if (result.RequiresTwoFactor)
                    ModelState.AddModelError(string.Empty, "Se requiere autenticación de dos factores.");
                else if (result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Cuenta bloqueada. Inténtalo de nuevo más tarde.");
                else
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión inválido.");
            }

            return View(model);
        }

        // ============== LOGOUT ===============

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // ======= FORMULARIO OLVIDO CLAVE =====
     
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("/enviar-recuperacion")]
        public async Task<IActionResult> EnviarRecuperacionReal([FromForm] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("📭 El correo es obligatorio.");

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { userId = user.Id, code = encodedToken, email = email },
                    protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(
                    email,
                    "🔐 Restablecer clave - FODUN Reservas",
                    $"<p>Hola,</p><p>Haz clic en el siguiente enlace para restablecer tu clave:</p><p><a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Restablecer clave</a></p>");
            }

            // Siempre redirige a la vista de confirmación, sin revelar si el usuario existe o no
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }


        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        // =========== RESET PASSWORD ==========


        [HttpGet]
        public IActionResult ResetPassword(string code = null, string email = null)
        {
            if (code == null || email == null)
                return BadRequest("Se requiere un código y un email para restablecer la clave.");

            var model = new ResetPasswordViewModel
            {
                Code = code,
                Email = email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction(nameof(ResetPasswordConfirmation));

            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
            var result = await _userManager.ResetPasswordAsync(user, decodedCode, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(ResetPasswordConfirmation));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
