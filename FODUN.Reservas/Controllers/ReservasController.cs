using FODUN.Reservas.Models;
using FODUN.Reservas.Services;
using FODUN.Reservas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using System.Security.Claims; 

namespace FODUN.Reservas.Controllers
{
    [Authorize] // Requiere que el usuario esté autenticado para acceder a este controlador
    public class ReservasController : Controller
    {
        private readonly IReservaService _reservaService;
        private readonly UserManager<ApplicationUser> _userManager; // Para obtener el ID del usuario actual
        private readonly IEmailSender _emailSender;

        public ReservasController(IReservaService reservaService, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _reservaService = reservaService;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: /Reservas/Buscar
        [HttpGet]
        [AllowAnonymous] 
        public IActionResult Buscar()
        {
            return View(new BusquedaDisponibilidadViewModel());
        }

        // POST: /Reservas/Buscar
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buscar(BusquedaDisponibilidadViewModel model)
        {
            model.SearchAttempted = true;

            if (ModelState.IsValid)
            {
                if (model.FechaLlegada >= model.FechaSalida)
                {
                    ModelState.AddModelError(string.Empty, "La fecha de salida debe ser posterior a la fecha de llegada.");
                }
                else
                {

                    model.ResultadosBusqueda = await _reservaService.BuscarAlojamientosDisponiblesYPersonas(model.FechaLlegada, model.FechaSalida, model.NumeroPersonas);

                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Crear(int alojamientoId, DateTime fechaLlegada, DateTime fechaSalida)
        {
            if (alojamientoId == 0 || fechaLlegada == DateTime.MinValue || fechaSalida == DateTime.MinValue)
            {
                TempData["ErrorMessage"] = "Datos insuficientes para crear una reserva. Por favor, asegúrese de seleccionar fechas y alojamiento.";
                return RedirectToAction(nameof(Buscar));
            }

            var alojamiento = await _reservaService.GetAlojamientoById(alojamientoId);
            if (alojamiento == null)
            {
                TempData["ErrorMessage"] = "Alojamiento no encontrado.";
                return RedirectToAction(nameof(Buscar));
            }

            
            int defaultNumeroPersonas = 1; 
            int defaultNumeroUnidadesReservadas = 1;
            bool defaultIncluyeLavanderia = false; 
            int defaultNumeroAcompanantesVisitaDia = 0; 

            var (valorTotalEstimado, mensajeError) = await _reservaService.CalcularTarifaTotalReserva(
                alojamientoId,
                fechaLlegada, 
                fechaSalida,  
                defaultNumeroPersonas,
                defaultNumeroUnidadesReservadas,
                defaultIncluyeLavanderia,
                defaultNumeroAcompanantesVisitaDia
            );

            if (!string.IsNullOrEmpty(mensajeError))
            {
                TempData["ErrorMessage"] = "Error al calcular la tarifa inicial: " + mensajeError;

            }

            var model = new CrearReservaViewModel
            {
                AlojamientoId = alojamiento.AlojamientoId,
                AlojamientoNombre = alojamiento.Nombre,
                UbicacionNombre = alojamiento.Ubicacion?.Nombre,
                FechaInicio = fechaLlegada,
                FechaFin = fechaSalida,
                NumeroPersonas = defaultNumeroPersonas, 
                NumeroUnidadesReservadas = defaultNumeroUnidadesReservadas, 
                IncluyeLavanderia = defaultIncluyeLavanderia,
                NumeroAcompanantesVisitaDia = defaultNumeroAcompanantesVisitaDia, 
                ValorTotalEstimado = valorTotalEstimado, 
                ValorTotal = valorTotalEstimado, 
                Notas = "" 
            };

            
            if (alojamiento.Ubicacion == null)
            {
                alojamiento.Ubicacion = await _reservaService.GetUbicacionById(alojamiento.UbicacionId);
                model.UbicacionNombre = alojamiento.Ubicacion?.Nombre;
            }

            return View(model);
        }

        // POST: /Reservas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearReservaViewModel model)
        {
            var alojamiento = await _reservaService.GetAlojamientoById(model.AlojamientoId);
            if (alojamiento == null)
            {
                ModelState.AddModelError("", "Alojamiento no encontrado.");
                return View(model);
            }
            model.AlojamientoNombre = alojamiento.Nombre; 

            var userIdString = _userManager.GetUserId(User);
            int currentUserId;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out currentUserId))
            {
                TempData["ErrorMessage"] = "Error de seguridad: No se pudo identificar al usuario. Por favor, inicie sesión de nuevo.";
                return RedirectToAction("Login", "Account");
            }


            model.ApplicationUserId = currentUserId;

            var (valorTotalCalculado, mensajeErrorCalculo) = await _reservaService.CalcularTarifaTotalReserva(
                model.AlojamientoId,
                model.FechaInicio,
                model.FechaFin,
                model.NumeroPersonas,
                model.NumeroUnidadesReservadas,
                model.IncluyeLavanderia,
                model.NumeroAcompanantesVisitaDia
            );

            if (!string.IsNullOrEmpty(mensajeErrorCalculo))
            {
                ModelState.AddModelError("", "Error al recalcular la tarifa: " + mensajeErrorCalculo);
                model.ValorTotal = valorTotalCalculado;
                return View(model);
            }

            model.ValorTotal = valorTotalCalculado;


            model.ValorTotalEstimado = valorTotalCalculado;


            if (ModelState.IsValid)
            {
                var reserva = new Reserva
                {
                    ApplicationUserId = currentUserId, 
                    AlojamientoId = model.AlojamientoId,
                    FechaInicio = model.FechaInicio,
                    FechaFin = model.FechaFin,
                    NumeroPersonas = model.NumeroPersonas,
                    ValorTotal = model.ValorTotal,
                    Notas = model.Notas, 
                    EstadoReserva = "Confirmada" 
                };

                try
                {
                    var createdReserva = await _reservaService.CrearReserva(reserva);
                    TempData["SuccessMessage"] = $"Reserva creada con éxito. ID: {createdReserva.ReservaId}";
                    var user = await _userManager.FindByIdAsync(userIdString);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        string asunto = "📅 Confirmación de Reserva - FODUN";
                        string cuerpo = $@"
                            <p>Hola {user.NombreCompleto},</p>
                            <p>Tu reserva ha sido creada exitosamente. Aquí están los detalles:</p>
                            <ul>
                                <li><strong>Reserva ID:</strong> {createdReserva.ReservaId}</li>
                                <li><strong>Alojamiento:</strong> {model.AlojamientoNombre}</li>
                                <li><strong>Ubicación:</strong> {model.UbicacionNombre}</li>
                                <li><strong>Fechas:</strong> {model.FechaInicio:dd/MM/yyyy} - {model.FechaFin:dd/MM/yyyy}</li>
                                <li><strong>Personas:</strong> {model.NumeroPersonas}</li>
                                <li><strong>Valor Total:</strong> ${model.ValorTotal:N0}</li>
                            </ul>
                            <p>Gracias por usar nuestro sistema.</p>
                            <p>Equipo FODUN</p>
                        ";

                        await _emailSender.SendEmailAsync(user.Email, asunto, cuerpo);
                    }

                    return RedirectToAction(nameof(Detalles), new { id = createdReserva.ReservaId });
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message); 
                }
                catch (Exception ex)
                {
                    if (ex is Microsoft.EntityFrameworkCore.DbUpdateException dbEx && dbEx.InnerException != null)
                    {
                        ModelState.AddModelError("", "Ocurrió un error al crear la reserva: " + dbEx.InnerException.Message);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocurrió un error al crear la reserva: " + ex.Message);
                    }
                }
            }

            return View(model);
        }

        // GET: /Reservas/Detalles/5
        public async Task<IActionResult> Detalles(int id)
        {
            var reserva = await _reservaService.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound();
            }

            // Verificar si el usuario actual es el propietario de la reserva o un administrador
            var currentUserId = int.Parse(_userManager.GetUserId(User));
            if (reserva.ApplicationUserId != currentUserId && !User.IsInRole("Administrador"))
            {
                return Forbid(); 
            }

            return View(reserva);
        }

        // GET: /Reservas/MisReservas 
        [HttpGet]
        public async Task<IActionResult> MisReservas()
        {
            var userId = _userManager.GetUserId(User);
            if (!int.TryParse(userId, out int currentUserId))
            {
                TempData["ErrorMessage"] = "Error al obtener el ID del usuario.";
                return View(new List<Reserva>()); 
            }

            var reservas = await _reservaService.GetReservasByUserId(currentUserId);
            return View(reservas);
        }


        // GET: /Reservas/Editar/5
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            
            var reserva = await _reservaService.GetReservaById(id); 
            if (reserva == null)
            {
                return NotFound();
            }

            var currentUserId = int.Parse(_userManager.GetUserId(User));
            if (reserva.ApplicationUserId != currentUserId && !User.IsInRole("Administrador"))
            {
                return Forbid();
            }

            var model = new EditarReservaViewModel
            {
                ReservaId = reserva.ReservaId,
                AlojamientoId = reserva.AlojamientoId,
                AlojamientoNombre = reserva.Alojamiento?.Nombre, 
                UbicacionNombre = reserva.Alojamiento?.Ubicacion?.Nombre, 
                FechaInicio = reserva.FechaInicio,
                FechaFin = reserva.FechaFin,
                NumeroPersonas = reserva.NumeroPersonas,
                ValorTotal = reserva.ValorTotal,
                ValorTotalEstimado = reserva.ValorTotal, //  Inicializar el valor estimado con el total actual
                EstadoReserva = reserva.EstadoReserva,
                ApplicationUserId = reserva.ApplicationUserId,
                Notas = reserva.Notas, //  Cargar las notas existentes

                
                NumeroUnidadesReservadas = 1, 
                IncluyeLavanderia = false,   
                NumeroAcompanantesVisitaDia = 0 
            };

            return View(model);
        }

        // POST: /Reservas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, EditarReservaViewModel model)
        {
            if (id != model.ReservaId)
            {
                return NotFound();
            }

            var reservaToUpdate = await _reservaService.GetReservaById(id); 
            if (reservaToUpdate == null)
            {
                return NotFound();
            }

            var currentUserId = int.Parse(_userManager.GetUserId(User));
            if (reservaToUpdate.ApplicationUserId != currentUserId && !User.IsInRole("Administrador"))
            {
                return Forbid();
            }

            reservaToUpdate.FechaInicio = model.FechaInicio;
            reservaToUpdate.FechaFin = model.FechaFin;
            reservaToUpdate.NumeroPersonas = model.NumeroPersonas;
            reservaToUpdate.EstadoReserva = model.EstadoReserva;
            reservaToUpdate.Notas = model.Notas; 

            var (valorTotalCalculado, mensajeErrorCalculo) = await _reservaService.CalcularTarifaTotalReserva(
                reservaToUpdate.AlojamientoId,
                reservaToUpdate.FechaInicio,
                reservaToUpdate.FechaFin,
                reservaToUpdate.NumeroPersonas,
                model.NumeroUnidadesReservadas,
                model.IncluyeLavanderia,
                model.NumeroAcompanantesVisitaDia
            );

            if (!string.IsNullOrEmpty(mensajeErrorCalculo))
            {
                ModelState.AddModelError("", "Error al recalcular la tarifa: " + mensajeErrorCalculo);
                model.AlojamientoNombre = reservaToUpdate.Alojamiento?.Nombre; 
                model.UbicacionNombre = reservaToUpdate.Alojamiento?.Ubicacion?.Nombre; 
                model.ValorTotal = valorTotalCalculado; 
                model.ValorTotalEstimado = valorTotalCalculado; 
                return View(model);
            }
            reservaToUpdate.ValorTotal = valorTotalCalculado; 

            if (ModelState.IsValid)
            {
                try
                {
                    bool updated = await _reservaService.ActualizarReserva(reservaToUpdate);
                    if (updated)
                    {
                        TempData["SuccessMessage"] = "Reserva actualizada con éxito.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "La reserva no pudo ser actualizada.";
                    }
                    return RedirectToAction(nameof(Detalles), new { id = reservaToUpdate.ReservaId });
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    if (ex is Microsoft.EntityFrameworkCore.DbUpdateException dbEx && dbEx.InnerException != null)
                    {
                        ModelState.AddModelError("", "Ocurrió un error al actualizar la reserva: " + dbEx.InnerException.Message);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocurrió un error al actualizar la reserva: " + ex.Message);
                    }
                }
            }

            model.AlojamientoNombre = reservaToUpdate.Alojamiento?.Nombre; 
            model.UbicacionNombre = reservaToUpdate.Alojamiento?.Ubicacion?.Nombre; 
            model.ValorTotalEstimado = model.ValorTotal; 
            return View(model);
        }

        // GET: /Reservas/Eliminar/5
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var reserva = await _reservaService.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound();
            }

            var currentUserId = int.Parse(_userManager.GetUserId(User));
            if (reserva.ApplicationUserId != currentUserId && !User.IsInRole("Administrador"))
            {
                return Forbid();
            }

            return View(reserva);
        }

        // POST: /Reservas/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var reserva = await _reservaService.GetReservaById(id);
            if (reserva == null)
            {
                return NotFound();
            }

            var currentUserId = int.Parse(_userManager.GetUserId(User));
            if (reserva.ApplicationUserId != currentUserId && !User.IsInRole("Administrador"))
            {
                return Forbid();
            }

            try
            {
                bool deleted = await _reservaService.EliminarReserva(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Reserva eliminada con éxito.";
                }
                else
                {
                    TempData["ErrorMessage"] = "La reserva no pudo ser eliminada.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar la reserva: " + ex.Message;
            }

            return RedirectToAction(nameof(MisReservas));
        }

        // GET: /Reservas/Gestionar (Para Administradores, muestra todas las reservas)
        [HttpGet]
        [Authorize(Roles = "Administrador")] 
        public async Task<IActionResult> Gestionar()
        {
            var reservas = await _reservaService.GetAllReservas();
            return View(reservas);
        }
        // Endpoint para el cálculo de tarifa via AJAX
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CalcularTarifa([FromBody] CrearReservaViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Datos de entrada no válidos." });
            }

            
            var (valorTotal, mensajeError) = await _reservaService.CalcularTarifaTotalReserva(
                model.AlojamientoId,
                model.FechaInicio,
                model.FechaFin,
                model.NumeroPersonas,
                model.NumeroUnidadesReservadas,
                model.IncluyeLavanderia,
                model.NumeroAcompanantesVisitaDia
            );

            if (string.IsNullOrEmpty(mensajeError))
            {
                return Json(new { success = true, valorTotal = valorTotal });
            }
            else
            {
                return Json(new { success = false, message = mensajeError });
            }
        }
    }

}